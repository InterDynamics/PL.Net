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
// for designer w/ Winforms
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace Planimate.Engine
{
  public partial class PLEngine : System.Windows.Forms.UserControl
  {
    /// <summary>Path to Planimate® engine DLL</summary>
    [
     Category("PLEngine"),
     Description("Compiled Planimate DLL path/filename"),
     Editor(typeof(FileNameEditor), typeof(UITypeEditor))
     ]
    public String dll_pathname
    {
      get;
      set;
    }

    /// <summary>Planimate interface core</summary>
    public PLEngineCore plengine = new PLEngineCore();

    public PLEngine()
    {
      InitializeComponent();
    }

    /// <summary>Override the OnResize handler to force a repaint message to be sent through to Planimate®</summary>
    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      this.Invalidate(true);
    }

    // WPF method untested
    // private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
    // {
    //  this.InvalidateVisual();
    // }

    public ePLRESULT InitPLEngine(string cmdline)
    {
      return plengine.InitPLEngine(dll_pathname,cmdline,this.Handle);
    }
  }
}
