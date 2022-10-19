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
	public class MainForm : System.Windows.Forms.Form
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
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}

				if (m_FPM != null) {
					m_FPM.Dispose();
				};
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
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
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.Run(new MainForm());
		}

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

			for (int i = 0; i < m_FPM.NumberOfDevice; i++) {
				m_DevList[i] = new SGFPMDeviceList();
				m_FPM.GetEnumDeviceInfo(i, m_DevList[i]);
				enum_device = m_DevList[i].DevName.ToString() + " : " + m_DevList[i].DevID;
				comboBoxDeviceName.Items.Add(enum_device);
			}

			if (comboBoxDeviceName.Items.Count > 0) {
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

			if (autoSelection) {
				// Order of search: Hamster IV(HFDU04) -> Plus(HFDU03) -> III (HFDU02)
				device_name = SGFPMDeviceName.DEV_AUTO;

				device_id = (Int32)(SGFPMPortAddr.USB_AUTO_DETECT);
			} else {
				device_name = m_DevList[deviceSelected].DevName;
				device_id = m_DevList[deviceSelected].DevID;
			}

			Int32 iError = OpenDevice(device_name, device_id);

			if (iError == (Int32)SGFPMError.ERROR_NONE) {
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
			if (iError == (Int32)SGFPMError.ERROR_NONE) {
				//GetBtn_Click(sender, e);
				GetBtn_Click(null, null);
				StatusBar.Text = "Initialization Success";
				EnableButtons(true);

				// FDU03, FDU04 or higher
				if (device_name >= SGFPMDeviceName.DEV_FDU03)
					CheckBoxAutoOn.Enabled = true;
			} else
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

			if (iError == (Int32)SGFPMError.ERROR_NONE) {
				elap_time = Environment.TickCount - elap_time;
				DrawImage(fp_image, pictureBox1);
				StatusBar.Text = "Capture Time : " + elap_time + " ms";
			} else
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

			if (iError == 0) {
				elap_time = Environment.TickCount - elap_time;
				StatusBar.Text = "Capture Time : " + elap_time + " ms";
			} else
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
			foreach (var thumb in m_thumbnails) {
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
			if (m_RegMins.Count < NUM_OF_REGS) {
				const int min_qlty_register = MinImageQualityRegistration;
				const long timeout = 5000; // 5 seconds
				byte[] newRegMin = new byte[SIZE_OF_FEATURE_BUF];
				byte[] fpImage;
				bool templateCreated = GetFpTemplate(newRegMin, CapturePurpose.Registration, min_qlty_register, timeout, progressBar_R1, pictureBoxR1, out fpImage);
				if (templateCreated) {
					const int numOfMinRegs = 1;
					int matchScore = 0;
					bool add = true;
					if (m_RegMins.Count > 0) {
						if (MatchTemplates(newRegMin, m_RegMins, numOfMinRegs, ref matchScore)) {
							StatusBar.Text += ", match score = " + matchScore;
						} else {
							StatusBar.Text += ", unable to register";
							add = false;    // Unable to register
						}
					}

					if (add) {
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
			do {
				iError = m_FPM.GetImage(fp_image);
				if (iError == (Int32)SGFPMError.ERROR_NONE) {
					DrawImage(fp_image, pictureBox);

					if (m_DevName == SGFPMDeviceName.DEV_FDU10A)
						m_FPM.GetLastImageQuality(ref img_qlty);    // U-Air only for now.
					else
						m_FPM.GetImageQuality(m_ImageWidth, m_ImageHeight, fp_image, ref img_qlty);

					progressBar.Value = img_qlty;
					if (img_qlty >= min_qlty) {
						iError = m_FPM.CreateTemplate(null, fp_image, fp_template);

						if (iError == (Int32)SGFPMError.ERROR_NONE) {
							template_created = true;
							outImage = fp_image;
							StatusBar.Text = "Image for " + purpose + " is captured with quality " + img_qlty;
						} else {
							DisplayError("CreateTemplate()", iError);
						}
					} else {
						StatusBar.Text = "Image Quality = " + img_qlty + ", minimum = " + min_qlty;
					}

				} else {
					//DisplayError("GetImage()", iError);
					StatusBar.Text = "The captured image is not good.Capturing...";
				}

				StatusBar.Update();

				elapsed = stopwatch.ElapsedMilliseconds;
			} while (!template_created && elapsed <= timeout);

			m_FPM.EndGetImage();

			if (!template_created) {
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
			if (m_RegDone) {
				StatusBar.Text = "Registration Success";
			} else {
				StatusBar.Text = "Need to register more (" + m_RegMins.Count + "/" + NUM_OF_REGS + " registered)";
			}
		}

		///////////////////////
		/// MatchTemplate(), GetMatchingScore()
		private void BtnVerify_Click(object sender, System.EventArgs e)
		{
			if (!m_RegDone) {
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

			foreach (var fetReg in templates) {
				bool matched = false;
				iError = m_FPM.MatchTemplate(fetReg, fetBuf, secu_level, ref matched);

				if (iError != (Int32)SGFPMError.ERROR_NONE) {
					StatusBar.Text = "MatchTemplate() failed. Error = " + iError; // just for error message
				} else {
					if (matched) {
						numOfMatched++;

						int score = 0;
						m_FPM.GetMatchingScore(fetReg, fetBuf, ref score);
						sumOfScores += score;

						if (numOfMatched >= numOfMinMatched) {
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

			if (iError == (Int32)SGFPMError.ERROR_NONE) {
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
			if (iError == (Int32)SGFPMError.ERROR_NONE) {
				StatusBar.Text = "SetBrightness success";
				GetBtn_Click(sender, e);
			} else
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
			if (message.Msg == (int)SGFPMMessages.DEV_AUTOONEVENT) {
				if (message.WParam.ToInt32() == (Int32)SGFPMAutoOnEvent.FINGER_ON) {
					LPDeviceInfoParam lparam = new LPDeviceInfoParam();
					Marshal.PtrToStructure(message.LParam, lparam);

					var serialNum = Encoding.ASCII.GetString(lparam.DeviceSN, 0, lparam.DeviceSN.Length);
					StatusBar.Text = "Device Message: Finger On" + ", " + serialNum;

				} else if (message.WParam.ToInt32() == (Int32)SGFPMAutoOnEvent.FINGER_OFF) {
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

			for (int i = 0; i < bmp.Width; i++) {
				for (int j = 0; j < bmp.Height; j++) {
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

			switch (iError) {
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


