Imports System.Text
Imports SecuGen.FDxSDKPro.Windows

Public Class Form1
   Inherits System.Windows.Forms.Form

   Dim m_FPM As SGFingerPrintManager
   Dim m_LedOn As Boolean
   Dim m_ImageWidth As Int32
   Dim m_ImageHeight As Int32
   Dim m_RegMin1(400) As Byte
   Dim m_RegMin2(400) As Byte
   Dim m_VrfMin(400) As Byte
   Dim m_DevList() As SGFPMDeviceList


#Region " Windows Form Designer generated code "

   Public Sub New()
      MyBase.New()

      'This call is required by the Windows Form Designer.
      InitializeComponent()

      'Add any initialization after the InitializeComponent() call

   End Sub

   'Form overrides dispose to clean up the component list.
   Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
      If disposing Then
         If Not (components Is Nothing) Then
            components.Dispose()
         End If
      End If
      MyBase.Dispose(disposing)
   End Sub

   'Required by the Windows Form Designer
   Private components As System.ComponentModel.IContainer

   'NOTE: The following procedure is required by the Windows Form Designer
   'It can be modified using the Windows Form Designer.  
   'Do not modify it using the code editor.
   Friend WithEvents EnumerateBtn As System.Windows.Forms.Button
   Friend WithEvents label1 As System.Windows.Forms.Label
   Friend WithEvents comboBoxDeviceName As System.Windows.Forms.ComboBox
   Friend WithEvents StatusBar As System.Windows.Forms.Label
   Friend WithEvents tabControl1 As System.Windows.Forms.TabControl
   Friend WithEvents tabPage2 As System.Windows.Forms.TabPage
   Friend WithEvents CheckBoxAutoOn As System.Windows.Forms.CheckBox
   Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
   Friend WithEvents BrightnessUpDown As System.Windows.Forms.NumericUpDown
   Friend WithEvents SetBrightnessBtn As System.Windows.Forms.Button
   Friend WithEvents ConfigBtn As System.Windows.Forms.Button
   Friend WithEvents groupBox4 As System.Windows.Forms.GroupBox
   Friend WithEvents textTimeout As System.Windows.Forms.TextBox
   Friend WithEvents label16 As System.Windows.Forms.Label
   Friend WithEvents label15 As System.Windows.Forms.Label
   Friend WithEvents textImgQuality As System.Windows.Forms.TextBox
   Friend WithEvents GetLiveImageBtn As System.Windows.Forms.Button
   Friend WithEvents GetImageBtn As System.Windows.Forms.Button
   Friend WithEvents pictureBox1 As System.Windows.Forms.PictureBox
   Friend WithEvents tabPage3 As System.Windows.Forms.TabPage
   Friend WithEvents BtnVerify As System.Windows.Forms.Button
   Friend WithEvents BtnRegister As System.Windows.Forms.Button
   Friend WithEvents groupBox6 As System.Windows.Forms.GroupBox
   Friend WithEvents comboBoxSecuLevel_V As System.Windows.Forms.ComboBox
   Friend WithEvents label14 As System.Windows.Forms.Label
   Friend WithEvents label4 As System.Windows.Forms.Label
   Friend WithEvents comboBoxSecuLevel_R As System.Windows.Forms.ComboBox
   Friend WithEvents groupBox2 As System.Windows.Forms.GroupBox
   Friend WithEvents progressBar_V1 As System.Windows.Forms.ProgressBar
   Friend WithEvents pictureBoxV1 As System.Windows.Forms.PictureBox
   Friend WithEvents BtnCapture3 As System.Windows.Forms.Button
   Friend WithEvents comboBox1 As System.Windows.Forms.ComboBox
   Friend WithEvents groupBox1 As System.Windows.Forms.GroupBox
   Friend WithEvents progressBar_R2 As System.Windows.Forms.ProgressBar
   Friend WithEvents progressBar_R1 As System.Windows.Forms.ProgressBar
   Friend WithEvents pictureBoxR2 As System.Windows.Forms.PictureBox
   Friend WithEvents pictureBoxR1 As System.Windows.Forms.PictureBox
   Friend WithEvents BtnCapture1 As System.Windows.Forms.Button
   Friend WithEvents BtnCapture2 As System.Windows.Forms.Button
   Friend WithEvents tabPage1 As System.Windows.Forms.TabPage
   Friend WithEvents GetBtn As System.Windows.Forms.Button
   Friend WithEvents groupBox3 As System.Windows.Forms.GroupBox
   Friend WithEvents textImageDPI As System.Windows.Forms.TextBox
   Friend WithEvents textImageHeight As System.Windows.Forms.TextBox
   Friend WithEvents textImageWidth As System.Windows.Forms.TextBox
   Friend WithEvents textSerialNum As System.Windows.Forms.TextBox
   Friend WithEvents textFWVersion As System.Windows.Forms.TextBox
   Friend WithEvents textDeviceID As System.Windows.Forms.TextBox
   Friend WithEvents textBrightness As System.Windows.Forms.TextBox
   Friend WithEvents textGain As System.Windows.Forms.TextBox
   Friend WithEvents textContrast As System.Windows.Forms.TextBox
   Friend WithEvents label12 As System.Windows.Forms.Label
   Friend WithEvents label11 As System.Windows.Forms.Label
   Friend WithEvents label10 As System.Windows.Forms.Label
   Friend WithEvents label9 As System.Windows.Forms.Label
   Friend WithEvents label8 As System.Windows.Forms.Label
   Friend WithEvents label7 As System.Windows.Forms.Label
   Friend WithEvents label6 As System.Windows.Forms.Label
   Friend WithEvents label5 As System.Windows.Forms.Label
   Friend WithEvents label13 As System.Windows.Forms.Label
   Friend WithEvents OpenDeviceBtn As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.EnumerateBtn = New System.Windows.Forms.Button()
        Me.label1 = New System.Windows.Forms.Label()
        Me.comboBoxDeviceName = New System.Windows.Forms.ComboBox()
        Me.StatusBar = New System.Windows.Forms.Label()
        Me.tabControl1 = New System.Windows.Forms.TabControl()
        Me.tabPage2 = New System.Windows.Forms.TabPage()
        Me.CheckBoxAutoOn = New System.Windows.Forms.CheckBox()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.BrightnessUpDown = New System.Windows.Forms.NumericUpDown()
        Me.SetBrightnessBtn = New System.Windows.Forms.Button()
        Me.ConfigBtn = New System.Windows.Forms.Button()
        Me.groupBox4 = New System.Windows.Forms.GroupBox()
        Me.textTimeout = New System.Windows.Forms.TextBox()
        Me.label16 = New System.Windows.Forms.Label()
        Me.label15 = New System.Windows.Forms.Label()
        Me.textImgQuality = New System.Windows.Forms.TextBox()
        Me.GetLiveImageBtn = New System.Windows.Forms.Button()
        Me.GetImageBtn = New System.Windows.Forms.Button()
        Me.pictureBox1 = New System.Windows.Forms.PictureBox()
        Me.tabPage3 = New System.Windows.Forms.TabPage()
        Me.BtnVerify = New System.Windows.Forms.Button()
        Me.BtnRegister = New System.Windows.Forms.Button()
        Me.groupBox6 = New System.Windows.Forms.GroupBox()
        Me.comboBoxSecuLevel_V = New System.Windows.Forms.ComboBox()
        Me.label14 = New System.Windows.Forms.Label()
        Me.label4 = New System.Windows.Forms.Label()
        Me.comboBoxSecuLevel_R = New System.Windows.Forms.ComboBox()
        Me.groupBox2 = New System.Windows.Forms.GroupBox()
        Me.progressBar_V1 = New System.Windows.Forms.ProgressBar()
        Me.pictureBoxV1 = New System.Windows.Forms.PictureBox()
        Me.BtnCapture3 = New System.Windows.Forms.Button()
        Me.comboBox1 = New System.Windows.Forms.ComboBox()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.progressBar_R2 = New System.Windows.Forms.ProgressBar()
        Me.progressBar_R1 = New System.Windows.Forms.ProgressBar()
        Me.pictureBoxR2 = New System.Windows.Forms.PictureBox()
        Me.pictureBoxR1 = New System.Windows.Forms.PictureBox()
        Me.BtnCapture1 = New System.Windows.Forms.Button()
        Me.BtnCapture2 = New System.Windows.Forms.Button()
        Me.tabPage1 = New System.Windows.Forms.TabPage()
        Me.GetBtn = New System.Windows.Forms.Button()
        Me.groupBox3 = New System.Windows.Forms.GroupBox()
        Me.textImageDPI = New System.Windows.Forms.TextBox()
        Me.textImageHeight = New System.Windows.Forms.TextBox()
        Me.textImageWidth = New System.Windows.Forms.TextBox()
        Me.textSerialNum = New System.Windows.Forms.TextBox()
        Me.textFWVersion = New System.Windows.Forms.TextBox()
        Me.textDeviceID = New System.Windows.Forms.TextBox()
        Me.textBrightness = New System.Windows.Forms.TextBox()
        Me.textGain = New System.Windows.Forms.TextBox()
        Me.textContrast = New System.Windows.Forms.TextBox()
        Me.label12 = New System.Windows.Forms.Label()
        Me.label11 = New System.Windows.Forms.Label()
        Me.label10 = New System.Windows.Forms.Label()
        Me.label9 = New System.Windows.Forms.Label()
        Me.label8 = New System.Windows.Forms.Label()
        Me.label7 = New System.Windows.Forms.Label()
        Me.label6 = New System.Windows.Forms.Label()
        Me.label5 = New System.Windows.Forms.Label()
        Me.label13 = New System.Windows.Forms.Label()
        Me.OpenDeviceBtn = New System.Windows.Forms.Button()
        Me.tabControl1.SuspendLayout()
        Me.tabPage2.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        CType(Me.BrightnessUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupBox4.SuspendLayout()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabPage3.SuspendLayout()
        Me.groupBox6.SuspendLayout()
        Me.groupBox2.SuspendLayout()
        CType(Me.pictureBoxV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupBox1.SuspendLayout()
        CType(Me.pictureBoxR2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictureBoxR1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabPage1.SuspendLayout()
        Me.groupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'EnumerateBtn
        '
        Me.EnumerateBtn.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.EnumerateBtn.Location = New System.Drawing.Point(331, 8)
        Me.EnumerateBtn.Name = "EnumerateBtn"
        Me.EnumerateBtn.Size = New System.Drawing.Size(72, 24)
        Me.EnumerateBtn.TabIndex = 13
        Me.EnumerateBtn.Text = "Enumerate"
        Me.EnumerateBtn.UseVisualStyleBackColor = False
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(5, 12)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(72, 24)
        Me.label1.TabIndex = 11
        Me.label1.Text = "Device Name"
        '
        'comboBoxDeviceName
        '
        Me.comboBoxDeviceName.Location = New System.Drawing.Point(85, 9)
        Me.comboBoxDeviceName.Name = "comboBoxDeviceName"
        Me.comboBoxDeviceName.Size = New System.Drawing.Size(148, 21)
        Me.comboBoxDeviceName.TabIndex = 10
        '
        'StatusBar
        '
        Me.StatusBar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.StatusBar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.StatusBar.ForeColor = System.Drawing.SystemColors.Highlight
        Me.StatusBar.Location = New System.Drawing.Point(0, 453)
        Me.StatusBar.Name = "StatusBar"
        Me.StatusBar.Size = New System.Drawing.Size(416, 24)
        Me.StatusBar.TabIndex = 12
        '
        'tabControl1
        '
        Me.tabControl1.Controls.Add(Me.tabPage2)
        Me.tabControl1.Controls.Add(Me.tabPage3)
        Me.tabControl1.Controls.Add(Me.tabPage1)
        Me.tabControl1.Location = New System.Drawing.Point(0, 38)
        Me.tabControl1.Name = "tabControl1"
        Me.tabControl1.SelectedIndex = 0
        Me.tabControl1.Size = New System.Drawing.Size(416, 404)
        Me.tabControl1.TabIndex = 9
        '
        'tabPage2
        '
        Me.tabPage2.Controls.Add(Me.CheckBoxAutoOn)
        Me.tabPage2.Controls.Add(Me.GroupBox8)
        Me.tabPage2.Controls.Add(Me.ConfigBtn)
        Me.tabPage2.Controls.Add(Me.groupBox4)
        Me.tabPage2.Controls.Add(Me.GetLiveImageBtn)
        Me.tabPage2.Controls.Add(Me.GetImageBtn)
        Me.tabPage2.Controls.Add(Me.pictureBox1)
        Me.tabPage2.Location = New System.Drawing.Point(4, 22)
        Me.tabPage2.Name = "tabPage2"
        Me.tabPage2.Size = New System.Drawing.Size(408, 378)
        Me.tabPage2.TabIndex = 1
        Me.tabPage2.Text = "  Image  "
        '
        'CheckBoxAutoOn
        '
        Me.CheckBoxAutoOn.Enabled = False
        Me.CheckBoxAutoOn.Location = New System.Drawing.Point(12, 356)
        Me.CheckBoxAutoOn.Name = "CheckBoxAutoOn"
        Me.CheckBoxAutoOn.Size = New System.Drawing.Size(248, 16)
        Me.CheckBoxAutoOn.TabIndex = 19
        Me.CheckBoxAutoOn.Text = "Enable AutoOn Event (FDU03, FDU04)"
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.BrightnessUpDown)
        Me.GroupBox8.Controls.Add(Me.SetBrightnessBtn)
        Me.GroupBox8.Location = New System.Drawing.Point(280, 200)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(120, 148)
        Me.GroupBox8.TabIndex = 18
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Brightness"
        '
        'BrightnessUpDown
        '
        Me.BrightnessUpDown.Increment = New Decimal(New Integer() {10, 0, 0, 0})
        Me.BrightnessUpDown.Location = New System.Drawing.Point(8, 24)
        Me.BrightnessUpDown.Name = "BrightnessUpDown"
        Me.BrightnessUpDown.Size = New System.Drawing.Size(44, 20)
        Me.BrightnessUpDown.TabIndex = 20
        Me.BrightnessUpDown.Value = New Decimal(New Integer() {70, 0, 0, 0})
        '
        'SetBrightnessBtn
        '
        Me.SetBrightnessBtn.Location = New System.Drawing.Point(56, 24)
        Me.SetBrightnessBtn.Name = "SetBrightnessBtn"
        Me.SetBrightnessBtn.Size = New System.Drawing.Size(56, 20)
        Me.SetBrightnessBtn.TabIndex = 19
        Me.SetBrightnessBtn.Text = "Apply"
        '
        'ConfigBtn
        '
        Me.ConfigBtn.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.ConfigBtn.Location = New System.Drawing.Point(324, 12)
        Me.ConfigBtn.Name = "ConfigBtn"
        Me.ConfigBtn.Size = New System.Drawing.Size(76, 24)
        Me.ConfigBtn.TabIndex = 12
        Me.ConfigBtn.Text = "Config..."
        Me.ConfigBtn.UseVisualStyleBackColor = False
        '
        'groupBox4
        '
        Me.groupBox4.Controls.Add(Me.textTimeout)
        Me.groupBox4.Controls.Add(Me.label16)
        Me.groupBox4.Controls.Add(Me.label15)
        Me.groupBox4.Controls.Add(Me.textImgQuality)
        Me.groupBox4.Location = New System.Drawing.Point(280, 44)
        Me.groupBox4.Name = "groupBox4"
        Me.groupBox4.Size = New System.Drawing.Size(120, 148)
        Me.groupBox4.TabIndex = 11
        Me.groupBox4.TabStop = False
        Me.groupBox4.Text = "LiveCapture"
        '
        'textTimeout
        '
        Me.textTimeout.Location = New System.Drawing.Point(8, 80)
        Me.textTimeout.Name = "textTimeout"
        Me.textTimeout.Size = New System.Drawing.Size(88, 20)
        Me.textTimeout.TabIndex = 18
        Me.textTimeout.Text = "10000"
        '
        'label16
        '
        Me.label16.Location = New System.Drawing.Point(8, 64)
        Me.label16.Name = "label16"
        Me.label16.Size = New System.Drawing.Size(96, 24)
        Me.label16.TabIndex = 17
        Me.label16.Text = "Capture Timeout"
        '
        'label15
        '
        Me.label15.Location = New System.Drawing.Point(8, 20)
        Me.label15.Name = "label15"
        Me.label15.Size = New System.Drawing.Size(96, 16)
        Me.label15.TabIndex = 16
        Me.label15.Text = "Image Quality:"
        '
        'textImgQuality
        '
        Me.textImgQuality.Location = New System.Drawing.Point(8, 36)
        Me.textImgQuality.MaxLength = 3
        Me.textImgQuality.Name = "textImgQuality"
        Me.textImgQuality.Size = New System.Drawing.Size(88, 20)
        Me.textImgQuality.TabIndex = 15
        Me.textImgQuality.Text = "50"
        '
        'GetLiveImageBtn
        '
        Me.GetLiveImageBtn.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.GetLiveImageBtn.Location = New System.Drawing.Point(96, 12)
        Me.GetLiveImageBtn.Name = "GetLiveImageBtn"
        Me.GetLiveImageBtn.Size = New System.Drawing.Size(80, 24)
        Me.GetLiveImageBtn.TabIndex = 8
        Me.GetLiveImageBtn.Text = "LiveCapture"
        Me.GetLiveImageBtn.UseVisualStyleBackColor = False
        '
        'GetImageBtn
        '
        Me.GetImageBtn.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.GetImageBtn.Location = New System.Drawing.Point(12, 12)
        Me.GetImageBtn.Name = "GetImageBtn"
        Me.GetImageBtn.Size = New System.Drawing.Size(75, 24)
        Me.GetImageBtn.TabIndex = 7
        Me.GetImageBtn.Text = "Capture"
        Me.GetImageBtn.UseVisualStyleBackColor = False
        '
        'pictureBox1
        '
        Me.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pictureBox1.Location = New System.Drawing.Point(8, 48)
        Me.pictureBox1.Name = "pictureBox1"
        Me.pictureBox1.Size = New System.Drawing.Size(260, 300)
        Me.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pictureBox1.TabIndex = 5
        Me.pictureBox1.TabStop = False
        '
        'tabPage3
        '
        Me.tabPage3.Controls.Add(Me.BtnVerify)
        Me.tabPage3.Controls.Add(Me.BtnRegister)
        Me.tabPage3.Controls.Add(Me.groupBox6)
        Me.tabPage3.Controls.Add(Me.groupBox2)
        Me.tabPage3.Controls.Add(Me.groupBox1)
        Me.tabPage3.Location = New System.Drawing.Point(4, 22)
        Me.tabPage3.Name = "tabPage3"
        Me.tabPage3.Size = New System.Drawing.Size(408, 378)
        Me.tabPage3.TabIndex = 2
        Me.tabPage3.Text = "Register/Verify"
        Me.tabPage3.Visible = False
        '
        'BtnVerify
        '
        Me.BtnVerify.BackColor = System.Drawing.SystemColors.Desktop
        Me.BtnVerify.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.BtnVerify.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnVerify.Location = New System.Drawing.Point(280, 308)
        Me.BtnVerify.Name = "BtnVerify"
        Me.BtnVerify.Size = New System.Drawing.Size(108, 23)
        Me.BtnVerify.TabIndex = 34
        Me.BtnVerify.Text = "Verify"
        Me.BtnVerify.UseVisualStyleBackColor = False
        '
        'BtnRegister
        '
        Me.BtnRegister.BackColor = System.Drawing.SystemColors.Desktop
        Me.BtnRegister.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.BtnRegister.Location = New System.Drawing.Point(52, 308)
        Me.BtnRegister.Name = "BtnRegister"
        Me.BtnRegister.Size = New System.Drawing.Size(132, 23)
        Me.BtnRegister.TabIndex = 33
        Me.BtnRegister.Text = "Register"
        Me.BtnRegister.UseVisualStyleBackColor = False
        '
        'groupBox6
        '
        Me.groupBox6.Controls.Add(Me.comboBoxSecuLevel_V)
        Me.groupBox6.Controls.Add(Me.label14)
        Me.groupBox6.Controls.Add(Me.label4)
        Me.groupBox6.Controls.Add(Me.comboBoxSecuLevel_R)
        Me.groupBox6.Location = New System.Drawing.Point(8, 8)
        Me.groupBox6.Name = "groupBox6"
        Me.groupBox6.Size = New System.Drawing.Size(392, 56)
        Me.groupBox6.TabIndex = 30
        Me.groupBox6.TabStop = False
        Me.groupBox6.Text = "Security Level"
        '
        'comboBoxSecuLevel_V
        '
        Me.comboBoxSecuLevel_V.Items.AddRange(New Object() {"LOWEST", "LOWER", "LOW", "BELOW_NORMAL", "NORMAL", "ABOVE_NORMAL", "HIGH", "HIGHER", "HIGHEST"})
        Me.comboBoxSecuLevel_V.Location = New System.Drawing.Point(272, 24)
        Me.comboBoxSecuLevel_V.Name = "comboBoxSecuLevel_V"
        Me.comboBoxSecuLevel_V.Size = New System.Drawing.Size(112, 21)
        Me.comboBoxSecuLevel_V.TabIndex = 24
        Me.comboBoxSecuLevel_V.Text = "NORMAL"
        '
        'label14
        '
        Me.label14.Location = New System.Drawing.Point(208, 24)
        Me.label14.Name = "label14"
        Me.label14.Size = New System.Drawing.Size(64, 24)
        Me.label14.TabIndex = 23
        Me.label14.Text = "Verification"
        '
        'label4
        '
        Me.label4.Location = New System.Drawing.Point(8, 24)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(72, 24)
        Me.label4.TabIndex = 22
        Me.label4.Text = "Registration"
        '
        'comboBoxSecuLevel_R
        '
        Me.comboBoxSecuLevel_R.Items.AddRange(New Object() {"LOWEST", "LOWER", "LOW", "BELOW_NORMAL", "NORMAL", "ABOVE_NORMAL", "HIGH", "HIGHER", "HIGHEST"})
        Me.comboBoxSecuLevel_R.Location = New System.Drawing.Point(80, 24)
        Me.comboBoxSecuLevel_R.Name = "comboBoxSecuLevel_R"
        Me.comboBoxSecuLevel_R.Size = New System.Drawing.Size(112, 21)
        Me.comboBoxSecuLevel_R.TabIndex = 21
        Me.comboBoxSecuLevel_R.Text = "NORMAL"
        '
        'groupBox2
        '
        Me.groupBox2.Controls.Add(Me.progressBar_V1)
        Me.groupBox2.Controls.Add(Me.pictureBoxV1)
        Me.groupBox2.Controls.Add(Me.BtnCapture3)
        Me.groupBox2.Controls.Add(Me.comboBox1)
        Me.groupBox2.Location = New System.Drawing.Point(264, 76)
        Me.groupBox2.Name = "groupBox2"
        Me.groupBox2.Size = New System.Drawing.Size(136, 220)
        Me.groupBox2.TabIndex = 29
        Me.groupBox2.TabStop = False
        Me.groupBox2.Text = "Verification"
        '
        'progressBar_V1
        '
        Me.progressBar_V1.Location = New System.Drawing.Point(16, 152)
        Me.progressBar_V1.Name = "progressBar_V1"
        Me.progressBar_V1.Size = New System.Drawing.Size(104, 12)
        Me.progressBar_V1.TabIndex = 31
        '
        'pictureBoxV1
        '
        Me.pictureBoxV1.BackColor = System.Drawing.SystemColors.Window
        Me.pictureBoxV1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pictureBoxV1.Location = New System.Drawing.Point(16, 24)
        Me.pictureBoxV1.Name = "pictureBoxV1"
        Me.pictureBoxV1.Size = New System.Drawing.Size(104, 128)
        Me.pictureBoxV1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pictureBoxV1.TabIndex = 29
        Me.pictureBoxV1.TabStop = False
        '
        'BtnCapture3
        '
        Me.BtnCapture3.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.BtnCapture3.Location = New System.Drawing.Point(16, 176)
        Me.BtnCapture3.Name = "BtnCapture3"
        Me.BtnCapture3.Size = New System.Drawing.Size(104, 23)
        Me.BtnCapture3.TabIndex = 27
        Me.BtnCapture3.Text = "Capture V1"
        Me.BtnCapture3.UseVisualStyleBackColor = False
        '
        'comboBox1
        '
        Me.comboBox1.Items.AddRange(New Object() {"LOWEST", "LOWER", "LOW", "BELOW_NORMAL", "NORMAL", "ABOVE_NORMAL", "HIGH", "HIGHER", "HIGHEST"})
        Me.comboBox1.Location = New System.Drawing.Point(48, -40)
        Me.comboBox1.Name = "comboBox1"
        Me.comboBox1.Size = New System.Drawing.Size(88, 21)
        Me.comboBox1.TabIndex = 30
        Me.comboBox1.Text = "NORMAL"
        '
        'groupBox1
        '
        Me.groupBox1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.groupBox1.Controls.Add(Me.progressBar_R2)
        Me.groupBox1.Controls.Add(Me.progressBar_R1)
        Me.groupBox1.Controls.Add(Me.pictureBoxR2)
        Me.groupBox1.Controls.Add(Me.pictureBoxR1)
        Me.groupBox1.Controls.Add(Me.BtnCapture1)
        Me.groupBox1.Controls.Add(Me.BtnCapture2)
        Me.groupBox1.Location = New System.Drawing.Point(8, 76)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(244, 220)
        Me.groupBox1.TabIndex = 28
        Me.groupBox1.TabStop = False
        Me.groupBox1.Text = "Registration"
        '
        'progressBar_R2
        '
        Me.progressBar_R2.Location = New System.Drawing.Point(128, 152)
        Me.progressBar_R2.Name = "progressBar_R2"
        Me.progressBar_R2.Size = New System.Drawing.Size(104, 12)
        Me.progressBar_R2.TabIndex = 29
        '
        'progressBar_R1
        '
        Me.progressBar_R1.Location = New System.Drawing.Point(8, 152)
        Me.progressBar_R1.Name = "progressBar_R1"
        Me.progressBar_R1.Size = New System.Drawing.Size(104, 12)
        Me.progressBar_R1.TabIndex = 28
        '
        'pictureBoxR2
        '
        Me.pictureBoxR2.BackColor = System.Drawing.SystemColors.Window
        Me.pictureBoxR2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pictureBoxR2.Location = New System.Drawing.Point(128, 24)
        Me.pictureBoxR2.Name = "pictureBoxR2"
        Me.pictureBoxR2.Size = New System.Drawing.Size(104, 128)
        Me.pictureBoxR2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pictureBoxR2.TabIndex = 27
        Me.pictureBoxR2.TabStop = False
        '
        'pictureBoxR1
        '
        Me.pictureBoxR1.BackColor = System.Drawing.SystemColors.Window
        Me.pictureBoxR1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pictureBoxR1.Location = New System.Drawing.Point(8, 24)
        Me.pictureBoxR1.Name = "pictureBoxR1"
        Me.pictureBoxR1.Size = New System.Drawing.Size(104, 128)
        Me.pictureBoxR1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pictureBoxR1.TabIndex = 26
        Me.pictureBoxR1.TabStop = False
        '
        'BtnCapture1
        '
        Me.BtnCapture1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.BtnCapture1.Location = New System.Drawing.Point(8, 176)
        Me.BtnCapture1.Name = "BtnCapture1"
        Me.BtnCapture1.Size = New System.Drawing.Size(104, 23)
        Me.BtnCapture1.TabIndex = 23
        Me.BtnCapture1.Text = "Capture R1"
        Me.BtnCapture1.UseVisualStyleBackColor = False
        '
        'BtnCapture2
        '
        Me.BtnCapture2.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.BtnCapture2.Location = New System.Drawing.Point(128, 176)
        Me.BtnCapture2.Name = "BtnCapture2"
        Me.BtnCapture2.Size = New System.Drawing.Size(104, 23)
        Me.BtnCapture2.TabIndex = 24
        Me.BtnCapture2.Text = "Capture R2"
        Me.BtnCapture2.UseVisualStyleBackColor = False
        '
        'tabPage1
        '
        Me.tabPage1.Controls.Add(Me.GetBtn)
        Me.tabPage1.Controls.Add(Me.groupBox3)
        Me.tabPage1.Location = New System.Drawing.Point(4, 22)
        Me.tabPage1.Name = "tabPage1"
        Me.tabPage1.Size = New System.Drawing.Size(408, 378)
        Me.tabPage1.TabIndex = 0
        Me.tabPage1.Text = "DeviceInfo"
        Me.tabPage1.Visible = False
        '
        'GetBtn
        '
        Me.GetBtn.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.GetBtn.Location = New System.Drawing.Point(288, 16)
        Me.GetBtn.Name = "GetBtn"
        Me.GetBtn.Size = New System.Drawing.Size(96, 24)
        Me.GetBtn.TabIndex = 43
        Me.GetBtn.Text = "Get"
        Me.GetBtn.UseVisualStyleBackColor = False
        '
        'groupBox3
        '
        Me.groupBox3.Controls.Add(Me.textImageDPI)
        Me.groupBox3.Controls.Add(Me.textImageHeight)
        Me.groupBox3.Controls.Add(Me.textImageWidth)
        Me.groupBox3.Controls.Add(Me.textSerialNum)
        Me.groupBox3.Controls.Add(Me.textFWVersion)
        Me.groupBox3.Controls.Add(Me.textDeviceID)
        Me.groupBox3.Controls.Add(Me.textBrightness)
        Me.groupBox3.Controls.Add(Me.textGain)
        Me.groupBox3.Controls.Add(Me.textContrast)
        Me.groupBox3.Controls.Add(Me.label12)
        Me.groupBox3.Controls.Add(Me.label11)
        Me.groupBox3.Controls.Add(Me.label10)
        Me.groupBox3.Controls.Add(Me.label9)
        Me.groupBox3.Controls.Add(Me.label8)
        Me.groupBox3.Controls.Add(Me.label7)
        Me.groupBox3.Controls.Add(Me.label6)
        Me.groupBox3.Controls.Add(Me.label5)
        Me.groupBox3.Controls.Add(Me.label13)
        Me.groupBox3.Location = New System.Drawing.Point(8, 8)
        Me.groupBox3.Name = "groupBox3"
        Me.groupBox3.Size = New System.Drawing.Size(264, 248)
        Me.groupBox3.TabIndex = 41
        Me.groupBox3.TabStop = False
        Me.groupBox3.Text = "DeviceInfo"
        '
        'textImageDPI
        '
        Me.textImageDPI.Enabled = False
        Me.textImageDPI.Location = New System.Drawing.Point(96, 144)
        Me.textImageDPI.Name = "textImageDPI"
        Me.textImageDPI.Size = New System.Drawing.Size(152, 20)
        Me.textImageDPI.TabIndex = 66
        '
        'textImageHeight
        '
        Me.textImageHeight.Enabled = False
        Me.textImageHeight.Location = New System.Drawing.Point(96, 120)
        Me.textImageHeight.Name = "textImageHeight"
        Me.textImageHeight.Size = New System.Drawing.Size(152, 20)
        Me.textImageHeight.TabIndex = 65
        '
        'textImageWidth
        '
        Me.textImageWidth.Enabled = False
        Me.textImageWidth.Location = New System.Drawing.Point(96, 96)
        Me.textImageWidth.Name = "textImageWidth"
        Me.textImageWidth.Size = New System.Drawing.Size(152, 20)
        Me.textImageWidth.TabIndex = 64
        '
        'textSerialNum
        '
        Me.textSerialNum.Enabled = False
        Me.textSerialNum.Location = New System.Drawing.Point(96, 72)
        Me.textSerialNum.Name = "textSerialNum"
        Me.textSerialNum.Size = New System.Drawing.Size(152, 20)
        Me.textSerialNum.TabIndex = 63
        '
        'textFWVersion
        '
        Me.textFWVersion.Enabled = False
        Me.textFWVersion.Location = New System.Drawing.Point(96, 48)
        Me.textFWVersion.Name = "textFWVersion"
        Me.textFWVersion.Size = New System.Drawing.Size(152, 20)
        Me.textFWVersion.TabIndex = 62
        '
        'textDeviceID
        '
        Me.textDeviceID.Enabled = False
        Me.textDeviceID.Location = New System.Drawing.Point(96, 24)
        Me.textDeviceID.Name = "textDeviceID"
        Me.textDeviceID.Size = New System.Drawing.Size(152, 20)
        Me.textDeviceID.TabIndex = 61
        '
        'textBrightness
        '
        Me.textBrightness.Enabled = False
        Me.textBrightness.Location = New System.Drawing.Point(96, 168)
        Me.textBrightness.Name = "textBrightness"
        Me.textBrightness.Size = New System.Drawing.Size(152, 20)
        Me.textBrightness.TabIndex = 58
        '
        'textGain
        '
        Me.textGain.Enabled = False
        Me.textGain.Location = New System.Drawing.Point(96, 216)
        Me.textGain.Name = "textGain"
        Me.textGain.Size = New System.Drawing.Size(152, 20)
        Me.textGain.TabIndex = 57
        '
        'textContrast
        '
        Me.textContrast.Enabled = False
        Me.textContrast.Location = New System.Drawing.Point(96, 192)
        Me.textContrast.Name = "textContrast"
        Me.textContrast.Size = New System.Drawing.Size(152, 20)
        Me.textContrast.TabIndex = 56
        '
        'label12
        '
        Me.label12.Location = New System.Drawing.Point(16, 216)
        Me.label12.Name = "label12"
        Me.label12.Size = New System.Drawing.Size(72, 16)
        Me.label12.TabIndex = 55
        Me.label12.Text = "Gain"
        Me.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label11
        '
        Me.label11.Location = New System.Drawing.Point(16, 192)
        Me.label11.Name = "label11"
        Me.label11.Size = New System.Drawing.Size(72, 16)
        Me.label11.TabIndex = 54
        Me.label11.Text = "Contrast"
        Me.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label10
        '
        Me.label10.Location = New System.Drawing.Point(16, 168)
        Me.label10.Name = "label10"
        Me.label10.Size = New System.Drawing.Size(72, 16)
        Me.label10.TabIndex = 53
        Me.label10.Text = "Brightness"
        Me.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label9
        '
        Me.label9.Location = New System.Drawing.Point(16, 144)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(72, 16)
        Me.label9.TabIndex = 51
        Me.label9.Text = "Image DPI"
        Me.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label8
        '
        Me.label8.Location = New System.Drawing.Point(16, 72)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(72, 16)
        Me.label8.TabIndex = 49
        Me.label8.Text = "Serial #"
        Me.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label7
        '
        Me.label7.Location = New System.Drawing.Point(16, 48)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(72, 16)
        Me.label7.TabIndex = 47
        Me.label7.Text = "F/W Version"
        Me.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label6
        '
        Me.label6.Location = New System.Drawing.Point(16, 120)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(72, 16)
        Me.label6.TabIndex = 45
        Me.label6.Text = "Image Height"
        Me.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label5
        '
        Me.label5.Location = New System.Drawing.Point(16, 96)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(72, 16)
        Me.label5.TabIndex = 43
        Me.label5.Text = "Image Width"
        Me.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label13
        '
        Me.label13.Location = New System.Drawing.Point(16, 24)
        Me.label13.Name = "label13"
        Me.label13.Size = New System.Drawing.Size(72, 16)
        Me.label13.TabIndex = 41
        Me.label13.Text = "Device ID"
        Me.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'OpenDeviceBtn
        '
        Me.OpenDeviceBtn.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.OpenDeviceBtn.Location = New System.Drawing.Point(240, 8)
        Me.OpenDeviceBtn.Name = "OpenDeviceBtn"
        Me.OpenDeviceBtn.Size = New System.Drawing.Size(80, 24)
        Me.OpenDeviceBtn.TabIndex = 14
        Me.OpenDeviceBtn.Text = "Init"
        Me.OpenDeviceBtn.UseVisualStyleBackColor = False
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(416, 477)
        Me.Controls.Add(Me.OpenDeviceBtn)
        Me.Controls.Add(Me.EnumerateBtn)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.comboBoxDeviceName)
        Me.Controls.Add(Me.StatusBar)
        Me.Controls.Add(Me.tabControl1)
        Me.Name = "Form1"
        Me.Text = "Matching VB .NET sample"
        Me.tabControl1.ResumeLayout(False)
        Me.tabPage2.ResumeLayout(False)
        Me.GroupBox8.ResumeLayout(False)
        CType(Me.BrightnessUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupBox4.ResumeLayout(False)
        Me.groupBox4.PerformLayout()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabPage3.ResumeLayout(False)
        Me.groupBox6.ResumeLayout(False)
        Me.groupBox2.ResumeLayout(False)
        CType(Me.pictureBoxV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupBox1.ResumeLayout(False)
        CType(Me.pictureBoxR2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictureBoxR1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabPage1.ResumeLayout(False)
        Me.groupBox3.ResumeLayout(False)
        Me.groupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
      m_LedOn = False
      comboBoxSecuLevel_R.SelectedIndex = 4
      comboBoxSecuLevel_V.SelectedIndex = 3
      EnableButtons(False)

      m_FPM = New SGFingerPrintManager
      EnumerateBtn_Click(sender, e)
   End Sub

   Private Sub EnumerateBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnumerateBtn.Click
      Dim iError As Int32
      Dim enum_device As String
      Dim i As Int32

      comboBoxDeviceName.Items.Clear()

      ' Enumerate Device
      iError = m_FPM.EnumerateDevice()

      'Get enumeration info into SGFPMDeviceList
      ReDim m_DevList(m_FPM.NumberOfDevice)


      For i = 0 To m_FPM.NumberOfDevice - 1

         m_DevList(i) = New SGFPMDeviceList
         m_FPM.GetEnumDeviceInfo(i, m_DevList(i))

         enum_device = m_DevList(i).DevName.ToString() + " : " + Convert.ToString(m_DevList(i).DevID)
         comboBoxDeviceName.Items.Add(enum_device)

         If (comboBoxDeviceName.Items.Count > 0) Then
            comboBoxDeviceName.SelectedIndex = 0
         End If

      Next

   End Sub

   Private Sub OpenDeviceBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenDeviceBtn.Click
      Dim iError As Int32
      Dim device_name As SGFPMDeviceName
      Dim device_id As Int32

      If m_FPM.NumberOfDevice = 0 Then
         Return
      End If

      device_name = m_DevList(comboBoxDeviceName.SelectedIndex).DevName
      device_id = m_DevList(comboBoxDeviceName.SelectedIndex).DevID

      iError = m_FPM.Init(device_name)
      iError = m_FPM.OpenDevice(device_id)

      If (iError = SGFPMError.ERROR_NONE) Then
         GetBtn_Click(sender, e)
         StatusBar.Text = "Initialization Success"
         EnableButtons(True)

            ' FDU03 or FDU04 only
            If ((device_name = SGFPMDeviceName.DEV_FDU03) Or (device_name = SGFPMDeviceName.DEV_FDU04)) Then
                CheckBoxAutoOn.Enabled = True
            Else
                CheckBoxAutoOn.Enabled = False
            End If

        Else
            DisplayError("OpenDevice", iError)
      End If


   End Sub

    Private Sub SetLedOnBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        m_LedOn = Not m_LedOn
        m_FPM.SetLedOn(m_LedOn)
    End Sub

    Private Sub GetImageBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetImageBtn.Click
        Dim fp_image() As Byte
        Dim iError As Int32
        Dim elap_time As Int32

        ReDim fp_image(m_ImageWidth * m_ImageHeight)

        elap_time = Environment.TickCount

        iError = m_FPM.GetImage(fp_image)

        If (iError = SGFPMError.ERROR_NONE) Then
            elap_time = Environment.TickCount - elap_time
            DrawImage(fp_image, pictureBox1)
            StatusBar.Text = "Capture Time : " + Convert.ToString(elap_time) + " ms"
        Else
            DisplayError("GetImage", iError)
        End If

    End Sub

    Private Sub GetLiveImageBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetLiveImageBtn.Click
        Dim fp_image() As Byte
        Dim iError As Int32
        Dim elap_time As Int32
        Dim timeout As Int32
        Dim quality As Int32

        ReDim fp_image(m_ImageWidth * m_ImageHeight)

        timeout = Convert.ToInt32(textTimeout.Text)
        quality = Convert.ToInt32(textImgQuality.Text)
        elap_time = Environment.TickCount

        iError = m_FPM.GetImageEx(fp_image, timeout, pictureBox1.Handle.ToInt32(), quality)

        If (iError = SGFPMError.ERROR_NONE) Then
            elap_time = Environment.TickCount - elap_time
            StatusBar.Text = "Capture Time : " + Convert.ToString(elap_time) + " ms"
        Else
            DisplayError("GetImage", iError)
        End If

    End Sub

    Private Sub ConfigBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConfigBtn.Click
        m_FPM.Configure(Handle.ToInt32())
    End Sub

    Private Sub BtnCapture1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCapture1.Click
        Dim fp_image() As Byte
        Dim iError As Int32
        Dim img_qlty As Int32
        ReDim fp_image(m_ImageWidth * m_ImageHeight)

        iError = m_FPM.GetImage(fp_image)

        If iError = SGFPMError.ERROR_NONE Then
            DrawImage(fp_image, pictureBoxR1)
            m_FPM.GetImageQuality(m_ImageWidth, m_ImageHeight, fp_image, img_qlty)
            progressBar_R1.Value = img_qlty
            iError = m_FPM.CreateTemplate(fp_image, m_RegMin1)

            If (iError = SGFPMError.ERROR_NONE) Then
                StatusBar.Text = "First image is captured"
            Else
                DisplayError("CreateTemplate", iError)
            End If
        Else
            DisplayError("GetImage", iError)
        End If

    End Sub

    Private Sub BtnCapture2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCapture2.Click
        Dim fp_image() As Byte
        Dim iError As Int32
        Dim img_qlty As Int32
        ReDim fp_image(m_ImageWidth * m_ImageHeight)

        iError = m_FPM.GetImage(fp_image)

        If iError = SGFPMError.ERROR_NONE Then
            DrawImage(fp_image, pictureBoxR2)
            m_FPM.GetImageQuality(m_ImageWidth, m_ImageHeight, fp_image, img_qlty)
            progressBar_R2.Value = img_qlty
            iError = m_FPM.CreateTemplate(fp_image, m_RegMin2)

            If (iError = SGFPMError.ERROR_NONE) Then
                StatusBar.Text = "Second image  is captured"
            Else
                DisplayError("CreateTemplate", iError)
            End If
        Else
            DisplayError("GetImage", iError)
        End If


    End Sub

    Private Sub BtnCapture3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCapture3.Click
        Dim fp_image() As Byte
        Dim iError As Int32
        Dim img_qlty As Int32
        ReDim fp_image(m_ImageWidth * m_ImageHeight)

        iError = m_FPM.GetImage(fp_image)

        If iError = SGFPMError.ERROR_NONE Then
            DrawImage(fp_image, pictureBoxV1)
            m_FPM.GetImageQuality(m_ImageWidth, m_ImageHeight, fp_image, img_qlty)
            progressBar_V1.Value = img_qlty
            iError = m_FPM.CreateTemplate(fp_image, m_VrfMin)

            If (iError = SGFPMError.ERROR_NONE) Then
                StatusBar.Text = "Image for verification is captured"
            Else
                DisplayError("CreateTemplate", iError)
            End If
        Else
            DisplayError("GetImage", iError)
        End If

    End Sub

    Private Sub BtnRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRegister.Click
        Dim iError As Int32
        Dim matched As Boolean
        Dim match_score As Int32
        Dim secu_level As SGFPMSecurityLevel


        matched = False
        match_score = 0

        secu_level = comboBoxSecuLevel_R.SelectedIndex

        iError = m_FPM.MatchTemplate(m_RegMin1, m_RegMin2, secu_level, matched)
        iError = m_FPM.GetMatchingScore(m_RegMin1, m_RegMin2, match_score)

        If (iError = SGFPMError.ERROR_NONE) Then
            If (matched) Then
                StatusBar.Text = "Registration Success, Matching Score: " + Convert.ToString(match_score)
            Else
                StatusBar.Text = "Registration Failed"
            End If

        Else
            DisplayError("GetMatchingScore", iError)
        End If

    End Sub

    Private Sub BtnVerify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnVerify.Click
        Dim iError As Int32
        Dim matched1 As Boolean
        Dim matched2 As Boolean
        Dim secu_level As SGFPMSecurityLevel

        secu_level = comboBoxSecuLevel_V.SelectedIndex

        iError = m_FPM.MatchTemplate(m_RegMin1, m_VrfMin, secu_level, matched1)
        iError = m_FPM.MatchTemplate(m_RegMin2, m_VrfMin, secu_level, matched2)

        If (iError = SGFPMError.ERROR_NONE) Then
            If (matched1 And matched2) Then
                StatusBar.Text = "Verification Success"
            Else
                StatusBar.Text = "Verification Failed"
            End If

        Else
            DisplayError("MatchTemplate", iError)
        End If

    End Sub

    Private Sub GetBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetBtn.Click
        Dim pInfo As SGFPMDeviceInfoParam
        Dim iError As Int32

        pInfo = New SGFPMDeviceInfoParam
        iError = m_FPM.GetDeviceInfo(pInfo)

        If (iError = SGFPMError.ERROR_NONE) Then
            m_ImageWidth = pInfo.ImageWidth
            m_ImageHeight = pInfo.ImageHeight

            textDeviceID.Text = Convert.ToString(pInfo.DeviceID)
            textFWVersion.Text = Convert.ToString(pInfo.FWVersion, 16)

            'Device Serial Number
            Dim encoding As New ASCIIEncoding
            textSerialNum.Text = encoding.GetString(pInfo.DeviceSN)

            textImageDPI.Text = Convert.ToString(pInfo.ImageDPI)
            textImageHeight.Text = Convert.ToString(pInfo.ImageHeight)
            textImageWidth.Text = Convert.ToString(pInfo.ImageWidth)
            textBrightness.Text = Convert.ToString(pInfo.Brightness)
            textContrast.Text = Convert.ToString(pInfo.Contrast)
            textGain.Text = Convert.ToString(pInfo.Gain)

            BrightnessUpDown.Value = pInfo.Brightness
        End If


    End Sub

    Private Sub SetBrightnessBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetBrightnessBtn.Click
        Dim iError As Int32
        Dim brightness As Int32

        brightness = BrightnessUpDown.Value
        iError = m_FPM.SetBrightness(brightness)
        If (iError = SGFPMError.ERROR_NONE) Then
            StatusBar.Text = "SetBrightness success"
            GetBtn_Click(sender, e)
        Else
            DisplayError("SetBrightness", iError)
        End If

    End Sub

    Private Sub EnableButtons(ByVal enable As Boolean)
        ConfigBtn.Enabled = enable
        GetImageBtn.Enabled = enable
        GetLiveImageBtn.Enabled = enable
        BtnCapture1.Enabled = enable
        BtnCapture2.Enabled = enable
        BtnCapture3.Enabled = enable
        BtnRegister.Enabled = enable
        BtnVerify.Enabled = enable
        GetBtn.Enabled = enable
        SetBrightnessBtn.Enabled = enable
    End Sub

    Private Sub DrawImage(ByVal imgData() As Byte, ByVal picBox As PictureBox)
        Dim colorval As Int32
        Dim bmp As Bitmap
        Dim i, j As Int32

        bmp = New Bitmap(m_ImageWidth, m_ImageHeight)
        picBox.Image = bmp

        For i = 0 To bmp.Width - 1
            For j = 0 To bmp.Height - 1
                colorval = imgData((j * m_ImageWidth) + i)
                bmp.SetPixel(i, j, Color.FromArgb(colorval, colorval, colorval))
            Next j
        Next i

        picBox.Refresh()
    End Sub

    Private Sub CheckBoxAutoOn_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxAutoOn.CheckedChanged
        If (CheckBoxAutoOn.Checked) Then
            m_FPM.EnableAutoOnEvent(True, Me.Handle.ToInt32())
        Else
            m_FPM.EnableAutoOnEvent(False, 0)
        End If
    End Sub


    Protected Overrides Sub WndProc(ByRef msg As Message)
        If (msg.Msg = SGFPMMessages.DEV_AUTOONEVENT) Then
            If (msg.WParam.ToInt32() = SGFPMAutoOnEvent.FINGER_ON) Then
                StatusBar.Text = "Device Message: Finger On"
            ElseIf (msg.WParam.ToInt32() = SGFPMAutoOnEvent.FINGER_OFF) Then
                StatusBar.Text = "Device Message: Finger Off"
            End If
        End If

        MyBase.WndProc(msg)
    End Sub

Private Sub DisplayError(ByVal funcName As String, ByVal iError As Int32)
    Dim text As String

        text = ""
      Select Case iError
            Case 0                             'SGFDX_ERROR_NONE				= 0,
               text = "Error none"

            Case 1 'SGFDX_ERROR_CREATION_FAILED	= 1,
               text = "Can not create object"

            Case 2 '   SGFDX_ERROR_FUNCTION_FAILED	= 2,
               text = "Function Failed"

            Case 3 '   SGFDX_ERROR_INVALID_PARAM	= 3,
               text = "Invalid Parameter"

            Case 4 '   SGFDX_ERROR_NOT_USED			= 4,
               text = "Not used function"

            Case 5 'SGFDX_ERROR_DLLLOAD_FAILED	= 5,
               text = "Can not create object"

            Case 6 'SGFDX_ERROR_DLLLOAD_FAILED_DRV	= 6,
               text = "Can not load device driver"

            Case 7 'SGFDX_ERROR_DLLLOAD_FAILED_ALGO = 7,
               text = "Can not load sgfpamx.dll"

            Case 51 'SGFDX_ERROR_SYSLOAD_FAILED	   = 51,	// system file load fail
               text = "Can not load driver kernel file"

            Case 52 'SGFDX_ERROR_INITIALIZE_FAILED  = 52,   // chip initialize fail
               text = "Failed to initialize the device"

            Case 53 'SGFDX_ERROR_LINE_DROPPED		   = 53,   // image data drop
               text = "Data transmission is not good"

            Case 54 'SGFDX_ERROR_TIME_OUT			   = 54,   // getliveimage timeout error
               text = "Time out"

            Case 55 'SGFDX_ERROR_DEVICE_NOT_FOUND	= 55,   // device not found
               text = "Device not found"

            Case 56 'SGFDX_ERROR_DRVLOAD_FAILED	   = 56,   // dll file load fail
               text = "Can not load driver file"

            Case 57 'SGFDX_ERROR_WRONG_IMAGE		   = 57,   // wrong image
               text = "Wrong Image"

            Case 58 'SGFDX_ERROR_LACK_OF_BANDWIDTH  = 58,   // USB Bandwith Lack Error
               text = "Lack of USB Bandwith"

            Case 59 'SGFDX_ERROR_DEV_ALREADY_OPEN	= 59,   // Device Exclusive access Error
               text = "Device is already opened"

            Case 60 'SGFDX_ERROR_GETSN_FAILED		   = 60,   // Fail to get Device Serial Number
               text = "Device serial number error"

            Case 61 'SGFDX_ERROR_UNSUPPORTED_DEV		   = 61,   // Unsupported device
               text = "Unsupported device"

               ' Extract & Verification error
            Case 101 'SGFDX_ERROR_FEAT_NUMBER		= 101, // utoo small number of minutiae
               text = "The number of minutiae is too small"

            Case 102 'SGFDX_ERROR_INVALID_TEMPLATE_TYPE		= 102, // wrong template type
               text = "Template is invalid"


            Case 103 'SGFDX_ERROR_INVALID_TEMPLATE1		= 103, // wrong template type
               text = "1st template is invalid"

            Case 104 'SGFDX_ERROR_INVALID_TEMPLATE2		= 104, // vwrong template type
               text = "2nd template is invalid"

            Case 105 'SGFDX_ERROR_EXTRACT_FAIL		= 105, // extraction fail
               text = "Minutiae extraction failed"

            Case 106 'SGFDX_ERROR_MATCH_FAIL		= 106, // matching  fail
               text = "Matching failed"


     End Select

     text = funcName + " Error # " + Convert.ToString(iError) + " :" + text
     StatusBar.Text = text

   End Sub


End Class
