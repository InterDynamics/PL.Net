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

    public Form1()
    {
      InitializeComponent();
      PLLoader1.InitPLLoader();
      PLLoader1.CreatePL("demo.mdl /debugdll");
      dataGridView1.DataSource = PLLoader1.GetEngine().GetDataTable("Input_1", true);
      dataGridView2.DataSource = PLLoader1.GetEngine().GetDataTable("Formats", true);
      DataTable dt = (DataTable)dataGridView2.DataSource;
      PLLoader1.GetEngine().SetDataTable(ref dt, PLLoader1.GetEngine().FindDataObjectName("formats_copy"));
    }

    public ePLRESULT broadcast_callback_function(IntPtr broadcast, int no_params, string[] tuple_names, double[] tuple_values)
    {
      MessageBox.Show("Broadcast Callback");
      return ePLRESULT.PLR_OK;
    }

    private void button3_Click(object sender, EventArgs e)
    {
      IntPtr broadcast = PLLoader1.GetEngine().FindBroadcastName("Process");
      if (broadcast == IntPtr.Zero)
      {
        MessageBox.Show("Broadcast Not Found");
        return;
      }
      PLEngine.tPL_BroadcastCallback callback;
      callback = new PLEngine.tPL_BroadcastCallback(broadcast_callback_function);
      ePLRESULT reg_res = PLLoader1.GetEngine().RegisterBroadcastCallback(broadcast, callback);
      ePLRESULT brd_res = PLLoader1.GetEngine().SendBroadcast(broadcast, 1, new string[] { "_height" }, new double[] { Convert.ToDouble(numericUpDown1.Value) });
    }

    private void button1_Click(object sender, EventArgs e)
    {
      dataGridView2.DataSource = PLLoader1.GetEngine().GetDataTable("Formats", true);
    }

    private void button2_Click(object sender, EventArgs e)
    {
      DataTable dt = (DataTable)dataGridView2.DataSource;
      PLLoader1.GetEngine().SetDataTable(ref dt, PLLoader1.GetEngine().FindDataObjectName("formats_copy"));
    }
  }
}
