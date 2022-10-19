// matchmin.h : main header file for the MATCHMIN application
//

#if !defined(AFX_MATCHINGUAIR_H__2CD9D5A5_65C2_11D2_8B52_00104B673D08__INCLUDED_)
#define AFX_MATCHINGUAIR_H__2CD9D5A5_65C2_11D2_8B52_00104B673D08__INCLUDED_

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CMatchingUAIRApp:
// See matchingUAIR.cpp for the implementation of this class
//

class CMatchingUAIRApp : public CWinApp
{
public:
	CMatchingUAIRApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CMatchingUAIRApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CMatchingUAIRApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Developer Studio will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_MATCHINGUAIR_H__2CD9D5A5_65C2_11D2_8B52_00104B673D08__INCLUDED_)
