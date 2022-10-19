// matchminDlg.cpp : implementation file
//

#include "stdafx.h"
#include <io.h>
#include <mbstring.h>
#include "matchingIso.h"
#include "matchingIsoDlg.h"
#include "sgfplib.h" // library header file

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

#define REG_IMG_QUALITY  70
#define VRF_IMG_QUALITY  50

bool g_CapturedR1, g_CapturedR2, g_CapturedV1;
CString g_FingerPosStr[11]= { "Unknown finger",
                              "Right thumb",
                              "Right index finger",
                              "Right middle finger",
                              "Right ring finger",
                              "Right little finger",
                              "Left thumb",
                              "Left index finger",
                              "Left middle finger",
                              "Left ring finger",
                              "Left little finger"};

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
	DDX_Control(pDX, IDC_COMBO1, m_SelectedFinger);
	DDX_Control(pDX, IDC_BTN_CHANGE_FORMAT, m_FormatBtn);
	DDX_Control(pDX, IDC_REGBUTTON, m_RegisterBtn);
	DDX_Control(pDX, IDC_MATBUTTON, m_VerifyBtn);
	DDX_Control(pDX, IDC_BTN_CAPTURE_R1, m_CapBtnR1);
	DDX_Control(pDX, IDC_BTN_CAPTURE_R2, m_CapBtnR2);
	DDX_Control(pDX, IDC_BTN_CAPTURE_V1, m_CapBtnV);
	DDX_Control(pDX, IDC_REGIMAGE2, m_RegImageBox2);
	DDX_Control(pDX, IDC_REGIMAGE, m_RegImageBox1);
	DDX_Control(pDX, IDC_MATIMAGE, m_VrfImageBox);
	DDX_Control(pDX, IDC_COMBO_DEVICE, m_CBDevName);
	DDX_Control(pDX, IDC_EDIT2, m_NameEdit);
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
	ON_BN_CLICKED(IDC_REGBUTTON, OnRegbutton)
	ON_BN_CLICKED(IDC_MATBUTTON, OnMatbutton)
	ON_BN_CLICKED(IDC_BTN_CAPTURE_R1, OnBtnCaptureR1)
	ON_BN_CLICKED(IDC_BTN_CAPTURE_R2, OnBtnCaptureR2)
	ON_BN_CLICKED(IDC_BTN_CAPTURE_V1, OnBtnCaptureV1)
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
   m_StoredTemplate = NULL;

   m_MaxTemplateSize = 0;
   m_ImageWidth = 260;
	m_ImageHeight = 300;
   
   m_DeviceOpened = false;
   g_CapturedR1 = g_CapturedR2 = g_CapturedV1 = false;

   m_CBDevName.AddString("Any Device");
   m_CBDevName.AddString("USB FDU10A(U-Air)");
   m_CBDevName.AddString("USB FDU09(U30A)");
   m_CBDevName.AddString("USB FDU09(U30)");
   m_CBDevName.AddString("USB FDU08(U20APA)");
   m_CBDevName.AddString("USB FDU08(U20AP)");
   m_CBDevName.AddString("USB FDU08(U20A)");
   m_CBDevName.AddString("USB FDU07A(U10A)");
   m_CBDevName.AddString("USB FDU07(U10)");
   m_CBDevName.AddString("USB FDU05(U20)");
   m_CBDevName.AddString("USB FDU04");
   m_CBDevName.AddString("USB FDU03");
   m_CBDevName.AddString("USB FDU02");
   m_CBDevName.AddString("Parallel FDP02");
   m_CBDevName.AddString("No device");

   m_CBDevName.SetCurSel(0); // Any Device
   m_SelectedFinger.SetCurSel(0); // Unknown

   m_NameEdit.SetWindowText("test");

   m_BtnEnrolledFinger[0] = (CButton*)GetDlgItem(IDC_RADIO_FINGERID00);
   m_BtnEnrolledFinger[1] = (CButton*)GetDlgItem(IDC_RADIO_FINGERID01);
   m_BtnEnrolledFinger[2] = (CButton*)GetDlgItem(IDC_RADIO_FINGERID02);
   m_BtnEnrolledFinger[3] = (CButton*)GetDlgItem(IDC_RADIO_FINGERID03);
   m_BtnEnrolledFinger[4] = (CButton*)GetDlgItem(IDC_RADIO_FINGERID04);
   m_BtnEnrolledFinger[5] = (CButton*)GetDlgItem(IDC_RADIO_FINGERID05);
   m_BtnEnrolledFinger[6] = (CButton*)GetDlgItem(IDC_RADIO_FINGERID06);
   m_BtnEnrolledFinger[7] = (CButton*)GetDlgItem(IDC_RADIO_FINGERID07);
   m_BtnEnrolledFinger[8] = (CButton*)GetDlgItem(IDC_RADIO_FINGERID08);
   m_BtnEnrolledFinger[9] = (CButton*)GetDlgItem(IDC_RADIO_FINGERID09);
   m_BtnEnrolledFinger[10] = (CButton*)GetDlgItem(IDC_RADIO_FINGERID10);

   for (int i=0; i < 11; i++ )
   {
      m_BtnEnrolledFinger[i]->EnableWindow(false);
      m_BtnEnrolledFinger[i]->SetCheck(false);
   }

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

   if (m_StoredTemplate)
      delete [] m_StoredTemplate;
   m_StoredTemplate = NULL;
}

