// sgdvcDlg.h : header file
//
#include "stdafx.h"
#include "sgfplib.h"
#include "dibclass.h"
#include "afxwin.h"


#if !defined(AFX_SGDVCDLG_H__C12C7C9F_9FBF_4E83_8C19_7EC6175F48FA__INCLUDED_)
#define AFX_SGDVCDLG_H__C12C7C9F_9FBF_4E83_8C19_7EC6175F48FA__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CSgdvcDlg dialog
class CDibClass;
class CSgdvcDlg : public CDialog
{
	// Construction
public:
	CSgdvcDlg(CWnd* pParent = NULL);	// standard constructor

	DWORD       ContinuousCapture(); 
	void        DisplayTime(DWORD time);
	SGFPM*      m_Sensor;
	BYTE*       m_ImgBuf;        // image buffer

	// Dialog Data
	//{{AFX_DATA(CSgdvcDlg)
	enum { IDD = IDD_SGDVC_DIALOG };
	CButton	m_EventBtn;
	CButton	m_AutoOnBtn;
	CComboBox	m_ComboDevID;
	CButton	m_ConCapBtn;
	CButton	m_LiveCaptureBtn;
	CButton	m_CaptureBtn;
	CButton	m_ConfigBtn;
	CButton	m_LedBtn;
	CButton	m_InitBtn;
	CStatic	m_ImageBox;
	CString	m_ErrorDisplay;
	UINT	m_ImgHeight;
	UINT	m_ImgQuality;
	UINT	m_ImgWidth;
	CString	m_DevName;
	CString	m_DevSN;
	UINT	m_DevID;
	int		m_Timeout;
	UINT	m_FWVer;
	UINT	m_Brightness;
	UINT	m_Contrast;
	UINT	m_Gain;
	CString	m_ModelName;
	CString	m_FWVersion;
	UINT	m_ImageDPI;
	CString	m_CaptureTime;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CSgdvcDlg)
protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL


	// Implementation
private:
	CDibClass*  m_DibCtl;  // for drawing fp image on first window
	HINSTANCE   m_hLib;
	SGDeviceList* m_EnumData; 
	int     m_CurDev;
	int		m_CurPort;

	void    DisplayErrorMsg(DWORD err_number);  
	void    DisplayImage();
	void    InitUI(bool is_usb);
	void	Initialize(bool is_usb);
	void	AddComPorts();
protected:
	HICON m_hIcon;

	// Generated message map functions
	//{{AFX_MSG(CSgdvcDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnDestroy();
	afx_msg void OnInitbtn();
	afx_msg void OnLedonoff();
	afx_msg void OnConfig();
	afx_msg void OnCapture();
	afx_msg void OnLivecapture();
	afx_msg void OnEnumBtn();
	afx_msg void OnMenuExit();
	afx_msg void OnMenuSaveBmp();
	afx_msg void OnMenuSaveRaw();
	afx_msg void OnMenuAbout();
	afx_msg void OnContCapture();
	afx_msg void OnAutoonbtn();
	afx_msg LRESULT OnAutoOnEvent(WPARAM wParam, LPARAM lParam);
	//afx_msg void OnBtnTest();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedCheckFingerLiveness();
private:
	CButton m_CheckFingerLivenessBtn;
public:
	CComboBox m_ports;
	afx_msg void OnBnClickedButtonSdaInit();
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SGDVCDLG_H__C12C7C9F_9FBF_4E83_8C19_7EC6175F48FA__INCLUDED_)
