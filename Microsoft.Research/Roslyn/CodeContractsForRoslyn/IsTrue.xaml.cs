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
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.Research.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Diagnostics;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.PlatformUI.OleComponentSupport;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Shell.Interop;
//using RoslynToCCICodeModel;

namespace Microsoft.Research.AskCodeContracts
{
  /// <summary>
  /// Interaction logic for IsTrue.xaml
  /// </summary>
  public partial class IsTrue : Window, IOleComponent
  {
    public bool IsValid { private set; get; }
    public string Assertion { private set; get; }

    private readonly Func<string, IEnumerable<CodeAssumption>, ProofOutcome?> WorkerForQuery;
    private readonly Func<string> WorkerForInvariantQuery;
    private readonly Func<string, IEnumerable<SearchResult>> WorkerForSearchCallersMatchingPredicate;

    private readonly IWpfTextView wpfTextView;

    public IsTrue(IWpfTextView wpfTextView, 
      Func<string, IEnumerable<CodeAssumption>, ProofOutcome?> WorkerForQuery, 
      Func<string> WorkerForInvariantQuery,
      Func<string, IEnumerable<SearchResult>> WorkerForSearchCallersMatchingPredicate,
      string lastQuery = null)
    {
      Contract.Requires(WorkerForQuery != null);
      Contract.Requires(WorkerForInvariantQuery != null);
      Contract.Requires(WorkerForSearchCallersMatchingPredicate != null);

      InitializeComponent();

      this.wpfTextView = wpfTextView;
      this.WorkerForQuery = WorkerForQuery;
      this.WorkerForInvariantQuery = WorkerForInvariantQuery;
      this.WorkerForSearchCallersMatchingPredicate = WorkerForSearchCallersMatchingPredicate;
      this.Assertion = lastQuery;
      this.IsValid = false;
    }

    #region Click handling

    private void AddAssumption_Click(object sender, RoutedEventArgs e)
    {
      var assumption = this.Assumption.Text;
      if (!string.IsNullOrWhiteSpace(assumption))
      {
        var selectionStart = this.wpfTextView.Selection.Start.Position.Position;
        var selectionEnd = this.wpfTextView.Selection.End.Position.Position;

        if(selectionStart == selectionEnd)
        {
          var line = this.wpfTextView.Selection.Start.Position.GetContainingLine().LineNumber + 1;

          Trace.WriteLine(string.Format("Adding assumption {0} @ line {1}, position {2}", assumption, line, selectionStart));

          this.Assumptions.Items.Add(new CodeAssumption(selectionStart, line, assumption));
          this.Assumption.Text = String.Empty; // swallow the assumption
        }
      }
    }

    private void ClearAssumptions_Click(object sender, RoutedEventArgs e)
    {
      this.Assumptions.Items.Clear();
    }

    private void IsTrueButton_Click(object sender, RoutedEventArgs e)
    {
      this.IsValid = true;
      this.Assertion = this.InputQuery.Text;
      this.AnswerText.Text = "Thinking...";

      this.UpdateLineInTextMessages();

      if (!String.IsNullOrEmpty(this.Assertion))
      {
        var result = this.WorkerForQuery(this.Assertion, this.Assumptions.Items.AsEnumerable());
        if (result.HasValue)
        {
          string text = null;
          switch (result.Value)
          {
            case ProofOutcome.Bottom:
              text = "No execution reaches this point";
              break;

            case ProofOutcome.False:
              text = "No, it is false";
              break;

            case ProofOutcome.Top:
              text = "It may sometimes be true, it may sometimes be false";
              break;

            case ProofOutcome.True:
              text = "Yes, it is true";
              break;
          }

          this.AnswerText.Text = text;
        }
        else
        {
          this.AnswerText.Text = "Something went wrong";
        }
      }
      else
      {
        this.AnswerText.Text = String.Empty;
      }
    }

    private void GetInvariantButton_Click(object sender, RoutedEventArgs e)
    {
      this.InvariantText.Text = "Thinking...";
      this.UpdateLineInTextMessages();

      var result = this.WorkerForInvariantQuery();

      if (result != null)
      {
        this.InvariantText.Text = result;
      }

    }

