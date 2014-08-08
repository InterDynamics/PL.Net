#ifndef PLANIDLL_HPP
#define PLANIDLL_HPP

// Version of the DLL API. You can retrieve the version of a DLL using
// PL_GetSstemInfo(PLSI_DLLVERSION);

// v1: release
// v2: Jan 2013 thread support
// v3: jun 2013 new table functions
// v4: jan 2014 pause callback, s.ContinueRun access
// v5: aug 2014: thread setup now directly in PL

#define PLANIDLL_VERSION        5

// System attributes the DLL can [R]ead (some can be [W]ritten,
// some only persist/can be set while [E]ngine runs)

enum ePLSysInfo
 {
  // for PL_GetSystemInfo()

  PLSI_CLOCK,                    // R    : simulation seconds since start
  PLSI_ADVANCETOTIME,            // R/W  : time to fast-advance to
  PLSI_CURRENTPENDING,           // R    : events pending on the simulation FEC
  PLSI_ENGINESTATE,              // R    : ePLMode
  PLSI_CURRENTFILEVERSION,       // R    : version of MDL we would save
  PLSI_OLDESTFILEVERSION,        // R    : oldest version of MDL we can load 
  PLSI_LOADEDFILEVERSION,        // R    : current version of MDL we hve loaded
  PLSI_DLLVERSION,               // R    : version of dllapi PL compiled with
  PLSI_PAUSEAFTERADVANCE         // R/W/E: controls if PL pauses after advance-to-time
 };

// Major mode for Planimate engine

enum ePLMode
 {
  MD_OBJECT = 0,    // editing objects or user mode with engine stopped
  MD_FLOWEDIT,      // flow or interaction edit mode (flow editor has submode)
  MD_PAINT,         // editing paint object layer
  MD_SIMULATE,      // engine started and model is running
  MD_PAUSED         // engine started and model is paused
 };

// Data Object types

enum eDOTypes
 {
  PLDO_LABELS,
  PLDO_SUBLABELS,
  PLDO_ATTRIBUTE,
  PLDO_TABLE
 };

//
// Error results. Basically
// 0 is good, not 0 is bad except searches where
// >=0 is good, -1 means not found
// NOTE:In general objects in collections, rows and columns are indexed from 0
//

enum ePLRESULT
 {
  PLR_NOTFOUND  = -1,  // search fail
  PLR_OK        =  0,
  PLR_LOADFAIL,        // loading model failed
  PLR_INVALID,         // bad paramter, mode
  PLR_BADINDEX,        // bad row/col index
  PLR_BADFORMAT,       // badly formatted number string
  PLR_NOSPACE          // no space in buffer
 };

// This is a handle to a data object. The DLL user does not have access to
// its internals but it will be passed back to PL for data operations.
// It can represent a label list, sub label list, table or attribute.

class PLDataObject;

// This is a handle to a broadcast

class PLBroadcast;

// Base class for all label lists

class PLLabelList;

// Result code

typedef int PLRESULT;

// type for the exported functions

#ifndef __GNUC__
#define PLDLLFN _declspec(dllexport)
#else
#define PLDLLFN
#endif

// Thread proc status (PL_InitThread)
// This is different to the PL Run Engine State 
// NOTE:The PL Engine is only valid when thread is in PLT_Running state

enum ePLThreadState
 {
  PLT_None,
  PLT_LoadError,
  PLT_BindError,
  PLT_ThreadPending,
  PLT_ThreadStart,
  PLT_InitDone,
  PLT_Running,
  PLT_Terminating,
  PLT_ThreadEnd
 };

// Function enum for PL_GetProc() which you can use to retrieve function
// pointers instead of LoadLibrary

