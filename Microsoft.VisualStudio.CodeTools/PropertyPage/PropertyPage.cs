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
using System.Reflection;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using System.Drawing;

namespace Microsoft.VisualStudio.CodeTools
{
  public class PropertyPage : IPropertyStorage, IPropertyPaneHost,
                              Microsoft.VisualStudio.OLE.Interop.IPropertyPage
  {
    #region Fields
    private bool initializing; // = false;
    private bool isDirty; // = false;
    private IPropertyPane pane; // = null;
    private IPropertyPageSite site; // = null;
    private IVsBuildPropertyStorage buildStorage; // = null;
    private object[] configs; // = null;
    private string[] configNames; // = null;
    #endregion

    #region Construction
    public PropertyPage(IPropertyPane pane)
    {
      this.pane = pane;
      pane.SetHost(this);
    }   
    #endregion

    #region IPropertyPage Members

    public void Activate(IntPtr hWndParent, RECT[] pRect, int bModal)
    {
      if (pane != null) {
        Win32Methods.SetParent(pane.Handle, hWndParent);
      }
    }

    public void Deactivate()
    {
      return;
    }

    public void GetPageInfo(PROPPAGEINFO[] pPageInfo)
    {
      if (pPageInfo != null && pPageInfo.Length > 0) {
        pPageInfo[0].cb = (uint)Marshal.SizeOf(typeof(PROPPAGEINFO));
        if (pane != null) {
          pPageInfo[0].dwHelpContext = (uint)pane.HelpContext;
          pPageInfo[0].pszHelpFile = pane.HelpFile;
          pPageInfo[0].pszDocString = null;
          pPageInfo[0].pszTitle = pane.Title;
          // return 0,0 for size so that a component can do its own scrolling.
          pPageInfo[0].SIZE.cx = 0; // pane.Size.Width;
          pPageInfo[0].SIZE.cy = 0; // pane.Size.Height;
        }
        else {
          pPageInfo[0].dwHelpContext = 0;
          pPageInfo[0].pszHelpFile = null;
          pPageInfo[0].pszDocString = null;
          pPageInfo[0].pszTitle = "<Unknown>";
          pPageInfo[0].SIZE.cx = 0;
          pPageInfo[0].SIZE.cy = 0;
        }
      }
    }

    public void Help(string pszHelpDir)
    {
      return;
    }

    public int IsPageDirty()
    {
      return (isDirty ? VSConstants.S_OK : VSConstants.S_FALSE);
    }

    public void Move(RECT[] pRect)
    {
      if (pane != null && pRect != null && pRect.Length > 0) {
        RECT r = pRect[0];
        pane.Location = new Point(r.left, r.top);
        pane.Size = new Size(r.right - r.left, r.bottom - r.top);
      }
    }

    public void SetPageSite(IPropertyPageSite pPageSite)
    {
      site = pPageSite;
    }

    public void Show(uint nCmdShow)
    {
      const int SW_HIDE = 0;
      if (pane != null) {
        if (nCmdShow != SW_HIDE)
          pane.Show();
        else
          pane.Hide();
      }
    }

    public int TranslateAccelerator(MSG[] pMsg)
    {
      if (site != null)
        return site.TranslateAccelerator(pMsg);
      else
        return 0;
    }

    public int Apply()
    {
      if (pane != null) {
        initializing = true;
         pane.SaveProperties(configNames, this);
        initializing = false;
        SetDirty(false);
      }
      return 0;
    }

    #endregion

    #region IPropertyPage.SetObjects
    private void SetConfigurationNames()
    {
      if (configs != null) {
        configNames = new string[configs.Length];
        for (int i = 0; i < configs.Length; i++) {
          IVsCfg cfg = configs[i] as IVsCfg;
          if (cfg != null) {
            cfg.get_DisplayName(out configNames[i]);            
          }
          if (configNames[i] == null) {
            configNames[i] = "";
          }
        }
      }
    }
    
    private void SetBuildStorage()
    {
      // Get the build storage associated with this property page.
      buildStorage = null;
      if (configs != null) {
        foreach (object config in configs) {
          if (config != null) {
            // try to retrieve the project using IVs(Cfg)BrowseObject
            IVsHierarchy project = null;
            IVsBrowseObject browse = config as IVsBrowseObject;
            if (browse != null) {
              uint itemid;
              browse.GetProjectItem(out project, out itemid);
            }
            else {
              IVsCfgBrowseObject cfgBrowse = config as IVsCfgBrowseObject;
              if (cfgBrowse != null) {
                uint itemid;
                cfgBrowse.GetProjectItem(out project, out itemid);
              }
            }

            // try to cast the project to a buildstorage
            buildStorage = project as IVsBuildPropertyStorage;
            if (buildStorage != null) {
              return;  // we are done.
            }
          }
        }
      }
    }

    public void SetObjects(uint cObjects, object[] ppunk)
    {
      configs = null;
      configNames = null;
      buildStorage = null;
      if (pane != null && ppunk != null) {
        configs = ppunk;
        SetBuildStorage();
        SetConfigurationNames();
        initializing = true;
         pane.LoadProperties(configNames, this);
        initializing = false;
        SetDirty(false);
      }
    }
    #endregion

    #region IPropertyStorage Members

    private object GetConfigObject(string configName)
    {
      if (configName != null && configs != null && configNames != null) {
        for (int i = 0; i < configNames.Length; i++) {
          if (String.CompareOrdinal(configNames[i], configName) == 0 &&
              i < configs.Length) {
            return configs[i];
          }
        }
      }
      return null;
    }

    public object GetProperties(bool perUser, string[] configNames, string propertyName, object defaultValue)
    {
      object result = null;
      if (configNames != null) {
        foreach (string configName in configNames) {
          object obj = GetProperty(perUser, configName, propertyName, defaultValue);
          if (obj == null) {  // error
            return null;  
          }
          else if (result == null) { // first result
            result = obj;
          }
          else if (!obj.Equals(result)) { // indeterminate
            return null;
          }
        }
      }
      return result;
    }

    public object GetProperty(bool perUser, string configName, string propertyName, object defaultValue)
    {
      System.Diagnostics.Debug.Assert(defaultValue != null);
      object value = null;

#if false // throws and seems to never work
      // first try to read it from the config object itself
      object config = GetConfigObject(configName);
      if (config != null) {
        value = GetDynamicProperty(propertyName, config);
      }
#endif
      // otherwise, try to read it from the buildstorage
      if (value == null && buildStorage != null) {
        string stringValue;
        int hr = buildStorage.GetPropertyValue(propertyName, configName
          , (uint)(perUser ? _PersistStorageType.PST_USER_FILE : _PersistStorageType.PST_PROJECT_FILE)
          , out stringValue);
        if (hr == 0) {
          value = stringValue;
        }
      }

      // return the found value
      if (value != null) {
        if (defaultValue != null) {
          try {
            return Convert.ChangeType(value, defaultValue.GetType(), CultureInfo.InvariantCulture);
          }
          catch (System.InvalidCastException) {
            return defaultValue;
          }
        }
        else {
          return value;
        }
      }
      else {
        return defaultValue;
      }
    }

    public T GetProperties<T>(bool perUser, string[] configNames, string propertyName, T defaultValue, out bool anyFound, out bool indeterminate ) where T : IComparable<T>
    {
      T result = defaultValue;
      bool first = true;
      bool allFound = true;
      anyFound = false;
      indeterminate = false;

      if (configNames != null) {
        foreach (string configName in configNames) {
          bool found;
          T obj = GetProperty(perUser, configName, propertyName, defaultValue, out found);
          allFound = allFound & found;
          anyFound = anyFound | found;
          if (first) {
            result = obj;
            first = false;
          }
          else {            
            if ((obj == null && result != null) || (obj != null && !obj.Equals(result))) {
              indeterminate = true;
            }
          }
        }
      }            
      return result;
    }

    public T GetProperty<T>(bool perUser, string configName, string propertyName, T defaultValue, out bool found)
    {
      found = false;
      object value = null;

#if false // throws
      // first try to read it from the config object itself
      object config = GetConfigObject(configName);
      if (config != null) {
        value = GetDynamicProperty(propertyName, config);
      }
#endif

      // otherwise, try to read it from the buildstorage
      if (value == null && buildStorage != null) {
        string stringValue;
        int hr = buildStorage.GetPropertyValue(propertyName, configName
          , (uint)(perUser ? _PersistStorageType.PST_USER_FILE : _PersistStorageType.PST_PROJECT_FILE)
          , out stringValue);
        if (hr == 0) {
          value = stringValue;
        }
      }

      // return the found value
      T result = defaultValue;
      if (value != null) {
        found = true;
        try {
          result = (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
        }
        catch (System.InvalidCastException) {
        }
      }
      return result;
    }

    public int RemoveProperty(bool perUser, string configName, string propertyName)
    {
      if (buildStorage == null) 
        return 0;
      else
        return buildStorage.RemoveProperty(propertyName, configName, (uint)(perUser ? _PersistStorageType.PST_USER_FILE : _PersistStorageType.PST_PROJECT_FILE));
    }

    /// <summary>Get a dynamic dispatch property on an IDispatch object</summary>
    private static object GetDynamicProperty(string propertyName, object dispatch)
    {
      try {
        if (dispatch != null) {
          Type tp = dispatch.GetType();
          if (tp != null) {
            return tp.InvokeMember(propertyName, BindingFlags.GetProperty, null, dispatch, null,  CultureInfo.InvariantCulture);
          }
        }
      }
      catch { }
      return null;
    }

    public int SetProperties(bool perUser, string[] configNames, string propertyName, object value)
    {
      int hr = 0;
      if (configNames != null) {
        foreach (string configName in configNames) {
          int res = SetProperty(perUser, configName, propertyName, value);
          if (res != 0) {
            hr = res;
          }
        }
      }
      return hr;
    }

    public int SetProperty(bool perUser, string configName, string propertyName, object value)
    {
      if (value == null) return 0;
      int hr;

#if false
      // Throws and seems to never work

      // first try to set it on the config object directly
      object config = GetConfigObject(configName);
      if (config != null) {
        hr = SetDynamicProperty(propertyName, value, config);
        if (hr == 0) {
          return hr;
        }
      }
#endif
      // otherwise, set it on the build storage
      if (buildStorage != null) {
        hr = buildStorage.SetPropertyValue(propertyName, configName
          , (uint)(perUser ? _PersistStorageType.PST_USER_FILE : _PersistStorageType.PST_PROJECT_FILE)
          , value.ToString());
        return hr;
      }
      else
        return VSConstants.E_FAIL;
    }

    /// <summary>Set a dynamic dispatch property on an IDispatch object</summary>
    private static int SetDynamicProperty(string propertyName, object val, object dispatch)
    {
      try {
        if (dispatch != null) {
          Type tp = dispatch.GetType();
          if (tp != null) {
            object[] args = { val };
            tp.InvokeMember(propertyName, BindingFlags.SetProperty, null, dispatch, args, CultureInfo.InvariantCulture);
            return 0;
          }
        }
      }
      catch { }
      return VSConstants.E_FAIL;
    }

    #endregion

    #region IPropertyPaneHost Members

    public void PropertiesChanged()
    {
      SetDirty(true);
    }

    private void SetDirty(bool dirty)
    {
      if (dirty != isDirty && !initializing) {
        isDirty = dirty;
        if (site != null) {
          site.OnStatusChange((uint)(dirty ? PROPPAGESTATUS.PROPPAGESTATUS_DIRTY | PROPPAGESTATUS.PROPPAGESTATUS_VALIDATE 
                                           : PROPPAGESTATUS.PROPPAGESTATUS_CLEAN));
        }
      }
    }

    #endregion
  }
}