    private void requiresSearchBox_Click(object sender, RoutedEventArgs e)
    {
      Trace.WriteLine("Handling the find box");

      // var myOutputWindow = VSIntegrationUtilities.OutputWindowManager.CreatePane(GuidList.guidClousotOutputPane, "Clousot find window");

      // Clear the previous search
      this.searchResultsFalseListBox.Items.Clear();
      this.searchResultsTopListBox.Items.Clear();
      this.searchResultsTrueListBox.Items.Clear();

      var query = this.requiresSearchConditionTextBox.Text;
      foreach (var r in this.WorkerForSearchCallersMatchingPredicate(query))
      {
        switch (r.Outcome)
        {
          case ProofOutcome.Bottom:
            // do nothing;            
            break;

          case ProofOutcome.False:
            this.searchResultsFalseListBox.Items.Add(r);
            break;

          case ProofOutcome.Top:
            this.searchResultsTopListBox.Items.Add(r);
            break;

          case ProofOutcome.True:
            this.searchResultsTrueListBox.Items.Add(r);
            break;

          default:
            // impossible;
            Contract.Assume(false);
            break;
            
        }
      }
    }

    #endregion

    #region Events

    protected override void OnKeyDown(KeyEventArgs e)
    {
      base.OnKeyDown(e);

      if (e.Key == Key.Delete)
      {
        Trace.WriteLine("Hit Delete key");

        if (this.Assumptions.IsKeyboardFocusWithin)
        {
          var selected = this.Assumptions.SelectedIndex;
          if (selected >= 0)
          {
            Trace.WriteLine(string.Format("Removing the assumption at position {0}", selected));

            this.Assumptions.Items.RemoveAt(selected);
          }
        }
      }

      this.UpdateLineInTextMessages();
    }

    #endregion


    #region Private

    private void UpdateLineInTextMessages()
    {
      var currLine = this.wpfTextView.Selection.Start.Position.GetContainingLine().LineNumber + 1;

      this.QueryText.Text = string.Format("Query @line {0} :", currLine);
      this.InvariantTextLabel.Text = string.Format("State @line {0} :", currLine);
    }

    #endregion


    #region Logic to make sure we do not lose arrows, backspace, etc.
    protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
    {
      base.OnGotKeyboardFocus(e);

      Trace.WriteLine("Get keyboard focus");

      AdjustShellTracking();
      e.Handled = true;
    }

    protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
    {
      base.OnLostKeyboardFocus(e);

      AdjustShellTracking();
      Trace.WriteLine("Lost keyboard focus");

    }


    public void AdjustShellTracking()
    {  // See if our keyboard focus is within our self 
      if (IsKeyboardFocusWithin)
      {
        // Then see if we should be tracking (only track on text boxes)
        if (Keyboard.FocusedElement is DependencyObject)
        {
          var obj = Keyboard.FocusedElement as DependencyObject;

          if (obj != null /*&& obj.FindParentOfType<TextBoxBase>() != null*/)
          {
            IsShellTrackingMe = true;
          }
          else
          {
            IsShellTrackingMe = false;
          }
        }
        else
        {
          IsShellTrackingMe = false;
        }
      }
      else
      {
        IsShellTrackingMe = false;
      }

      // Update our indicators that we are active. Our border and the caret will change if
      // we are tracking
      if (this.wpfTextView != null && this.wpfTextView.Caret != null)
        this.wpfTextView.Caret.IsHidden = IsShellTrackingMe;

      // If we have the keyboard focus, but we are not shell tracking, make sure
      // to set the keyboard focus back to the text editor. Otherwise
      // it looks like nobody has the focus
      if (this.wpfTextView != null && IsKeyboardFocusWithin && !IsShellTrackingMe)
        Keyboard.Focus(this.wpfTextView.VisualElement);
    }

