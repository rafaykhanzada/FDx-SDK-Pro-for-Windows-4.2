// matchingUAIRDlg.cpp : implementation file
//

#include "stdafx.h"
#include <fstream>
#include <io.h>
#include <mbstring.h>
#include "matchingUAIR.h"
#include "matchingUAIRDlg.h"

#include "sgfplib.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

#define REG_IMG_QUALITY  70
#define VRF_IMG_QUALITY  50

bool g_CapturedR1, g_CapturedR2, g_CapturedV1;

int SecurityLevel[9]= {SL_LOWEST, SL_LOWER, SL_LOW, SL_BELOW_NORMAL, SL_NORMAL, SL_ABOVE_NORMAL, SL_HIGH, SL_HIGHER, SL_HIGHEST};


/////////////////////////////////////////////////////////////////////////////
// CMatchingUAIRDlg dialog

CMatchingUAIRDlg::CMatchingUAIRDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CMatchingUAIRDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CMatchingUAIRDlg)
	m_ResultEdit = _T("");
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CMatchingUAIRDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CMatchingUAIRDlg)
	DDX_Control(pDX, IDC_REGBUTTON, m_RegisterBtn);
	DDX_Control(pDX, IDC_MATBUTTON, m_VerifyBtn);
	DDX_Control(pDX, IDC_IDENTIFY, m_IdentifyBtn);
	DDX_Control(pDX, IDC_CONFBUTTON, m_ConfigBtn);
	DDX_Control(pDX, IDC_BTN_CAPTURE_V1, m_CapBtnV);
	DDX_Control(pDX, IDC_REGIMAGE, m_RegImageBox1);
	DDX_Control(pDX, IDC_MATIMAGE, m_VrfImageBox);
	DDX_Control(pDX, IDC_COMBO_DEVICE, m_CBDevName);
	DDX_Control(pDX, IDC_COMBO1, m_SecureLevel);
	DDX_Text(pDX, IDC_EDIT3, m_ResultEdit);
	//}}AFX_DATA_MAP
	DDX_Control(pDX, IDC_REG_THUMBNAIL_1, m_RegThumbnail1);
	DDX_Control(pDX, IDC_REG_THUMBNAIL_2, m_RegThumbnail2);
	DDX_Control(pDX, IDC_BTN_CAPTURE_REG, m_CapBtnReg);
	DDX_Control(pDX, IDC_BTN_CLEAR_REG, m_ClearBtnReg);
	DDX_Control(pDX, IDC_REG_THUMBNAIL_3, m_RegThumbnail3);
	DDX_Control(pDX, IDC_REG_THUMBNAIL_4, m_RegThumbnail4);
	DDX_Control(pDX, IDC_REG_THUMBNAIL_5, m_RegThumbnail5);
}

BEGIN_MESSAGE_MAP(CMatchingUAIRDlg, CDialog)
	//{{AFX_MSG_MAP(CMatchingUAIRDlg)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_WM_CREATE()
	ON_WM_DESTROY()
	ON_BN_CLICKED(IDC_INITBUTTON, OnInitbutton)
	ON_BN_CLICKED(IDC_CONFBUTTON, OnConfbutton)
	ON_BN_CLICKED(IDC_REGBUTTON, OnRegbutton)
	ON_BN_CLICKED(IDC_MATBUTTON, OnMatbutton)
	ON_BN_CLICKED(IDC_IDENTIFY, OnIdentify)
	ON_BN_CLICKED(IDC_BTN_CAPTURE_V1, OnBtnCaptureV1)
	//}}AFX_MSG_MAP
	ON_BN_CLICKED(IDC_BTN_CAPTURE_REG, &CMatchingUAIRDlg::OnBtnCaptureReg)
	ON_BN_CLICKED(IDC_BTN_CLEAR_REG, &CMatchingUAIRDlg::OnBtnClearReg)
	ON_COMMAND(IDOK, &CMatchingUAIRDlg::OnIdok)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CMatchingUAIRDlg message handlers

