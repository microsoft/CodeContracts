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
using System.Runtime;
using System.Diagnostics.Contracts;

namespace System.Windows
{
  // Summary:
  //     Defines an object that has a modifiable state and a read-only (frozen) state.
  //     Classes that derive from System.Windows.Freezable provide detailed change
  //     notification, can be made immutable, and can clone themselves.
  [ContractClass(typeof(FreezableContracts))]
  public abstract class Freezable //: DependencyObject, ISealable
  {
    protected Freezable() { }

    extern public bool CanFreeze { get; }

    extern public bool IsFrozen { get; }

    // public event EventHandler Changed;

    [Pure]
    public Freezable Clone()
    {
      Contract.Ensures(Contract.Result<Freezable>() != null);

      return null;
    }

    protected virtual void CloneCore(Freezable sourceFreezable)
    {
    }

    //
    // Summary:
    //     Creates a modifiable clone (deep copy) of the System.Windows.Freezable using
    //     its current values.
    //
    // Returns:
    //     A modifiable clone of the current object. The cloned object's System.Windows.Freezable.IsFrozen
    //     property is false even if the source's System.Windows.Freezable.IsFrozen
    //     property is true.
    [Pure]
    public Freezable CloneCurrentValue()
    {
      Contract.Ensures(Contract.Result<Freezable>() != null);
      Contract.Ensures(!Contract.Result<Freezable>().IsFrozen);

      return null;
    }
    //
    // Summary:
    //     Makes the instance a modifiable clone (deep copy) of the specified System.Windows.Freezable
    //     using current property values.
    //
    // Parameters:
    //   sourceFreezable:
    //     The System.Windows.Freezable to be cloned.
    protected virtual void CloneCurrentValueCore(Freezable sourceFreezable) { }

    //
    // Summary:
    //     Initializes a new instance of the System.Windows.Freezable class.
    //
    // Returns:
    //     The new instance.
    [Pure]
    protected Freezable CreateInstance()
    {
      Contract.Ensures(Contract.Result<Freezable>() != null);

      return null;
    }

    //
    // Summary:
    //     When implemented in a derived class, creates a new instance of the System.Windows.Freezable
    //     derived class.
    //
    // Returns:
    //     The new instance.
    [Pure]
    protected abstract Freezable CreateInstanceCore();

    //
    // Summary:
    //     Makes the current object unmodifiable and sets its System.Windows.Freezable.IsFrozen
    //     property to true.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The System.Windows.Freezable cannot be made unmodifiable.
    public void Freeze()
    {
      Contract.Requires(this.CanFreeze);
      Contract.Ensures(this.IsFrozen);
    }
    //
    // Summary:
    //     If the isChecking parameter is true, this method indicates whether the specified
    //     System.Windows.Freezable can be made unmodifiable. If the isChecking parameter
    //     is false, this method attempts to make the specified System.Windows.Freezable
    //     unmodifiable and indicates whether the operation succeeded.
    //
    // Parameters:
    //   freezable:
    //     The object to check or make unmodifiable. If isChecking is true, the object
    //     is checked to determine whether it can be made unmodifiable. If isChecking
    //     is false, the object is made unmodifiable, if possible.
    //
    //   isChecking:
    //     true to return an indication of whether the object can be frozen (without
    //     actually freezing it); false to actually freeze the object.
    //
    // Returns:
    //     If isChecking is true, this method returns true if the specified System.Windows.Freezable
    //     can be made unmodifiable, or false if it cannot be made unmodifiable. If
    //     isChecking is false, this method returns true if the specified System.Windows.Freezable
    //     is now unmodifiable, or false if it cannot be made unmodifiable.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     When isChecking is false, the attempt to make freezable unmodifiable was
    //     unsuccessful; the object is now in an unknown state (it might be partially
    //     frozen).
    //protected internal static bool Freeze(Freezable freezable, bool isChecking);

    //
    // Summary:
    //     Makes the System.Windows.Freezable object unmodifiable or tests whether it
    //     can be made unmodifiable.
    //
    // Parameters:
    //   isChecking:
    //     true to return an indication of whether the object can be frozen (without
    //     actually freezing it); false to actually freeze the object.
    //
    // Returns:
    //     If isChecking is true, this method returns true if the System.Windows.Freezable
    //     can be made unmodifiable, or false if it cannot be made unmodifiable. If
    //     isChecking is false, this method returns true if the if the specified System.Windows.Freezable
    //     is now unmodifiable, or false if it cannot be made unmodifiable.
    protected virtual bool FreezeCore(bool isChecking)
    {
      return false;
    }
    //
    // Summary:
    //     Creates a frozen copy of the System.Windows.Freezable, using base (non-animated)
    //     property values. Because the copy is frozen, any frozen sub-objects are copied
    //     by reference.
    //
    // Returns:
    //     A frozen copy of the System.Windows.Freezable. The copy's System.Windows.Freezable.IsFrozen
    //     property is set to true.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The System.Windows.Freezable cannot be frozen because it contains expressions
    //     or animated properties.
    public Freezable GetAsFrozen()
    {
      Contract.Ensures(Contract.Result<Freezable>() != null);
      Contract.Ensures(Contract.Result<Freezable>().IsFrozen);

      return null;
    }

