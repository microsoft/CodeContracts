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
using System.Security;
using System.Diagnostics.Contracts;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;

namespace System.Windows
{
  // Summary:
  //     Provides a framework of common APIs for objects that participate in Silverlight
  //     layout. System.Windows.FrameworkElement also defines APIs related to data
  //     binding, object tree, and object lifetime feature areas in Silverlight.
  public abstract class FrameworkElement : UIElement
  {
    // Summary:
    //     Identifies the System.Windows.FrameworkElement.ActualHeight dependency property.
    //
    // Returns:
    //     The identifier for the System.Windows.FrameworkElement.ActualHeight dependency
    //     property.
    public static readonly DependencyProperty ActualHeightProperty;
    //
    // Summary:
    //     Identifies the System.Windows.FrameworkElement.ActualWidth dependency property.
    //
    // Returns:
    //     The identifier for the System.Windows.FrameworkElement.ActualWidth dependency
    //     property.
    public static readonly DependencyProperty ActualWidthProperty;
    //
    // Summary:
    //     Identifies the System.Windows.FrameworkElement.DataContext dependency property.
    //
    // Returns:
    //     The System.Windows.FrameworkElement.DataContext dependency property identifier.
    public static readonly DependencyProperty DataContextProperty;
    //
    // Summary:
    //     Identifies the System.Windows.FrameworkElement.Height dependency property.
    //
    // Returns:
    //     The identifier for the System.Windows.FrameworkElement.Height dependency
    //     property.
    public static readonly DependencyProperty HeightProperty;
    //
    // Summary:
    //     Identifies the System.Windows.FrameworkElement.HorizontalAlignment dependency
    //     property.
    //
    // Returns:
    //     The System.Windows.FrameworkElement.HorizontalAlignment dependency property
    //     identifier.
    public static readonly DependencyProperty HorizontalAlignmentProperty;
    //
    // Summary:
    //     Identifies the System.Windows.FrameworkElement.Language dependency property.
    //
    // Returns:
    //     The System.Windows.FrameworkElement.Language dependency property identifier.
    public static readonly DependencyProperty LanguageProperty;
    //
    // Summary:
    //     Identifies the System.Windows.FrameworkElement.Loaded routed event.
    //
    // Returns:
    //     The identifier for the System.Windows.FrameworkElement.Loaded routed event.
    public static readonly RoutedEvent LoadedEvent;
    //
    // Summary:
    //     Identifies the System.Windows.FrameworkElement.Margin dependency property.
    //
    // Returns:
    //     The System.Windows.FrameworkElement.Margin dependency property identifier.
    public static readonly DependencyProperty MarginProperty;
    //
    // Summary:
    //     Identifies the System.Windows.FrameworkElement.MaxHeight dependency property.
    //
    // Returns:
    //     The identifier for the System.Windows.FrameworkElement.MaxHeight dependency
    //     property.
    public static readonly DependencyProperty MaxHeightProperty;
    //
    // Summary:
    //     Identifies the System.Windows.FrameworkElement.MaxWidth dependency property.
    //
    // Returns:
    //     The identifier for the System.Windows.FrameworkElement.MaxWidth dependency
    //     property.
    public static readonly DependencyProperty MaxWidthProperty;
    //
    // Summary:
    //     Identifies the System.Windows.FrameworkElement.MinHeight dependency property.
    //
    // Returns:
    //     The identifier for the System.Windows.FrameworkElement.MinHeight dependency
    //     property.
    public static readonly DependencyProperty MinHeightProperty;
    //
    // Summary:
    //     Identifies the System.Windows.FrameworkElement.MinWidth dependency property.
    //
    // Returns:
    //     The identifier for the System.Windows.FrameworkElement.MinWidth dependency
    //     property.
    public static readonly DependencyProperty MinWidthProperty;
    //
    // Summary:
    //     Identifies the System.Windows.FrameworkElement.Name dependency property.
    //
    // Returns:
    //     The identifier for the System.Windows.FrameworkElement.Name dependency property.
    public static readonly DependencyProperty NameProperty;
    //
    // Summary:
    //     Identifies the System.Windows.FrameworkElement.Style dependency property.
    //
    // Returns:
    //     The identifier for the System.Windows.FrameworkElement.Style dependency property.
    public static readonly DependencyProperty StyleProperty;
    //
    // Summary:
    //     Identifies the System.Windows.FrameworkElement.Tag dependency property.
    //
    // Returns:
    //     The identifier for the System.Windows.FrameworkElement.Tag dependency property.
    public static readonly DependencyProperty TagProperty;
    //
    // Summary:
    //     Identifies the System.Windows.FrameworkElement.VerticalAlignment dependency
    //     property.
    //
    // Returns:
    //     The System.Windows.FrameworkElement.VerticalAlignment dependency property
    //     identifier.
    public static readonly DependencyProperty VerticalAlignmentProperty;
    //
    // Summary:
    //     Identifies the System.Windows.FrameworkElement.Width dependency property.
    //
    // Returns:
    //     The identifier for the System.Windows.FrameworkElement.Width dependency property.
    public static readonly DependencyProperty WidthProperty;

