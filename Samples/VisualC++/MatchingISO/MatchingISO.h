// matchmin.h : main header file for the MATCHMIN application
//

#if !defined(AFX_MATCHMIN_H__2CD9D5A5_65C2_11D2_8B52_00104B673D08__INCLUDED_)
#define AFX_MATCHMIN_H__2CD9D5A5_65C2_11D2_8B52_00104B673D08__INCLUDED_

#if _MSC_VER >= 1000
#pragma once
#endif // _MSC_VER >= 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CMatchminApp:
// See matchmin.cpp for the implementation of this class
//

class CMatchminApp : public CWinApp
{
public:
	CMatchminApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CMatchminApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CMatchminApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Developer Studio will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_MATCHMIN_H__2CD9D5A5_65C2_11D2_8B52_00104B673D08__INCLUDED_)