enum ePLProcs
 { 
  ePL_SetInstance,
  ePL_Init,
  ePL_Term,
  ePL_AppVersion,
  ePL_LoadModel,
  ePL_Run,
  ePL_GetSystemInfo,
  ePL_SetSystemInfo,
  ePL_DataObjectCount,
  ePL_GetDataObject,
  ePL_FindDataObjectName,
  ePL_FindDataObject,
  ePL_DataObjectType,
  ePL_DataObjectName,
  ePL_ListFromDataObject,
  ePL_GetNamedLabelList,
  ePL_LabelCount,
  ePL_GetLabelName,
  ePL_GetLabelIndex,
  ePL_FindLabelName,
  ePL_LookUpLabel,
  ePL_LookUpLValue,
  ePL_LookUpDValue,
  ePL_FindLabelAutoAdd,
  ePL_Rows,
  ePL_Columns,
  ePL_ColumnName,
  ePL_GetColumnFormat,
  ePL_SetColumnFormat,
  ePL_GetColumnLabels,
  ePL_FindColumn,
  ePL_TableResize,
  ePL_GetCell,
  ePL_SetCell,
  ePL_GetCellText, 
  ePL_GetColumn,
  ePL_SetColumn,
  ePL_InsertRow,
  ePL_DeleteRow,
  ePL_InsertColumn,
  ePL_DeleteColumn,
  ePL_BroadcastCount,
  ePL_GetBroadcast,
  ePL_GetBroadcastName,
  ePL_SendBroadcast,
  ePL_SendBroadcastTuple,
  ePL_RegisterBroadcastCallback,
  ePL_FormatModeCount,
  ePL_FormatName,
  ePL_StringToValue,
  ePL_ValueToString,
  ePL_GetWindow,
  ePL_Process,
  ePL_SuspendThread,
  ePL_ResumeThread,
  ePL_RegisterPauseCallback,
  ePL_SetCellText, 
  ePL_GetOwnerWindow,

  ePL_PROCCOUNT
 };

////////////////////////////////////////////////////////////////////
//
// The following typedefs define the functions exported by a PL DLL. The actual
// functions dont include the leading lowercase 't' in the function names.
//
// Typedefs are used rather than actually naming the functions as they
// will be more useful in creating function pointers, used with
// LoadLibary()/GetProcAddress().
//
// You can use these in conjunction with the enums above and PL_GetProc()
// A useful macro for C users is:
// #define GETPLPROC(pl,procname)  ((t##procname *)((pl->PL_GetProc)(e##procname)))
// 
//

// version of PL as a string eg: "5.36". Call any time
typedef PLDLLFN const char * tPL_AppVersion();

// Threaded mode initialise PL (new August 2014)
//  - cmdline can be a PL commandline option or just "". "/debugfile" writes log.
//  - inplace_window should be the handle of a window into which PL will
//    place a child window for its display. If NULL PL will create a new
//    window
//
// Call to initialise. PL is started in its own thread. This returns
// immediately so you can start other instances. You must call 
// PL_WaitThreadRunning() after PL_InitThread() before doing anything else
//

typedef PLDLLFN int tPL_InitThread(HMODULE,
                                   const char * the_args,
                                   HWND         the_owner_hwnd);

// PL_WaitThreadRunning() must be called after PL_InitThread()

typedef PLDLLFN int tPL_WaitThreadRunning();

// PL_TermThread() must be used to terminate when PL_InitThread() is used

typedef PLDLLFN void tPL_TermThread();

// PL_GetPRoc() can be used to retrieve proc pointers. It is ONLY valid
// after PL_InitThread() has been called

typedef PLDLLFN void * tPL_GetProc(int);

///////////////////////////////////////////////////////////////////////////////
//
// Direct initialisation without using threads or managing
// threads externally. This is now obsolete but still works for this release
//
// invoke PL_SetInstance before PL_Init() to set the instance. Not necessary if
// using PL_InitThread()

typedef PLDLLFN void      tPL_SetInstance(HINSTANCE h);

enum ePLINITOptions
 {
  PLINIT_NONE      = 0,
  PLINIT_OWNTHREAD = 1
 };

