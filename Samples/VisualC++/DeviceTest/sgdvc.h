// sgdvc.h : main header file for the SGDVC application
//

#if !defined(AFX_SGDVC_H__BB9CD1DE_9DC2_4C4D_8EF4_3CADF8C2900E__INCLUDED_)
#define AFX_SGDVC_H__BB9CD1DE_9DC2_4C4D_8EF4_3CADF8C2900E__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CSgdvcApp:
// See sgdvc.cpp for the implementation of this class
//

class CSgdvcApp : public CWinApp
{
public:
	CSgdvcApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CSgdvcApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CSgdvcApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SGDVC_H__BB9CD1DE_9DC2_4C4D_8EF4_3CADF8C2900E__INCLUDED_)