//////////////////////////////////////////////////////////
BOOL CMatchingUAIRDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	// TODO: Add extra initialization here

	m_DibCtl = NULL;
	m_ImgBuf = NULL;
	m_FetBuf1 = NULL;
	m_FetBuf2 = NULL;
	m_FetBufM = NULL;

	m_ImageWidth = 0;
	m_ImageHeight = 0;
	m_MaxTemplateSize = 0;

	g_CapturedR1 = g_CapturedR2 = g_CapturedV1 = false;

	m_CBDevName.AddString("USB FDU10A(U-AIR)");
	m_CBDevName.AddString("USB FDU09(U30A)");
	m_CBDevName.AddString("USB FDU09(U30)");
	m_CBDevName.AddString("USB FDU08(U20APA)");
	m_CBDevName.AddString("USB FDU08(U20AP)");
	m_CBDevName.AddString("USB FDU08(U20A)");
	m_CBDevName.AddString("USB FDU07A(U10A)");
	m_CBDevName.AddString("USB FDU07(U10)");
	m_CBDevName.AddString("USB FDU06(UPx)");
	m_CBDevName.AddString("USB FDU05(U20)");
	m_CBDevName.AddString("USB FDU04");
	m_CBDevName.AddString("USB FDU03");
	m_CBDevName.AddString("USB FDU02");
	m_CBDevName.AddString("Parallel FDP02");

	HWND hwnd = ::GetDlgItem(m_hWnd, IDC_EDIT_USER);
	::SetWindowText(hwnd, TEXT("Anonymous"));

	m_CBDevName.SetCurSel(0);	// USB U-AIR
	m_SecureLevel.SetCurSel(4); // NORMAL
	SetTemplateFormat(0);		// SG400

	// thumbnails
	m_RegThumbnails.push_back(&m_RegThumbnail1);
	m_RegThumbnails.push_back(&m_RegThumbnail2);
	m_RegThumbnails.push_back(&m_RegThumbnail3);
	m_RegThumbnails.push_back(&m_RegThumbnail4);
	m_RegThumbnails.push_back(&m_RegThumbnail5);

	// temporary feature buffer
	m_FetBuf = make_unique<BYTE[]>(SIZE_OF_FEATURE_BUF);
	
	return TRUE;  // return TRUE  unless you set the focus to a control
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CMatchingUAIRDlg::OnPaint() 
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
		CDialog::OnPaint();
	}
}

// The system calls this to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CMatchingUAIRDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

////////////////////////////////////////////////////////////
int CMatchingUAIRDlg::OnCreate(LPCREATESTRUCT lpCreateStruct) 
{
	if (CDialog::OnCreate(lpCreateStruct) == -1)
		return -1;

	m_hFPM = NULL;
	DWORD err = SGFPM_Create(&m_hFPM);
	return 0;
}

////////////////////////////////////////////////////////////
void CMatchingUAIRDlg::OnDestroy() 
{
	CDialog::OnDestroy();

	if (m_hFPM)
	{
		SGFPM_Terminate(m_hFPM);
	}
	m_hFPM = 0;

	if (m_DibCtl)
		delete m_DibCtl;
	m_DibCtl = NULL;

	if (m_ImgBuf)
		delete[] m_ImgBuf;
	m_ImgBuf = NULL;

	if (m_FetBuf1)
		delete [] m_FetBuf1;
	m_FetBuf1 = NULL;

	if (m_FetBuf2)
		delete [] m_FetBuf2;
	m_FetBuf2 = NULL;

	if (m_FetBufM)
		delete [] m_FetBufM;
	m_FetBufM = NULL;

}

