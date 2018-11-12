/* Copyright 2009 Craig Chandler
 * 
 * 
 * This file is part of PL.Net.
 * 
 * PL.Net is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * PL.Net is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public License
 * along with pl5engine.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Data;
using System.Text;

namespace Planimate.Engine
{
  #region PL.Net enums
  /// <summary>
  /// Planimate® system attributes for GetSystemInfo()
  /// </summary>
  public enum ePLSysInfo
  {
    /// <summary>This is the current simulation time as indicated by the system clock</summary>
    PLSI_CLOCK,
    /// <summary>Used to initiate or stop a fast advancement.
    /// Set the attribute to the time you wish to advance to</summary>
    PLSI_ADVANCETOTIME,
    /// <summary>Events pending on FEC</summary>
    PLSI_CURRENTPENDING,
    /// <summary>Engine initialisation state</summary>
    PLSI_ENGINESTATE,
    /// <summary>Model file version number we save</summary>
    PLSI_CURRENTFILEVERSION,
    /// <summary>Oldest model version we accept</summary>
    PLSI_OLDESTFILEVERSION,
    /// <summary>Loaded model version</summary>
    PLSI_LOADEDFILEVERSION,
    /// <summary>Returns the version of the engine (not the core Planimate® version)</summary>
    PLSI_DLLVERSION,
    /// <summary>R/W/E: controls if PL pauses after advance-to-time</summary>
    PLSI_PAUSEAFTERADVANCE,
    /// <summary>Returns offset of date-times to 1-Jan-1970</summary>
    PLSI_UNIXTIMEOFFSET
  };

  /// <summary>
  /// Major mode for Planimate® engine
  /// </summary>
  enum ePLMode
  {
    /// <summary>Editing objects or user mode with engine stopped</summary>
    MD_OBJECT = 0,    
    /// <summary>Editing flows</summary>
    MD_FLOWEDIT,
    /// <summary>Editing paint</summary>
    MD_PAINT, 
    /// <summary>Simulation running</summary>
    MD_SIMULATE,
    /// <summary>Model in run mode, Simulation paused</summary>
    MD_PAUSED
  };

  /// <summary>
  /// Planimate® Data Object types
  /// </summary>
  public enum eDOTypes
  {
    /// <summary>Label List</summary>
    PLDO_LABELS,
    /// <summary>Sub Label List</summary>
    PLDO_SUBLABELS,
    /// <summary>Portal Attribute</summary>
    PLDO_ATTRIBUTE,
    /// <summary>Table</summary>
    PLDO_TABLE
  };

  /// <summary>
  /// Planimate® engine return results, returned by many calls
  /// </summary>
  public enum ePLRESULT
  {
    /// <summary>Not found</summary>
    PLR_NOTFOUND = -1,
    /// <summary>Return was OK</summary>
    PLR_OK = 0,
    /// <summary>Loading model failed</summary>
    PLR_LOADFAIL,
    /// <summary>Bad paramter / mode</summary>
    PLR_INVALID,
    /// <summary>Bad row / col index</summary>
    PLR_BADINDEX,
    /// <summary>Badly formatted number string</summary>
    PLR_BADFORMAT,
    /// <summary>No room in buffer</summary>
    PLR_NOSPACE
  };

  /// <summary>
  /// Planimate® value formats, eg: for convertStringToPLValue
  /// </summary>
  public enum eTFUnit
  {
    /// <summary>time W D HH:MM SS</summary>
    UNIT_TIME,
    /// <summary>Plain value</summary>
    UNIT_VALUE,
    /// <summary>$x.xx</summary>
    UNIT_MONEY,
    /// <summary>rate/s</summary>
    UNIT_RATESEC,
    /// <summary>rate/m</summary>
    UNIT_RATEMIN,
    /// <summary>rate/h</summary>
    UNIT_RATEHOUR,
    /// <summary>rate/d</summary>
    UNIT_RATEDAY,
    /// <summary>rate/w</summary>
    UNIT_RATEWEEK,
    /// <summary>rate/M</summary>
    UNIT_RATEMONTH,
    /// <summary>rate/y</summary>
    UNIT_RATEYEAR,
    /// <summary>calendar HH:MM SS</summary>
    UNIT_ABSTIME,
    /// <summary>label list</summary>
    UNIT_LABEL,
    /// <summary>$x</summary>
    UNIT_MONEY_U,
    /// <summary>calendar HHMM</summary>
    UNIT_ABSTIME2,
    /// <summary>value with commas</summary>
    UNIT_VALUESPACED,
    /// <summary>Time of day HHMM</summary>
    UNIT_TIMEOFDAY,
    /// <summary>Time of day 12hr</summary>
    UNIT_TIMEAMPM,
    /// <summary>hexadecimal</summary>
    UNIT_HEX,
    /// <summary>Time W D HH:MM:SS</summary>
    UNIT_TIME2,
    /// <summary>calendar HH:MM:SS</summary>
    UNIT_ABSTIME3,
    /// <summary>Time minutes</summary>
    UNIT_MINUTES,                         // 20
    /// <summary>Time hours</summary>
    UNIT_HOURS,
    /// <summary>Time days</summary>
    UNIT_DAYS,
    /// <summary>Time weeks</summary>
    UNIT_WEEKS,
    /// <summary>fixed xxxx.x</summary>
    UNIT_VALUE_F1,
    /// <summary>fixed xxxx.xx</summary>
    UNIT_VALUE_F2,
    /// <summary>fixed x,xxx.x</summary>
    UNIT_VALUE_CF1,
    /// <summary>fixed x,xxx.xx</summary>
    UNIT_VALUE_CF2,
    /// <summary>Date only</summary>
    UNIT_DATEONLY,
    /// <summary>jan 3 feb, 2000</summary>
    UNIT_DAYDATE,
    /// <summary>Time W D HH:MM (Week/Day from 1)</summary>
    UNIT_WEEKDAY1,
    /// <summary>right justified zero filled 00XXX</summary>
    UNIT_VALUE_RJZ,
    /// <summary>US date HH:MM:SS</summary>
    UNIT_ABSTIME3U,
    /// <summary>Longitude DD:MM:SS[E|W]</summary>
    UNIT_LONG_EW,
    /// <summary>Longitude [-]DD:MM:SS.SS</summary>
    UNIT_LONG_PRECISE,
    /// <summary>Latitude DD:MM:SS[N|S]</summary>
    UNIT_LAT_NS,
    /// <summary>Latitude [-]DD:MM:SS.SS</summary>
    UNIT_LAT_PRECISE,
    /// <summary>Time of day HH:MM</summary>
    UNIT_TIMEOFDAY_2,
    /// <summary>Calendar YYYY-MM-DD</summary>
    UNIT_DATE2,
    /// <summary>(free text)</summary>
    UNIT_FREETEXT,
    /// <summary>Rand currency Rx.xx</summary>
    UNIT_MONEY_R,                            // 40
    /// <summary>Rand currency no cents Rx</summary>
    UNIT_MONEY_RU,
    /// <summary>Euro currency</summary>
    UNIT_MONEY_E,
    /// <summary>Euro currency no cents</summary>
    UNIT_MONEY_EU,
    /// <summary>Time HH:MM (no seconds)</summary>
    UNIT_TIME3,
    /// <summary>Calendar HH:MM:SS.SSS</summary>
    UNIT_ABSTIMEMS,
    /// <summary>Calendar YYYY-MM-DD HH:MM:SS</summary>
    UNIT_DATE2TIME,
    /// <summary>Time Day HHMM  Mon 1234</summary>
    UNIT_DAYHHMM,
    /// <summary>Time Day HHMM  1d 1234, 7d 2359</summary>
    UNIT_DAY2HHMM,
    /// <summary>As percentage with % symbol</summary>
    UNIT_PERCENT,
    /// <summary>As percentage with variable fractions</summary>
    UNIT_PERCENT2,
    /// <summary>calendar YYYYMMDD</summary>
    UNIT_DATE3,
    /// <summary>scientific eg: 1.2345e-002</summary>
    UNIT_SCIENTIFIC,
    /// <summary>C-style DAY MMM DD HH:MM:SS YYYY</summary>
    UNIT_ABSTIMEC,
    /// <summary>As percentage x.xxx%</summary>
    UNIT_PERCENT3,
    /// <summary>Parts per million</summary>
    UNIT_PPM,
    /// <summary>as percent without symbol</summary>
    UNIT_PERCENT_NS,
    /// <summary>as ppm without symbol</summary>
    UNIT_PPM_NS,
    /// <summary>As percentage x.xxxxxx%</summary>
    UNIT_PERCENT6,
    /// <summary>value spaced without decimal</summary>
    UNIT_VALUESPACED2,
    /// <summary>Calendar dd MMM (no year)</summary>
    UNIT_DATE4,
    /// <summary>Calendar dd MMM HHMM (no year)</summary>
    UNIT_DATE5,
    /// <summary>Time span xxhr xxmin</summary>
    UNIT_HOURMIN,
    /// <summary>Operating System Locality Date Time format</summary>
    UNIT_OS_DATETIME,
    /// <summary>Operating System Locality Date format</summary>
    UNIT_OS_DATEONLY,
    /// <summary>Operating System Locality Time format</summary>
    UNIT_OS_TIME,
    /// <summary>Operating System Locality Currency format</summary>
    UNIT_OS_CURRENCY,
    /// <summary>Operating System Locality Value format</summary>
    UNIT_OS_VALUE,
    /// <summary>Operating System Locality Percentage format</summary>
    UNIT_OS_PERCENT,
    /// <summary>RGB Colour format</summary>
    UNIT_RGB,
    /// <summary>Value with no decimal</summary>
    UNIT_VALUENODEC,
    /// <summary>Fixed value xxxx.xxx</summary>
    UNIT_VALUE_F3,
    /// <summary>Fixed value x,xxx.xxx</summary>
    UNIT_VALUE_CF3,
    /// <summary>Percentage value x.x%</summary>
    UNIT_PERCENT1,
    /// <summary>Calendar YYYY-MM-DD HHMMSS</summary>
    UNIT_DATE3TIME,
    /// <summary>Time D HH:MM</summary>
    UNIT_TIMEDHHMM,
    /// <summary>Time D 0H:MM (hour always 2 digits</summary>
    UNIT_TIMED0HMM,
    /// <summary>Time D HH:MM  1d 12:34, 7d 23:59 (day from 1d)</summary>
    UNIT_DAY2HHMMCOLON,
    /// <summary>week and day from 1</summary>
    UNIT_WEEKDAYNOTIME, 
    /// <summary>Day from 1</summary>
    UNIT_DAYFROM1,
    /// <summary>Calendar yyyymmddhhmmss</summary>
    UNIT_DATE4TIME,
    /// <summary>Time minimal HH:MM if both zero</summary>
    UNIT_TIMEMINIMAL,
    /// <summary>Calendar D Mmm Y hh:mm</summary>
    UNIT_ABSTIME2C,
    /// <summary>Dotted Calendar Time</summary>
    UNIT_ABSTIMEDOT,
    /// <summary>Time hhh:mm</summary>
    UNIT_HOURMIN2,
    /// <summary>Calendar YYYY-Mmm-Dd-HH:MM:SS</summary>
    UNIT_DATE5TIME,
    /// <summary>#AARRGGBB</summary>
    UNIT_ARGB,
    /// <summary>Scaled Kilo x,xxx</summary>
    UNIT_SCALEDKILO_F0,
    /// <summary>Scaled Kilo x,xxx.x</summary>
    UNIT_SCALEDKILO_F1,
    /// <summary>Scaled Kilo x,xxx.xx</summary>
    UNIT_SCALEDKILO_F2,
    /// <summary>Calendar US date HH:MM</summary>
    UNIT_ABSTIME3US,    
    /// <summary>Calendar YYYY-MM-DD HH:MM</summary>
    UNIT_DATE2TIMES,
    /// <summary>Class instance reference</summary>
    UNIT_INSTANCE,
    /// <summary>calendar yyyymmdd hh:mm:ss</summary>
    UNIT_DATE6TIME,
    /// <summary>calendar  DD/Mmm/YYYY hh:mm:ss</summary>
    UNIT_ABSTIMESLASH, 
    
    /// <summary>this counts unit modes</summary>
    UNIT_MODECOUNT,
    /// <summary>special case - must be 255 and last</summary>
    UNIT_NULL = 255
  };

  /// <summary>
  /// Planimate® format classifiers
  /// </summary>
  public enum eUnitClass
  {
    /// <summary>numerical value</summary>
    TUF_VAL,
    /// <summary>currency value</summary>
    TUF_CUR,
    /// <summary>relative time / time interval</summary>
    TUF_TIME,
    /// <summary>relative time with scaled values</summary>
    TUF_STIME,  // relative time with scaled values
    /// <summary>time of day (clamped 0..<...86400)</summary>
    TUF_TOD,    // time-of-day (clamped time)
    /// <summary>calendar date</summary>
    TUF_DATE,   // calendar date
    /// <summary>rate (1 / time)</summary>
    TUF_RATE,
    /// <summary>value which is scaled on display/parse</summary>
    TUF_SVAL,   // value scaled
    /// <summary>percentage value</summary>
    TUF_PC,     // percentage
    /// <summary>other type (text, label, colour etc</summary>
    TUF_OTH     // other basic type
  };
  
  /// <summary>
  /// Thread proc status for Planimate DLL loader class
  /// This is different to the PL Run Engine State 
  /// NOTE:The PL Engine is only valid when thread is in PLT_Running state
  /// NOTE:Obsolete as now InitThread is used and this is managed in PL
  /// </summary>
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

  /// <summary>
  /// Function enum for PL_GetProc(). We wrap all functions so no need to
  /// expose this.
  /// </summary>
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
    ePL_WaitModelStarted,

    // v6
    ePL_AddTableChangeCallback,
    ePL_RemoveTableChangeCallback,

    // v7
    ePL_GetColumnPtr,
    ePL_UpdateDependencies,

    // v8
    ePL_ValueToColor,
    ePL_GetCellFormatted,

    // v9
    ePL_GetFormatClass,
    ePL_ValueToARGB,

    // v10
    ePL_CreateTable,
    ePL_DeleteTable,
    ePL_InsertColumnNamed,
    
    //
    ePL_PROCCOUNT
  };
  
  /// <summary>
  /// Run commands for use within the PL_Run() call. Not for use when Planimate
  /// calls dotNET DLL.
  /// </summary>
  public enum ePLRunCMD
  {
    /// <summary>
    /// End simulation and close down (PL_Run() returns)
    /// </summary>
    PLRUNCMD_Close = -1,
    /// <summary>
    /// Stop the run engine
    /// </summary>
    PLRUNCMD_Stop = 0,
    /// <summary>
    /// Start engine/run (or continue) the model. Pauses if no events
    /// </summary>
    PLRUNCMD_Run = 1,
    /// <summary>
    /// Pause the model
    /// </summary>
    PLRUNCMD_Pause = 2,
    /// <summary>
    /// Start engine and pause (no-op if already running)
    /// </summary>
    PLRUNCMD_StartPause = 3
  };

  /// <summary>
  /// Pause callback enables a user provided function to be called
  /// every time Planimate becomes paused. This function is called in PL's
  /// thread context so do as little as possible and message to your main
  /// thread as required.
  ///                                    
  /// Reasons the run engine becomes paused are listed in the ePLPauseReason enum
  /// </summary>
  public enum ePLPauseReason
  {
    /// <summary>
    /// User pressed ESC, mouse button, may resume
    /// </summary>
    SIMUL_UserPause = 0,
    /// <summary>
    /// Nominated end time reached, user may extend
    /// </summary>
    SIMUL_EndTimeReached,
    /// <summary>
    /// FEC empty, user may trigger new activity
    /// </summary>
    SIMUL_NoMoreEvents,
    /// <summary>
    /// Error has occured and was reported, must End()
    /// </summary>
    SIMUL_SimulateError,
    /// <summary>
    /// Model has set finished state (eg:Exit), must End()
    /// </summary>
    SIMUL_Finished,
    /// <summary>
    /// Advance to time reached and pause after advance set
    /// </summary>
    SIMUL_AdvanceTimeReached,
    /// <summary>
    /// Out of memory during run
    /// </summary>
    SIMUL_RunMemoryError,
    /// <summary>
    /// Breakpoint hit in model
    /// </summary>
    SIMUL_BreakPointStop,
    /// <summary>
    /// Undefined reason.
    /// </summary>
    
    SIMUL_Undefined
  };

  /// <summary>
  /// Table change callback cmd field
  /// </summary>
  public enum eTableChangeCommand
  {
    /// <summary>
    /// Data changed but size same   
    /// </summary>
    PLTCC_DATACHANGE,
    /// <summary>
    /// Table size has changed (rows or columns)
    /// </summary>
    PLTCC_RESIZED,
    /// <summary>
    /// Table is being destroyed, forget about its callback handle as returned
    /// by AddTableCallback. Its dataobject handle will also now be invalid.
    /// </summary>
    PLTCC_TABLEDELETED
  };
  #endregion

  /// <summary>This exception is thrown if a proc fails to bind during init or use.
  /// This would suggest an older version of PL than the enum's here. A newer
  /// version of PL is Ok.</summary>
  public class PLBindFailure : System.Exception {}

  /// <summary>
  /// This implements communication with Planimate using callbacks and works
  /// both with Planimate being a called DLL or Planimate being the caller to
  /// dotNET.
  /// </summary>
  public class PLEngineCore
  {
    #region kernel32 import
    [DllImport("kernel32.dll")]
    private static extern IntPtr LoadLibrary(string dllToLoad);
    private const int PLMaxString = 65536;

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

    [DllImport("kernel32.dll")]
    private static extern bool FreeLibrary(IntPtr hModule);
    #endregion
    
    #region PLLoader function delegations
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate int tPL_InitThread(IntPtr dll_handle,
                                        [MarshalAs(UnmanagedType.LPStr)] string cmdline,
                                        IntPtr inplace_window);

    // PL_WaitThreadRunning() must be called after PL_InitThread()
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate int tPL_WaitThreadRunning();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate IntPtr tPL_TermThread();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate IntPtr tPL_GetProc(ePLProcs function);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate void tPL_SuspendThread();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate void tPL_ResumeThread();
    #endregion

    #region PLEngineCore function delegations
    // Can be called anytime
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate IntPtr tPL_AppVersion();

    //////////////////////////////////////////////////////////////////////////////
    //
    // Thread Notes:
    //
    // When running PL in its own thread, the following must be enclosed
    // in PL_SuspendThread() / PL_ResumeThread() or data corruption
    // can be expected.
    // 
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate ePLRESULT tPL_Run(ePLRunCMD runcmd);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate ePLRESULT tPL_LoadModel([MarshalAs(UnmanagedType.LPStr)] string modelname,
                                             [MarshalAs(UnmanagedType.LPStr)] string loadfile);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate double tPL_GetSystemInfo(ePLSysInfo sysinfo_id);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate ePLRESULT tPL_SetSystemInfo(ePLSysInfo sysinfo_id, double value);

    /// <summary> Data Object interface
    ///
    /// Data Objects are the data placed in the data objects label list in the model.
    /// They can be tables, label lists, attributes and sub label lists.
    /// </summary>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate int tPL_DataObjectCount();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate IntPtr tPL_GetDataObject(int w);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate IntPtr tPL_FindDataObjectName([MarshalAs(UnmanagedType.LPStr)] string DO_name);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate IntPtr tPL_FindDataObject(int index);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate eDOTypes tPL_DataObjectType(IntPtr dataobject);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate IntPtr tPL_DataObjectName(IntPtr dataobject);

    #region For label list data objects
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate IntPtr tPL_ListFromDataObject(IntPtr dataobject);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate IntPtr tPL_GetNamedLabelList([MarshalAs(UnmanagedType.LPStr)] string list_name);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate int tPL_LabelCount(IntPtr labellist);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate IntPtr tPL_GetLabelName(IntPtr labellist, int order);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate Int32 tPL_GetLabelIndex(IntPtr labellist, int order);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate int tPL_FindLabelName(IntPtr labellist, [MarshalAs(UnmanagedType.LPStr)] string label);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate Int32 tPL_LookUpLabel(IntPtr labellist, [MarshalAs(UnmanagedType.LPStr)] string label);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate IntPtr tPL_LookUpLValue(IntPtr labellist, Int32 index);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate IntPtr tPL_LookUpDValue(IntPtr labellist, double index);
    #endregion

    #region For Table data objects:
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate int tPL_Rows(IntPtr dataobject);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate int tPL_Columns(IntPtr dataobject);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate IntPtr tPL_ColumnName(IntPtr dataobject, int column);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate eTFUnit tPL_GetColumnFormat(IntPtr dataobject, int column);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate IntPtr tPL_GetColumnLabels(IntPtr dataobject, int column);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate int tPL_FindColumn(IntPtr dataobject,
                                        [MarshalAs(UnmanagedType.LPStr)] string col_name);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate ePLRESULT tPL_TableResize(IntPtr dataobject, int rows, int cols);

    // TODO:Att methods missing from proc table
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate double    tPL_GetAttValue(IntPtr dataobject);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate ePLRESULT tPL_SetAttValue(IntPtr dataobject, double v);
        
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate double tPL_GetCell(IntPtr dataobject, int row, int col);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate ePLRESULT tPL_SetCell(IntPtr dataobject, int row, int col, double data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate IntPtr tPL_GetCellText(IntPtr dataobject, int row, int col);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate ePLRESULT tPL_SetCellText(IntPtr dataobject, int row, int col, [MarshalAs(UnmanagedType.LPStr)] string data);

    //Entire Column operations
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate ePLRESULT tPL_GetColumn(IntPtr dataobject, int column, int rows, double[] into);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate ePLRESULT tPL_SetColumn(IntPtr dataobject, int column, int rows, double[] to);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate ePLRESULT tPL_InsertColumn(IntPtr dataobject, int atCol,int count);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate ePLRESULT tPL_InsertColumnNamed(IntPtr dataobject, int atCol,int units,[MarshalAs(UnmanagedType.LPStr)]string name,IntPtr labels);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate ePLRESULT tPL_DeleteRow(IntPtr dataobject, int row,int for_rows);
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate IntPtr tPL_GetOwnerWindow();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate ePLRESULT tPL_WaitModelStarted(int timeout);

    /// <summary>Table changed callback function definition</summary>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ePLRESULT tPL_TableChangeCallback(IntPtr dataobject,
                                                      int r,
                                                      int c,
                                                      eTableChangeCommand commands);

    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate int tPL_AddTableChangeCallback(tPL_TableChangeCallback callback,IntPtr dataobject);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate void tPL_RemoveTableChangeCallback(int pl_cb_handle,IntPtr dataobject);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate eUnitClass tPL_GetFormatClass(eTFUnit format);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate int tPL_CreateTable([MarshalAs(UnmanagedType.LPStr)] string name,int scope);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate int tPL_DeleteTable(int tableDO);

    #endregion

    #region Broadcasts
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate IntPtr tPL_GetBroadcastName([MarshalAs(UnmanagedType.LPStr)] string broadcast_name);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate ePLRESULT tPL_SendBroadcast(IntPtr broadcast);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate ePLRESULT tPL_SendBroadcastTuple(IntPtr broadcast, int no_params,
                                                      [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 1)] string[] tuple_names,
                                                      [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] double[] tuple_values);

    /// <summary>Planimate® broadcast callback function definition</summary>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ePLRESULT tPL_BroadcastCallback(IntPtr broadcast,
                                                    int no_params,
                                                    [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr,
                                                               SizeParamIndex = 1)] string[] tuple_names,
                                                    [MarshalAs(UnmanagedType.LPArray,
                                                               SizeParamIndex = 1)] double[] tuple_values,
                                                    IntPtr user_data);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate ePLRESULT tPL_RegisterBroadcastCallback(IntPtr broadcast, tPL_BroadcastCallback function);
    #endregion

    /// <summary>Planimate has paused callback function</summary>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ePLRESULT tPL_PauseCallback(double the_time, ePLPauseReason stop_reason,
                                                IntPtr userdata);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate ePLRESULT tPL_RegisterPauseCallback(tPL_PauseCallback function, IntPtr userdata);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate ePLRESULT tPL_StringToValue([MarshalAs(UnmanagedType.LPStr)] string str,
                                                 ref double val,   // double*
                                                 eTFUnit format);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate ePLRESULT tPL_ValueToString(double v,
                                                 int buffer_len,
                                                 StringBuilder buffer,
                                                 eTFUnit format);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate ePLRESULT tPL_ValueToColor(double v,
                                                int buffer_len,
                                                StringBuilder buffer);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate ePLRESULT tPL_GetCellFormatted(IntPtr table,
                                                    int    r,
                                                    int    c,
                                                    int    buffer_len,
                                                    StringBuilder buffer);
    
    #endregion

    #region Special Compatibility
    
    // procs that aren't in enum but need to be callable for older versions of PL
    tPL_SetAttValue  ltPL_SetAttValue;
    tPL_GetAttValue  ltPL_GetAttValue;

    #endregion
    
    private tPL_GetProc ltPL_GetProc             = null;
    private tPL_SuspendThread ltPL_SuspendThread = null;
    private tPL_ResumeThread ltPL_ResumeThread   = null;
    private tPL_TermThread   ltPL_TermThread     = null;

    private int      ProcCount;
    private IntPtr[] ProcTable;
    private int      suspendLevel = 0;

    /// <summary>Engine constructor</summary>
    public PLEngineCore()
    {
    }

    /// <summary>
    /// Initialise the Planimate® engine for use when embedding PL in
    /// a dotNET application
    /// <param name='dll_pathname'>Path to the Planimate DLL</param>
    /// <param name='cmdline'>Command line arguments to initialise Planimate with</param>
    /// <param name='windowHandle'>HWND of containing window or IntPtr.Zero.</param>
    /// <param_name='apiVersion'>API compatibility version, 5..n or 0 for latest only.</param>
    /// </summary>
    public ePLRESULT InitPLEngine(String dll_pathname,
                                  string cmdline,
                                  IntPtr? windowHandle = null,
                                  int apiVersion=0)
    {
      IntPtr dll_handle = LoadLibrary(dll_pathname);

      if (dll_handle == IntPtr.Zero)
        return ePLRESULT.PLR_NOTFOUND;
      
      IntPtr pAddressOfFunctionToCall;

      // these must bind
      pAddressOfFunctionToCall = GetProcAddress(dll_handle, "PL_GetProc");
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        throw new PLBindFailure();
      ltPL_GetProc = (tPL_GetProc)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_GetProc));

      pAddressOfFunctionToCall = GetProcAddress(dll_handle, "PL_SuspendThread");
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        throw new PLBindFailure();
      ltPL_SuspendThread = (tPL_SuspendThread)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_SuspendThread));

      pAddressOfFunctionToCall = GetProcAddress(dll_handle, "PL_ResumeThread");
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        throw new PLBindFailure();
      ltPL_ResumeThread = (tPL_ResumeThread)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_ResumeThread));

      pAddressOfFunctionToCall = GetProcAddress(dll_handle, "PL_TermThread");
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        throw new PLBindFailure();
      ltPL_TermThread = (tPL_TermThread)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_TermThread));
      
      // one time setup
      pAddressOfFunctionToCall = GetProcAddress(dll_handle, "PL_InitThread");
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        throw new PLBindFailure();
      tPL_InitThread ltPL_InitThread = (tPL_InitThread)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_InitThread));

      pAddressOfFunctionToCall = GetProcAddress(dll_handle, "PL_WaitThreadRunning");
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        throw new PLBindFailure();
      tPL_WaitThreadRunning ltPL_WaitThreadRunning = (tPL_WaitThreadRunning)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_WaitThreadRunning));

      // ProcTable will be filled in as procs are requested
      ProcTable = new IntPtr[(int)ePLProcs.ePL_PROCCOUNT];
      suspendLevel = 0;

      ltPL_InitThread(dll_handle, cmdline,  windowHandle ?? IntPtr.Zero);
      ltPL_WaitThreadRunning();

      // check PL supports all the procs we expect - will throw exception on fail
      // (PL returns NULL on bad proc as of 8.57.2)
      switch (apiVersion)
      {
        case 5:
          ProcCount = (int)ePLProcs.ePL_WaitModelStarted + 1;
          break;
        case 6:
          ProcCount = (int)ePLProcs.ePL_RemoveTableChangeCallback + 1;
          break;
        case 7:
          ProcCount = (int)ePLProcs.ePL_UpdateDependencies + 1;
          break;
        case 8:
          ProcCount = (int)ePLProcs.ePL_GetCellFormatted + 1;
          break;
        case 9:
          ProcCount = (int)ePLProcs.ePL_ValueToARGB + 1;
          break;

        default:
          ProcCount = (int)ePLProcs.ePL_PROCCOUNT;
          break;
      }
      
      // ensure all the procs we expect are there, this will throw on fail
      GetFunction(ProcCount-1);

      // bindings not in proc table eeded for older version of Pl
      pAddressOfFunctionToCall = GetProcAddress(dll_handle, "PL_SetAttValue");
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        throw new PLBindFailure();
      ltPL_SetAttValue = (tPL_SetAttValue)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_SetAttValue));

      pAddressOfFunctionToCall = GetProcAddress(dll_handle, "PL_GetAttValue");
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        throw new PLBindFailure();
      ltPL_GetAttValue = (tPL_GetAttValue)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_GetAttValue));
      
      return ePLRESULT.PLR_OK;
    }

    /// <summary>
    /// Initialise this for use with an existing Planimate which is calling
    /// C# using the PLCLR bridge DLL. PLCLR attempts to call
    /// the instantiated class which should call this if the engine is required.
    /// </summary>
    public void InitPLEngine(IntPtr[] pl_proctable)
    {
      // currently require PL at our version for the PL call DLL API
      ProcCount = (int)ePLProcs.ePL_PROCCOUNT;
      if (pl_proctable.Length < ProcCount)
        throw new PLBindFailure();

      ProcTable = pl_proctable;
      
      // no need for suspend when PL calls C# DLL, we are in PL's thread
      suspendLevel = 1;
    }

    /// <summary>
    /// Clean up gracefully. This is only required if Planimate is being
    /// embedded and the caller wants to gracefully shut it down without
    /// closing themselves.
    /// </summary>
    public void TermPLEngine()
    {
      if (ltPL_TermThread != null)
      {
        ltPL_TermThread();
        ltPL_SuspendThread = null;
        ltPL_ResumeThread = null;
        ltPL_TermThread = null;
      }
    }

    /// <summary>
    /// Return a callback function pointer based on enum. Guaranteed to
    /// return a non null pointer or throw exception if something is really
    /// wrong with the callback table.
    /// </summary>
    private IntPtr GetFunction(int function)
    {
      if (ProcTable[function] == IntPtr.Zero)
        if (ltPL_GetProc != null)
          ProcTable[function] = ltPL_GetProc((ePLProcs)function);
        else
          throw new PLBindFailure(); // should have been caught on init
          
      return ProcTable[function];
    }

    private IntPtr GetFunction(ePLProcs function)
    {
      return GetFunction((int)function);
    }
    
    /// <summary>
    /// Return a callback delegate for function based on enum. Unfortuantely
    /// you cant template a case to a delegate so caller still has to typecast
    /// the returned object to the actual delegate type.
    /// </summary>
    private Delegate GetFunction<T>(ePLProcs f)
    {
      return Marshal.GetDelegateForFunctionPointer(GetFunction(f), typeof(T));
    }

    /// <summary>
    /// Send a run command to the Planimate® engine.
    /// </summary>
    /// <param name='cmd'>Run command from ePLRunCMD.</param>
    public ePLRESULT RunCommand(ePLRunCMD cmd)
    {
      return ((tPL_Run)(GetFunction<tPL_Run>(ePLProcs.ePL_Run)))(cmd);
    }

    #region Thread Functions
    /// <summary>
    /// Suspend the engine thread.
    /// </summary>
    public void SuspendThread()
    {
      internalSuspendThread();
    }

    /// <summary>
    /// Resume the engine thread.
    /// </summary>
    public void ResumeThread()
    {
      internalResumeThread();
    }

    private void internalSuspendThread()
    {
      if (suspendLevel == 0)
        ltPL_SuspendThread();
      suspendLevel++;
    }

    private void internalResumeThread()
    {
      if (suspendLevel != 0)
      {
        suspendLevel--;
        if (suspendLevel == 0)
          ltPL_ResumeThread();
      }
    }

    /// <summary>
    /// Enable client to disable automatic suspend/resume of thread.
    /// Must invoke EnableSuspendThread after this if you want to re-enable.
    /// </summary>
    public void DisableSuspendThread()
    {
      suspendLevel++;
    }

    /// <summary>
    /// Enable client to re-enable automatic suspend/resume of thread
    /// Must invoke DisableSuspendThread before this
    /// </summary>
    public void EnableSuspendThread()
    {
      if (suspendLevel > 0)
        suspendLevel--;
      else
        throw new Exception("plengine.EnableSuspendThread misused");
    }
    
    #endregion

    /// <summary>Returns the Planimate® version powering the engine.</summary>
    public string PlanimateVersion()
    {
      var pl_version = ((tPL_AppVersion)GetFunction<tPL_AppVersion>(ePLProcs.ePL_AppVersion))();  
      return System.Runtime.InteropServices.Marshal.PtrToStringAnsi(pl_version);
    }

    /// <summary>
    /// Control the run engine
    /// </summary>
    public ePLRESULT Run(ePLRunCMD runcmd)
    {
      return ((tPL_Run)GetFunction<tPL_Run>(ePLProcs.ePL_Run))(runcmd);
    }
    
    /// <summary>Gets Planimate® System info. Times are in seconds with 0 = the run start date</summary>
    /// <param name='sysinfo_id'>System info id from ePLSysInfo.</param>
    public double GetSystemInfo(ePLSysInfo sysinfo_id)
    {
      var ltPL_GetSystemInfo = (tPL_GetSystemInfo)GetFunction<tPL_GetSystemInfo>(ePLProcs.ePL_GetSystemInfo);
      internalSuspendThread();
      double res = ltPL_GetSystemInfo(sysinfo_id);
      internalResumeThread();
      return res;
    }

    /// <summary>Sets writable Planimate® System info. Times are in seconds with 0 = the run start date</summary>
    /// <param name='sysinfo_id'>System info id from ePLSysInfo.</param>
    /// <param name='value'>Value to write.</param>
    public ePLRESULT SetSystemInfo(ePLSysInfo sysinfo_id, double value)
    {
      var ltPL_SetSystemInfo = (tPL_SetSystemInfo)GetFunction<tPL_SetSystemInfo>(ePLProcs.ePL_SetSystemInfo);
      internalSuspendThread();
      ePLRESULT res = ltPL_SetSystemInfo(sysinfo_id, value);
      internalResumeThread();
      return res;
    }

    #region Data Object Functions
    /// <summary>Finds a Planimate® data object by name</summary>
    /// <param name='DO_name'>String name of the Planimate® data object</param>
    public IntPtr FindDataObjectName(string DO_name)
    {
      var ltPL_FindDataObjectName = (tPL_FindDataObjectName)GetFunction<tPL_FindDataObjectName>(ePLProcs.ePL_FindDataObjectName);
      internalSuspendThread();
      IntPtr res = ltPL_FindDataObjectName(DO_name);
      internalResumeThread();
      return res;
    }
    
    /// <summary>
    /// </summary>
    public IntPtr FindDataObject(int DO_id)
    {
      var ltPL_FindDataObject = (tPL_FindDataObject)GetFunction<tPL_FindDataObject>(ePLProcs.ePL_FindDataObject);
      internalSuspendThread();
      IntPtr res = ltPL_FindDataObject(DO_id);
      internalResumeThread();
      return res;
    }    
    
    /// <summary>Returns the data object type</summary>
    /// <param name='data_object'>Pointer to Planimate® data object</param>
    public eDOTypes DataObjectType(IntPtr data_object)
    {
      var ltPL_DataObjectType = (tPL_DataObjectType)GetFunction<tPL_DataObjectType>(ePLProcs.ePL_DataObjectType);
      internalSuspendThread();
      eDOTypes res = ltPL_DataObjectType(data_object);
      internalResumeThread();
      return res;
    }
    #endregion

    #region Attribute Functions

    /// <summary>
    /// Get Attribute value
    /// TODO: add attribute functions to proc table in PL
    /// </summary>
    public ePLRESULT SetAttValue(IntPtr data_object, double v)
    {
      internalSuspendThread();
      var res = ltPL_SetAttValue(data_object,v);
      internalResumeThread();

      return res;
    }

    /// <summary>
    /// Return value of attribute
    /// </summary>
    public double GetAttValue(IntPtr data_object)
    {
      internalSuspendThread();
      var res = ltPL_GetAttValue(data_object);
      internalResumeThread();
      
      return res;
    }
    
    #endregion

    #region Table Functions
    /// <summary>Returns the number of rows in a table data object</summary>
    /// <param name='data_object'>Pointer to Planimate® data object</param>
    public int Rows(IntPtr data_object)
    {
      var ltPL_Rows = (tPL_Rows)GetFunction<tPL_Rows>(ePLProcs.ePL_Rows);
      internalSuspendThread();
      int res = ltPL_Rows(data_object);
      internalResumeThread();
      return res;
    }

    /// <summary>Returns the number of columns in a table data object</summary>
    /// <param name='data_object'>Pointer to Planimate® data object</param>
    public int Columns(IntPtr data_object)
    {
      var ltPL_Columns = (tPL_Columns)GetFunction<tPL_Columns>(ePLProcs.ePL_Columns);
      internalSuspendThread();
      int res = ltPL_Columns(data_object);
      internalResumeThread();
      return res;
    }

    /// <summary>Returns the column name of the specified column</summary>
    /// <param name='data_object'>Pointer to Planimate® data object</param>
    /// <param name='column'>Index of the column</param>
    public string ColumnName(IntPtr data_object, int column)
    {
      var ltPL_ColumnName = (tPL_ColumnName)GetFunction<tPL_ColumnName>(ePLProcs.ePL_ColumnName);
      internalSuspendThread();
      string res = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ltPL_ColumnName(data_object, column));
      internalResumeThread();
      return res;
    }

    /// <summary>Returns Planimate® value format of the specified column</summary>
    /// <param name='data_object'>Pointer to Planimate® data object</param>
    /// <param name='column'>Index of the column</param>
    public eTFUnit GetColumnFormat(IntPtr data_object, int column)
    {
      var ltPL_GetColumnFormat = (tPL_GetColumnFormat)GetFunction<tPL_GetColumnFormat>(ePLProcs.ePL_GetColumnFormat);
      internalSuspendThread();
      eTFUnit res = ltPL_GetColumnFormat(data_object, column);
      internalResumeThread();
      return res;
    }

    /// <summary>Returns Planimate® label list that the specified column is formatted to.</summary>
    /// <param name='data_object'>Pointer to Planimate® data object</param>
    /// <param name='column'>Index of the column</param>
    public IntPtr GetColumnLabelList(IntPtr data_object, int column)
    {
      var ltPL_GetColumnLabels = (tPL_GetColumnLabels)GetFunction<tPL_GetColumnLabels>(ePLProcs.ePL_GetColumnLabels);
      internalSuspendThread();
      IntPtr res = ltPL_GetColumnLabels(data_object, column);
      internalResumeThread();
      return res;
    }

    /// <summary>Finds the column index based on the string name of the column</summary>
    /// <param name='data_object'>Pointer to Planimate® data object</param>
    /// <param name='col_name'>String column name of the target column</param>
    public int FindColumn(IntPtr data_object, string col_name)
    {
      var ltPL_FindColumn = (tPL_FindColumn)GetFunction<tPL_FindColumn >(ePLProcs.ePL_FindColumn);
      internalSuspendThread();
      int res = ltPL_FindColumn(data_object, col_name);
      internalResumeThread();
      return res;
    }

    /// <summary>Resize table to given rows/columns</summary>
    /// <param name='data_object'>Pointer to Planimate® data object</param>
    /// <param name='rows'>Number of rows to resize table to</param>
    /// <param name='rows'>Number of columns to resize table to (default leaves unchanged)</param>
    public ePLRESULT TableResize(IntPtr data_object, int rows, int cols = -1)
    {
      if (cols == -1)
        cols = Columns(data_object);

      if (cols > 0)
      {
        var ltPL_TableResize = (tPL_TableResize)GetFunction<tPL_TableResize>(ePLProcs.ePL_TableResize);
        internalSuspendThread();
        ePLRESULT res = ltPL_TableResize(data_object, rows, cols);
        internalResumeThread();
        return res;
      }
      else
        return ePLRESULT.PLR_INVALID;
    }
      
    /// <summary>Gets the value from a cell</summary>
    /// <param name='data_object'>Pointer to Planimate® data object (table)</param>
    /// <param name='row'>Cell row index</param>
    /// <param name='col'>Cell column index</param>
    public double GetCell(IntPtr data_object, int row, int col)
    {
      var ltPL_GetCell = (tPL_GetCell)GetFunction<tPL_GetCell>(ePLProcs.ePL_GetCell);
      internalSuspendThread();
      double res = ltPL_GetCell(data_object, row, col);
      internalResumeThread();
      return res;
    }
    
    /// <summary>Sets the value of a cell</summary>
    /// <param name='data_object'>Pointer to Planimate® data object (table)</param>
    /// <param name='row'>Cell row index</param>
    /// <param name='col'>Cell column index</param>
    /// <param name='data'>Data to write to the cell</param>
    public ePLRESULT SetCell(IntPtr data_object, int row, int col, double data)
    {
      var ltPL_SetCell = (tPL_SetCell)GetFunction<tPL_SetCell>(ePLProcs.ePL_SetCell);
      internalSuspendThread();
      ePLRESULT res = ltPL_SetCell(data_object, row, col, data);
      internalResumeThread();
      return res;
    }

    /// <summary>Sets the value of a Free Text cell</summary>
    /// <param name='data_object'>Pointer to Planimate® data object (table)</param>
    /// <param name='row'>Cell row index</param>
    /// <param name='col'>Cell column index</param>
    /// <param name='data'>Data to write to the cell (string)</param>
    public ePLRESULT SetCell(IntPtr data_object, int row, int col, string data)
    {
      var ltPL_SetCellText = (tPL_SetCellText)GetFunction<tPL_SetCellText>(ePLProcs.ePL_SetCellText);
      internalSuspendThread();
      ePLRESULT res = ltPL_SetCellText(data_object, row, col, data);
      internalResumeThread();
      return res;
    }

    /// <summary>Gets the text from a Free Text cell</summary>
    /// <param name='data_object'>Pointer to Planimate® data object (table)</param>
    /// <param name='row'>Cell row index</param>
    /// <param name='col'>Cell column index</param>
    public string GetCellText(IntPtr data_object, int row, int col)
    {
      var ltPL_GetCellText = (tPL_GetCellText)GetFunction<tPL_GetCellText>(ePLProcs.ePL_GetCellText);
      internalSuspendThread();
      string res = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ltPL_GetCellText(data_object, row, col));
      internalResumeThread();
      return res;
    }
    #endregion

    #region Label list functions
    /// <summary>Returns a label list object based on a string label list name</summary>
    /// <param name='list_name'>Label list name</param>
    public IntPtr GetNamedLabelList(string list_name)
    {
      var ltPL_GetNamedLabelList = (tPL_GetNamedLabelList)GetFunction<tPL_GetNamedLabelList>(ePLProcs.ePL_GetNamedLabelList);
      internalSuspendThread();
      IntPtr res = ltPL_GetNamedLabelList(list_name);
      internalResumeThread();
      return res;
    }

    /// <summary>Returns the label string based on a long(Int32) label index</summary>
    /// <param name='data_object'>Pointer to Planimate® data object (label list)</param>
    /// <param name='index'>Label index as integer</param>
    public string LookUpLabelLValue(IntPtr data_object, Int32 index)
    {
      var ltPL_LookUpLValue = (tPL_LookUpLValue)GetFunction<tPL_LookUpLValue>(ePLProcs.ePL_LookUpLValue);
      internalSuspendThread();
      string res = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ltPL_LookUpLValue(data_object, index));
      internalResumeThread();
      return res;
    }

    /// <summary>Returns the label string based on a double label index</summary>
    /// <param name='data_object'>Pointer to Planimate® data object (label list)</param>
    /// <param name='index'>Label index as double</param>
    public string LookUpLabelDValue(IntPtr data_object, double index)
    {
      var ltPL_LookUpDValue = (tPL_LookUpDValue)GetFunction<tPL_LookUpDValue>(ePLProcs.ePL_LookUpDValue);
      internalSuspendThread();
      string res = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ltPL_LookUpDValue(data_object, index));
      internalResumeThread();
      return res;
    }

    /// <summary>
    /// Get Number of labels in a Planimate label list
    /// </summary>
    public int LabelCount(IntPtr labellist)
    {
      var ltPL_LabelCount = (tPL_LabelCount)GetFunction<tPL_LabelCount>(ePLProcs.ePL_LabelCount);
      internalSuspendThread();
      int res = ltPL_LabelCount(labellist);
      internalResumeThread();
      return res;
    }

    /// <summary>
    /// Return name of a label for a given position in the list
    /// </summary>
    public string GetLabelName(IntPtr labellist, int ordinal)
    {
      var ltPL_GetLabelName = (tPL_GetLabelName)GetFunction<tPL_GetLabelName>(ePLProcs.ePL_GetLabelName);
      internalSuspendThread();
      string res = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ltPL_GetLabelName(labellist, ordinal));
      internalResumeThread();
      return res;
    }
    
    /// <summary>
    /// Return index of a label for a given index in the list
    /// </summary>
    public int GetLabelIndex(IntPtr labellist, int ordinal)
    {
      internalSuspendThread();
      int res = ((tPL_GetLabelIndex)(GetFunction<tPL_GetLabelIndex>(ePLProcs.ePL_GetLabelIndex)))(labellist,ordinal);
      internalResumeThread();
      return res;
    }
    
    /// <summary>Returns the label index based on a label string</summary>
    /// <param name='data_object'>Pointer to Planimate® data object (label list)</param>
    /// <param name='label'>Label string</param>
    public Int32 LookUpLabelIndex(IntPtr data_object, string label)
    {
      var ltPL_LookUpLabel = (tPL_LookUpLabel)GetFunction<tPL_LookUpLabel>(ePLProcs.ePL_LookUpLabel);
      internalSuspendThread();
      Int32 res = ltPL_LookUpLabel(data_object, label);
      internalResumeThread();
      return res;
    }
    #endregion

    // Entire Column
    /// <summary>Returns a column as an array</summary>
    /// <param name='data_object'>Pointer to Planimate® data object (table)</param>
    /// <param name='column'>Index of column in DataTable</param>
    /// <param name='rows'>The number of rows to return in the array</param>
    /// <param name='into'>Array of double to place column in</param>
    public ePLRESULT GetColumn(IntPtr data_object, int column, int rows, double[] into)
    {
      var ltPL_GetColumn = (tPL_GetColumn)GetFunction<tPL_GetColumn>(ePLProcs.ePL_GetColumn);
      internalSuspendThread();
      ePLRESULT res = ltPL_GetColumn(data_object, column, rows, into);
      internalResumeThread();
      return res;
    }

    /// <summary>Sets an array to a column</summary>
    /// <param name='data_object'>Pointer to Planimate® data object (table)</param>
    /// <param name='column'>Index of column in DataTable</param>
    /// <param name='rows'>The number of rows to set from the array</param>
    /// <param name='to'>Array of doubles to place into the column</param>
    public ePLRESULT SetColumn(IntPtr data_object, int column, int rows, double[] to)
    {
      var ltPL_SetColumn = (tPL_SetColumn)GetFunction<tPL_SetColumn>(ePLProcs.ePL_SetColumn);
      internalSuspendThread();
      ePLRESULT res = ltPL_SetColumn(data_object, column, rows, to);
      internalResumeThread();
      return res;
    }

    /// <summary>Insert a Column into Table
    /// </summary>
    public ePLRESULT InsertColumn(IntPtr data_object, int atCol, int count=1)
    {
      var ltPL_InsertColumn = (tPL_InsertColumn)GetFunction<tPL_InsertColumn>(ePLProcs.ePL_InsertColumn);
      internalSuspendThread();
      ePLRESULT res = ltPL_InsertColumn(data_object,atCol,count);
      internalResumeThread();
      return res;
    }
    
    /// <summary>Insert a Column into Table
    /// </summary>
    public ePLRESULT InsertColumnNamed(IntPtr data_object, int atCol, eTFUnit units, string name,IntPtr ? labels=null)
    {
      var ltPL_InsertColumnNamed = (tPL_InsertColumnNamed)GetFunction<tPL_InsertColumnNamed>(ePLProcs.ePL_InsertColumnNamed);
      internalSuspendThread();
      ePLRESULT res = ltPL_InsertColumnNamed(data_object,atCol,(int)units,name,labels??IntPtr.Zero);
      internalResumeThread();
      return res;
    }

    /// <summary>Delete's a row range from a table
    /// </summary>
    public ePLRESULT DeleteRow(IntPtr data_object, int row, int for_rows=1)
    {
      var ltPL_DeleteRow = (tPL_DeleteRow)GetFunction<tPL_DeleteRow>(ePLProcs.ePL_DeleteRow);
      internalSuspendThread();
      ePLRESULT res = ltPL_DeleteRow(data_object,row,for_rows);
      internalResumeThread();
      return res;
    }

    //
    
    void PrepareColumn(DataTable data_table,
                       int    i,
                       Type   the_type,
                       String the_name,
                       int    rows)
    {
      // if column already exists and its the correct type, reuse it
      // otherwise if it exists with wrong type, replace it
      if (data_table.Columns.Count > i)
        if  (data_table.Columns[i].DataType != the_type)
        {
          data_table.Columns.RemoveAt(i);
          DataColumn new_col =  data_table.Columns.Add(the_name,the_type);
          new_col.SetOrdinal(i);
        }
        else
          data_table.Columns[i].ColumnName = the_name;
      else
        data_table.Columns.Add(the_name,the_type);

      // load up all rows - this happens only for the first column
      while (data_table.Rows.Count < rows)
        data_table.Rows.Add();      
    }
    
    /// <summary>Updates a DataTable object with data from a Planimate® data object
    /// table and returns it as a DataTable. If the DataTable is specified to be formatted,
    /// all columns will be correct .NET data formats. Unformatted data is formatted as type
    /// Double</summary>
    /// <param name='data_table'>DataTable that will be updated</param>
    /// <param name='data_object'>Pointer to Planimate® data object (table)</param>
    /// <param name='formatted'>Specifies if the returned DataTable should be formatted. True = format</param>
    public ePLRESULT UpdateDataTable(DataTable data_table,
                                     IntPtr data_object,
                                     Boolean formatted,
                                     Boolean labels_as_text=false)
    {
      ePLRESULT res = ePLRESULT.PLR_OK;

      int columns = Columns(data_object);
      int rows = Rows(data_object);

      // re-use existing allocation as much as possible
      // remove from end to avoid shuffling
      while (data_table.Columns.Count > columns)
        data_table.Columns.RemoveAt(data_table.Columns.Count-1);
      while (data_table.Rows.Count > rows)
        data_table.Rows.RemoveAt(data_table.Rows.Count-1);

      // preallocate rows
      if (data_table.MinimumCapacity < rows)
        data_table.MinimumCapacity = rows;

      double[] col = new double[rows];

      if (formatted)
      {
        for (int i = 0; i < columns; i++)
        {
          eTFUnit col_unit = GetColumnFormat(data_object, i);
          switch (col_unit)
          {
            case eTFUnit.UNIT_ABSTIME:
            case eTFUnit.UNIT_ABSTIME2:
            case eTFUnit.UNIT_TIMEOFDAY:
            case eTFUnit.UNIT_TIMEAMPM:
            case eTFUnit.UNIT_ABSTIME3:
            case eTFUnit.UNIT_DATEONLY:
            case eTFUnit.UNIT_DAYDATE:
            case eTFUnit.UNIT_ABSTIME3U:
            case eTFUnit.UNIT_TIMEOFDAY_2:
            case eTFUnit.UNIT_DATE2:
            case eTFUnit.UNIT_ABSTIMEMS:
            case eTFUnit.UNIT_DATE2TIME:
            case eTFUnit.UNIT_DATE3:
            case eTFUnit.UNIT_ABSTIMEC:
            case eTFUnit.UNIT_DATE4:
            case eTFUnit.UNIT_DATE5:
            case eTFUnit.UNIT_OS_DATETIME:
            case eTFUnit.UNIT_OS_DATEONLY:
            case eTFUnit.UNIT_OS_TIME:
            case eTFUnit.UNIT_DATE3TIME:
              res = GetColumn(data_object, i, rows, col);
              if (res != ePLRESULT.PLR_OK)
                return res;

              PrepareColumn(data_table,i,typeof(DateTime),ColumnName(data_object, i),rows);

              // note: at this point the table has the right number of rows
              // (extra rows deleted at start, PrepareColumn has added needed
              // rows.
              // note: by using .Rows.Count it enables optimisation
                    
              for (int j = 0; j < data_table.Rows.Count; j++)
              {
                DateTime dt = new DateTime(0);
                dt = ConvertFromPLTimestamp(col[j]);
                data_table.Rows[j][i] = dt;
              }
              break;
            case eTFUnit.UNIT_TIME:
            case eTFUnit.UNIT_TIME2:
            case eTFUnit.UNIT_TIME3:
            case eTFUnit.UNIT_MINUTES:
            case eTFUnit.UNIT_HOURS:
            case eTFUnit.UNIT_DAYS:
            case eTFUnit.UNIT_WEEKS:
            case eTFUnit.UNIT_HOURMIN:
            case eTFUnit.UNIT_WEEKDAY1:
            case eTFUnit.UNIT_TIMEDHHMM:
            case eTFUnit.UNIT_TIMED0HMM:
            case eTFUnit.UNIT_DAY2HHMM:
            case eTFUnit.UNIT_DAY2HHMMCOLON:
              res = GetColumn(data_object, i, rows, col);
              if (res != ePLRESULT.PLR_OK)
                return res;

              PrepareColumn(data_table,i,typeof(TimeSpan),ColumnName(data_object, i),rows);
                    
              for (int j = 0; j < data_table.Rows.Count; j++)
              {
                TimeSpan span = new TimeSpan(0, 0, Convert.ToInt32(col[j].ToString()));
                data_table.Rows[j][i] = span;
              }
              break;
            case eTFUnit.UNIT_LABEL:
              IntPtr llist = GetColumnLabelList(data_object, i);
              if (llist == IntPtr.Zero)
                return ePLRESULT.PLR_BADFORMAT;

              res = GetColumn(data_object, i, rows, col);
              if (res != ePLRESULT.PLR_OK)
                return res;

              if (labels_as_text)
              {
                PrepareColumn(data_table,i,typeof(string),ColumnName(data_object, i),rows);
                for (int j = 0; j < data_table.Rows.Count; j++)
                  data_table.Rows[j][i] = LookUpLabelDValue(llist,col[j]);
              }
              else
              {
                PrepareColumn(data_table,i,typeof(int),ColumnName(data_object, i),rows);
                for (int j = 0; j < data_table.Rows.Count; j++)
                  data_table.Rows[j][i] = col[j];
              }                    
              break;
            case eTFUnit.UNIT_FREETEXT:
              res = GetColumn(data_object, i, rows, col);
              if (res != ePLRESULT.PLR_OK)
                return res;

              PrepareColumn(data_table,i,typeof(string),ColumnName(data_object, i),rows);

              for (int j = 0; j < data_table.Rows.Count; j++)
                data_table.Rows[j][i] = GetCellText(data_object, j, i);
              break;
            default:
              res = GetColumn(data_object, i, rows, col);
              if (res != ePLRESULT.PLR_OK)
                return res;

              PrepareColumn(data_table,i,typeof(double),ColumnName(data_object, i),rows);
              for (int j = 0; j < data_table.Rows.Count; j++)
                data_table.Rows[j][i] = col[j];
              break;

          };

        }
      }
      else  // unformatted (all columns doubles)
      {
        for (int i = 0; i < columns; i++)
        {
          res = GetColumn(data_object, i, rows, col);
          if (res != ePLRESULT.PLR_OK)
            return res;

          PrepareColumn(data_table,i,typeof(double),ColumnName(data_object, i),rows);

          for (int j = 0; j < data_table.Rows.Count; j++)
            data_table.Rows[j][i] = col[j];
        }
      }

      return res;
    }

    /// <summary>Finds a Planimate® data object by name and updates the supplied DataTable.
    /// If the DataTable is specified to be formatted, all columns will be correct .NET data formats.
    /// Unformatted data is formatted as type Double</summary>
    /// <param name='rTable'>DataTable that will be updated.</param>
    /// <param name='DO_name'>String name of the Planimate® table data object</param>
    /// <param name='formatted'>Specifies if the returned DataTable should be formatted. True = format</param>
    public ePLRESULT UpdateDataTable(DataTable rTable,
                                     string DO_name,
                                     Boolean formatted)
    {
      var PL_data_object = FindDataObjectName(DO_name);
      if (PL_data_object == IntPtr.Zero)
        return ePLRESULT.PLR_NOTFOUND;

      return UpdateDataTable(rTable, PL_data_object, formatted);
    }

    /// <summary>Gets a Planimate® data object table and returns it as a DataTable.
    /// If the DataTable is specified to be formatted, all columns will be correct .NET data formats.
    /// Unformatted data is formatted as type Double</summary>
    /// <param name='data_object'>Pointer to Planimate® data object (table)</param>
    /// <param name='formatted'>Specifies if the returned DataTable should be formatted. True = format</param>
    public DataTable GetDataTable(IntPtr data_object, Boolean formatted, Boolean labels_as_text=false)
    {
      DataTable data_table = new DataTable();
      ePLRESULT res = UpdateDataTable(data_table,data_object,formatted,labels_as_text);
      
      if (res == ePLRESULT.PLR_OK)
        return data_table;
      else
        return null;
    }

    /// <summary>Finds a Planimate® data object by name and returns it as a DataTable.
    /// If the DataTable is specified to be formatted, all columns will be correct .NET data formats.
    /// Unformatted data is formatted as type Double</summary>
    /// <param name='DO_name'>String name of the Planimate® table data object</param>
    /// <param name='formatted'>Specifies if the returned DataTable should be formatted. True = format</param>
    public DataTable GetDataTable(string DO_name,
                                  Boolean formatted,
                                  Boolean labels_as_text=false)
    {
      var PL_data_object = FindDataObjectName(DO_name);
      if (PL_data_object == IntPtr.Zero)
        return null;

      return GetDataTable(PL_data_object, formatted,labels_as_text);
    }

    /// <summary>
    /// Return PL units corresponding to c# type
    /// </summary>
    public eTFUnit MapDataTypeToPL(Type type)
    {
      if (type == typeof(double) ||
          type == typeof(int) ||
          type == typeof(bool))
        return eTFUnit.UNIT_VALUE;
      if (type == typeof(string))
        return eTFUnit.UNIT_FREETEXT;
      else
        if (type == typeof(DateTime))
          return eTFUnit.UNIT_ABSTIME;
        else
          if (type ==  typeof(TimeSpan))
            return eTFUnit.UNIT_TIME;
          else
            return eTFUnit.UNIT_NULL;
    }    
    
    public ePLRESULT SetFromDataTable(DataTable data_table,
                                      int dt_r,
                                      int dt_c,
                                      IntPtr data_object,
                                      int r,
                                      int c)
    {
      var data = data_table.Rows[dt_r][dt_c];
      
      if (data_table.Columns[dt_c].DataType == typeof(double))
      {
        if (data is System.DBNull)
          SetCell(data_object, r, c,0.0);
        else
          SetCell(data_object, r, c, (double)data);
      }
      else
        if (data_table.Columns[dt_c].DataType == typeof(int))
        {
          if (data is System.DBNull)
            SetCell(data_object, r, c,0);
          else
            SetCell(data_object, r, c, (int)data);
        }
        else
          if (data_table.Columns[dt_c].DataType == typeof(string))
          {
            if (data is System.DBNull)
              data = "";
              
            eTFUnit col_unit = GetColumnFormat(data_object, c);
            if (col_unit == eTFUnit.UNIT_LABEL)
            {
              IntPtr llist = GetColumnLabelList(data_object, c);
              if (llist == IntPtr.Zero)
                return ePLRESULT.PLR_BADFORMAT;

              SetCell(data_object, r, c, Convert.ToDouble(LookUpLabelIndex(llist, (string)data)));
            }
            else
              SetCell(data_object, r, c, (string)data);
          }
          else
            if (data_table.Columns[dt_c].DataType == typeof(DateTime))
            {
              if (data is System.DBNull)
                SetCell(data_object, r, c, 0.0);
              else
                SetCell(data_object, r, c, ConvertToPLTimestamp((DateTime)data));
            }
            else
              if (data_table.Columns[dt_c].DataType == typeof(TimeSpan))
              {
                if (data is System.DBNull)
                  SetCell(data_object, r, c, 0.0);
                else
                {
                  TimeSpan span = (TimeSpan)data;
                  SetCell(data_object, r, c, span.TotalSeconds);
                }
              }
              else
                return ePLRESULT.PLR_BADFORMAT;
     
      
      return ePLRESULT.PLR_OK;
    }

    /// <summary>Sets a Planimate® data object table and from a DataTable.</summary>
    /// <param name='data_table'>Reference to the DataTable to be written to Planimate®</param>
    /// <param name='data_object'>Pointer to Planimate® data object (table)</param>
    public ePLRESULT SetFromDataTable(DataTable data_table,
                                      IntPtr    data_object)
    {
      if (data_table == null || data_object == IntPtr.Zero)
        return ePLRESULT.PLR_BADINDEX;

      // only resize rows (columns may be option added later)
      int rc = data_table.Rows.Count;
      TableResize(data_object,rc);
      
      for (int c = 0; c < data_table.Columns.Count; c++)
        for (int r = 0; r < data_table.Rows.Count; r++)
        {
          ePLRESULT res = SetFromDataTable(data_table,r,c,data_object,r,c);
          if (res != ePLRESULT.PLR_OK)
            return res;
        }

      return ePLRESULT.PLR_OK;
    }

    
    #region Broadcast Functions
    /// <summary>Returns a broadcast object based on a string broadcast name</summary>
    /// <param name='BC_name'>Name of broadcast (string)</param>
    public IntPtr FindBroadcastName(string BC_name)
    {
      var ltPL_GetBroadcastName = (tPL_GetBroadcastName)GetFunction<tPL_GetBroadcastName>(ePLProcs.ePL_GetBroadcastName);
      internalSuspendThread();
      IntPtr res = ltPL_GetBroadcastName(BC_name);
      internalResumeThread();
      return res;
    }

    /// <summary>Sends a broadcast into the engine based on broadcast object</summary>
    /// <param name='broadcast'>Pointer to broadcast object</param>
    public ePLRESULT SendBroadcast(IntPtr broadcast)
    {
      ePLRESULT res = ePLRESULT.PLR_NOTFOUND;
      if (broadcast != IntPtr.Zero)
      {
        var ltPL_SendBroadcast = (tPL_SendBroadcast)GetFunction<tPL_SendBroadcast>(ePLProcs.ePL_SendBroadcast);
        internalSuspendThread();
        res = ltPL_SendBroadcast(broadcast);
        internalResumeThread();
      }
      return res;
    }

    /// <summary>Sends a broadcast into the engine based on broadcast name</summary>
    /// <param name='BC_name'>Name of broadcast (string)</param>
    public ePLRESULT SendBroadcast(string BC_name)
    {
      IntPtr broadcast = FindBroadcastName(BC_name);
      if (broadcast != IntPtr.Zero)
        return SendBroadcast(broadcast);
      else
        return ePLRESULT.PLR_NOTFOUND;
    }

    /// <summary>Sends a broadcast into the engine based on broadcast object and include tuple data for the item</summary>
    /// <param name='broadcast'>Pointer to broadcast object</param>
    /// <param name='no_params'>Number of tuple parameters to include</param>
    /// <param name='tuple_names'>Array of tuple names (length = no_params)</param>
    /// <param name='tuple_values'>Array of tuple values (length = no_params)</param>
    public ePLRESULT SendBroadcast(IntPtr broadcast, int no_params, string[] tuple_names, double[] tuple_values)
    {
      ePLRESULT res = ePLRESULT.PLR_NOTFOUND;
      if (broadcast != IntPtr.Zero)
      {
        var ltPL_SendBroadcastTuple = (tPL_SendBroadcastTuple)GetFunction<tPL_SendBroadcastTuple>(ePLProcs.ePL_SendBroadcastTuple);
        internalSuspendThread();
        res = ltPL_SendBroadcastTuple(broadcast, no_params, tuple_names, tuple_values);
        internalResumeThread();
      }

      return res;
    }

    /// <summary>Sends a broadcast into the engine based on broadcast name and include tuple data for the item</summary>
    /// <param name='BC_name'>Name of broadcast (string)</param>
    /// <param name='no_params'>Number of tuple parameters to include</param>
    /// <param name='tuple_names'>Array of tuple names (length = no_params)</param>
    /// <param name='tuple_values'>Array of tuple values (length = no_params)</param>
    public ePLRESULT SendBroadcast(string BC_name, int no_params, string[] tuple_names, double[] tuple_values)
    {
      var ltPL_GetBroadcastName = (tPL_GetBroadcastName)GetFunction<tPL_GetBroadcastName>(ePLProcs.ePL_GetBroadcastName);
      var ltPL_SendBroadcastTuple = (tPL_SendBroadcastTuple)GetFunction<tPL_SendBroadcastTuple>(ePLProcs.ePL_SendBroadcastTuple);
      ePLRESULT res = ePLRESULT.PLR_NOTFOUND;
      internalSuspendThread();
      IntPtr broadcast = ltPL_GetBroadcastName(BC_name);
      if (broadcast != IntPtr.Zero)
        res = ltPL_SendBroadcastTuple(broadcast, no_params, tuple_names, tuple_values);
      
      internalResumeThread();
      return res;
    }

    /// <summary>
    /// Broadcast callback  enables a user provided function to be called
    /// every time Planimate sends a defined broadcast.
    /// Thread Notes:
    ///
    /// The callback will be called in the context of PL's thread.
    /// The PLBroadcast handle is unique per broadcast per thread.
    /// The userdata will be returned and is useful for passing a pointer
    /// to the class/instance.
    ///
    /// Data *must* be processed/copied before returning from this function.
    /// All pointers are invalid after the callback returns.
    /// </summary>
    /// <param name='broadcast'>Pointer to broadcast object</param>
    /// <param name='callback_func'>Function to register as callback</param>
    public ePLRESULT RegisterBroadcastCallback(IntPtr broadcast, tPL_BroadcastCallback callback_func)
    {
      var ltPL_RegisterBroadcastCallback = (tPL_RegisterBroadcastCallback)GetFunction<tPL_RegisterBroadcastCallback>(ePLProcs.ePL_RegisterBroadcastCallback);
      internalSuspendThread();
      var res = ltPL_RegisterBroadcastCallback(broadcast, callback_func);
      internalResumeThread();
      return res;
    }

    /// <summary>
    /// Register broadcast callback using broadcast name.
    /// </summary>
    /// <param name='BC_name'>Pointer to broadcast object</param>
    /// <param name='callback_func'>Function to register as callback</param>
    public ePLRESULT RegisterBroadcastCallback(string BC_name, tPL_BroadcastCallback callback_func)
    {
      var ltPL_GetBroadcastName = (tPL_GetBroadcastName)GetFunction<tPL_GetBroadcastName>(ePLProcs.ePL_GetBroadcastName);
      var ltPL_RegisterBroadcastCallback = (tPL_RegisterBroadcastCallback)GetFunction<tPL_RegisterBroadcastCallback>(ePLProcs.ePL_RegisterBroadcastCallback);
      
      ePLRESULT res = ePLRESULT.PLR_NOTFOUND;
      internalSuspendThread();
      IntPtr broadcast = ltPL_GetBroadcastName(BC_name);
      if (broadcast != IntPtr.Zero)
        res = ltPL_RegisterBroadcastCallback(broadcast, callback_func);
      internalResumeThread();
      return res;
    }

    /// <summary>
    /// Register a function for Planimate to call when a table changes. The
    /// returned handle must be used to Remove the callback when it is no longer
    /// needed.
    /// </summary>
    public int AddTableChangeCallback(tPL_TableChangeCallback callback,IntPtr dataobject)
    {
      var ltPL_AddTableChangeCallback = (tPL_AddTableChangeCallback)GetFunction<tPL_AddTableChangeCallback>(ePLProcs.ePL_AddTableChangeCallback);
      internalSuspendThread();
      int handle = ltPL_AddTableChangeCallback(callback,dataobject);
      internalResumeThread();
      return handle;
    }
    
    /// <summary>
    /// Remove a previously registered table change callback handler
    /// </summary>
    public void RemoveTableChangeCallback(int handle,IntPtr dataobject)
    {
      var ltPL_RemoveTableChangeCallback = (tPL_RemoveTableChangeCallback)GetFunction<tPL_RemoveTableChangeCallback>(ePLProcs.ePL_RemoveTableChangeCallback);
      internalSuspendThread();
      ltPL_RemoveTableChangeCallback(handle,dataobject);
      internalResumeThread();
    }    
    
    #endregion

    /// <summary>Pause callback enables a user provided function to be called
    /// every time Planimate becomes paused. This function is called in PL's
    /// thread context so do as little as possible and message to your main
    /// thread as required.
    /// </summary>
    /// <param name='callback_func'>Function to register as callback</param>
    /// <param name='userdata'>User data</param>
    public ePLRESULT RegisterPauseCallback(tPL_PauseCallback callback_func, IntPtr userdata)
    {
      var ltPL_RegisterPauseCallback = (tPL_RegisterPauseCallback)GetFunction<tPL_RegisterPauseCallback>(ePLProcs.ePL_RegisterPauseCallback);
      internalSuspendThread();
      var res = ltPL_RegisterPauseCallback(callback_func, userdata);
      internalResumeThread();
      return res;
    }

    /// <summary>Converts a string to a Planimate® value based on the specified Planimate® format</summary>
    /// <param name='str'>String to convert</param>
    /// <param name='val'>Pointer to value that will be returned (as double)</param>
    /// <param name='format'>Planimate® format of the string 'str'</param>
    public ePLRESULT ConvertStringToPLValue(string str,ref double val, eTFUnit format)
    {
      var ltPL_StringToValue = (tPL_StringToValue)GetFunction<tPL_StringToValue >(ePLProcs.ePL_StringToValue);
      internalSuspendThread();
      ePLRESULT res = ltPL_StringToValue(str,ref val,format);
      internalResumeThread();
      return res;
    }
    
    /// <summary>Gets Planimate to process a value to a string based on the
    /// specified Planimate® format</summary>
    /// <param name='value'>Value to convert</param>
    /// <param name='format'>Planimate format to use (Label and Text not supported)</param>
    /// <param name='result'>Reference to errror result code</param>
    public string ConvertPLValueToString(double value,
                                         eTFUnit format,
                                         out ePLRESULT result)
    {
      var ltPL_ValueToString = (tPL_ValueToString)GetFunction<tPL_ValueToString>(ePLProcs.ePL_ValueToString);
      internalSuspendThread();
      StringBuilder buffer = new StringBuilder(PLMaxString);
      result = ltPL_ValueToString(value,PLMaxString,buffer,format);
      internalResumeThread();
      
      if (result == ePLRESULT.PLR_OK)
        return buffer.ToString();
      else
        return null;
    }

    /// <summary>Gets Planimate to process a value to a string based on the
    /// specified Planimate® format</summary>
    /// <param name='value'>Value to convert</param>
    public string ConvertPLValueToString(double value,
                                         eTFUnit format)
    {
      ePLRESULT result;
      return ConvertPLValueToString(value,format,out result);
    }
    
    /// <summary>Converts a Planimate colour value or index to an ARGB string.
    /// It handles both Planimate palette indicies and ARGB formatted colours.
    /// Returns colour as #aarrggbb</summary>
    /// <param name='value'>Planimate colour value</param>
    public string PLValueToColor(double value)
    {
      var ltPL_ValueToColor = (tPL_ValueToColor)GetFunction<tPL_ValueToColor>(ePLProcs.ePL_ValueToColor);
      internalSuspendThread();
      StringBuilder buffer = new StringBuilder(10);  // #AARRGGBBnul
      ePLRESULT res = ltPL_ValueToColor(value,10,buffer);
      string str = buffer.ToString();
      internalResumeThread();
      return str;
    }

    /// <summary>Retrieve a table cell in its textually formatted form.
    //  Works for numeric formats, label and text formatted cells.
    /// <param name='data_object'>Pointer to Planimate® data object (table)</param>
    /// <param name='row'>Cell row index</param>
    /// <param name='col'>Cell column index</param>
    /// <param name='result'>Reference to errror result code</param>
    public string GetCellFormatted(IntPtr data_object,
                                   int    row,
                                   int    col,
                                   out ePLRESULT result)
    {
      var ltPL_GetCellFormatted = (tPL_GetCellFormatted)GetFunction<tPL_GetCellFormatted>(ePLProcs.ePL_GetCellFormatted);
      internalSuspendThread();
      StringBuilder buffer = new StringBuilder(PLMaxString);
      result = ltPL_GetCellFormatted(data_object, row, col,PLMaxString,buffer);
      internalResumeThread();

      if (result == ePLRESULT.PLR_OK)
        return buffer.ToString();
      else
        return null;
    }
      
    /// <summary>Return format class for a formatting unit mode
    /// <param name='format'>eTFUnit format</param>
    /// </summary>
    public eUnitClass GetFormatClass(eTFUnit format)
    {
      var ltPL_GetFormatClass = (tPL_GetFormatClass)GetFunction<tPL_GetFormatClass>(ePLProcs.ePL_GetFormatClass);
      return ltPL_GetFormatClass(format);
    }
    
    /// <summary>Retrieve a table cell in its textually formatted form.
    //  Works for numeric formats, label and text formatted cells.
    /// <param name='data_object'>Pointer to Planimate® data object (table)</param>
    /// <param name='row'>Cell row index</param>
    /// <param name='col'>Cell column index</param>
    public string GetCellFormatted(IntPtr data_object,
                                   int    row,
                                   int    col)
    {
      ePLRESULT result;
      return GetCellFormatted(data_object,row,col,out result);
    }
    
    /// <summary>
    ///  Return offset to apply to Planimate date-times to get unix (1-Jan-1970)
    /// based times.
    /// </summary>
    public double UnixTimeOffset()
    {
      // no need to suspend thread for this one
      var ltPL_GetSystemInfo = (tPL_GetSystemInfo)GetFunction<tPL_GetSystemInfo>(ePLProcs.ePL_GetSystemInfo);
      return ltPL_GetSystemInfo(ePLSysInfo.PLSI_UNIXTIMEOFFSET);
    }

    /// <summary>
    ///  Create a table returning its index or 0 if name collision
    /// </summary>
    public int CreateTable(string name,int scope=0)
    {
      // no need to suspend thread for this one
      var ltPL_CreateTable = (tPL_CreateTable)GetFunction<tPL_CreateTable>(ePLProcs.ePL_CreateTable);
      return ltPL_CreateTable(name,scope);
    }

    /// <summary>
    ///   Delete a table - assuming no code references!
    /// </summary>
    public int DeleteTable(int tableDO)
    {
      var ltPL_DeleteTable = (tPL_DeleteTable)GetFunction<tPL_DeleteTable>(ePLProcs.ePL_DeleteTable);
      return ltPL_DeleteTable(tableDO);
    }
    
    /// <summary>Converts a Planimate® timestamp (seconds) into a DateTime structure.</summary>
    /// <param name='timestamp'>Planimate® timestamp (seconds from offset)</param>
    public DateTime ConvertFromPLTimestamp(double timestamp)
    {
      timestamp += UnixTimeOffset();
      DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
      return origin.AddSeconds(timestamp);
    }

    /// <summary>Converts a DateTime structure into a Planimate® timestamp (seconds).</summary>
    /// <param name='date'>DateTime structure to convert</param>
    public double ConvertToPLTimestamp(DateTime date)
    {
      DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
      TimeSpan diff = date - origin;
      return diff.TotalSeconds - UnixTimeOffset();
    }

    /// <summary>
    /// Wait for model run to start
    /// </summary>
    public ePLRESULT WaitModelStarted(int timeout)
    {
      var fn = (tPL_WaitModelStarted)GetFunction<tPL_WaitModelStarted>(ePLProcs.ePL_WaitModelStarted);
      return fn(timeout);
    }

    /// <summary>
    /// Return true if in simulate/process BC mode (not paused)
    /// </summary>
    public bool Processing()
    {
      return ePLMode.MD_SIMULATE == (ePLMode)Convert.ToInt32(GetSystemInfo(ePLSysInfo.PLSI_ENGINESTATE));
    }

    /// <summary>
    /// Load model or reload PBA model
    /// </summary>
    public ePLRESULT LoadModel(string modelName,string dataSetFile)
    {
      var fn = (tPL_LoadModel)GetFunction<tPL_LoadModel>(ePLProcs.ePL_LoadModel); 
      return fn(modelName,dataSetFile);
    }
  }
  
  // class PLDataObject { }
}
