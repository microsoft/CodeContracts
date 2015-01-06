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
//using System.Windows.Automation.Peers;
//using System.Windows.Input;
using System.Windows.Media;

namespace System.Windows
{
  // Summary:
  //     System.Windows.UIElement is a base class for most of the objects that have
  //     visual appearance and can process basic input in Silverlight.
  public abstract class UIElement : DependencyObject
  {
    // Summary:
    //     Identifies the System.Windows.UIElement.CacheMode dependency property.
    //
    // Returns:
    //     The identifier for the System.Windows.UIElement.CacheMode dependency property.
    public static readonly DependencyProperty CacheModeProperty;
    //
    // Summary:
    //     Identifies the System.Windows.UIElement.Clip dependency property.
    //
    // Returns:
    //     The identifier for the System.Windows.UIElement.Clip dependency property.
    public static readonly DependencyProperty ClipProperty;
    //
    // Summary:
    //     Identifies the System.Windows.UIElement.IsHitTestVisible dependency property.
    //
    // Returns:
    //     The identifier for the System.Windows.UIElement.IsHitTestVisible dependency
    //     property.
    public static readonly DependencyProperty IsHitTestVisibleProperty;
    //
    // Summary:
    //     Identifies the System.Windows.UIElement.KeyDown routed event.
    //
    // Returns:
    //     The identifier for the System.Windows.UIElement.KeyDown routed event.
    public static readonly RoutedEvent KeyDownEvent;
    //
    // Summary:
    //     Identifies the System.Windows.UIElement.KeyUp routed event.
    //
    // Returns:
    //     The identifier for the System.Windows.UIElement.KeyUp routed event.
    public static readonly RoutedEvent KeyUpEvent;
    //
    // Summary:
    //     Identifies the System.Windows.UIElement.ManipulationCompleted routed event.
    //
    // Returns:
    //     The identifier for the System.Windows.UIElement.ManipulationCompleted routed
    //     event.
    public static readonly RoutedEvent ManipulationCompletedEvent;
    //
    // Summary:
    //     Identifies the System.Windows.UIElement.ManipulationDelta routed event.
    //
    // Returns:
    //     The identifier for the System.Windows.UIElement.ManipulationDelta routed
    //     event.
    public static readonly RoutedEvent ManipulationDeltaEvent;
    //
    // Summary:
    //     Identifies the System.Windows.UIElement.ManipulationStarted routed event.
    //
    // Returns:
    //     The identifier for the System.Windows.UIElement.ManipulationStarted routed
    //     event.
    public static readonly RoutedEvent ManipulationStartedEvent;
    //
    // Summary:
    //     Identifies the System.Windows.UIElement.MouseLeftButtonDown routed event.
    //
    // Returns:
    //     The identifier for the System.Windows.UIElement.MouseLeftButtonDown routed
    //     event.
    public static readonly RoutedEvent MouseLeftButtonDownEvent;
    //
    // Summary:
    //     Identifies the System.Windows.UIElement.MouseLeftButtonUp routed event.
    //
    // Returns:
    //     The identifier for the System.Windows.UIElement.MouseLeftButtonUp routed
    //     event.
    public static readonly RoutedEvent MouseLeftButtonUpEvent;
    //
    // Summary:
    //     Identifies the System.Windows.UIElement.IsHitTestVisible dependency property.
    //
    // Returns:
    //     The identifier for the System.Windows.UIElement.Clip dependency property.
    public static readonly DependencyProperty OpacityMaskProperty;
    //
    // Summary:
    //     Identifies the System.Windows.UIElement.IsHitTestVisible dependency property.
    //
    // Returns:
    //     The identifier for the System.Windows.UIElement.IsHitTestVisible dependency
    //     property.
    public static readonly DependencyProperty OpacityProperty;
    //
    // Summary:
    //     Identifies the System.Windows.UIElement.Projection dependency property.
    //
    // Returns:
    //     The identifier for the System.Windows.UIElement.Projection dependency property.
    public static readonly DependencyProperty ProjectionProperty;
    //
    // Summary:
    //     Identifies the System.Windows.UIElement.RenderTransformOrigin dependency
    //     property.
    //
    // Returns:
    //     The identifier for the System.Windows.UIElement.RenderTransformOrigin dependency
    //     property.
    public static readonly DependencyProperty RenderTransformOriginProperty;
    //
    // Summary:
    //     Identifies the System.Windows.UIElement.RenderTransform dependency property.
    //
    // Returns:
    //     The identifier for the System.Windows.UIElement.RenderTransform dependency
    //     property.
    public static readonly DependencyProperty RenderTransformProperty;
    //
    // Summary:
    //     Identifies the System.Windows.UIElement.UseLayoutRounding dependency property.
    //
    // Returns:
    //     The identifier for the System.Windows.UIElement.UseLayoutRounding dependency
    //     property.
    public static readonly DependencyProperty UseLayoutRoundingProperty;
    //
    // Summary:
    //     Identifies the System.Windows.UIElement.Visibility dependency property.
    //
    // Returns:
    //     The identifier for the System.Windows.UIElement.Visibility dependency property.
    public static readonly DependencyProperty VisibilityProperty;