////////////////////////////////////////////////////////////
void CMatchingUAIRDlg::OnInitbutton() 
{
	DWORD err;
	DWORD devname = SG_DEV_FDU03;

	// 255 for Auto detect.
	// call to SGFPM_EnumerateDevice() to find device ID.
	// U20-AP and U20-A share device ID, which means that IDs will increase by one for both devices.
	DWORD devid = 255;

	CString devstr;
	m_CBDevName.GetWindowText(devstr);

	if (devstr == "USB FDU10A(U-AIR)")			// USB: FDU10A(U-AIR)
		devname = SG_DEV_FDU10A;
	else if (devstr == "USB FDU09(U30A)")		// USB: FDU09(U30A)
		devname = SG_DEV_FDU09A;
	else if (devstr == "USB FDU09(U30)")		// USB: FDU09(U30)
		devname = SG_DEV_FDU09;
	else if (devstr == "USB FDU08(U20APA)")		// USB: FDU08P(U20APA)
		devname = SG_DEV_FDU08A;
	else if (devstr == "USB FDU08(U20AP)")		// USB: FDU08P(U20AP)
		devname = SG_DEV_FDU08P;
	else if (devstr == "USB FDU08(U20A)")		// USB: FDU08(U20A)
		devname = SG_DEV_FDU08;
	else if (devstr == "USB FDU07A(U10A)")		// USB: FDU07A(U10A)
		devname = SG_DEV_FDU07A;
	else if (devstr == "USB FDU07(U10)")		// USB: FDU07(U10)
		devname = SG_DEV_FDU07;
	else if (devstr == "USB FDU06(UPx)")		// USB: FDU06(UPx)
		devname = SG_DEV_FDU06;
	else if (devstr == "USB FDU05(U20)")		// USB: FDU05
		devname = SG_DEV_FDU05;
	else if (devstr == "USB FDU04")				// USB: FDU04
		devname = SG_DEV_FDU04;
	else if (devstr == "USB FDU03")				// USB: FDU03
		devname = SG_DEV_FDU03;
	else if (devstr == "USB FDU02")				// USB: FDU02 
		devname = SG_DEV_FDU02;
	else if (devstr == "Parallel FDP02")		// USB: FDP02 
		devname = SG_DEV_FDP02;
	else
		devname = SG_DEV_UNKNOWN;

	err = SGFPM_Init(m_hFPM, devname); 

	if (err != SGFDX_ERROR_NONE)
	{
		m_ResultEdit.Format("Initialization Failed. Error = %d", err);
		UpdateData(false);   
		return;
	}

	err = SGFPM_OpenDevice(m_hFPM, devid);

	if (err != SGFDX_ERROR_NONE)
	{
		m_ResultEdit.Format("OpenDevice() failed. Error = %d", err);
		UpdateData(false);   
		return;
	}

	SGDeviceInfoParam device_info;
	err = SGFPM_GetDeviceInfo(m_hFPM, &device_info);

	if (err != SGFDX_ERROR_NONE)
	{
		m_ResultEdit.Format("GetDeviceInfo() failed. Error = %d", err);
		UpdateData(false);   
		return;
	}

	m_ImageWidth = device_info.ImageWidth;
	m_ImageHeight = device_info.ImageHeight;

	if (m_DibCtl == NULL)
		m_DibCtl = new CDibClass(m_hWnd);

	m_DibCtl->DibInit(m_ImageWidth, m_ImageHeight);

	if (m_ImgBuf)
		delete [] m_ImgBuf;
	m_ImgBuf = new BYTE[m_ImageWidth*m_ImageHeight];

	// Initialize minutiae buffer
	err = SGFPM_GetMaxTemplateSize(m_hFPM, &m_MaxTemplateSize);
	if (err != SGFDX_ERROR_NONE)
	{
		m_ResultEdit.Format("GetMaxTemplateSize() failed. Error = %d", err);
		UpdateData(false);   
	}

	if (m_FetBuf1)
		delete [] m_FetBuf1;
	m_FetBuf1 = new BYTE[m_MaxTemplateSize];

	if (m_FetBuf2)
		delete [] m_FetBuf2;
	m_FetBuf2 = new BYTE[m_MaxTemplateSize];

	if (m_FetBufM)
		delete [] m_FetBufM;
	m_FetBufM = new BYTE[m_MaxTemplateSize];

	m_ConfigBtn.EnableWindow();
	m_ConfigBtn.EnableWindow();
	m_CapBtnReg.EnableWindow();
	m_CapBtnV.EnableWindow();

	m_ClearBtnReg.EnableWindow();

	// clear thumnails
	OnBtnClearReg();

	m_DevName = devname;
	
	m_ResultEdit.Format("Initialization Success");
	UpdateData(false);
}

////////////////////////////////////////////////////////////
// Change Template format
// TEMPLATE_FORMAT_ANSI378 = 0x0100,  TEMPLATE_FORMAT_SG400   = 0x0200,
void CMatchingUAIRDlg::SetTemplateFormat(int templateSelected) 
{
	WORD template_format;

	if (templateSelected == 1)
		template_format = TEMPLATE_FORMAT_ANSI378;
	else if (templateSelected == 2)
		template_format = TEMPLATE_FORMAT_ISO19794;
	else if (templateSelected == 3)
		template_format = TEMPLATE_FORMAT_ISO19794_COMPACT;
	else // if (templateSelected == 0)
		template_format = TEMPLATE_FORMAT_SG400;

	DWORD err = SGFPM_SetTemplateFormat(m_hFPM, template_format);
	if (err == SGFDX_ERROR_NONE)
		m_ResultEdit.Format("SetTemplateFormat() Success");
	else
		m_ResultEdit.Format("SetTemplateFormat() failed. Error = %d", err);

	UpdateData(false);   
}

