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

namespace System.Windows.Media.Animation
{
  // Summary:
  //     Defines a segment of time.
  public abstract class Timeline : DependencyObject
  {
    // Summary:
    //     Identifies the System.Windows.Media.Animation.Timeline.AutoReverse dependency
    //     property.
    //
    // Returns:
    //     The identifier for the System.Windows.Media.Animation.Timeline.AutoReverse dependency
    //     property.
    public static readonly DependencyProperty AutoReverseProperty;
    //
    // Summary:
    //     Identifies the System.Windows.Media.Animation.Timeline.BeginTime dependency
    //     property.
    //
    // Returns:
    //     The identifier for the System.Windows.Media.Animation.Timeline.BeginTime dependency
    //     property.
    public static readonly DependencyProperty BeginTimeProperty;
    //
    // Summary:
    //     Identifies the System.Windows.Media.Animation.Timeline.Duration dependency
    //     property.
    //
    // Returns:
    //     The identifier for the System.Windows.Media.Animation.Timeline.Duration dependency
    //     property.
    public static readonly DependencyProperty DurationProperty;
    //
    // Summary:
    //     Identifies the System.Windows.Media.Animation.Timeline.FillBehavior dependency
    //     property.
    //
    // Returns:
    //     The identifier for the System.Windows.Media.Animation.Timeline.FillBehavior dependency
    //     property.
    public static readonly DependencyProperty FillBehaviorProperty;
    //
    // Summary:
    //     Identifies the System.Windows.Media.Animation.Timeline.RepeatBehavior dependency
    //     property.
    //
    // Returns:
    //     The identifier for the System.Windows.Media.Animation.Timeline.RepeatBehavior dependency
    //     property.
    public static readonly DependencyProperty RepeatBehaviorProperty;
    //
    // Summary:
    //     Identifies for the System.Windows.Media.Animation.Timeline.SpeedRatio dependency
    //     property.
    //
    // Returns:
    //     The identifier for the System.Windows.Media.Animation.Timeline.SpeedRatio dependency
    //     property.
    public static readonly DependencyProperty SpeedRatioProperty;

    // Summary:
    //     Initializes a new instance of the System.Windows.Media.Animation.Timeline
    //     class.
    extern protected Timeline();

    // Summary:
    //     Gets or sets a value that indicates whether the timeline plays in reverse
    //     after it completes a forward iteration.
    //
    // Returns:
    //     true if the timeline plays in reverse at the end of each iteration; otherwise,
    //     false. The default value is false.
    //public bool AutoReverse { get; set; }
    //
    // Summary:
    //     Gets or sets the time at which this System.Windows.Media.Animation.Timeline
    //     should begin.
    //
    // Returns:
    //     The start time of the time line. The default value is zero.
    public TimeSpan? BeginTime { get; set; }
    //
    // Summary:
    //     Gets or sets the length of time for which this timeline plays, not counting
    //     repetitions.
    //
    // Returns:
    //     The timeline's simple duration: the amount of time this timeline takes to
    //     complete a single forward iteration. The default value is System.Windows.Duration.Automatic.
    public Duration Duration { get; set; }
    //
    // Summary:
    //     Gets or sets a value that specifies how the animation behaves after it reaches
    //     the end of its active period.
    //
    // Returns:
    //     A value that specifies how the timeline behaves after it reaches the end
    //     of its active period but its parent is inside its active or fill period.
    //     The default value is System.Windows.Media.Animation.FillBehavior.HoldEnd.
    public FillBehavior FillBehavior { get; set; }
    //
    // Summary:
    //     Gets or sets the repeating behavior of this timeline.
    //
    // Returns:
    //     An iteration System.Windows.Media.Animation.RepeatBehavior.Count that specifies
    //     the number of times the timeline should play, a System.TimeSpan value that
    //     specifies the total length of this timeline's active period, or the special
    //     value System.Windows.Media.Animation.RepeatBehavior.Forever, which specifies
    //     that the timeline should repeat indefinitely. The default value is a System.Windows.Media.Animation.RepeatBehavior
    //     with a System.Windows.Media.Animation.RepeatBehavior.Count of 1, which indicates
    //     that the timeline plays once.
    public RepeatBehavior RepeatBehavior { get; set; }
    //
    // Summary:
    //     Gets or sets the rate, relative to its parent, at which time progresses for
    //     this System.Windows.Media.Animation.Timeline.
    //
    // Returns:
    //     A finite value greater than 0 that specifies the rate at which time progresses
    //     for this timeline, relative to the speed of the timeline's parent. If this
    //     timeline is a root timeline, specifies the default timeline speed. The value
    //     is expressed as a factor where 1 represents normal speed, 2 is double speed,
    //     0.5 is half speed, and so on. The default value is 1.
    public double SpeedRatio { get; set; }

    // Summary:
    //     Occurs when the System.Windows.Media.Animation.Storyboard object has completed
    //     playing.
    //public event EventHandler Completed;
  }
}
