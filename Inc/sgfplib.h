

#ifndef SGFPLIB_H
#define SGFPLIB_H

#if (defined(WIN32) || defined(_WIN32_WCE))
   #include <windows.h>
  
   #ifdef SGFPLIB_EXPORTS
     #define SGFPM_DLL_DECL __declspec(dllexport)
   #else
     //#define SGFPM_DLL_DECL __declspec(dllimport)
     #define SGFPM_DLL_DECL
   #endif

#else
   #define SGFPM_DLL_DECL
   #define WINAPI
   #define FAR

   #ifndef NULL
   #define NULL 0
   #endif

   typedef void*           HWND;
   typedef void*           HDC;
   typedef unsigned long   DWORD;
   typedef int             BOOL;
   typedef unsigned char   BYTE;
   typedef unsigned short  WORD;

   typedef struct
   {   
      int left, top, right, bottom;
   } RECT;

   typedef RECT* LPRECT;

#endif/* WIN32 */


#ifdef __cplusplus
extern "C" {
#endif

enum SGFDxDeviceName
{
   SG_DEV_UNKNOWN = 0,
   SG_DEV_FDP02 = 0x01,
   SG_DEV_FDU02 = 0x03,
   SG_DEV_FDU03 = 0x04,       // Hamster Plus
   SG_DEV_FDU04 = 0x05,       // Hamster IV
   SG_DEV_FDU05 = 0x06,       // HU20
   SG_DEV_FDU06 = 0x07,       // UPx
   SG_DEV_FDU07 = 0x08,       // U10
   SG_DEV_FDU07A = 0x09,	   // U10-AP //(A)
   SG_DEV_FDU08 = 0x0A,	      // U20A
   SG_DEV_FDU08P =0x0B,	      // discontinued U20-AP
   SG_DEV_FDU06P = 0x0C,      // UPx-P
   SG_DEV_FDUSDA = 0x0D,      // U20-ASF-BT (SPP/Serial)
   SG_DEV_FDUSDA_BLE = 0x0E,  // U20-ASF-BT (BLE)
   SG_DEV_FDU08X = 0x0F,      // U20-ASFX (USB)
   SG_DEV_FDU09 = 0x10,       // discontinued U30
   SG_DEV_FDU08A = 0x11,	   // U20-AP //(A)
   SG_DEV_FDU09A = 0x12,	   // U30    //(A)
   SG_DEV_FDU10A = 0x13,      // U-AIR  //(A)
   SG_DEV_AUTO = 0xFF,
};

enum SGPPPortAddr {
   AUTO_DETECT = 0,
   LPT1        = 0x378,
   LPT2        = 0x278,
   LPT3        = 0x3BC,
   USB_AUTO_DETECT = 0x3BC+1,
};

enum SGFDxSecurityLevel 
{
   SL_NONE = 0,
   SL_LOWEST = 1,
   SL_LOWER = 2,
   SL_LOW = 3,
   SL_BELOW_NORMAL = 4,   
   SL_NORMAL = 5,
   SL_ABOVE_NORMAL = 6,
   SL_HIGH = 7,
   SL_HIGHER = 8,
   SL_HIGHEST = 9,
};


enum SGFDxTemplateFormat 
{
   TEMPLATE_FORMAT_ANSI378 = 0x0100,
   TEMPLATE_FORMAT_SG400   = 0x0200,
   TEMPLATE_FORMAT_ISO19794 = 0x0300,
   TEMPLATE_FORMAT_ISO19794_COMPACT = 0x0400,
};

enum SGCallBackFuntion {
   CALLBACK_LIVE_CAPTURE = 1,
   CALLBACK_AUTO_ON_EVENT = 2,
};


enum SGFDxErrorCode {
   // General error
   SGFDX_ERROR_NONE = 0,
   SGFDX_ERROR_CREATION_FAILED = 1,
   SGFDX_ERROR_FUNCTION_FAILED = 2,
   SGFDX_ERROR_INVALID_PARAM = 3,
   SGFDX_ERROR_NOT_USED = 4,
   SGFDX_ERROR_DLLLOAD_FAILED = 5,
   SGFDX_ERROR_DLLLOAD_FAILED_DRV = 6,
   SGFDX_ERROR_DLLLOAD_FAILED_ALGO = 7,
   SGFDX_ERROR_NO_LONGER_SUPPORTED = 8,
   SGFDX_ERROR_DLLLOAD_FAILED_WSQ = 9,   //  11/3/2017


   // Device error
   SGFDX_ERROR_SYSLOAD_FAILED = 51,      // system file load fail
   SGFDX_ERROR_INITIALIZE_FAILED = 52,   // chip initialize fail
   SGFDX_ERROR_LINE_DROPPED = 53,        // image data drop
   SGFDX_ERROR_TIME_OUT = 54,            // getliveimage timeout error
   SGFDX_ERROR_DEVICE_NOT_FOUND = 55,    // device not found
   SGFDX_ERROR_DRVLOAD_FAILED = 56,      // dll file load fail
   SGFDX_ERROR_WRONG_IMAGE = 57,         // wrong image
   SGFDX_ERROR_LACK_OF_BANDWIDTH = 58,  // USB Bandwith Lack Error
   SGFDX_ERROR_DEV_ALREADY_OPEN = 59,    // Device Exclusive access Error
   SGFDX_ERROR_GETSN_FAILED = 60,        // Fail to get Device Serial Number
   SGFDX_ERROR_UNSUPPORTED_DEV = 61,     // Unsupported device
   SGFDX_ERROR_FAKE_FINGER = 62,		 // Fake finger
   SGFDX_ERROR_FAKE_INITIALIZE_FAILED = 63,	  // Initialization failure
   
   // Extract &Matching error
   SGFDX_ERROR_FEAT_NUMBER = 101,  // too small number of minutiae
   SGFDX_ERROR_INVALID_TEMPLATE_TYPE = 102,      // wrong template type
   SGFDX_ERROR_INVALID_TEMPLATE1 = 103,         //error in decoding template 1
   SGFDX_ERROR_INVALID_TEMPLATE2 = 104,         //error in decoding template 2
   SGFDX_ERROR_EXTRACT_FAIL = 105,      
   SGFDX_ERROR_MATCH_FAIL = 106,

   // Licensing error
   SGFDX_ERROR_LICENSE_LOAD = 501,
   SGFDX_ERROR_LICENSE_KEY = 502,
   SGFDX_ERROR_LICENSE_EXPIRED = 503,
   SGFDX_ERROR_LICENSE_WRITE = 504,

	// WSQ error
	SGFDX_ERROR_NO_IMAGE = 600,

};

enum SGImpressionType 
{
   SG_IMPTYPE_LP =	0x00,		// Live-scan plain
   SG_IMPTYPE_LR =	0x01,		// Live-scan rolled
   SG_IMPTYPE_NP =	0x02,		// Nonlive-scan plain
   SG_IMPTYPE_NR =	0x03,		// Nonlive-scan rolled
};

enum SGFingerPosition
{ 
   SG_FINGPOS_UK = 0x00,		// Unknown finger
   SG_FINGPOS_RT = 0x01,		// Right thumb
   SG_FINGPOS_RI = 0x02,		// Right index finger
   SG_FINGPOS_RM = 0x03,		// Right middle finger
   SG_FINGPOS_RR = 0x04,		// Right ring finger
   SG_FINGPOS_RL = 0x05,		// Right little finger
   SG_FINGPOS_LT = 0x06,		// Left thumb
   SG_FINGPOS_LI = 0x07,		// Left index finger
   SG_FINGPOS_LM = 0x08,		// Left middle finger
   SG_FINGPOS_LR = 0x09,		// Left ring finger
   SG_FINGPOS_LL = 0x0A,		// Left little finger
};


#define SGDEV_SN_LEN          15   // Device Serial Number Length
// For AutoOn event
#ifndef WM_APP_SGAUTOONEVENT
#define WM_APP_SGAUTOONEVENT    0x8100  // From ISensor.h
#define SGDEVEVNET_FINGER_OFF   0
#define SGDEVEVNET_FINGER_ON    1

#endif


// 11.3.2017   WSQ image lossy compression bit rates
#ifndef WSQ_BITRATE_5_TO_1
#define WSQ_BITRATE_5_TO_1  2.25 // yields around 5:1 compression
#endif

#ifndef WSQ_BITRATE_15_TO_1
#define WSQ_BITRATE_15_TO_1 0.75 // yields around 15:1 compression
#endif

// EnumerateDevice()
typedef struct tagSGDeviceList
{
   DWORD DevName;
   DWORD DevID;
   WORD  DevType;   
   BYTE  DevSN[SGDEV_SN_LEN+1];
} SGDeviceList;
typedef SGDeviceList *LPDeviceList;

// FindDevices()
#define SGDEV_NAME_LEN	15
#define SGDEV_ID_LEN	63
typedef struct tagSGDeviceInfo
{
   wchar_t Name[SGDEV_NAME_LEN + 1];	// null-terminated wide string
   wchar_t ID[SGDEV_ID_LEN + 1];		// null-terminated wide string
} SGDeviceInfo, *LPSGDeviceInfo;

// GetDeviceInfo()
typedef struct tagSGDeviceInfoParam
{
   DWORD DeviceID;         // 0 - 9
   BYTE  DeviceSN[SGDEV_SN_LEN+1];         // Device Serial Number, Length of SN = 15    
   DWORD ComPort;          // Parallel device=>PP address, USB device=>USB(0x3BC+1)
   DWORD ComSpeed;         // Parallel device=>PP mode, USB device=>0 
   DWORD ImageWidth;       // Image Width
   DWORD ImageHeight;      // Image Height
   DWORD Contrast; 	      // 0 ~ 100
   DWORD Brightness;       // 0 ~ 100
   DWORD Gain;             // Dependent on each device
   DWORD ImageDPI;         // DPI
   DWORD FWVersion;        // FWVersion
} SGDeviceInfoParam;
typedef SGDeviceInfoParam* LPSGDeviceInfoParam;

typedef struct tagSGFingerInfo {
    WORD FingerNumber;           // FingerNumber. 
    WORD ViewNumber;             // Sample number
    WORD ImpressionType;         // impression type. Should be 0
    WORD ImageQuality;           // Image quality
} SGFingerInfo;

typedef struct tagSGANSITemplateInfo {
    DWORD TotalSamples; 
    SGFingerInfo SampleInfo[225];   //max finger number 15 x max view number 15 = 225
} SGANSITemplateInfo, SGISOTemplateInfo;

typedef struct tagSGCBLiveCaptureParam {
   DWORD  ImageWidth;
   DWORD  ImageHeight;
   DWORD  Quality;
   DWORD  ErrorCode;
} SGCBLiveCaptureParam;

// 2006.10.9, Used in CreateTemplate2()
typedef struct tagSGFPImageInfo {
	WORD CaptureEquip;      // capture equipment ID
	WORD ImageSizeInX;      // in pixels
	WORD ImageSizeInY;      // in pixels
	WORD XResolution;       // in pixels per cm 500dpi = 197
	WORD YResolution;       // in pixels per cm
	WORD FingerNumber;      // capture equipment ID
	WORD ViewNumber;        // in pixels
	WORD ImpressionType;    // in pixels
	WORD ImageQuality;      // in pixels per cm 500dpi = 197
} SGFPImageInfo;

typedef struct tagSGANSITemplateInfoEx {
    DWORD TotalSamples; 
    SGFPImageInfo SampleInfo[225];  //max finger number 15 x max view number 15 = 225
} SGANSITemplateInfoEx, SGISOTemplateInfoEx;


///////////////////////////////////////////////
// For direcrect access for SGFPM Class 
///////////////////////////////////////////////
struct SGFPM
{
   virtual ~SGFPM(){};
   virtual DWORD WINAPI  GetLastError() = 0;
   virtual DWORD WINAPI  Init(DWORD devName) = 0;
   //virtual DWORD WINAPI  Init(DWORD width, DWORD height, DWORD dpi) = 0; // Some compiler can not recognize fuction overloading..
   virtual DWORD WINAPI  InitEx(DWORD width, DWORD height, DWORD dpi) = 0;
   virtual DWORD WINAPI  SetTemplateFormat(WORD format) = 0; // default is SG400

   // Image sensor API
   virtual DWORD WINAPI  EnumerateDevice(DWORD* ndevs, SGDeviceList** devList) = 0;
   virtual DWORD WINAPI  OpenDevice(DWORD devId) = 0;
   virtual DWORD WINAPI  CloseDevice() = 0;
   virtual DWORD WINAPI  GetDeviceInfo(SGDeviceInfoParam* pInfo)= 0;
   virtual DWORD WINAPI  Configure(HWND hwnd) = 0;
   virtual DWORD WINAPI  SetBrightness(DWORD brightness) = 0;
   virtual DWORD WINAPI  SetLedOn(bool on) = 0;

   virtual DWORD WINAPI  GetImage(BYTE* buffer)= 0;
   virtual DWORD WINAPI  GetImageEx(BYTE* buffer, DWORD timeout, HWND dispWnd, DWORD quality)= 0;
   virtual DWORD WINAPI  GetImageEx2(BYTE* buffer, DWORD timeout, HDC dispDC, LPRECT dispRect, DWORD quality)= 0;
   virtual DWORD WINAPI  GetImageQuality(DWORD width, DWORD height, BYTE* imgBuf, DWORD* quality)= 0;
   virtual DWORD WINAPI  SetCallBackFunction(DWORD selector, DWORD (WINAPI*)(void* pUserData, void* pCallBackData), void* pUserData) = 0;
   
   // FDU03 Only APIs
	virtual DWORD WINAPI  EnableAutoOnEvent(BOOL enable, HWND hwnd, void* reserved)= 0;

   // Algorithm: Extraction API
   virtual DWORD WINAPI  GetMaxTemplateSize(DWORD* size) = 0;
   virtual DWORD WINAPI  CreateTemplate(SGFingerInfo* fpInfo, BYTE *rawImage, BYTE* minTemplate)= 0;
   virtual DWORD WINAPI  GetTemplateSize(BYTE* buf, DWORD* size) = 0;

   // Algorithm: Matching API
   virtual DWORD  WINAPI  MatchTemplate(BYTE *minTemplate1, BYTE *minTemplate2, DWORD secuLevel, BOOL* matched) = 0;
   virtual DWORD  WINAPI  GetMatchingScore(BYTE* minTemplate1, BYTE* minTemplate2, DWORD* score) = 0;

   // Algorithim: Only work with ANSI378 Template
   virtual DWORD  WINAPI  GetTemplateSizeAfterMerge(BYTE* ansiTemplate1, BYTE* ansiTemplate2, DWORD* size) = 0;
   virtual DWORD  WINAPI  MergeAnsiTemplate(BYTE* ansiTemplate1, BYTE* ansiTemplate2, BYTE* outTemplate) = 0;
   virtual DWORD  WINAPI  MergeMultipleAnsiTemplate(BYTE* inTemplates, DWORD nTemplates, BYTE* outTemplate) = 0;
   virtual DWORD  WINAPI  GetAnsiTemplateInfo(BYTE* ansiTemplate, SGANSITemplateInfo* templateInfo) = 0;
   virtual DWORD  WINAPI  MatchAnsiTemplate(BYTE*  ansiTemplate1, DWORD  sampleNum1, BYTE*  ansiTemplate2, DWORD sampleNum2, DWORD secuLevel, BOOL*  matched) = 0;
   virtual DWORD  WINAPI  GetAnsiMatchingScore(BYTE*  ansiTemplate1, DWORD    sampleNum1, BYTE* ansiTemplate2, DWORD sampleNum2, DWORD* score) = 0;


   // Algorithim: Only work with ISO19794 Template
   virtual DWORD  WINAPI  GetIsoTemplateSizeAfterMerge(BYTE* isoTemplate1, BYTE* isoTemplate2, DWORD* size) = 0;
   virtual DWORD  WINAPI  MergeIsoTemplate(BYTE* isoTemplate1, BYTE* isoTemplate2, BYTE* outTemplate) = 0;
   virtual DWORD  WINAPI  MergeMultipleIsoTemplate(BYTE* inTemplates, DWORD nTemplates, BYTE* outTemplate) = 0;
   virtual DWORD  WINAPI  GetIsoTemplateInfo(BYTE* isoTemplate, SGISOTemplateInfo* templateInfo) = 0;
   virtual DWORD  WINAPI  MatchIsoTemplate(BYTE*  isoTemplate1, DWORD sampleNum1, BYTE*  isoTemplate2, DWORD sampleNum2, DWORD secuLevel, BOOL*  matched) = 0;
   virtual DWORD  WINAPI  GetIsoMatchingScore(BYTE*  isoTemplate1, DWORD sampleNum1, BYTE* isoTemplate2, DWORD sampleNum2, DWORD* score) = 0;

   // Algorithim: Only work with ISO19794 Compact Template
   virtual DWORD  WINAPI  GetIsoCompactTemplateSizeAfterMerge(BYTE* isoTemplate1, BYTE* isoTemplate2, DWORD* size) = 0;
   virtual DWORD  WINAPI  MergeIsoCompactTemplate(BYTE* isoTemplate1, BYTE* isoTemplate2, BYTE* outTemplate) = 0;
   virtual DWORD  WINAPI  MergeMultipleIsoCompactTemplate(BYTE* inTemplates, DWORD nTemplates, BYTE* outTemplate) = 0;
   virtual DWORD  WINAPI  GetIsoCompactTemplateInfo(BYTE* isoTemplate, SGISOTemplateInfo* templateInfo) = 0;
   virtual DWORD  WINAPI  MatchIsoCompactTemplate(BYTE*  isoTemplate1, DWORD sampleNum1, BYTE*  isoTemplate2, DWORD sampleNum2, DWORD secuLevel, BOOL*  matched) = 0;
   virtual DWORD  WINAPI  GetIsoCompactMatchingScore(BYTE*  isoTemplate1, DWORD sampleNum1, BYTE* isoTemplate2, DWORD sampleNum2, DWORD* score) = 0;

   // Algorithim: 
   virtual DWORD  WINAPI  MatchTemplateEx(BYTE*  minTemplate1, WORD tempateType1,  DWORD sampleNum1, BYTE* minTemplate2, WORD tempateType2,  DWORD sampleNum2, DWORD  secuLevel, BOOL*  matched) = 0;
   virtual DWORD  WINAPI  GetMatchingScoreEx(BYTE* minTemplate1, WORD tempateType1, DWORD sampleNum1, BYTE* minTemplate2, WORD tempateType2, DWORD sampleNum2, DWORD* score) = 0;

   // 2006.6.5, Device Driver
   virtual  DWORD	WINAPI  SetAutoOnIRLedTouchOn(BOOL iRLed, BOOL touchOn) = 0;
   // Algorithm, 2006.10.9, jkang
   virtual  DWORD  WINAPI  GetMinexVersion(DWORD *extractor, DWORD* matcher) = 0;

   // Algorithm, 2006.10.9, Can set Image width, height, xRes, yRes separately 
   virtual  DWORD  WINAPI  CreateTemplateEx(SGFPImageInfo* fpImageInfo, BYTE *rawImage, BYTE* minTemplate)= 0;
   // Algorithm, 2006.10.9, have Image width, height, xRes, yRes separately 
   virtual DWORD  WINAPI   GetAnsiTemplateInfoEx(BYTE* ansiTemplate, SGANSITemplateInfoEx* templateInfo) = 0;

   // 06/08/2009, Enable/disable the check of finger liveness
   virtual DWORD WINAPI		EnableCheckOfFingerLiveness(bool enable) = 0;

   // 05/19/2011, Adjust fake detection level
   virtual DWORD WINAPI		SetFakeDetectionLevel(int level) = 0;

   // 05/27/2011, Get fake detection level
   virtual DWORD WINAPI		GetFakeDetectionLevel(int *level) = 0;

   // 09/09/2011, Send commands to device
   virtual DWORD WINAPI		WriteData(unsigned char index, unsigned char data) = 0;

   // 1/9/2015, Set/Get data 
   virtual DWORD WINAPI		SetGetData(DWORD flag, void* data) = 0;

   // 11/10/2015, InitEx2()
   virtual DWORD WINAPI		InitEx2(DWORD width, DWORD height, DWORD dpi, char* licenseFilePath) = 0;

   // 8/25/2016, Get the number of minutiae
   virtual DWORD WINAPI		GetNumOfMinutiae(WORD templateType, BYTE* minTemplate, DWORD *numOfMinutiae) = 0;

	// 11/3/2017, JosKang, Encode Raw image data to WSQ format
	virtual DWORD WINAPI  EncodeWSQ(BYTE ** wsqImageOut, DWORD *wsqImageOutSize, float wsqBitRate,  BYTE * rawImage, DWORD width, DWORD height, DWORD pixelDepth, DWORD ppi, char *commentText) = 0;

   // 11/3/2017, JosKang, Decode WSQ image to raw image
	virtual DWORD WINAPI  DecodeWSQ(BYTE **rawImageOut, DWORD *width, DWORD *height, DWORD *pixelDepth, DWORD *ppi, DWORD *lossyFlag, BYTE *wsqImage, DWORD wsqImageLength) = 0;
	
	// 11/3/2017, JosKang, Free image buffer inside dll. Should be called after EncodeToWSQ() or DecodeFromWSQ()
	virtual DWORD WINAPI FreeWSQ(BYTE *imgBuffer) = 0;

	//11.16.2017, JKang, Convert text data to binary. For coverting, base64 encoding/decoding is used.
	virtual DWORD WINAPI ByteToText(BYTE *data,  DWORD dwdatasize, 	LPTSTR textData) = 0;

	// 11.16.2017, JKang, Convert binary data to text. For coverting, base64 encoding/decoding is used.
	virtual DWORD WINAPI TextToByte(LPTSTR textData, BYTE *destdata, DWORD *pdatasize) = 0;

	// 11/19/2019, Find devices with ID string
    virtual DWORD WINAPI FindDevices(DWORD* ndevs, SGDeviceInfo** devList, DWORD timeout) = 0;
	virtual DWORD WINAPI CancelFind() = 0;
	virtual DWORD WINAPI OpenDevice2(wchar_t *devId) = 0;

	// 12/02/2019, Get a template from a device if available
	virtual DWORD WINAPI CreateTemplateDev(DWORD *size) = 0;
	virtual DWORD WINAPI GetTemplateDev(BYTE* min) = 0;
	virtual DWORD WINAPI GetTemplateFormatDev(WORD *format) = 0;
	virtual DWORD WINAPI SetTemplateFormatDev(WORD format) = 0;

	// 7/14/2021, complementary functions of GetImage()
	virtual DWORD WINAPI BeginGetImage() = 0;
	virtual DWORD WINAPI EndGetImage() = 0;

	// 7/14/2021, Get simple image quality for last image captured.
	virtual DWORD WINAPI GetLastImageQuality(DWORD* quality) = 0;

	// 7/14/2021, Enable smart capture
	virtual DWORD  WINAPI EnableSmartCapture(bool enable) = 0;
};

//struct SGFPM;
typedef SGFPM  FAR* LPSGFPM;
typedef DWORD (WINAPI* SGFPM_CreateFunc)(LPSGFPM* ppFPM);
typedef DWORD (WINAPI* SGFPM_DestroyFunc)(SGFPM* pFPM);
 
SGFPM_DLL_DECL DWORD WINAPI  CreateSGFPMObject(LPSGFPM* ppFPM = NULL);
SGFPM_DLL_DECL DWORD WINAPI  DestroySGFPMObject(SGFPM* pFPM = NULL);

//------------------------------------------------------------------------------
typedef void*  HSGFPM;

SGFPM_DLL_DECL DWORD WINAPI  SGFPM_Create(HSGFPM* phFPM);
SGFPM_DLL_DECL DWORD WINAPI  SGFPM_Terminate(HSGFPM hFpm);
SGFPM_DLL_DECL DWORD WINAPI  SGFPM_Init(HSGFPM hFpm, DWORD devName);
SGFPM_DLL_DECL DWORD WINAPI  SGFPM_InitEx(HSGFPM hFpm, DWORD width, DWORD height, DWORD dpi);
SGFPM_DLL_DECL DWORD WINAPI  SGFPM_SetTemplateFormat(HSGFPM hFpm, WORD format); // default is ANSI378
SGFPM_DLL_DECL DWORD WINAPI  SGFPM_GetLastError(HSGFPM hFpm);

// Image sensor API
SGFPM_DLL_DECL DWORD WINAPI  SGFPM_EnumerateDevice(HSGFPM hFpm, DWORD* ndevs, SGDeviceList** devList);
SGFPM_DLL_DECL DWORD WINAPI  SGFPM_OpenDevice(HSGFPM hFpm, DWORD devId);
SGFPM_DLL_DECL DWORD WINAPI  SGFPM_CloseDevice(HSGFPM hFpm);
SGFPM_DLL_DECL DWORD WINAPI  SGFPM_GetDeviceInfo(HSGFPM hFpm, SGDeviceInfoParam* pInfo);
SGFPM_DLL_DECL DWORD WINAPI  SGFPM_Configure(HSGFPM hFpm, HWND hwnd);
SGFPM_DLL_DECL DWORD WINAPI  SGFPM_SetBrightness(HSGFPM hFpm, DWORD brightness);
SGFPM_DLL_DECL DWORD WINAPI  SGFPM_SetLedOn(HSGFPM hFpm, bool on);

SGFPM_DLL_DECL DWORD WINAPI  SGFPM_GetImage(HSGFPM hFpm, BYTE* buffer);
SGFPM_DLL_DECL DWORD WINAPI  SGFPM_GetImageEx(HSGFPM hFpm, BYTE* buffer, DWORD time = 0, HWND dispWnd = NULL, DWORD quality = 50);
SGFPM_DLL_DECL DWORD WINAPI  SGFPM_GetImageEx2(HSGFPM hFpm, BYTE* buffer, DWORD time, HDC dispDC, LPRECT dispRect, DWORD quality);

SGFPM_DLL_DECL DWORD WINAPI  SGFPM_GetImageQuality(HSGFPM hFpm, DWORD width, DWORD height, BYTE* imgBuf, DWORD* quality);
SGFPM_DLL_DECL DWORD WINAPI  SGFPM_SetCallBackFunction(HSGFPM hFpm, DWORD selector, DWORD (WINAPI*)(void* pUserData, void* pCallBackData), void* pUserData);

// FDU03 or Later device Only APIs
SGFPM_DLL_DECL DWORD WINAPI  SGFPM_EnableAutoOnEvent(HSGFPM hFpm, BOOL enable, HWND hwnd, void* reserved);

// Algorithm: Extraction API
SGFPM_DLL_DECL DWORD WINAPI  SGFPM_GetMaxTemplateSize(HSGFPM hFpm, DWORD* size);
SGFPM_DLL_DECL DWORD WINAPI  SGFPM_CreateTemplate(HSGFPM hFpm, SGFingerInfo* fpInfo, BYTE *rawImage, BYTE* minTemplate);
SGFPM_DLL_DECL DWORD WINAPI  SGFPM_GetTemplateSize(HSGFPM hFpm, BYTE* minTemplate, DWORD* size);

// Algorithm: Matching API
SGFPM_DLL_DECL DWORD WINAPI  SGFPM_MatchTemplate(HSGFPM hFpm, BYTE *minTemplate1, BYTE *minTemplate2, DWORD secuLevel, BOOL* matched);
SGFPM_DLL_DECL DWORD WINAPI  SGFPM_GetMatchingScore(HSGFPM hFpm, BYTE* minTemplate1, BYTE* minTemplate2, DWORD* score);

// Algorithim: Only work with ANSI378 Template
SGFPM_DLL_DECL DWORD  WINAPI  SGFPM_GetTemplateSizeAfterMerge(HSGFPM hFpm, BYTE* ansiTemplate1, BYTE* ansiTemplate2, DWORD* size);
SGFPM_DLL_DECL DWORD  WINAPI  SGFPM_MergeAnsiTemplate(HSGFPM hFpm, BYTE* ansiTemplate1, BYTE* ansiTemplate2, BYTE* outTemplate);
SGFPM_DLL_DECL DWORD  WINAPI  SGFPM_MergeMultipleAnsiTemplate(HSGFPM hFpm, BYTE* inTemplates, DWORD nTemplates, BYTE* outTemplate);
SGFPM_DLL_DECL DWORD  WINAPI  SGFPM_GetAnsiTemplateInfo(HSGFPM hFpm, BYTE* ansiTemplate, SGANSITemplateInfo* templateInfo);
SGFPM_DLL_DECL DWORD  WINAPI  SGFPM_MatchAnsiTemplate(HSGFPM hFpm, BYTE* ansiTemplate1, DWORD sampleNum1, BYTE* ansiTemplate2, DWORD  sampleNum2, DWORD  secuLevel, BOOL*  matched);
SGFPM_DLL_DECL DWORD  WINAPI  SGFPM_GetAnsiMatchingScore(HSGFPM hFpm, BYTE* ansiTemplate1, DWORD  sampleNum1, BYTE* ansiTemplate2, DWORD    sampleNum2, DWORD* score);

SGFPM_DLL_DECL DWORD  WINAPI  SGFPM_MatchTemplateEx(HSGFPM hFpm, BYTE* minTemplate1, WORD   tempateType1,  DWORD  sampleNum1, BYTE* minTemplate2, WORD   tempateType2,  DWORD sampleNum2, DWORD  secuLevel, BOOL*  matched);
SGFPM_DLL_DECL DWORD  WINAPI  SGFPM_GetMatchingScoreEx(HSGFPM hFpm, BYTE* minTemplate1, WORD tempateType1, DWORD sampleNum1, BYTE* minTemplate2, WORD tempateType2, DWORD sampleNum2, DWORD* score);

SGFPM_DLL_DECL DWORD  WINAPI  SGFPM_SetAutoOnIRLedTouchOn(HSGFPM hFpm, BOOL iRLed, BOOL touchOn);

SGFPM_DLL_DECL DWORD  WINAPI  SGFPM_GetMinexVersion(HSGFPM hFpm, DWORD *extractor, DWORD* matcher);
SGFPM_DLL_DECL DWORD  WINAPI  SGFPM_CreateTemplateEx(HSGFPM hFpm, SGFPImageInfo* fpImageInfo, BYTE *rawImage, BYTE* minTemplate);
SGFPM_DLL_DECL DWORD  WINAPI  SGFPM_GetAnsiTemplateInfoEx(HSGFPM hFpm, BYTE* ansiTemplate, SGANSITemplateInfoEx* templateInfo);

// Algorithim: Only work with ISO19794 Template
SGFPM_DLL_DECL DWORD  WINAPI  SGFPM_GetIsoTemplateSizeAfterMerge(HSGFPM hFpm, BYTE* isoTemplate1, BYTE* isoTemplate2, DWORD* size);
SGFPM_DLL_DECL DWORD  WINAPI  SGFPM_MergeIsoTemplate(HSGFPM hFpm, BYTE* isoTemplate1, BYTE* isoTemplate2, BYTE* outTemplate);
SGFPM_DLL_DECL DWORD  WINAPI  SGFPM_MergeMultipleIsoTemplate(HSGFPM hFpm, BYTE* inTemplates, DWORD nTemplates, BYTE* outTemplate);
SGFPM_DLL_DECL DWORD  WINAPI  SGFPM_GetIsoTemplateInfo(HSGFPM hFpm, BYTE* isoTemplate, SGISOTemplateInfo* templateInfo);
SGFPM_DLL_DECL DWORD  WINAPI  SGFPM_MatchIsoTemplate(HSGFPM hFpm, BYTE* isoTemplate1, DWORD sampleNum1, BYTE* isoTemplate2, DWORD  sampleNum2, DWORD  secuLevel, BOOL*  matched);
SGFPM_DLL_DECL DWORD  WINAPI  SGFPM_GetIsoMatchingScore(HSGFPM hFpm, BYTE* isoTemplate1, DWORD  sampleNum1, BYTE* isoTemplate2, DWORD sampleNum2, DWORD* score);

// Algorithim: Only work with ISO19794 Compact Template
SGFPM_DLL_DECL DWORD  WINAPI  SGFPM_GetIsoCompactTemplateSizeAfterMerge(HSGFPM hFpm, BYTE* isoTemplate1, BYTE* isoTemplate2, DWORD* size);
SGFPM_DLL_DECL DWORD  WINAPI  SGFPM_MergeIsoCompactTemplate(HSGFPM hFpm, BYTE* isoTemplate1, BYTE* isoTemplate2, BYTE* outTemplate);
SGFPM_DLL_DECL DWORD  WINAPI  SGFPM_MergeMultipleIsoCompactTemplate(HSGFPM hFpm, BYTE* inTemplates, DWORD nTemplates, BYTE* outTemplate);
SGFPM_DLL_DECL DWORD  WINAPI  SGFPM_GetIsoCompactTemplateInfo(HSGFPM hFpm, BYTE* isoTemplate, SGISOTemplateInfo* templateInfo);
SGFPM_DLL_DECL DWORD  WINAPI  SGFPM_MatchIsoCompactTemplate(HSGFPM hFpm, BYTE* isoTemplate1, DWORD sampleNum1, BYTE* isoTemplate2, DWORD  sampleNum2, DWORD  secuLevel, BOOL*  matched);
SGFPM_DLL_DECL DWORD  WINAPI  SGFPM_GetIsoCompactMatchingScore(HSGFPM hFpm, BYTE* isoTemplate1, DWORD  sampleNum1, BYTE* isoTemplate2, DWORD sampleNum2, DWORD* score);

// 06/08/2009, Enable/disable the check of finger liveness
SGFPM_DLL_DECL DWORD WINAPI	  SGFPM_EnableCheckOfFingerLiveness(HSGFPM hFpm, BOOL enable);

// 05/19/2011, Adjust fake detection level
SGFPM_DLL_DECL DWORD WINAPI	  SGFPM_SetFakeDetectionLevel(HSGFPM hFpm, int level);	

// 05/27/2011, Get fake detection level
SGFPM_DLL_DECL DWORD WINAPI	  SGFPM_GetFakeDetectionLevel(HSGFPM hFpm, int *level);

// 09/09/2011, Send commands to device
SGFPM_DLL_DECL DWORD WINAPI	  SGFPM_WriteData(HSGFPM hFpm, unsigned char index, unsigned char data);

// 1/9/2015, Set/Get data 
SGFPM_DLL_DECL DWORD WINAPI	  SGFPM_SetGetData(HSGFPM hFpm, DWORD flag, void* data);

// 11/10/2015, InitEx2
SGFPM_DLL_DECL DWORD WINAPI	  SGFPM_InitEx2(HSGFPM hFpm, DWORD width, DWORD height, DWORD dpi, char* licenseFilePath);

// 8/25/2016, Get the number of minutiae
SGFPM_DLL_DECL DWORD WINAPI	  SGFPM_GetNumOfMinutiae(HSGFPM hFpm, WORD templateType, BYTE* minTemplate, DWORD *numOfMinutiae);


// 11.3.2017, jkang, Encode Raw image data to WSQ format
SGFPM_DLL_DECL DWORD WINAPI  SGFPM_EncodeWSQ(HSGFPM hFpm, BYTE ** wsqImageOut, DWORD *wsqImageOutSize, float wsqBitRate,  BYTE * rawImage, DWORD width, DWORD height, DWORD pixelDepth, DWORD ppi, char *commentText);

// 11.3.2017, jkangg, Decode WSQ image to raw image
SGFPM_DLL_DECL DWORD WINAPI  SGFPM_DecodeWSQ(HSGFPM hFpm, BYTE **rawImageOut, DWORD *width, DWORD *height, DWORD *pixelDepth, DWORD *ppi, DWORD *lossyFlag, BYTE *wsqImage, DWORD wsqImageLength);
	
// 11.3.2017, jkangg, Free image buffer inside dll. Should be called after EncodeToWSQ() or DecodeFromWSQ()
SGFPM_DLL_DECL DWORD WINAPI SGFPM_FreeWSQ(HSGFPM hFpm, BYTE *imgBuffer);

// 11.16.2017, JKang, Convert text data to binary. For coverting, base64 encoding/decoding is used.
SGFPM_DLL_DECL DWORD WINAPI SGFPM_ByteToText(HSGFPM hFpm, BYTE *data,  DWORD dwdatasize, 	LPTSTR textData);

// 11.16.2017, JKang, Convert text data to text. For coverting, base64 encoding/decoding is used.
SGFPM_DLL_DECL DWORD WINAPI SGFPM_TextToByte(HSGFPM hFpm, LPTSTR textData, BYTE *destdata, DWORD *pdatasize);

// 11/19/2019, Find devices with ID string
SGFPM_DLL_DECL DWORD WINAPI SGFPM_FindDevices(HSGFPM hFpm, DWORD* ndevs, SGDeviceInfo** devList, DWORD timeout);
SGFPM_DLL_DECL DWORD WINAPI SGFPM_CancelFind(HSGFPM hFpm);
SGFPM_DLL_DECL DWORD WINAPI SGFPM_OpenDevice2(HSGFPM hFpm, wchar_t *devId);

// 12/02/2019, Get a template from a device if available
SGFPM_DLL_DECL DWORD WINAPI SGFPM_CreateTemplateDev(HSGFPM hFpm, DWORD* size);
SGFPM_DLL_DECL DWORD WINAPI SGFPM_GetTemplateDev(HSGFPM hFpm, BYTE* min);
SGFPM_DLL_DECL DWORD WINAPI SGFPM_GetTemplateFormatDev(HSGFPM hFpm, WORD *format);
SGFPM_DLL_DECL DWORD WINAPI SGFPM_SetTemplateFormatDev(HSGFPM hFpm, WORD format);

// 7/14/2021, complementary functions of GetImage()
SGFPM_DLL_DECL DWORD WINAPI SGFPM_BeginGetImage(HSGFPM hFpm);
SGFPM_DLL_DECL DWORD WINAPI SGFPM_EndGetImage(HSGFPM hFpm);

// 7/14/2021, Get simple image quality for last image captured.
SGFPM_DLL_DECL DWORD WINAPI SGFPM_GetLastImageQuality(HSGFPM hFpm, DWORD* quality);

// 7/14/2021, Enable smart capture
SGFPM_DLL_DECL DWORD  WINAPI SGFPM_EnableSmartCapture(HSGFPM hFpm, bool enable);

#ifdef __cplusplus
};
#endif

#endif