    /// <summary>
    /// Indicates if this tip is the IOleComponent that is being tracked by the shell. This is necessary
    /// to ensure that keyboard input is sent entirely to the WPF window. Otherwise the PreTranslateMessage of 
    /// the shell will send it to other windows. (Like backspace going to the editor window)
    /// </summary>
    public bool IsShellTrackingMe
    {
      get
      {
        return _isTracking;
      }
      set
      {
        if (_isTracking != value)
        {
          if (value)
          {
            // Register ourselves as a tracking component so we
            // get first crack at backspace/up/down/left/right/CTRL+A/CTRL+C

            // Sometimes this might not work because somebody else is already
            // the tracking target. 
            _isTracking = VSIntegrationUtilities.OLE.StartTrackingComponent(this);
          }
          else
          {
            // Unregister ourselves as a command target
            VSIntegrationUtilities.OLE.StopTrackingComponent(this);

            // This always succeeds. Can't not unregister
            _isTracking = false;
          }
        }
      }
    }

    private bool _isTracking;

    #region implementation of IOleComponent

    public int FContinueMessageLoop(uint uReason, IntPtr pvLoopData, MSG[] pMsgPeeked)
    {
      // If we have keyboard focus, then allow more messages to be pumped
      if (IsKeyboardFocusWithin)
        return 1;

      return 0;
    }

    public int FDoIdle(uint grfidlef)
    {
      return 0;
    }

    public int FPreTranslateMessage(MSG[] pMsg)
    {
      // If we handle it, then tell the shell that (1 means we handled it)
      return VSIntegrationUtilities.OLE.HandleComponentMessage(pMsg[0]) ? 1 : 0;
    }

    public int FQueryTerminate(int fPromptUser)
    {
      return 1;
    }

    public int FReserved1(uint dwReserved, uint message, IntPtr wParam, IntPtr lParam)
    {
      return 0;
    }

    public IntPtr HwndGetWindow(uint dwWhich, uint dwReserved)
    {
      // F: LOOK AT THAT
      return IntPtr.Zero;

    }

    public void OnActivationChange(IOleComponent pic, int fSameComponent, OLECRINFO[] pcrinfo, int fHostIsActivating, OLECHOSTINFO[] pchostinfo, uint dwReserved)
    {
    }

    public void OnAppActivate(int fActive, uint dwOtherThreadID)
    {
    }

    public void OnEnterState(uint uStateID, int fEnter)
    {
    }

    public void OnLoseActivation()
    {
    }

    public void Terminate()
    {
    }

    #endregion

    #endregion
  }

  public static class Extensions
  {
    public static IEnumerable<CodeAssumption> AsEnumerable(this ItemCollection collection)
    {
      if (collection != null)
      {
        foreach (var item in collection)
        {
          var assumption = item as CodeAssumption;
          if (assumption != null)
          {
            yield return assumption;
          }
        }
      }
    }
  }
}

namespace VSIntegrationUtilities
{
  public static class OutputWindowManager
  {
    /// <summary>
    /// Create an output window
    /// </summary>
    static public IVsOutputWindowPane CreatePane(Guid guid, string title, bool visible = true, bool clearWithSolution = true)
    {
      var outputWindow = ServiceProvider.GlobalProvider.GetService(typeof(SVsOutputWindow)) as IVsOutputWindow;
      if (outputWindow != null)
      {
        IVsOutputWindowPane result;
        outputWindow.CreatePane(ref guid, title, Convert.ToInt32(visible), Convert.ToInt32(clearWithSolution));
        outputWindow.GetPane(ref guid, out result);

        return result;
      }

      return null;
    }
  }

  public static class OLE
  {
    private static IOleComponent ActiveComponent
    {
      get;
      set;
    }