typedef PLDLLFN PLRESULT  tPL_Init(const char * cmdline,
                                   HWND         inplace_window,
                                   int          opts);

// close down model and cleanup/terminate.
// Only call after PL_Init() has returned.
// Must be called before parent window destroyed

typedef PLDLLFN void  tPL_Term();

// Windows event dispatch loop. Useful when PL running in own thread.
// Returns when WM_QUIT shuts down the message loop.
// NOTE:Models in a threaded DLL should not be configured to self-close

typedef PLDLLFN PLRESULT tPL_Process();

///////////////////////////////////////////////////////////////////////////////
//
// General purpose calls
//


// PL_Run() engine control
// - only call after PL_Init() has completed 
// - OK to call from main thread (it posts a message to PL's thread)
// - do not use within PL_SuspendThread/PL_ResumeThread calls

enum ePLRunCMD
 {
  PLRUNCMD_Close     = -1,      // end simulation and close down (PL_Run() returns)
  PLRUNCMD_Stop       = 0,       // stop the run engine
  PLRUNCMD_Run        = 1,       // start engine/run (or continu) the model. Pauses if no events
  PLRUNCMD_Pause      = 2,       // pause the model
  PLRUNCMD_StartPause = 3        // start engine and pause (no-op if already running)
 };

typedef PLDLLFN PLRESULT tPL_Run(int runcmd);

// retrieve window handle to PL main window. This might be useful sometime

typedef PLDLLFN HWND     tPL_GetWindow();

// retrieve parent window to PL main window (what was passed)

typedef PLDLLFN HWND     tPL_GetOwnerWindow();

///////////////////////////////////////////////////////////////////////
//
// suepend/resume PL thread when PL is running in its own thread
// A suspended PL thread goes into a windows message dispatch loop
// keeping it away from access to PL structures
//
// PL_SuspendThread blocks (pumping calling thread messages) until PL is
// in a suspended state.
//
// NOTE: It is important your UI does not feed new commands while
//       this wait occurs. The calls here are not re-entrant.
//

typedef PLDLLFN void     tPL_SuspendThread();
typedef PLDLLFN void     tPL_ResumeThread();


//////////////////////////////////////////////////////////////////////////////
//
// Thread Notes:
//
// When running PL in its own thread, the following must be enclosed
// in PL_SuspendThread() / PL_ResumeThread() or data corruption
// can be expected.
// 

// PL_LoadModel enables loading a model; will fail if:
// Direct mode:
// - PL is a compiled PBA
// - model run is ac5ive/paused
// Threaded mode:
// - Will terminate any running model
// - then loads the named model (or the PBA is reloaded if PL is a compiled PBA)
//   filename must be non NULL
//   loadfile, if non NULL sets s.LastLoadFilePath, useful for loading a dataset

typedef PLDLLFN PLRESULT tPL_LoadModel(const char * modelname,const char * loadfile);

// system info from ePLSysInfo
// Times are in seconds with 0 = the run start date 

typedef PLDLLFN double   tPL_GetSystemInfo(int sysinfoid);
typedef PLDLLFN PLRESULT tPL_SetSystemInfo(int sysinfoid,double v);

///////////////////////////////////////////////////////////////////////////////
//
// Data Object interface
//
// Data Objects are the data placed in the _data_objects label list in the model.
// They can be tables, label lists, attributes and sub label lists.
//

typedef PLDLLFN int            tPL_DataObjectCount();
typedef PLDLLFN PLDataObject * tPL_GetDataObject(int w);
typedef PLDLLFN PLDataObject * tPL_FindDataObjectName(const char *);
typedef PLDLLFN PLDataObject * tPL_FindDataObject(long index);
typedef PLDLLFN int            tPL_DataObjectType(PLDataObject*); // eDOTypes
typedef PLDLLFN const char *   tPL_DataObjectName(PLDataObject*);

//////////////////////////////////////////////////////////////////////////////
//
// For label list data objects
//

