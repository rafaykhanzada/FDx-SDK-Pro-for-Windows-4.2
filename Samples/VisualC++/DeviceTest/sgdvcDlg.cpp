#include "stdafx.h"
#include "sgdvc.h"
#include "sgdvcDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

// 2005.5.3, For Continuous Capture
bool g_StopFlag;
bool g_Stopped;
DWORD g_StartTime;
DWORD g_EndTime;
// 2005.5.3, For Continuous Capture


/////////////////////////////////////////////////////////////////////////////
// CAboutDlg dialog used for App About

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

	// Dialog Data
	//{{AFX_DATA(CAboutDlg)
	enum { IDD = IDD_ABOUTBOX };
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CAboutDlg)
protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

	// Implementation
protected:
	//{{AFX_MSG(CAboutDlg)
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
	//{{AFX_DATA_INIT(CAboutDlg)
	//}}AFX_DATA_INIT
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CAboutDlg)
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
	//{{AFX_MSG_MAP(CAboutDlg)
	// No message handlers
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CSgdvcDlg dialog

CSgdvcDlg::CSgdvcDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CSgdvcDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CSgdvcDlg)
	m_ErrorDisplay = _T("");
	m_ImgHeight = 0;
	m_ImgQuality = 0;
	m_ImgWidth = 0;
	m_DevName = _T("");
	m_DevSN = _T("");
	m_DevID = 0;
	m_Timeout = 0;
	m_FWVer = 0;
	m_Brightness = 0;
	m_Contrast = 0;
	m_Gain = 0;
	m_ModelName = _T("");
	m_FWVersion = _T("");
	m_ImageDPI = 0;
	m_CaptureTime = _T("");
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CSgdvcDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CSgdvcDlg)
	DDX_Control(pDX, IDC_AUTOONBTN, m_AutoOnBtn);
	//DDX_Control(pDX, IDC_IMGFRAME, m_ImgFrame);
	DDX_Control(pDX, IDC_ENUM_DEVID, m_ComboDevID);
	DDX_Control(pDX, IDC_CONT_CAPTURE, m_ConCapBtn);
	DDX_Control(pDX, IDC_LIVECAPTURE, m_LiveCaptureBtn);
	DDX_Control(pDX, IDC_CAPTURE, m_CaptureBtn);
	DDX_Control(pDX, IDC_CONFIG, m_ConfigBtn);
	DDX_Control(pDX, IDC_LEDONOFF, m_LedBtn);
	DDX_Control(pDX, IDC_INITBTN, m_InitBtn);
	DDX_Control(pDX, IDC_STATIC_DRAW, m_ImageBox);
	DDX_Text(pDX, IDC_STATIC_STATUS, m_ErrorDisplay);
	DDX_Text(pDX, IDC_EDIT_IMG_HEIGHT, m_ImgHeight);
	DDX_Text(pDX, IDC_EDIT_IMG_QUALITY, m_ImgQuality);
	DDX_Text(pDX, IDC_EDIT_IMG_WIDTH, m_ImgWidth);
	DDX_Text(pDX, IDC_EDIT_SN, m_DevSN);
	DDX_Text(pDX, IDC_EDIT_ID, m_DevID);
	DDX_Text(pDX, IDC_EDIT_TIMEOUT, m_Timeout);
	DDX_Text(pDX, IDC_EDIT_BRIGHTNESS, m_Brightness);
	DDX_Text(pDX, IDC_EDIT_Contrast, m_Contrast);
	DDX_Text(pDX, IDC_EDIT_Gain, m_Gain);
	DDX_Text(pDX, IDC_EDIT_FWVersion, m_FWVersion);
	DDX_Text(pDX, IDC_EDIT_ImageDPI, m_ImageDPI);
	//}}AFX_DATA_MAP
	DDX_Control(pDX, IDC_CHECK_FINGER_LIVENESS, m_CheckFingerLivenessBtn);
	DDX_Control(pDX, IDC_COMBO_PORTS, m_ports);
}