    public static bool HandleComponentMessage(Microsoft.VisualStudio.OLE.Interop.MSG msg)
    {
      // Swallow all keyboard input
      if ((msg.message >= VSIntegrationUtilities.NativeMethods.WM_KEYFIRST &&
           msg.message <= VSIntegrationUtilities.NativeMethods.WM_KEYLAST) ||
          (msg.message >= VSIntegrationUtilities.NativeMethods.WM_IME_FIRST &&
           msg.message <= VSIntegrationUtilities.NativeMethods.WM_IME_LAST))
      {

        /*
        // Special case when the Alt key is down
        if (Keyboard.IsKeyDown(Key.LeftAlt) ||
            Keyboard.IsKeyDown(Key.RightAlt))
        {
          // Give the main window focus. This will allow it to handle
          // the ALT + <key> commands and bring up the menu. 
          //
          // This works because we lose focus and revoke IOleComponent tracking status
          Utilities.NativeMethods.SetFocus(Shell.MainWindow.Handle);
          return false;
        }
      }
         */

        // Otherwise it's our message. Don't let the shell translate it 
        // into some goofy command.

        // Give WPF a chance to translate.
        System.Windows.Interop.MSG windowsMsg = NativeMethods.MSGFromOleMSG(ref msg);
        if (!System.Windows.Interop.ComponentDispatcher.RaiseThreadMessage(ref windowsMsg))
        {

          VSIntegrationUtilities.NativeMethods.TranslateMessage(ref msg);
          VSIntegrationUtilities.NativeMethods.DispatchMessage(ref msg);
        }
        return true;
      }

      return false;
    }

    /// <summary>
    /// Exposing Service provider when the class to get the service is different than the interface
    /// that throws if the GetService call or cast to T fails
    /// </summary>
    /// <typeparam name="C">Service SID value. E.g. SVsShell</typeparam>
    /// <typeparam name="T">Interface to cast to. E.g. IVsShell</typeparam>
    /// <returns></returns>
    public static T GetRequiredService<C, T>() where T : class
    {
      T serviceInterface = (T)ServiceProvider.GlobalProvider.GetService(typeof(C).GUID);
      if (serviceInterface == null)
      {
        throw new InvalidOperationException(String.Format("GetService for {0} failed!", typeof(T)));
      }
      return serviceInterface;
    }

    private static IOleComponentManager OleComponentManager
    {
      get
      {
        return GetRequiredService<SOleComponentManager, IOleComponentManager>();
      }
    }
    private static Dictionary<IOleComponent, uint> _registeredComponents = new Dictionary<IOleComponent, uint>();
    private static uint RegisterComponent(IOleComponent me)
    {
      uint returnValue = 0;
      if (!_registeredComponents.TryGetValue(me, out returnValue))
      {
        // treat us as a modal component that takes care of all input
        OLECRINFO info = new OLECRINFO();
        info.cbSize = (uint)Marshal.SizeOf(info);
        info.grfcadvf = (uint)_OLECADVF.olecadvfModal;
        info.grfcrf = (uint)(_OLECRF.olecrfPreTranslateKeys | _OLECRF.olecrfPreTranslateAll | _OLECRF.olecrfNeedAllActiveNotifs | _OLECRF.olecrfExclusiveActivation);
        OLECRINFO[] array = new OLECRINFO[1];
        array[0] = info;
        OleComponentManager.FRegisterComponent(me, array, out returnValue);
        _registeredComponents.Add(me, returnValue);
      }

      return returnValue;
    }
    public static bool StartTrackingComponent(IOleComponent me)
    {
      if (ActiveComponent != me)
      {
        // Make sure there isn't somebody already registered
        IOleComponent activeComponent;
        OleComponentManager.FGetActiveComponent(
            //(uint)FGetActiveComponentType.olegacTracking, // f: changed
            1,
            out activeComponent,
            null,
            0);

        if (activeComponent == null)
        {
          // Save ourselves as the currently active component (prevents reentrancy)
          ActiveComponent = me;

          // Tell the shell we are the active component
          OleComponentManager.FSetTrackingComponent(RegisterComponent(me), 1);
          return true;
        }
        else if (Marshal.IsComObject(activeComponent))
        {
          Marshal.ReleaseComObject(activeComponent);
        }
      }
      return false;
    }

    public static bool StopTrackingComponent(IOleComponent me)
    {
      OleComponentManager.FSetTrackingComponent(RegisterComponent(me), 0);

      if (ActiveComponent == me)
      {
        ActiveComponent = null;
      }

      return true;
    }
  }