////////////////////////////////////////////////////////////
void CMatchingUAIRDlg::OnConfbutton() 
{
	DWORD err = SGFPM_Configure(m_hFPM, m_hWnd);	
	if (err == SGFDX_ERROR_NONE)
		m_ResultEdit.Format("Configure() Success");
	else
		m_ResultEdit.Format("Configure() failed. Error = %d", err);

	UpdateData(false);   
}

////////////////////////////////////////////////////////////
void CMatchingUAIRDlg::OnBtnClearReg()
{
	m_FetBufRegs.clear();

	memset(m_ImgBuf, 0xff, m_ImageHeight*m_ImageWidth);
	for (auto regThumbnail : m_RegThumbnails) {
		DisplayImage(regThumbnail, m_ImgBuf);
	}
	DisplayImage(&m_RegImageBox1, m_ImgBuf);
}

////////////////////////////////////////////////////////////
void CMatchingUAIRDlg::OnBtnCaptureReg()
{
	if ((int)m_FetBufRegs.size() < NUM_OF_REGS) {
		CaptureRegs();
	}
}

////////////////////////////////////////////////////////////
void CMatchingUAIRDlg::CaptureRegs()
{
	if (!m_DibCtl)
		return;

	bool bSmartCapture = true;
	DWORD err = SGFPM_EnableSmartCapture(m_hFPM, bSmartCapture); // optional

	DWORD dwStart = GetTickCount();

	// Capture fingerprint
	err = SGFPM_GetImageEx(m_hFPM, m_ImgBuf, 5000, m_RegImageBox1.m_hWnd/*NULL*/, REG_IMG_QUALITY);
	if (err == SGFDX_ERROR_NONE) {
		DWORD img_qlty;
		DisplayImage(&m_RegImageBox1, m_ImgBuf);

		if (m_DevName == SG_DEV_FDU10A)
			err = SGFPM_GetLastImageQuality(m_hFPM, &img_qlty); // U-Air only, fast but gets higher scores than those of GetImageQuality()
		else
			err = SGFPM_GetImageQuality(m_hFPM, m_ImageWidth, m_ImageHeight, m_ImgBuf, &img_qlty);
		
		// Extract Fingerprint
		err = SGFPM_CreateTemplate(m_hFPM, 0, m_ImgBuf, m_FetBuf.get());
		if (err == SGFDX_ERROR_NONE) {
			BOOL matched = false;
			DWORD match_score = 0;
			int numOfRegs = (int)m_FetBufRegs.size();
			if (numOfRegs == 0) {
				matched = true;		// the first template
				match_score = 199;	// max score
			} else {
				matched = MatchRegs(m_FetBuf.get(), match_score);
			}
			
			if (matched) {
				DisplayImage(m_RegThumbnails[numOfRegs], m_ImgBuf);
				
				auto newFetBuf = make_unique<BYTE[]>(SIZE_OF_FEATURE_BUF);
				memcpy_s(newFetBuf.get(), SIZE_OF_FEATURE_BUF, m_FetBuf.get(), SIZE_OF_FEATURE_BUF);
				m_FetBufRegs.push_back(move(newFetBuf));

				m_ResultEdit.Format("The template(%d/%d) is created and added for registration. Image Quality=%d, Time= %d ms, Match Score=%d", 
					numOfRegs + 1, NUM_OF_REGS, img_qlty, GetTickCount() - dwStart, match_score);
			} else {
				m_ResultEdit.Format("Try again. Not matched against previous ones, Image Quality=%d, Time= %d ms", img_qlty, GetTickCount() - dwStart);
			}
		} else {
			m_ResultEdit.Format("CreateTemplate() failed. Error = %d", err);
		}
	} else {
		m_ResultEdit.Format("The captured image is not good. Try again...");
	}

	// To keep the SmartCapture active, comment the following statements.
	bSmartCapture = false;
	err = SGFPM_EnableSmartCapture(m_hFPM, bSmartCapture);

	if ((int)m_FetBufRegs.size() == NUM_OF_REGS) {
		m_RegisterBtn.EnableWindow();
	}

	UpdateData(false);
}

////////////////////////////////////////////////////////////
BOOL CMatchingUAIRDlg::MatchRegs(BYTE* fetBuf, DWORD& match_score)
{
	BOOL matched = false;
	DWORD err = SGFDX_ERROR_NONE;
	for (auto& fetReg : m_FetBufRegs) {
		err = SGFPM_MatchTemplate(m_hFPM, fetBuf, fetReg.get(), SecurityLevel[m_SecureLevel.GetCurSel()], &matched);
		if (matched) {		
			err = SGFPM_GetMatchingScore(m_hFPM, fetBuf, fetReg.get(), &match_score);
			break;
		}
	}
	return matched;
}