    // Summary:
    //     Gets or sets a value that indicates that rendered content should be cached
    //     when possible.
    //
    // Returns:
    //     A value that indicates that rendered content should be cached when possible.
    //     If you specify a value of System.Windows.Media.CacheMode, rendering operations
    //     from System.Windows.UIElement.RenderTransform and System.Windows.UIElement.Opacity
    //     execute on the graphics processing unit (GPU), if available. The default
    //     is null, which does not enable a cached composition mode.
    public CacheMode CacheMode { get; set; }
    //
    // Summary:
    //     Gets or sets the System.Windows.Media.Geometry used to define the outline
    //     of the contents of a System.Windows.UIElement.
    //
    // Returns:
    //     The geometry to be used for clipping area sizing. The default value is null.
    public Geometry Clip { get; set; }
    //
    // Summary:
    //     Gets the size that this System.Windows.UIElement computed during the measure
    //     pass of the layout process.
    //
    // Returns:
    //     The size that this System.Windows.UIElement computed during the measure pass
    //     of the layout process.
    public Size DesiredSize { get; }
    //
    // Summary:
    //     Gets or sets whether the contained area of this System.Windows.UIElement
    //     can return true values for hit testing.
    //
    // Returns:
    //     true if the contained area of this System.Windows.UIElement can be used for
    //     hit-testing; otherwise, false. The default is true.
    public bool IsHitTestVisible { get; set; }
    //
    // Summary:
    //     Gets or sets the degree of the object's opacity.
    //
    // Returns:
    //     A value between 0 and 1.0 that declares the opacity factor, with 1.0 meaning
    //     full opacity and 0 meaning transparent. The default value is 1.0.
    public double Opacity { get; set; }
    //
    // Summary:
    //     Gets or sets the brush used to alter the opacity of regions of this object.
    //
    // Returns:
    //     A brush that describes the opacity applied to this object. The default is
    //     null.
    public Brush OpacityMask { get; set; }
    //
    // Summary:
    //     Gets or sets the perspective projection (3-D effect) to apply when rendering
    //     this System.Windows.UIElement.
    //
    // Returns:
    //     The perspective projection to apply when rendering this System.Windows.UIElement.
    //     The default is null (no perspective applied).
    public Projection Projection { get; set; }
    //
    // Summary:
    //     Gets the final render size of a System.Windows.UIElement.
    //
    // Returns:
    //     The rendered size for this object. There is no default value.
    public Size RenderSize { get; }
    //
    // Summary:
    //     Gets or sets transform information that affects the rendering position of
    //     a System.Windows.UIElement.
    //
    // Returns:
    //     Describes the specifics of the desired render transform. The default value
    //     is null.
    public Transform RenderTransform { get; set; }
    //
    // Summary:
    //     Gets or sets the origin point of any possible render transform declared by
    //     System.Windows.UIElement.RenderTransform, relative to the bounds of the System.Windows.UIElement.
    //
    // Returns:
    //     The origin point of the render transform. The default value is a point with
    //     value 0,0.
    public Point RenderTransformOrigin { get; set; }
    //
    // Summary:
    //     Gets or sets a value that determines whether rendering for the object and
    //     its visual subtree should use rounding behavior that aligns rendering to
    //     whole pixels.
    //
    // Returns:
    //     true if rendering and layout should use layout rounding to whole pixels;
    //     otherwise, false. The default is true.
    public bool UseLayoutRounding { get; set; }
    //
    // Summary:
    //     Gets or sets the visibility of a System.Windows.UIElement. A System.Windows.UIElement
    //     that is not visible does not render and does not communicate its desired
    //     size to layout.
    //
    // Returns:
    //     A value of the enumeration. The default value is System.Windows.Visibility.Visible.
    public Visibility Visibility { get; set; }

