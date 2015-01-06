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

namespace Microsoft.VisualStudio.CodeTools
{
  using System;
  using Microsoft.VisualStudio.OLE.Interop;
  using Microsoft.VisualStudio.Shell.Interop;
  using Microsoft.VisualStudio.TextManager.Interop;


  internal class Task : IVsTaskItem, IVsTaskItem2, IVsTaskItem3, IVsErrorItem
                      , IVsTextMarkerClient, IVsProvideUserContext
                      , ITask, IVsTask
  {
 
    #region Fields
    private TaskManager   taskManager;
    private ITaskCommands commands;

    private string code;
    private string tipText;
    private string description;
    private string helpKeyword;
    private TaskPriority priority;
    private TaskCategory category;
    private TaskMarker markerType;
    private Location initLoc;
    private Location persistLoc;

    private Location location;
    private bool isDeleted;

    private bool isChecked;
    private IVsTextLineMarker marker;
    private IVsUserContext userContext;

    #endregion

    #region Task
    public Task( TaskManager taskManager
               , string description, string tipText, string code, string helpKeyword
               , TaskPriority priority, TaskCategory category, TaskMarker markerType
               , Location loc
               , ITaskCommands commands
               )
    {
      this.taskManager = taskManager;
      this.code = code;
      this.description = description;
      this.tipText = ((tipText == null || tipText.Length == 0) ? description : tipText);
      this.helpKeyword = helpKeyword;
      this.priority = priority;
      this.category = category;
      this.markerType = markerType;
      this.commands = commands;

      this.location = (loc == null ? new Location(null, null) : loc);
      this.initLoc = this.location.Clone();
      this.persistLoc = this.location.Clone();

      // isChecked = false;
      // isDeleted = false;
      // marker = null;

      // Create markers if the document is already opened.
      IVsTextLines textLines = this.location.GetTextLines(false);
      if (textLines != null) {
        OnOpenFile(textLines,false);
      }
    }

    ~Task()
    {
      Release();
    }

    public void Release()
    {
      // Called from "OnDelete"
      // task list makes sure it doesn't show up 
      //  and we remove it later when an enumeration is asked.
      isDeleted = true;     
      if (marker != null) {
        marker.Invalidate();
        marker = null;
      }
      if (commands != null) {
        try
        {
          commands.Dispose();
        }
        catch { }
        commands = null;
      }     
      userContext = null;
      taskManager = null;
      if (location != null) {
        location.Dispose();
        location = null;
      }
      if (persistLoc != null) {
        persistLoc.Dispose();
        persistLoc = null;
      }
      if (initLoc != null) {
        initLoc.Dispose();
        initLoc = null;
      }
    }

    internal void OnOpenDocument(IVsHierarchy docHier, uint docItemId, string filePath, IVsTextLines textLines)
    {
      Common.Trace("Task.OnOpenDocument: " + filePath);
      if (!isDeleted && marker == null && textLines != null && location != null &&
          location.IsSameDocument(docHier, docItemId, filePath)) {
        OnOpenFile(textLines,true);
      }
      else if (location != null) {
        location.OnPotentialRename(docHier, docItemId, filePath);
      }
    }

    private void OnOpenFile(IVsTextLines textLines,bool refresh)
    {
      if (!isDeleted && marker == null && textLines != null) {
        Common.Trace("Task.OnOpenFile: " + location);
        const MARKERTYPE MARKER_COMPILE_WARNING = (MARKERTYPE)11;
        MARKERTYPE mtype;
        switch (markerType) {
          case TaskMarker.CodeSense: mtype = MARKERTYPE.MARKER_CODESENSE_ERROR; break;
          case TaskMarker.Error: mtype = MARKERTYPE.MARKER_COMPILE_ERROR; break;
          case TaskMarker.Warning: mtype = MARKER_COMPILE_WARNING; break;
          case TaskMarker.Other: mtype = MARKERTYPE.MARKER_OTHER_ERROR; break;
          case TaskMarker.Invisible: mtype = MARKERTYPE.MARKER_INVISIBLE; break; 
          default:
            if ((int)markerType  < (int)MARKERTYPE.DEF_MARKER_COUNT)
              mtype = (MARKERTYPE)markerType;
            else
              mtype = MARKERTYPE.MARKER_INVISIBLE; break; // still create a marker to track position changes
        }
        IVsTextLineMarker[] markers = new IVsTextLineMarker[1];
        TextSpan ts = location.TextSpan;
				IVsTextMarkerClient client = (taskManager.TrackMarkerEvents ? this : null);

        textLines.CreateLineMarker( (int)mtype
                                  , ts.iStartLine, ts.iStartIndex, ts.iEndLine, ts.iEndIndex
                                  , client, markers);
        marker = markers[0];
        if (marker == null) {
          Common.Trace("Task: failed to create marker at " + location);
        }
      }
    }

    internal bool IsVisible()
    {
      return (category != TaskCategory.Invisible && !isDeleted);
    }

    internal bool IsSameFile(IVsHierarchy project /* can be null */, string fileName)
    {
      return (location!=null && location.IsSameDocument(project,VSConstants.VSITEMID_NIL,fileName));
    }

    internal bool Overlaps( Location span )
    {
      return (span != null && location != null && location.Overlaps(span));
    }

    internal bool IsSameHierarchy(IVsHierarchy hier)
    {
      return (location != null && location.IsSameHierarchy(hier));
    }

    #endregion

    #region IVsTaskItem Members

    public int CanDelete(out int pfCanDelete)
    {
      pfCanDelete = 1;
      return 0;
    }

    public int Category(VSTASKCATEGORY[] pCat)
    {
      // Common.Trace("Task.Category");
      if (pCat != null) {
        switch (category) {
          case TaskCategory.Comment: pCat[0] = VSTASKCATEGORY.CAT_COMMENTS; break;
          case TaskCategory.User: pCat[0] = VSTASKCATEGORY.CAT_USER; break;
          case TaskCategory.Other: pCat[0] = VSTASKCATEGORY.CAT_MISC; break;
          case TaskCategory.Shortcut: pCat[0] = VSTASKCATEGORY.CAT_SHORTCUTS; break;
          default: {
              if (markerType == TaskMarker.CodeSense)
                pCat[0] = VSTASKCATEGORY.CAT_CODESENSE;
              else
                pCat[0] = VSTASKCATEGORY.CAT_BUILDCOMPILE;
              break;
            }
        }
      }
      return 0;
    }

    public int Column(out int piCol)
    {
      if (location != null)
        piCol = location.iStartIndex;
      else
        piCol = 0;
      return 0;
    }

    public int Document(out string pbstrMkDocument)
    {
      if (location != null)
        pbstrMkDocument = location.FilePath;
      else
        pbstrMkDocument = "";
      return 0;
    }

    public int HasHelp(out int pfHasHelp)
    {
      pfHasHelp = (helpKeyword != null) ? 1 : 0;
      return 0;
    }

    public int ImageListIndex(out int pIndex)
    {
      switch (category) {
        case TaskCategory.Comment: pIndex = (int)_vstaskbitmap.BMP_COMMENT; break;
        case TaskCategory.Shortcut: pIndex = (int)_vstaskbitmap.BMP_SHORTCUT; break;
        case TaskCategory.User: pIndex = (int)_vstaskbitmap.BMP_USER; break;
        case TaskCategory.Error: pIndex = (int)_vstaskbitmap.BMP_SQUIGGLE; break;
        default: pIndex = (int)_vstaskbitmap.BMP_COMPILE; break;
      }
      return 0;
    }

    public int IsReadOnly(VSTASKFIELD field, out int pfReadOnly)
    {
      switch (field) {
        case VSTASKFIELD.FLD_CHECKED: pfReadOnly = 0; break;
        case VSTASKFIELD.FLD_PRIORITY: pfReadOnly = 0; break;
        default: pfReadOnly = 1; break;
      }
      return 0;
    }

    public int Line(out int piLine)
    {
      if (location != null)
        piLine = location.iStartLine;
      else
        piLine = 0;
      return 0;
    }


    public int NavigateTo()
    {
			if (location != null) {
				location.NavigateTo(true);  // do not return the error to avoid an annoying messagebox
			}
			return 0;  
    }

    public int NavigateToHelp()
    {
      Common.ShowHelp(helpKeyword);
      return 0;
    }

    public int OnDeleteTask()
    {
      Release();
      return 0;
    }

    public int OnFilterTask(int fVisible)
    {
      return 0;
    }

    public int SubcategoryIndex(out int pIndex)
    {
      pIndex = 0;
      return 0;
    }

    public int get_Checked(out int pfChecked)
    {
      pfChecked = (isChecked ? 1 : 0);
      return 0;
    }

    public int get_Priority(VSTASKPRIORITY[] ptpPriority)
    {
      if (ptpPriority != null) {
        switch (priority) {
          case TaskPriority.Low: ptpPriority[0] = VSTASKPRIORITY.TP_LOW; break;
          case TaskPriority.Normal: ptpPriority[0] = VSTASKPRIORITY.TP_NORMAL; break;
          case TaskPriority.High: ptpPriority[0] = VSTASKPRIORITY.TP_HIGH; break;
          default: ptpPriority[0] = VSTASKPRIORITY.TP_NORMAL; break;
        }
      }
      return 0;
    }

    public int get_Text(out string pbstrName)
    {
      pbstrName = description;
      return 0;
    }

    public int put_Checked(int fChecked)
    {
      isChecked = (fChecked != 0);
      return 0;
    }

    public int put_Priority(VSTASKPRIORITY tpPriority)
    {
      switch (tpPriority) {
        case VSTASKPRIORITY.TP_LOW: priority = TaskPriority.Low; break;
        case VSTASKPRIORITY.TP_NORMAL: priority = TaskPriority.Normal; break;
        case VSTASKPRIORITY.TP_HIGH: priority = TaskPriority.High; break;
        // don't do anything in another case
      }
      return 0;
    }

    public int put_Text(string bstrName)
    {
      if (bstrName != null) {
        if (description == tipText) {
          tipText = bstrName;
        }
        description = bstrName;
      }
      return 0;
    }

    #endregion

    #region IVsErrorItem Members

    public int GetCategory(out uint pCategory)
    {
      switch (category) {
        case TaskCategory.Error: pCategory = (uint)__VSERRORCATEGORY.EC_ERROR; break;
        case TaskCategory.Warning: pCategory = (uint)__VSERRORCATEGORY.EC_WARNING; break;
        case TaskCategory.Message: pCategory = (uint)__VSERRORCATEGORY.EC_MESSAGE; break;
        default: pCategory = 0; break;
      }
      return 0;
    }

    public int GetHierarchy(out IVsHierarchy ppProject)
    {
      if (location != null) {
        ppProject = location.GetAssociatedHierarchy();
      }
      else {
        ppProject = null;
      }
      return 0;
    }

    #endregion

    #region IVsProvideUserContext Members

    public int GetUserContext(out IVsUserContext ppctx)
    {
      // Common.Trace("Task.GetUserContext");     
      // set the user context
      if (userContext == null && helpKeyword != null && helpKeyword.Length > 0) {
        IVsMonitorUserContext monitor = Common.GetService(typeof(SVsMonitorUserContext)) as IVsMonitorUserContext;
        if (monitor != null) {
          int hr = monitor.CreateEmptyContext(out userContext);
          if (hr == 0 && userContext != null) {
            hr = userContext.AddAttribute(VSUSERCONTEXTATTRIBUTEUSAGE.VSUC_Usage_LookupF1, "keyword", helpKeyword);
          }
          if (hr != 0) {
            userContext = null;
          }
        }
      }
      ppctx = userContext;
      return 0;
    }

    #endregion

    #region IVsTextMarkerClient Members

    public void MarkerInvalidated()
    {
      if (marker != null) {
        marker.UnadviseClient();
        marker = null;
      }
    }

    public int OnAfterMarkerChange(IVsTextMarker pMarker)
    {
      if (marker != null) {
        // Common.Trace("Task: OnAfterMarkerChange: " + location);
        TextSpan[] tspan = new TextSpan[1];
        int hr = marker.GetCurrentSpan(tspan);
        if (tspan != null && hr == 0) {
          location.TextSpan = tspan[0];
          if (taskManager != null)
          {
            taskManager.RefreshDelayed();
          }
        }
      }
      return 0;
    }

    public void OnAfterSpanReload()
    {
      return;
    }

    public void OnBeforeBufferClose()
    {
      if (persistLoc != null) {
        location = persistLoc.Clone(); // back to persistent span
        if (marker != null) {
          marker.Invalidate();
          marker = null;
        }
      }
    }

    public void OnBufferSave(string pszFileName)
    {
      if (location != null) {
        location.OnRename(pszFileName);
        persistLoc = location.Clone(); // save the persistent span
      }
    }

    public int ExecMarkerCommand(IVsTextMarker pMarker, int iItem)
    {
      if (iItem == (int)MarkerCommandValues.mcvBodyDoubleClickCommand) {
        ExecOnDoubleClick();
      }
      else if (iItem >= (int)MarkerCommandValues.mcvFirstContextMenuCommand &&
                iItem <= (int)MarkerCommandValues.mcvLastContextMenuCommand) {
        ExecCommand(iItem - (int)MarkerCommandValues.mcvFirstContextMenuCommand);
      }
      return 0;
    }

    public int GetMarkerCommandInfo(IVsTextMarker pMarker, int iItem, string[] pbstrText, uint[] pcmdf)
    {
      if (commands != null) {
        try
        {
          const OLECMDF OLECMDF_ENABLE = (OLECMDF.OLECMDF_SUPPORTED | OLECMDF.OLECMDF_ENABLED);
          if (iItem == (int)MarkerCommandValues.mcvBodyDoubleClickCommand &&
              pcmdf != null)
          {
            pcmdf[0] = (uint)OLECMDF_ENABLE;
          }
          else if (iItem >= (int)MarkerCommandValues.mcvFirstContextMenuCommand &&
                   iItem <= (int)MarkerCommandValues.mcvLastContextMenuCommand &&
                   pbstrText != null && pcmdf != null)
          {
            int item = iItem - (int)MarkerCommandValues.mcvFirstContextMenuCommand;
            ITaskMenuItem[] menuItems = commands.GetMenuItems(null);
            if (menuItems != null && item < menuItems.Length)
            {
              ITaskMenuItem menuItem = menuItems[item];
              if (menuItem != null &&
                  menuItem.Kind != TaskMenuKind.Submenu)
              {
                pbstrText[0] = menuItem.Caption;
                if (commands.IsEnabled(menuItems[item]))
                {
                  pcmdf[0] = (uint)OLECMDF_ENABLE;
                  if (menuItem.Kind == TaskMenuKind.Checkable)
                  {
                    if (commands.IsChecked(menuItems[item]))
                      pcmdf[0] |= (uint)(OLECMDF_ENABLE | OLECMDF.OLECMDF_LATCHED);
                    else
                      pcmdf[0] |= (uint)(OLECMDF_ENABLE | OLECMDF.OLECMDF_NINCHED);
                  }
                }
                else
                {
                  pcmdf[0] = (uint)OLECMDF.OLECMDF_SUPPORTED;
                }
              }
            }
          }
          return 0;
        }
        catch
        {
          // communication to msbuild task may be broken, need to catch all errors
          return VSConstants.E_NOTIMPL;
        }
      }
      else {
        return VSConstants.E_NOTIMPL;
      }
    }


    public int GetTipText(IVsTextMarker pMarker, string[] pbstrText)
    {
      if (pbstrText != null && pbstrText.Length > 0) {
        pbstrText[0] = tipText;
      }
      ExecOnHover();
      return 0;
    }

    #endregion

    #region IVsTaskItem2 Members

    public int BrowseObject(out object ppObj)
    {
      ppObj = null;
      return VSConstants.E_NOTIMPL;
    }

    public int IsCustomColumnReadOnly(ref Guid guidView, uint iCustomColumnIndex, out int pfReadOnly)
    {
      pfReadOnly = 0;
      return VSConstants.E_NOTIMPL;
    }

    public int get_CustomColumnText(ref Guid guidView, uint iCustomColumnIndex, out string pbstrText)
    {
      pbstrText = null;
      return VSConstants.E_NOTIMPL;
    }

    public int put_CustomColumnText(ref Guid guidView, uint iCustomColumnIndex, string bstrText)
    {
      return VSConstants.E_NOTIMPL;
    }

    #endregion

    #region IVsTaskItem3 Members

    public int GetColumnValue(int iField, out uint ptvtType, out uint ptvfFlags, out object pvarValue, out string pbstrAccessibilityName)
    {
      ptvtType = 0;
      pvarValue = null;
      ptvfFlags = 0;
      pbstrAccessibilityName = null;
      return VSConstants.E_NOTIMPL;
    }

    public int GetDefaultEditField(out int piField)
    {
      piField = 0;
      return VSConstants.E_NOTIMPL;
    }

    public int GetEnumCount(int iField, out int pnValues)
    {
      pnValues = 0;
      return VSConstants.E_NOTIMPL;
    }

    public int GetEnumValue(int iField, int iValue, out object pvarValue, out string pbstrAccessibilityName)
    {
      pvarValue = null;
      pbstrAccessibilityName = null;
      return VSConstants.E_NOTIMPL;
    }

    public int GetNavigationStatusText(out string pbstrText)
    {
      if (description != null) {
        pbstrText = description;
        return 0;
      }
      else {
        pbstrText = null;
        return VSConstants.E_NOTIMPL;
      }         
    }

    public int GetSurrogateProviderGuid(out Guid pguidProvider)
    {
      pguidProvider = Guid.Empty;
      return VSConstants.E_NOTIMPL;
    }

    public int GetTaskName(out string pbstrName)
    {
      if (code != null) {
        pbstrName = code;
        return 0;
      }
      else {
        pbstrName = null;
        return VSConstants.E_NOTIMPL;
      }
    }

    public int GetTaskProvider(out IVsTaskProvider3 ppProvider)
    {
      ppProvider = taskManager as IVsTaskProvider3;
      return 0;
    }

    public int GetTipText(int iField, out string pbstrTipText)
    {
      pbstrTipText = description;
      return VSConstants.E_NOTIMPL;
    }

    public int IsDirty(out int pfDirty)
    {
      pfDirty = 1; /* always refresh */
      return 0;
    }

    public int OnLinkClicked(int iField, int iLinkIndex)
    {
      return VSConstants.E_NOTIMPL;
    }

    public int SetColumnValue(int iField, ref object pvarValue)
    {
      return VSConstants.E_NOTIMPL;
    }

    #endregion

    #region Command handling
    private void WithCommands(Action action)
    {
      WithCommands(false, delegate() { action(); return true; });      
    }

    // Due to the RPC mechanism in VS2012 sometimes the commands object
    // is no longer accessible. To guard against crashes we wrap all calls
    // to the commands object in a try-catch wrapper
    private A WithCommands<A>(A def, Func<A> action)
    {
      if (commands == null) return def;

      try {
        return action();
      }
      catch (Exception exn) {
        Common.Log("Task.SafeCommands exception:\n" + exn.ToString());
        if (commands != null) {
          try { commands.Dispose(); }
          catch { /* disregard further errors */ }
          commands = null;
        }
        return def;
      }
    }

    internal ITaskMenuItem[] GetMenuItems(ITaskMenuItem parent)
    {
      return WithCommands(null, delegate() {
        return commands.GetMenuItems(parent);
      });
    }

    internal bool IsChecked(ITaskMenuItem menuItem)
    {
      if (commands != null && menuItem != null)
        return WithCommands(false, delegate() { return commands.IsChecked(menuItem); });
      else
        return false;
    }

    internal bool IsEnabled(ITaskMenuItem menuItem)
    {
      if (commands != null && menuItem != null)
        return WithCommands(false, delegate() { return commands.IsEnabled(menuItem); });
      else
        return false;
    }
    internal void ExecCommand(ITaskMenuItem menuItem)
    {
      if (commands != null && menuItem != null && location != null) {
        WithCommands(delegate() { commands.OnContextMenu(this, menuItem); });
      }
    }

    internal void ExecCommand(int item)
    {
      if (commands != null && item >= 0) {
        ITaskMenuItem[] menuItems = GetMenuItems(null);
        if (menuItems != null && item < menuItems.Length) {
          WithCommands(delegate() { ExecCommand(menuItems[item]); });
        }
      }
    }

    internal void ExecOnHover()
    {
      if (commands != null && location != null) {
        WithCommands(delegate() { commands.OnHover(this); });
      }
    }

    internal void ExecOnDoubleClick()
    {
      if (commands != null && location != null) {
        WithCommands(delegate() { commands.OnDoubleClick(this); });
      }
    }

    #endregion

    #region ITask Members

    public int StartLine
    {
      get { return (location == null ? 0 : location.StartLine); }
    }

    public int StartColumn
    {
      get { return (location == null ? 0 : location.StartColumn); }
    }

    public int EndLine
    {
      get { return (location == null ? 0 : location.EndLine); }
    }

    public int EndColumn
    {
      get { return (location == null ? 0 : location.EndColumn); }
    }

    public string FilePath
    {
      get { return (location == null ? null : location.FilePath); }
    }

    public string ProjectName
    {
      get { return (location == null ? null : location.ProjectName); }
    }

    public string Code
    {
      get { return code; }
    }

    ITaskManager ITask.TaskManager
    {
      get { return taskManager; }
    }

    public string Description
    {
      get { return description; }
      set {
        if (value != null) { 
          description = value; 
          Refresh(); 
        }
      }
    }

    public string TipText
    {
      get { return tipText; }
      set {
        if (value != null) {
          tipText = value;
        }
      }
    }

    public bool Checked
    {
      get { return isChecked; }
      set { 
        put_Checked((value ? 1 : 0)); 
        Refresh(); 
      }
    }

    public TaskPriority Priority
    {
      get { return priority; }
      set { 
        priority = value; 
        Refresh();
      }
    }

    public TaskMarker Marker
    {
      get { return markerType; }
      set {
        /* TODO: ignore for now */
      }
    }

    public void Remove()
    {
      if (taskManager != null) {
        taskManager.ClearTask(this);
      }
    }

    public void Refresh()
    {
      if (taskManager != null) {
        taskManager.RefreshTask(this);
      }      
    }

    #endregion

    #region IVsTask Members

    public void GetHierarchy(out object hierarchy, out int hierarchyItem)
    {
      if (location != null) {
        uint itemId;
        location.GetHierarchy(out hierarchy, out itemId);
        hierarchyItem = (int)itemId;
      }
      else {
        hierarchy = null;
        hierarchyItem = unchecked((int)Location.Nil);
      }
    }

    public object GetTextLines(bool openIfClosed)
    {
      return Location.GetTextLines((IVsWindowFrame)GetWindowFrame(openIfClosed));
    }

    public object GetTextView(bool openIfClosed)
    {
      return Location.GetTextView((IVsWindowFrame)GetWindowFrame(openIfClosed));
    }

    public object GetWindowFrame(bool openIfClosed)
    {
      if (location != null) {
        return location.GetWindowFrame(openIfClosed);
      }
      else {
        return null;
      }
    }

    public void NavigateTo(bool highlight)
    {
      Common.ThreadSafeASync(delegate()
      {
        if (location != null)
        {
          location.NavigateTo(highlight);
        }
      });
    }

    public void NavigateToLocation(string projectName, string fileName, int startLine, int startColumn, int endLine, int endColumn, bool highlight)
    {
      if (projectName == null || projectName.Length == 0) projectName = this.ProjectName;
      if (fileName == null || fileName.Length == 0) fileName = this.FilePath;
      Location location = new Location(projectName, fileName, startLine, startColumn, endLine, endColumn);
      Common.ThreadSafeASync(delegate()
      {
        location.NavigateTo(highlight);
      });
    }

    public void NavigateToHelp(string helpKeyword)
    {
      if (helpKeyword == null) {
        NavigateToHelp();
      }
      else {
        Common.ShowHelp(helpKeyword);
      }
    }

    #endregion
  } 
}
