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

// ==++==
// 
//   Copyright (c) Microsoft Corporation.  All rights reserved.
//   Code adapted from Pex.
// 
// ==--==
using System;
using System.ComponentModel.Design;
using System.Diagnostics.Contracts;
using Microsoft.VisualStudio.Shell;

namespace Microsoft.Contracts
{
  internal abstract class DynamicMenuCommand
      : OleMenuCommand
  {
    readonly int id;
    readonly ContractsVsPackage package;

    public int ID
    {
      get { return this.id; }
    }

    public ContractsVsPackage Package
    {
      get { return this.package; }
    }

    [ContractInvariantMethod]
    private void ObjectInvariant() { 
      Contract.Invariant(this.package != null); 
    }

    protected abstract void QueryStatus();
    protected abstract void Execute();

    /// <summary>
    /// Dispatches the callback to inherited method
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void QueryStatusCallback(object sender, EventArgs e)
    {
      UIThreadInvoker.Invoke(() => QueryStatusCallbackUIThread(sender));
    }

    private static void QueryStatusCallbackUIThread(object sender)
    {
      UIThreadInvoker.AssertUIThread();
      try
      {
        DynamicMenuCommand command = sender as DynamicMenuCommand;
        if (command == null)
          throw new InvalidOperationException();
        if (command.Package.Zombied) return;

        command.QueryStatus();
      }
      catch (Exception ex)
      {
        ContractsVsPackage.ReportException(ex);
      }
    }

    /// <summary>
    /// Dispatches the callback to inherited method
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void InvokeCallback(object sender, EventArgs e)
    {
      UIThreadInvoker.Invoke(() => InvokeCallbackUIThread(sender));
    }

    public void ExecuteCommand()
    {
      if (this.Package.Zombied) return;

      this.Execute();
    }

    private static void InvokeCallbackUIThread(object sender)
    {
      UIThreadInvoker.AssertUIThread();
      try
      {
        var command = sender as DynamicMenuCommand;
        if (command == null)
          throw new InvalidOperationException("invalid command");
        command.ExecuteCommand();
      }
      catch (Exception ex)
      {
        ContractsVsPackage.ReportException(ex);
      }
    }

    /// <summary>
    /// Undocumented callback?
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void ChangeCallback(object sender, EventArgs e)
    {
    }

    protected DynamicMenuCommand(ContractsVsPackage package, Guid commandSet, uint id)
      : base(
          new EventHandler(InvokeCallback),
          new EventHandler(ChangeCallback),
          new EventHandler(QueryStatusCallback),
          new CommandID(commandSet, (int)id)
          )
    {
      Contract.Requires(package != null);

      this.id = (int)id;
      this.package = package;
    }

    protected DynamicMenuCommand(ContractsVsPackage package, uint id)
      : this(package, IdTable.ContractsVsPackageCmdSet.ID, id)
    {
      Contract.Requires(package != null);
    }
  }
}