    // Summary:
    //     Initializes a new instance of the System.Windows.FrameworkElement class.
    protected FrameworkElement();

    // Summary:
    //     Gets the rendered height of a System.Windows.FrameworkElement.
    //
    // Returns:
    //     The height, in pixels, of the object. The default is 0. The default might
    //     be encountered if the object has not been loaded and undergone a layout pass.
    public double ActualHeight { get; }
    //
    // Summary:
    //     Gets the rendered width of a System.Windows.FrameworkElement.
    //
    // Returns:
    //     The width, in pixels, of the object. The default is 0. The default might
    //     be encountered if the object has not been loaded and undergone a layout pass.
    public double ActualWidth { get; }
    //
    // Summary:
    //     Gets or sets the cursor that displays while the mouse pointer is over a System.Windows.FrameworkElement.
    //
    // Returns:
    //     The cursor to display. The default is defined as null for code access. However,
    //     the appearance of the cursor in UI at run time will come from a variety of
    //     factors.
    public Cursor Cursor { get; set; }
    //
    // Summary:
    //     Gets or sets the data context for a System.Windows.FrameworkElement when
    //     it participates in data binding.
    //
    // Returns:
    //     The object to use as data context.
    public object DataContext { get; set; }
    //
    // Summary:
    //     Gets or sets the suggested height of a System.Windows.FrameworkElement.
    //
    // Returns:
    //     The height, in pixels, of the object. The default is System.Double.NaN. Except
    //     for the special System.Double.NaN value, this value must be equal to or greater
    //     than 0. See Remarks for upper-bound information.
    public double Height { get; set; }
    //
    // Summary:
    //     Gets or sets the horizontal alignment characteristics that are applied to
    //     a System.Windows.FrameworkElement when it is composed in a layout parent,
    //     such as a panel or items control.
    //
    // Returns:
    //     A horizontal alignment setting, as a value of the enumeration. The default
    //     is System.Windows.HorizontalAlignment.Stretch.
    public HorizontalAlignment HorizontalAlignment { get; set; }
    //
    // Summary:
    //     Gets or sets localization/globalization language information that applies
    //     to a System.Windows.FrameworkElement.
    //
    // Returns:
    //     The language information for this object. The default is an System.Windows.Markup.XmlLanguage
    //     object that has its System.Windows.Markup.XmlLanguage.IetfLanguageTag value
    //     set to the string "en-US".
    public XmlLanguage Language { get; set; }
    //
    // Summary:
    //     Gets or sets the outer margin of a System.Windows.FrameworkElement.
    //
    // Returns:
    //     Provides margin values for the object. The default value is a default System.Windows.Thickness
    //     with all properties (dimensions) equal to 0.
    public Thickness Margin { get; set; }
    //
    // Summary:
    //     Gets or sets the maximum height constraint of a System.Windows.FrameworkElement.
    //
    // Returns:
    //     The maximum height of the object, in pixels. The default value is System.Double.PositiveInfinity.
    //     This value can be any value equal to or greater than 0. System.Double.PositiveInfinity
    //     is also valid.
    public double MaxHeight { get; set; }
    //
    // Summary:
    //     Gets or sets the maximum width constraint of a System.Windows.FrameworkElement.
    //
    // Returns:
    //     The maximum width of the object, in pixels. The default is System.Double.PositiveInfinity.
    //     This value can be any value equal to or greater than 0. System.Double.PositiveInfinity
    //     is also valid.
    public double MaxWidth { get; set; }
    //
    // Summary:
    //     Gets or sets the minimum height constraint of a System.Windows.FrameworkElement.
    //
    // Returns:
    //     The minimum height of the object, in pixels. The default is 0. This value
    //     can be any value equal to or greater than 0. However, System.Double.PositiveInfinity
    //     is not valid.
    public double MinHeight { get; set; }
    //
    // Summary:
    //     Gets or sets the minimum width constraint of a System.Windows.FrameworkElement.
    //
    // Returns:
    //     The minimum width of the object, in pixels. The default is 0. This value
    //     can be any value equal to or greater than 0. However, System.Double.PositiveInfinity
    //     is not valid.
    public double MinWidth { get; set; }
    //
    // Summary:
    //     Gets or sets the identifying name of the object. When a XAML processor creates
    //     the object tree from XAML markup, run-time code can refer to the XAML-declared
    //     object by this name.
    //
    // Returns:
    //     The name of the object, which must be a string that is valid in the XamlName
    //     Grammar. The default is an empty string.
    public string Name { get; set; }
    //
    // Summary:
    //     Gets the parent object of this System.Windows.FrameworkElement in the object
    //     tree.
    //
    // Returns:
    //     The parent object of this object in the object tree.
    public DependencyObject Parent { get; }
    //
    // Summary:
    //     Gets the locally defined resource dictionary. In XAML, you can establish
    //     resource items as child object elements of a frameworkElement.Resources property
    //     element, through XAML implicit collection syntax.
    //
    // Returns:
    //     The current locally defined dictionary of resources, where each resource
    //     can be accessed by its key.
    public ResourceDictionary Resources { get; set; }
    //
    // Summary:
    //     Gets or sets an instance System.Windows.Style that is applied for this object
    //     during rendering.
    //
    // Returns:
    //     The applied style for the object, if present; otherwise, null. The default
    //     for a default-constructed System.Windows.FrameworkElement is null.
    public Style Style { get; set; }
    //
    // Summary:
    //     Gets or sets an arbitrary object value that can be used to store custom information
    //     about this object.
    //
    // Returns:
    //     The intended value. This property has no default value.
    public object Tag { get; set; }
    //
    // Summary:
    //     Gets the collection of triggers for animations that are defined for a System.Windows.FrameworkElement.
    //
    // Returns:
    //     The collection of triggers for animations that are defined for this object.
    public TriggerCollection Triggers { get; }
    //
    // Summary:
    //     Gets or sets the vertical alignment characteristics that are applied to a
    //     System.Windows.FrameworkElement when it is composed in a parent object such
    //     as a panel or items control.
    //
    // Returns:
    //     A vertical alignment setting. The default is System.Windows.VerticalAlignment.Stretch.
    public VerticalAlignment VerticalAlignment { get; set; }
    //
    // Summary:
    //     Gets or sets the width of a System.Windows.FrameworkElement.
    //
    // Returns:
    //     The width of the object, in pixels. The default is System.Double.NaN. Except
    //     for the special System.Double.NaN value, this value must be equal to or greater
    //     than 0. See Remarks for upper bound information.
    public double Width { get; set; }