BEGIN_MESSAGE_MAP(CSgdvcDlg, CDialog)
	//{{AFX_MSG_MAP(CSgdvcDlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_WM_DESTROY()
	ON_BN_CLICKED(IDC_INITBTN, OnInitbtn)
	ON_BN_CLICKED(IDC_LEDONOFF, OnLedonoff)
	ON_BN_CLICKED(IDC_CONFIG, OnConfig)
	ON_BN_CLICKED(IDC_CAPTURE, OnCapture)
	ON_BN_CLICKED(IDC_LIVECAPTURE, OnLivecapture)
	ON_BN_CLICKED(IDC_ENUMBTN, OnEnumBtn)
	ON_COMMAND(ID_MENU_EXIT, OnMenuExit)
	ON_COMMAND(ID_MENU_SAVE_BMP, OnMenuSaveBmp)
	ON_COMMAND(ID_MENU_SAVE_RAW, OnMenuSaveRaw)
	ON_COMMAND(ID_MENU_ABOUT, OnMenuAbout)
	ON_BN_CLICKED(IDC_CONT_CAPTURE, OnContCapture)
	ON_BN_CLICKED(IDC_AUTOONBTN, OnAutoonbtn)
	ON_MESSAGE (WM_APP_SGAUTOONEVENT, OnAutoOnEvent) // 2005.6.2, JKang
	//	ON_BN_CLICKED(IDC_BTN_TEST, OnBtnTest)
	//}}AFX_MSG_MAP
	ON_BN_CLICKED(IDC_CHECK_FINGER_LIVENESS, &CSgdvcDlg::OnBnClickedCheckFingerLiveness)
	ON_BN_CLICKED(IDC_BUTTON_SDA_INIT, &CSgdvcDlg::OnBnClickedButtonSdaInit)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CSgdvcDlg message handlers
void CSgdvcDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialog::OnSysCommand(nID, lParam);
	}
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.
void CSgdvcDlg::OnPaint() 
{

	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, (WPARAM) dc.GetSafeHdc(), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		if (m_Sensor && m_ImgBuf && m_DibCtl)
			DisplayImage();
		CDialog::OnPaint();
	}
}

// The system calls this to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CSgdvcDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

BOOL CSgdvcDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Add "About..." menu item to system menu.

	// IDM_ABOUTBOX must be in the system command range.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		CString strAboutMenu;
		strAboutMenu.LoadString(IDS_ABOUTBOX);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// Set Menu
	CMenu pMenu;
	pMenu.LoadMenu(IDR_MENU1);
	SetMenu(&pMenu);


	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon


	// TODO: Add extra initialization here
	// Initialize Variables
	m_ImgBuf = NULL;  // bImage buffer
	m_DibCtl = NULL;  // for drawing fp bImage on first window
	m_Sensor = NULL;
	//m_DevID = -1;
	m_Timeout = 10000; // 10 seconds
	m_ImgQuality = 50; // Default Value


	m_LedBtn.EnableWindow(FALSE);
	m_CaptureBtn.EnableWindow(FALSE);
	m_ConCapBtn.EnableWindow(FALSE);
	m_LiveCaptureBtn.EnableWindow(FALSE);
	m_ConfigBtn.EnableWindow(FALSE);

	g_StopFlag = true;

	if (CreateSGFPMObject(&m_Sensor) == SGFDX_ERROR_NONE)
		OnEnumBtn();

	AddComPorts();

	return TRUE;  // return TRUE  unless you set the focus to a control
}

///////////////////////////////////////////////
void CSgdvcDlg::OnDestroy() 
{
	CDialog::OnDestroy();

	if (m_ImgBuf)
		delete [] m_ImgBuf;

	if (m_DibCtl)
		delete m_DibCtl;

	// Check Thread
	if (g_StopFlag == false) // If thread is working.
	{
		g_StopFlag = true;
		Sleep(500);  // Wait for thread termination
	}

	if (m_Sensor)
	{
		DestroySGFPMObject(m_Sensor);
	}
}

