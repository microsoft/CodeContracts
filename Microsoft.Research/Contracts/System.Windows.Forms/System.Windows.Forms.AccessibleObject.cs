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

// using Accessibility;
using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    /// <summary>
    /// Provides information that accessibility applications use to adjust an application's user interface (UI) for users with impairments.
    /// </summary>
    public class AccessibleObject : StandardOleMarshalObject
        // IReflect, IAccessible, UnsafeNativeMethods.IEnumVariant, UnsafeNativeMethods.IOleWindow
    {
        // <summary>
        // Gets the location and size of the accessible object.
        // </summary>
        // 
        // <returns>
        // A <see cref="T:System.Drawing.Rectangle"/> that represents the bounds of the accessible object.
        // </returns>
        // <exception cref="T:System.Runtime.InteropServices.COMException">The bounds of control cannot be retrieved. </exception>
        // public virtual Rectangle Bounds { get; }
        
        // <summary>
        // Gets a string that describes the default action of the object. Not all objects have a default action.
        // </summary>
        // 
        // <returns>
        // A description of the default action for an object, or null if this object has no default action.
        // </returns>
        // <exception cref="T:System.Runtime.InteropServices.COMException">The default action for the control cannot be retrieved. </exception>
        // public virtual string DefaultAction {get;}
        
        // <summary>
        // Gets a string that describes the visual appearance of the specified object. Not all objects have a description.
        // </summary>
        // 
        // <returns>
        // A description of the object's visual appearance to the user, or null if the object does not have a description.
        // </returns>
        // <exception cref="T:System.Runtime.InteropServices.COMException">The description for the control cannot be retrieved. </exception>
        // public virtual string Description { get; }
        
        // <summary>
        // Gets a description of what the object does or how the object is used.
        // </summary>
        // 
        // <returns>
        // A <see cref="T:System.String"/> that contains the description of what the object does or how the object is used. Returns null if no help is defined.
        // </returns>
        // <exception cref="T:System.Runtime.InteropServices.COMException">The help string for the control cannot be retrieved. </exception>
        // public virtual string Help {get;}
        
        // <summary>
        // Gets the shortcut key or access key for the accessible object.
        // </summary>
        // 
        // <returns>
        // The shortcut key or access key for the accessible object, or null if there is no shortcut key associated with the object.
        // </returns>
        // <exception cref="T:System.Runtime.InteropServices.COMException">The shortcut for the control cannot be retrieved. </exception>
        // public virtual string KeyboardShortcut {get;}
        
        // <summary>
        // Gets or sets the object name.
        // </summary>
        // 
        // <returns>
        // The object name, or null if the property has not been set.
        // </returns>
        // <exception cref="T:System.Runtime.InteropServices.COMException">The name of the control cannot be retrieved or set. </exception>
        // public virtual string Name {get; set;}
        
        // <summary>
        // Gets the parent of an accessible object.
        // </summary>
        // 
        // <returns>
        // An <see cref="T:System.Windows.Forms.AccessibleObject"/> that represents the parent of an accessible object, or null if there is no parent object.
        // </returns>
        // <PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode"/></PermissionSet>
        // public virtual AccessibleObject Parent { get; }
        
        // <summary>
        // Gets the role of this accessible object.
        // </summary>
        // 
        // <returns>
        // One of the <see cref="T:System.Windows.Forms.AccessibleRole"/> values, or <see cref="F:System.Windows.Forms.AccessibleRole.None"/> if no role has been specified.
        // </returns>
        // public virtual AccessibleRole Role { get; }
        
        // <summary>
        // Gets the state of this accessible object.
        // </summary>
        // 
        // <returns>
        // One of the <see cref="T:System.Windows.Forms.AccessibleStates"/> values, or <see cref="F:System.Windows.Forms.AccessibleStates.None"/>, if no state has been set.
        // </returns>
        // public virtual AccessibleStates State {get;}
       
        // <summary>
        // Gets or sets the value of an accessible object.
        // </summary>
        // 
        // <returns>
        // The value of an accessible object, or null if the object has no value set.
        // </returns>
        // <exception cref="T:System.Runtime.InteropServices.COMException">The value cannot be set or retrieved. </exception><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode"/></PermissionSet>
        // public virtual string Value {get; set;}
        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Windows.Forms.AccessibleObject"/> class.
        /// </summary>
        public AccessibleObject()
        {
        }
        
        /// <summary>
        /// Retrieves the accessible child corresponding to the specified index.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Windows.Forms.AccessibleObject"/> that represents the accessible child corresponding to the specified index.
        /// </returns>
        /// <param name="index">The zero-based index of the accessible child. </param>
        public virtual AccessibleObject GetChild(int index)
        {
            Contract.Requires(index >= 0);
            return default(AccessibleObject);
        }

        // <summary>
        // Retrieves the number of children belonging to an accessible object.
        // </summary>
        // 
        // <returns>
        // The number of children belonging to an accessible object.
        // </returns>
        // public virtual int GetChildCount()
        
        
        // <summary>
        // Retrieves the object that has the keyboard focus.
        // </summary>
        // 
        // <returns>
        // An <see cref="T:System.Windows.Forms.AccessibleObject"/> that specifies the currently focused child. This method returns the calling object if the object itself is focused. Returns null if no object has focus.
        // </returns>
        // <exception cref="T:System.Runtime.InteropServices.COMException">The control cannot be retrieved. </exception>
        // public virtual AccessibleObject GetFocused()
        
        // <summary>
        // Gets an identifier for a Help topic identifier and the path to the Help file associated with this accessible object.
        // </summary>
        // 
        // <returns>
        // An identifier for a Help topic, or -1 if there is no Help topic. On return, the <paramref name="fileName"/> parameter contains the path to the Help file associated with this accessible object.
        // </returns>
        // <param name="fileName">On return, this property contains the path to the Help file associated with this accessible object. </param><exception cref="T:System.Runtime.InteropServices.COMException">The Help topic for the control cannot be retrieved. </exception><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        // public virtual int GetHelpTopic(out string fileName)
        
        // <summary>
        // Retrieves the currently selected child.
        // </summary>
        // 
        // <returns>
        // An <see cref="T:System.Windows.Forms.AccessibleObject"/> that represents the currently selected child. This method returns the calling object if the object itself is selected. Returns null if is no child is currently selected and the object itself does not have focus.
        // </returns>
        // <exception cref="T:System.Runtime.InteropServices.COMException">The selected child cannot be retrieved. </exception>
        // public virtual AccessibleObject GetSelected()
        
        // <summary>
        // Retrieves the child object at the specified screen coordinates.
        // </summary>
        // 
        // <returns>
        // An <see cref="T:System.Windows.Forms.AccessibleObject"/> that represents the child object at the given screen coordinates. This method returns the calling object if the object itself is at the location specified. Returns null if no object is at the tested location.
        // </returns>
        // <param name="x">The horizontal screen coordinate. </param><param name="y">The vertical screen coordinate. </param><exception cref="T:System.Runtime.InteropServices.COMException">The control cannot be hit tested. </exception>
        // public virtual AccessibleObject HitTest(int x, int y)
        
        // <summary>
        // Performs the default action associated with this accessible object.
        // </summary>
        // <exception cref="T:System.Runtime.InteropServices.COMException">The default action for the control cannot be performed. </exception><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode"/></PermissionSet>
        // public virtual void DoDefaultAction()
        
        // <summary>
        // Navigates to another accessible object.
        // </summary>
        // 
        // <returns>
        // An <see cref="T:System.Windows.Forms.AccessibleObject"/> that represents one of the <see cref="T:System.Windows.Forms.AccessibleNavigation"/> values.
        // </returns>
        // <param name="navdir">One of the <see cref="T:System.Windows.Forms.AccessibleNavigation"/> values. </param><exception cref="T:System.Runtime.InteropServices.COMException">The navigation attempt fails. </exception><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode"/></PermissionSet>
        // public virtual AccessibleObject Navigate(AccessibleNavigation navdir)
        
        // <summary>
        // Modifies the selection or moves the keyboard focus of the accessible object.
        // </summary>
        // <param name="flags">One of the <see cref="T:System.Windows.Forms.AccessibleSelection"/> values. </param><exception cref="T:System.Runtime.InteropServices.COMException">The selection cannot be performed. </exception><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode"/></PermissionSet>
        // public virtual void Select(AccessibleSelection flags)
        
        // <summary>
        // Associates an object with an instance of an <see cref="T:System.Windows.Forms.AccessibleObject"/> based on the handle of the object.
        // </summary>
        // <param name="handle">An <see cref="T:System.IntPtr"/> that contains the handle of the object. </param>
        // protected void UseStdAccessibleObjects(IntPtr handle)
        
        // <summary>
        // Associates an object with an instance of an <see cref="T:System.Windows.Forms.AccessibleObject"/> based on the handle and the object id of the object.
        // </summary>
        // <param name="handle">An <see cref="T:System.IntPtr"/> that contains the handle of the object. </param><param name="objid">An Int that defines the type of object that the <paramref name="handle"/> parameter refers to. </param>
        // protected void UseStdAccessibleObjects(IntPtr handle, int objid)
    }
}