    // Summary:
    //     Occurs when a System.Windows.UIElement receives focus.
    public event RoutedEventHandler GotFocus;
    //
    // Summary:
    //     Occurs when a keyboard key is pressed while the System.Windows.UIElement
    //     has focus.
    public event KeyEventHandler KeyDown;
    //
    // Summary:
    //     Occurs when a keyboard key is released while the System.Windows.UIElement
    //     has focus.
    public event KeyEventHandler KeyUp;
    //
    // Summary:
    //     Occurs when a System.Windows.UIElement loses focus.
    public event RoutedEventHandler LostFocus;
    //
    // Summary:
    //     Occurs when the System.Windows.UIElement loses mouse capture.
    public event MouseEventHandler LostMouseCapture;
    //
    // Summary:
    //     Occurs when a manipulation and inertia on the System.Windows.UIElement is
    //     complete.
    public event EventHandler<ManipulationCompletedEventArgs> ManipulationCompleted;
    //
    // Summary:
    //     Occurs when the input device changes position during a manipulation.
    public event EventHandler<ManipulationDeltaEventArgs> ManipulationDelta;
    //
    // Summary:
    //     Occurs when an input device begins a manipulation on the System.Windows.UIElement.
    public event EventHandler<ManipulationStartedEventArgs> ManipulationStarted;
    //
    // Summary:
    //     Occurs when the mouse (or a stylus) enters the bounding area of a System.Windows.UIElement.
    public event MouseEventHandler MouseEnter;
    //
    // Summary:
    //     Occurs when the mouse (or the stylus) leaves the bounding area of a System.Windows.UIElement.
    public event MouseEventHandler MouseLeave;
    //
    // Summary:
    //     Occurs when the left mouse button is pressed (or when the tip of the stylus
    //     touches the tablet) while the mouse pointer is over a System.Windows.UIElement.
    public event MouseButtonEventHandler MouseLeftButtonDown;
    //
    // Summary:
    //     Occurs when the left mouse button is released (or the tip of the stylus is
    //     removed from the tablet) while the mouse (or the stylus) is over a System.Windows.UIElement
    //     (or while a System.Windows.UIElement holds mouse capture).
    public event MouseButtonEventHandler MouseLeftButtonUp;
    //
    // Summary:
    //     Occurs when the coordinate position of the mouse (or stylus) changes while
    //     over a System.Windows.UIElement (or while a System.Windows.UIElement holds
    //     mouse capture).
    public event MouseEventHandler MouseMove;
    //
    // Summary:
    //     Occurs when the user rotates the mouse wheel while the mouse pointer is over
    //     a System.Windows.UIElement, or the System.Windows.UIElement has focus.
    public event MouseWheelEventHandler MouseWheel;

