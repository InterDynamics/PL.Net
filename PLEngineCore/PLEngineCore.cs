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

namespace Planimate.Engine
{
  #region PL.Net enums
  /// <summary>
  /// Planimate® system attributes the DLL can read (some can be set)
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
    PLSI_PAUSEAFTERADVANCE
  };

  /// <summary>
  /// Major mode for Planimate® engine
  /// </summary>
  public enum ePLMode
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
  /// Planimate® data object types
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
  /// Planimate® engine return results
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
  /// Planimate® value formats
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
    /// <summary>this counts unit modes</summary>
    UNIT_MODECOUNT,
    /// <summary>special case - must be 255 and last</summary>
    UNIT_NULL = 255
  };

  /// <summary>
  /// Thread proc status for Planimate DLL loader class
  /// This is different to the PL Run Engine State 
  /// NOTE:The PL Engine is only valid when thread is in PLT_Running state
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
  /// Function enum for PL_GetProc() which you can use to retrieve function
  /// pointers instead of LoadLibrary
  /// </summary>
  public enum ePLProcs
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
  
  /// <summary>
  /// Run commands for use within the PL_Run() call
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
                                                 IntPtr val,   // double*
                                                 eTFUnit format);
    #endregion

    private tPL_GetProc ltPL_GetProc             = null;
    private tPL_SuspendThread ltPL_SuspendThread = null;
    private tPL_ResumeThread ltPL_ResumeThread   = null;
    private tPL_TermThread   ltPL_TermThread     = null;

    private IntPtr[] ProcTable;
    private int      suspendLevel = 0;

    /// <summary>Engine constructor</summary>
    public PLEngineCore()
    {
    }

    /// <summary>
    /// Initialise the Planimate® engine for use when embedding PL in
    /// a dotNET application
    /// </summary>
    public ePLRESULT InitPLEngine(String dll_pathname,string cmdline,IntPtr window_handle)
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
      
      ltPL_InitThread(dll_handle, cmdline, window_handle);
      ltPL_WaitThreadRunning();

      // check PL supports all the procs we expect - will throw exception on fail
      // (PL returns NULL on bad proc as of 8.57.2)
      GetFunction(ePLProcs.ePL_PROCCOUNT-1);
      return ePLRESULT.PLR_OK;
    }

    /// <summary>
    /// Initialise this for use with an existing Planimate which is calling
    /// C# using the PLCLR bridge DLL. PLCLR attempts to call
    /// the instantiated class which should call this if the engine is required.
    /// </summary>
    public void InitPLEngine(IntPtr[] pl_proctable)
    {
      if (pl_proctable.Length < (int)ePLProcs.ePL_PROCCOUNT)
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
    private IntPtr GetFunction(ePLProcs function)
    {    
      if (ProcTable[(int)function] == IntPtr.Zero)
        if (ltPL_GetProc != null)
          ProcTable[(int)function] = ltPL_GetProc(function);
        else
          throw new PLBindFailure(); // should have been caught on init
          
      return ProcTable[(int)function];
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
    #endregion

    /// <summary>Returns the Planimate® version powering the engine.</summary>
    public string PlanimateVersion()
    {
      IntPtr pl_version = ((tPL_AppVersion)GetFunction<tPL_AppVersion>(ePLProcs.ePL_AppVersion))();
  
      return System.Runtime.InteropServices.Marshal.PtrToStringAnsi(pl_version);
    }

    /// <summary>Gets Planimate® System info. Times are in seconds with 0 = the run start date</summary>
    /// <param name='sysinfo_id'>System info id from ePLSysInfo.</param>
    public double GetSystemInfo(ePLSysInfo sysinfo_id)
    {
      IntPtr pAddressOfFunctionToCall = GetFunction(ePLProcs.ePL_GetSystemInfo);
      tPL_GetSystemInfo ltPL_GetSystemInfo = (tPL_GetSystemInfo)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_GetSystemInfo));
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
      IntPtr pAddressOfFunctionToCall = GetFunction(ePLProcs.ePL_SetSystemInfo);
      tPL_SetSystemInfo ltPL_SetSystemInfo = (tPL_SetSystemInfo)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_SetSystemInfo));
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
      IntPtr pAddressOfFunctionToCall = GetFunction(ePLProcs.ePL_FindDataObjectName);
      tPL_FindDataObjectName ltPL_FindDataObjectName = (tPL_FindDataObjectName)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_FindDataObjectName));
      internalSuspendThread();
      IntPtr res = ltPL_FindDataObjectName(DO_name);
      internalResumeThread();
      return res;
    }

    /// <summary>Returns the data object type</summary>
    /// <param name='data_object'>Pointer to Planimate® data object</param>
    public eDOTypes DataObjectType(IntPtr data_object)
    {
      IntPtr pAddressOfFunctionToCall = GetFunction(ePLProcs.ePL_DataObjectType);
      tPL_DataObjectType ltPL_DataObjectType = (tPL_DataObjectType)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_DataObjectType));
      internalSuspendThread();
      eDOTypes res = ltPL_DataObjectType(data_object);
      internalResumeThread();
      return res;
    }
    #endregion

    #region Table Functions
    /// <summary>Returns the number of rows in a table data object</summary>
    /// <param name='data_object'>Pointer to Planimate® data object</param>
    public int Rows(IntPtr data_object)
    {
      IntPtr pAddressOfFunctionToCall = GetFunction(ePLProcs.ePL_Rows);
      tPL_Rows ltPL_Rows = (tPL_Rows)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_Rows));
      internalSuspendThread();
      int res = ltPL_Rows(data_object);
      internalResumeThread();
      return res;
    }

    /// <summary>Returns the number of columns in a table data object</summary>
    /// <param name='data_object'>Pointer to Planimate® data object</param>
    public int Columns(IntPtr data_object)
    {
      IntPtr pAddressOfFunctionToCall = GetFunction(ePLProcs.ePL_Columns);
      tPL_Columns ltPL_Columns = (tPL_Columns)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_Columns));
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
      IntPtr pAddressOfFunctionToCall = GetFunction(ePLProcs.ePL_ColumnName);
      tPL_ColumnName ltPL_ColumnName = (tPL_ColumnName)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_ColumnName));
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
      IntPtr pAddressOfFunctionToCall = GetFunction(ePLProcs.ePL_GetColumnFormat);
      tPL_GetColumnFormat ltPL_GetColumnFormat = (tPL_GetColumnFormat)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_GetColumnFormat));
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
      IntPtr pAddressOfFunctionToCall = GetFunction(ePLProcs.ePL_GetColumnLabels);
      tPL_GetColumnLabels ltPL_GetColumnLabels = (tPL_GetColumnLabels)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_GetColumnLabels));
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
      IntPtr pAddressOfFunctionToCall = GetFunction(ePLProcs.ePL_FindColumn);
      tPL_FindColumn ltPL_FindColumn = (tPL_FindColumn)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_FindColumn));
      internalSuspendThread();
      int res = ltPL_FindColumn(data_object, col_name);
      internalResumeThread();
      return res;
    }

    /// <summary>Gets the value from a cell</summary>
    /// <param name='data_object'>Pointer to Planimate® data object (table)</param>
    /// <param name='row'>Cell row index</param>
    /// <param name='col'>Cell column index</param>
    public double GetCell(IntPtr data_object, int row, int col)
    {
      IntPtr pAddressOfFunctionToCall = GetFunction(ePLProcs.ePL_GetCell);
      tPL_GetCell ltPL_GetCell = (tPL_GetCell)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_GetCell));
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
      IntPtr pAddressOfFunctionToCall = GetFunction(ePLProcs.ePL_SetCell);
      tPL_SetCell ltPL_SetCell = (tPL_SetCell)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_SetCell));
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
      IntPtr pAddressOfFunctionToCall = GetFunction(ePLProcs.ePL_SetCellText);
      tPL_SetCellText ltPL_SetCellText = (tPL_SetCellText)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_SetCellText));
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
      IntPtr pAddressOfFunctionToCall = GetFunction(ePLProcs.ePL_GetCellText);
      tPL_GetCellText ltPL_GetCellText = (tPL_GetCellText)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_GetCellText));
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
      IntPtr pAddressOfFunctionToCall = GetFunction(ePLProcs.ePL_GetNamedLabelList);
      tPL_GetNamedLabelList ltPL_GetNamedLabelList = (tPL_GetNamedLabelList)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_GetNamedLabelList));
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
      IntPtr pAddressOfFunctionToCall = GetFunction(ePLProcs.ePL_LookUpLValue);
      tPL_LookUpLValue ltPL_LookUpLValue = (tPL_LookUpLValue)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_LookUpLValue));
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
      IntPtr pAddressOfFunctionToCall = GetFunction(ePLProcs.ePL_LookUpDValue);
      tPL_LookUpDValue ltPL_LookUpDValue = (tPL_LookUpDValue)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_LookUpDValue));
      internalSuspendThread();
      string res = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ltPL_LookUpDValue(data_object, index));
      internalResumeThread();
      return res;
    }

    /// <summary>Returns the label index based on a label string</summary>
    /// <param name='data_object'>Pointer to Planimate® data object (label list)</param>
    /// <param name='label'>Label string</param>
    public Int32 LookUpLabelIndex(IntPtr data_object, string label)
    {
      IntPtr pAddressOfFunctionToCall = GetFunction(ePLProcs.ePL_LookUpLabel);
      tPL_LookUpLabel ltPL_LookUpLabel = (tPL_LookUpLabel)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_LookUpLabel));
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
      IntPtr pAddressOfFunctionToCall = GetFunction(ePLProcs.ePL_GetColumn);
      tPL_GetColumn ltPL_GetColumn = (tPL_GetColumn)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_GetColumn));
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
      IntPtr pAddressOfFunctionToCall = GetFunction(ePLProcs.ePL_SetColumn);
      tPL_SetColumn ltPL_SetColumn = (tPL_SetColumn)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_SetColumn));
      internalSuspendThread();
      ePLRESULT res = ltPL_SetColumn(data_object, column, rows, to);
      internalResumeThread();
      return res;
    }

    /// <summary>Updates a DataTable object with data from a Planimate® data object
    /// table and returns it as a DataTable. If the DataTable is specified to be formatted,
    /// all columns will be correct .NET data formats. Unformatted data is formatted as type
    /// Double</summary>
    /// <param name='pl_table'>DataTable that will be updated</param>
    /// <param name='data_object'>Pointer to Planimate® data object (table)</param>
    /// <param name='formatted'>Specifies if the returned DataTable should be formatted. True = format</param>
    public ePLRESULT UpdateDataTable(ref DataTable pl_table, IntPtr data_object, Boolean formatted)
    {
      ePLRESULT res = ePLRESULT.PLR_OK;

      int columns = Columns(data_object);
      int rows = Rows(data_object);

      pl_table.Clear();
      pl_table.Columns.Clear();

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
                    pl_table.Columns.Add(ColumnName(data_object, i), typeof(DateTime));
                    for (int j = 0; j < rows; j++)
                      {
                        if (i == 0)
                          pl_table.Rows.Add();

                        DateTime dt = new DateTime(0);
                        dt = ConvertFromPLTimestamp(col[j]);
                        pl_table.Rows[j][i] = dt;
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
                    pl_table.Columns.Add(ColumnName(data_object, i), typeof(TimeSpan));
                    for (int j = 0; j < rows; j++)
                      {
                        if (i == 0)
                          pl_table.Rows.Add();

                        TimeSpan span = new TimeSpan(0, 0, Convert.ToInt32(col[j].ToString()));
                        pl_table.Rows[j][i] = span;
                      }
                    break;
                  case eTFUnit.UNIT_LABEL:
                    IntPtr llist = GetColumnLabelList(data_object, i);
                    if (llist == IntPtr.Zero)
                      return ePLRESULT.PLR_BADFORMAT;

                    pl_table.Columns.Add(ColumnName(data_object, i), typeof(string));

                    res = GetColumn(data_object, i, rows, col);
                    if (res != ePLRESULT.PLR_OK)
                      return res;

                    for (int j = 0; j < rows; j++)
                      {
                        if (i == 0)
                          pl_table.Rows.Add();

                        Int32 val = Convert.ToInt32(col[j].ToString());
                        pl_table.Rows[j][i] = LookUpLabelLValue(llist, val);
                      }
                    break;
                  case eTFUnit.UNIT_FREETEXT:
                    pl_table.Columns.Add(ColumnName(data_object, i), typeof(string));

                    res = GetColumn(data_object, i, rows, col);
                    if (res != ePLRESULT.PLR_OK)
                      return res;

                    for (int j = 0; j < rows; j++)
                      {
                        if (i == 0)
                          pl_table.Rows.Add();

                        pl_table.Rows[j][i] = GetCellText(data_object, j, i);
                      }
                    break;
                  default:
                    pl_table.Columns.Add(ColumnName(data_object, i), typeof(double));

                    res = GetColumn(data_object, i, rows, col);
                    if (res != ePLRESULT.PLR_OK)
                      return res;

                    for (int j = 0; j < rows; j++)
                      {
                        if (i == 0)
                          pl_table.Rows.Add();

                        pl_table.Rows[j][i] = col[j];
                      }
                    break;

                };

            }
        }
      else  // unformatted (all columns doubles)
        {
          for (int i = 0; i < columns; i++)
            {
              pl_table.Columns.Add(ColumnName(data_object, i), typeof(double));

              res = GetColumn(data_object, i, rows, col);
              if (res != ePLRESULT.PLR_OK)
                return res;

              for (int j = 0; j < rows; j++)
                {
                  if (i == 0)
                    pl_table.Rows.Add();

                  pl_table.Rows[j][i] = col[j];
                }
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
    public ePLRESULT UpdateDataTable(ref DataTable rTable, string DO_name, Boolean formatted)
    {
      IntPtr PL_data_object = FindDataObjectName(DO_name);
      if (PL_data_object == IntPtr.Zero)
        return ePLRESULT.PLR_NOTFOUND;

      return UpdateDataTable(ref rTable, PL_data_object, formatted);
    }

    /// <summary>Gets a Planimate® data object table and returns it as a DataTable.
    /// If the DataTable is specified to be formatted, all columns will be correct .NET data formats.
    /// Unformatted data is formatted as type Double</summary>
    /// <param name='data_object'>Pointer to Planimate® data object (table)</param>
    /// <param name='formatted'>Specifies if the returned DataTable should be formatted. True = format</param>
    public DataTable GetDataTable(IntPtr data_object, Boolean formatted)
    {
      ePLRESULT res = ePLRESULT.PLR_OK;

      int columns = Columns(data_object);
      int rows = Rows(data_object);

      DataTable pl_table = new DataTable();

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
                      return null;
                    pl_table.Columns.Add(ColumnName(data_object, i), typeof(DateTime));
                    for (int j = 0; j < rows; j++)
                      {
                        if (i == 0)
                          pl_table.Rows.Add();

                        DateTime dt = new DateTime(0);
                        dt = ConvertFromPLTimestamp(col[j]);
                        pl_table.Rows[j][i] = dt;
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
                      return null;
                    pl_table.Columns.Add(ColumnName(data_object, i), typeof(TimeSpan));
                    for (int j = 0; j < rows; j++)
                      {
                        if (i == 0)
                          pl_table.Rows.Add();

                        TimeSpan span = new TimeSpan(0, 0, Convert.ToInt32(col[j].ToString()));
                        pl_table.Rows[j][i] = span;
                      }
                    break;
                  case eTFUnit.UNIT_LABEL:
                    IntPtr llist = GetColumnLabelList(data_object, i);
                    if (llist == IntPtr.Zero)
                      return null;

                    pl_table.Columns.Add(ColumnName(data_object, i), typeof(string));

                    res = GetColumn(data_object, i, rows, col);
                    if (res != ePLRESULT.PLR_OK)
                      return null;

                    for (int j = 0; j < rows; j++)
                      {
                        if (i == 0)
                          pl_table.Rows.Add();

                        Int32 val = Convert.ToInt32(col[j].ToString());
                        pl_table.Rows[j][i] = LookUpLabelLValue(llist, val);
                      }
                    break;
                  case eTFUnit.UNIT_FREETEXT:
                    pl_table.Columns.Add(ColumnName(data_object, i), typeof(string));

                    res = GetColumn(data_object, i, rows, col);
                    if (res != ePLRESULT.PLR_OK)
                      return null;

                    for (int j = 0; j < rows; j++)
                      {
                        if (i == 0)
                          pl_table.Rows.Add();

                        pl_table.Rows[j][i] = GetCellText(data_object, j, i);
                      }
                    break;
                  default:
                    pl_table.Columns.Add(ColumnName(data_object, i), typeof(double));

                    res = GetColumn(data_object, i, rows, col);
                    if (res != ePLRESULT.PLR_OK)
                      return null;

                    for (int j = 0; j < rows; j++)
                      {
                        if (i == 0)
                          pl_table.Rows.Add();

                        pl_table.Rows[j][i] = col[j];
                      }
                    break;

                };

            }
        }
      else  // unformatted (all columns doubles)
        {
          for (int i = 0; i < columns; i++)
            {
              pl_table.Columns.Add(ColumnName(data_object, i), typeof(double));

              res = GetColumn(data_object, i, rows, col);
              if (res != ePLRESULT.PLR_OK)
                return null;

              for (int j = 0; j < rows; j++)
                {
                  if (i == 0)
                    pl_table.Rows.Add();

                  pl_table.Rows[j][i] = col[j];
                }
            }
        }
      return pl_table;
    }

    /// <summary>Finds a Planimate® data object by name and returns it as a DataTable.
    /// If the DataTable is specified to be formatted, all columns will be correct .NET data formats.
    /// Unformatted data is formatted as type Double</summary>
    /// <param name='DO_name'>String name of the Planimate® table data object</param>
    /// <param name='formatted'>Specifies if the returned DataTable should be formatted. True = format</param>
    public DataTable GetDataTable(string DO_name, Boolean formatted)
    {
      IntPtr PL_data_object = FindDataObjectName(DO_name);
      if (PL_data_object == IntPtr.Zero)
        return null;

      return GetDataTable(PL_data_object, formatted);
    }

    /// <summary>Sets a Planimate® data object table and from a DataTable.</summary>
    /// <param name='data_table'>Reference to the DataTable to be written to Planimate®</param>
    /// <param name='data_object'>Pointer to Planimate® data object (table)</param>
    public ePLRESULT SetDataTable(ref DataTable data_table, IntPtr data_object)
    {
      if (data_table == null || data_object == IntPtr.Zero)
        return ePLRESULT.PLR_BADINDEX;

      for (int i = 0; i < data_table.Columns.Count; i++)
        {
          for (int j = 0; j < data_table.Rows.Count; j++)
            {
              if (data_table.Columns[i].DataType == typeof(double))
                {
                  SetCell(data_object, j, i, (double)data_table.Rows[j][i]);
                }
              else if (data_table.Columns[i].DataType == typeof(string))
                {
                  eTFUnit col_unit = GetColumnFormat(data_object, i);
                  if (col_unit == eTFUnit.UNIT_LABEL)
                    {
                      IntPtr llist = GetColumnLabelList(data_object, i);
                      if (llist == IntPtr.Zero)
                        return ePLRESULT.PLR_BADFORMAT;

                      SetCell(data_object, j, i, Convert.ToDouble(LookUpLabelIndex(llist, (string)data_table.Rows[j][i])));
                    }
                  else
                    SetCell(data_object, j, i, (string)data_table.Rows[j][i]);
                }
              else if (data_table.Columns[i].DataType == typeof(DateTime))
                {
                  SetCell(data_object, j, i, ConvertToPLTimestamp((DateTime)data_table.Rows[j][i]));
                }
              else if (data_table.Columns[i].DataType == typeof(TimeSpan))
                {
                  TimeSpan span = (TimeSpan)data_table.Rows[j][i];
                  SetCell(data_object, j, i, span.TotalSeconds);
                }
              else
                {
                  return ePLRESULT.PLR_BADFORMAT;
                }
            }
        }
      return ePLRESULT.PLR_OK;
    }

    #region Broadcast Functions
    /// <summary>Returns a broadcast object based on a string broadcast name</summary>
    /// <param name='BC_name'>Name of broadcast (string)</param>
    public IntPtr FindBroadcastName(string BC_name)
    {
      IntPtr pAddressOfFunctionToCall = GetFunction(ePLProcs.ePL_GetBroadcastName);
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        return IntPtr.Zero;

      tPL_GetBroadcastName ltPL_GetBroadcastName = (tPL_GetBroadcastName)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_GetBroadcastName));
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
          IntPtr pAddressOfFunctionToCall = GetFunction(ePLProcs.ePL_SendBroadcast);
          if (pAddressOfFunctionToCall != IntPtr.Zero)
            {
              tPL_SendBroadcast ltPL_SendBroadcast = (tPL_SendBroadcast)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_SendBroadcast));
              internalSuspendThread();
              res = ltPL_SendBroadcast(broadcast);
              internalResumeThread();
            }
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
          IntPtr pAddressOfFunctionToCall = GetFunction(ePLProcs.ePL_SendBroadcastTuple);
          if (pAddressOfFunctionToCall != IntPtr.Zero)
            {
              tPL_SendBroadcastTuple ltPL_SendBroadcastTuple = (tPL_SendBroadcastTuple)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_SendBroadcastTuple));
              internalSuspendThread();
              res = ltPL_SendBroadcastTuple(broadcast, no_params, tuple_names, tuple_values);
              internalResumeThread();
            }
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
      IntPtr pAddressOfFunctionToCall = GetFunction(ePLProcs.ePL_GetBroadcastName);
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        return ePLRESULT.PLR_NOTFOUND;

      tPL_GetBroadcastName ltPL_GetBroadcastName = (tPL_GetBroadcastName)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_GetBroadcastName));

      pAddressOfFunctionToCall = GetFunction(ePLProcs.ePL_SendBroadcastTuple);
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        return ePLRESULT.PLR_NOTFOUND;

      tPL_SendBroadcastTuple ltPL_SendBroadcastTuple = (tPL_SendBroadcastTuple)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_SendBroadcastTuple));

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
      ePLRESULT res = ePLRESULT.PLR_NOTFOUND;
      IntPtr pAddressOfFunctionToCall = GetFunction(ePLProcs.ePL_RegisterBroadcastCallback);
      if (pAddressOfFunctionToCall != IntPtr.Zero)
        {
          internalSuspendThread();
          tPL_RegisterBroadcastCallback ltPL_RegisterBroadcastCallback = (tPL_RegisterBroadcastCallback)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_RegisterBroadcastCallback));
          res = ltPL_RegisterBroadcastCallback(broadcast, callback_func);
          internalResumeThread();
        }
      return res;
    }

    /// <summary>
    /// Register broadcast callback using broadcast name.
    /// </summary>
    /// <param name='BC_name'>Pointer to broadcast object</param>
    /// <param name='callback_func'>Function to register as callback</param>
    public ePLRESULT RegisterBroadcastCallback(string BC_name, tPL_BroadcastCallback callback_func)
    {
      IntPtr pAddressOfFunctionToCall = GetFunction(ePLProcs.ePL_GetBroadcastName);
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        return ePLRESULT.PLR_NOTFOUND;

      tPL_GetBroadcastName ltPL_GetBroadcastName = (tPL_GetBroadcastName)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_GetBroadcastName));

      pAddressOfFunctionToCall = GetFunction(ePLProcs.ePL_RegisterBroadcastCallback);
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        return ePLRESULT.PLR_NOTFOUND;

      tPL_RegisterBroadcastCallback ltPL_RegisterBroadcastCallback = (tPL_RegisterBroadcastCallback)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_RegisterBroadcastCallback));

      ePLRESULT res = ePLRESULT.PLR_NOTFOUND;
      internalSuspendThread();
      IntPtr broadcast = ltPL_GetBroadcastName(BC_name);
      if (broadcast != IntPtr.Zero)
        res = ltPL_RegisterBroadcastCallback(broadcast, callback_func);

      internalResumeThread();
      return res;
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
      ePLRESULT res = ePLRESULT.PLR_NOTFOUND;
      IntPtr pAddressOfFunctionToCall = GetFunction(ePLProcs.ePL_RegisterPauseCallback);
      if (pAddressOfFunctionToCall != IntPtr.Zero)
        {
          tPL_RegisterPauseCallback ltPL_RegisterPauseCallback = (tPL_RegisterPauseCallback)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_RegisterPauseCallback));
          internalSuspendThread();
          res = ltPL_RegisterPauseCallback(callback_func, userdata);
          internalResumeThread();
        }
      return res;
    }

    /// <summary>Converts a string to a Planimate® value based on the specified Planimate® format</summary>
    /// <param name='str'>String to convert</param>
    /// <param name='val'>Pointer to value that will be returned (as double)</param>
    /// <param name='format'>Planimate® format of the string 'str'</param>
    public ePLRESULT ConvertStringToPLValue(string str, IntPtr val, eTFUnit format)
    {
      IntPtr pAddressOfFunctionToCall = GetFunction(ePLProcs.ePL_StringToValue);
      tPL_StringToValue ltPL_StringToValue = (tPL_StringToValue)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_StringToValue));
      internalSuspendThread();
      ePLRESULT res = ltPL_StringToValue(str, val, format);
      internalResumeThread();
      return res;
    }

    /// <summary>Converts a Planimate® timestamp (seconds) into a DateTime structure.</summary>
    /// <param name='timestamp'>Planimate® timestamp (seconds from offset)</param>
    public DateTime ConvertFromPLTimestamp(double timestamp)
    {
      double[] ref_time = new double[1];
      IntPtr double_ref = Marshal.AllocHGlobal(Marshal.SizeOf(ref_time[0]));
      DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);

      ePLRESULT res = ConvertStringToPLValue("1 Jan 1970 00:00 00", double_ref, eTFUnit.UNIT_ABSTIME);
      if (res == ePLRESULT.PLR_OK)
        {
          Marshal.Copy(double_ref, ref_time, 0, 1);
          return origin.AddSeconds(timestamp - ref_time[0]);
        }
      else
        return origin;
    }

    /// <summary>Converts a DateTime structure into a Planimate® timestamp (seconds).</summary>
    /// <param name='date'>DateTime structure to convert</param>
    public double ConvertToPLTimestamp(DateTime date)
    {
      double[] ref_time = new double[1];
      IntPtr double_ref = Marshal.AllocHGlobal(Marshal.SizeOf(ref_time[0]));
      DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);

      ePLRESULT res = ConvertStringToPLValue("1 Jan 1970 00:00 00", double_ref, eTFUnit.UNIT_ABSTIME);
      if (res == ePLRESULT.PLR_OK)
        {
          Marshal.Copy(double_ref, ref_time, 0, 1);
          TimeSpan diff = date - origin;
          return Math.Floor(diff.TotalSeconds + ref_time[0]);
        }
      else
        return 0;
    }
  }
    
  class PLDataObject { }
}
