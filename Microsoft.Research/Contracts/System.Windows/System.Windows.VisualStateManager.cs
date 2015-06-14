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

#region Assembly System.Windows.dll, v2.0.50727
// C:\Program Files\Reference Assemblies\Microsoft\Framework\Silverlight\v4.0\Profile\WindowsPhone\System.Windows.dll
#endregion

using System;
using System.Collections;
using System.Diagnostics.Contracts;
//using System.Windows.Controls;

namespace System.Windows
{
  // Summary:
  //     Manages states and the logic for transitioning between states for controls.
  public class VisualStateManager : DependencyObject
  {
    // Summary:
    //     Identifies the System.Windows.VisualStateManager.CustomVisualStateManager
    //     dependency property.
    //
    // Returns:
    //     The identifier for the System.Windows.VisualStateManager.CustomVisualStateManager
    //     dependency property.
    public static readonly DependencyProperty CustomVisualStateManagerProperty;

    // Summary:
    //     Initializes a new instance of the System.Windows.VisualStateManager class.
    public VisualStateManager();

    // Summary:
    //     Gets the value of the System.Windows.VisualStateManager.CustomVisualStateManager
    //     attached property.
    //
    // Parameters:
    //   obj:
    //     The element from which to get the System.Windows.VisualStateManager.CustomVisualStateManager.
    //
    // Returns:
    //     The System.Windows.VisualStateManager that transitions between the states
    //     of a control.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     obj is null.
    public static VisualStateManager GetCustomVisualStateManager(FrameworkElement obj);
    //
    // Summary:
    //     Gets the value of the VisualStateManager.VisualStateGroups attached property.
    //
    // Parameters:
    //   obj:
    //     The element from which to get the VisualStateManager.VisualStateGroups.
    //
    // Returns:
    //     The collection of System.Windows.VisualStateGroup objects that is associated
    //     with the specified object.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     obj is null.
    public static IList GetVisualStateGroups(FrameworkElement obj);
    //
    // Summary:
    //     Transitions the control between two states.
    //
    // Parameters:
    //   control:
    //     The control to transition between states.
    //
    //   stateName:
    //     The state to transition to.
    //
    //   useTransitions:
    //     true to use a System.Windows.VisualTransition to transition between states;
    //     otherwise, false.
    //
    // Returns:
    //     true if the control successfully transitioned to the new state; otherwise,
    //     false.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     control is null.
    //
    //   System.ArgumentNullException:
    //     stateName is null.
    public static bool GoToState(Control control, string stateName, bool useTransitions);
    //
    // Summary:
    //     Transitions a control between states.
    //
    // Parameters:
    //   control:
    //     The control to transition between states.
    //
    //   templateRoot:
    //     The root element of the control's System.Windows.Controls.ControlTemplate.
    //
    //   stateName:
    //     The name of the state to transition to.
    //
    //   group:
    //     The System.Windows.VisualStateGroup that the state belongs to.
    //
    //   state:
    //     The representation of the state to transition to.
    //
    //   useTransitions:
    //     true to use a System.Windows.VisualTransition to transition between states;
    //     otherwise, false.
    //
    // Returns:
    //     true if the control successfully transitioned to the new state; otherwise,
    //     false.
    protected virtual bool GoToStateCore(Control control, FrameworkElement templateRoot, string stateName, VisualStateGroup group, VisualState state, bool useTransitions);
    //
    // Summary:
    //     Raises the System.Windows.VisualStateGroup.CurrentStateChanged event on the
    //     specified System.Windows.VisualStateGroup.
    //
    // Parameters:
    //   stateGroup:
    //     The object on which the System.Windows.VisualStateGroup.CurrentStateChanging
    //     event
    //
    //   oldState:
    //     The state that the control transitioned from.
    //
    //   newState:
    //     The state that the control transitioned to.
    //
    //   control:
    //     The control that transitioned states.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     stateGroup is null.-or-newState is null.-or-control is null.
    protected void RaiseCurrentStateChanged(VisualStateGroup stateGroup, VisualState oldState, VisualState newState, Control control);
    //
    // Summary:
    //     Raises the System.Windows.VisualStateGroup.CurrentStateChanging event on
    //     the specified System.Windows.VisualStateGroup.
    //
    // Parameters:
    //   stateGroup:
    //     The object on which the System.Windows.VisualStateGroup.CurrentStateChanging
    //     event
    //
    //   oldState:
    //     The state that the control is transitioning from.
    //
    //   newState:
    //     The state that the control is transitioning to.
    //
    //   control:
    //     The control that is transitioning states.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     stateGroup is null.-or-newState is null.-or-control is null.
    protected void RaiseCurrentStateChanging(VisualStateGroup stateGroup, VisualState oldState, VisualState newState, Control control);
    //
    // Summary:
    //     Sets the value of the System.Windows.VisualStateManager.CustomVisualStateManager
    //     attached property.
    //
    // Parameters:
    //   obj:
    //     The object on which to set the property.
    //
    //   value:
    //     The System.Windows.VisualStateManager that transitions between the states
    //     of a control.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     obj is null.
    public static void SetCustomVisualStateManager(FrameworkElement obj, VisualStateManager value);
  }
}
