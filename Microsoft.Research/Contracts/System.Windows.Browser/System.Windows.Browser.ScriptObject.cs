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
using System.Diagnostics.Contracts;
using System.Windows.Threading;

namespace System.Windows.Browser
{
  // Summary:
  //     Defines the core behavior for the System.Windows.Browser.HtmlObject class,
  //     and provides a base class for browser Document Object Model (DOM) access
  //     types.
  public class ScriptObject
  {
    internal ScriptObject() { }

    // Summary:
    //     Gets an instance of the dispatcher.
    public Dispatcher Dispatcher
    {
      get
      {
        Contract.Ensures(Contract.Result<Dispatcher>() != null);
        return default(Dispatcher);
      }
    }
    //
    // Summary:
    //     Gets the underlying managed object reference of the System.Windows.Browser.ScriptObject.
    //
    // Returns:
    //     A managed object reference if the current System.Windows.Browser.ScriptObject
    //     wraps a managed type; otherwise, null.
    extern public object ManagedObject { get; }

    // Summary:
    //     Determines whether the current thread is the browser's UI thread.
    //
    // Returns:
    //     true if the current thread is the browser's UI thread; false if it is a background
    //     thread.
    [Pure]
    extern public bool CheckAccess();
    //
    // Summary:
    //     Converts the current scriptable object to a specified type.
    //
    // Type parameters:
    //   T:
    //     The type to convert the current scriptable object to.
    //
    // Returns:
    //     An object of type T.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The conversion failed or is not supported.
    [Pure]
    extern public T ConvertTo<T>();
    //
    // Summary:
    //     Converts the current scriptable object to a specified type, with serialization
    //     support.
    //
    // Parameters:
    //   targetType:
    //     The type to convert the current scriptable object to.
    //
    //   allowSerialization:
    //     A flag which enables the current scriptable object to be serialized.
    //
    // Returns:
    //     An object of type targetType.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     The conversion failed or is not supported.
    [Pure]
    protected internal virtual object ConvertTo(Type targetType, bool allowSerialization)
    {
      Contract.Ensures(Contract.Result<object>() != null);
      return default(object);
    }
    //
    // Summary:
    //     Gets the value of a property that is identified by ordinal number on the
    //     current scriptable object.
    //
    // Parameters:
    //   index:
    //     The ordinal number of the property.
    //
    // Returns:
    //     A null reference (Nothing in Visual Basic) if the property does not exist
    //     or if the underlying System.Windows.Browser.ScriptObject is a managed type.
    [Pure]
    extern public object GetProperty(int index);
    //
    // Summary:
    //     Gets the value of a property that is identified by name on the current scriptable
    //     object.
    //
    // Parameters:
    //   name:
    //     The name of the property.
    //
    // Returns:
    //     A null reference (Nothing in Visual Basic) if the property does not exist
    //     or if the underlying System.Windows.Browser.ScriptObject is a managed type.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     name is null.
    //
    //   System.ArgumentException:
    //     name is an empty string.  -or- name contains an embedded null character (\0).
    [Pure]
    public virtual object GetProperty(string name)
    {
      Contract.Requires(name != null);
      return default(object);
    }
    //
    // Summary:
    //     Initializes a scriptable object.
    //
    // Parameters:
    //   handle:
    //     The handle to the object to initialize.
    //
    //   identity:
    //     The identity of the object.
    //
    //   addReference:
    //     true to specify that the HTML Bridge should assign a reference count to this
    //     object; otherwise, false.
    //
    //   releaseReferenceOnDispose:
    //     true to release the reference on dispose; otherwise, false.
    extern protected void Initialize(IntPtr handle, IntPtr identity, bool addReference, bool releaseReferenceOnDispose);
    //
    // Summary:
    //     Invokes a method on the current scriptable object, and optionally passes
    //     in one or more method parameters.
    //
    // Parameters:
    //   name:
    //     The method to invoke.
    //
    //   args:
    //     Parameters to be passed to the method.
    //
    // Returns:
    //     An object that represents the return value from the underlying JavaScript
    //     method.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     name is null.
    //
    //   System.ArgumentException:
    //     name is an empty string.  -or- name contains an embedded null character (\0).
    //      -or- The method does not exist or is not scriptable.
    //
    //   System.InvalidOperationException:
    //     The underlying method invocation results in an error. The .NET Framework
    //     attempts to return the error text that is associated with the error.
    public virtual object Invoke(string name, params object[] args)
    {
      Contract.Requires(name != null);
      Contract.Ensures(Contract.Result<object>() != null);
      return default(object);
    }
    //
    // Summary:
    //     Invokes the current System.Windows.Browser.ScriptObject and assumes that
    //     it represents a JavaScript method.
    //
    // Parameters:
    //   args:
    //     Parameters to be passed to the underlying JavaScript method.
    //
    // Returns:
    //     An object that represents the return value from the underlying JavaScript
    //     method.
    //
    // Exceptions:

    //   System.InvalidOperationException:
    //     The current System.Windows.Browser.ScriptObject is not a method.  -or- The
    //     underlying method invocation results in an error.
    public virtual object InvokeSelf(params object[] args)
    {
      Contract.Ensures(Contract.Result<object>() != null);
      return default(object);
    }
    //
    // Summary:
    //     Sets the value of a property that is identified by ordinal number on the
    //     current scriptable object.
    //
    // Parameters:
    //   index:
    //     The ordinal number of the property.
    //
    //   value:
    //     The value to set the property to.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     index is null.
    //
    //   System.ArgumentException:
    //     index identifies an empty string.
    //
    //   System.InvalidOperationException:
    //     A type mismatch exists between the supplied type and the target property.
    //      -or- The property is not settable.  -or- All other failures.
    extern public void SetProperty(int index, object value);
    //
    // Summary:
    //     Sets a property that is identified by name on the current scriptable object.
    //
    // Parameters:
    //   name:
    //     The name of the property.
    //
    //   value:
    //     The value to set the property to.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     name is null.
    //
    //   System.ArgumentException:
    //     name is an empty string.  -or- name contains an embedded null character (\0).
    //
    //   System.InvalidOperationException:
    //     A type mismatch exists between the supplied type and the target property.
    //      -or- The property is not settable.  -or- All other failures.
    public virtual void SetProperty(string name, object value)
    {
      Contract.Requires(name != null);
    }
  }
}
