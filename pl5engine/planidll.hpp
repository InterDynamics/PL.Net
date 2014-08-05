#ifndef PLANIDLL_HPP
#define PLANIDLL_HPP

// system attributes the DLL can read (some can be set)

enum ePLSysInfo
 {
  PLSI_CLOCK,
  PLSI_ADVANCETOTIME,
  PLSI_CURRENTPENDING,
  PLSI_ENGINESTATE,
  PLSI_CURRENTFILEVERSION,
  PLSI_OLDESTFILEVERSION,
  PLSI_LOADEDFILEVERSION,
  PLSI_DLLVERSION
 };


// this enumerates the data object types

enum eDOTypes
 {
  PLDO_LABELS,
  PLDO_SUBLABELS,
  PLDO_ATTRIBUTE,
  PLDO_TABLE
 };

// error result meanings. Basically
// 0 is good, not 0 is bad except searches where
// >=0 is good, -1 means not found
//
// NOTE: Things are indexed from 0
//

enum ePLRESULT
 {
  PLR_NOTFOUND  = -1,
  PLR_OK        =  0,
  PLR_LOADFAIL,        // loading model failed
  PLR_INVALID,         // bad paramter, mode
  PLR_BADINDEX,        // bad row/col index
  PLR_BADFORMAT,       // badly formatted number string
  PLR_NOSPACE          // no space in buffer
 };

// version of the DLL API. You can retrieve the version of a DLL using
// PL_GetSstemInfo(PLSI_DLLVERSION);

#define PLANIDLL_VERSION        1

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

#define PLDLLFN _declspec(dllexport)


////////////////////////////////////////////////////////////////////
//
// The following typedefs define the functions exported by the DLL. The actual
// functions dont inlude the leading lowercase 't' in the function names.
//
// Typedefs are used rather than actually naming the functions as they
// will be more useful in creating function pointers, used with
// LoadLibary()/GetProcAddress().
//

// version of PL as a string eg: "5.25a"

typedef PLDLLFN const char * tPL_AppVersion();

typedef PLDLLFN void      tPL_SetInstance(HINSTANCE h);

// initialise PL.
//   cmdline can be a PL commandline option or just ""
//   inplace_window should be the handle of a window into which PL will
//   place a child window for its display. If NULL PL will create a new
//   window

typedef PLDLLFN PLRESULT  tPL_Init(const char * cmdline,
                                   HWND         inplace_window);

// close down model and cleanup/terminate. Must be called before parent window destroyed

typedef PLDLLFN void  tPL_Term();

// This enables loading a model; will fail if DLL is a compiled PBA

typedef PLDLLFN PLRESULT tPL_LoadModel(const char *);

// system info from ePLSysInfo
// Times are in seconds

typedef PLDLLFN double   tPL_GetSystemInfo(int sysinfoid);
typedef PLDLLFN PLRESULT tPL_SetSystemInfo(int sysinfoid,double v);

///////////////////////////////////////////////////////////////////////////////
//
// Data Object interface
//
// Data Objects are the data placed in the data objects label list in the model.
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

//////////////////////////////////////////////////////////////////////////////
//
// Broadcasting
//

typedef PLDLLFN int           tPL_BroadcastCount();
typedef PLDLLFN PLBroadcast * tPL_GetBroadcast(int w);
typedef PLDLLFN PLBroadcast * tPL_GetBroadcastName(const char * name);

// send broadcast to PL

typedef PLDLLFN PLRESULT      tPL_SendBroadcast(PLBroadcast *);
typedef PLDLLFN PLRESULT      tPL_SendBroadcastTuple(PLBroadcast *,
                                                     int           params,
                                                     const char ** names,
                                                     double      * values);

// send broadcast as a background event - PL processes it silently without
// mode change and sideeffects are processed even if paused with events at 
// the current time.

typedef PLDLLFN PLRESULT      tPL_SendBroadcastBG(PLBroadcast *);
typedef PLDLLFN PLRESULT      tPL_SendBroadcastTupleBG(PLBroadcast *,
                                                       int           params,
                                                       const char ** names,
                                                       double      * values);

// broadcast callback we can register

typedef PLRESULT CALLBACK tPL_BroadcastCallback(PLBroadcast *,
                                                int           params,
                                                const char ** names,
                                                double      * values);

// register a function for PL to call back

typedef PLDLLFN PLRESULT tPL_RegisterBroadcastCallback(PLBroadcast  *,
                                                       tPL_BroadcastCallback *);

//////////////////////////////////////////////////////////////////////////////
//  
// formatting and number parsing
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

#endif
