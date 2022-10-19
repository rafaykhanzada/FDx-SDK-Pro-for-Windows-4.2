// matchingUAIRDlg.h : header file
//
#include "dibclass.h"
#include "..\..\..\inc\sgfplib.h"
#include "afxwin.h"

#include <vector>
#include <memory>
#include <string>

using namespace std;

#if !defined(AFX_MATCHMINDLG_H__2CD9D5A7_65C2_11D2_8B52_00104B673D08__INCLUDED_)
#define AFX_MATCHMINDLG_H__2CD9D5A7_65C2_11D2_8B52_00104B673D08__INCLUDED_

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000

/////////////////////////////////////////////////////////////////////////////
// CMatchingUAIRDlg dialog

class CMatchingUAIRDlg : public CDialog
{
// Construction
public:
	DWORD m_DeviceType;
	CMatchingUAIRDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CMatchingUAIRDlg)
	enum { IDD = IDD_MATCHINGUAIR_DIALOG };
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
	//{{AFX_VIRTUAL(CMatchingUAIRDlg)
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
   void		SaveMinFile(string fname, vector<unique_ptr<BYTE[]>> &mins);

   void		UpdateStatusbar();
   void		SetTemplateFormat(int templateSelected);

	// Generated message map functions
	//{{AFX_MSG(CMatchingUAIRDlg)
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
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()

	// No longer used. only for reference
	void OnRegbuttonObsolete();
	void OnMatbuttonObsolete();
	void OnIdentifyObsolete();

private:
	const int SIZE_OF_FEATURE_BUF = 400;	// SG400 template
	const int NUM_OF_REGS = 5;				// 5 through 10 recommended for U-Air
	const int MIN_NUM_OF_MATCHED = 2;		// 2..NUM_OF_REGS

	vector<unique_ptr<BYTE[]>> m_FetBufRegs;	// features for registration
	vector<CStatic*> m_RegThumbnails;			// thumbnails for registration
	unique_ptr<BYTE[]> m_FetBuf;				// temporay feature buffer
	DWORD m_DevName = SG_DEV_UNKNOWN;

	BOOL IsAllRegsCaptured();
	BOOL MatchRegs(BYTE* fetBuf, DWORD& match_score);	// match fetBuf against ones stored for registration
	BOOL MatchTemplates(BYTE* fetBuf, vector<unique_ptr<BYTE[]>> &templates, int numOfMinMatched);
	void CaptureRegs();									// capture for registration

public:

	afx_msg void OnBtnCaptureReg();
	afx_msg void OnBtnClearReg();

	CButton m_CapBtnReg;
	CButton m_ClearBtnReg;

	CStatic m_RegThumbnail1;
	CStatic m_RegThumbnail2;
	CStatic m_RegThumbnail3;
	CStatic m_RegThumbnail4;
	CStatic m_RegThumbnail5;
	afx_msg void OnIdok();
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Developer Studio will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_MATCHINGUAIRDLG_H__2CD9D5A7_65C2_11D2_8B52_00104B673D08__INCLUDED_)