////////////////////////////////////////////////////////////
BOOL  CMatchingUAIRDlg::MatchTemplates(BYTE* fetBuf, vector<unique_ptr<BYTE[]>> &templates, int numOfMinMatched)
{
	int numOfMatched = 0;
	DWORD err = SGFDX_ERROR_NONE;

	numOfMinMatched = max(numOfMinMatched, MIN_NUM_OF_MATCHED); // MIN_NUM_OF_MATCHED(2) or higher recommended

	for (auto& fetReg : templates) {
		BOOL matched = false;

		err = SGFPM_MatchTemplate(m_hFPM, fetReg.get(), fetBuf, SecurityLevel[m_SecureLevel.GetCurSel()], &matched);
		if (err != SGFDX_ERROR_NONE) {
			m_ResultEdit.Format("MatchTemplate() failed. Error = %d ", err); // just for error message
		} else {
			if (matched) {
				numOfMatched++;

				if (numOfMatched >= numOfMinMatched)
					return true;
			}
		}
	}
	return false;
}

////////////////////////////////////////////////////////////
void CMatchingUAIRDlg::OnBtnCaptureR1() 
{
	if (!m_DibCtl)
		return;

	bool bSmartCapture = true;
	DWORD err = SGFPM_EnableSmartCapture(m_hFPM, bSmartCapture); // optional

	DWORD dwStart = GetTickCount();
	// Capture fingerprint
	err = SGFPM_GetImageEx(m_hFPM, m_ImgBuf, 5000, m_RegImageBox1.m_hWnd/*NULL*/, REG_IMG_QUALITY);
	if (err == SGFDX_ERROR_NONE) {
		DWORD img_qlty;		
		DisplayImage(&m_RegImageBox1, m_ImgBuf);
		DisplayImage(&m_RegThumbnail1, m_ImgBuf);

		err = SGFPM_GetImageQuality(m_hFPM, m_ImageWidth, m_ImageHeight, m_ImgBuf, &img_qlty);

		// Extract Fingerprint
		err = SGFPM_CreateTemplate(m_hFPM, 0, m_ImgBuf, m_FetBuf1);
		if (err == SGFDX_ERROR_NONE) {
			g_CapturedR1 = true;
			m_ResultEdit.Format("The first template is created. Image Quality=%d, Time= %d ms", img_qlty, GetTickCount()-dwStart);
		} else {
			m_ResultEdit.Format("CreateTemplate() failed. Error = %d", err);
		}
	} else {
		m_ResultEdit.Format("The captured image is not good. Try again...");
	}

	// To keep the SmartCapture active, comment the following statements.
	bSmartCapture = false;
	err = SGFPM_EnableSmartCapture(m_hFPM, bSmartCapture);

	if (g_CapturedR1 && g_CapturedR2) {
		m_RegisterBtn.EnableWindow();
	}

	UpdateData(false);   
}


////////////////////////////////////////////////////////////
void CMatchingUAIRDlg::OnBtnCaptureR2() 
{
	if (!m_DibCtl)
		return;

	// Capture fingerprint
	DWORD img_qlty;
	bool bSmartCapture = true;
	DWORD err = SGFPM_EnableSmartCapture(m_hFPM, bSmartCapture); // optional

	err = SGFPM_GetImageEx(m_hFPM, m_ImgBuf, 5000, m_RegImageBox2.m_hWnd/*NULL*/, REG_IMG_QUALITY);

	if (err == SGFDX_ERROR_NONE) {
		DisplayImage(&m_RegImageBox2, m_ImgBuf);
		err = SGFPM_GetImageQuality(m_hFPM, m_ImageWidth, m_ImageHeight, m_ImgBuf, &img_qlty);

		// Extract Fingerprint.
		err = SGFPM_CreateTemplate(m_hFPM, 0, m_ImgBuf, m_FetBuf2);

		if (err == SGFDX_ERROR_NONE) {
			m_ResultEdit.Format("The second template is created. Image Quality=%d", img_qlty);
			g_CapturedR2 = true;
		} else {
			m_ResultEdit.Format("CreateTemplate() failed. Error = %d", err);
		}
	} else {
		m_ResultEdit.Format("The captured image is not good. Try again...");
	}

	// To keep the SmartCapture active, comment the following statements.
	bSmartCapture = false;
	err = SGFPM_EnableSmartCapture(m_hFPM, bSmartCapture);

	if (g_CapturedR1 && g_CapturedR2) {
		m_RegisterBtn.EnableWindow();
	}

	UpdateData(false);   
}