////////////////////////////////////////////////////////////
void CMatchminDlg::OnInitbutton() 
{
	DWORD err;
   DWORD devname = SG_DEV_FDU05;

   m_DeviceOpened = false;

   CString devstr;
   m_CBDevName.GetWindowText(devstr);

   if (devstr == "Any Device")				// Any device detected
		devname = SG_DEV_AUTO;
   else if (devstr == "USB FDU10A(U-Air)")	// USB: FDU10A(U-AIR)
		devname = SG_DEV_FDU10A;
   else if (devstr == "USB FDU09(U30A)")	// USB: FDU09A
		devname = SG_DEV_FDU09A;
   else if (devstr == "USB FDU09(U30)")		// USB: FDU09
		devname = SG_DEV_FDU09;
   else if (devstr == "USB FDU08(U20APA)")	// USB: FDU08A
		devname = SG_DEV_FDU08A;
   else if (devstr == "USB FDU08(U20AP)")	// USB: FDU08P
		devname = SG_DEV_FDU08P;
   else if (devstr == "USB FDU08(U20A)")	// USB: FDU08
		devname = SG_DEV_FDU08;
   else if (devstr == "USB FDU07A(U10A)")	// USB: FDU07A
		devname = SG_DEV_FDU07A;
   else if (devstr == "USB FDU07(U10)")	// USB: FDU07
		devname = SG_DEV_FDU07;
   else if (devstr == "USB FDU05(U20)")	// USB: FDU05
		devname = SG_DEV_FDU05;
   else if (devstr == "USB FDU04")	// USB: FDU04
		devname = SG_DEV_FDU04;
   else if (devstr == "USB FDU03")	// USB: FDU03
		devname = SG_DEV_FDU03;
   else if (devstr == "USB FDU02")	// USB: FDU02 
		devname = SG_DEV_FDU02; 
   else if (devstr == "Parallel FDP02") // USB: FDP02 
		devname = SG_DEV_FDP02;
   else
      devname = SG_DEV_UNKNOWN;

   // Initialize library
   if (devname != SG_DEV_UNKNOWN)
      err = SGFPM_Init(m_hFPM, devname); 
   else
      err = SGFPM_InitEx(m_hFPM, m_ImageWidth, m_ImageHeight, 500); 

   if (err != SGFDX_ERROR_NONE)
   {
      m_ResultEdit.Format("Library/Driver initialization failed. Error = %d", err);
      UpdateData(false);   
      return;
   }
      
   // Get Algorithm version to display

	// Compare to codes for ANSI 
	/* 
   DWORD extractor, matcher;
   err = SGFPM_GetMinexVersion(m_hFPM, &extractor, &matcher); 

   CString sz_ver;
   sz_ver.Format("(Extractor:0x%08X, Matcher:0x%08X)", extractor, matcher); 
   SetWindowText(_T("SecuGen ANSI MINEX Test ") + sz_ver);

   // Set template format to ANSI 378
   err = SGFPM_SetTemplateFormat(m_hFPM, TEMPLATE_FORMAT_ANSI378);
	//*/

   // Set template format to ISO 19794
   err = SGFPM_SetTemplateFormat(m_hFPM, TEMPLATE_FORMAT_ISO19794);

   // OpenDevice
   if (devname != SG_DEV_UNKNOWN)
   {
      DWORD devid = USB_AUTO_DETECT;
      err = SGFPM_OpenDevice(m_hFPM, devid);
      if (err == SGFDX_ERROR_NONE)
      {
         SGDeviceInfoParam device_info;
	      SGFPM_GetDeviceInfo(m_hFPM, &device_info);

         m_ImageWidth = device_info.ImageWidth;
	      m_ImageHeight = device_info.ImageHeight;
         m_DeviceOpened = true;
      }
      else
      {
         m_ResultEdit.Format("OpenDevice() failed. Error = %d", err);
         UpdateData(false);   
         return;
      }
   }

   // Initialize Variable
	if (m_DibCtl)
      delete m_DibCtl;
   m_DibCtl = new CDibClass(m_hWnd);
   m_DibCtl->DibInit(m_ImageWidth, m_ImageHeight);

   if (m_ImgBuf)
      delete [] m_ImgBuf;
   m_ImgBuf = new BYTE[m_ImageWidth*m_ImageHeight];

   err = SGFPM_GetMaxTemplateSize(m_hFPM, &m_MaxTemplateSize);
   if (m_FetBuf1)
      delete [] m_FetBuf1;
   m_FetBuf1 = new BYTE[m_MaxTemplateSize];

   if (m_FetBuf2)
      delete [] m_FetBuf2;
   m_FetBuf2 = new BYTE[m_MaxTemplateSize];

   if (m_FetBufM)
      delete [] m_FetBufM;
   m_FetBufM = new BYTE[m_MaxTemplateSize];
         

   m_CapBtnR1.EnableWindow();
   m_CapBtnR2.EnableWindow();
   m_CapBtnV.EnableWindow();

   m_ResultEdit.Format("Initialization Success");
   UpdateData(false);
}