    // Summary:
    //     Occurs when a data validation error is reported by a binding source.
    public event EventHandler<ValidationErrorEventArgs> BindingValidationError;
    //
    // Summary:
    //     Occurs when the layout of the Silverlight visual tree changes.
    public event EventHandler LayoutUpdated;
    //
    // Summary:
    //     Occurs when a System.Windows.FrameworkElement has been constructed and added
    //     to the object tree.
    public event RoutedEventHandler Loaded;
    //
    // Summary:
    //     Occurs when either the System.Windows.FrameworkElement.ActualHeight or the
    //     System.Windows.FrameworkElement.ActualWidth properties change value on a
    //     System.Windows.FrameworkElement.
    public event SizeChangedEventHandler SizeChanged;
    //
    // Summary:
    //     Occurs when this object is no longer connected to the main object tree.
    public event RoutedEventHandler Unloaded;

    // Summary:
    //     Provides the behavior for the Arrange pass of Silverlight layout. Classes
    //     can override this method to define their own Arrange pass behavior.
    //
    // Parameters:
    //   finalSize:
    //     The final area within the parent that this object should use to arrange itself
    //     and its children.
    //
    // Returns:
    //     The actual size that is used after the element is arranged in layout.
    protected virtual Size ArrangeOverride(Size finalSize);
    //
    // Summary:
    //     Retrieves an object that has the specified identifier name.
    //
    // Parameters:
    //   name:
    //     The name of the requested object.
    //
    // Returns:
    //     The requested object. This can be null if no matching object was found in
    //     the current XAML namescope.
    public object FindName(string name);
    //
    // Summary:
    //     Retrieves the System.Windows.Data.BindingExpression for a dependency property
    //     where a binding is established.
    //
    // Parameters:
    //   dp:
    //     The dependency property identifier for the specific property on this System.Windows.FrameworkElement
    //     where you want to obtain the System.Windows.Data.BindingExpression.
    //
    // Returns:
    //     A System.Windows.Data.BindingExpression for the binding, if the local value
    //     represented a data-bound value. May return null if the property is not a
    //     data-bound value.
    public BindingExpression GetBindingExpression(DependencyProperty dp);
    //
    // Summary:
    //     Provides the behavior for the Measure pass of Silverlight layout. Classes
    //     can override this method to define their own Measure pass behavior.
    //
    // Parameters:
    //   availableSize:
    //     The available size that this object can give to child objects. Infinity (System.Double.PositiveInfinity)
    //     can be specified as a value to indicate that the object will size to whatever
    //     content is available.
    //
    // Returns:
    //     The size that this object determines it needs during layout, based on its
    //     calculations of the allocated sizes for child objects; or based on other
    //     considerations, such as a fixed container size.
    protected virtual Size MeasureOverride(Size availableSize);
    //
    // Summary:
    //     When overridden in a derived class, is invoked whenever application code
    //     or internal processes (such as a rebuilding layout pass) call System.Windows.Controls.Control.ApplyTemplate().
    //     In simplest terms, this means the method is called just before a UI element
    //     displays in an application. For more information, see Remarks.
    [SecuritySafeCritical]
    public virtual void OnApplyTemplate();
    //
    // Summary:
    //     Attaches a binding to a System.Windows.FrameworkElement, using the provided
    //     binding object, and returns a System.Windows.Data.BindingExpressionBase for
    //     possible later use.
    //
    // Parameters:
    //   dp:
    //     The dependency property identifier of the property that is data bound.
    //
    //   binding:
    //     The binding to use for the property.
    //
    // Returns:
    //     A System.Windows.Data.BindingExpressionBase object. See Remarks.
    //
    // Exceptions:
    //   System.ArgumentException:
    //     binding is specified as System.Windows.Data.BindingMode.TwoWay, but has an
    //     empty System.Windows.Data.Binding.Path.-or-dp or binding parameters are null.
    public BindingExpressionBase SetBinding(DependencyProperty dp, Binding binding);
  }
}