/*
* Add comports
*/
void CSgdvcDlg::AddComPorts()
{
	const int num_ports = 9;	// can have more.

	m_ports.ResetContent();
	for (int i=0; i < num_ports; i++)
	{
		CString str;
		str.Format(_T("COM%d"), i+1);
		m_ports.AddString(str);
	}

	int m_CurPort = 5;	// comport = m_CurPort + 1
	m_ports.SetCurSel(m_CurPort);  
}

/******************************************
Show EnumerateDevice() usage
EnmerateDevice() can be called before Init() function
/******************************************/
void CSgdvcDlg::OnEnumBtn() 
{
	m_ComboDevID.ResetContent();

	if (m_Sensor)
	{
		DWORD dev_nums;
		m_Sensor->EnumerateDevice(&dev_nums, &m_EnumData);

		for (int i = 0; i < (int)dev_nums; i++)
		{
			CString dev_name;
			if (m_EnumData[i].DevName == SG_DEV_FDP02)
				dev_name = "Parallel FDP02";
			else if (m_EnumData[i].DevName == SG_DEV_FDU02)
				dev_name = "USB FDU02";
			else if (m_EnumData[i].DevName == SG_DEV_FDU03)
				dev_name = "USB FDU03";
			else if (m_EnumData[i].DevName == SG_DEV_FDU04)
				dev_name = "USB FDU04";
			else if (m_EnumData[i].DevName == SG_DEV_FDU05)
				dev_name = "USB FDU05 (U20)";
			else if (m_EnumData[i].DevName == SG_DEV_FDU06)
				dev_name = "USB FDU06 (UPx)";
			else if (m_EnumData[i].DevName == SG_DEV_FDU07)
				dev_name = "USB FDU07 (U10)";
			else if (m_EnumData[i].DevName == SG_DEV_FDU07A)
				dev_name = "USB FDU07A (U10A)";
			else if (m_EnumData[i].DevName == SG_DEV_FDU08)
				dev_name = "USB FDU08 (U20A)";
			else if (m_EnumData[i].DevName == SG_DEV_FDU08P)
				dev_name = "USB FDU08 (U20AP)";
			else if (m_EnumData[i].DevName == SG_DEV_FDU08A)
				dev_name = "USB FDU08 (U20APA)";
			else if (m_EnumData[i].DevName == SG_DEV_FDU09)
				dev_name = "USB FDU09 (U30)";
			else if (m_EnumData[i].DevName == SG_DEV_FDU09A)
				dev_name = "USB FDU09 (U30A)";
			else if (m_EnumData[i].DevName == SG_DEV_FDU10A)
				dev_name = "USB FDU10 (Air)";

			CString str;
			str.Format(_T("%s: %d"), dev_name, m_EnumData[i].DevID);
			m_ComboDevID.AddString(str);
		}

		if (m_ComboDevID.GetCount() > 0)
			m_ComboDevID.SetCurSel(0);   
	}

	UpdateData(FALSE);
}


////////////////////////////////////////////////
// Initialize the device
// Show Init(),OpenDevice(), GetDeviceInfo() usage
////////////////////////////////////////////////
void CSgdvcDlg::OnInitbtn() 
{
	Initialize(true);
}

/*
* Initialize the SDA device
*/
void CSgdvcDlg::OnBnClickedButtonSdaInit()
{
	Initialize(false);
}

