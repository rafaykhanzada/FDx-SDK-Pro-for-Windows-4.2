using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;
using SecuGen.FDxSDKPro.Windows;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace sgdm
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public partial class MainForm : System.Windows.Forms.Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>

        private SGFingerPrintManager m_FPM;
        private enum CapturePurpose { Registration, Registration_1, Registration_2, Verification };
        private const int MinImageQualityRegistration = 60; // 70
        private const int MinImageQualityVerification = 50;

        private const int SIZE_OF_FEATURE_BUF = 400;    // SG400 template
        private const int NUM_OF_REGS = 5;              // 5 through 10 recommended for U-Air
        private const int MIN_NUM_OF_MATCHED = 2;       // 2..NUM_OF_REGS

        private bool m_LedOn = false;
        private Int32 m_ImageWidth;
        private Int32 m_ImageHeight;

        private List<byte[]> m_RegMins = new List<byte[]>();
        private Byte[] m_VrfMin;
        private List<PictureBox> m_thumbnails = new List<PictureBox>(); // thumbnails for registration
        private bool m_RegDone = false;
        private SGFPMDeviceList[] m_DevList; // Used for EnumerateDevice
        SGFPMDeviceName m_DevName;

        ///////////////////////
        /// Create SGFingerPrintManager Object
        /// new SGFingerPrintManager()
        private void MainForm_Load(object sender, System.EventArgs e)
        {
            m_LedOn = false;

            m_VrfMin = new Byte[SIZE_OF_FEATURE_BUF];

            comboBoxSecuLevel_R.SelectedIndex = 4;
            comboBoxSecuLevel_V.SelectedIndex = 3;
            tabControl1.SelectedIndex = 1;         // Select Regster/Verify tab by default

            m_thumbnails.Add(pictureBoxThumbnail1);
            m_thumbnails.Add(pictureBoxThumbnail2);
            m_thumbnails.Add(pictureBoxThumbnail3);
            m_thumbnails.Add(pictureBoxThumbnail4);
            m_thumbnails.Add(pictureBoxThumbnail5);

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

            SGFPMDeviceName device_name = SGFPMDeviceName.DEV_UNKNOWN;
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

            m_DevName = device_name;
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
                StatusBar.Text = "Capture Time : " + elap_time + " ms";
            }
            else
                DisplayError("GetLiveImageEx()", iError);

            Cursor.Current = Cursors.Default;
        }

        ///////////////////////
        private void DrawThumbnail(byte[] fpImage)
        {
            int i = m_RegMins.Count;
            DrawImage(fpImage, m_thumbnails[i]);
        }

        ///////////////////////
        private void BtnNewReg_Click(object sender, EventArgs e)
        {
            // empty image
            byte[] emptyImage = Enumerable.Repeat((byte)0xff, m_ImageWidth * m_ImageHeight).ToArray();
            foreach (var thumb in m_thumbnails)
            {
                DrawImage(emptyImage, thumb);
            }
            DrawImage(emptyImage, pictureBoxR1);

            m_RegMins.Clear();
            progressBar_R1.Value = 0;
            m_RegDone = false;
        }

        ///////////////////////
        private void BtnCaptureReg_Click(object sender, EventArgs e)
        {
            if (m_RegMins.Count < NUM_OF_REGS)
            {
                const int min_qlty_register = MinImageQualityRegistration;
                const long timeout = 5000; // 5 seconds
                byte[] newRegMin = new byte[SIZE_OF_FEATURE_BUF];
                byte[] fpImage;
                bool templateCreated = GetFpTemplate(newRegMin, CapturePurpose.Registration, min_qlty_register, timeout, progressBar_R1, pictureBoxR1, out fpImage);
                if (templateCreated)
                {
                    const int numOfMinRegs = 1;
                    int matchScore = 0;
                    bool add = true;
                    if (m_RegMins.Count > 0)
                    {
                        if (MatchTemplates(newRegMin, m_RegMins, numOfMinRegs, ref matchScore))
                        {
                            StatusBar.Text += ", match score = " + matchScore;
                        }
                        else
                        {
                            StatusBar.Text += ", unable to register";
                            add = false;    // Unable to register
                        }
                    }

                    if (add)
                    {
                        DrawThumbnail(fpImage);
                        m_RegMins.Add(newRegMin);
                        StatusBar.Text += " (" + m_RegMins.Count + "/" + NUM_OF_REGS + " registered)";
                    }
                }
            }
        }
        private void BtnCapture3_Click(object sender, System.EventArgs e)
        {
            const int min_qlty_verify = MinImageQualityVerification;
            const long timeout = 5000; // 5 seconds
            byte[] fpImage;
            GetFpTemplate(m_VrfMin, CapturePurpose.Verification, min_qlty_verify, timeout, progressBar_V1, pictureBoxV1, out fpImage);
        }
        private bool GetFpTemplate(byte[] fp_template, CapturePurpose purpose, int min_qlty, long timeout, ProgressBar progressBar, PictureBox pictureBox, out byte[] outImage)
        {
            Cursor.Current = Cursors.WaitCursor;

            Int32 iError;
            Byte[] fp_image;
            int img_qlty = 0;
            bool template_created = false;

            outImage = null;
            fp_image = new Byte[m_ImageWidth * m_ImageHeight];

            long elapsed = 0;
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            m_FPM.EnableSmartCapture(true);
            m_FPM.BeginGetImage();
            do
            {
                iError = m_FPM.GetImage(fp_image);
                if (iError == (Int32)SGFPMError.ERROR_NONE)
                {
                    DrawImage(fp_image, pictureBox);

                    if (m_DevName == SGFPMDeviceName.DEV_FDU10A)
                        m_FPM.GetLastImageQuality(ref img_qlty);    // U-Air only for now.
                    else
                        m_FPM.GetImageQuality(m_ImageWidth, m_ImageHeight, fp_image, ref img_qlty);

                    progressBar.Value = img_qlty;
                    if (img_qlty >= min_qlty)
                    {
                        iError = m_FPM.CreateTemplate(null, fp_image, fp_template);

                        if (iError == (Int32)SGFPMError.ERROR_NONE)
                        {
                            template_created = true;
                            outImage = fp_image;
                            StatusBar.Text = "Image for " + purpose + " is captured with quality " + img_qlty;
                        }
                        else
                        {
                            DisplayError("CreateTemplate()", iError);
                        }
                    }
                    else
                    {
                        StatusBar.Text = "Image Quality = " + img_qlty + ", minimum = " + min_qlty;
                    }

                }
                else
                {
                    //DisplayError("GetImage()", iError);
                    StatusBar.Text = "The captured image is not good.Capturing...";
                }

                StatusBar.Update();

                elapsed = stopwatch.ElapsedMilliseconds;
            } while (!template_created && elapsed <= timeout);

            m_FPM.EndGetImage();

            if (!template_created)
            {
                StatusBar.Text = "The time has expired. Try again...";
            }
            Cursor.Current = Cursors.Default;

            return template_created;
        }

        ///////////////////////
        /// MatchTemplate(), GetMatchingScore()
        private void BtnRegister_Click(object sender, System.EventArgs e)
        {
            // Add somthing when registering.
            m_RegDone = m_RegMins.Count == NUM_OF_REGS;
            if (m_RegDone)
            {
                StatusBar.Text = "Registration Success";
            }
            else
            {
                StatusBar.Text = "Need to register more (" + m_RegMins.Count + "/" + NUM_OF_REGS + " registered)";
            }
        }

        ///////////////////////
        /// MatchTemplate(), GetMatchingScore()
        private void BtnVerify_Click(object sender, System.EventArgs e)
        {
            if (!m_RegDone)
            {
                StatusBar.Text = "Not Registered!";
                return;
            }

            int matchScore = 0;
            bool matched = MatchTemplates(m_VrfMin, m_RegMins, MIN_NUM_OF_MATCHED, ref matchScore);

            if (matched)
                StatusBar.Text = "Verification Success (match score=" + matchScore + ")";
            else
                StatusBar.Text = "Verification Failed";
        }

        ////////////////////////
        private bool MatchTemplates(byte[] fetBuf, List<byte[]> templates, int numOfMinMatched, ref int matchScore)
        {
            int numOfMatched = 0;
            int sumOfScores = 0;
            Int32 iError = (Int32)SGFPMError.ERROR_NONE;
            SGFPMSecurityLevel secu_level = (SGFPMSecurityLevel)comboBoxSecuLevel_V.SelectedIndex;

            foreach (var fetReg in templates)
            {
                bool matched = false;
                iError = m_FPM.MatchTemplate(fetReg, fetBuf, secu_level, ref matched);

                if (iError != (Int32)SGFPMError.ERROR_NONE)
                {
                    StatusBar.Text = "MatchTemplate() failed. Error = " + iError; // just for error message
                }
                else
                {
                    if (matched)
                    {
                        numOfMatched++;

                        int score = 0;
                        m_FPM.GetMatchingScore(fetReg, fetBuf, ref score);
                        sumOfScores += score;

                        if (numOfMatched >= numOfMinMatched)
                        {
                            matchScore = sumOfScores / numOfMatched;    // average matching score
                            return true;    // MATCHED
                        }
                    }
                }
            }
            return false;
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
            BtnCapture3.Enabled = enable;
            BtnRegister.Enabled = enable;
            BtnVerify.Enabled = enable;
            GetBtn.Enabled = enable;
            SetBrightnessBtn.Enabled = enable;

            BtnNewReg.Enabled = enable;
            BtnCaptureReg.Enabled = enable;
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
            StatusBar.Update();
        }
    }
}
