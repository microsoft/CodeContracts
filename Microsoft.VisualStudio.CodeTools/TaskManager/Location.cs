// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.VisualStudio.CodeTools
{
    using System;
    using Microsoft.VisualStudio.Shell.Interop;
    using Microsoft.VisualStudio.TextManager.Interop;

    /// <summary>
    /// Maintains "Location"s: an abstraction of a text span, a file name
    /// and an associated project. It tracks renames etc. to keep the location up-to-date.
    /// 
    /// A location works with just a textspan and a filename. However, if a project name 
    /// is supplied, the association with a specific project and item in the project is 
    /// maintained. This way, we can track renames and deletions in the project. Furthermore,
    /// we can ensure only to show squiggles in those sourcefiles that were opened by the
    /// project that caused the errors in the first place.
    /// 
    /// The code is complicated a bit by the <c>dirty</c> flag: this is to update  
    /// the associated <c>project</c> and <c>itemId</c>, only when requested.
    /// They can not be updated immediately since some methods are called during an
    /// event (like <c>OnRename</c>) when the associated project is still in an
    /// inconsistent state.
    /// </summary>
    internal class Location : IDisposable
    {
        #region Private fields
        private const uint E_FILENOTFOUND = 0x80070002;
        public const uint Nil = VSConstants.VSITEMID_NIL;

        private int startLine;
        private int startColumn;
        private int endLine;
        private int endColumn;

        private IVsProject project;
        private uint itemId;

        private string projectName;
        private string filePath;   // full path name

        #endregion

        #region Validation
        private bool dirty;

        /// <summary>
        /// Update the associated project and itemId if necessary.
        /// </summary>
        private void Validate()
        {
            if (dirty)
            {
                // get project interface
                if (project == null &&
                    projectName != null &&
                    projectName.Length > 0)
                {
                    project = Common.GetProjectByName(projectName) as IVsProject;
                }

                if (project != null)
                {
                    // get projectName from project
                    IVsHierarchy projHier = project as IVsHierarchy;
                    string projname = Common.GetProjectName(projHier);
                    // project.GetMkDocument(VSConstants.VSITEMID_ROOT, out projname);
                    if (projname != null && projname.Length > 0)
                    {
                        projectName = projname;
                    }

                    // get itemId 
                    if (itemId == Nil &&
                        filePath != null && filePath.Length > 0)
                    {
                        int found;
                        uint item;
                        VSDOCUMENTPRIORITY[] prs = { 0 };
                        project.IsDocumentInProject(filePath, out found, prs, out item);
                        if (found != 0)
                        {
                            itemId = item;
                        }
                    }

                    // get filePath from project
                    if (itemId != Nil)
                    {
                        string name;
                        project.GetMkDocument(itemId, out name);
                        if (name != null && name.Length > 0)
                        {
                            filePath = System.IO.Path.GetFullPath(name);
                        }
                    }
                }
            }
        }

        private bool ValidProject()
        {
            Validate();
            return (project != null && itemId != Nil);
        }
        #endregion

        #region Properties

        public string FilePath
        {
            get
            {
                Validate();
                return filePath;
            }
            set
            {
                string s;
                if (value == null)
                {
                    s = "";
                }
                else
                {
                    s = value.Trim();
                }
                if (s.Length > 0)
                {
                    string fpath = System.IO.Path.GetFullPath(s);
                    if (String.Compare(filePath, fpath, StringComparison.OrdinalIgnoreCase) != 0)
                    {
                        filePath = fpath;
                        project = null;
                        itemId = Nil;
                        dirty = true;
                    }
                }
                else
                {
                    filePath = s;
                    itemId = Nil;
                    dirty = true;
                    // leave project as is.
                }
            }
        }

        public string ProjectName
        {
            get
            {
                Validate();
                return projectName;
            }
        }

        public int iStartLine
        {
            get { return (startLine - 1); }
        }

        public int iStartIndex
        {
            get { return (startColumn - 1); }
        }

        public int StartLine
        {
            get { return startLine; }
        }

        public int StartColumn
        {
            get { return startColumn; }
        }

        public int EndLine
        {
            get { return endLine; }
        }

        public int EndColumn
        {
            get { return endColumn; }
        }
        #endregion

        #region Constructing
        public void Dispose()
        {
            project = null;
        }

        public Location()
        {
            SetFileSpan(null, null, 1, 1, 1, 1);
        }

        public Location(string projectName, string fname)
        {
            SetFileSpan(projectName, fname, 1, 1, 1, 1);
        }

        public Location(string projectName, string fname, int startLine, int startColumn, int endLine, int endColumn)
        {
            SetFileSpan(projectName, fname, startLine, startColumn, endLine, endColumn);
        }

        public Location Clone()
        {
            if (ValidProject())
            {
                return new Location(project, itemId, ProjectName, FilePath, startLine, startColumn, endLine, endColumn);
            }
            else
            {
                return new Location(ProjectName, FilePath, startLine, startColumn, endLine, endColumn);
            }
        }

        public Location(IVsProject project, uint itemId, string pname, string fname, int startLine, int startColumn, int endLine, int endColumn)
        {
            if (fname == null || fname.Length == 0)
            {
                fname = "<Unknown>";
            }
            FilePath = fname;
            projectName = pname;
            this.project = project;
            this.itemId = itemId;
            SetSpan(startLine, startColumn, endLine, endColumn);
            dirty = true;
        }


        private void SetFileSpan(string pname, string fname, int startLine, int startColumn, int endLine, int endColumn)
        {
            if (fname == null)
            {
                fname = "";
            }
            FilePath = fname;
            projectName = pname;
            SetSpan(startLine, startColumn, endLine, endColumn);
        }

        private void SetSpan(int startLine, int startColumn, int endLine, int endColumn)
        {
            this.startLine = startLine;
            this.startColumn = startColumn;
            this.endLine = endLine;
            this.endColumn = endColumn;
            Normalize();
        }

        private void Normalize()
        {
            if (startLine <= 0) startLine = 1;
            if (endLine <= 0) endLine = 1;
            if (startColumn <= 0) startColumn = 1;
            if (endColumn <= 0) endColumn = 1;

            if (endLine < startLine)
            {
                int temp = startLine;
                startLine = endLine;
                endLine = temp;
            }
            if (endLine == startLine && endColumn < startColumn)
            {
                int temp = startColumn;
                startColumn = endColumn;
                endColumn = temp;
            }
        }
        #endregion

        #region Document related methods

        /// <summary>
        /// Call this method if a buffer has been saved under a different file name. 
        /// This allows the location to update the itemid of the associated project.
        /// </summary>
        /// <param name="newFileName">The new file name</param>
        public void OnRename(string newFileName)
        {
            FilePath = newFileName; // this sets the 'dirty' flag.        
        }


        public void OnPotentialRename(IVsHierarchy docHier, uint docItemId, string fname)
        {
            // if the project+itemid matches, the given filename is an improvement
            if (ValidProject())
            {
                if (project == docHier && docItemId == itemId)
                {
                    OnRename(fname);
                }
            }
        }


        /// <summary>
        /// Get the window frame associated with this location.
        /// </summary>
        /// <param name="openIfClosed">Should the frame be opened if it is not yet open?</param>
        /// <returns></returns>
        public IVsWindowFrame GetWindowFrame(bool openIfClosed)
        {
            Validate();

            IVsWindowFrame windowFrame = null;

            IVsUIShellOpenDocument doc = Common.GetService(typeof(SVsUIShellOpenDocument)) as IVsUIShellOpenDocument;
            if (doc != null)
            {
                IVsUIHierarchy uihier = project as IVsUIHierarchy; // also works if project==null
                Guid textViewGuid = new Guid(LogicalViewID.TextView);
                IVsWindowFrame frame;
                IVsUIHierarchy uiHierOpen;
                uint[] itemIds = { 0 };
                int open;
                //note: we explicitly only look at a *text* views belonging to the correct project
                // we do not want a designer form, or a view opened from another project that might
                // have been build using different settings. However, this code also works if the
                // project and itemid are unknown, but in that case we just pick the first best text
                // view.
                int hr = doc.IsDocumentOpen(uihier, itemId, FilePath, ref textViewGuid
                                           , 0, out uiHierOpen, itemIds, out frame, out open);
                //success
                if ((open != 0) && (frame != null) &&
                    (project == null || uihier == uiHierOpen) &&
                    (itemId == Nil || itemId == itemIds[0]))
                {
                    windowFrame = frame;
                }
                // failure: try to open it.
                else if (openIfClosed)
                {
                    if (ValidProject())
                    {
                        hr = project.OpenItem(itemId, ref textViewGuid, IntPtr.Zero, out frame);
                        if (hr == 0 && frame != null)
                        {
                            windowFrame = frame;
                        }
                    }
                    else
                    {
                        Microsoft.VisualStudio.OLE.Interop.IServiceProvider sp;
                        uint item;
                        hr = doc.OpenDocumentViaProject(FilePath, ref textViewGuid, out sp, out uiHierOpen, out item, out frame);
                        if (hr == 0 && frame != null)
                        {
                            project = uiHierOpen as IVsProject;
                            if (project != null)
                            {
                                itemId = item;
                            }
                            windowFrame = frame;
                        }
                    }
                }
            }
            return windowFrame;
        }

        /// <summary>
        /// Get the text buffer associated with the location.
        /// </summary>
        /// <param name="openIfClosed">Should we open the buffer if it is not yet open?</param>
        /// <returns></returns>
        internal IVsTextLines GetTextLines(bool openIfClosed)
        {
            return GetTextLines(GetWindowFrame(openIfClosed));
        }

        internal static IVsTextLines GetTextLines(IVsWindowFrame windowFrame)
        {
            if (windowFrame != null)
            {
                object docData;
                windowFrame.GetProperty((int)__VSFPROPID.VSFPROPID_DocData, out docData);
                return docData as IVsTextLines;
            }
            else
            {
                return null;
            }
        }

        internal static IVsTextView GetTextView(IVsWindowFrame windowFrame)
        {
            if (windowFrame != null)
            {
                object docView;
                windowFrame.Show();
                windowFrame.GetProperty((int)__VSFPROPID.VSFPROPID_DocView, out docView);
                if (docView != null)
                {
                    IVsTextView textView = docView as IVsTextView;
                    if (textView == null)
                    {
                        IVsCodeWindow codeWindow = docView as IVsCodeWindow;
                        if (codeWindow != null)
                        {
                            codeWindow.GetPrimaryView(out textView);
                        }
                    }
                    return textView;
                }
            }
            return null;
        }

        /// <summary>
        /// Open and activate the window that holds the location and highlight 
        /// the corresponding span.
        /// </summary>
        /// <param name="highlight">Highlight the selection.</param>
        /// <returns>A <c>HRESULT</c>.</returns>
        public int NavigateTo(bool highlight)
        {
            IVsTextView textView = GetTextView(GetWindowFrame(true));
            if (textView != null)
            {
                TextSpan ts = TextSpan;
                textView.SetCaretPos(ts.iStartLine, ts.iStartIndex);
                if (highlight)
                {
                    textView.SetSelection(ts.iStartLine, ts.iStartIndex, ts.iEndLine, ts.iEndIndex);
                }
                textView.EnsureSpanVisible(ts);
                return 0;
            }
            else
            {
                return unchecked((int)E_FILENOTFOUND); // the system can not find the file specified
            }
        }

        /// <summary>
        /// Does the location correspond to a certain document?
        /// </summary>
        /// <param name="docHier">The associated project (can be null)</param>
        /// <param name="docItemId">The item id (can be VSITEMID_NIL)</param>
        /// <param name="fpath">The file name</param>
        /// <returns><c>true</c> if the location is in this document</returns>
        public bool IsSameDocument(IVsHierarchy docHier, uint docItemId, string fpath)
        {
            Validate();

            // compare file names if we have no project info
            if (project == null || docHier == null)
            {
                return IsSameFile(fpath);
            }

            // otherwise compare the projects
            if (project == docHier || (projectName != null && Common.GetProjectName(docHier) == projectName))
            {
                // compare file names if the itemid's are nil
                if (docItemId == Nil || itemId == Nil)
                {
                    return IsSameFile(fpath);
                }
                // otherwise compare the itemids
                else
                {
                    return (itemId == docItemId);
                }
            }
            else
            {
                return false;
            }
        }

        public bool IsSameHierarchy(IVsHierarchy hier)
        {
            // Validate();
            return (hier != null && GetAssociatedHierarchy() == hier);
        }

        public IVsHierarchy GetAssociatedHierarchy()
        {
            Validate();
            if (project != null)
            {
                return project as IVsHierarchy;
            }
            else
            {
                // make a good guess..
                foreach (IVsHierarchy hier in Common.GetProjects())
                {
                    IVsProject proj = hier as IVsProject;
                    if (proj != null)
                    {
                        int found;
                        uint item;
                        VSDOCUMENTPRIORITY[] ps = { 0 };
                        proj.IsDocumentInProject(filePath, out found, ps, out item);
                        if (found != 0)
                        {
                            return hier;
                        }
                    }
                }
                return null;
            }
        }

        public void GetHierarchy(out object hierarchy, out uint hierarchyItem)
        {
            if (ValidProject())
            {
                hierarchy = project;
                hierarchyItem = itemId;
            }
            else
            {
                hierarchy = null;
                hierarchyItem = Nil;
            }
        }


        #endregion

        #region Span related methods
        /// <summary>
        /// Convert to a string that can be used in error messages
        /// </summary>
        /// <returns>An error message location</returns>
        public override string ToString()
        {
            return (FilePath + "(" + startLine + "," + startColumn + ")");
        }

        /// <summary>
        /// Compare to a file name/
        /// </summary>
        /// <param name="fname">A (relative) file name</param>
        /// <returns><c>true</c> if this location is in the same file</returns>
        public bool IsSameFile(string fname)
        {
            if (fname != null && fname.Length > 0)
            {
                string fpath = System.IO.Path.GetFullPath(fname);
                if (fpath != null && fpath.Length > 0)
                {
                    return (string.Compare(FilePath, fpath, StringComparison.OrdinalIgnoreCase) == 0);
                }
            }
            return false;
        }

        /// <summary>
        /// Do two locations overlap?
        /// </summary>
        public bool Overlaps(Location span)
        {
            // do the files match
            if (span == null) return false;
            if (!IsSameFile(span.FilePath)) return false;
            // are they line-overlapping?
            if (startLine > span.endLine ||
                endLine < span.startLine)
                return false;
            // are they line/column-overlapping?
            if ((startLine == span.endLine && startColumn > span.endColumn) ||
                (endLine == span.startLine && endColumn < span.startColumn))
                return false;
            // they overlap
            return true;
        }

        /// <summary>
        /// Get the textspan of a location.
        /// </summary>
        /// <returns></returns>
        public TextSpan TextSpan
        {
            get
            {
                TextSpan ts = new TextSpan();
                ts.iStartLine = startLine - 1;
                ts.iStartIndex = startColumn - 1;
                ts.iEndLine = endLine - 1;
                ts.iEndIndex = endColumn - 1;
                return ts;
            }
            set
            {
                SetSpan(value.iStartLine + 1, value.iStartIndex + 1, value.iEndLine + 1, value.iEndIndex + 1);
            }
        }
        #endregion
    }
}