/*
* Initialize both types of devices: USB and SDA-serial
*/
void CSgdvcDlg::Initialize(bool is_usb)
{
	CWaitCursor hourglass;

	DWORD error;

	if (is_usb)
	{
		m_CurDev  = m_ComboDevID.GetCurSel();

		if (m_CurDev < 0)
		{
			m_ErrorDisplay = TEXT("Initialization Fail - No device selected.");
			UpdateData(FALSE);
			return;
		}
	}
	else
	{
		m_CurPort = m_ports.GetCurSel();
	}

	if ((m_Sensor))
	{
		m_Sensor->CloseDevice();

		if (is_usb)
		{
			error = m_Sensor->Init(m_EnumData[m_CurDev].DevName);
			error = m_Sensor->OpenDevice(m_EnumData[m_CurDev].DevID);
		}
		else
		{
			int port_no = m_CurPort + 1;
			error = m_Sensor->Init(SG_DEV_FDUSDA);
			error = m_Sensor->OpenDevice(port_no);
		}

		if (error != 0)
		{
			m_ErrorDisplay = TEXT("Initialization Fail");
			UpdateData(FALSE);
			return;
		}

		// GetDeviceInfo()
		SGDeviceInfoParam device_info;
		memset(&device_info, 0x00, sizeof(device_info));
		error = m_Sensor->GetDeviceInfo(&device_info);

		if (error == SGFDX_ERROR_NONE)
		{
			m_DevID = device_info.DeviceID; 

			m_DevSN = device_info.DeviceSN; 
			//if (m_DevSN == "")
			//   m_DevSN = "Not Supported"; 
			m_ImgWidth = device_info.ImageWidth;
			m_ImgHeight = device_info.ImageHeight;
			m_Contrast = device_info.Contrast;
			m_Brightness = device_info.Brightness;
			m_Gain = device_info.Gain;
			m_ImageDPI = device_info.ImageDPI;
			char buffer[20];
			_ultoa(device_info.FWVersion, buffer, 16);
			m_FWVersion = CString(buffer);
		}

		if (m_ImgBuf)
			delete[] m_ImgBuf;
		m_ImgBuf = new BYTE[m_ImgWidth*m_ImgHeight];

		if (m_DibCtl)
			delete m_DibCtl;
		m_DibCtl = new CDibClass(m_ImageBox.m_hWnd);
		m_DibCtl->DibInit(m_ImgWidth, m_ImgHeight);

		InitUI(is_usb);
		m_ErrorDisplay = TEXT("Initialization Success");
		UpdateData(FALSE);
	}
}


////////////////////////////////////////////////
// On/Off device LED - SetLedOn()
////////////////////////////////////////////////
void CSgdvcDlg::OnLedonoff() 
{
	if (!m_Sensor)
		return;

	static bool onoff = FALSE;
	onoff = !onoff;
	DWORD error = m_Sensor->SetLedOn(onoff);
	if (error == SGFDX_ERROR_NONE)
		if (onoff)
			m_ErrorDisplay = TEXT("Led on");
		else
			m_ErrorDisplay = TEXT("Led off");
	else
		DisplayErrorMsg(error);

	UpdateData(FALSE);
}


////////////////////////////////////////////////
// Configure Device parameter - Configure()
////////////////////////////////////////////////
void CSgdvcDlg::OnConfig() 
{
	if (!m_Sensor)
		return;

	DWORD error = m_Sensor->Configure(m_hWnd);	
	if (error == SGFDX_ERROR_NONE)
	{
		m_ErrorDisplay = TEXT("Function Success");
		UpdateData(FALSE);
	}
	else
		DisplayErrorMsg(error);

}


////////////////////////////////////////////////
// Capture Fingerprint Image - GetImage()
////////////////////////////////////////////////
void CSgdvcDlg::OnCapture() 
{
	CWaitCursor hourglass;

	if (!m_Sensor)
		return;

	DWORD start_time, end_time;
	DWORD error;

	start_time = GetTickCount();
	error = m_Sensor->GetImage(m_ImgBuf);
	end_time = GetTickCount();

	if (error == SGFDX_ERROR_NONE)
	{
		DisplayImage();
		DisplayTime(end_time - start_time);
	}
	else
		DisplayErrorMsg(error);
}