////////////////////////////////////////////////////////////
void CMatchingUAIRDlg::OnBtnCaptureV1() 
{
	if (!m_DibCtl)
		return;

	const DWORD timeout = 5000; // 5 seconds
	const DWORD min_quality_verify = 50;
	DWORD img_qlty = 0;
	bool template_created = false;
	DWORD dwStart = GetTickCount();

	bool bSmartCapture = true;
	DWORD err = SGFPM_EnableSmartCapture(m_hFPM, bSmartCapture); // optional

	// Calling BeginGetImage() will make GetImage() faster.
	err = SGFPM_BeginGetImage(m_hFPM);
	do {
		err = SGFPM_GetImage(m_hFPM, m_ImgBuf);
		if (err == SGFDX_ERROR_NONE) {
			DisplayImage(&m_VrfImageBox, m_ImgBuf);

			if (m_DevName == SG_DEV_FDU10A)
				err = SGFPM_GetLastImageQuality(m_hFPM, &img_qlty); // U-Air only, fast but gets higher scores than those of GetImageQuality()
			else
				err = SGFPM_GetImageQuality(m_hFPM, m_ImageWidth, m_ImageHeight, m_ImgBuf, &img_qlty);

			if (err != SGFDX_ERROR_NONE) {
				m_ResultEdit.Format("Error on GetImageQuality=%d", err);
			} else {
				m_ResultEdit.Format("Image Quality=%d", img_qlty);
				if (img_qlty >= min_quality_verify) {
					// Extract Fingerprint.
					err = SGFPM_CreateTemplate(m_hFPM, 0, m_ImgBuf, m_FetBufM);
					if (err == SGFDX_ERROR_NONE) {
						g_CapturedV1 = true;
						template_created = true;
						m_ResultEdit.Format("The template for verification and identification is created. Image Quality=%d", img_qlty);
					} else {
						m_ResultEdit.Format("CreateTemplate() failed. Error = %d", err);
					}
				}
			}
		} else {
			m_ResultEdit.Format("The captured image is not good(%d). Capturing...", err);
		}

		UpdateStatusbar();

	} while (!template_created && (GetTickCount() - dwStart <= timeout));

	err = SGFPM_EndGetImage(m_hFPM);

	if (!template_created) {
		m_ResultEdit.Format("The time has expired. Try again...");
		UpdateStatusbar();
	}

	if (IsAllRegsCaptured() && g_CapturedV1) {
		m_VerifyBtn.EnableWindow();
		m_IdentifyBtn.EnableWindow();
	}
	UpdateData(false);   	
}

////////////////////////////////////////////////////////////
BOOL CMatchingUAIRDlg::IsAllRegsCaptured()
{
	return m_FetBufRegs.size() == NUM_OF_REGS;
}

////////////////////////////////////////////////////////////
void CMatchingUAIRDlg::OnRegbutton()
{
	if (m_FetBufRegs.size() == NUM_OF_REGS) {
		HWND hwnd = ::GetDlgItem(m_hWnd, IDC_EDIT_USER);
		char file_name[MAX_PATH];
		::GetWindowText(hwnd, file_name, MAX_PATH);
		strcat(file_name, ".min");
		SaveMinFile(file_name, m_FetBufRegs);
		m_ResultEdit.Format("Registration Success");
	} else {
		m_ResultEdit.Format("Cannot register. Only %d / %d captured.", m_FetBufRegs.size(), NUM_OF_REGS);
	}
	UpdateData(false);
}

////////////////////////////////////////////////////////////
void CMatchingUAIRDlg::OnRegbuttonObsolete()
{
	if ((m_FetBuf1 == NULL) || (m_FetBuf2 == NULL))
		return;

	BOOL matched = false;   
	DWORD err = SGFPM_MatchTemplate(m_hFPM, m_FetBuf1, m_FetBuf2, SecurityLevel[m_SecureLevel.GetCurSel()], &matched);

	if ((err == SGFDX_ERROR_NONE) && matched)
	{
		HWND hwnd = ::GetDlgItem(m_hWnd, IDC_EDIT_USER);
		char file_name[MAX_PATH];
		::GetWindowText(hwnd, file_name, MAX_PATH);
		strcat(file_name, ".min");
		SaveMinFile(file_name, m_FetBuf1, m_FetBuf2);

		DWORD match_score;
		err = SGFPM_GetMatchingScore(m_hFPM, m_FetBuf1, m_FetBuf2, &match_score);
		m_ResultEdit.Format("Registration Success : Matching Score = %d", match_score);
	}
	else
		m_ResultEdit = _T("Registration Fail");

	UpdateData(false);   
}

