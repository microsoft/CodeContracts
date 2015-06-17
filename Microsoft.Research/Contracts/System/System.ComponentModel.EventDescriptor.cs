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

#if !SILVERLIGHT

using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel
{
  // Summary:
  //     Provides information about an event.
  //[ComVisible(true)]
  public abstract class EventDescriptor //: MemberDescriptor
  {
    // Summary:
    //     Initializes a new instance of the System.ComponentModel.EventDescriptor class
    //     with the name and attributes in the specified System.ComponentModel.MemberDescriptor.
    //
    // Parameters:
    //   descr:
    //     A System.ComponentModel.MemberDescriptor that contains the name of the event
    //     and its attributes.
    //protected EventDescriptor(MemberDescriptor descr);
    //
    // Summary:
    //     Initializes a new instance of the System.ComponentModel.EventDescriptor class
    //     with the name in the specified System.ComponentModel.MemberDescriptor and
    //     the attributes in both the System.ComponentModel.MemberDescriptor and the
    //     System.Attribute array.
    //
    // Parameters:
    //   descr:
    //     A System.ComponentModel.MemberDescriptor that has the name of the member
    //     and its attributes.
    //
    //   attrs:
    //     An System.Attribute array with the attributes you want to add to this event
    //     description.
    //protected EventDescriptor(MemberDescriptor descr, Attribute[] attrs);
    //
    // Summary:
    //     Initializes a new instance of the System.ComponentModel.EventDescriptor class
    //     with the specified name and attribute array.
    //
    // Parameters:
    //   name:
    //     The name of the event.
    //
    //   attrs:
    //     An array of type System.Attribute that contains the event attributes.
    //protected EventDescriptor(string name, Attribute[] attrs);

    // Summary:
    //     When overridden in a derived class, gets the type of component this event
    //     is bound to.
    //
    // Returns:
    //     A System.Type that represents the type of component the event is bound to.
    //public abstract Type ComponentType { get; }
    //
    // Summary:
    //     When overridden in a derived class, gets the type of delegate for the event.
    //
    // Returns:
    //     A System.Type that represents the type of delegate for the event.
    //public abstract Type EventType { get; }
    //
    // Summary:
    //     When overridden in a derived class, gets a value indicating whether the event
    //     delegate is a multicast delegate.
    //
    // Returns:
    //     true if the event delegate is multicast; otherwise, false.
    //public abstract bool IsMulticast { get; }

    // Summary:
    //     When overridden in a derived class, binds the event to the component.
    //
    // Parameters:
    //   component:
    //     A component that provides events to the delegate.
    //
    //   value:
    //     A delegate that represents the method that handles the event.
    //public abstract void AddEventHandler(object component, Delegate value);
    //
    // Summary:
    //     When overridden in a derived class, unbinds the delegate from the component
    //     so that the delegate will no longer receive events from the component.
    //
    // Parameters:
    //   component:
    //     The component that the delegate is bound to.
    //
    //   value:
    //     The delegate to unbind from the component.
    //public abstract void RemoveEventHandler(object component, Delegate value);
  }
}

#endif