////////////////////////////////////////////////
// Capture Fingerprint Image - GetLiveImageEx()
////////////////////////////////////////////////
void CSgdvcDlg::OnLivecapture() 
{
	CWaitCursor hourglass;

	if (!m_Sensor)
		return;

	DWORD error;
	DWORD start_time, end_time;

	UpdateData(TRUE);
	start_time = GetTickCount();
	error = m_Sensor->GetImageEx(m_ImgBuf, m_Timeout, (&m_ImageBox)->m_hWnd, m_ImgQuality);
	end_time = GetTickCount();


	if (error == SGFDX_ERROR_NONE)
		DisplayTime(end_time - start_time);
	else
		DisplayErrorMsg(error);
}

////////////////////////////////////////////////
// Capture Fingerprint Image Continuoulsy 
//  - GetLiveImageEx(), SetCallBackFuntion()
////////////////////////////////////////////////
static DWORD WINAPI MyCallbackFunction(LPVOID pParam1, LPVOID pParam2);
static UINT MyCaptureThread(LPVOID pParam);

void CSgdvcDlg::OnContCapture() 
{
	CWaitCursor hourglass;

	if (!m_Sensor)
		return;

	g_StopFlag = !g_StopFlag;

	if (!g_StopFlag)
	{
		g_Stopped = false;

		m_ConCapBtn.SetWindowText("Stop Capturing");

		m_InitBtn.EnableWindow(false);
		m_CaptureBtn.EnableWindow(false);
		m_LiveCaptureBtn.EnableWindow(false);
		m_LedBtn.EnableWindow(false);
		m_ConfigBtn.EnableWindow(false);

		m_Sensor->SetCallBackFunction(CALLBACK_LIVE_CAPTURE, MyCallbackFunction, this);
		AfxBeginThread(MyCaptureThread, this); 
		g_StartTime = GetTickCount();
	}
	else
	{
		if (g_Stopped)
			m_Sensor->SetCallBackFunction(CALLBACK_LIVE_CAPTURE, NULL, this);

		m_ConCapBtn.SetWindowText("Continuous Capture");

		m_InitBtn.EnableWindow(true);
		m_CaptureBtn.EnableWindow(true);
		m_LiveCaptureBtn.EnableWindow(true);
		m_LedBtn.EnableWindow(true);
		m_ConfigBtn.EnableWindow(true);
	}
}

//////////////////////////////////////////////////////////////
DWORD CSgdvcDlg::ContinuousCapture() 
{
	DWORD err = 0;
	if (m_Sensor)
		err = m_Sensor->GetImageEx(m_ImgBuf, 0xFFFF, m_ImageBox.m_hWnd, 100);

	return err;
}


//////////////////////////////////////////////////////////////
// Callback Function Related
// Used in Thread
static DWORD WINAPI MyCallbackFunction(LPVOID pParam1, LPVOID pParam2)
{
	CSgdvcDlg* dlg = (CSgdvcDlg*)pParam1;  // My Data
	SGCBLiveCaptureParam* pCaptureInfo = (SGCBLiveCaptureParam*)pParam2; // Data from driver

	// Calculate Capture Time
	g_EndTime = GetTickCount();
	DWORD cap_time = g_EndTime - g_StartTime;

	// Display Capture Time
	CWnd* timeEdit = dlg->GetDlgItem(IDC_STATIC_STATUS);
	if (timeEdit)
	{
		char temp[100];
		memset(temp, 0x00, 100);
		//wsprintf(temp, "Capture Time: %d", cap_time);
		wsprintf(temp, "Capture Time: %d, Img Quality=%d", cap_time, pCaptureInfo->Quality);
		timeEdit->SetWindowText(temp);
	}

	g_StartTime = g_EndTime;

	if (g_StopFlag)
	{
		g_Stopped = true;
		return SGFDX_ERROR_FUNCTION_FAILED;
	}
	else
		return SGFDX_ERROR_NONE;
}

