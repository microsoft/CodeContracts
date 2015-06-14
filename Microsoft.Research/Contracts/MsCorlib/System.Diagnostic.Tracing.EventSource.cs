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

#region Assembly mscorlib.dll, v4.0.0.0
// C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5\mscorlib.dll
#endregion

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime;
using System.Security;

namespace System.Diagnostics.Tracing
{
#if  !SILVERLIGHT_4_0_WP && !SILVERLIGHT && !SILVERLIGHT_3_0 && !SILVERLIGHT_5_0  && !NETFRAMEWORK_3_5 && !NETFRAMEWORK_4_0

  // Summary:
  //     Provides the ability to create events for event tracing for Windows (ETW).
  public class EventSource // : IDisposable
  {
    // Summary:
    //     Creates a new instance of the System.Diagnostics.Tracing.EventSource class.
    protected EventSource() { }
    //
    // Summary:
    //     Creates a new instance of the System.Diagnostics.Tracing.EventSource class
    //     and specifies whether to throw an exception when an error occurs in the underlying
    //     Windows code.
    //
    // Parameters:
    //   throwOnEventWriteErrors:
    //     true to throw an exception when an error occurs in the underlying Windows
    //     code; otherwise, false.
    protected EventSource(bool throwOnEventWriteErrors) { }

    // Summary:
    //     The unique identifier for the event source.
    //
    // Returns:
    //     A unique identifier for the event source.
    extern public Guid Guid { get; }
    //
    // Summary:
    //     The friendly name of the class that is derived from the event source.
    //
    // Returns:
    //     The friendly name of the derived class. The default is the simple name of
    //     the class.
    extern public string Name { get; }

    // Summary:
    //     Returns a string of the XML manifest that is associated with the current
    //     event source.
    //
    // Parameters:
    //   eventSourceType:
    //     The type of the event source.
    //
    //   assemblyPathToIncludeInManifest:
    //     The path to the .dll file to include in the manifest.
    //
    // Returns:
    //     The XML data string.
    public static string GenerateManifest(Type eventSourceType, string assemblyPathToIncludeInManifest) { return null; }
    //
    // Summary:
    //     Gets the unique identifier for this implementation of the event source.
    //
    // Parameters:
    //   eventSourceType:
    //     The type of the event source.
    //
    // Returns:
    //     A unique identifier for this event source type.
    public static Guid GetGuid(Type eventSourceType) { return default(Guid);  }
    //
    // Summary:
    //     Gets the friendly name of the event source.
    //
    // Parameters:
    //   eventSourceType:
    //     The type of the event source.
    //
    // Returns:
    //     The friendly name of the event source. The default is the simple name of
    //     the class.
    public static string GetName(Type eventSourceType) { { return null; } }
    //
    // Summary:
    //     Gets a snapshot of all the event sources for the application domain.
    //
    // Returns:
    //     An enumeration of all the event sources in the application domain.
    public static IEnumerable<EventSource> GetSources() { return null; }
    //
    // Summary:
    //     Determines whether the current event source is enabled.
    //
    // Returns:
    //     true if the current event source is enabled; otherwise, false.
    public bool IsEnabled() { return false; }
    //
    // Summary:
    //     Determines whether the current event source that has the specified level
    //     and keyword is enabled.
    //
    // Parameters:
    //   level:
    //     The level of the event source.
    //
    //   keywords:
    //     The keyword of the event source.
    //
    // Returns:
    //     true if the event source is enabled; otherwise, false.
//    public bool IsEnabled(EventLevel level, EventKeywords keywords);
    //
    // Summary:
    //     Called when the current event source is updated by the controller.
    //
    // Parameters:
    //   command:
    //     The arguments for the event.
  //  protected virtual void OnEventCommand(EventCommandEventArgs command);
    //
    // Summary:
    //     Sends a command to a specified event source.
    //
    // Parameters:
    //   eventSource:
    //     The event source to send the command to.
    //
    //   command:
    //     The event command to send.
    //
    //   commandArguments:
    //     The arguments for the event command.
    //public static void SendCommand(EventSource eventSource, EventCommand command, IDictionary<string, string> commandArguments);
    
    //
    // Summary:
    //     Writes an event by using the provided event identifier.
    //
    // Parameters:
    //   eventId:
    //     The event identifier.
    protected void WriteEvent(int eventId) {  }
    //
    // Summary:
    //     Writes an event by using the provided event identifier and 32-bit integer
    //     argument.
    //
    // Parameters:
    //   eventId:
    //     The event identifier.
    //
    //   arg1:
    //     An integer argument.

    protected void WriteEvent(int eventId, int arg1) { }

    //
    // Summary:
    //     Writes an event by using the provided event identifier and 64-bit integer
    //     argument.
    //
    // Parameters:
    //   eventId:
    //     The event identifier.
    //
    //   arg1:
    //     A 64 bit integer argument.

    protected void WriteEvent(int eventId, long arg1) { }

    //
    // Summary:
    //     Writes an event by using the provided event identifier and array of arguments.
    //
    // Parameters:
    //   eventId:
    //     The event identifier.
    //
    //   args:
    //     An array of objects.

