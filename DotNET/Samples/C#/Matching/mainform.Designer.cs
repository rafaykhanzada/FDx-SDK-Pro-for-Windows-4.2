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
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button BtnCapture1;
        private System.Windows.Forms.Button BtnCapture2;
        private System.Windows.Forms.Button BtnCapture3;
        private System.Windows.Forms.PictureBox pictureBoxR2;
        private System.Windows.Forms.PictureBox pictureBoxV1;
        private System.Windows.Forms.PictureBox pictureBoxR1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBrightness;
        private System.Windows.Forms.TextBox textGain;
        private System.Windows.Forms.TextBox textContrast;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button ConfigBtn;
        private System.Windows.Forms.TextBox textImgQuality;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox comboBoxSecuLevel_V;
        private System.Windows.Forms.ComboBox comboBoxSecuLevel_R;
        private System.Windows.Forms.Button GetBtn;
        private System.Windows.Forms.TextBox textDeviceID;
        private System.Windows.Forms.TextBox textSerialNum;
        private System.Windows.Forms.TextBox textImageWidth;
        private System.Windows.Forms.TextBox textImageHeight;
        private System.Windows.Forms.TextBox textImageDPI;
        private System.Windows.Forms.ProgressBar progressBar_R1;
        private System.Windows.Forms.ProgressBar progressBar_R2;
        private System.Windows.Forms.ProgressBar progressBar_V1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textTimeout;
        private System.Windows.Forms.Button BtnRegister;
        private System.Windows.Forms.Button BtnVerify;
        internal System.Windows.Forms.GroupBox GroupBoxBrightness;
        internal System.Windows.Forms.Button SetBrightnessBtn;
        private System.Windows.Forms.TextBox textFWVersion;
        private System.Windows.Forms.Button GetLiveImageBtn;
        private System.Windows.Forms.Button GetImageBtn;
        internal System.Windows.Forms.NumericUpDown BrightnessUpDown;
        private System.Windows.Forms.CheckBox CheckBoxAutoOn;
        private GroupBox groupBoxUsbDevs;
        private Button OpenDeviceBtn;
        private Button EnumerateBtn;
        private Label label1;
        private ComboBox comboBoxDeviceName;
        private GroupBox groupBoxSda;
        private Label label2;
        private ComboBox comboBoxComPorts;
        private Button OpenSdaBtn;
        private System.Windows.Forms.Label StatusBar;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
                if (m_FPM != null)
                {
                    m_FPM.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.CheckBoxAutoOn = new System.Windows.Forms.CheckBox();
            this.GroupBoxBrightness = new System.Windows.Forms.GroupBox();
            this.BrightnessUpDown = new System.Windows.Forms.NumericUpDown();
            this.SetBrightnessBtn = new System.Windows.Forms.Button();
            this.ConfigBtn = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textTimeout = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.textImgQuality = new System.Windows.Forms.TextBox();
            this.GetLiveImageBtn = new System.Windows.Forms.Button();
            this.GetImageBtn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.BtnVerify = new System.Windows.Forms.Button();
            this.BtnRegister = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.comboBoxSecuLevel_V = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxSecuLevel_R = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.progressBar_V1 = new System.Windows.Forms.ProgressBar();
            this.pictureBoxV1 = new System.Windows.Forms.PictureBox();
            this.BtnCapture3 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.progressBar_R2 = new System.Windows.Forms.ProgressBar();
            this.progressBar_R1 = new System.Windows.Forms.ProgressBar();
            this.pictureBoxR2 = new System.Windows.Forms.PictureBox();
            this.pictureBoxR1 = new System.Windows.Forms.PictureBox();
            this.BtnCapture1 = new System.Windows.Forms.Button();
            this.BtnCapture2 = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.GetBtn = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textImageDPI = new System.Windows.Forms.TextBox();
            this.textImageHeight = new System.Windows.Forms.TextBox();
            this.textImageWidth = new System.Windows.Forms.TextBox();
            this.textSerialNum = new System.Windows.Forms.TextBox();
            this.textFWVersion = new System.Windows.Forms.TextBox();
            this.textDeviceID = new System.Windows.Forms.TextBox();
            this.textBrightness = new System.Windows.Forms.TextBox();
            this.textGain = new System.Windows.Forms.TextBox();
            this.textContrast = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.StatusBar = new System.Windows.Forms.Label();
            this.groupBoxUsbDevs = new System.Windows.Forms.GroupBox();
            this.OpenDeviceBtn = new System.Windows.Forms.Button();
            this.EnumerateBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxDeviceName = new System.Windows.Forms.ComboBox();
            this.groupBoxSda = new System.Windows.Forms.GroupBox();
            this.OpenSdaBtn = new System.Windows.Forms.Button();
            this.comboBoxComPorts = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.GroupBoxBrightness.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BrightnessUpDown)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxV1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxR2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxR1)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBoxUsbDevs.SuspendLayout();
            this.groupBoxSda.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(5, 153);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(499, 497);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.CheckBoxAutoOn);
            this.tabPage2.Controls.Add(this.GroupBoxBrightness);
            this.tabPage2.Controls.Add(this.ConfigBtn);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.GetLiveImageBtn);
            this.tabPage2.Controls.Add(this.GetImageBtn);
            this.tabPage2.Controls.Add(this.pictureBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(491, 469);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "  Image  ";
            // 
            // CheckBoxAutoOn
            // 
            this.CheckBoxAutoOn.Enabled = false;
            this.CheckBoxAutoOn.Location = new System.Drawing.Point(14, 438);
            this.CheckBoxAutoOn.Name = "CheckBoxAutoOn";
            this.CheckBoxAutoOn.Size = new System.Drawing.Size(339, 24);
            this.CheckBoxAutoOn.TabIndex = 19;
            this.CheckBoxAutoOn.Text = "Enable AutoOn Event (FDU03, FDU04, or higher)";
            this.CheckBoxAutoOn.CheckedChanged += new System.EventHandler(this.CheckBoxAutoOn_CheckedChanged);
            // 
            // GroupBoxBrightness
            // 
            this.GroupBoxBrightness.Controls.Add(this.BrightnessUpDown);
            this.GroupBoxBrightness.Controls.Add(this.SetBrightnessBtn);
            this.GroupBoxBrightness.Location = new System.Drawing.Point(336, 246);
            this.GroupBoxBrightness.Name = "GroupBoxBrightness";
            this.GroupBoxBrightness.Size = new System.Drawing.Size(144, 182);
            this.GroupBoxBrightness.TabIndex = 18;
            this.GroupBoxBrightness.TabStop = false;
            this.GroupBoxBrightness.Text = "Brightness";
            // 
            // BrightnessUpDown
            // 
            this.BrightnessUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.BrightnessUpDown.Location = new System.Drawing.Point(10, 30);
            this.BrightnessUpDown.Name = "BrightnessUpDown";
            this.BrightnessUpDown.Size = new System.Drawing.Size(52, 23);
            this.BrightnessUpDown.TabIndex = 20;
            this.BrightnessUpDown.Value = new decimal(new int[] {
            70,
            0,
            0,
            0});
            // 
            // SetBrightnessBtn
            // 
            this.SetBrightnessBtn.Location = new System.Drawing.Point(67, 30);
            this.SetBrightnessBtn.Name = "SetBrightnessBtn";
            this.SetBrightnessBtn.Size = new System.Drawing.Size(67, 24);
            this.SetBrightnessBtn.TabIndex = 19;
            this.SetBrightnessBtn.Text = "Apply";
            this.SetBrightnessBtn.Click += new System.EventHandler(this.SetBrightnessBtn_Click);
            // 
            // ConfigBtn
            // 
            this.ConfigBtn.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ConfigBtn.Location = new System.Drawing.Point(389, 15);
            this.ConfigBtn.Name = "ConfigBtn";
            this.ConfigBtn.Size = new System.Drawing.Size(91, 29);
            this.ConfigBtn.TabIndex = 12;
            this.ConfigBtn.Text = "Config...";
            this.ConfigBtn.UseVisualStyleBackColor = false;
            this.ConfigBtn.Click += new System.EventHandler(this.ConfigBtn_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textTimeout);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.textImgQuality);
            this.groupBox4.Location = new System.Drawing.Point(336, 64);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(144, 172);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "LiveCapture";
            // 
            // textTimeout
            // 
            this.textTimeout.Location = new System.Drawing.Point(10, 98);
            this.textTimeout.Name = "textTimeout";
            this.textTimeout.Size = new System.Drawing.Size(105, 23);
            this.textTimeout.TabIndex = 18;
            this.textTimeout.Text = "10000";
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(10, 79);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(115, 29);
            this.label16.TabIndex = 17;
            this.label16.Text = "Capture Timeout";
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(10, 25);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(115, 19);
            this.label15.TabIndex = 16;
            this.label15.Text = "Image Quality:";
            // 
            // textImgQuality
            // 
            this.textImgQuality.Location = new System.Drawing.Point(10, 44);
            this.textImgQuality.MaxLength = 3;
            this.textImgQuality.Name = "textImgQuality";
            this.textImgQuality.Size = new System.Drawing.Size(105, 23);
            this.textImgQuality.TabIndex = 15;
            this.textImgQuality.Text = "50";
            // 
            // GetLiveImageBtn
            // 
            this.GetLiveImageBtn.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.GetLiveImageBtn.Location = new System.Drawing.Point(120, 15);
            this.GetLiveImageBtn.Name = "GetLiveImageBtn";
            this.GetLiveImageBtn.Size = new System.Drawing.Size(91, 29);
            this.GetLiveImageBtn.TabIndex = 8;
            this.GetLiveImageBtn.Text = "LiveCapture";
            this.GetLiveImageBtn.UseVisualStyleBackColor = false;
            this.GetLiveImageBtn.Click += new System.EventHandler(this.GetLiveImageBtn_Click);
            // 
            // GetImageBtn
            // 
            this.GetImageBtn.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.GetImageBtn.Location = new System.Drawing.Point(14, 15);
            this.GetImageBtn.Name = "GetImageBtn";
            this.GetImageBtn.Size = new System.Drawing.Size(92, 29);
            this.GetImageBtn.TabIndex = 7;
            this.GetImageBtn.Text = "Capture";
            this.GetImageBtn.UseVisualStyleBackColor = false;
            this.GetImageBtn.Click += new System.EventHandler(this.GetImageBtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(10, 59);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(312, 369);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.BtnVerify);
            this.tabPage3.Controls.Add(this.BtnRegister);
            this.tabPage3.Controls.Add(this.groupBox6);
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(491, 469);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Register/Verify";
            // 
            // BtnVerify
            // 
            this.BtnVerify.BackColor = System.Drawing.SystemColors.Desktop;
            this.BtnVerify.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.BtnVerify.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnVerify.Location = new System.Drawing.Point(336, 379);
            this.BtnVerify.Name = "BtnVerify";
            this.BtnVerify.Size = new System.Drawing.Size(130, 28);
            this.BtnVerify.TabIndex = 34;
            this.BtnVerify.Text = "Verify";
            this.BtnVerify.UseVisualStyleBackColor = false;
            this.BtnVerify.Click += new System.EventHandler(this.BtnVerify_Click);
            // 
            // BtnRegister
            // 
            this.BtnRegister.BackColor = System.Drawing.SystemColors.Desktop;
            this.BtnRegister.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.BtnRegister.Location = new System.Drawing.Point(62, 379);
            this.BtnRegister.Name = "BtnRegister";
            this.BtnRegister.Size = new System.Drawing.Size(159, 28);
            this.BtnRegister.TabIndex = 33;
            this.BtnRegister.Text = "Register";
            this.BtnRegister.UseVisualStyleBackColor = false;
            this.BtnRegister.Click += new System.EventHandler(this.BtnRegister_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.comboBoxSecuLevel_V);
            this.groupBox6.Controls.Add(this.label14);
            this.groupBox6.Controls.Add(this.label4);
            this.groupBox6.Controls.Add(this.comboBoxSecuLevel_R);
            this.groupBox6.Location = new System.Drawing.Point(10, 10);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(470, 69);
            this.groupBox6.TabIndex = 30;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Security Level";
            // 
            // comboBoxSecuLevel_V
            // 
            this.comboBoxSecuLevel_V.Items.AddRange(new object[] {
            "LOWEST",
            "LOWER",
            "LOW",
            "BELOW_NORMAL",
            "NORMAL",
            "ABOVE_NORMAL",
            "HIGH",
            "HIGHER",
            "HIGHEST"});
            this.comboBoxSecuLevel_V.Location = new System.Drawing.Point(326, 30);
            this.comboBoxSecuLevel_V.Name = "comboBoxSecuLevel_V";
            this.comboBoxSecuLevel_V.Size = new System.Drawing.Size(135, 23);
            this.comboBoxSecuLevel_V.TabIndex = 24;
            this.comboBoxSecuLevel_V.Text = "NORMAL";
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(250, 30);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(76, 29);
            this.label14.TabIndex = 23;
            this.label14.Text = "Verification";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(10, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 29);
            this.label4.TabIndex = 22;
            this.label4.Text = "Registration";
            // 
            // comboBoxSecuLevel_R
            // 
            this.comboBoxSecuLevel_R.Items.AddRange(new object[] {
            "LOWEST",
            "LOWER",
            "LOW",
            "BELOW_NORMAL",
            "NORMAL",
            "ABOVE_NORMAL",
            "HIGH",
            "HIGHER",
            "HIGHEST"});
            this.comboBoxSecuLevel_R.Location = new System.Drawing.Point(96, 30);
            this.comboBoxSecuLevel_R.Name = "comboBoxSecuLevel_R";
            this.comboBoxSecuLevel_R.Size = new System.Drawing.Size(134, 23);
            this.comboBoxSecuLevel_R.TabIndex = 21;
            this.comboBoxSecuLevel_R.Text = "NORMAL";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.progressBar_V1);
            this.groupBox2.Controls.Add(this.pictureBoxV1);
            this.groupBox2.Controls.Add(this.BtnCapture3);
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Location = new System.Drawing.Point(317, 94);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(163, 270);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Verification";
            // 
            // progressBar_V1
            // 
            this.progressBar_V1.Location = new System.Drawing.Point(19, 187);
            this.progressBar_V1.Name = "progressBar_V1";
            this.progressBar_V1.Size = new System.Drawing.Size(125, 15);
            this.progressBar_V1.TabIndex = 31;
            // 
            // pictureBoxV1
            // 
            this.pictureBoxV1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBoxV1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxV1.Location = new System.Drawing.Point(19, 30);
            this.pictureBoxV1.Name = "pictureBoxV1";
            this.pictureBoxV1.Size = new System.Drawing.Size(125, 157);
            this.pictureBoxV1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxV1.TabIndex = 29;
            this.pictureBoxV1.TabStop = false;
            // 
            // BtnCapture3
            // 
            this.BtnCapture3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BtnCapture3.Location = new System.Drawing.Point(19, 217);
            this.BtnCapture3.Name = "BtnCapture3";
            this.BtnCapture3.Size = new System.Drawing.Size(125, 28);
            this.BtnCapture3.TabIndex = 27;
            this.BtnCapture3.Text = "Capture V1";
            this.BtnCapture3.UseVisualStyleBackColor = false;
            this.BtnCapture3.Click += new System.EventHandler(this.BtnCapture3_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Items.AddRange(new object[] {
            "LOWEST",
            "LOWER",
            "LOW",
            "BELOW_NORMAL",
            "NORMAL",
            "ABOVE_NORMAL",
            "HIGH",
            "HIGHER",
            "HIGHEST"});
            this.comboBox1.Location = new System.Drawing.Point(58, -49);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(105, 23);
            this.comboBox1.TabIndex = 30;
            this.comboBox1.Text = "NORMAL";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox1.Controls.Add(this.progressBar_R2);
            this.groupBox1.Controls.Add(this.progressBar_R1);
            this.groupBox1.Controls.Add(this.pictureBoxR2);
            this.groupBox1.Controls.Add(this.pictureBoxR1);
            this.groupBox1.Controls.Add(this.BtnCapture1);
            this.groupBox1.Controls.Add(this.BtnCapture2);
            this.groupBox1.Location = new System.Drawing.Point(10, 94);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(292, 270);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Registration";
            // 
            // progressBar_R2
            // 
            this.progressBar_R2.Location = new System.Drawing.Point(154, 187);
            this.progressBar_R2.Name = "progressBar_R2";
            this.progressBar_R2.Size = new System.Drawing.Size(124, 15);
            this.progressBar_R2.TabIndex = 29;
            // 
            // progressBar_R1
            // 
            this.progressBar_R1.Location = new System.Drawing.Point(10, 187);
            this.progressBar_R1.Name = "progressBar_R1";
            this.progressBar_R1.Size = new System.Drawing.Size(124, 15);
            this.progressBar_R1.TabIndex = 28;
            // 
            // pictureBoxR2
            // 
            this.pictureBoxR2.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBoxR2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxR2.Location = new System.Drawing.Point(154, 30);
            this.pictureBoxR2.Name = "pictureBoxR2";
            this.pictureBoxR2.Size = new System.Drawing.Size(124, 157);
            this.pictureBoxR2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxR2.TabIndex = 27;
            this.pictureBoxR2.TabStop = false;
            // 
            // pictureBoxR1
            // 
            this.pictureBoxR1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBoxR1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxR1.Location = new System.Drawing.Point(10, 30);
            this.pictureBoxR1.Name = "pictureBoxR1";
            this.pictureBoxR1.Size = new System.Drawing.Size(124, 157);
            this.pictureBoxR1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxR1.TabIndex = 26;
            this.pictureBoxR1.TabStop = false;
            // 
            // BtnCapture1
            // 
            this.BtnCapture1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BtnCapture1.Location = new System.Drawing.Point(10, 217);
            this.BtnCapture1.Name = "BtnCapture1";
            this.BtnCapture1.Size = new System.Drawing.Size(124, 28);
            this.BtnCapture1.TabIndex = 23;
            this.BtnCapture1.Text = "Capture R1";
            this.BtnCapture1.UseVisualStyleBackColor = false;
            this.BtnCapture1.Click += new System.EventHandler(this.BtnCapture1_Click);
            // 
            // BtnCapture2
            // 
            this.BtnCapture2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BtnCapture2.Location = new System.Drawing.Point(154, 217);
            this.BtnCapture2.Name = "BtnCapture2";
            this.BtnCapture2.Size = new System.Drawing.Size(124, 28);
            this.BtnCapture2.TabIndex = 24;
            this.BtnCapture2.Text = "Capture R2";
            this.BtnCapture2.UseVisualStyleBackColor = false;
            this.BtnCapture2.Click += new System.EventHandler(this.BtnCapture2_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.GetBtn);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(491, 469);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "DeviceInfo";
            // 
            // GetBtn
            // 
            this.GetBtn.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.GetBtn.Location = new System.Drawing.Point(346, 20);
            this.GetBtn.Name = "GetBtn";
            this.GetBtn.Size = new System.Drawing.Size(115, 29);
            this.GetBtn.TabIndex = 43;
            this.GetBtn.Text = "Get";
            this.GetBtn.UseVisualStyleBackColor = false;
            this.GetBtn.Click += new System.EventHandler(this.GetBtn_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textImageDPI);
            this.groupBox3.Controls.Add(this.textImageHeight);
            this.groupBox3.Controls.Add(this.textImageWidth);
            this.groupBox3.Controls.Add(this.textSerialNum);
            this.groupBox3.Controls.Add(this.textFWVersion);
            this.groupBox3.Controls.Add(this.textDeviceID);
            this.groupBox3.Controls.Add(this.textBrightness);
            this.groupBox3.Controls.Add(this.textGain);
            this.groupBox3.Controls.Add(this.textContrast);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Location = new System.Drawing.Point(10, 10);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(316, 305);
            this.groupBox3.TabIndex = 41;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "DeviceInfo";
            // 
            // textImageDPI
            // 
            this.textImageDPI.Enabled = false;
            this.textImageDPI.Location = new System.Drawing.Point(115, 177);
            this.textImageDPI.Name = "textImageDPI";
            this.textImageDPI.Size = new System.Drawing.Size(183, 23);
            this.textImageDPI.TabIndex = 66;
            // 
            // textImageHeight
            // 
            this.textImageHeight.Enabled = false;
            this.textImageHeight.Location = new System.Drawing.Point(115, 148);
            this.textImageHeight.Name = "textImageHeight";
            this.textImageHeight.Size = new System.Drawing.Size(183, 23);
            this.textImageHeight.TabIndex = 65;
            // 
            // textImageWidth
            // 
            this.textImageWidth.Enabled = false;
            this.textImageWidth.Location = new System.Drawing.Point(115, 118);
            this.textImageWidth.Name = "textImageWidth";
            this.textImageWidth.Size = new System.Drawing.Size(183, 23);
            this.textImageWidth.TabIndex = 64;
            // 
            // textSerialNum
            // 
            this.textSerialNum.Enabled = false;
            this.textSerialNum.Location = new System.Drawing.Point(115, 89);
            this.textSerialNum.Name = "textSerialNum";
            this.textSerialNum.Size = new System.Drawing.Size(183, 23);
            this.textSerialNum.TabIndex = 63;
            // 
            // textFWVersion
            // 
            this.textFWVersion.Enabled = false;
            this.textFWVersion.Location = new System.Drawing.Point(115, 59);
            this.textFWVersion.Name = "textFWVersion";
            this.textFWVersion.Size = new System.Drawing.Size(183, 23);
            this.textFWVersion.TabIndex = 62;
            // 
            // textDeviceID
            // 
            this.textDeviceID.Enabled = false;
            this.textDeviceID.Location = new System.Drawing.Point(115, 30);
            this.textDeviceID.Name = "textDeviceID";
            this.textDeviceID.Size = new System.Drawing.Size(183, 23);
            this.textDeviceID.TabIndex = 61;
            // 
            // textBrightness
            // 
            this.textBrightness.Enabled = false;
            this.textBrightness.Location = new System.Drawing.Point(115, 207);
            this.textBrightness.Name = "textBrightness";
            this.textBrightness.Size = new System.Drawing.Size(183, 23);
            this.textBrightness.TabIndex = 58;
            // 
            // textGain
            // 
            this.textGain.Enabled = false;
            this.textGain.Location = new System.Drawing.Point(115, 266);
            this.textGain.Name = "textGain";
            this.textGain.Size = new System.Drawing.Size(183, 23);
            this.textGain.TabIndex = 57;
            // 
            // textContrast
            // 
            this.textContrast.Enabled = false;
            this.textContrast.Location = new System.Drawing.Point(115, 236);
            this.textContrast.Name = "textContrast";
            this.textContrast.Size = new System.Drawing.Size(183, 23);
            this.textContrast.TabIndex = 56;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(19, 266);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(87, 20);
            this.label12.TabIndex = 55;
            this.label12.Text = "Gain";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(19, 236);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(87, 20);
            this.label11.TabIndex = 54;
            this.label11.Text = "Contrast";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(19, 207);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(87, 19);
            this.label10.TabIndex = 53;
            this.label10.Text = "Brightness";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(19, 177);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 20);
            this.label9.TabIndex = 51;
            this.label9.Text = "Image DPI";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(19, 89);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 19);
            this.label8.TabIndex = 49;
            this.label8.Text = "Serial #";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(19, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 20);
            this.label7.TabIndex = 47;
            this.label7.Text = "F/W Version";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(19, 148);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 19);
            this.label6.TabIndex = 45;
            this.label6.Text = "Image Height";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(19, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 20);
            this.label5.TabIndex = 43;
            this.label5.Text = "Image Width";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(19, 30);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(87, 19);
            this.label13.TabIndex = 41;
            this.label13.Text = "Device ID";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatusBar
            // 
            this.StatusBar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.StatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.StatusBar.ForeColor = System.Drawing.SystemColors.Highlight;
            this.StatusBar.Location = new System.Drawing.Point(0, 654);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(514, 30);
            this.StatusBar.TabIndex = 7;
            this.StatusBar.Text = "Click Init Button";
            // 
            // groupBoxUsbDevs
            // 
            this.groupBoxUsbDevs.Controls.Add(this.OpenDeviceBtn);
            this.groupBoxUsbDevs.Controls.Add(this.EnumerateBtn);
            this.groupBoxUsbDevs.Controls.Add(this.label1);
            this.groupBoxUsbDevs.Controls.Add(this.comboBoxDeviceName);
            this.groupBoxUsbDevs.Location = new System.Drawing.Point(5, 2);
            this.groupBoxUsbDevs.Name = "groupBoxUsbDevs";
            this.groupBoxUsbDevs.Size = new System.Drawing.Size(497, 71);
            this.groupBoxUsbDevs.TabIndex = 10;
            this.groupBoxUsbDevs.TabStop = false;
            this.groupBoxUsbDevs.Text = "USB";
            // 
            // OpenDeviceBtn
            // 
            this.OpenDeviceBtn.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.OpenDeviceBtn.Location = new System.Drawing.Point(294, 23);
            this.OpenDeviceBtn.Name = "OpenDeviceBtn";
            this.OpenDeviceBtn.Size = new System.Drawing.Size(86, 30);
            this.OpenDeviceBtn.TabIndex = 13;
            this.OpenDeviceBtn.Text = "Init";
            this.OpenDeviceBtn.UseVisualStyleBackColor = false;
            this.OpenDeviceBtn.Click += new System.EventHandler(this.OpenDeviceBtn_Click);
            // 
            // EnumerateBtn
            // 
            this.EnumerateBtn.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.EnumerateBtn.Location = new System.Drawing.Point(395, 23);
            this.EnumerateBtn.Name = "EnumerateBtn";
            this.EnumerateBtn.Size = new System.Drawing.Size(86, 30);
            this.EnumerateBtn.TabIndex = 12;
            this.EnumerateBtn.Text = "Enumerate";
            this.EnumerateBtn.UseVisualStyleBackColor = false;
            this.EnumerateBtn.Click += new System.EventHandler(this.EnumerateBtn_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 30);
            this.label1.TabIndex = 11;
            this.label1.Text = "Device Name";
            // 
            // comboBoxDeviceName
            // 
            this.comboBoxDeviceName.Location = new System.Drawing.Point(97, 23);
            this.comboBoxDeviceName.Name = "comboBoxDeviceName";
            this.comboBoxDeviceName.Size = new System.Drawing.Size(183, 23);
            this.comboBoxDeviceName.TabIndex = 10;
            // 
            // groupBoxSda
            // 
            this.groupBoxSda.Controls.Add(this.OpenSdaBtn);
            this.groupBoxSda.Controls.Add(this.comboBoxComPorts);
            this.groupBoxSda.Controls.Add(this.label2);
            this.groupBoxSda.Location = new System.Drawing.Point(5, 81);
            this.groupBoxSda.Name = "groupBoxSda";
            this.groupBoxSda.Size = new System.Drawing.Size(497, 64);
            this.groupBoxSda.TabIndex = 11;
            this.groupBoxSda.TabStop = false;
            this.groupBoxSda.Text = "SDA-Serial";
            // 
            // OpenSdaBtn
            // 
            this.OpenSdaBtn.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.OpenSdaBtn.Location = new System.Drawing.Point(294, 23);
            this.OpenSdaBtn.Name = "OpenSdaBtn";
            this.OpenSdaBtn.Size = new System.Drawing.Size(86, 30);
            this.OpenSdaBtn.TabIndex = 14;
            this.OpenSdaBtn.Text = "Init";
            this.OpenSdaBtn.UseVisualStyleBackColor = false;
            this.OpenSdaBtn.Click += new System.EventHandler(this.OpenSdaBtn_Click);
            // 
            // comboBoxComPorts
            // 
            this.comboBoxComPorts.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9"});
            this.comboBoxComPorts.Location = new System.Drawing.Point(97, 22);
            this.comboBoxComPorts.Name = "comboBoxComPorts";
            this.comboBoxComPorts.Size = new System.Drawing.Size(183, 23);
            this.comboBoxComPorts.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Port";
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
            this.ClientSize = new System.Drawing.Size(514, 684);
            this.Controls.Add(this.groupBoxSda);
            this.Controls.Add(this.groupBoxUsbDevs);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.StatusBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.Text = "Matching C# Sample";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.GroupBoxBrightness.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BrightnessUpDown)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxV1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxR2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxR1)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBoxUsbDevs.ResumeLayout(false);
            this.groupBoxSda.ResumeLayout(false);
            this.groupBoxSda.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}


