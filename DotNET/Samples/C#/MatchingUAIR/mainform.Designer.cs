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

        private System.ComponentModel.Container components = null;
        private GroupBox groupBoxUsbDevs;
        private Button OpenDeviceBtn;
        private Button EnumerateBtn;
        private Label label1;
        private ComboBox comboBoxDeviceName;
        private TabPage tabPage1;
        private Button GetBtn;
        private GroupBox groupBox3;
        private TextBox textImageDPI;
        private TextBox textImageHeight;
        private TextBox textImageWidth;
        private TextBox textSerialNum;
        private TextBox textFWVersion;
        private TextBox textDeviceID;
        private TextBox textBrightness;
        private TextBox textGain;
        private TextBox textContrast;
        private Label label12;
        private Label label11;
        private Label label10;
        private Label label9;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label13;
        private TabPage tabPage3;
        private Button BtnVerify;
        private Button BtnRegister;
        private GroupBox groupBox6;
        private ComboBox comboBoxSecuLevel_V;
        private Label label14;
        private Label label4;
        private ComboBox comboBoxSecuLevel_R;
        private GroupBox groupBox2;
        private ProgressBar progressBar_V1;
        private PictureBox pictureBoxV1;
        private Button BtnCapture3;
        private ComboBox comboBox1;
        private GroupBox groupBox1;
        private ProgressBar progressBar_R1;
        private PictureBox pictureBoxR1;
        private TabControl tabControl1;
        private TabPage tabPage2;
        private CheckBox CheckBoxAutoOn;
        internal GroupBox GroupBoxBrightness;
        internal NumericUpDown BrightnessUpDown;
        internal Button SetBrightnessBtn;
        private Button ConfigBtn;
        private GroupBox groupBox4;
        private TextBox textTimeout;
        private Label label16;
        private Label label15;
        private TextBox textImgQuality;
        private Button GetLiveImageBtn;
        private Button GetImageBtn;
        private PictureBox pictureBox1;
        private Button BtnCaptureReg;
        private PictureBox pictureBoxThumbnail1;
        private PictureBox pictureBoxThumbnail2;
        private Button BtnNewReg;
        private PictureBox pictureBoxThumbnail5;
        private PictureBox pictureBoxThumbnail4;
        private PictureBox pictureBoxThumbnail3;
        private System.Windows.Forms.Label StatusBar;

        public MainForm()
        {
            InitializeComponent();
        }

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
                };
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.StatusBar = new System.Windows.Forms.Label();
            this.groupBoxUsbDevs = new System.Windows.Forms.GroupBox();
            this.OpenDeviceBtn = new System.Windows.Forms.Button();
            this.EnumerateBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxDeviceName = new System.Windows.Forms.ComboBox();
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
            this.progressBar_R1 = new System.Windows.Forms.ProgressBar();
            this.pictureBoxR1 = new System.Windows.Forms.PictureBox();
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
            this.BtnCaptureReg = new System.Windows.Forms.Button();
            this.pictureBoxThumbnail1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxThumbnail2 = new System.Windows.Forms.PictureBox();
            this.BtnNewReg = new System.Windows.Forms.Button();
            this.pictureBoxThumbnail3 = new System.Windows.Forms.PictureBox();
            this.pictureBoxThumbnail4 = new System.Windows.Forms.PictureBox();
            this.pictureBoxThumbnail5 = new System.Windows.Forms.PictureBox();
            this.groupBoxUsbDevs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxV1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxR1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.GroupBoxBrightness.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BrightnessUpDown)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThumbnail1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThumbnail2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThumbnail3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThumbnail4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThumbnail5)).BeginInit();
            this.SuspendLayout();
            // 
            // StatusBar
            // 
            this.StatusBar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.StatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.StatusBar.ForeColor = System.Drawing.SystemColors.Highlight;
            this.StatusBar.Location = new System.Drawing.Point(0, 945);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(957, 52);
            this.StatusBar.TabIndex = 7;
            this.StatusBar.Text = "Click Init Button";
            // 
            // groupBoxUsbDevs
            // 
            this.groupBoxUsbDevs.Controls.Add(this.OpenDeviceBtn);
            this.groupBoxUsbDevs.Controls.Add(this.EnumerateBtn);
            this.groupBoxUsbDevs.Controls.Add(this.label1);
            this.groupBoxUsbDevs.Controls.Add(this.comboBoxDeviceName);
            this.groupBoxUsbDevs.Location = new System.Drawing.Point(8, 4);
            this.groupBoxUsbDevs.Name = "groupBoxUsbDevs";
            this.groupBoxUsbDevs.Size = new System.Drawing.Size(942, 105);
            this.groupBoxUsbDevs.TabIndex = 10;
            this.groupBoxUsbDevs.TabStop = false;
            this.groupBoxUsbDevs.Text = "USB";
            // 
            // OpenDeviceBtn
            // 
            this.OpenDeviceBtn.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.OpenDeviceBtn.Location = new System.Drawing.Point(506, 35);
            this.OpenDeviceBtn.Name = "OpenDeviceBtn";
            this.OpenDeviceBtn.Size = new System.Drawing.Size(144, 44);
            this.OpenDeviceBtn.TabIndex = 13;
            this.OpenDeviceBtn.Text = "Init";
            this.OpenDeviceBtn.UseVisualStyleBackColor = false;
            this.OpenDeviceBtn.Click += new System.EventHandler(this.OpenDeviceBtn_Click);
            // 
            // EnumerateBtn
            // 
            this.EnumerateBtn.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.EnumerateBtn.Location = new System.Drawing.Point(690, 35);
            this.EnumerateBtn.Name = "EnumerateBtn";
            this.EnumerateBtn.Size = new System.Drawing.Size(144, 44);
            this.EnumerateBtn.TabIndex = 12;
            this.EnumerateBtn.Text = "Enumerate";
            this.EnumerateBtn.UseVisualStyleBackColor = false;
            this.EnumerateBtn.Click += new System.EventHandler(this.EnumerateBtn_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 44);
            this.label1.TabIndex = 11;
            this.label1.Text = "Device Name";
            // 
            // comboBoxDeviceName
            // 
            this.comboBoxDeviceName.Location = new System.Drawing.Point(162, 35);
            this.comboBoxDeviceName.Name = "comboBoxDeviceName";
            this.comboBoxDeviceName.Size = new System.Drawing.Size(304, 33);
            this.comboBoxDeviceName.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.GetBtn);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(8, 39);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(936, 775);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "DeviceInfo";
            // 
            // GetBtn
            // 
            this.GetBtn.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.GetBtn.Location = new System.Drawing.Point(640, 50);
            this.GetBtn.Name = "GetBtn";
            this.GetBtn.Size = new System.Drawing.Size(192, 44);
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
            this.groupBox3.Location = new System.Drawing.Point(16, 15);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(528, 458);
            this.groupBox3.TabIndex = 41;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "DeviceInfo";
            // 
            // textImageDPI
            // 
            this.textImageDPI.Enabled = false;
            this.textImageDPI.Location = new System.Drawing.Point(192, 266);
            this.textImageDPI.Name = "textImageDPI";
            this.textImageDPI.Size = new System.Drawing.Size(304, 31);
            this.textImageDPI.TabIndex = 66;
            // 
            // textImageHeight
            // 
            this.textImageHeight.Enabled = false;
            this.textImageHeight.Location = new System.Drawing.Point(192, 222);
            this.textImageHeight.Name = "textImageHeight";
            this.textImageHeight.Size = new System.Drawing.Size(304, 31);
            this.textImageHeight.TabIndex = 65;
            // 
            // textImageWidth
            // 
            this.textImageWidth.Enabled = false;
            this.textImageWidth.Location = new System.Drawing.Point(192, 177);
            this.textImageWidth.Name = "textImageWidth";
            this.textImageWidth.Size = new System.Drawing.Size(304, 31);
            this.textImageWidth.TabIndex = 64;
            // 
            // textSerialNum
            // 
            this.textSerialNum.Enabled = false;
            this.textSerialNum.Location = new System.Drawing.Point(192, 133);
            this.textSerialNum.Name = "textSerialNum";
            this.textSerialNum.Size = new System.Drawing.Size(304, 31);
            this.textSerialNum.TabIndex = 63;
            // 
            // textFWVersion
            // 
            this.textFWVersion.Enabled = false;
            this.textFWVersion.Location = new System.Drawing.Point(192, 89);
            this.textFWVersion.Name = "textFWVersion";
            this.textFWVersion.Size = new System.Drawing.Size(304, 31);
            this.textFWVersion.TabIndex = 62;
            // 
            // textDeviceID
            // 
            this.textDeviceID.Enabled = false;
            this.textDeviceID.Location = new System.Drawing.Point(192, 44);
            this.textDeviceID.Name = "textDeviceID";
            this.textDeviceID.Size = new System.Drawing.Size(304, 31);
            this.textDeviceID.TabIndex = 61;
            // 
            // textBrightness
            // 
            this.textBrightness.Enabled = false;
            this.textBrightness.Location = new System.Drawing.Point(192, 310);
            this.textBrightness.Name = "textBrightness";
            this.textBrightness.Size = new System.Drawing.Size(304, 31);
            this.textBrightness.TabIndex = 58;
            // 
            // textGain
            // 
            this.textGain.Enabled = false;
            this.textGain.Location = new System.Drawing.Point(192, 399);
            this.textGain.Name = "textGain";
            this.textGain.Size = new System.Drawing.Size(304, 31);
            this.textGain.TabIndex = 57;
            // 
            // textContrast
            // 
            this.textContrast.Enabled = false;
            this.textContrast.Location = new System.Drawing.Point(192, 354);
            this.textContrast.Name = "textContrast";
            this.textContrast.Size = new System.Drawing.Size(304, 31);
            this.textContrast.TabIndex = 56;
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(32, 399);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(144, 29);
            this.label12.TabIndex = 55;
            this.label12.Text = "Gain";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(32, 354);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(144, 30);
            this.label11.TabIndex = 54;
            this.label11.Text = "Contrast";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(32, 310);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(144, 30);
            this.label10.TabIndex = 53;
            this.label10.Text = "Brightness";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(32, 266);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(144, 29);
            this.label9.TabIndex = 51;
            this.label9.Text = "Image DPI";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(32, 133);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(144, 29);
            this.label8.TabIndex = 49;
            this.label8.Text = "Serial #";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(32, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(144, 29);
            this.label7.TabIndex = 47;
            this.label7.Text = "F/W Version";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(32, 222);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(144, 29);
            this.label6.TabIndex = 45;
            this.label6.Text = "Image Height";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(32, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(144, 30);
            this.label5.TabIndex = 43;
            this.label5.Text = "Image Width";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(32, 44);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(144, 30);
            this.label13.TabIndex = 41;
            this.label13.Text = "Device ID";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.BtnVerify);
            this.tabPage3.Controls.Add(this.BtnRegister);
            this.tabPage3.Controls.Add(this.groupBox6);
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Location = new System.Drawing.Point(8, 39);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(936, 775);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Register/Verify";
            // 
            // BtnVerify
            // 
            this.BtnVerify.BackColor = System.Drawing.SystemColors.Desktop;
            this.BtnVerify.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.BtnVerify.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnVerify.Location = new System.Drawing.Point(632, 663);
            this.BtnVerify.Name = "BtnVerify";
            this.BtnVerify.Size = new System.Drawing.Size(246, 42);
            this.BtnVerify.TabIndex = 34;
            this.BtnVerify.Text = "Verify";
            this.BtnVerify.UseVisualStyleBackColor = false;
            this.BtnVerify.Click += new System.EventHandler(this.BtnVerify_Click);
            // 
            // BtnRegister
            // 
            this.BtnRegister.BackColor = System.Drawing.SystemColors.Desktop;
            this.BtnRegister.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.BtnRegister.Location = new System.Drawing.Point(154, 663);
            this.BtnRegister.Name = "BtnRegister";
            this.BtnRegister.Size = new System.Drawing.Size(264, 42);
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
            this.groupBox6.Location = new System.Drawing.Point(16, 15);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(784, 103);
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
            this.comboBoxSecuLevel_V.Location = new System.Drawing.Point(544, 44);
            this.comboBoxSecuLevel_V.Name = "comboBoxSecuLevel_V";
            this.comboBoxSecuLevel_V.Size = new System.Drawing.Size(224, 33);
            this.comboBoxSecuLevel_V.TabIndex = 24;
            this.comboBoxSecuLevel_V.Text = "NORMAL";
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(416, 44);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(128, 45);
            this.label14.TabIndex = 23;
            this.label14.Text = "Verification";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(16, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 45);
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
            this.comboBoxSecuLevel_R.Location = new System.Drawing.Point(160, 44);
            this.comboBoxSecuLevel_R.Name = "comboBoxSecuLevel_R";
            this.comboBoxSecuLevel_R.Size = new System.Drawing.Size(224, 33);
            this.comboBoxSecuLevel_R.TabIndex = 21;
            this.comboBoxSecuLevel_R.Text = "NORMAL";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.progressBar_V1);
            this.groupBox2.Controls.Add(this.pictureBoxV1);
            this.groupBox2.Controls.Add(this.BtnCapture3);
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Location = new System.Drawing.Point(614, 140);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(288, 512);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Verification";
            // 
            // progressBar_V1
            // 
            this.progressBar_V1.Location = new System.Drawing.Point(20, 378);
            this.progressBar_V1.Name = "progressBar_V1";
            this.progressBar_V1.Size = new System.Drawing.Size(250, 23);
            this.progressBar_V1.TabIndex = 31;
            // 
            // pictureBoxV1
            // 
            this.pictureBoxV1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBoxV1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxV1.Location = new System.Drawing.Point(18, 44);
            this.pictureBoxV1.Name = "pictureBoxV1";
            this.pictureBoxV1.Size = new System.Drawing.Size(250, 323);
            this.pictureBoxV1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxV1.TabIndex = 29;
            this.pictureBoxV1.TabStop = false;
            // 
            // BtnCapture3
            // 
            this.BtnCapture3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BtnCapture3.Location = new System.Drawing.Point(20, 438);
            this.BtnCapture3.Name = "BtnCapture3";
            this.BtnCapture3.Size = new System.Drawing.Size(244, 42);
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
            this.comboBox1.Location = new System.Drawing.Point(96, -74);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(176, 33);
            this.comboBox1.TabIndex = 30;
            this.comboBox1.Text = "NORMAL";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox1.Controls.Add(this.BtnNewReg);
            this.groupBox1.Controls.Add(this.BtnCaptureReg);
            this.groupBox1.Controls.Add(this.pictureBoxThumbnail5);
            this.groupBox1.Controls.Add(this.pictureBoxThumbnail4);
            this.groupBox1.Controls.Add(this.pictureBoxThumbnail3);
            this.groupBox1.Controls.Add(this.progressBar_R1);
            this.groupBox1.Controls.Add(this.pictureBoxThumbnail2);
            this.groupBox1.Controls.Add(this.pictureBoxR1);
            this.groupBox1.Controls.Add(this.pictureBoxThumbnail1);
            this.groupBox1.Location = new System.Drawing.Point(17, 140);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(572, 512);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Registration";
            // 
            // progressBar_R1
            // 
            this.progressBar_R1.Location = new System.Drawing.Point(295, 378);
            this.progressBar_R1.Name = "progressBar_R1";
            this.progressBar_R1.Size = new System.Drawing.Size(250, 23);
            this.progressBar_R1.TabIndex = 28;
            // 
            // pictureBoxR1
            // 
            this.pictureBoxR1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBoxR1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxR1.Location = new System.Drawing.Point(295, 44);
            this.pictureBoxR1.Name = "pictureBoxR1";
            this.pictureBoxR1.Size = new System.Drawing.Size(250, 323);
            this.pictureBoxR1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxR1.TabIndex = 26;
            this.pictureBoxR1.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(8, 120);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(952, 822);
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
            this.tabPage2.Location = new System.Drawing.Point(8, 39);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(936, 775);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "  Image  ";
            // 
            // CheckBoxAutoOn
            // 
            this.CheckBoxAutoOn.Enabled = false;
            this.CheckBoxAutoOn.Location = new System.Drawing.Point(576, 676);
            this.CheckBoxAutoOn.Name = "CheckBoxAutoOn";
            this.CheckBoxAutoOn.Size = new System.Drawing.Size(260, 35);
            this.CheckBoxAutoOn.TabIndex = 19;
            this.CheckBoxAutoOn.Text = "Enable AutoOn Event";
            this.CheckBoxAutoOn.CheckedChanged += new System.EventHandler(this.CheckBoxAutoOn_CheckedChanged);
            // 
            // GroupBoxBrightness
            // 
            this.GroupBoxBrightness.Controls.Add(this.BrightnessUpDown);
            this.GroupBoxBrightness.Controls.Add(this.SetBrightnessBtn);
            this.GroupBoxBrightness.Location = new System.Drawing.Point(576, 369);
            this.GroupBoxBrightness.Name = "GroupBoxBrightness";
            this.GroupBoxBrightness.Size = new System.Drawing.Size(240, 273);
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
            this.BrightnessUpDown.Location = new System.Drawing.Point(16, 44);
            this.BrightnessUpDown.Name = "BrightnessUpDown";
            this.BrightnessUpDown.Size = new System.Drawing.Size(88, 31);
            this.BrightnessUpDown.TabIndex = 20;
            this.BrightnessUpDown.Value = new decimal(new int[] {
            70,
            0,
            0,
            0});
            // 
            // SetBrightnessBtn
            // 
            this.SetBrightnessBtn.Location = new System.Drawing.Point(112, 44);
            this.SetBrightnessBtn.Name = "SetBrightnessBtn";
            this.SetBrightnessBtn.Size = new System.Drawing.Size(112, 37);
            this.SetBrightnessBtn.TabIndex = 19;
            this.SetBrightnessBtn.Text = "Apply";
            this.SetBrightnessBtn.Click += new System.EventHandler(this.SetBrightnessBtn_Click);
            // 
            // ConfigBtn
            // 
            this.ConfigBtn.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ConfigBtn.Location = new System.Drawing.Point(576, 22);
            this.ConfigBtn.Name = "ConfigBtn";
            this.ConfigBtn.Size = new System.Drawing.Size(240, 44);
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
            this.groupBox4.Location = new System.Drawing.Point(576, 96);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(240, 258);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "LiveCapture";
            // 
            // textTimeout
            // 
            this.textTimeout.Location = new System.Drawing.Point(16, 148);
            this.textTimeout.Name = "textTimeout";
            this.textTimeout.Size = new System.Drawing.Size(176, 31);
            this.textTimeout.TabIndex = 18;
            this.textTimeout.Text = "10000";
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(16, 118);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(192, 44);
            this.label16.TabIndex = 17;
            this.label16.Text = "Capture Timeout";
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(16, 37);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(192, 29);
            this.label15.TabIndex = 16;
            this.label15.Text = "Image Quality:";
            // 
            // textImgQuality
            // 
            this.textImgQuality.Location = new System.Drawing.Point(16, 66);
            this.textImgQuality.MaxLength = 3;
            this.textImgQuality.Name = "textImgQuality";
            this.textImgQuality.Size = new System.Drawing.Size(176, 31);
            this.textImgQuality.TabIndex = 15;
            this.textImgQuality.Text = "50";
            // 
            // GetLiveImageBtn
            // 
            this.GetLiveImageBtn.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.GetLiveImageBtn.Location = new System.Drawing.Point(200, 22);
            this.GetLiveImageBtn.Name = "GetLiveImageBtn";
            this.GetLiveImageBtn.Size = new System.Drawing.Size(152, 44);
            this.GetLiveImageBtn.TabIndex = 8;
            this.GetLiveImageBtn.Text = "LiveCapture";
            this.GetLiveImageBtn.UseVisualStyleBackColor = false;
            this.GetLiveImageBtn.Click += new System.EventHandler(this.GetLiveImageBtn_Click);
            // 
            // GetImageBtn
            // 
            this.GetImageBtn.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.GetImageBtn.Location = new System.Drawing.Point(24, 22);
            this.GetImageBtn.Name = "GetImageBtn";
            this.GetImageBtn.Size = new System.Drawing.Size(152, 44);
            this.GetImageBtn.TabIndex = 7;
            this.GetImageBtn.Text = "Capture";
            this.GetImageBtn.UseVisualStyleBackColor = false;
            this.GetImageBtn.Click += new System.EventHandler(this.GetImageBtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(16, 89);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(500, 646);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // BtnCaptureReg
            // 
            this.BtnCaptureReg.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BtnCaptureReg.Location = new System.Drawing.Point(295, 438);
            this.BtnCaptureReg.Name = "BtnCaptureReg";
            this.BtnCaptureReg.Size = new System.Drawing.Size(250, 42);
            this.BtnCaptureReg.TabIndex = 30;
            this.BtnCaptureReg.Text = "Capture Reg";
            this.BtnCaptureReg.UseVisualStyleBackColor = false;
            this.BtnCaptureReg.Click += new System.EventHandler(this.BtnCaptureReg_Click);
            // 
            // pictureBoxThumbnail1
            // 
            this.pictureBoxThumbnail1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBoxThumbnail1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxThumbnail1.Location = new System.Drawing.Point(21, 43);
            this.pictureBoxThumbnail1.Name = "pictureBoxThumbnail1";
            this.pictureBoxThumbnail1.Size = new System.Drawing.Size(127, 121);
            this.pictureBoxThumbnail1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxThumbnail1.TabIndex = 27;
            this.pictureBoxThumbnail1.TabStop = false;
            // 
            // pictureBoxThumbnail2
            // 
            this.pictureBoxThumbnail2.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBoxThumbnail2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxThumbnail2.Location = new System.Drawing.Point(159, 43);
            this.pictureBoxThumbnail2.Name = "pictureBoxThumbnail2";
            this.pictureBoxThumbnail2.Size = new System.Drawing.Size(127, 121);
            this.pictureBoxThumbnail2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxThumbnail2.TabIndex = 28;
            this.pictureBoxThumbnail2.TabStop = false;
            // 
            // BtnNewReg
            // 
            this.BtnNewReg.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BtnNewReg.Location = new System.Drawing.Point(21, 438);
            this.BtnNewReg.Name = "BtnNewReg";
            this.BtnNewReg.Size = new System.Drawing.Size(250, 42);
            this.BtnNewReg.TabIndex = 31;
            this.BtnNewReg.Text = "New Reg";
            this.BtnNewReg.UseVisualStyleBackColor = false;
            this.BtnNewReg.Click += new System.EventHandler(this.BtnNewReg_Click);
            // 
            // pictureBoxThumbnail3
            // 
            this.pictureBoxThumbnail3.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBoxThumbnail3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxThumbnail3.Location = new System.Drawing.Point(21, 174);
            this.pictureBoxThumbnail3.Name = "pictureBoxThumbnail3";
            this.pictureBoxThumbnail3.Size = new System.Drawing.Size(127, 121);
            this.pictureBoxThumbnail3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxThumbnail3.TabIndex = 29;
            this.pictureBoxThumbnail3.TabStop = false;
            // 
            // pictureBoxThumbnail4
            // 
            this.pictureBoxThumbnail4.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBoxThumbnail4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxThumbnail4.Location = new System.Drawing.Point(159, 174);
            this.pictureBoxThumbnail4.Name = "pictureBoxThumbnail4";
            this.pictureBoxThumbnail4.Size = new System.Drawing.Size(127, 121);
            this.pictureBoxThumbnail4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxThumbnail4.TabIndex = 30;
            this.pictureBoxThumbnail4.TabStop = false;
            // 
            // pictureBoxThumbnail5
            // 
            this.pictureBoxThumbnail5.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBoxThumbnail5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxThumbnail5.Location = new System.Drawing.Point(21, 307);
            this.pictureBoxThumbnail5.Name = "pictureBoxThumbnail5";
            this.pictureBoxThumbnail5.Size = new System.Drawing.Size(127, 121);
            this.pictureBoxThumbnail5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxThumbnail5.TabIndex = 31;
            this.pictureBoxThumbnail5.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(10, 24);
            this.ClientSize = new System.Drawing.Size(957, 997);
            this.Controls.Add(this.groupBoxUsbDevs);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.StatusBar);
            this.Name = "MainForm";
            this.Text = "Matching C# U-Air Sample";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBoxUsbDevs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxV1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxR1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.GroupBoxBrightness.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.BrightnessUpDown)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThumbnail1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThumbnail2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThumbnail3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThumbnail4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxThumbnail5)).EndInit();
            this.ResumeLayout(false);

        }
    }
}