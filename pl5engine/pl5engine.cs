/* Copyright 2009 Craig Chandler
 * 
 * 
 * This file is part of pl5engine.
 * 
 * pl5engine is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * Foobar is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public License
 * along with pl5engine.  If not, see <http://www.gnu.org/licenses/>.
 */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Runtime.InteropServices;
using System.Collections; // DllImport

namespace Planimate5engine
{
  #region pl5engine enums
    /// <summary>Planimate® system attributes the DLL can read (some can be set)</summary>
    public enum ePLSysInfo
    {
      /// <summary>This is the current simulation time as indicated by the system clock</summary>
      PLSI_CLOCK,
      /// <summary>Used to initiate or stop a fast advancement.
      /// Set the attribute to the time you wish to advance to</summary>
      PLSI_ADVANCETOTIME,
      PLSI_CURRENTPENDING,
      PLSI_ENGINESTATE,
      PLSI_CURRENTFILEVERSION,
      PLSI_OLDESTFILEVERSION,
      PLSI_LOADEDFILEVERSION,
      /// <summary>Returns the version of the engine (not the core Planimate® version)</summary>
      PLSI_DLLVERSION
    };

    /// <summary>Planimate® data object types</summary>
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

    /// <summary>Planimate® 5 engine return results</summary>
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

