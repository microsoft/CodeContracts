// CodeContracts
// 
// Copyright (c) Microsoft Corporation
// 
// All rights reserved. 
// 
// MIT License
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Research.ClousotPulse.Messages;

namespace CCCheckPulseUI
{
  public partial class CCCheckPulseForm : Form
  {
    readonly string myCallBackPipe;
   
    public CCCheckPulseForm()
    {
      InitializeComponent();
      SetUpGrids();
      SetUpTimer();

      //this.myCallBackPipe = CommonNames.GetPipeNameForCallBack(Process.GetCurrentProcess().Id);
      this.myCallBackPipe = CommonNames.GetPipeNameForCCCheckPulseCallBack(0);

      // Start the worker thread for call backs
      var listenerThread = new Thread(CallBack);
      listenerThread.IsBackground = true; // To make sure that when we close the window, we exit even if the background listener is still waiting for a connection
      listenerThread.Start();
    }

    private void SetUpGrids()
    {
      // Processes
      var clousotProcesses = this.clousotProcessesGrid;
      clousotProcesses.ColumnHeadersVisible = true;
      clousotProcesses.ColumnCount = 3;
      clousotProcesses.Columns[0].Name = "Proc Id";
      clousotProcesses.Columns[1].Name = "Running time";
      clousotProcesses.Columns[2].Name = "CPU time";

      // Clousot Pulse
      var clousotPulse = this.clousotPulseGrid;
      var fields = typeof(ClousotAnalysisState).GetFields();

      clousotPulse.ColumnHeadersVisible = true;
      clousotPulse.ColumnCount = fields.Count() + 1;

      clousotPulse.Columns[0].Name = "Proc Id";

      var i = 1;
      foreach (var f in fields)
      {
        clousotPulse.Columns[i].Name = f.Name;
        i++;
      }

      // ClousotResults
      fields = typeof(ClousotAnalysisResults).GetFields();

      var clousotResults = this.clousotResultsGrid;
      clousotResults.ColumnHeadersVisible = true;
      clousotResults.ColumnCount = fields.Count();

      i = 0;
      foreach (var f in fields)
      {
        clousotResults.Columns[i].Name = f.Name;
        i++;
      }
    }

    private void SetUpTimer()
    {
      this.timer1.Tick += timer1_Tick;
      this.timer1.Interval = Int32.MaxValue;
      this.automaticUpdate.Value = 1;
      // timer is stopped here
    }

    void timer1_Tick(object sender, EventArgs e)
    {
      UpdateGrids();
    }

    private void updateButton_Click(object sender, EventArgs e)
    {
      UpdateGrids();
    }

    private void UpdateGrids()
    {
      // Clear the pulse grid
      this.clousotPulseGrid.Rows.Clear();

      var ClousotProcesses = Process.GetProcessesByName("Clousot").Union(Process.GetProcessesByName("cccheck"));

      foreach (var proc in ClousotProcesses)
      {
        var pipeName = CommonNames.GetPipeNameForCCCheckPulse(proc.Id);
        var namedPipeClient = new NamedPipeClientStream(".", pipeName, PipeDirection.InOut);
        try
        {
           InsertOrUpdateRowToProcessesGrid(proc);

          namedPipeClient.Connect(20);
          var stream = new PipeStreamSimple(namedPipeClient);

          stream.WriteIfConnected(this.myCallBackPipe);

          List<Tuple<string, object>> result;
          if (stream.TryRead(out result))
          {
            var obj = new ClousotAnalysisState(result);
            AddRowToPulseGrid(proc.Id, obj);
          }
          else
          {
            Trace.TraceWarning("failed to get a meaningfull answer from proc id {0}", proc.Id);
          }
        }
        catch
        {
          // does nothing
        }
        namedPipeClient.Close();
      }
    }

    private void ClearGrids()
    {
      this.clousotProcessesGrid.Rows.Clear();
      this.clousotPulseGrid.Rows.Clear();
      this.clousotResultsGrid.Rows.Clear();
    }

    private void InsertOrUpdateRowToProcessesGrid(Process proc)
    {
      Contract.Requires(proc != null);

      var row = new string[clousotProcessesGrid.ColumnCount];
      row[0] = proc.Id.ToString();
      row[1] = (DateTime.Now - proc.StartTime).ToString();
      row[2] = proc.TotalProcessorTime.ToString();

      InsertOrUpdate(this.clousotProcessesGrid, proc.Id, row);
    }

    static private void InsertOrUpdate(DataGridView grid, int Id, string[] row)
    {
      Contract.Requires(grid != null);

      int index = 0;
      if (TryFindProcInRows(grid, Id.ToString(), out index))
      {
        var rowIsSelected = grid.CurrentRow.Index == index;
        grid.Rows.RemoveAt(index);
        grid.Rows.Insert(index, row);

        if (rowIsSelected)
        {
          // TODO: how do we set a row?
        }
      }
      else
      {
        grid.Rows.Insert(0, row);
      }
    }

