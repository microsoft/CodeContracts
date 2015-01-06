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

namespace System.Xml.Linq
{
  // Summary:
  //     Specifies the event type when an event is raised for an System.Xml.Linq.XObject.
  public enum XObjectChange
  {
    // Summary:
    //     An System.Xml.Linq.XObject has been or will be added to an System.Xml.Linq.XContainer.
    Add = 0,
    //
    // Summary:
    //     An System.Xml.Linq.XObject has been or will be removed from an System.Xml.Linq.XContainer.
    Remove = 1,
    //
    // Summary:
    //     An System.Xml.Linq.XObject has been or will be renamed.
    Name = 2,
    //
    // Summary:
    //     The value of an System.Xml.Linq.XObject has been or will be changed. In addition,
    //     a change in the serialization of an empty element (either from an empty tag
    //     to start/end tag pair or vice versa) raises this event.
    Value = 3,
  }

  // Summary:
  //     Provides data for the System.Xml.Linq.XObject.Changing and System.Xml.Linq.XObject.Changed
  //     events.
  public class XObjectChangeEventArgs : EventArgs
  {
    // Summary:
    //     Event argument for an System.Xml.Linq.XObjectChange.Add change event.
    public static readonly XObjectChangeEventArgs Add;
    //
    // Summary:
    //     Event argument for a System.Xml.Linq.XObjectChange.Name change event.
    public static readonly XObjectChangeEventArgs Name;
    //
    // Summary:
    //     Event argument for a System.Xml.Linq.XObjectChange.Remove change event.
    public static readonly XObjectChangeEventArgs Remove;
    //
    // Summary:
    //     Event argument for a System.Xml.Linq.XObjectChange.Value change event.
    public static readonly XObjectChangeEventArgs Value;

    // Summary:
    //     Initializes a new instance of the System.Xml.Linq.XObjectChangeEventArgs
    //     class.
    //
    // Parameters:
    //   objectChange:
    //     An System.Xml.Linq.XObjectChange that contains the event arguments for LINQ
    //     to XML events.
    extern public XObjectChangeEventArgs(XObjectChange objectChange);

    // Summary:
    //     Gets the type of change.
    //
    // Returns:
    //     An System.Xml.Linq.XObjectChange that contains the type of change.
    extern public XObjectChange ObjectChange { get; }
  }
}
