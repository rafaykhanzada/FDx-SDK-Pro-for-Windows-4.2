#include "stdafx.h"
#include <windows.h>
#include "dibclass.h"

//------------------------------------------------------------------------
CDibClass::CDibClass(HWND parent)
{
	memset(&m_bmpInfoHdr, 0, sizeof(BITMAPINFOHEADER));
	m_pBits = NULL;
	m_pBMI = NULL;
	m_hParent = parent;
	m_hPal = NULL;
}

//------------------------------------------------------------------------
CDibClass::~CDibClass()
{
   CloseAll();
}

//------------------------------------------------------------------------
void CDibClass::CloseAll()
{
	if (m_pBMI)
      free(m_pBMI);
	if (m_pBits)
      free(m_pBits);
   if (m_hPal)
      DeleteObject(m_hPal);

  	m_pBits = NULL;
	m_pBMI = NULL;
	m_hPal = NULL;
}

//------------------------------------------------------------------------
void CDibClass::DibInit(int w, int h)
{
   CloseAll();

   int i;
   m_Width = w;
   m_TWidth = w-w%4;
	m_Height = h;

	m_bmpInfoHdr.biSize = sizeof(BITMAPINFOHEADER);
	m_bmpInfoHdr.biWidth = m_TWidth;
	m_bmpInfoHdr.biHeight = m_Height;
	m_bmpInfoHdr.biPlanes = 1;
	m_bmpInfoHdr.biBitCount = 8;
	m_bmpInfoHdr.biCompression = BI_RGB;
	m_bmpInfoHdr.biSizeImage = w * h;
	m_bmpInfoHdr.biClrUsed = 256;
	m_bmpInfoHdr.biClrImportant = 256;

	m_pBMI = (BITMAPINFO*)malloc(sizeof(BITMAPINFOHEADER) + 256*sizeof(RGBQUAD));
	memcpy(m_pBMI, &m_bmpInfoHdr, sizeof(BITMAPINFOHEADER));

	for(i=0; i < 256; i++)
	{
		m_pBMI->bmiColors[i].rgbRed = i;
		m_pBMI->bmiColors[i].rgbGreen = i;
		m_pBMI->bmiColors[i].rgbBlue = i;
		m_pBMI->bmiColors[i].rgbReserved = 0;
	}

	//------------------------------------------------------------------------
   LOGPALETTE* pPal;

   pPal = (LOGPALETTE*)malloc(sizeof(LOGPALETTE) + sizeof(PALETTEENTRY) * 256);
   if (!pPal)
      return;

   pPal->palVersion = 0x300;
   pPal->palNumEntries = 256;
   for (i = 0; i < 256; i++)
	{
      pPal->palPalEntry[i].peRed   = i;
      pPal->palPalEntry[i].peGreen = i;
      pPal->palPalEntry[i].peBlue  = i;
      pPal->palPalEntry[i].peFlags = 0;
    }
    m_hPal = CreatePalette(pPal);
    free(pPal);
	//------------------------------------------------------------------------
}

//------------------------------------------------------------------------
/*void CDibClass::SetBits(BYTE* pBits, int size)
{
	if(m_pBits) free(m_pBits);

	m_pBits = (BYTE*)malloc(size);
	memcpy(m_pBits, pBits, size);
}*/

void CDibClass::SetBits(const BYTE* pBits)
{
	if (m_pBits)
      free(m_pBits);


	m_pBits = (BYTE*)malloc(m_TWidth*m_Height);

	for(int row = 0; row<m_Height; row++)
		memcpy(m_pBits+row*m_TWidth, pBits+(m_Height-row-1)*m_Width, m_TWidth);
    
}
//------------------------------------------------------------------------
void CDibClass::DrawDib(const RECT* r)
{
	if (m_hParent==NULL)
      return;

   RECT rcParent;

   if (r == NULL)
      GetClientRect(m_hParent, &rcParent);
   else
      CopyRect(&rcParent, r);

	HDC hdc = GetDC(m_hParent);

	if(m_hPal)
	{
		HPALETTE hOldPal = SelectPalette(hdc, m_hPal, FALSE);
		RealizePalette(hdc);

       	/* Make sure to use the stretching mode best for color pictures */
  
        ::SetStretchBltMode(hdc, HALFTONE);

		::StretchDIBits(hdc,
						rcParent.left, rcParent.top,
						rcParent.right-rcParent.left,
						rcParent.bottom-rcParent.top,
						0, 0, m_TWidth, m_Height,
						m_pBits,
						m_pBMI,
						DIB_RGB_COLORS,
						SRCCOPY);

        if (hOldPal)
			SelectPalette(hdc, hOldPal, TRUE);
	}

   ReleaseDC(m_hParent, hdc);
}

//------------------------------------------------------------------------
void CDibClass::DrawDib(const HDC hdc, const RECT* r)
{
   RECT rcParent;

   if (r == NULL)
      GetClientRect(m_hParent, &rcParent);
   else
      CopyRect(&rcParent, r);

	if(m_hPal)
	{
		HPALETTE hOldPal = SelectPalette(hdc, m_hPal, FALSE);
		RealizePalette(hdc);

       	/* Make sure to use the stretching mode best for color pictures */
  	    ::SetStretchBltMode(hdc, HALFTONE);

		::StretchDIBits(hdc,
						rcParent.left, rcParent.top,
						rcParent.right-rcParent.left,
						rcParent.bottom-rcParent.top,
						0, 0, m_TWidth, m_Height,
						m_pBits,
						m_pBMI,
						DIB_RGB_COLORS,
						SRCCOPY);
		if (hOldPal)
			SelectPalette(hdc, hOldPal, TRUE);
	}
}

