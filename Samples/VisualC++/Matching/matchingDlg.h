// matchminDlg.h : header file
//
#include "dibclass.h"
#include "..\..\..\inc\sgfplib.h"

#if !defined(AFX_MATCHMINDLG_H__2CD9D5A7_65C2_11D2_8B52_00104B673D08__INCLUDED_)
#define AFX_MATCHMINDLG_H__2CD9D5A7_65C2_11D2_8B52_00104B673D08__INCLUDED_

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000

/////////////////////////////////////////////////////////////////////////////
// CMatchminDlg dialog

class CMatchminDlg : public CDialog
{
// Construction
public:
	DWORD m_DeviceType;
	CMatchminDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CMatchminDlg)
	enum { IDD = IDD_MATCHMIN_DIALOG };
	CButton	m_FormatBtn;
	CComboBox	m_TemplateFormat;
	CButton	m_RegisterBtn;
	CButton	m_VerifyBtn;
	CButton	m_IdentifyBtn;
	CButton	m_ConfigBtn;
	CButton	m_CapBtnR1;
	CButton	m_CapBtnR2;
	CButton	m_CapBtnV;
	CStatic	m_RegImageBox2;
	CStatic	m_RegImageBox1;
	CStatic	m_VrfImageBox;
	CComboBox	m_CBDevName;
	CProgressCtrl	m_VrfImgQaulity;
	CProgressCtrl	m_RegImgQuality;
	CEdit	m_NameEdit;
	CComboBox	m_SecureLevel;
	CString	m_ResultEdit;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CMatchminDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON m_hIcon;

private:
   HSGFPM   m_hFPM;

   CDibClass* m_DibCtl;
   BYTE*    m_ImgBuf;
   BYTE*     m_FetBuf1;
   BYTE*     m_FetBuf2;
   BYTE*     m_FetBufM;
   DWORD     m_MaxTemplateSize;

   DWORD    m_ImageWidth, m_ImageHeight;
private:
   void     DisplayImage(CStatic *img, BYTE *img_buf);
   void     SaveMinFile(char* fname, BYTE* min1, BYTE* min2);

	// Generated message map functions
	//{{AFX_MSG(CMatchminDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg int OnCreate(LPCREATESTRUCT lpCreateStruct);
	afx_msg void OnDestroy();
	afx_msg void OnInitbutton();
	afx_msg void OnConfbutton();
	afx_msg void OnRegbutton();
	afx_msg void OnMatbutton();
	afx_msg void OnIdentify();
	afx_msg void OnBtnCaptureR1();
	afx_msg void OnBtnCaptureR2();
	afx_msg void OnBtnCaptureV1();
	afx_msg void OnBtnChangeFormat();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()

};

//{{AFX_INSERT_LOCATION}}
// Microsoft Developer Studio will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_MATCHMINDLG_H__2CD9D5A7_65C2_11D2_8B52_00104B673D08__INCLUDED_)
