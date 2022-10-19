// matchminDlg.cpp : implementation file
//

#include "stdafx.h"
#include <io.h>
#include <mbstring.h>
#include "matching.h"
#include "matchingDlg.h"

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
// CMatchminDlg dialog

CMatchminDlg::CMatchminDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CMatchminDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CMatchminDlg)
	m_ResultEdit = _T("");
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CMatchminDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CMatchminDlg)
	DDX_Control(pDX, IDC_BTN_CHANGE_FORMAT, m_FormatBtn);
	DDX_Control(pDX, IDC_COMBO_FORMAT, m_TemplateFormat);
	DDX_Control(pDX, IDC_REGBUTTON, m_RegisterBtn);
	DDX_Control(pDX, IDC_MATBUTTON, m_VerifyBtn);
	DDX_Control(pDX, IDC_IDENTIFY, m_IdentifyBtn);
	DDX_Control(pDX, IDC_CONFBUTTON, m_ConfigBtn);
	DDX_Control(pDX, IDC_BTN_CAPTURE_R1, m_CapBtnR1);
	DDX_Control(pDX, IDC_BTN_CAPTURE_R2, m_CapBtnR2);
	DDX_Control(pDX, IDC_BTN_CAPTURE_V1, m_CapBtnV);
	DDX_Control(pDX, IDC_REGIMAGE2, m_RegImageBox2);
	DDX_Control(pDX, IDC_REGIMAGE, m_RegImageBox1);
	DDX_Control(pDX, IDC_MATIMAGE, m_VrfImageBox);
	DDX_Control(pDX, IDC_COMBO_DEVICE, m_CBDevName);
	DDX_Control(pDX, IDC_EDIT2, m_NameEdit);
	DDX_Control(pDX, IDC_COMBO1, m_SecureLevel);
	DDX_Text(pDX, IDC_EDIT3, m_ResultEdit);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CMatchminDlg, CDialog)
	//{{AFX_MSG_MAP(CMatchminDlg)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_WM_CREATE()
	ON_WM_DESTROY()
	ON_BN_CLICKED(IDC_INITBUTTON, OnInitbutton)
	ON_BN_CLICKED(IDC_CONFBUTTON, OnConfbutton)
	ON_BN_CLICKED(IDC_REGBUTTON, OnRegbutton)
	ON_BN_CLICKED(IDC_MATBUTTON, OnMatbutton)
	ON_BN_CLICKED(IDC_IDENTIFY, OnIdentify)
	ON_BN_CLICKED(IDC_BTN_CAPTURE_R1, OnBtnCaptureR1)
	ON_BN_CLICKED(IDC_BTN_CAPTURE_R2, OnBtnCaptureR2)
	ON_BN_CLICKED(IDC_BTN_CAPTURE_V1, OnBtnCaptureV1)
	ON_BN_CLICKED(IDC_BTN_CHANGE_FORMAT, OnBtnChangeFormat)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CMatchminDlg message handlers

//////////////////////////////////////////////////////////
BOOL CMatchminDlg::OnInitDialog()
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

   m_CBDevName.AddString("USB FDU10A(U-Air)");
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

   m_CBDevName.SetCurSel(0); // USB FDU05 (FDU02 Plus)
   m_TemplateFormat.SetCurSel(0); // SG400 
   m_SecureLevel.SetCurSel(4); // NORMAL

   m_NameEdit.SetWindowText("test");

	return TRUE;  // return TRUE  unless you set the focus to a control
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CMatchminDlg::OnPaint() 
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
HCURSOR CMatchminDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

////////////////////////////////////////////////////////////
int CMatchminDlg::OnCreate(LPCREATESTRUCT lpCreateStruct) 
{
	if (CDialog::OnCreate(lpCreateStruct) == -1)
		return -1;

   m_hFPM = NULL;
   DWORD err = SGFPM_Create(&m_hFPM);
   return 0;
}