typedef PLDLLFN PLLabelList * tPL_ListFromDataObject(PLDataObject *);
typedef PLDLLFN PLLabelList * tPL_GetNamedLabelList(const char *);

// for a label list:

typedef PLDLLFN int          tPL_LabelCount(PLLabelList *);
typedef PLDLLFN const char * tPL_GetLabelName(PLLabelList *,int w);
typedef PLDLLFN long         tPL_GetLabelIndex(PLLabelList *,int w);
typedef PLDLLFN int          tPL_FindLabelName(PLLabelList *,const char *);
typedef PLDLLFN long         tPL_LookUpLabel(PLLabelList *,const char *);
typedef PLDLLFN const char * tPL_LookUpLValue(PLLabelList *,long v);
typedef PLDLLFN const char * tPL_LookUpDValue(PLLabelList *,double v);
typedef PLDLLFN int          tPL_FindLabelAutoAdd(PLLabelList *,const char *);

//////////////////////////////////////////////////////////////////////////////
//
// For portal attributes
//

typedef PLDLLFN int          tPL_GetAttFormat(PLDataObject *);
typedef PLDLLFN PLLabelList* tPL_GetAttLabels(PLDataObject *);
typedef PLDLLFN double       tPL_GetAttValue(PLDataObject *);
typedef PLDLLFN PLRESULT     tPL_SetAttValue(PLDataObject *,double data);
typedef PLDLLFN const char * tPL_GetAttText(PLDataObject *);
typedef PLDLLFN PLRESULT     tPL_SetAttText(PLDataObject *,const char *);

//////////////////////////////////////////////////////////////////////////////
//
// For Table data objects:
//

typedef PLDLLFN int           tPL_Rows(PLDataObject *);
typedef PLDLLFN int           tPL_Columns(PLDataObject *);
typedef PLDLLFN const char *  tPL_ColumnName(PLDataObject *,int column);
typedef PLDLLFN int           tPL_GetColumnFormat(PLDataObject *,int column);
typedef PLDLLFN PLRESULT      tPL_SetColumnFormat(PLDataObject *,int column,int format,PLLabelList *);
typedef PLDLLFN PLLabelList * tPL_GetColumnLabels(PLDataObject *,int column);
typedef PLDLLFN int           tPL_FindColumn(PLDataObject *,const char * col_name);

// Resize table - r/c can be -1 to leave existing as-is. Returns 0 on success

typedef PLDLLFN PLRESULT     tPL_TableResize(PLDataObject *,int r,int c);

typedef PLDLLFN double       tPL_GetCell(PLDataObject *,int row,int col);
typedef PLDLLFN PLRESULT     tPL_SetCell(PLDataObject *,int row,int col,double data);

// for free text access. Text returned only valid until the table is changed

typedef PLDLLFN const char * tPL_GetCellText(PLDataObject *,int row,int col);
typedef PLDLLFN PLRESULT     tPL_SetCellText(PLDataObject *,int row,int col,const char *);

// entire column set

typedef PLDLLFN PLRESULT     tPL_GetColumn(PLDataObject *,int col,int rows,double * into);
typedef PLDLLFN PLRESULT     tPL_SetColumn(PLDataObject *,int col,int rows,double * to);

typedef PLDLLFN PLRESULT     tPL_InsertRow(PLDataObject *,int at_row,int count);
typedef PLDLLFN PLRESULT     tPL_DeleteRow(PLDataObject *,int at_row,int count);
typedef PLDLLFN PLRESULT     tPL_InsertColumn(PLDataObject *,int at_row,int count);
typedef PLDLLFN PLRESULT     tPL_DeleteColumn(PLDataObject *,int at_row,int count);


//////////////////////////////////////////////////////////////////////////////
//
// Broadcasting
//

typedef PLDLLFN int           tPL_BroadcastCount();
typedef PLDLLFN PLBroadcast * tPL_GetBroadcast(int w);
typedef PLDLLFN PLBroadcast * tPL_GetBroadcastName(const char * name);

