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
using System.Windows;
using System.Diagnostics.Contracts;
//using System.Windows.Markup;

namespace System.Windows.Media.Animation
{
  // Summary:
  //     Controls animations with a timeline, and provides object and property targeting
  //     information for its child animations.
  //  [ContentProperty("Children", true)]
  public sealed class Storyboard : Timeline
  {
    // Summary:
    //     Identifies the System.Windows.Media.Animation.Storyboard.TargetName attached
    //     property.
    //
    // Returns:
    //     The identifier for the System.Windows.Media.Animation.Storyboard.TargetName
    //     attached property.
    public static readonly DependencyProperty TargetNameProperty;
    //
    // Summary:
    //     Identifies the System.Windows.Media.Animation.Storyboard.TargetProperty attached
    //     property.
    //
    // Returns:
    //     The identifier for the System.Windows.Media.Animation.Storyboard.TargetProperty
    //     attached property.
    public static readonly DependencyProperty TargetPropertyProperty;

    // Summary:
    //     Initializes a new instance of the System.Windows.Media.Animation.Storyboard
    //     class.
    extern public Storyboard();

    // Summary:
    //     Gets the collection of child System.Windows.Media.Animation.Timeline objects.
    //
    // Returns:
    //     The collection of child System.Windows.Media.Animation.Timeline objects.
    //     The default is an empty collection.
    public TimelineCollection Children
    {
      get
      {
        Contract.Ensures(Contract.Result<TimelineCollection>() != null);
        return default(TimelineCollection);
      }
    }

    // Summary:
    //     Initiates the set of animations associated with the storyboard.
    extern public void Begin();
    //
    // Summary:
    //     Gets the clock state of the storyboard.
    //
    // Returns:
    //     One of the enumeration values: System.Windows.Media.Animation.ClockState.Active,
    //     System.Windows.Media.Animation.ClockState.Filling, or System.Windows.Media.Animation.ClockState.Stopped.
    [Pure]
    extern public ClockState GetCurrentState();
    //
    // Summary:
    //     Gets the current time of the storyboard.
    //
    // Returns:
    //     The current time of the storyboard, or null if the storyboard's clock is
    //     System.Windows.Media.Animation.ClockState.Stopped.
    [Pure]
    extern public TimeSpan GetCurrentTime();
    //
    // Summary:
    //     Gets the System.Windows.Media.Animation.Storyboard.TargetName of the specified
    //     System.Windows.Media.Animation.Timeline object.
    //
    // Parameters:
    //   element:
    //     The System.Windows.Media.Animation.Timeline object to get the target name
    //     from.
    //
    // Returns:
    //     The string name of the target object.
    [Pure]
    extern public static string GetTargetName(Timeline element);
    //
    // Summary:
    //     Gets the System.Windows.Media.Animation.Storyboard.TargetProperty of the
    //     specified System.Windows.Media.Animation.Timeline object.
    //
    // Parameters:
    //   element:
    //     The System.Windows.Media.Animation.Timeline object to get the target property
    //     from.
    //
    // Returns:
    //     The property path information for the animated property.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     element is null.
    [Pure]
    extern public static PropertyPath GetTargetProperty(Timeline element);
    //
    // Summary:
    //     Pauses the animation clock associated with the storyboard.
    extern public void Pause();
    //
    // Summary:
    //     Resumes the animation clock, or run-time state, associated with the storyboard.
    extern public void Resume();
    //
    // Summary:
    //     Moves the storyboard to the specified animation position. The storyboard
    //     performs the requested seek when the next clock tick occurs.
    //
    // Parameters:
    //   offset:
    //     A positive or negative time value that describes the amount by which the
    //     timeline should move forward or backward from the beginning of the animation.
    //     By using the System.TimeSpan Parse behavior, a System.TimeSpan can be specified
    //     as a string in the following format (in this syntax, the [] characters denote
    //     optional components of the string, but the quotes, colons, and periods are
    //     all a literal part of the syntax):"[days.]hours:minutes:seconds[.fractionalSeconds]"-
    //     or -"days"
    extern public void Seek(TimeSpan offset);
    //
    // Summary:
    //     Moves the storyboard to the specified animation position immediately (synchronously).
    //
    // Parameters:
    //   offset:
    //     A positive or negative time value that describes the amount by which the
    //     timeline should move forward or backward from the beginning of the animation.
    //     By using the System.TimeSpan Parse behavior, a System.TimeSpan can be specified
    //     as a string in the following format (in this syntax, the [] characters denote
    //     optional components of the string, but the quotes, colons, and periods are
    //     all a literal part of the syntax):"[days.]hours:minutes:seconds[.fractionalSeconds]"-
    //     or -"days"
    extern public void SeekAlignedToLastTick(TimeSpan offset);
    //
    // Summary:
    //     Causes the specified System.Windows.Media.Animation.Timeline to target the
    //     specified object.
    //
    // Parameters:
    //   timeline:
    //     The timeline that targets the specified dependency object.
    //
    //   target:
    //     The actual instance of the object to target.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     One or more of the parameters is null.
    extern public static void SetTarget(Timeline timeline, DependencyObject target);
    //
    // Summary:
    //     Causes the specified System.Windows.Media.Animation.Timeline to target the
    //     object with the specified name.
    //
    // Parameters:
    //   element:
    //     The timeline that targets the specified dependency object.
    //
    //   name:
    //     The name of the object to target.
    extern public static void SetTargetName(Timeline element, string name);
    //
    // Summary:
    //     Causes the specified System.Windows.Media.Animation.Timeline to target the
    //     specified dependency property.
    //
    // Parameters:
    //   element:
    //     The timeline with which to associate the specified dependency property.
    //
    //   path:
    //     A path that describe the dependency property to be animated.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     One or more of the parameters is null.
    extern public static void SetTargetProperty(Timeline element, PropertyPath path);
    //
    // Summary:
    //     Advances the current time of the storyboard's clock to the end of its active
    //     period.
    extern public void SkipToFill();
    //
    // Summary:
    //     Stops the storyboard.
    extern public void Stop();
  }
}
