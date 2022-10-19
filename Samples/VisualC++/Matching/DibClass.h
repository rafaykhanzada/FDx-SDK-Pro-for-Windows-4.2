/*
 * $Header: $
 * Author : Josephin, Kang
 * Description : Declaration of CDibClass
 * Copyright(c): Jos.
 * $History : $
 *
*/


#ifndef __DIBCLASS_H_
#define __DIBCLASS_H_

/////////////////////////////////////////////////////////////////////////////
class CDibClass
{
public:
    CDibClass(HWND);
    ~CDibClass();

protected:
	BITMAPINFOHEADER	m_bmpInfoHdr;
	BYTE*				m_pBits;

	BITMAPINFO*		m_pBMI;
   HPALETTE			m_hPal;

	HWND				m_hParent;
	int				m_Width, m_Height;
    int             m_TWidth;

   void CloseAll();
public:
	void DibInit(int, int);
	void SetBits(const BYTE* pBits);
	void DrawDib(const RECT* r=NULL);
   void DrawDib(const HDC dc, const RECT* r);
};
/////////////////////////////////////////////////////////////////////////////

#endif // __DIBCLASS_H_