//////////////////////////////////////////////////////////////
static UINT MyCaptureThread(LPVOID pParam)
{
	CSgdvcDlg* pDlg = (CSgdvcDlg*)pParam;

	while (!g_StopFlag)
	{
		DWORD err = pDlg->ContinuousCapture();
		if (err != SGFDX_ERROR_NONE)
			break;
	}
	return 1;
}


////////////////////////////////////////////////
// Display fingerprint Image
////////////////////////////////////////////////
void CSgdvcDlg::DisplayImage()
{
	if (m_Sensor && m_ImgBuf && m_DibCtl)
	{
		m_DibCtl->SetBits((BYTE*)m_ImgBuf);
		m_DibCtl->DrawDib();
	}
}


////////////////////////////////////////////////
// Display Capture Time
////////////////////////////////////////////////
void CSgdvcDlg::DisplayTime(DWORD time)
{
	char temp[20];
	memset(temp, 0x00, 20);
	wsprintf(temp, "%d", time);
	m_ErrorDisplay = "Capture Time: " + CString(temp);
	UpdateData(FALSE);
}


////////////////////////////////////////////////
// Display Error Message
////////////////////////////////////////////////
void CSgdvcDlg::DisplayErrorMsg(DWORD err_number) 
{
	CString msg;
	switch(err_number)
	{
	case SGFDX_ERROR_NONE:
		msg = "Function Success";      
		break;
	case SGFDX_ERROR_CREATION_FAILED:
		msg = "Failed to ISensor Object"; 
		break;
	case SGFDX_ERROR_FUNCTION_FAILED:
		msg = "Function Failed";   
		break;
	case SGFDX_ERROR_INVALID_PARAM:
		msg = "Invalid Parameter"; 
		break;
	case SGFDX_ERROR_NOT_USED:
		msg = "Not used function"; 
		break;
		//      case SGFDX_ERROR_VXDLOAD_FAILED:
		//         msg = "Failed to load venusdrv.sys";
		//         break;
	case SGFDX_ERROR_INITIALIZE_FAILED:
		msg = "Failed to initialize the device";
		break;
	case SGFDX_ERROR_LINE_DROPPED:
		msg = "Line dropped";
		break;
	case SGFDX_ERROR_TIME_OUT:      
		msg = "Time out";
		break;
	case SGFDX_ERROR_DEVICE_NOT_FOUND:
		msg = "Device not found";
		break;
	case SGFDX_ERROR_DLLLOAD_FAILED:
		msg = "Dll load failed";  // Actually it is not used...
		break;
	case SGFDX_ERROR_WRONG_IMAGE:         
		msg = "Wrong Image";
		break;
	case SGFDX_ERROR_LACK_OF_BANDWIDTH:
		msg = "USB Bandwidth is lack";
		break;
		//      case SGFDX_ERROR_ALREADY_OPEN:
		//         msg = "Device is already opened";
		//         break;
	case SGFDX_ERROR_GETSN_FAILED:
		msg = "Failed to get serial number";
		break;
	}
	TCHAR buf[100];
	wsprintf(buf, "ERROR = %d, %s", err_number, msg);
	m_ErrorDisplay = TEXT(buf);
	UpdateData(FALSE);
}