////////////////////////////////////////////////////////////
void CMatchingUAIRDlg::OnMatbutton()
{
	BOOL matched = MatchTemplates(m_FetBufM, m_FetBufRegs, MIN_NUM_OF_MATCHED);

	if (matched) 
		m_ResultEdit.Format("The templates are matched");
	else
		m_ResultEdit.Format("The templates are not matched");

	UpdateData(false);
}

////////////////////////////////////////////////////////////
void CMatchingUAIRDlg::OnMatbuttonObsolete()
{
	if ((m_FetBuf1 == NULL) || (m_FetBuf2 == NULL) || (m_FetBufM == NULL))
		return;

	DWORD err;
	BOOL matched1, matched2;   
	err = SGFPM_MatchTemplate(m_hFPM, m_FetBuf1, m_FetBufM, SecurityLevel[m_SecureLevel.GetCurSel()], &matched1);
	err = SGFPM_MatchTemplate(m_hFPM, m_FetBuf2, m_FetBufM, SecurityLevel[m_SecureLevel.GetCurSel()], &matched2);

	if (err == SGFDX_ERROR_NONE)
	{
		if (matched1 && matched2)
			m_ResultEdit.Format("The templates are matched");
		else
			m_ResultEdit.Format("The templates are not matched");
	}
	else
	{
		m_ResultEdit.Format("MatchTemplate() failed. Error = %d ", err);
	}
	
	UpdateData(false);
}

////////////////////////////////////////////////////////////
void CMatchingUAIRDlg::OnIdentify()
{
	BYTE* searchMin = m_FetBufM;
	char userName[MAX_PATH];

	ifstream ifs;
	vector<unique_ptr<BYTE[]>> fetBufs;

	char min_file[MAX_PATH];
	DWORD persons = 0;
	struct _finddata_t  ffblk2;
	const int num_data = 2;
	struct _finddata_t  ffblk;
	intptr_t hfile;
	int i;
	BYTE *user_arr;

	hfile = _findfirst("*.min", &ffblk);
	if (hfile < 0) {
		m_ResultEdit = _T("_findfirst() error");
		UpdateData(false);
		return;
	}

	while (hfile) {
		persons++;
		if (_findnext(hfile, &ffblk) != 0)
			break;
	}

	user_arr = new BYTE[MAX_PATH];

	hfile = _findfirst("*.min", &ffblk2);
	if (!hfile) {
		delete[] user_arr;
		m_ResultEdit = _T("_findfirst() error");
		UpdateData(false);
		return;
	}

	for (i = 0; i < (int)persons; i++) {
		memset(min_file, 0, MAX_PATH);
		strcpy(min_file, ffblk2.name);

		ifs.open(min_file, ios::binary);
		if (!ifs) {
			delete[] user_arr;
			m_ResultEdit = _T("fopen() error");
			UpdateData(false);
			return;
		}

		memset(user_arr, 0x00, MAX_PATH);
		_mbsnbcpy(user_arr, (const unsigned char*)ffblk2.name, strlen(ffblk2.name) - 4);

		// read all features
		while (ifs) {
			auto newFetBuf = make_unique<BYTE[]>(SIZE_OF_FEATURE_BUF);

			ifs.read((char*)newFetBuf.get(), SIZE_OF_FEATURE_BUF);
			if (ifs) {
				fetBufs.push_back(move(newFetBuf));
			}
		}	
		ifs.close();

		BOOL matched = MatchTemplates(searchMin, fetBufs, MIN_NUM_OF_MATCHED);

		if (matched) {
			strcpy(userName, (const char*)(user_arr));
			delete[] user_arr;

			m_ResultEdit.Format(_T("You are %s"), userName);
			UpdateData(false);
			return;
		}
		
		fetBufs.clear();
		if (_findnext(hfile, &ffblk2) != 0)
			break;
	}

	delete[] user_arr;

	m_ResultEdit = _T("Cannot find your fingerprints");
	UpdateData(false);
}