    // Summary:
    //     Adds a routed event handler for a specified routed event, adding the handler
    //     to the handler collection on the current element. Specify handledEventsToo
    //     as true to have the provided handler be invoked for routed event that had
    //     already been marked as handled by another element along the event route.
    //
    // Parameters:
    //   routedEvent:
    //     An identifier for the routed event to be handled.
    //
    //   handler:
    //     A reference to the handler implementation.
    //
    //   handledEventsToo:
    //     true to register the handler such that it is invoked even when the routed
    //     event is marked handled in its event data; false to register the handler
    //     with the default condition that it will not be invoked if the routed event
    //     is already marked handled. The default is false. Do not routinely ask to
    //     rehandle a routed event. For more information, see Remarks.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     routedEvent or handler is null.
    //
    //   System.ArgumentException:
    //     routedEvent does not represent a supported routed event.-or-handler does
    //     not implement a supported delegate.
    //
    //   System.NotImplementedException:
    //     Attempted to add handler for an event not supported by the current platform
    //     variation.
    public void AddHandler(RoutedEvent routedEvent, Delegate handler, bool handledEventsToo);
    //
    // Summary:
    //     Positions child objects and determines a size for a System.Windows.UIElement.
    //     Parent objects that implement custom layout for their child elements should
    //     call this method from their layout override implementations to form a recursive
    //     layout update.
    //
    // Parameters:
    //   finalRect:
    //     The final size that the parent computes for the child in layout, provided
    //     as a System.Windows.Rect value.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     finalRect contained a System.Double.NaN or infinite value. See Remarks.
    public void Arrange(Rect finalRect);
    //
    // Summary:
    //     Sets mouse capture to a System.Windows.UIElement.
    //
    // Returns:
    //     Returns true if the object has mouse capture; otherwise, returns false.
    public bool CaptureMouse();
    //
    // Summary:
    //     Invalidates the arrange state (layout) for a System.Windows.UIElement. After
    //     the invalidation, the System.Windows.UIElement will have its layout updated,
    //     which will occur asynchronously.
    public void InvalidateArrange();
    //
    // Summary:
    //     Invalidates the measurement state (layout) for a System.Windows.UIElement.
    public void InvalidateMeasure();
    //
    // Summary:
    //     Updates the System.Windows.UIElement.DesiredSize of a System.Windows.UIElement.
    //     Typically, objects that implement custom layout for their layout children
    //     call this method from their own System.Windows.FrameworkElement.MeasureOverride(System.Windows.Size)
    //     implementations to form a recursive layout update.
    //
    // Parameters:
    //   availableSize:
    //     The available space that a parent can allocate a child object. A child object
    //     can request a larger space than what is available; the provided size might
    //     be accommodated if scrolling or other resize behavior is possible in that
    //     particular container.
    //
    // Exceptions:
    //   System.InvalidOperationException:
    //     availableSize contained a System.Double.NaN value. See Remarks.
    public void Measure(Size availableSize);
    //
    // Summary:
    //     When implemented in a derived class, returns class-specific System.Windows.Automation.Peers.AutomationPeer
    //     implementations for the Silverlight automation infrastructure.
    //
    // Returns:
    //     The class-specific System.Windows.Automation.Peers.AutomationPeer subclass
    //     to return.
    protected virtual AutomationPeer OnCreateAutomationPeer();
    //
    // Summary:
    //     Removes mouse capture from a System.Windows.UIElement. After this call, typically
    //     no object holds mouse capture.
    public void ReleaseMouseCapture();
    //
    // Summary:
    //     Removes the specified routed event handler from this System.Windows.UIElement.
    //
    // Parameters:
    //   routedEvent:
    //     The identifier of the routed event for which the handler is attached.
    //
    //   handler:
    //     The specific handler implementation to remove from the event handler collection
    //     on this System.Windows.UIElement.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     routedEvent or handler is null.
    //
    //   System.ArgumentException:
    //     routedEvent does not represent a supported routed event.-or-handler does
    //     not implement a supported delegate.
    //
    //   System.NotImplementedException:
    //     Attempted to remove handler for an event not supported by the current platform
    //     variation.
    public void RemoveHandler(RoutedEvent routedEvent, Delegate handler);
    //
    // Summary:
    //     Returns a transform object that can be used to transform coordinates from
    //     the System.Windows.UIElement to the specified object.
    //
    // Parameters:
    //   visual:
    //     The object to compare to the current object for purposes of obtaining the
    //     transform.
    //
    // Returns:
    //     The transform information as an object. Call System.Windows.Media.GeneralTransform.Transform(System.Windows.Point)
    //     on this object to get a practical transform.
    public GeneralTransform TransformToVisual(UIElement visual);
    //
    // Summary:
    //     Ensures that all positions of child objects of a System.Windows.UIElement
    //     are properly updated for layout.
    public void UpdateLayout();
  }
}
