using SecuGen.FDxSDKPro.Windows;
using System.Runtime.InteropServices;
using System.Text;

namespace sgdm
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>

        private SGFingerPrintManager m_FPM = null;

        private bool m_LedOn = false;
        private Int32 m_ImageWidth;
        private Int32 m_ImageHeight;
        private Byte[] m_RegMin1 = null;
        private Byte[] m_RegMin2;
        private Byte[] m_VrfMin;
        private SGFPMDeviceList[] m_DevList; // Used for EnumerateDevice

        public MainForm()
        {
            InitializeComponent();
        }

        ///////////////////////
        /// Create SGFingerPrintManager Object
        /// new SGFingerPrintManager()
        private void MainForm_Load(object sender, System.EventArgs e)
        {
            m_LedOn = false;

            m_RegMin1 = new Byte[400];
            m_RegMin2 = new Byte[400];
            m_VrfMin = new Byte[400];

            comboBoxSecuLevel_R.SelectedIndex = 4;
            comboBoxSecuLevel_V.SelectedIndex = 3;
            comboBoxComPorts.SelectedIndex = 5;    // com6

            EnableButtons(false);

            m_FPM = new SGFingerPrintManager();
            EnumerateBtn_Click(sender, e);
        }

        ///////////////////////
        /// EnumerateDevice(), GetEnumDeviceInfo()
        /// EnumerateDevice() can be called before Initializing SGFingerPrintManager
        private void EnumerateBtn_Click(object sender, System.EventArgs e)
        {
            Int32 iError;
            string enum_device;

            comboBoxDeviceName.Items.Clear();
            comboBoxDeviceName.ResetText();    // remove text as well

            // Enumerate Device
            iError = m_FPM.EnumerateDevice();

            // Get enumeration info into SGFPMDeviceList
            m_DevList = new SGFPMDeviceList[m_FPM.NumberOfDevice];

            for (int i = 0; i < m_FPM.NumberOfDevice; i++)
            {
                m_DevList[i] = new SGFPMDeviceList();
                m_FPM.GetEnumDeviceInfo(i, m_DevList[i]);
                enum_device = m_DevList[i].DevName.ToString() + " : " + m_DevList[i].DevID;
                comboBoxDeviceName.Items.Add(enum_device);
            }

            if (comboBoxDeviceName.Items.Count > 0)
            {
                // Add Auto Selection
                enum_device = "Auto Selection";
                comboBoxDeviceName.Items.Add(enum_device);

                comboBoxDeviceName.SelectedIndex = 0;  //First selected one
            }

        }


        ///////////////////////
        // Initialize SGFingerprint manage with device name
        // Init(), OpenDeice()
        private void OpenDeviceBtn_Click(object sender, System.EventArgs e)
        {
            if (m_FPM.NumberOfDevice == 0)
                return;

            SGFPMDeviceName device_name;
            Int32 device_id;

            Int32 numberOfDevices = comboBoxDeviceName.Items.Count;
            Int32 deviceSelected = comboBoxDeviceName.SelectedIndex;
            Boolean autoSelection = (deviceSelected == (numberOfDevices - 1));  // Last index

            if (autoSelection)
            {
                // Order of search: Hamster IV(HFDU04) -> Plus(HFDU03) -> III (HFDU02)
                device_name = SGFPMDeviceName.DEV_AUTO;

                device_id = (Int32)(SGFPMPortAddr.USB_AUTO_DETECT);
            }
            else
            {
                device_name = m_DevList[deviceSelected].DevName;
                device_id = m_DevList[deviceSelected].DevID;
            }

            Int32 iError = OpenDevice(device_name, device_id);

            if (iError == (Int32)SGFPMError.ERROR_NONE)
            {
                GroupBoxBrightness.Enabled = true;
                ConfigBtn.Enabled = true;
            }

        }

        ///////////////////////
        /// Oepn SDA device throught comport
        private void OpenSdaBtn_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            SGFPMDeviceName device_name = SGFPMDeviceName.DEV_FDUSDA;
            Int32 port_no = comboBoxComPorts.SelectedIndex + 1;

            Int32 iError = OpenDevice(device_name, port_no);

            if (iError == (Int32)SGFPMError.ERROR_NONE)
            {
                // These are not supported for SDA device
                GroupBoxBrightness.Enabled = false;
                ConfigBtn.Enabled = false;
            }

            Cursor.Current = Cursors.Default;
        }

        private Int32 OpenDevice(SGFPMDeviceName device_name, Int32 device_id)
        {
            Int32 iError = m_FPM.Init(device_name);
            iError = m_FPM.OpenDevice(device_id);

            CheckBoxAutoOn.Enabled = false;
            if (iError == (Int32)SGFPMError.ERROR_NONE)
            {
                //GetBtn_Click(sender, e);
                GetBtn_Click(null, null);
                StatusBar.Text = "Initialization Success";
                EnableButtons(true);

                // FDU03, FDU04 or higher
                if (device_name >= SGFPMDeviceName.DEV_FDU03)
                    CheckBoxAutoOn.Enabled = true;
            }
            else
                DisplayError("OpenDevice()", iError);
            return iError;
        }

        private void CloseDevice()
        {
            m_FPM.CloseDevice();
        }

        ///////////////////////
        /// SetLedOn()
        private void LedBtn_Click(object sender, System.EventArgs e)
        {
            m_LedOn = !m_LedOn;
            m_FPM.SetLedOn(m_LedOn);
        }

        ///////////////////////
        /// Configure()
        private void ConfigBtn_Click(object sender, System.EventArgs e)
        {
            m_FPM.Configure((int)this.Handle);
        }

        ///////////////////////
        /// GetImage()
        private void GetImageBtn_Click(object sender, System.EventArgs e)
        {
            Int32 iError;
            Int32 elap_time;
            Byte[] fp_image;

            Cursor.Current = Cursors.WaitCursor;

            elap_time = Environment.TickCount;
            fp_image = new Byte[m_ImageWidth * m_ImageHeight];

            iError = m_FPM.GetImage(fp_image);

            if (iError == (Int32)SGFPMError.ERROR_NONE)
            {
                elap_time = Environment.TickCount - elap_time;
                DrawImage(fp_image, pictureBox1);
                StatusBar.Text = "Capture Time : " + elap_time + " ms";
            }
            else
                DisplayError("GetImage()", iError);

            Cursor.Current = Cursors.Default;
        }


        ///////////////////////
        /// GetImageEx()
        private void GetLiveImageBtn_Click(object sender, System.EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            Int32 iError;
            Int32 timeout;
            Int32 quality;
            Byte[] fp_image;
            Int32 elap_time;

            timeout = Convert.ToInt32(textTimeout.Text);
            quality = Convert.ToInt32(textImgQuality.Text);
            fp_image = new Byte[m_ImageWidth * m_ImageHeight];
            elap_time = Environment.TickCount;

            iError = m_FPM.GetImageEx(fp_image, timeout, this.pictureBox1.Handle.ToInt32(), quality);

            if (iError == 0)
            {
                elap_time = Environment.TickCount - elap_time;
                StatusBar.Text = "Capture Time : " + elap_time + "millisec";
            }
            else
                DisplayError("GetLiveImageEx()", iError);

            Cursor.Current = Cursors.Default;
        }


        ///////////////////////
        /// GetImage(), GetImageQuality(), CreateTemplate
        private void BtnCapture1_Click(object sender, System.EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            Int32 iError;
            Byte[] fp_image;
            Int32 img_qlty;

            fp_image = new Byte[m_ImageWidth * m_ImageHeight];
            img_qlty = 0;

            iError = m_FPM.GetImage(fp_image);

            m_FPM.GetImageQuality(m_ImageWidth, m_ImageHeight, fp_image, ref img_qlty);
            progressBar_R1.Value = img_qlty;

            if (iError == (Int32)SGFPMError.ERROR_NONE)
            {
                DrawImage(fp_image, pictureBoxR1);
                iError = m_FPM.CreateTemplate(fp_image, m_RegMin1);

                if (iError == (Int32)SGFPMError.ERROR_NONE)
                    StatusBar.Text = "First image is captured";
                else
                    DisplayError("CreateTemplate()", iError);
            }
            else
                DisplayError("GetImage()", iError);

            Cursor.Current = Cursors.Default;
        }

        ///////////////////////
        /// GetImage(), GetImageQuality(), CreateTemplate
        private void BtnCapture2_Click(object sender, System.EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            Int32 iError;
            Byte[] fp_image;
            Int32 img_qlty;

            fp_image = new Byte[m_ImageWidth * m_ImageHeight];
            img_qlty = 0;

            iError = m_FPM.GetImage(fp_image);
            m_FPM.GetImageQuality(m_ImageWidth, m_ImageHeight, fp_image, ref img_qlty);
            progressBar_R2.Value = img_qlty;

            if (iError == (Int32)SGFPMError.ERROR_NONE)
            {
                DrawImage(fp_image, pictureBoxR2);
                iError = m_FPM.CreateTemplate(fp_image, m_RegMin2);

                if (iError == (Int32)SGFPMError.ERROR_NONE)
                    StatusBar.Text = "Second image is captured";
                else
                    DisplayError("CreateTemplate()", iError);
            }
            else
                DisplayError("GetImage()", iError);

            Cursor.Current = Cursors.Default;
        }

        ///////////////////////
        /// GetImage(), GetImageQuality(), CreateTemplate
        private void BtnCapture3_Click(object sender, System.EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            Int32 iError;
            Byte[] fp_image;
            Int32 img_qlty;

            fp_image = new Byte[m_ImageWidth * m_ImageHeight];
            img_qlty = 0;

            iError = m_FPM.GetImage(fp_image);

            m_FPM.GetImageQuality(m_ImageWidth, m_ImageHeight, fp_image, ref img_qlty);
            progressBar_V1.Value = img_qlty;

            if (iError == (Int32)SGFPMError.ERROR_NONE)
            {
                DrawImage(fp_image, pictureBoxV1);
                iError = m_FPM.CreateTemplate(null, fp_image, m_VrfMin);

                if (iError == (Int32)SGFPMError.ERROR_NONE)
                    StatusBar.Text = "Image for verification is captured";
                else
                    DisplayError("CreateTemplate()", iError);
            }
            else
                DisplayError("GetImage()", iError);

            Cursor.Current = Cursors.Default;
        }


        ///////////////////////
        /// MatchTemplate(), GetMatchingScore()
        private void BtnRegister_Click(object sender, System.EventArgs e)
        {
            Int32 iError;
            bool matched = false;
            Int32 match_score = 0;
            SGFPMSecurityLevel secu_level; //

            secu_level = (SGFPMSecurityLevel)comboBoxSecuLevel_R.SelectedIndex;

            iError = m_FPM.MatchTemplate(m_RegMin1, m_RegMin2, secu_level, ref matched);
            iError = m_FPM.GetMatchingScore(m_RegMin1, m_RegMin2, ref match_score);

            if (iError == (Int32)SGFPMError.ERROR_NONE)
            {
                if (matched)
                    StatusBar.Text = "Registration Success, Matching Score: " + match_score;
                else
                    StatusBar.Text = "Registration Failed";
            }
            else
                DisplayError("GetMatchingScore()", iError);

        }

        ///////////////////////
        /// MatchTemplate(), GetMatchingScore()
        private void BtnVerify_Click(object sender, System.EventArgs e)
        {
            Int32 iError;
            bool matched1 = false;
            bool matched2 = false;
            SGFPMSecurityLevel secu_level;

            secu_level = (SGFPMSecurityLevel)comboBoxSecuLevel_V.SelectedIndex;

            iError = m_FPM.MatchTemplate(m_RegMin1, m_VrfMin, secu_level, ref matched1);
            iError = m_FPM.MatchTemplate(m_RegMin2, m_VrfMin, secu_level, ref matched2);

            if (iError == (Int32)SGFPMError.ERROR_NONE)
            {
                if (matched1 & matched2)
                    StatusBar.Text = "Verification Success";
                else
                    StatusBar.Text = "Verification Failed";
            }
            else
                DisplayError("MatchTemplate()", iError);
        }


        ///////////////////////
        /// GetDeviceInfo()
        private void GetBtn_Click(object sender, System.EventArgs e)
        {
            SGFPMDeviceInfoParam pInfo = new SGFPMDeviceInfoParam();
            Int32 iError = m_FPM.GetDeviceInfo(pInfo);

            if (iError == (Int32)SGFPMError.ERROR_NONE)
            {
                m_ImageWidth = pInfo.ImageWidth;
                m_ImageHeight = pInfo.ImageHeight;

                textDeviceID.Text = Convert.ToString(pInfo.DeviceID);
                textImageDPI.Text = Convert.ToString(pInfo.ImageDPI);
                textFWVersion.Text = Convert.ToString(pInfo.FWVersion, 16);

                ASCIIEncoding encoding = new ASCIIEncoding();
                textSerialNum.Text = encoding.GetString(pInfo.DeviceSN);

                textImageHeight.Text = Convert.ToString(pInfo.ImageHeight);
                textImageWidth.Text = Convert.ToString(pInfo.ImageWidth);
                textBrightness.Text = Convert.ToString(pInfo.Brightness);
                textContrast.Text = Convert.ToString(pInfo.Contrast);
                textGain.Text = Convert.ToString(pInfo.Gain);

                BrightnessUpDown.Value = pInfo.Brightness;
            }
        }

        ///////////////////////
        private void SetBrightnessBtn_Click(object sender, System.EventArgs e)
        {
            Int32 iError;
            Int32 brightness;

            brightness = (int)BrightnessUpDown.Value;
            iError = m_FPM.SetBrightness(brightness);
            if (iError == (Int32)SGFPMError.ERROR_NONE)
            {
                StatusBar.Text = "SetBrightness success";
                GetBtn_Click(sender, e);
            }
            else
                DisplayError("SetBrightness()", iError);

        }


        ///////////////////////
        private void CheckBoxAutoOn_CheckedChanged(object sender, System.EventArgs e)
        {
            if (CheckBoxAutoOn.Checked)
                m_FPM.EnableAutoOnEvent(true, (int)this.Handle);
            else
                m_FPM.EnableAutoOnEvent(false, 0);
        }

        ///////////////////////
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class LPDeviceInfoParam
        {
            public Int32 DeviceID;         // 0 - 9

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
            public Byte[] DeviceSN;         // Device Serial Number, Length of SN = 15, Total 16bytes    

            public Int32 ComPort;          // Parallel device=>PP address, USB device=>USB(0x3BC+1)
            public Int32 ComSpeed;         // Parallel device=>PP mode, USB device=>0 
            public Int32 ImageWidth;       // Image Width
            public Int32 ImageHeight;      // Image Height
            public Int32 Contrast;        // 0 ~ 100
            public Int32 Brightness;       // 0 ~ 100
            public Int32 Gain;             // Dependent on each device
            public Int32 ImageDPI;         // DPI
            public Int32 FWVersion;        // FWVersion
        }
        protected override void WndProc(ref Message message)
        {
            if (message.Msg == (int)SGFPMMessages.DEV_AUTOONEVENT)
            {
                if (message.WParam.ToInt32() == (Int32)SGFPMAutoOnEvent.FINGER_ON)
                {
                    LPDeviceInfoParam lparam = new LPDeviceInfoParam();
                    Marshal.PtrToStructure(message.LParam, lparam);

                    var serialNum = Encoding.ASCII.GetString(lparam.DeviceSN, 0, lparam.DeviceSN.Length);
                    StatusBar.Text = "Device Message: Finger On" + ", " + serialNum;

                }
                else if (message.WParam.ToInt32() == (Int32)SGFPMAutoOnEvent.FINGER_OFF)
                {
                    StatusBar.Text = "Device Message: Finger Off";
                }
            }
            base.WndProc(ref message);
        }

        ///////////////////////
        private void DrawImage(Byte[] imgData, PictureBox picBox)
        {
            int colorval;
            Bitmap bmp = new Bitmap(m_ImageWidth, m_ImageHeight);
            picBox.Image = (Image)bmp;

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    colorval = (int)imgData[(j * m_ImageWidth) + i];
                    bmp.SetPixel(i, j, Color.FromArgb(colorval, colorval, colorval));
                }
            }
            picBox.Refresh();
        }

        ///////////////////////
        private void EnableButtons(bool enable)
        {
            ConfigBtn.Enabled = enable;
            GetImageBtn.Enabled = enable;
            GetLiveImageBtn.Enabled = enable;
            BtnCapture1.Enabled = enable;
            BtnCapture2.Enabled = enable;
            BtnCapture3.Enabled = enable;
            BtnRegister.Enabled = enable;
            BtnVerify.Enabled = enable;
            GetBtn.Enabled = enable;
            SetBrightnessBtn.Enabled = enable;
        }

        ///////////////////////
        void DisplayError(string funcName, int iError)
        {
            string text = "";

            switch (iError)
            {
                case 0:                             //SGFDX_ERROR_NONE				= 0,
                    text = "Error none";
                    break;

                case 1:                             //SGFDX_ERROR_CREATION_FAILED	= 1,
                    text = "Can not create object";
                    break;

                case 2:                             //   SGFDX_ERROR_FUNCTION_FAILED	= 2,
                    text = "Function Failed";
                    break;

                case 3:                             //   SGFDX_ERROR_INVALID_PARAM	= 3,
                    text = "Invalid Parameter";
                    break;

                case 4:                          //   SGFDX_ERROR_NOT_USED			= 4,
                    text = "Not used function";
                    break;

                case 5:                                //SGFDX_ERROR_DLLLOAD_FAILED	= 5,
                    text = "Can not create object";
                    break;

                case 6:                                //SGFDX_ERROR_DLLLOAD_FAILED_DRV	= 6,
                    text = "Can not load device driver";
                    break;
                case 7:                                //SGFDX_ERROR_DLLLOAD_FAILED_ALGO = 7,
                    text = "Can not load sgfpamx.dll";
                    break;

                case 51:                //SGFDX_ERROR_SYSLOAD_FAILED	   = 51,	// system file load fail
                    text = "Can not load driver kernel file";
                    break;

                case 52:                //SGFDX_ERROR_INITIALIZE_FAILED  = 52,   // chip initialize fail
                    text = "Failed to initialize the device";
                    break;

                case 53:                //SGFDX_ERROR_LINE_DROPPED		   = 53,   // image data drop
                    text = "Data transmission is not good";
                    break;

                case 54:                //SGFDX_ERROR_TIME_OUT			   = 54,   // getliveimage timeout error
                    text = "Time out";
                    break;

                case 55:                //SGFDX_ERROR_DEVICE_NOT_FOUND	= 55,   // device not found
                    text = "Device not found";
                    break;

                case 56:                //SGFDX_ERROR_DRVLOAD_FAILED	   = 56,   // dll file load fail
                    text = "Can not load driver file";
                    break;

                case 57:                //SGFDX_ERROR_WRONG_IMAGE		   = 57,   // wrong image
                    text = "Wrong Image";
                    break;

                case 58:                //SGFDX_ERROR_LACK_OF_BANDWIDTH  = 58,   // USB Bandwith Lack Error
                    text = "Lack of USB Bandwith";
                    break;

                case 59:                //SGFDX_ERROR_DEV_ALREADY_OPEN	= 59,   // Device Exclusive access Error
                    text = "Device is already opened";
                    break;

                case 60:                //SGFDX_ERROR_GETSN_FAILED		   = 60,   // Fail to get Device Serial Number
                    text = "Device serial number error";
                    break;

                case 61:                //SGFDX_ERROR_UNSUPPORTED_DEV		   = 61,   // Unsupported device
                    text = "Unsupported device";
                    break;

                // Extract & Verification error
                case 101:                //SGFDX_ERROR_FEAT_NUMBER		= 101, // utoo small number of minutiae
                    text = "The number of minutiae is too small";
                    break;

                case 102:                //SGFDX_ERROR_INVALID_TEMPLATE_TYPE		= 102, // wrong template type
                    text = "Template is invalid";
                    break;

                case 103:                //SGFDX_ERROR_INVALID_TEMPLATE1		= 103, // wrong template type
                    text = "1st template is invalid";
                    break;

                case 104:                //SGFDX_ERROR_INVALID_TEMPLATE2		= 104, // vwrong template type
                    text = "2nd template is invalid";
                    break;

                case 105:                //SGFDX_ERROR_EXTRACT_FAIL		= 105, // extraction fail
                    text = "Minutiae extraction failed";
                    break;

                case 106:                //SGFDX_ERROR_MATCH_FAIL		= 106, // matching  fail
                    text = "Matching failed";
                    break;

            }

            text = funcName + " Error # " + iError + " :" + text;
            StatusBar.Text = text;
        }

    }
}