    protected void WriteEvent(int eventId, params object[] args) { }

    //
    // Summary:
    //     Writes an event by using the provided event identifier and string argument.
    //
    // Parameters:
    //   eventId:
    //     The event identifier.
    //
    //   arg1:
    //     A string argument.

    protected void WriteEvent(int eventId, string arg1) { }

    //
    // Summary:
    //     Writes an event by using the provided event identifier and 32-bit integer
    //     arguments.
    //
    // Parameters:
    //   eventId:
    //     The event identifier.
    //
    //   arg1:
    //     An integer argument.
    //
    //   arg2:
    //     An integer argument.

    protected void WriteEvent(int eventId, int arg1, int arg2) { }

    //
    // Summary:
    //     Writes an event by using the provided event identifier and 64-bit arguments.
    //
    // Parameters:
    //   eventId:
    //     The event identifier.
    //
    //   arg1:
    //     A 64 bit integer argument.
    //
    //   arg2:
    //     A 64 bit integer argument.

    protected void WriteEvent(int eventId, long arg1, long arg2) { }

    //
    // Summary:
    //     Writes an event by using the provided event identifier and arguments.
    //
    // Parameters:
    //   eventId:
    //     The event identifier.
    //
    //   arg1:
    //     A string argument.
    //
    //   arg2:
    //     A 32 bit integer argument.

    protected void WriteEvent(int eventId, string arg1, int arg2) { }

    //
    // Summary:
    //     Writes an event by using the provided event identifier and arguments.
    //
    // Parameters:
    //   eventId:
    //     The event identifier.
    //
    //   arg1:
    //     A string argument.
    //
    //   arg2:
    //     A 64 bit integer argument.

    protected void WriteEvent(int eventId, string arg1, long arg2) { }

    //
    // Summary:
    //     Writes an event by using the provided event identifier and string arguments.
    //
    // Parameters:
    //   eventId:
    //     The event identifier.
    //
    //   arg1:
    //     A string argument.
    //
    //   arg2:
    //     A string argument.

    protected void WriteEvent(int eventId, string arg1, string arg2) { }

    //
    // Summary:
    //     Writes an event by using the provided event identifier and 32-bit integer
    //     arguments.
    //
    // Parameters:
    //   eventId:
    //     The event identifier.
    //
    //   arg1:
    //     An integer argument.
    //
    //   arg2:
    //     An integer argument.
    //
    //   arg3:
    //     An integer argument.

    protected void WriteEvent(int eventId, int arg1, int arg2, int arg3) { }

    //
    // Summary:
    //     Writes an event by using the provided event identifier and 64-bit arguments.
    //
    // Parameters:
    //   eventId:
    //     The event identifier.
    //
    //   arg1:
    //     A 64 bit integer argument.
    //
    //   arg2:
    //     A 64 bit integer argument.
    //
    //   arg3:
    //     A 64 bit integer argument.

    protected void WriteEvent(int eventId, long arg1, long arg2, long arg3) { }

    //
    // Summary:
    //     Writes an event by using the provided event identifier and arguments.
    //
    // Parameters:
    //   eventId:
    //     The event identifier.
    //
    //   arg1:
    //     A string argument.
    //
    //   arg2:
    //     A 32 bit integer argument.
    //
    //   arg3:
    //     A 32 bit integer argument.

    protected void WriteEvent(int eventId, string arg1, int arg2, int arg3) { }

    //
    // Summary:
    //     Writes an event by using the provided event identifier and string arguments.
    //
    // Parameters:
    //   eventId:
    //     The event identifier.
    //
    //   arg1:
    //     A string argument.
    //
    //   arg2:
    //     A string argument.
    //
    //   arg3:
    //     A string argument.

    protected void WriteEvent(int eventId, string arg1, string arg2, string arg3) { }

    //
    // Summary:
    //     Creates a new Overload:System.Diagnostics.Tracing.EventSource.WriteEvent
    //     overload by using the provided event identifier and event data.
    //
    // Parameters:
    //   eventId:
    //     The event identifier.
    //
    //   eventDataCount:
    //     The number of event data items.
    //
    //   data:
    //     The structure that contains the event data.
    //protected void WriteEventCore(int eventId, int eventDataCount, EventSource.EventData* data) { }


    // Summary:
    //     Provides the event data for creating fast Overload:System.Diagnostics.Tracing.EventSource.WriteEvent
    //     overloads by using the System.Diagnostics.Tracing.EventSource.WriteEventCore(System.Int32,System.Int32,System.Diagnostics.Tracing.EventSource.EventData*)
    //     method.
    protected internal struct EventData
    {

      // Summary:
      //     Gets or sets the pointer to the data for the new Overload:System.Diagnostics.Tracing.EventSource.WriteEvent
      //     overload.
      //
      // Returns:
      //     The pointer to the data.
      public IntPtr DataPointer { get; set; }
      //
      // Summary:
      //     Gets or sets the number of payload items in the new Overload:System.Diagnostics.Tracing.EventSource.WriteEvent
      //     overload.
      //
      // Returns:
      //     The number of payload items in the new overload.
      public int Size { get; set; }
    }
  }

#endif
}