////////////////////////////////////////////////////////////
void CMatchingUAIRDlg::OnIdentifyObsolete() 
{
	BYTE* searchMin = m_FetBufM;
	char userName[MAX_PATH];

	FILE *stream;
	char min_file[MAX_PATH];
	DWORD persons = 0;
	struct _finddata_t  ffblk2;
	const int num_data = 2;
	struct _finddata_t  ffblk;
	intptr_t hfile;
	int i;
	BYTE* min_arr, *user_arr;

	hfile = _findfirst("*.min", &ffblk);
	if (hfile < 0)
	{
		m_ResultEdit = _T("_findfirst() error");
		UpdateData(false);
		return;
	}

	while (hfile)
	{
		persons++;
		if (_findnext(hfile, &ffblk) != 0)
			break;
	}

	min_arr = new BYTE[800];
	if (NULL == min_arr)
	{
		m_ResultEdit = _T("Memory allocation error");
		UpdateData(false);
		return;
	}

	user_arr = new BYTE[MAX_PATH];

	hfile = _findfirst("*.min", &ffblk2);
	if (!hfile)
	{
		delete[] user_arr;
		delete[] min_arr;
		m_ResultEdit = _T("_findfirst() error");
		UpdateData(false);
		return;
	}

	for (i = 0; i < (int)persons; i++)
	{
		memset(min_file, 0, MAX_PATH);
		strcpy(min_file, ffblk2.name);
		if ((stream  = fopen(min_file, "rb")) == NULL)
		{
			delete[] user_arr;
			delete[] min_arr;
			m_ResultEdit = _T("fopen() error");
			UpdateData(false);
			return;
		}

		memset(user_arr, 0x00, MAX_PATH);
		_mbsnbcpy(user_arr, (const unsigned char*)ffblk2.name, strlen(ffblk2.name)-4);

		fread(min_arr, 800, 1, stream);

		DWORD err;
		BOOL matched1, matched2;
		
		err = SGFPM_MatchTemplate(m_hFPM, min_arr, searchMin, SecurityLevel[m_SecureLevel.GetCurSel()], &matched1);
		err = SGFPM_MatchTemplate(m_hFPM, &min_arr[400], searchMin, SecurityLevel[m_SecureLevel.GetCurSel()], &matched2);

		if ((err == SGFDX_ERROR_NONE) && matched1 && matched2)
		{
			strcpy(userName, (const char*)(user_arr));

			delete[] user_arr;
			delete[] min_arr;

			m_ResultEdit.Format(_T("You are %s"), userName);
			UpdateData(false);
			return;

		}

		fclose(stream);
		if (_findnext(hfile, &ffblk2)!= 0)
			break;
	}

	delete[] user_arr;
	delete[] min_arr;

	m_ResultEdit = _T("Cannot find your fingerprints");
	UpdateData(false);
}


// misc functions....
// Display fingerprint image
void CMatchingUAIRDlg::DisplayImage(CStatic *img, BYTE *img_buf)
{
	RECT pr;		
	RECT r;
	RECT rect;

	GetClientRect(&pr);
	ClientToScreen(&pr);

	img->GetClientRect(&r);
	rect = r;
	img->ClientToScreen(&r);

	int diff_x = r.left - pr.left;
	int diff_y = r.top - pr.top;

	rect.left = diff_x + 1;
	rect.top = diff_y + 1;
	rect.right = diff_x + rect.right - 2;
	rect.bottom = diff_y + rect.bottom - 2;      

	//m_DibCtl->SetBits((BYTE*)img_buf, m_ImageWidth*m_ImageHeight);
	m_DibCtl->SetBits((BYTE*)img_buf);
	m_DibCtl->DrawDib(&rect);
}

// Save minutiae file
void CMatchingUAIRDlg::SaveMinFile(char* fname, BYTE* min1, BYTE* min2)
{
	FILE* fp;
	if ((fp = fopen(LPCTSTR(fname), "wb")) == NULL) 
		return;
	fwrite(min1, 400, 1, fp);
	fwrite(min2, 400, 1, fp);
	fclose(fp);
}

void CMatchingUAIRDlg::SaveMinFile(string fname, vector<unique_ptr<BYTE[]>> &mins)
{
	ofstream ofs(fname, ios::binary);
	if (ofs.is_open()) {
		for (auto& min : mins) {
			ofs.write((char*)min.get(), SIZE_OF_FEATURE_BUF);
		}
		ofs.close();
	}
}

void CMatchingUAIRDlg::UpdateStatusbar()
{
	UpdateData(false);
	CWnd* statusbar = GetDlgItem(IDC_EDIT3);
	statusbar->UpdateWindow();
}

void CMatchingUAIRDlg::OnIdok()
{
	// Prevent from existing the app when Enter key is pressed in the User Edit control
}
