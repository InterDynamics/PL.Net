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
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Planimate.Engine;

namespace WindowsFormsApplication1
{
  public partial class Form1 : Form
  {
    private Planimate.Engine.PLEngineCore plEngine1;

    public Form1()
    {
      InitializeComponent();
      ePLRESULT init_result = plEngineForm.InitPLEngine("demo.mdl");
      if (init_result != ePLRESULT.PLR_OK)
        {
          MessageBox.Show(String.Format("Failed to load Planimate, code {0}",(int)init_result));
        }
      else
        {
          plEngine1 = plEngineForm.plengine;
          dataGridView1.DataSource = plEngine1.GetDataTable("Input_1", true,true);
          dataGridView2.DataSource = plEngine1.GetDataTable("Formats", true,true);
          DataTable dt = (DataTable)dataGridView2.DataSource;
          plEngine1.SetFromDataTable(dt, plEngine1.FindDataObjectName("formats_copy"));
        }
    }

    
    public ePLRESULT broadcast_callback_function(IntPtr broadcast, int no_params, string[] tuple_names, double[] tuple_values,IntPtr user_data)
    {
      // Callback function - this is invoked in Planimate's thread
      String report = "Broadcast callback";

      for (int i=0;i<no_params;i++)
        {
          report += "\n";
          report += tuple_names[i] + "=" + tuple_values[i];
        }

    MessageBox.Show(report);
    return ePLRESULT.PLR_OK;
  }

    PLEngineCore.tPL_BroadcastCallback callback = null;

    private void button3_Click(object sender, EventArgs e)
    {
      String bc_name = "Process";
      IntPtr broadcast = plEngine1.FindBroadcastName(bc_name);
      if (broadcast == IntPtr.Zero)
        MessageBox.Show(String.Format("The model does not have a broadcast called {0}",bc_name));
      else
        if (callback == null)
          {
            // 1) only register callback once to protect against PL (in own thread)
            //    being in process of doing callback to old handler
            // 2) use KeepAlive to avoid GC disposing of apparently unused object
            
            callback = new PLEngineCore.tPL_BroadcastCallback(broadcast_callback_function);
            GC.KeepAlive(callback);
            ePLRESULT reg_res = plEngine1.RegisterBroadcastCallback(broadcast, callback);
          }

      double[] values = new double[] { Convert.ToDouble(numericUpDown1.Value),999.0 };
      string[] names  = new string[] { "_height", "_width" };
      
      ePLRESULT brd_res = plEngine1.SendBroadcast(broadcast,names.Length, names,values);
    }

    private void button1_Click(object sender, EventArgs e)
    {
      dataGridView2.DataSource = plEngine1.GetDataTable("Formats", true,true);
    }

    private void button2_Click(object sender, EventArgs e)
    {
      DataTable dt = (DataTable)dataGridView2.DataSource;
      plEngine1.SetFromDataTable(dt, plEngine1.FindDataObjectName("formats_copy"));
    }
  }
}
