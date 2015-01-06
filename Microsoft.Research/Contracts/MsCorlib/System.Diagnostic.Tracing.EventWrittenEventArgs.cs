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
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;

namespace System.Diagnostics.Tracing
{

#if !SILVERLIGHT_4_0_WP && !SILVERLIGHT && !SILVERLIGHT_3_0 && !SILVERLIGHT_5_0 &&  !NETFRAMEWORK_3_5 && !NETFRAMEWORK_4_0
  // Summary:
  //     Provides data for the System.Diagnostics.Tracing.EventListener.OnEventWritten(System.Diagnostics.Tracing.EventWrittenEventArgs)
  //     callback.
  public class EventWrittenEventArgs : EventArgs
  {
    // Summary:
    //     Gets the event identifier.
    //
    // Returns:
    //     The event identifier.
    public int EventId { get; internal set; }
    //
    // Summary:
    //     Gets the event source object.
    //
    // Returns:
    //     The event source object.
    public EventSource EventSource
    {
      get
      {
        Contract.Ensures(Contract.Result<EventSource>() != null);

        return null;
      }
    }

    //public EventKeywords Keywords { get; }
    // public EventLevel Level { get; }
    
    //
    // Summary:
    //     Gets the message for the event.
    //
    // Returns:
    //     The message for the event.
    public string Message { get { Contract.Ensures(Contract.Result<string>() != null); return null; } }
//    public EventOpcode Opcode { get; }
  
    //
    // Summary:
    //     Gets the payload for the event.
    //
    // Returns:
    //     The payload for the event.
    public ReadOnlyCollection<object> Payload
    {
      get
      {
        Contract.Ensures(Contract.Result<ReadOnlyCollection<object>>() != null);

        return null;
      }
      internal set { }
    }
    
    //public EventTask Task { get; }

    //
    // Summary:
    //     Gets the version of the event.
    //
    // Returns:
    //     The version of the event.
    public byte Version
    {
      get
      {
        return 0;
      }
    }
  }

#endif
}