// send broadcast to PL
//
// Thread Notes:
//
// When PL is running in its own thread broadcasts are always
// enqueued to the model FEC. This means they do not occur immediately
// when you call the API and will be processed when the thread lock is
// released and the run is resumed.
//

typedef PLDLLFN PLRESULT      tPL_SendBroadcast(PLBroadcast *);
typedef PLDLLFN PLRESULT      tPL_SendBroadcastTuple(PLBroadcast *,
                                                     int           params,
                                                     const char ** names,
                                                     double      * values);

// send broadcast as a background event - PL processes it silently without
// mode change and sideeffects are processed even if paused with events at 
// the current time. Do not perform any UI with these, they execute
// in the caller thread.

typedef PLDLLFN PLRESULT      tPL_SendBroadcastBG(PLBroadcast *);
typedef PLDLLFN PLRESULT      tPL_SendBroadcastTupleBG(PLBroadcast *,
                                                       int           params,
                                                       const char ** names,
                                                       double      * values);

// broadcast callback 
//
// Thread Notes:
//
// The callback will be called in the context of PL's thread.
// The PLBroadcast handle is unique per broadcast per thread.
// The userdata will be returned and is useful for passing a pointer
// to the class/instance.
//
// Data *must* be processed/copied before returning from this function.
// All pointers are invalid after the callback returns.

typedef PLRESULT CALLBACK tPL_BroadcastCallback(PLBroadcast *,
                                                int           params,
                                                const char ** names,
                                                double      * values,
                                                void        * userdata);

// register a function for PL to call back

typedef PLDLLFN PLRESULT tPL_RegisterBroadcastCallback(PLBroadcast  *,
                                                       tPL_BroadcastCallback *,
                                                       void * userdata);

//////////////////////////////////////////////////////////////////////////////
//  
// Pause callback enables a user provided function to be called
// every time Planimate becomes paused. This function is called in PL's
// thread context so do as little as possible and message to your main
// thread as required.
//                                    
// Reasons the run engine becomes paused

enum ePLPauseReason
 {
  SIMUL_UserPause = 0,      // user pressed ESC, mouse button, may resume
  SIMUL_EndTimeReached,     // nominated end time reached, user may extend
  SIMUL_NoMoreEvents,       // FEC empty, user may trigger new activity
  SIMUL_SimulateError,      // error has occured and was reported, must End()
  SIMUL_Finished,           // model has set finished state (eg:Exit), must End()
  SIMUL_AdvanceTimeReached, // advance to time reached and pause after advance set
  SIMUL_RunMemoryError,     // out of memory during run      
  SIMUL_Undefined
 };

typedef PLRESULT CALLBACK tPL_PauseCallback(double  the_time,
                                            int     stop_reason,  // ePLPauseReason
                                            void  * userdata);

typedef PLDLLFN void     tPL_RegisterPauseCallback(tPL_PauseCallback * fn,
                                                   void              * userdata);

//////////////////////////////////////////////////////////////////////////////
//  
// formatting and number parsing
// Also must be called within PL_SuspendThread / PL_ReleaseThread and
// they are not re-entrant
//

typedef PLDLLFN int           tPL_FormatModeCount();
typedef PLDLLFN const char *  tPL_FormatName(int fmt);
typedef PLDLLFN PLRESULT      tPL_StringToValue(const char * s,
                                                double     * v,
                                                int          format);

typedef PLDLLFN PLRESULT      tPL_ValueToString(double       v,
                                                int          buffer_len,
                                                char       * buffer,
                                                int          format);


//
// PL uses this to convert doubles to integers. It provide rounding to the
// nearest integer for both +ve and -ve values
//

inline long  PL_NearestInt(double v) 
{
 // return closest int to v, handling C's -ve number round behaviour
 return (long)(v + 0.5 - (v < 0.0));
}

#endif
