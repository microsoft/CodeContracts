// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.VisualStudio.CodeTools
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using Microsoft.VisualStudio.OLE.Interop;
    using Microsoft.VisualStudio.Shell.Interop;

    // Collect OLE error codes.
    static internal class OLECMDERR
    {
        public const int
          OLE_E_LAST = unchecked((int)0x800400FF),
          OLECMDERR_E_FIRST = OLE_E_LAST + 1,
          OLECMDERR_E_NOTSUPPORTED = OLECMDERR_E_FIRST,
          OLECMDERR_E_DISABLED = OLECMDERR_E_FIRST + 1,
          OLECMDERR_E_NOHELP = OLECMDERR_E_FIRST + 2,
          OLECMDERR_E_CANCELED = OLECMDERR_E_FIRST + 3,
          OLECMDERR_E_UNKNOWNGROUP = OLECMDERR_E_FIRST + 4;
    }

    // The status of a menu item
    internal enum MenuStatus
    {
        Disabled,
        Invisible,
        Enabled,
        Checked,
        Unchecked
    }

    // Basic menu items
    internal class MenuItem
    {
        #region Properties
        protected ITaskMenuItem taskMenuItem;  // The identifying task menu item
        protected List<Task> tasks;            // Tasks listening to this menu item
        protected MenuStatus status;           // The status

        internal MenuStatus Status
        {
            get { return status; }
        }

        internal string Caption
        {
            get { return (taskMenuItem == null ? "<root>" : taskMenuItem.Caption); }
        }

        internal ITaskMenuItem TaskMenuItem
        {
            get { return taskMenuItem; }
        }
        #endregion

        #region Methods
        internal MenuItem(ITaskMenuItem menuItem)
        {
            this.taskMenuItem = menuItem;
            tasks = new List<Task>(1);
            status = MenuStatus.Disabled;
        }

        internal void AddTask(Task task)
        {
            tasks.Add(task);
        }

        internal void SetStatus(int totalSelected)
        {
            if (// disable on errors
                tasks == null || taskMenuItem == null ||
                // disable if not supported by all selected tasks
                tasks.Count == 0 || tasks.Count != totalSelected ||
                // disable if null tasks or not enabled by all tasks
                tasks.Exists(delegate (Task t) { return (t == null || !t.IsEnabled(taskMenuItem)); })
               )
            {
                status = MenuStatus.Disabled;
            }
            // Set checkable items (disable if selected tasks disagree on checked state)
            else if (taskMenuItem.Kind == TaskMenuKind.Checkable && tasks != null)
            {
                Task task0 = tasks[0];
                if (task0 != null)
                {
                    bool isChecked = task0.IsChecked(taskMenuItem);
                    if (tasks.TrueForAll(delegate (Task t) { return (t != null && isChecked == t.IsChecked(taskMenuItem)); }))
                    {
                        status = (isChecked ? MenuStatus.Checked : MenuStatus.Unchecked);
                    }
                    else
                    {
                        status = MenuStatus.Disabled;
                    }
                }
                else
                    status = MenuStatus.Disabled;
            }
            // Disable singular menus if multiple selected tasks
            else if (taskMenuItem.Kind == TaskMenuKind.Singular)
            {
                status = (tasks.Count == 1 ? MenuStatus.Enabled : MenuStatus.Disabled);
            }
            // normal menu item
            else
            {
                status = MenuStatus.Enabled;
            }
        }

        internal virtual void OnCommand()
        {
            foreach (Task task in tasks)
            {
                if (task != null)
                {
                    task.ExecCommand(taskMenuItem);
                }
            }
        }
        #endregion
    }

    // Sub menus extend menu items
    internal class SubMenu : MenuItem
    {
        #region Fields
        protected static Guid taskManagerCmdGroup = new Guid("{2CDA027E-722C-4148-B953-6CCE16AA982D}");

        protected List<MenuItem> menuItems; // the sub menu items
        protected int subMenuCount;         // count of submenus in this submenu
        protected List<int> indices;        // submenu path used to encode the group guid
        protected Guid cmdGroup;            // the OLE command group
        #endregion

        #region Construction
        public SubMenu(ITaskMenuItem item, List<int> indices) : base(item)
        {
            this.indices = indices;
            // menuItems    = null;
            // subMenuCount = 0;

            // root menu has a single 0 index
            if (this.indices == null)
            {
                this.indices = new List<int>(1);
                this.indices.Add(0);
            }

            // create a command group guid based on the submenu path
            // for example, a submenu of a second submenu in the root menu
            // gets a path: 0,1,0. The guid will end with this path.
            // The CTC file hard-codes these guids 
            Byte[] cmdGroupBytes = taskManagerCmdGroup.ToByteArray();
            if (cmdGroupBytes != null)
            {
                for (int i = 0; i < this.indices.Count; i++)
                {
                    cmdGroupBytes[16 - this.indices.Count + i] = (Byte)(this.indices[i]);
                }
            }
            cmdGroup = new Guid(cmdGroupBytes);
        }

        // Force refresh next time
        protected virtual void Invalidate()
        {
            menuItems = null;
            subMenuCount = 0;
        }

        // refresh the item list, possibly reloading the menu information
        protected virtual void Refresh()
        {
            if (menuItems == null)
            {
                Reload();
            }
        }

        protected void Reload()
        {
            // Common.Trace("Reload menu info: " + Caption);

            // Build a new menu
            menuItems = new List<MenuItem>(1);
            subMenuCount = 0;

            foreach (Task task in tasks)
            {
                ITaskMenuItem[] taskMenuItems = (task == null ? null : task.GetMenuItems(this.taskMenuItem));

                // add to the context menu item list
                if (taskMenuItems != null)
                {
                    for (int i = 0; i < taskMenuItems.Length; i++)
                    {
                        ITaskMenuItem taskMenuItem = taskMenuItems[i];
                        if (taskMenuItem != null)
                        {
                            // update an existing menu or add a new one
                            int index = menuItems.FindIndex(delegate (MenuItem inf)
                            {
                                return (inf == null ? false : inf.TaskMenuItem == taskMenuItem);
                            });
                            MenuItem item;
                            if (index >= 0)
                            {
                                item = menuItems[index];
                            }
                            else
                            {
                                if (taskMenuItem.Kind == TaskMenuKind.Submenu)
                                {
                                    List<int> path = new List<int>(indices);
                                    path.Add(subMenuCount);
                                    subMenuCount++;
                                    item = new SubMenu(taskMenuItem, path);
                                }
                                else
                                {
                                    item = new MenuItem(taskMenuItem);
                                }
                                menuItems.Add(item);
                            }
                            item.AddTask(task);
                        }
                    }
                }
            }

            // finally, set the status of each command:
            // - disable those menus that are not supported by all selected tasks
            // - set checkable menus
            // - disable those checkable menus that disagree on the checked state
            for (int item = 0; item < menuItems.Count; item++)
            {
                MenuItem menuItem = menuItems[item];
                if (menuItem != null)
                    menuItem.SetStatus(tasks.Count);
            }
        }
        #endregion

        #region Submenus

        // Get a menu item at a specified index. 
        internal MenuItem GetMenuItem(int index, bool refresh)
        {
            if (refresh)
            {
                Refresh();
            }

            if (menuItems != null)
            {
                for (int i = 0; i < menuItems.Count && index >= 0; i++)
                {
                    MenuItem menuItem = menuItems[i];
                    if (menuItem != null)
                    {
                        ITaskMenuItem taskItem = menuItem.TaskMenuItem;
                        if (taskItem != null && taskItem.Kind != TaskMenuKind.Submenu)
                        {
                            if (index == 0)
                            {
                                return menuItem;
                            }
                            else
                            {
                                index--;
                            }
                        }
                    }
                }
            }
            return null;
        }

        // Get a submenu item.
        internal SubMenu GetSubMenu(int index)
        {
            Refresh();
            if (menuItems != null)
            {
                for (int i = 0; i < menuItems.Count && index >= 0; i++)
                {
                    MenuItem menuItem = menuItems[i];
                    if (menuItem != null)
                    {
                        ITaskMenuItem taskItem = menuItem.TaskMenuItem;
                        if (taskItem != null && taskItem.Kind == TaskMenuKind.Submenu)
                        {
                            if (index == 0)
                            {
                                return menuItem as SubMenu;
                            }
                            else
                            {
                                index--;
                            }
                        }
                    }
                }
            }
            return null;
        }

        // Enumerate all submenus.
        public IEnumerable<SubMenu> SubMenus(bool refresh)
        {
            if (refresh) Refresh();

            if (menuItems != null)
            {
                foreach (MenuItem item in menuItems)
                {
                    if (item != null)
                    {
                        SubMenu subMenu = item as SubMenu;
                        if (subMenu != null)
                        {
                            yield return subMenu;
                        }
                    }
                }
            }
        }

        internal override void OnCommand()
        {
            System.Diagnostics.Debug.Assert(false, "Sub menus have no command handler");
            return;
        }
        #endregion

        #region IOleCommandTarget
        private const uint customStart = 0x400;
        private const uint customMenuStart = 0x1000;

        public int Exec(ref Guid pguidCmdGroup, uint nCmdID, uint nCmdexecopt, IntPtr pvaIn, IntPtr pvaOut)
        {
            if (pguidCmdGroup == cmdGroup)
            {
                // A command in this submenu 
                MenuItem item = GetMenuItem((int)(nCmdID - customStart), false);
                if (item != null)
                {
                    item.OnCommand();
                    Invalidate(); // might cause changed menu items etc.
                    return 0;
                }
                else
                {
                    return VSConstants.E_INVALIDARG;
                }
            }
            else
            {
                foreach (SubMenu subMenu in SubMenus(false))
                {
                    if (subMenu != null)
                    {
                        // propagate query to all submenus
                        int hr = subMenu.Exec(ref pguidCmdGroup, nCmdID, nCmdexecopt, pvaIn, pvaOut);
                        if (hr != OLECMDERR.OLECMDERR_E_UNKNOWNGROUP)
                        {
                            if (hr == 0)
                            {
                                // command was executed in submenu;
                                Invalidate();
                            }
                            return hr;
                        }
                    }
                }
                return OLECMDERR.OLECMDERR_E_UNKNOWNGROUP;
            }
        }

        public int QueryStatus(ref Guid guidCmdGroup, uint cCmds, OLECMD[] prgCmds, IntPtr pCmdText)
        {
            if (prgCmds == null || cCmds != 1)
            {
                return VSConstants.E_INVALIDARG;
            }
            else if (guidCmdGroup == cmdGroup)
            {
                // Common.Trace("QueryStatus: " + Caption + ": " + (prgCmds[0].cmdID - customStart));
                prgCmds[0].cmdf = (uint)(OLECMDF.OLECMDF_SUPPORTED);

                bool isSubMenu = prgCmds[0].cmdID >= customMenuStart;
                int index = (isSubMenu ? (int)(prgCmds[0].cmdID - customMenuStart)
                                       : (int)(prgCmds[0].cmdID - customStart));
                MenuItem item = (isSubMenu ? GetSubMenu(index) : GetMenuItem(index, true));

                if (item == null)
                {
                    prgCmds[0].cmdf |= (uint)(OLECMDF.OLECMDF_ENABLED | OLECMDF.OLECMDF_INVISIBLE);
                    //We must return S_OK or otherwise all task items show custom menus
                    //However, we must also say E_NOTSUPPORTED or QueryStatus loops
                    //So, we just return S_OK for the first 16 queries :-(
                    return (index > 16 ? OLECMDERR.OLECMDERR_E_NOTSUPPORTED : 0);
                }
                else
                {
                    switch (item.Status)
                    {
                        case MenuStatus.Enabled: prgCmds[0].cmdf |= (uint)(OLECMDF.OLECMDF_ENABLED); break;
                        case MenuStatus.Checked: prgCmds[0].cmdf |= (uint)(OLECMDF.OLECMDF_ENABLED | OLECMDF.OLECMDF_LATCHED); break;
                        case MenuStatus.Unchecked: prgCmds[0].cmdf |= (uint)(OLECMDF.OLECMDF_ENABLED | OLECMDF.OLECMDF_NINCHED); break;
                        case MenuStatus.Invisible: prgCmds[0].cmdf |= (uint)(OLECMDF.OLECMDF_ENABLED | OLECMDF.OLECMDF_INVISIBLE); break;
                        case MenuStatus.Disabled:
                            // Submenus somehow do not disable, so we make them invisible..
                            if (isSubMenu)
                            {
                                prgCmds[0].cmdf |= (uint)(OLECMDF.OLECMDF_ENABLED | OLECMDF.OLECMDF_INVISIBLE); break;
                            };
                            break;
                        default:
                            break;
                    }
                    SetCommandText(pCmdText, item.Caption);
                    return 0;
                }
            }
            else
            {
                // Common.Trace("QueryStatus: " + guidCmdGroup + "\n  " + Caption + ": " + (prgCmds[0].cmdID - customStart));
                foreach (SubMenu subMenu in SubMenus(false))
                {
                    if (subMenu != null)
                    {
                        // propagate query to all submenus
                        int hr = subMenu.QueryStatus(ref guidCmdGroup, cCmds, prgCmds, pCmdText);
                        if (hr != OLECMDERR.OLECMDERR_E_UNKNOWNGROUP)
                        {
                            return hr;
                        }
                    }
                }
                return OLECMDERR.OLECMDERR_E_UNKNOWNGROUP;
            }
        }

        // Custom marshalling to set the text of a menu item
        static private bool SetCommandText(IntPtr cmdText, string text)
        {
            if (cmdText != IntPtr.Zero && text != null)
            {
                int ofsFlags = 0;
                int ofsActualSize = ofsFlags + 4 /* sizeof(UInt32)*/;
                int ofsBufSize = ofsActualSize + UIntPtr.Size;
                int ofsChar = ofsBufSize + UIntPtr.Size;

                int flags = Marshal.ReadInt32(cmdText, ofsFlags);
                if (flags == 1)
                {
                    int bufSize = Marshal.ReadInt32(cmdText, ofsBufSize);
                    int max = (bufSize - 1 > text.Length ? text.Length : bufSize - 1);
                    for (int i = 0; i < max; i++)
                    {
                        Marshal.WriteInt16(cmdText, ofsChar + 2 /* sizeof(Int16) */ * i, text[i]);
                    }
                    Marshal.WriteInt16(cmdText, ofsChar + 2 /* sizeof(Int16) */ * max, '\0');

                    if (UIntPtr.Size == 8)
                    {
                        Marshal.WriteInt64(cmdText, ofsActualSize, text.Length);
                    }
                    else
                    {
                        Marshal.WriteInt32(cmdText, ofsActualSize, text.Length);
                    }
                    return true;
                }
            }
            return false;
        }
        #endregion
    }

    // Root menu specializes a submenu for the top-level context menu
    internal class RootMenu : SubMenu
    {
        private IVsTaskList2 taskList; // The global task list object

        public RootMenu() : base(null, null)
        {
            taskList = Common.GetService(typeof(SVsErrorList)) as IVsTaskList2;
        }

        public void Release()
        {
            taskList = null; // this also disables menu handling
        }


        // Refresh checks "smartly" if the selected tasks have changed
        // or not and only reloads all menu information on a change.
        protected override void Refresh()
        {
            // Check if we really need to reload..
            List<Task> selected = GetSelectedTasks(taskList);
            if (selected == null || selected.Count == 0)
            {
                menuItems = null;
                tasks = null;
                return;
            }

            // Check if cached info is still the same..
            if (menuItems != null && tasks != null && tasks.Count == selected.Count)
            {
                bool equal = true;
                for (int i = 0; i < selected.Count; i++)
                {
                    if (tasks[i] != selected[i])
                    {
                        equal = false;
                        break;
                    }
                }
                if (equal) return;  // cached information is up-to-date
            }

            // Otherwise, reload with the new selected tasks
            tasks = selected;
            Reload();
        }

        // Get the currently selected task items
        static public List<Task> GetSelectedTasks(IVsTaskList2 taskList)
        {
            if (taskList == null) return null;

            int selectedCount = 0;
            taskList.GetSelectionCount(out selectedCount);
            if (selectedCount <= 0) return null;

            IVsEnumTaskItems enumItems;
            int hr = taskList.EnumSelectedItems(out enumItems);
            if (hr != 0 || enumItems == null) return null;

            List<Task> items = new List<Task>(selectedCount);
            uint[] fetched = { 0 };
            IVsTaskItem[] elems = { null };
            do
            {
                hr = enumItems.Next(1, elems, fetched);
                if (fetched[0] == 1)
                {
                    Task task = elems[0] as Task;
                    items.Add(task); // can add null items
                }
            }
            while (hr == 0 && fetched[0] == 1);

            return items;
        }
    }
}