////////////////////////////////////////////////
// Initialize Dialog User Inferface
////////////////////////////////////////////////
void CSgdvcDlg::InitUI(bool is_usb) 
{
	// Enable all Buttons
	m_CaptureBtn.EnableWindow();
	m_ConCapBtn.EnableWindow();
	m_LiveCaptureBtn.EnableWindow();
	m_LedBtn.EnableWindow();
	m_ConfigBtn.EnableWindow();
	m_CheckFingerLivenessBtn.EnableWindow();
	m_CheckFingerLivenessBtn.SetCheck(true);
	m_AutoOnBtn.EnableWindow(false);

	if (is_usb)
	{
		if (
			(m_EnumData[m_CurDev].DevName == SG_DEV_FDU10A) ||
			(m_EnumData[m_CurDev].DevName == SG_DEV_FDU09A) ||
			(m_EnumData[m_CurDev].DevName == SG_DEV_FDU09)  ||
			(m_EnumData[m_CurDev].DevName == SG_DEV_FDU08A) ||
			(m_EnumData[m_CurDev].DevName == SG_DEV_FDU08P)||
			(m_EnumData[m_CurDev].DevName == SG_DEV_FDU08) ||
			(m_EnumData[m_CurDev].DevName == SG_DEV_FDU07A)||
			(m_EnumData[m_CurDev].DevName == SG_DEV_FDU07) ||
			(m_EnumData[m_CurDev].DevName == SG_DEV_FDU06) ||
			(m_EnumData[m_CurDev].DevName == SG_DEV_FDU05) ||
			(m_EnumData[m_CurDev].DevName == SG_DEV_FDU04) ||
			(m_EnumData[m_CurDev].DevName == SG_DEV_FDU03)
			)
		{
			m_AutoOnBtn.EnableWindow();
		}
	}
	else // SDA-device
	{
		m_AutoOnBtn.EnableWindow();

		// not supported.
		m_CheckFingerLivenessBtn.EnableWindow(FALSE);
		m_LedBtn.EnableWindow(FALSE);
		m_ConfigBtn.EnableWindow(FALSE);
	}
}



//////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////////
// Utility Functions

/******************************************
* Save Fingerprint image to BMP format
/******************************************/
#define WIDTHBYTES(bits)		(((bits)+31)/32*4)
void CSgdvcDlg::OnMenuSaveBmp() 
{
	// TODO: Add your command handler code here
	BYTE *bImage = m_ImgBuf;
	LONG dwWidth = m_ImgWidth;
	LONG dwHeight = m_ImgHeight;   
	LONG dwWidthx4 = WIDTHBYTES(8 * dwWidth);	// 8 bits per pixel

	FILE *fp = fopen("fp_image.bmp", "wb");
	if (fp)
	{
		BITMAPFILEHEADER bmpFileHdr = {0};
		BITMAPINFOHEADER bmpInfoHdr;
		BITMAPINFO *pBmpInfo = (BITMAPINFO*)malloc(sizeof(BITMAPINFOHEADER) + 256 * sizeof(RGBQUAD));

		// initialize bitmap info header
		bmpInfoHdr.biSize = sizeof(BITMAPINFOHEADER);
		bmpInfoHdr.biWidth = dwWidth;
		bmpInfoHdr.biHeight = dwHeight;
		bmpInfoHdr.biPlanes = 1;
		bmpInfoHdr.biBitCount = 8;
		bmpInfoHdr.biCompression = BI_RGB;
		bmpInfoHdr.biSizeImage = dwWidthx4 * dwHeight;
		bmpInfoHdr.biClrUsed = 256;
		bmpInfoHdr.biClrImportant = 256;

		memcpy(pBmpInfo, &bmpInfoHdr, sizeof(BITMAPINFOHEADER));
		int i;
		for (i = 0; i < 256; i++)
		{
			pBmpInfo->bmiColors[i].rgbRed = i;
			pBmpInfo->bmiColors[i].rgbGreen = i;
			pBmpInfo->bmiColors[i].rgbBlue = i;
			pBmpInfo->bmiColors[i].rgbReserved = 0;
		}
		// initialize bitmap file header
		bmpFileHdr.bfType = 0x4d42;  // "BM"
		bmpFileHdr.bfSize = (DWORD)sizeof(BITMAPFILEHEADER) + bmpInfoHdr.biSize +
			bmpInfoHdr.biClrUsed * sizeof(RGBQUAD) + bmpInfoHdr.biSizeImage;
		bmpFileHdr.bfOffBits = (DWORD)sizeof(BITMAPFILEHEADER) + bmpInfoHdr.biSize +
			bmpInfoHdr.biClrUsed * sizeof(RGBQUAD);

		// write to file
		fwrite(&bmpFileHdr, 1, sizeof(BITMAPFILEHEADER), fp);
		fwrite(pBmpInfo, 1, sizeof(BITMAPINFOHEADER) + 256 * sizeof(RGBQUAD), fp);

		// reverse the bImage
		BYTE *bmp = (BYTE*)malloc(dwWidthx4 * dwHeight);
		for (i = 0; i < dwHeight; i++)
			memcpy(&bmp[i*dwWidthx4], &bImage[(dwHeight - 1 - i) * dwWidth], dwWidth);
		fwrite(bmp, 1, bmpInfoHdr.biSizeImage, fp);
		free (bmp);

		free(pBmpInfo);
		fclose(fp);
		m_ErrorDisplay = TEXT("Fingerprint data was saved in fp_image.bmp");
	}
	else
		m_ErrorDisplay= TEXT("Function Failed");
	UpdateData(FALSE);
}


