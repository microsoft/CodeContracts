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

// File System.Windows.Automation.Peers.AutomationPeer.cs
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


namespace System.Windows.Automation.Peers
{
  abstract public partial class AutomationPeer : System.Windows.Threading.DispatcherObject
  {
    #region Methods and constructors
    protected AutomationPeer()
    {
    }

    public string GetAcceleratorKey()
    {
      return default(string);
    }

    protected abstract string GetAcceleratorKeyCore();

    public string GetAccessKey()
    {
      return default(string);
    }

    protected abstract string GetAccessKeyCore();

    public AutomationControlType GetAutomationControlType()
    {
      return default(AutomationControlType);
    }

    protected abstract AutomationControlType GetAutomationControlTypeCore();

    public string GetAutomationId()
    {
      return default(string);
    }

    protected abstract string GetAutomationIdCore();

    public System.Windows.Rect GetBoundingRectangle()
    {
      return default(System.Windows.Rect);
    }

    protected abstract System.Windows.Rect GetBoundingRectangleCore();

    public List<System.Windows.Automation.Peers.AutomationPeer> GetChildren()
    {
      return default(List<System.Windows.Automation.Peers.AutomationPeer>);
    }

    protected abstract List<System.Windows.Automation.Peers.AutomationPeer> GetChildrenCore();

    public string GetClassName()
    {
      return default(string);
    }

    protected abstract string GetClassNameCore();

    public System.Windows.Point GetClickablePoint()
    {
      return default(System.Windows.Point);
    }

    protected abstract System.Windows.Point GetClickablePointCore();

    public string GetHelpText()
    {
      return default(string);
    }

    protected abstract string GetHelpTextCore();

    protected virtual new HostedWindowWrapper GetHostRawElementProviderCore()
    {
      return default(HostedWindowWrapper);
    }

    public string GetItemStatus()
    {
      return default(string);
    }

    protected abstract string GetItemStatusCore();

    public string GetItemType()
    {
      return default(string);
    }

    protected abstract string GetItemTypeCore();

    public System.Windows.Automation.Peers.AutomationPeer GetLabeledBy()
    {
      return default(System.Windows.Automation.Peers.AutomationPeer);
    }

    protected abstract System.Windows.Automation.Peers.AutomationPeer GetLabeledByCore();

    public string GetLocalizedControlType()
    {
      return default(string);
    }

    protected virtual new string GetLocalizedControlTypeCore()
    {
      return default(string);
    }

    public string GetName()
    {
      return default(string);
    }

    protected abstract string GetNameCore();

    public AutomationOrientation GetOrientation()
    {
      return default(AutomationOrientation);
    }

    protected abstract AutomationOrientation GetOrientationCore();

    public System.Windows.Automation.Peers.AutomationPeer GetParent()
    {
      return default(System.Windows.Automation.Peers.AutomationPeer);
    }

    public abstract Object GetPattern(PatternInterface patternInterface);

    public bool HasKeyboardFocus()
    {
      return default(bool);
    }

    protected abstract bool HasKeyboardFocusCore();

    public void InvalidatePeer()
    {
    }

    public bool IsContentElement()
    {
      return default(bool);
    }

    protected abstract bool IsContentElementCore();

    public bool IsControlElement()
    {
      return default(bool);
    }

    protected abstract bool IsControlElementCore();

    public bool IsEnabled()
    {
      return default(bool);
    }

    protected abstract bool IsEnabledCore();

    public bool IsKeyboardFocusable()
    {
      return default(bool);
    }

    protected abstract bool IsKeyboardFocusableCore();

    public bool IsOffscreen()
    {
      return default(bool);
    }

    protected abstract bool IsOffscreenCore();

    public bool IsPassword()
    {
      return default(bool);
    }

    protected abstract bool IsPasswordCore();

    public bool IsRequiredForForm()
    {
      return default(bool);
    }

    protected abstract bool IsRequiredForFormCore();

    public static bool ListenerExists(AutomationEvents eventId)
    {
      return default(bool);
    }

    protected System.Windows.Automation.Peers.AutomationPeer PeerFromProvider(System.Windows.Automation.Provider.IRawElementProviderSimple provider)
    {
      return default(System.Windows.Automation.Peers.AutomationPeer);
    }

    protected internal System.Windows.Automation.Provider.IRawElementProviderSimple ProviderFromPeer(System.Windows.Automation.Peers.AutomationPeer peer)
    {
      return default(System.Windows.Automation.Provider.IRawElementProviderSimple);
    }

    public void RaiseAsyncContentLoadedEvent(System.Windows.Automation.AsyncContentLoadedEventArgs args)
    {
    }

    public void RaiseAutomationEvent(AutomationEvents eventId)
    {
    }

    public void RaisePropertyChangedEvent(System.Windows.Automation.AutomationProperty property, Object oldValue, Object newValue)
    {
    }

    public void ResetChildrenCache()
    {
    }

    public void SetFocus()
    {
    }

    protected abstract void SetFocusCore();
    #endregion

    #region Properties and indexers
    public System.Windows.Automation.Peers.AutomationPeer EventsSource
    {
      get
      {
        return default(System.Windows.Automation.Peers.AutomationPeer);
      }
      set
      {
      }
    }

    internal protected virtual new bool IsHwndHost
    {
      get
      {
        return default(bool);
      }
    }
    #endregion
  }
}