////////////////////////////////////////////////////////////
void CMatchminDlg::OnBtnCaptureR1() 
{
	if (!m_DibCtl)
		return;

	// Capture fingerprint
   DWORD err;
   if (m_DeviceOpened)
      err = SGFPM_GetImage(m_hFPM, m_ImgBuf);
   else
      err = GetImageFromFile(m_ImgBuf);

   // Create Template
   if (err == SGFDX_ERROR_NONE)
	{
      DWORD qlty;
      DisplayImage(&m_RegImageBox1, m_ImgBuf);
      err = SGFPM_GetImageQuality(m_hFPM, m_ImageWidth, m_ImageHeight, m_ImgBuf, &qlty);
		
      // Create ISO Template from image
      SGFingerInfo finger_info;
      finger_info.FingerNumber = m_SelectedFinger.GetCurSel();
      finger_info.ImageQuality = (WORD)qlty;
      finger_info.ImpressionType = SG_IMPTYPE_LP;
      finger_info.ViewNumber = 1;

  		err = SGFPM_CreateTemplate(m_hFPM, &finger_info, m_ImgBuf, m_FetBuf1);
      if (err == SGFDX_ERROR_NONE)
      {
         g_CapturedR1 = true;
         m_ResultEdit.Format("The first template for registration is created. Image Quality=%d", qlty);
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

   DWORD err;
   if (m_DeviceOpened)
      err = SGFPM_GetImage(m_hFPM, m_ImgBuf);
   else
      err = GetImageFromFile(m_ImgBuf);

   if (err == SGFDX_ERROR_NONE)
	{
      DWORD qlty;
      DisplayImage(&m_RegImageBox2, m_ImgBuf);
      err = SGFPM_GetImageQuality(m_hFPM, m_ImageWidth, m_ImageHeight, m_ImgBuf, &qlty);

      // Create Fingerprint Template
      SGFingerInfo finger_info;
      finger_info.FingerNumber = m_SelectedFinger.GetCurSel();
      finger_info.ImageQuality = (WORD)qlty;
      finger_info.ImpressionType = SG_IMPTYPE_LP;
      finger_info.ViewNumber = 2;
		
      // Extract Fingerprint
  		err = SGFPM_CreateTemplate(m_hFPM, &finger_info, m_ImgBuf, m_FetBuf2);

      if (err == SGFDX_ERROR_NONE)
      {
         g_CapturedR2 = true;
         m_ResultEdit.Format("The second template for registration is created. Image Quality=%d", qlty);
      }
      else
      {
         m_ResultEdit.Format("CreateTemplate() failed. Error = %d", err);
      }
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

   DWORD err;
   if (m_DeviceOpened)
      err = SGFPM_GetImage(m_hFPM, m_ImgBuf);
   else
      err = GetImageFromFile(m_ImgBuf);

   if (err == SGFDX_ERROR_NONE)
	{
      DWORD qlty;
      DisplayImage(&m_VrfImageBox, m_ImgBuf);
      err = SGFPM_GetImageQuality(m_hFPM, m_ImageWidth, m_ImageHeight, m_ImgBuf, &qlty);
		
      // Create Fingerprint Template from image
      SGFingerInfo finger_info;
      finger_info.FingerNumber = 0;
      finger_info.ImageQuality = (WORD)qlty;
      finger_info.ImpressionType = SG_IMPTYPE_LP;
      finger_info.ViewNumber = 1;

  		err = SGFPM_CreateTemplate(m_hFPM, 0, m_ImgBuf, m_FetBufM);
      if (err == SGFDX_ERROR_NONE)
      {
         g_CapturedV1 = true;
         m_ResultEdit.Format("The template for verification is created. Image Quality=%d", qlty);
      }
      else
         m_ResultEdit.Format("CreateTemplate() failed. Error = %d", err);

	}
   else
      m_ResultEdit.Format("The captured image is not good. Try again...");


   if (g_CapturedR1 && g_CapturedR2 && g_CapturedV1)
   {
      m_VerifyBtn.EnableWindow();
   }

   UpdateData(false);   	
}


////////////////////////////////////////////////////////////
void CMatchminDlg::OnRegbutton() 
{
   if ((m_FetBuf1 == NULL) || (m_FetBuf2 == NULL))
      return;
   
   BOOL matched = FALSE;   
   DWORD err = 0;
   err = SGFPM_MatchTemplate(m_hFPM, m_FetBuf1, m_FetBuf2, SL_NORMAL, &matched);


   if ((err == SGFDX_ERROR_NONE) && matched)
   {
      DWORD match_score;
      err = SGFPM_GetMatchingScore(m_hFPM, m_FetBuf1, m_FetBuf2, &match_score);
      m_ResultEdit.Format("Registration Success : Matching Score = %d", match_score);

      // Save template after merging two template - m_FetBuf1, m_FetBuf2
      BYTE*  merged_template; 
      DWORD  buf_size;  
		
      // ANSI 378
      /*
		err = SGFPM_GetTemplateSizeAfterMerge(m_hFPM, m_FetBuf1, m_FetBuf2, &buf_size);
      merged_template = new BYTE[buf_size];
      err = SGFPM_MergeAnsiTemplate(m_hFPM, m_FetBuf1, m_FetBuf2, merged_template);
		*/

		// ISO 19794
		err = SGFPM_GetIsoTemplateSizeAfterMerge(m_hFPM, m_FetBuf1, m_FetBuf2, &buf_size);
      merged_template = new BYTE[buf_size];
		err = SGFPM_MergeIsoTemplate(m_hFPM, m_FetBuf1, m_FetBuf2, merged_template);
      
      if (err != 0)
      {
         m_ResultEdit.Format("Template Merge failed");
         UpdateData(false);
         return;
      }

      if (!m_StoredTemplate)
      {
         m_StoredTemplate = new BYTE[buf_size];
         memcpy(m_StoredTemplate, merged_template, buf_size);
      }
      else        
      {
         BYTE*  new_enroll_template;    

			// ANSI 378
			/*
         err = SGFPM_GetTemplateSizeAfterMerge(m_hFPM, merged_template, m_StoredTemplate, &buf_size);
         new_enroll_template = new BYTE[buf_size];
         err = SGFPM_MergeAnsiTemplate(m_hFPM, merged_template, m_StoredTemplate, new_enroll_template);
			*/

			// ISO 19494
         err = SGFPM_GetIsoTemplateSizeAfterMerge(m_hFPM, merged_template, m_StoredTemplate, &buf_size);
         new_enroll_template = new BYTE[buf_size];
         err = SGFPM_MergeIsoTemplate(m_hFPM, merged_template, m_StoredTemplate, new_enroll_template);
         
         delete m_StoredTemplate;
         m_StoredTemplate = new BYTE[buf_size];
         
         memcpy(m_StoredTemplate, new_enroll_template, buf_size);

         delete [] new_enroll_template;  // freed by calling application
      }
      delete [] merged_template; // freed by calling application
      
      // Display ISO template information         
      int i;
      for (i = 0; i < 11; i++)
         m_BtnEnrolledFinger[i]->SetCheck(false);

		// ANSI 378
		/*
      SGANSITemplateInfo sample_info;
      err = SGFPM_GetAnsiTemplateInfo(m_hFPM, m_StoredTemplate, &sample_info); 
		*/

		// ISO 19794
		SGISOTemplateInfo sample_info;
      err = SGFPM_GetIsoTemplateInfo(m_hFPM, m_StoredTemplate, &sample_info); 
      for (i = 0; i < (int)sample_info.TotalSamples; i++)
      {
         m_BtnEnrolledFinger[sample_info.SampleInfo[i].FingerNumber]->SetCheck(true);
      }

      // Save m_StoredTemplate to file
      HWND hwnd = ::GetDlgItem(m_hWnd, IDC_EDIT_USER);
      char file_name[MAX_PATH];
      ::GetWindowText(hwnd, file_name, MAX_PATH);
      strcat(file_name, ".iso");
      SaveTemplate(file_name, m_StoredTemplate, buf_size);
   }
   else
      m_ResultEdit = _T("Registration Fail");
         
   UpdateData(false);   
}
////////////////////////////////////////////////////////////
void CMatchminDlg::OnMatbutton() 
{
   if ((m_FetBuf1 == NULL) || (m_FetBuf2 == NULL) || (m_FetBufM == NULL) || (m_StoredTemplate == NULL))
      return;
  
   DWORD err;
   BOOL matched = FALSE;
	// ANSI 378
	/*
   SGANSITemplateInfo sample_info;
   err = SGFPM_GetAnsiTemplateInfo(m_hFPM, m_StoredTemplate, &sample_info); 
	*/

	// ISO 19794
	SGISOTemplateInfo sample_info = {0};
   err = SGFPM_GetIsoTemplateInfo(m_hFPM, m_StoredTemplate, &sample_info); 

   matched = FALSE;
   int found_finger = -1;
   for (int i = 0; i < (int)sample_info.TotalSamples; i++)
   {
		// ANSI 378
      // err = SGFPM_MatchAnsiTemplate(m_hFPM, m_StoredTemplate, i, m_FetBufM, 0, SL_NORMAL, &matched);

		// ISO 19794
		err = SGFPM_MatchIsoTemplate(m_hFPM, m_StoredTemplate, i, m_FetBufM, 0, SL_NORMAL, &matched);
      if (matched)
      {
         found_finger = sample_info.SampleInfo[i].FingerNumber;
         break;
      }
   }

   if (err == SGFDX_ERROR_NONE)
   {
      if (found_finger >= 0)
     	   m_ResultEdit.Format("The fingerprint data found. Finger Position: %s", g_FingerPosStr[found_finger]);
      else
         m_ResultEdit.Format("Cannot find matched fingerprint data");
   }
   else
   {
	   m_ResultEdit.Format("MatchIsoTemplate() failed. Error = %d ", err);
   }

   UpdateData(false);
}


//////////////////////////////////
DWORD CMatchminDlg::GetImageFromFile(BYTE* imgBuf)
{
   char img_file[128];
   
   CFileDialog dlgFile(TRUE, "Image RAW file", "*.raw");
   dlgFile.m_ofn.lpstrInitialDir = ".";
   if (dlgFile.DoModal() == IDOK )
      strcpy(img_file, (LPCSTR)dlgFile.GetPathName());
   else
      return 1; // error;

   // Read file
   FILE* fp;
   int fsize;
   if ((fp = fopen(img_file, "rb")) == NULL) 
   {
      ::MessageBox(0, "Can not read file", "Error", MB_OK);
      return 1; // error
   }
   fseek(fp, 0, SEEK_END);
   fsize = ftell(fp);

   fseek(fp, 0, SEEK_SET);
   fread(imgBuf, fsize, 1, fp);
   fclose(fp);

   return 0;
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

	m_DibCtl->SetBits((BYTE*)img_buf);
	m_DibCtl->DrawDib(&rect);
}


// Save template
bool CMatchminDlg::SaveTemplate(char* fname, BYTE* data, DWORD size)
{
   if (size > 0)
   {
      FILE* fp;
      if ((fp = fopen(LPCTSTR(fname), "wb")) == NULL) 
         return false;
      fwrite(data, size, 1, fp);
      fclose(fp);
      return true;
   }
   return false;
}