  public static class NativeMethods
  {
    public const int 
    WM_NULL = 0x0000,
        WM_CREATE = 0x0001,
        WM_DELETEITEM = 0x002D,
        WM_DESTROY = 0x0002,
        WM_MOVE = 0x0003,
        WM_SIZE = 0x0005,
        WM_ACTIVATE = 0x0006,
        WA_INACTIVE = 0,
        WA_ACTIVE = 1,
        WA_CLICKACTIVE = 2,
        WM_SETFOCUS = 0x0007,
        WM_KILLFOCUS = 0x0008,
        WM_ENABLE = 0x000A,
        WM_SETREDRAW = 0x000B,
        WM_SETTEXT = 0x000C,
        WM_GETTEXT = 0x000D,
        WM_GETTEXTLENGTH = 0x000E,
        WM_PAINT = 0x000F,
        WM_CLOSE = 0x0010,
        WM_QUERYENDSESSION = 0x0011,
        WM_QUIT = 0x0012,
        WM_QUERYOPEN = 0x0013,
        WM_ERASEBKGND = 0x0014,
        WM_SYSCOLORCHANGE = 0x0015,
        WM_ENDSESSION = 0x0016,
        WM_SHOWWINDOW = 0x0018,
        WM_WININICHANGE = 0x001A,
        WM_SETTINGCHANGE = 0x001A,
        WM_DEVMODECHANGE = 0x001B,
        WM_ACTIVATEAPP = 0x001C,
        WM_FONTCHANGE = 0x001D,
        WM_TIMECHANGE = 0x001E,
        WM_CANCELMODE = 0x001F,
        WM_SETCURSOR = 0x0020,
        WM_MOUSEACTIVATE = 0x0021,
        WM_CHILDACTIVATE = 0x0022,
        WM_QUEUESYNC = 0x0023,
        WM_GETMINMAXINFO = 0x0024,
        WM_PAINTICON = 0x0026,
        WM_ICONERASEBKGND = 0x0027,
        WM_NEXTDLGCTL = 0x0028,
        WM_SPOOLERSTATUS = 0x002A,
        WM_DRAWITEM = 0x002B,
        WM_MEASUREITEM = 0x002C,
        WM_VKEYTOITEM = 0x002E,
        WM_CHARTOITEM = 0x002F,
        WM_SETFONT = 0x0030,
        WM_GETFONT = 0x0031,
        WM_SETHOTKEY = 0x0032,
        WM_GETHOTKEY = 0x0033,
        WM_QUERYDRAGICON = 0x0037,
        WM_COMPAREITEM = 0x0039,
        WM_GETOBJECT = 0x003D,
        WM_COMPACTING = 0x0041,
        WM_COMMNOTIFY = 0x0044,
        WM_WINDOWPOSCHANGING = 0x0046,
        WM_WINDOWPOSCHANGED = 0x0047,
        WM_POWER = 0x0048,
        WM_COPYDATA = 0x004A,
        WM_CANCELJOURNAL = 0x004B,
        WM_NOTIFY = 0x004E,
        WM_INPUTLANGCHANGEREQUEST = 0x0050,
        WM_INPUTLANGCHANGE = 0x0051,
        WM_TCARD = 0x0052,
        WM_HELP = 0x0053,
        WM_USERCHANGED = 0x0054,
        WM_NOTIFYFORMAT = 0x0055,
        WM_CONTEXTMENU = 0x007B,
        WM_STYLECHANGING = 0x007C,
        WM_STYLECHANGED = 0x007D,
        WM_DISPLAYCHANGE = 0x007E,
        WM_GETICON = 0x007F,
        WM_SETICON = 0x0080,
        WM_NCCREATE = 0x0081,
        WM_NCDESTROY = 0x0082,
        WM_NCCALCSIZE = 0x0083,
        WM_NCHITTEST = 0x0084,
        WM_NCPAINT = 0x0085,
        WM_NCACTIVATE = 0x0086,
        WM_GETDLGCODE = 0x0087,
        WM_NCMOUSEMOVE = 0x00A0,
        WM_NCLBUTTONDOWN = 0x00A1,
        WM_NCLBUTTONUP = 0x00A2,
        WM_NCLBUTTONDBLCLK = 0x00A3,
        WM_NCRBUTTONDOWN = 0x00A4,
        WM_NCRBUTTONUP = 0x00A5,
        WM_NCRBUTTONDBLCLK = 0x00A6,
        WM_NCMBUTTONDOWN = 0x00A7,
        WM_NCMBUTTONUP = 0x00A8,
        WM_NCMBUTTONDBLCLK = 0x00A9,
        WM_NCXBUTTONDOWN = 0x00AB,
        WM_NCXBUTTONUP = 0x00AC,
        WM_NCXBUTTONDBLCLK = 0x00AD,
        WM_KEYFIRST = 0x0100,
        WM_KEYDOWN = 0x0100,
        WM_KEYUP = 0x0101,
        WM_CHAR = 0x0102,
        WM_DEADCHAR = 0x0103,
        WM_CTLCOLOR = 0x0019,
        WM_SYSKEYDOWN = 0x0104,
        WM_SYSKEYUP = 0x0105,
        WM_SYSCHAR = 0x0106,
        WM_SYSDEADCHAR = 0x0107,
        WM_KEYLAST = 0x0108,
        WM_IME_COMPOSITION_FIRST = 0x010D,
        WM_IME_STARTCOMPOSITION = 0x010D,
        WM_IME_ENDCOMPOSITION = 0x010E,
        WM_IME_COMPOSITION = 0x010F,
        WM_IME_KEYLAST = 0x010F,
        WM_IME_COMPOSITION_LAST = 0x010F,
        WM_INITDIALOG = 0x0110,
        WM_COMMAND = 0x0111,
        WM_SYSCOMMAND = 0x0112,
        WM_TIMER = 0x0113,
        WM_HSCROLL = 0x0114,
        WM_VSCROLL = 0x0115,
        WM_INITMENU = 0x0116,
        WM_INITMENUPOPUP = 0x0117,
        WM_MENUSELECT = 0x011F,
        WM_MENUCHAR = 0x0120,
        WM_ENTERIDLE = 0x0121,
        WM_CHANGEUISTATE = 0x0127,
        WM_UPDATEUISTATE = 0x0128,
        WM_QUERYUISTATE = 0x0129,
        WM_CTLCOLORMSGBOX = 0x0132,
        WM_CTLCOLOREDIT = 0x0133,
        WM_CTLCOLORLISTBOX = 0x0134,
        WM_CTLCOLORBTN = 0x0135,
        WM_CTLCOLORDLG = 0x0136,
        WM_CTLCOLORSCROLLBAR = 0x0137,
        WM_CTLCOLORSTATIC = 0x0138,
        WM_MOUSEFIRST = 0x0200,
        WM_MOUSEMOVE = 0x0200,
        WM_LBUTTONDOWN = 0x0201,
        WM_LBUTTONUP = 0x0202,
        WM_LBUTTONDBLCLK = 0x0203,
        WM_RBUTTONDOWN = 0x0204,
        WM_RBUTTONUP = 0x0205,
        WM_RBUTTONDBLCLK = 0x0206,
        WM_MBUTTONDOWN = 0x0207,
        WM_MBUTTONUP = 0x0208,
        WM_MBUTTONDBLCLK = 0x0209,
        WM_XBUTTONDOWN = 0x020B,
        WM_XBUTTONUP = 0x020C,
        WM_XBUTTONDBLCLK = 0x020D,
        WM_MOUSEWHEEL = 0x020A,
        WM_MOUSELAST = 0x020A,
        WM_PARENTNOTIFY = 0x0210,
        WM_ENTERMENULOOP = 0x0211,
        WM_EXITMENULOOP = 0x0212,
        WM_NEXTMENU = 0x0213,
        WM_SIZING = 0x0214,
        WM_CAPTURECHANGED = 0x0215,
        WM_MOVING = 0x0216,
        WM_POWERBROADCAST = 0x0218,
        WM_DEVICECHANGE = 0x0219,
        WM_IME_FIRST = 0x0281,
        WM_IME_SETCONTEXT = 0x0281,
        WM_IME_NOTIFY = 0x0282,
        WM_IME_CONTROL = 0x0283,
        WM_IME_COMPOSITIONFULL = 0x0284,
        WM_IME_SELECT = 0x0285,
        WM_IME_CHAR = 0x0286,
        WM_IME_KEYDOWN = 0x0290,
        WM_IME_KEYUP = 0x0291,
        WM_IME_LAST = 0x0291,
        WM_MDICREATE = 0x0220,
        WM_MDIDESTROY = 0x0221,
        WM_MDIACTIVATE = 0x0222,
        WM_MDIRESTORE = 0x0223,
        WM_MDINEXT = 0x0224,
        WM_MDIMAXIMIZE = 0x0225,
        WM_MDITILE = 0x0226,
        WM_MDICASCADE = 0x0227,
        WM_MDIICONARRANGE = 0x0228,
        WM_MDIGETACTIVE = 0x0229,
        WM_MDISETMENU = 0x0230,
        WM_ENTERSIZEMOVE = 0x0231,
        WM_EXITSIZEMOVE = 0x0232,
        WM_DROPFILES = 0x0233,
        WM_MDIREFRESHMENU = 0x0234,
        WM_MOUSEHOVER = 0x02A1,
        WM_MOUSELEAVE = 0x02A3,
        WM_CUT = 0x0300,
        WM_COPY = 0x0301,
        WM_PASTE = 0x0302,
        WM_CLEAR = 0x0303,
        WM_UNDO = 0x0304,
        WM_RENDERFORMAT = 0x0305,
        WM_RENDERALLFORMATS = 0x0306,
        WM_DESTROYCLIPBOARD = 0x0307,
        WM_DRAWCLIPBOARD = 0x0308,
        WM_PAINTCLIPBOARD = 0x0309,
        WM_VSCROLLCLIPBOARD = 0x030A,
        WM_SIZECLIPBOARD = 0x030B,
        WM_ASKCBFORMATNAME = 0x030C,
        WM_CHANGECBCHAIN = 0x030D,
        WM_HSCROLLCLIPBOARD = 0x030E,
        WM_QUERYNEWPALETTE = 0x030F,
        WM_PALETTEISCHANGING = 0x0310,
        WM_PALETTECHANGED = 0x0311,
        WM_HOTKEY = 0x0312,
        WM_PRINT = 0x0317,
        WM_PRINTCLIENT = 0x0318,
        WM_HANDHELDFIRST = 0x0358,
        WM_HANDHELDLAST = 0x035F,
        WM_AFXFIRST = 0x0360,
        WM_AFXLAST = 0x037F,
        WM_PENWINFIRST = 0x0380,
        WM_PENWINLAST = 0x038F,
        WM_APP = unchecked((int)0x8000),
        WM_USER = 0x0400,
        WM_REFLECT =
        WM_USER + 0x1C00,
        WS_OVERLAPPED = 0x00000000,
        WPF_SETMINPOSITION = 0x0001,
        WM_CHOOSEFONT_GETLOGFONT = (0x0400 + 1),
        WM_THEMECHANGED = 0x31a;

            public static System.Windows.Interop.MSG MSGFromOleMSG(ref Microsoft.VisualStudio.OLE.Interop.MSG oleMsg)
        {
            System.Windows.Interop.MSG msg = new System.Windows.Interop.MSG();
            msg.hwnd = oleMsg.hwnd;
            msg.message = (int)oleMsg.message;
            msg.wParam = oleMsg.wParam;
            msg.lParam = oleMsg.lParam;
            msg.time = (int)oleMsg.time;
            msg.pt_x = oleMsg.pt.x;
            msg.pt_y = oleMsg.pt.y;
            return msg;
        }

        [DllImport("user32.dll")]
        internal static extern bool TranslateMessage([In] ref MSG lpMsg);

        [DllImport("user32.dll")]
        internal static extern IntPtr DispatchMessage([In] ref MSG lpMsg);
  }
}
