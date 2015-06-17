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
//using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Reflection {
  // Summary:
  //     Discovers the attributes of an event and provides access to event metadata.
  public abstract class EventInfo // : MemberInfo 
  {
    // Summary:
    //     Initializes a new instance of the EventInfo class.
#if SILVERLIGHT && !SILVERLIGHT_5_0
    extern internal EventInfo();
#else
    extern protected EventInfo();
#endif

    // Summary:
    //     Gets the attributes for this event.
    //
    // Returns:
    //     The read-only attributes for this event.
    //public abstract EventAttributes Attributes { get; }
    //
    // Summary:
    //     Gets the Type object of the underlying event-handler delegate associated
    //     with this event.
    //
    // Returns:
    //     A read-only Type object representing the delegate event handler.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The caller does not have the required permission.
    public virtual Type EventHandlerType
    {
      get
      {
        Contract.Ensures(Contract.Result<Type>() != null);

        return default(Type);
      }
    }
    //
    // Summary:
    //     Gets a value indicating whether the event is multicast.
    //
    // Returns:
    //     true if the delegate is an instance of a multicast delegate; otherwise, false.
    //
    // Exceptions:
    //   System.Security.SecurityException:
    //     The caller does not have the required permission.
    extern public virtual bool IsMulticast { get; }
    //
    // Summary:
    //     Gets a value indicating whether the EventInfo has a name with a special meaning.
    //
    // Returns:
    //     true if this event has a special name; otherwise, false.
    extern public virtual bool IsSpecialName { get; }

    // Summary:
    //     Adds an event handler to an event source.
    //
    // Parameters:
    //   target:
    //     The event source.
    //
    //   handler:
    //     Encapsulates a method or methods to be invoked when the event is raised by
    //     the target.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The event does not have a public add accessor.
    //
    //   System.ArgumentException:
    //     The handler that was passed in cannot be used.
    //
    //   System.MethodAccessException:
    //     The caller does not have access permission to the member.
    //
    //   System.Reflection.TargetException:
    //     The target parameter is null and the event is not static.-or- The System.Reflection.EventInfo
    //     is not declared on the target.
    extern public virtual void AddEventHandler(object target, Delegate handler);
    //
    // Summary:
    //     Returns the method used to add an event handler delegate to the event source.
    //
    // Returns:
    //     A System.Reflection.MethodInfo object representing the method used to add
    //     an event handler delegate to the event source.
    public virtual MethodInfo GetAddMethod()
    {
      Contract.Ensures(Contract.Result<MethodInfo>() != null);

      return default(MethodInfo);
    }
    //
    // Summary:
    //     When overridden in a derived class, retrieves the MethodInfo object for the
    //     System.Reflection.EventInfo.AddEventHandler(System.Object,System.Delegate)
    //     method of the event, specifying whether to return non-public methods.
    //
    // Parameters:
    //   nonPublic:
    //     true if non-public methods can be returned; otherwise, false.
    //
    // Returns:
    //     A System.Reflection.MethodInfo object representing the method used to add
    //     an event handler delegate to the event source.
    //
    // Exceptions:
    //   System.MethodAccessException:
    //     nonPublic is true, the method used to add an event handler delegate is non-public,
    //     and the caller does not have permission to reflect on non-public methods.
    public abstract MethodInfo GetAddMethod(bool nonPublic);

#if !SILVERLIGHT
    //
    // Summary:
    //     Returns the public methods that have been associated with an event in metadata
    //     using the .other directive.
    //
    // Returns:
    //     An array of System.Reflection.EventInfo objects representing the public methods
    //     that have been associated with the event in metadata by using the .other
    //     directive. If there are no such public methods, an empty array is returned.
    public MethodInfo[] GetOtherMethods()
    {
      Contract.Ensures(Contract.Result<MethodInfo[]>() != null);

      return default(MethodInfo[]);
    }

    //
    // Summary:
    //     Returns the methods that have been associated with the event in metadata
    //     using the .other directive, specifying whether to include non-public methods.
    //
    // Parameters:
    //   nonPublic:
    //     true to include non-public methods; otherwise, false.
    //
    // Returns:
    //     An array of System.Reflection.EventInfo objects representing methods that
    //     have been associated with an event in metadata by using the .other directive.
    //     If there are no methods matching the specification, an empty array is returned.
    //
    // Exceptions:
    //   System.NotImplementedException:
    //     This method is not implemented.
    public virtual MethodInfo[] GetOtherMethods(bool nonPublic)
    {
      Contract.Ensures(Contract.Result<MethodInfo[]>() != null);

      return default(MethodInfo[]);
    }
#endif
    //
    // Summary:
    //     Returns the method that is called when the event is raised.
    //
    // Returns:
    //     The method that is called when the event is raised.
    extern public virtual MethodInfo GetRaiseMethod();
    //
    // Summary:
    //     When overridden in a derived class, returns the method that is called when
    //     the event is raised, specifying whether to return non-public methods.
    //
    // Parameters:
    //   nonPublic:
    //     true if non-public methods can be returned; otherwise, false.
    //
    // Returns:
    //     A MethodInfo object that was called when the event was raised.
    //
    // Exceptions:
    //   System.MethodAccessException:
    //     nonPublic is true, the method used to add an event handler delegate is non-public,
    //     and the caller does not have permission to reflect on non-public methods.
    public abstract MethodInfo GetRaiseMethod(bool nonPublic);

    //
    // Summary:
    //     Returns the method used to remove an event handler delegate from the event
    //     source.
    //
    // Returns:
    //     A System.Reflection.MethodInfo object representing the method used to remove
    //     an event handler delegate from the event source.
    public virtual MethodInfo GetRemoveMethod()
    {
      Contract.Ensures(Contract.Result<MethodInfo>() != null);

      return default(MethodInfo);
    }

    //
    // Summary:
    //     When overridden in a derived class, retrieves the MethodInfo object for removing
    //     a method of the event, specifying whether to return non-public methods.
    //
    // Parameters:
    //   nonPublic:
    //     true if non-public methods can be returned; otherwise, false.
    //
    // Returns:
    //     A System.Reflection.MethodInfo object representing the method used to remove
    //     an event handler delegate from the event source.
    //
    // Exceptions:
    //   System.MethodAccessException:
    //     nonPublic is true, the method used to add an event handler delegate is non-public,
    //     and the caller does not have permission to reflect on non-public methods.
    public abstract MethodInfo GetRemoveMethod(bool nonPublic);
    //
    // Summary:
    //     Removes an event handler from an event source.
    //
    // Parameters:
    //   target:
    //     The event source.
    //
    //   handler:
    //     The delegate to be disassociated from the events raised by target.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The event does not have a public remove accessor.
    //
    //   System.ArgumentException:
    //     The handler that was passed in cannot be used.
    //
    //   System.Reflection.TargetException:
    //     The target parameter is null and the event is not static.-or- The System.Reflection.EventInfo
    //     is not declared on the target.
    //
    //   System.MethodAccessException:
    //     The caller does not have access permission to the member.
    extern public virtual void RemoveEventHandler(object target, Delegate handler);
  }
}