////////////////////////////////////////////////////////////
void CMatchminDlg::OnDestroy() 
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
   m_DibCtl = NULL;

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
void CMatchminDlg::OnInitbutton() 
{
	DWORD err;
   DWORD devname = SG_DEV_FDU03;

   // 255 for Auto detect.
   // call to SGFPM_EnumerateDevice() to find device ID.
   // U20-AP and U20-A share device ID, which means that IDs will increase by one for both devices.
   DWORD devid = 255;

   CString devstr;
   m_CBDevName.GetWindowText(devstr);

	if (devstr == "USB FDU10A(U-Air)")			// USB: FDU10A(U-AIR)
		devname = SG_DEV_FDU10A;
	else if (devstr == "USB FDU09(U30A)")			// USB: FDU09(U30A)
		devname = SG_DEV_FDU09A;
   else if (devstr == "USB FDU09(U30)")		// USB: FDU09(U30)
		devname = SG_DEV_FDU09;
   else if (devstr == "USB FDU08(U20APA)")	// USB: FDU08P(U20APA)
	   devname = SG_DEV_FDU08A;
   else if (devstr == "USB FDU08(U20AP)")	// USB: FDU08P(U20AP)
		devname = SG_DEV_FDU08P;
   else if (devstr == "USB FDU08(U20A)")	// USB: FDU08(U20A)
		devname = SG_DEV_FDU08;
   else if (devstr == "USB FDU07A(U10A)")	// USB: FDU07A(U10A)
		devname = SG_DEV_FDU07A;
   else if (devstr == "USB FDU07(U10)")	// USB: FDU07(U10)
		devname = SG_DEV_FDU07;
   else if (devstr == "USB FDU06(UPx)")	// USB: FDU06(UPx)
		devname = SG_DEV_FDU06;
   else if (devstr == "USB FDU05(U20)") // USB: FDU05
		devname = SG_DEV_FDU05;
   else if (devstr == "USB FDU04") // USB: FDU04
		devname = SG_DEV_FDU04;
   else if (devstr == "USB FDU03") // USB: FDU03
		devname = SG_DEV_FDU03;
   else if (devstr == "USB FDU02") // USB: FDU02 
		devname = SG_DEV_FDU02; 
   else if (devstr == "Parallel FDP02") // USB: FDP02 
		devname = SG_DEV_FDP02; 

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

   m_FormatBtn.EnableWindow();
   m_ConfigBtn.EnableWindow();
   m_ConfigBtn.EnableWindow();
   m_CapBtnR1.EnableWindow();
   m_CapBtnR2.EnableWindow();
   m_CapBtnV.EnableWindow();

   m_ResultEdit.Format("Initialization Success");
   UpdateData(false);
}