    static private bool TryFindProcInRows(DataGridView grid, string id, out int index)
    {
      Contract.Requires(grid != null);
      for (var i = 0; i < grid.RowCount; i++)
      {
        var value = grid[0, i].Value;
        if (value != null && value.ToString() == id)
        {
          index = i;
          return true;
        }
      }

      index = -1;
      return false;
    }

    private void AddRowToPulseGrid(int Id, ClousotAnalysisState obj)
    {
      var fields = typeof(ClousotAnalysisState).GetFields();

      var row = new string[fields.Count() + 1];

      row[0] = Id.ToString();

      var i = 1;

      foreach (var f in fields)
      {
        var v = f.GetValue(obj);
        if (v != null)
        {
          if (v is double)
          {
            row[i] = string.Format("{0,6:P1}", v);
          }
          else
          {
            row[i] = v.ToString();
          }
        }
        i++;
      }

      InsertOrUpdate(this.clousotPulseGrid, Id, row);
    }


    private void UpdateResults(ClousotAnalysisResults results)
    {
      var fields = typeof(ClousotAnalysisResults).GetFields();

      var row = new string[fields.Count()];
      var i = 0;
      foreach (var f in fields)
      {
        var v = f.GetValue(results);
        if (v != null)
        {
          row[i] = v.ToString();
        }
        i++;
      }

      this.clousotResultsGrid.Invoke(
        new Action(delegate() { this.clousotResultsGrid.Rows.Insert(0, row); }));

      // This will throw, as we are in a different thread!
      //      this.gridResults.Rows.Insert(0, row);


    }

    private void clearButton_Click(object sender, EventArgs e)
    {
      ClearGrids();
    }

    private void automaticUpdate_ValueChanged(object sender, EventArgs e)
    {
      this.timer1.Stop();
      var newInterval = ((long)(this.automaticUpdate.Value)) * 1000;
      if (newInterval <= 0)
      {
        this.timer1.Interval = Int32.MaxValue;
      }
      else
      {
        this.timer1.Interval = (int)newInterval;
        this.timer1.Start();
      }
    }

    private void CallBack(object data)
    {
      NamedPipeServerStream namedPipeServer = null;

      while (true)
      {
        try
        {
          namedPipeServer = new NamedPipeServerStream(this.myCallBackPipe, PipeDirection.InOut, 254);

          namedPipeServer.WaitForConnection(); // wait

          var stream = new PipeStreamSimple(namedPipeServer);

          List<Tuple<string, object>> list;
          if (stream.TryRead(out list))
          {
            var results = new ClousotAnalysisResults(list);

            UpdateResults(results);
          }
        }
        catch
        {
          // do nothing
        }
        finally
        {
          if (namedPipeServer != null)
            namedPipeServer.Disconnect();
        }
      }
    }

    private void sendToExcelButton_Click(object sender, EventArgs e)
    {
      var excel = new Microsoft.Office.Interop.Excel.Application();

#if DEBUG
      excel.Visible = true;
#endif
      var workBook = excel.Workbooks.Add();
      Contract.Assume(workBook != null);
      workBook.Sheets["Sheet1"].Name = "ClousotOutput";

      // Write headers
      for (var j = 0; j < this.clousotResultsGrid.Columns.Count; j++)
      {
        excel.Cells[1, j + 1] = this.clousotResultsGrid.Columns[j].Name;
      }

      // Write data
      for (var i = 0; i < this.clousotResultsGrid.RowCount; i++)
      {
        for (var j = 0; j < this.clousotResultsGrid.ColumnCount; j++)
        {
          var cell = this.clousotResultsGrid[j, i];
          excel.Cells[i + 2, j + 1] = cell.Value;
        }
      }

      var currSheet = (Microsoft.Office.Interop.Excel._Worksheet) workBook.ActiveSheet;
      var lastColumnInExcel = (char)('A' + clousotResultsGrid.ColumnCount-1);
      var range = currSheet.get_Range("A1", lastColumnInExcel + "1");
      Contract.Assume(range != null);
      range.Font.Bold = true;
      range.EntireColumn.AutoFit();

      excel.Visible = true;
    }

    private void killAllClousot_Click(object sender, EventArgs e)
    {
      var clousotProcesses = Process.GetProcessesByName("Clousot").Union(Process.GetProcessesByName("cccheck"));

      foreach (var clousotInstance in clousotProcesses)
      {
        try
        {
          clousotInstance.Kill();
        }
        catch
        {
          System.Diagnostics.Trace.WriteLine("Exception in killing process {0}", clousotInstance.Id.ToString());
        }
      }
    }
  }
}
