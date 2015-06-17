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

// File System.Windows.Controls.ItemContainerGenerator.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0067
// Disable the "this event is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Windows.Controls
{
  sealed public partial class ItemContainerGenerator : System.Windows.Controls.Primitives.IRecyclingItemContainerGenerator, System.Windows.Controls.Primitives.IItemContainerGenerator, System.Windows.IWeakEventListener
  {
    #region Methods and constructors
    public System.Windows.DependencyObject ContainerFromIndex(int index)
    {
      return default(System.Windows.DependencyObject);
    }

    public System.Windows.DependencyObject ContainerFromItem(Object item)
    {
      return default(System.Windows.DependencyObject);
    }

    public int IndexFromContainer(System.Windows.DependencyObject container)
    {
      return default(int);
    }

    internal ItemContainerGenerator()
    {
    }

    public Object ItemFromContainer(System.Windows.DependencyObject container)
    {
      return default(Object);
    }

    System.Windows.DependencyObject System.Windows.Controls.Primitives.IItemContainerGenerator.GenerateNext(out bool isNewlyRealized)
    {
      isNewlyRealized = default(bool);

      return default(System.Windows.DependencyObject);
    }

    System.Windows.DependencyObject System.Windows.Controls.Primitives.IItemContainerGenerator.GenerateNext()
    {
      return default(System.Windows.DependencyObject);
    }

    System.Windows.Controls.Primitives.GeneratorPosition System.Windows.Controls.Primitives.IItemContainerGenerator.GeneratorPositionFromIndex(int itemIndex)
    {
      return default(System.Windows.Controls.Primitives.GeneratorPosition);
    }

    ItemContainerGenerator System.Windows.Controls.Primitives.IItemContainerGenerator.GetItemContainerGeneratorForPanel(Panel panel)
    {
      return default(ItemContainerGenerator);
    }

    int System.Windows.Controls.Primitives.IItemContainerGenerator.IndexFromGeneratorPosition(System.Windows.Controls.Primitives.GeneratorPosition position)
    {
      return default(int);
    }

    void System.Windows.Controls.Primitives.IItemContainerGenerator.PrepareItemContainer(System.Windows.DependencyObject container)
    {
    }

    void System.Windows.Controls.Primitives.IItemContainerGenerator.Remove(System.Windows.Controls.Primitives.GeneratorPosition position, int count)
    {
    }

    void System.Windows.Controls.Primitives.IItemContainerGenerator.RemoveAll()
    {
    }

    IDisposable System.Windows.Controls.Primitives.IItemContainerGenerator.StartAt(System.Windows.Controls.Primitives.GeneratorPosition position, System.Windows.Controls.Primitives.GeneratorDirection direction)
    {
      return default(IDisposable);
    }

    IDisposable System.Windows.Controls.Primitives.IItemContainerGenerator.StartAt(System.Windows.Controls.Primitives.GeneratorPosition position, System.Windows.Controls.Primitives.GeneratorDirection direction, bool allowStartAtRealizedItem)
    {
      return default(IDisposable);
    }

    void System.Windows.Controls.Primitives.IRecyclingItemContainerGenerator.Recycle(System.Windows.Controls.Primitives.GeneratorPosition position, int count)
    {
    }

    bool System.Windows.IWeakEventListener.ReceiveWeakEvent(Type managerType, Object sender, EventArgs e)
    {
      return default(bool);
    }
    #endregion

    #region Properties and indexers
    public System.Windows.Controls.Primitives.GeneratorStatus Status
    {
      get
      {
        return default(System.Windows.Controls.Primitives.GeneratorStatus);
      }
    }
    #endregion

    #region Events
    public event System.Windows.Controls.Primitives.ItemsChangedEventHandler ItemsChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler StatusChanged
    {
      add
      {
      }
      remove
      {
      }
    }
    #endregion
  }
}
