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
using System.Runtime;
using System.Diagnostics.Contracts;
using System.Collections.Specialized;

namespace System.Diagnostics
{
  // Summary:
  //     Provides a set of methods and properties that enable applications to trace
  //     the execution of code and associate trace messages with their source.
  public class TraceSource
  {
    public TraceSource(string name)
    {
      Contract.Requires(!string.IsNullOrEmpty(name));
    }

    public TraceSource(string name, SourceLevels defaultLevel)
    {
      Contract.Requires(!string.IsNullOrEmpty(name));

    }


    public StringDictionary Attributes
    {
      get
      {
        Contract.Ensures(Contract.Result<StringDictionary>() != null);

        return null;
      }
    }
    
    public TraceListenerCollection Listeners
    {
      get
      {
        Contract.Ensures(Contract.Result<TraceListenerCollection>() != null);

        return null;
      }
    }


    public string Name { get { Contract.Ensures(Contract.Result<string>() != null); return null; } }
    
    public SourceSwitch Switch
    {
      get
      {
        Contract.Ensures(Contract.Result<SourceSwitch>() != null);
        return null;
      }
      set
      {
        Contract.Requires(value != null);
      }
    }

    protected internal virtual string[] GetSupportedAttributes()
    {
      Contract.Ensures(Contract.Result<string[]>() != null);

      return null;
    }

    // public void TraceData(TraceEventType eventType, int id, object data);
    
    //public void TraceData(TraceEventType eventType, int id, params object[] data);
    
    // public void TraceEvent(TraceEventType eventType, int id);

    // public void TraceEvent(TraceEventType eventType, int id, string message);
//    public void TraceEvent(TraceEventType eventType, int id, string format, params object[] args);
  
    // public void TraceInformation(string message);
    //
    public void TraceInformation(string format, params object[] args)
    {
      Contract.Requires(format != null);
    }
    public void TraceTransfer(int id, string message, Guid relatedActivityId) { }
  }

  // Summary:
  //     Specifies the levels of trace messages filtered by the source switch and
  //     event type filter.
  [Flags]
  public enum SourceLevels
  {
    // Summary:
    //     Allows all events through.
    All = -1,
    //
    // Summary:
    //     Does not allow any events through.
    Off = 0,
    //
    // Summary:
    //     Allows only System.Diagnostics.TraceEventType.Critical events through.
    Critical = 1,
    //
    // Summary:
    //     Allows System.Diagnostics.TraceEventType.Critical and System.Diagnostics.TraceEventType.Error
    //     events through.
    Error = 3,
    //
    // Summary:
    //     Allows System.Diagnostics.TraceEventType.Critical, System.Diagnostics.TraceEventType.Error,
    //     and System.Diagnostics.TraceEventType.Warning events through.
    Warning = 7,
    //
    // Summary:
    //     Allows System.Diagnostics.TraceEventType.Critical, System.Diagnostics.TraceEventType.Error,
    //     System.Diagnostics.TraceEventType.Warning, and System.Diagnostics.TraceEventType.Information
    //     events through.
    Information = 15,
    //
    // Summary:
    //     Allows System.Diagnostics.TraceEventType.Critical, System.Diagnostics.TraceEventType.Error,
    //     System.Diagnostics.TraceEventType.Warning, System.Diagnostics.TraceEventType.Information,
    //     and System.Diagnostics.TraceEventType.Verbose events through.
    Verbose = 31,
    //
    // Summary:
    //     Allows the System.Diagnostics.TraceEventType.Stop, System.Diagnostics.TraceEventType.Start,
    //     System.Diagnostics.TraceEventType.Suspend, System.Diagnostics.TraceEventType.Transfer,
    //     and System.Diagnostics.TraceEventType.Resume events through.
    ActivityTracing = 65280,
  }
}
#endif