////////////////////////////////////////////////////////////
// Change Template format
// TEMPLATE_FORMAT_ANSI378 = 0x0100,  TEMPLATE_FORMAT_SG400   = 0x0200,
void CMatchminDlg::OnBtnChangeFormat() 
{
   WORD template_format;
   int templateSelected = m_TemplateFormat.GetCurSel();

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
void CMatchminDlg::OnConfbutton() 
{
   DWORD err = SGFPM_Configure(m_hFPM, m_hWnd);	
   if (err == SGFDX_ERROR_NONE)
      m_ResultEdit.Format("Configure() Success");
   else
      m_ResultEdit.Format("Configure() failed. Error = %d", err);

   UpdateData(false);   
}


////////////////////////////////////////////////////////////
void CMatchminDlg::OnBtnCaptureR1() 
{
	if (!m_DibCtl)
		return;

	// Capture fingerprint
   DWORD err = SGFPM_GetImageEx(m_hFPM, m_ImgBuf, 5000, NULL, REG_IMG_QUALITY);
   if (err == SGFDX_ERROR_NONE)
	{
      DWORD img_qlty;		
      DisplayImage(&m_RegImageBox1, m_ImgBuf);
      err = SGFPM_GetImageQuality(m_hFPM, m_ImageWidth, m_ImageHeight, m_ImgBuf, &img_qlty);
		
      // Extract Fingerprint
  		err = SGFPM_CreateTemplate(m_hFPM, 0, m_ImgBuf, m_FetBuf1);
      if (err == SGFDX_ERROR_NONE)
      {
         g_CapturedR1 = true;
         m_ResultEdit.Format("The first template is created. Image Quality=%d", img_qlty);
      }
      else
         m_ResultEdit.Format("CreateTemplate() failed. Error = %d", err);
	}
   else
      m_ResultEdit.Format("The captured image is not good. Try again...");

   if (g_CapturedR1 && g_CapturedR2)
   {
      m_RegisterBtn.EnableWindow();
   }

   UpdateData(false);   
}


////////////////////////////////////////////////////////////
void CMatchminDlg::OnBtnCaptureR2() 
{
	if (!m_DibCtl)
		return;

	// Capture fingerprint
   DWORD img_qlty;
   DWORD err = SGFPM_GetImageEx(m_hFPM, m_ImgBuf, 5000, NULL, REG_IMG_QUALITY);
  
   if (err == SGFDX_ERROR_NONE)
	{
      DisplayImage(&m_RegImageBox2, m_ImgBuf);
      err = SGFPM_GetImageQuality(m_hFPM, m_ImageWidth, m_ImageHeight, m_ImgBuf, &img_qlty);
		
      // Extract Fingerprint.
  		err = SGFPM_CreateTemplate(m_hFPM, 0, m_ImgBuf, m_FetBuf2);

      if (err == SGFDX_ERROR_NONE)
      {
         m_ResultEdit.Format("The second template is created. Image Quality=%d", img_qlty);
         g_CapturedR2 = true;
      }
      else
         m_ResultEdit.Format("CreateTemplate() failed. Error = %d", err);
	}
   else
      m_ResultEdit.Format("The captured image is not good. Try again...");

   if (g_CapturedR1 && g_CapturedR2)
   {
      m_RegisterBtn.EnableWindow();
   }

   UpdateData(false);   
}

////////////////////////////////////////////////////////////
void CMatchminDlg::OnBtnCaptureV1() 
{
	if (!m_DibCtl)
		return;

	// Capture fingerprint
   DWORD img_qlty;
   DWORD err = SGFPM_GetImageEx(m_hFPM, m_ImgBuf, 5000, NULL, VRF_IMG_QUALITY);
   if (err == SGFDX_ERROR_NONE)
	{
		DisplayImage(&m_VrfImageBox, m_ImgBuf);
      err = SGFPM_GetImageQuality(m_hFPM,m_ImageWidth, m_ImageHeight, m_ImgBuf, &img_qlty);
		
      // Extract Fingerprint.
  		err = SGFPM_CreateTemplate(m_hFPM, 0, m_ImgBuf, m_FetBufM);
      if (err == SGFDX_ERROR_NONE)
      {
         g_CapturedV1 = true;
         m_ResultEdit.Format("The template for verification and identification. Image Quality=%d", img_qlty);
      }
      else
         m_ResultEdit.Format("CreateTemplate() failed. Error = %d", err);

	}
   else
      m_ResultEdit.Format("The captured image is not good. Try again...");


   if (g_CapturedR1 && g_CapturedR2 && g_CapturedV1)
   {
      m_VerifyBtn.EnableWindow();
      m_IdentifyBtn.EnableWindow();
   }

    UpdateData(false);   	
}


////////////////////////////////////////////////////////////
void CMatchminDlg::OnRegbutton() 
{
   if ((m_FetBuf1 == NULL) || (m_FetBuf2 == NULL))
      return;

   BOOL matched;   
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
void CMatchminDlg::OnMatbutton() 
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
void CMatchminDlg::OnIdentify() 
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
      //m_SecLevel = 
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
void CMatchminDlg::DisplayImage(CStatic *img, BYTE *img_buf)
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
void CMatchminDlg::SaveMinFile(char* fname, BYTE* min1, BYTE* min2)
{
   FILE* fp;
   if ((fp = fopen(LPCTSTR(fname), "wb")) == NULL) 
      return;
   fwrite(min1, 400, 1, fp);
   fwrite(min2, 400, 1, fp);
   fclose(fp);
}