/******************************************
* Save Fingerprint image to RAW format
/******************************************/
void CSgdvcDlg::OnMenuSaveRaw() 
{
	FILE *fp = fopen("fp_image.raw", "wb");
	if (fp)
	{
		fwrite(m_ImgBuf, 1, m_ImgWidth*m_ImgHeight, fp);
		fclose(fp);
		m_ErrorDisplay= TEXT(_T("Fingerprint data was saved in fp_image.raw"));
	}
	else
		m_ErrorDisplay= TEXT("Function Failed");

	UpdateData(FALSE);

}

/******************************************
* Show AboutBox
/******************************************/
void CSgdvcDlg::OnMenuAbout() 
{
	CAboutDlg dlgAbout;
	dlgAbout.DoModal();
}

/******************************************
* Exit
/******************************************/
void CSgdvcDlg::OnMenuExit() 
{
	DestroyWindow();
}


/******************************************
* Auto-on Test
*
*
*
/******************************************/
///////////////////////////////////////////////////////////
//DWORD WINAPI MyAutoOnCallbackFunc(LPVOID pParam1, LPVOID pParam2);
void CSgdvcDlg::OnAutoonbtn() 
{
	DWORD err = m_Sensor->SetAutoOnIRLedTouchOn(1, 1);   

	static bool EnableAutoOn = false;
	EnableAutoOn = !EnableAutoOn;

	if (EnableAutoOn)
	{
		m_Sensor->EnableAutoOnEvent(TRUE, m_hWnd, 0);  
		m_AutoOnBtn.SetWindowText("Disable AutoOn Event");

	}
	else
	{
		m_Sensor->EnableAutoOnEvent(FALSE, 0, 0);  
		m_AutoOnBtn.SetWindowText("Enable AutoOn Event");
	}
}

LRESULT CSgdvcDlg::OnAutoOnEvent(WPARAM wParam, LPARAM lParam)
{
	WORD isfinger = (WORD)wParam;
	SGDeviceInfoParam device_info;
	memcpy(&device_info, (SGDeviceInfoParam*)lParam, sizeof(device_info));

	if(isfinger == SGDEVEVNET_FINGER_ON)
	{
		m_ErrorDisplay.Format(_T("Device Event:Finger ON, DevId:%d, SN:%s"), device_info.DeviceID, device_info.DeviceSN);
		DWORD err = m_Sensor->GetImageEx(m_ImgBuf, 3000, m_ImageBox.m_hWnd, 30);
	}
	else if(isfinger == SGDEVEVNET_FINGER_OFF)
	{
		m_ErrorDisplay = TEXT("Device event: Finger Off");
	}

	UpdateData(FALSE);

	return 1;
}


void CSgdvcDlg::OnBnClickedCheckFingerLiveness()
{
	// TODO: Add your control notification handler code here
	DWORD errorCode = SGFDX_ERROR_NONE;
	int checked = m_CheckFingerLivenessBtn.GetCheck();

	bool enable = (checked) ? true : false;

	if (m_Sensor)
	{
		errorCode = m_Sensor->EnableCheckOfFingerLiveness(enable);
	}
}