    //
    // Summary:
    //     Makes the instance a frozen clone of the specified System.Windows.Freezable
    //     using base (non-animated) property values.
    //
    // Parameters:
    //   sourceFreezable:
    //     The instance to copy.
    protected virtual void GetAsFrozenCore(Freezable sourceFreezable) { }
    //
    // Summary:
    //     Creates a frozen copy of the System.Windows.Freezable using current property
    //     values. Because the copy is frozen, any frozen sub-objects are copied by
    //     reference.
    //
    // Returns:
    //     A frozen copy of the System.Windows.Freezable. The copy's System.Windows.Freezable.IsFrozen
    //     property is set to true.
    public Freezable GetCurrentValueAsFrozen()
    {
      Contract.Ensures(Contract.Result<Freezable>() != null);
      Contract.Ensures(Contract.Result<Freezable>().IsFrozen);

      return null;
    }
    //
    // Summary:
    //     Makes the current instance a frozen clone of the specified System.Windows.Freezable.
    //     If the object has animated dependency properties, their current animated
    //     values are copied.
    //
    // Parameters:
    //   sourceFreezable:
    //     The System.Windows.Freezable to copy and freeze.
    protected virtual void GetCurrentValueAsFrozenCore(Freezable sourceFreezable)
    {
      Contract.Requires(sourceFreezable != null);
    }
    //
    // Summary:
    //     Called when the current System.Windows.Freezable object is modified.
    protected virtual void OnChanged() { }
    //
    // Summary:
    //     Ensures that appropriate context pointers are established for a System.Windows.DependencyObjectType
    //     data member that has just been set.
    //
    // Parameters:
    //   oldValue:
    //     The previous value of the data member.
    //
    //   newValue:
    //     The current value of the data member.
    // protected void OnFreezablePropertyChanged(DependencyObject oldValue, DependencyObject newValue);
    //
    // Summary:
    //     This member supports the Windows Presentation Foundation (WPF) infrastructure
    //     and is not intended to be used directly from your code.
    //
    // Parameters:
    //   oldValue:
    //     The previous value of the data member.
    //
    //   newValue:
    //     The current value of the data member.
    //
    //   property:
    //     The property that changed.
    //protected void OnFreezablePropertyChanged(DependencyObject oldValue, DependencyObject newValue, DependencyProperty property);
    //
    // Summary:
    //     Overrides the System.Windows.DependencyObject implementation of System.Windows.DependencyObject.OnPropertyChanged(System.Windows.DependencyPropertyChangedEventArgs)
    //     to also invoke any System.Windows.Freezable.Changed handlers in response
    //     to a changing dependency property of type System.Windows.Freezable.
    //
    // Parameters:
    //   e:
    //     Event data that contains information about which property changed, and its
    //     old and new values.
    // protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e);
    //
    // Summary:
    //     Ensures that the System.Windows.Freezable is being accessed from a valid
    //     thread. Inheritors of System.Windows.Freezable must call this method at the
    //     beginning of any API that reads data members that are not dependency properties.
    protected void ReadPreamble() { }
    //
    // Summary:
    //     Raises the System.Windows.Freezable.Changed event for the System.Windows.Freezable
    //     and invokes its System.Windows.Freezable.OnChanged() method. Classes that
    //     derive from System.Windows.Freezable should call this method at the end of
    //     any API that modifies class members that are not stored as dependency properties.

    protected void WritePostscript() { }
    //
    // Summary:
    //     Verifies that the System.Windows.Freezable is not frozen and that it is being
    //     accessed from a valid threading context. System.Windows.Freezable inheritors
    //     should call this method at the beginning of any API that writes to data members
    //     that are not dependency properties.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     The System.Windows.Freezable instance is frozen and cannot have its members
    //     written to.
    protected void WritePreamble()
    {
      Contract.Requires(!this.IsFrozen);
    }
  }

  [ContractClassFor(typeof(Freezable))]
  abstract class FreezableContracts : Freezable
  {
    protected override Freezable CreateInstanceCore()
    {
      Contract.Ensures(Contract.Result<Freezable>() != null);

      return null;
    }
  }
}