    /// <summary>Planimate® value formats</summary>
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
    }
  #endregion
  
  /// <summary>Implements a UserControl that contains a Planimate® engine</summary>
  public partial class pl5engine : System.Windows.Forms.UserControl, IDataSourceDesigner
  {
    #region kernel32 import
      [DllImport("kernel32.dll")]
      private static extern IntPtr LoadLibrary(string dllToLoad);

      [DllImport("kernel32.dll")]
      private static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

      [DllImport("kernel32.dll")]
      private static extern bool FreeLibrary(IntPtr hModule);
    #endregion

    #region pl5engine function delegations

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      private delegate IntPtr tPL_AppVersion();

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      private delegate void tPL_SetInstance(IntPtr handle);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      private delegate ePLRESULT tPL_Init([MarshalAs(UnmanagedType.LPStr)] string cmdline,
                                  IntPtr inplace_window);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      private delegate void tPL_Term(IntPtr handle);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      private delegate ePLRESULT tPL_LoadModel([MarshalAs(UnmanagedType.LPStr)] string cmdline);

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      private delegate double tGetSystemInfo(ePLSysInfo sysinfo_id);

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
        public delegate ePLRESULT tPL_BroadcastCallback(IntPtr broadcast, int no_params,
                                                        [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStr, SizeParamIndex = 1)] string[] tuple_names,
                                                        [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] double[] tuple_values);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate ePLRESULT tPL_RegisterBroadcastCallback(IntPtr broadcast, tPL_BroadcastCallback function);
      #endregion

      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      private delegate ePLRESULT tPL_StringToValue([MarshalAs(UnmanagedType.LPStr)] string str,
                                                   IntPtr val,   // double*
                                                   eTFUnit format);
    #endregion

    /// <summary>Override the OnResize handler to force a repaint message to be sent through to Planimate®</summary>
    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      this.Invalidate(true);
    }

    /// <summary>Path to Planimate® 5 engine DLL</summary>
    [
      Category("PL5Engine"),
      Description("Compiled Planimate DLL path/filename"),
      Editor(typeof(FileNameEditor), typeof(UITypeEditor))
    ]
    public String dll_pathname
    {
      get;
      set;
    }

    private IntPtr dll_handle = IntPtr.Zero;

    /// <summary>Engine constructor</summary>
    public pl5engine()
    {
      InitializeComponent();
    }

    private IntPtr DLLHandle()
    {
      if (dll_handle == IntPtr.Zero)
        dll_handle = LoadLibrary(dll_pathname);

      return dll_handle;
    }

    /// <summary>Returns the Planimate® version powering the engine.</summary>
    public string PlanimateVersion()
    {
      if (DLLHandle() == IntPtr.Zero)
        return "";
      IntPtr pAddressOfFunctionToCall = GetProcAddress(DLLHandle(), "PL_AppVersion");
      //oh dear, error handling here
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        return "";

      tPL_AppVersion ltPL_AppVersion = (tPL_AppVersion)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_AppVersion));
      return System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ltPL_AppVersion());
    }

    /// <summary>Initialise the Planimate® engine.</summary>
    /// <param name='cmdline'>can be a Planimate® commandline option or just ""</param>
    /// <param name='par_handle'>should be the handle of a window into which
    /// Planimate® will place a child window for its display.If NULL PL will create a new window</param>
    public ePLRESULT EngineInit(string cmdline, IntPtr par_handle)
    {
      if (DLLHandle() == IntPtr.Zero)
        return ePLRESULT.PLR_NOTFOUND;

      IntPtr pAddressOfFunctionToCall = GetProcAddress(DLLHandle(), "PL_SetInstance");
      //oh dear, error handling here
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        return ePLRESULT.PLR_NOTFOUND;

      tPL_SetInstance ltPL_SetInstance = (tPL_SetInstance)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_SetInstance));
      ltPL_SetInstance(DLLHandle());

      pAddressOfFunctionToCall = GetProcAddress(DLLHandle(), "PL_Init");
      //oh dear, error handling here
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        return ePLRESULT.PLR_NOTFOUND;

      tPL_Init ltPL_Init = (tPL_Init)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_Init));
      return ltPL_Init(cmdline, this.Handle);
    }

    // Data Object Functions

    /// <summary>Finds a Planimate® data object by name</summary>
    /// <param name='DO_name'>String name of the Planimate® data object</param>
    public IntPtr FindDataObjectName(string DO_name)
    {
      if (DLLHandle() == IntPtr.Zero)
        return IntPtr.Zero;

      IntPtr pAddressOfFunctionToCall = GetProcAddress(DLLHandle(), "PL_FindDataObjectName");
      //oh dear, error handling here
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        return IntPtr.Zero;

      tPL_FindDataObjectName ltPL_FindDataObjectName = (tPL_FindDataObjectName)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_FindDataObjectName));
      return ltPL_FindDataObjectName(DO_name);
    }

    /// <summary>Returns the data object type</summary>
    /// <param name='data_object'>Pointer to Planimate® data object</param>
    public eDOTypes DataObjectType(IntPtr data_object)
    {
      if (DLLHandle() == IntPtr.Zero)
        return (eDOTypes)(-1);

      IntPtr pAddressOfFunctionToCall = GetProcAddress(DLLHandle(), "PL_DataObjectType");
      //oh dear, error handling here
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        return (eDOTypes)(-1);

      tPL_DataObjectType ltPL_DataObjectType = (tPL_DataObjectType)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_DataObjectType));
      return ltPL_DataObjectType(data_object);
    }

    /// <summary>Returns the number of rows in a table data object</summary>
    /// <param name='data_object'>Pointer to Planimate® data object</param>
    public int Rows(IntPtr data_object)
    {
      if (DLLHandle() == IntPtr.Zero)
        return 0;

      IntPtr pAddressOfFunctionToCall = GetProcAddress(DLLHandle(), "PL_Rows");
      //oh dear, error handling here
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        return 0;

      tPL_Rows ltPL_Rows = (tPL_Rows)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_Rows));
      return ltPL_Rows(data_object);
    }

    /// <summary>Returns the number of columns in a table data object</summary>
    /// <param name='data_object'>Pointer to Planimate® data object</param>
    public int Columns(IntPtr data_object)
    {
      if (DLLHandle() == IntPtr.Zero)
        return 0;

      IntPtr pAddressOfFunctionToCall = GetProcAddress(DLLHandle(), "PL_Columns");
      //oh dear, error handling here
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        return 0;

      tPL_Columns ltPL_Columns = (tPL_Columns)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_Columns));
      return ltPL_Columns(data_object);
    }

    /// <summary>Returns the column name of the specified column</summary>
    /// <param name='data_object'>Pointer to Planimate® data object</param>
    /// <param name='column'>Index of the column</param>
    public string ColumnName(IntPtr data_object, int column)
    {
      if (DLLHandle() == IntPtr.Zero)
        return "";

      IntPtr pAddressOfFunctionToCall = GetProcAddress(DLLHandle(), "PL_ColumnName");
      //oh dear, error handling here
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        return "";

      tPL_ColumnName ltPL_ColumnName = (tPL_ColumnName)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_ColumnName));
      return System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ltPL_ColumnName(data_object, column));
    }

    /// <summary>Returns Planimate® value format of the specified column</summary>
    /// <param name='data_object'>Pointer to Planimate® data object</param>
    /// <param name='column'>Index of the column</param>
    public eTFUnit GetColumnFormat(IntPtr data_object, int column)
    {
      if (DLLHandle() == IntPtr.Zero)
        return (eTFUnit)(-1);

      IntPtr pAddressOfFunctionToCall = GetProcAddress(DLLHandle(), "PL_GetColumnFormat");
      //oh dear, error handling here
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        return (eTFUnit)(-1);

      tPL_GetColumnFormat ltPL_GetColumnFormat = (tPL_GetColumnFormat)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_GetColumnFormat));
      return ltPL_GetColumnFormat(data_object, column);
    }

    /// <summary>Returns Planimate® label list that the specified column is formatted to.</summary>
    /// <param name='data_object'>Pointer to Planimate® data object</param>
    /// <param name='column'>Index of the column</param>
    public IntPtr GetColumnLabelList(IntPtr data_object, int column)
    {
      if (DLLHandle() == IntPtr.Zero)
        return IntPtr.Zero;

      IntPtr pAddressOfFunctionToCall = GetProcAddress(DLLHandle(), "PL_GetColumnLabels");
      //oh dear, error handling here
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        return IntPtr.Zero;

      tPL_GetColumnLabels ltPL_GetColumnLabels = (tPL_GetColumnLabels)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_GetColumnLabels));
      return ltPL_GetColumnLabels(data_object, column);
    }

    /// <summary>Finds the column index based on the string name of the column</summary>
    /// <param name='data_object'>Pointer to Planimate® data object</param>
    /// <param name='col_name'>String column name of the target column</param>
    public int FindColumn(IntPtr data_object, string col_name)
    {
      if (DLLHandle() == IntPtr.Zero)
        return 0;

      IntPtr pAddressOfFunctionToCall = GetProcAddress(DLLHandle(), "PL_FindColumn");
      //oh dear, error handling here
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        return 0;

      tPL_FindColumn ltPL_FindColumn = (tPL_FindColumn)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_FindColumn));
      return ltPL_FindColumn(data_object, col_name);
    }

    /// <summary>Gets the value from a cell</summary>
    /// <param name='data_object'>Pointer to Planimate® data object (table)</param>
    /// <param name='row'>Cell row index</param>
    /// <param name='col'>Cell column index</param>
    public double GetCell(IntPtr data_object, int row, int col)
    {
      if (DLLHandle() == IntPtr.Zero)
        return 0;

      IntPtr pAddressOfFunctionToCall = GetProcAddress(DLLHandle(), "PL_GetCell");
      //oh dear, error handling here
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        return 0;

      tPL_GetCell ltPL_GetCell = (tPL_GetCell)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_GetCell));
      return ltPL_GetCell(data_object, row, col);
    }

    /// <summary>Sets the value of a cell</summary>
    /// <param name='data_object'>Pointer to Planimate® data object (table)</param>
    /// <param name='row'>Cell row index</param>
    /// <param name='col'>Cell column index</param>
    /// <param name='data'>Data to write to the cell</param>
    public ePLRESULT SetCell(IntPtr data_object, int row, int col, double data)
    {
      if (DLLHandle() == IntPtr.Zero)
        return 0;

      IntPtr pAddressOfFunctionToCall = GetProcAddress(DLLHandle(), "PL_SetCell");
      //oh dear, error handling here
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        return 0;

      tPL_SetCell ltPL_SetCell = (tPL_SetCell)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_SetCell));
      return ltPL_SetCell(data_object, row, col, data);
    }

    /// <summary>Sets the value of a Free Text cell</summary>
    /// <param name='data_object'>Pointer to Planimate® data object (table)</param>
    /// <param name='row'>Cell row index</param>
    /// <param name='col'>Cell column index</param>
    /// <param name='data'>Data to write to the cell (string)</param>
    public ePLRESULT SetCell(IntPtr data_object, int row, int col, string data)
    {
      if (DLLHandle() == IntPtr.Zero)
        return 0;

      IntPtr pAddressOfFunctionToCall = GetProcAddress(DLLHandle(), "PL_SetCellText");
      //oh dear, error handling here
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        return 0;

      tPL_SetCellText ltPL_SetCellText = (tPL_SetCellText)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_SetCellText));
      return ltPL_SetCellText(data_object, row, col, data);
    }

    /// <summary>Gets the text from a Free Text cell</summary>
    /// <param name='data_object'>Pointer to Planimate® data object (table)</param>
    /// <param name='row'>Cell row index</param>
    /// <param name='col'>Cell column index</param>
    public string GetCellText(IntPtr data_object, int row, int col)
    {
      if (DLLHandle() == IntPtr.Zero)
        return "";

      IntPtr pAddressOfFunctionToCall = GetProcAddress(DLLHandle(), "PL_GetCellText");
      //oh dear, error handling here
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        return "";

      tPL_GetCellText ltPL_GetCellText = (tPL_GetCellText)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_GetCellText));
      return System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ltPL_GetCellText(data_object, row, col));
    }

    // Label list functions
    /// <summary>Returns a label list object based on a string label list name</summary>
    /// <param name='list_name'>Label list name</param>
    public IntPtr GetNamedLabelList(string list_name)
    {
      if (DLLHandle() == IntPtr.Zero)
        return IntPtr.Zero;

      IntPtr pAddressOfFunctionToCall = GetProcAddress(DLLHandle(), "PL_GetNamedLabelList");
      //oh dear, error handling here
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        return IntPtr.Zero;

      tPL_GetNamedLabelList ltPL_GetNamedLabelList = (tPL_GetNamedLabelList)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_GetNamedLabelList));
      return ltPL_GetNamedLabelList(list_name);
    }

    /// <summary>Returns the label string based on a long(Int32) label index</summary>
    /// <param name='data_object'>Pointer to Planimate® data object (label list)</param>
    /// <param name='index'>Label index as integer</param>
    public string LookUpLabelLValue(IntPtr data_object, Int32 index)
    {
      if (DLLHandle() == IntPtr.Zero)
        return "";

      IntPtr pAddressOfFunctionToCall = GetProcAddress(DLLHandle(), "PL_LookUpLValue");
      //oh dear, error handling here
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        return "";

      tPL_LookUpLValue ltPL_LookUpLValue = (tPL_LookUpLValue)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_LookUpLValue));
      return System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ltPL_LookUpLValue(data_object, index));
    }

    /// <summary>Returns the label string based on a double label index</summary>
    /// <param name='data_object'>Pointer to Planimate® data object (label list)</param>
    /// <param name='index'>Label index as double</param>
    public string LookUpLabelDValue(IntPtr data_object, double index)
    {
      if (DLLHandle() == IntPtr.Zero)
        return "";

      IntPtr pAddressOfFunctionToCall = GetProcAddress(DLLHandle(), "PL_LookUpDValue");
      //oh dear, error handling here
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        return "";

      tPL_LookUpDValue ltPL_LookUpDValue = (tPL_LookUpDValue)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_LookUpDValue));
      return System.Runtime.InteropServices.Marshal.PtrToStringAnsi(ltPL_LookUpDValue(data_object, index));
    }

    /// <summary>Returns the label index based on a label string</summary>
    /// <param name='data_object'>Pointer to Planimate® data object (label list)</param>
    /// <param name='label'>Label string</param>
    public Int32 LookUpLabelIndex(IntPtr data_object, string label)
    {
      if (DLLHandle() == IntPtr.Zero)
        return -1;

      IntPtr pAddressOfFunctionToCall = GetProcAddress(DLLHandle(), "PL_LookUpLabel");
      //oh dear, error handling here
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        return -1;

      tPL_LookUpLabel ltPL_LookUpLabel = (tPL_LookUpLabel)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_LookUpLabel));
      return ltPL_LookUpLabel(data_object, label);
    }

    // Entire Column
    /// <summary>Returns a column as an array</summary>
    /// <param name='data_object'>Pointer to Planimate® data object (table)</param>
    /// <param name='column'>Index of column in DataTable</param>
    /// <param name='rows'>The number of rows to return in the array</param>
    /// <param name='into'>Array of double to place column in</param>
    public ePLRESULT GetColumn(IntPtr data_object, int column, int rows, double[] into)
    {
      if (DLLHandle() == IntPtr.Zero)
        return ePLRESULT.PLR_NOTFOUND;

      IntPtr pAddressOfFunctionToCall = GetProcAddress(DLLHandle(), "PL_GetColumn");
      //oh dear, error handling here
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        return ePLRESULT.PLR_NOTFOUND;

      tPL_GetColumn ltPL_GetColumn = (tPL_GetColumn)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_GetColumn));
      return ltPL_GetColumn(data_object, column, rows, into);
    }

    /// <summary>Sets an array to a column</summary>
    /// <param name='data_object'>Pointer to Planimate® data object (table)</param>
    /// <param name='column'>Index of column in DataTable</param>
    /// <param name='rows'>The number of rows to set from the array</param>
    /// <param name='to'>Array of doubles to place into the column</param>
    public ePLRESULT SetColumn(IntPtr data_object, int column, int rows, double[] to)
    {
      if (DLLHandle() == IntPtr.Zero)
        return ePLRESULT.PLR_NOTFOUND;

      IntPtr pAddressOfFunctionToCall = GetProcAddress(DLLHandle(), "PL_SetColumn");
      //oh dear, error handling here
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        return ePLRESULT.PLR_NOTFOUND;

      tPL_SetColumn ltPL_SetColumn = (tPL_SetColumn)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_SetColumn));
      return ltPL_SetColumn(data_object, column, rows, to);
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

                TimeSpan span = new TimeSpan(0,0,Convert.ToInt32(col[j].ToString()));
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
        if (DLLHandle() == IntPtr.Zero)
          return IntPtr.Zero;

        IntPtr pAddressOfFunctionToCall = GetProcAddress(DLLHandle(), "PL_GetBroadcastName");
        //oh dear, error handling here
        if (pAddressOfFunctionToCall == IntPtr.Zero)
          return IntPtr.Zero;

        tPL_GetBroadcastName ltPL_GetBroadcastName = (tPL_GetBroadcastName)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_GetBroadcastName));
        return ltPL_GetBroadcastName(BC_name);
      }

      /// <summary>Sends a broadcast into the engine based on broadcast object</summary>
      /// <param name='broadcast'>Pointer to broadcast object</param>
      public ePLRESULT SendBroadcast(IntPtr broadcast)
      {
        if (broadcast == IntPtr.Zero)
          return ePLRESULT.PLR_NOTFOUND;

        if (DLLHandle() == IntPtr.Zero)
          return ePLRESULT.PLR_NOTFOUND;

        IntPtr pAddressOfFunctionToCall = GetProcAddress(DLLHandle(), "PL_SendBroadcast");
        //oh dear, error handling here
        if (pAddressOfFunctionToCall == IntPtr.Zero)
          return ePLRESULT.PLR_NOTFOUND;

        tPL_SendBroadcast ltPL_SendBroadcast = (tPL_SendBroadcast)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_SendBroadcast));
        return ltPL_SendBroadcast(broadcast);
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
        if (broadcast == IntPtr.Zero)
          return ePLRESULT.PLR_NOTFOUND;

        if (DLLHandle() == IntPtr.Zero)
          return ePLRESULT.PLR_NOTFOUND;

        IntPtr pAddressOfFunctionToCall = GetProcAddress(DLLHandle(), "PL_SendBroadcastTuple");
        //oh dear, error handling here
        if (pAddressOfFunctionToCall == IntPtr.Zero)
          return ePLRESULT.PLR_NOTFOUND;

        tPL_SendBroadcastTuple ltPL_SendBroadcastTuple = (tPL_SendBroadcastTuple)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_SendBroadcastTuple));
        return ltPL_SendBroadcastTuple(broadcast, no_params, tuple_names, tuple_values);
      }

      /// <summary>Sends a broadcast into the engine based on broadcast object and include tuple data for the item</summary>
      /// <param name='broadcast'>Pointer to broadcast object</param>
      /// <param name='callback_func'>Function to register as callback</param>
      public ePLRESULT RegisterBroadcastCallback(IntPtr broadcast, tPL_BroadcastCallback callback_func)
      {
        if (DLLHandle() == IntPtr.Zero)
          return ePLRESULT.PLR_NOTFOUND;

        IntPtr pAddressOfFunctionToCall = GetProcAddress(DLLHandle(), "PL_RegisterBroadcastCallback");
        //oh dear, error handling here
        if (pAddressOfFunctionToCall == IntPtr.Zero)
          return ePLRESULT.PLR_NOTFOUND;

        tPL_RegisterBroadcastCallback ltPL_RegisterBroadcastCallback = (tPL_RegisterBroadcastCallback)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_RegisterBroadcastCallback));
        return ltPL_RegisterBroadcastCallback(broadcast, callback_func);
      }

    #endregion

    /// <summary>Converts a string to a Planimate® value based on the specified Planimate® format</summary>
    /// <param name='str'>String to convert</param>
    /// <param name='val'>Pointer to value that will be returned (as double)</param>
    /// <param name='format'>Planimate® format of the string 'str'</param>
    public ePLRESULT ConvertStringToPLValue(string str, IntPtr val, eTFUnit format)
    {
      if (DLLHandle() == IntPtr.Zero)
        return ePLRESULT.PLR_NOTFOUND;

      IntPtr pAddressOfFunctionToCall = GetProcAddress(DLLHandle(), "PL_StringToValue");
      //oh dear, error handling here
      if (pAddressOfFunctionToCall == IntPtr.Zero)
        return ePLRESULT.PLR_NOTFOUND;

      tPL_StringToValue ltPL_StringToValue = (tPL_StringToValue)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(tPL_StringToValue));
      
      return ltPL_StringToValue(str, val, format);
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

    #region IDataSourceDesigner Members

      public bool CanConfigure
    {
      get { throw new NotImplementedException(); }
    }

    public bool CanRefreshSchema
    {
      get { throw new NotImplementedException(); }
    }

    public void Configure()
    {
      throw new NotImplementedException();
    }

    DesignerDataSourceView IDataSourceDesigner.GetView(string viewName)
    {
      throw new NotImplementedException();
    }

    string[] IDataSourceDesigner.GetViewNames()
    {
      throw new NotImplementedException();
    }

    public void RefreshSchema(bool preferSilent)
    {
      throw new NotImplementedException();
    }

    public void ResumeDataSourceEvents()
    {
      throw new NotImplementedException();
    }

    public event EventHandler SchemaRefreshed;

    public event EventHandler DataSourceChanged;

    public void SuppressDataSourceEvents()
    {
      throw new NotImplementedException();
    }

    #endregion
  }

  public class pl5DesignerDataSourceView : DesignerDataSourceView
  {
    private pl5engine _owner;

    public pl5DesignerDataSourceView(pl5engine owner, string viewName)
      : base(owner, viewName)
    {
      _owner = owner;
    }

    public override IDataSourceViewSchema Schema
    {
      get
      {
        TypeSchema ts = new TypeSchema(typeof(string));
        return ts.GetViews()[0];
      }
    }
  }

  class PLDataObject { }
}
