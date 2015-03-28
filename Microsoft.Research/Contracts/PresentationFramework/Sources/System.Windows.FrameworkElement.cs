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

// File System.Windows.FrameworkElement.cs
// Automatically generated contract file.
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics.Contracts;
using System;

// Disable the "this variable is not used" warning as every field would imply it.
#pragma warning disable 0414
// Disable the "this variable is never assigned to".
#pragma warning disable 0067
// Disable the "this event is never assigned to".
#pragma warning disable 0649
// Disable the "this variable is never used".
#pragma warning disable 0169
// Disable the "new keyword not required" warning.
#pragma warning disable 0109
// Disable the "extern without DllImport" warning.
#pragma warning disable 0626
// Disable the "could hide other member" warning, can happen on certain properties.
#pragma warning disable 0108


namespace System.Windows
{
  public partial class FrameworkElement : UIElement, IFrameworkInputElement, IInputElement, System.ComponentModel.ISupportInitialize, System.Windows.Markup.IHaveResources, System.Windows.Markup.IQueryAmbient
  {
    #region Methods and constructors
    protected internal void AddLogicalChild(Object child)
    {
    }

    public bool ApplyTemplate()
    {
      return default(bool);
    }

    protected sealed override void ArrangeCore(Rect finalRect)
    {
    }

    /// <summary> 
    /// ArrangeOverride allows for the customization of the positioning of children. 
    /// </summary>
    /// <param name="finalSize">The final size that element should use to arrange itself and its children.</param> 
    /// <returns>The size that element actually is going to use for rendering. If this size is not the same as finalSize
    /// input parameter, the AlignmentX/AlignmentY properties will position the ink rect of the element 
    /// appropriately.</returns> 
    protected virtual Size ArrangeOverride(Size finalSize)
    {
      // Only a positive number is allowed here for width and height, since this will be used as the size of a physical Rect.
      // See also comments or implementation of UIElement.Arrange()
      Contract.Requires(!finalSize.IsEmpty 
        && !double.IsNaN(finalSize.Width) && !double.IsNaN(finalSize.Height)
        && !double.IsPositiveInfinity(finalSize.Width) && !double.IsPositiveInfinity(finalSize.Height));

      /* The function must return a physical size, i.e. positive numbers for Width and Height, so this would be the correct result contract: 
       * 
       * Contract.Ensures(!Contract.Result<Size>().IsEmpty
       * && !double.IsNaN(Contract.Result<Size>().Width) && !double.IsNaN(Contract.Result<Size>().Height)
       * && !double.IsInfinity(Contract.Result<Size>().Width) && !double.IsInfinity(Contract.Result<Size>().Height));
       * 
       * However in practice this is unusable; the analyzer can't infer all double operations in the method, 
       * because there are usually complex calculations, and we would end up with always the same huge Contract.Assume at the end, just 
       * to make the analyzer happy.
       */

      return default(Size);
    }

    public virtual new void BeginInit()
    {
    }

    public void BeginStoryboard(System.Windows.Media.Animation.Storyboard storyboard, System.Windows.Media.Animation.HandoffBehavior handoffBehavior)
    {
    }

    public void BeginStoryboard(System.Windows.Media.Animation.Storyboard storyboard, System.Windows.Media.Animation.HandoffBehavior handoffBehavior, bool isControllable)
    {
    }

    public void BeginStoryboard(System.Windows.Media.Animation.Storyboard storyboard)
    {
    }

    public void BringIntoView()
    {
      Contract.Ensures(System.Windows.Rect.Empty.Width == System.Windows.Rect.Empty.Height);
      Contract.Ensures(System.Windows.Rect.Empty.X == System.Windows.Rect.Empty.Y);
    }

    public void BringIntoView(Rect targetRectangle)
    {
    }

    public virtual new void EndInit()
    {
    }

    public Object FindName(string name)
    {
      return default(Object);
    }

    public Object FindResource(Object resourceKey)
    {
      return default(Object);
    }

    public FrameworkElement()
    {
    }

    public System.Windows.Data.BindingExpression GetBindingExpression(DependencyProperty dp)
    {
      return default(System.Windows.Data.BindingExpression);
    }

    public static FlowDirection GetFlowDirection(DependencyObject element)
    {
      return default(FlowDirection);
    }

    protected override System.Windows.Media.Geometry GetLayoutClip(Size layoutSlotSize)
    {
      return default(System.Windows.Media.Geometry);
    }

    protected internal DependencyObject GetTemplateChild(string childName)
    {
      return default(DependencyObject);
    }

    protected override DependencyObject GetUIParentCore()
    {
      return default(DependencyObject);
    }

    protected override System.Windows.Media.Visual GetVisualChild(int index)
    {
      return default(System.Windows.Media.Visual);
    }

    protected sealed override Size MeasureCore(Size availableSize)
    {
      return default(Size);
    }

    /// <summary> 
    /// Measurement override. Implement your size-to-content logic here.
    /// </summary> 
    /// <param name="availableSize">Available size that parent can give to the child. May be infinity (when parent wants to 
    /// measure to content). This is soft constraint. Child can return bigger size to indicate that it wants bigger space and hope 
    /// that parent can throw in scrolling...</param>
    /// <returns>Desired Size of the control, given available size passed as parameter.</returns> 
    protected virtual Size MeasureOverride(Size availableSize)
    {
      // Only positive number or positive infinity is allowed here for width and height, see also comments on UIElement.Measure.
      Contract.Requires(!availableSize.IsEmpty && !double.IsNaN(availableSize.Width) && !double.IsNaN(availableSize.Height));

      /* The function must return a physical size, i.e. positive numbers for Width and Height, so this would be the correct result contract: 
       * 
       * Contract.Ensures(!Contract.Result<Size>().IsEmpty
       * && !double.IsNaN(Contract.Result<Size>().Width) && !double.IsNaN(Contract.Result<Size>().Height)
       * && !double.IsInfinity(Contract.Result<Size>().Width) && !double.IsInfinity(Contract.Result<Size>().Height));
       * 
       * However in practice this is unusable; the analyzer can't infer all double operations in the method, 
       * because there are usually complex calculations, and we would end up with always the same huge Contract.Assume at the end, just 
       * to make the analyzer happy.
       */

      return default(Size);
    }

    public sealed override bool MoveFocus(System.Windows.Input.TraversalRequest request)
    {
      return default(bool);
    }

    public virtual new void OnApplyTemplate()
    {
    }

    protected virtual new void OnContextMenuClosing(System.Windows.Controls.ContextMenuEventArgs e)
    {
        Contract.Requires(e != null);
    }

    protected virtual new void OnContextMenuOpening(System.Windows.Controls.ContextMenuEventArgs e)
    {
        Contract.Requires(e != null);
    }

    protected override void OnGotFocus(RoutedEventArgs e)
    {
    }

    protected virtual new void OnInitialized(EventArgs e)
    {
        Contract.Requires(e != null);
    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
    }

    protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
    {
    }

    protected internal virtual new void OnStyleChanged(Style oldStyle, Style newStyle)
    {
    }

    protected virtual new void OnToolTipClosing(System.Windows.Controls.ToolTipEventArgs e)
    {
        Contract.Requires(e != null);
    }

    protected virtual new void OnToolTipOpening(System.Windows.Controls.ToolTipEventArgs e)
    {
        Contract.Requires(e != null);
    }

    protected override void OnVisualParentChanged(DependencyObject oldParent)
    {
    }

    protected internal virtual new void ParentLayoutInvalidated(UIElement child)
    {
    }

    public sealed override DependencyObject PredictFocus(System.Windows.Input.FocusNavigationDirection direction)
    {
      return default(DependencyObject);
    }

    public void RegisterName(string name, Object scopedElement)
    {
    }

    protected internal void RemoveLogicalChild(Object child)
    {
    }

    public System.Windows.Data.BindingExpression SetBinding(DependencyProperty dp, string path)
    {
      return default(System.Windows.Data.BindingExpression);
    }

    public System.Windows.Data.BindingExpressionBase SetBinding(DependencyProperty dp, System.Windows.Data.BindingBase binding)
    {
      return default(System.Windows.Data.BindingExpressionBase);
    }

    public static void SetFlowDirection(DependencyObject element, FlowDirection value)
    {
    }

    public void SetResourceReference(DependencyProperty dp, Object name)
    {
    }

    public bool ShouldSerializeResources()
    {
      Contract.Ensures(0 <= this.Resources.Count);

      return default(bool);
    }

    public bool ShouldSerializeStyle()
    {
      return default(bool);
    }

    public bool ShouldSerializeTriggers()
    {
      return default(bool);
    }

    bool System.Windows.Markup.IQueryAmbient.IsAmbientPropertyAvailable(string propertyName)
    {
      return default(bool);
    }

    public Object TryFindResource(Object resourceKey)
    {
      return default(Object);
    }

    public void UnregisterName(string name)
    {
    }
    #endregion

    #region Properties and indexers
    public double ActualHeight
    {
      get
      {
        Contract.Ensures(Contract.Result<double>() >= 0.0);

        return default(double);
      }
    }

    public double ActualWidth
    {
      get
      {
        Contract.Ensures(Contract.Result<double>() >= 0.0);

        return default(double);
      }
    }

    public System.Windows.Data.BindingGroup BindingGroup
    {
      get
      {
        return default(System.Windows.Data.BindingGroup);
      }
      set
      {
      }
    }

    public System.Windows.Controls.ContextMenu ContextMenu
    {
      get
      {
        return default(System.Windows.Controls.ContextMenu);
      }
      set
      {
      }
    }

    public System.Windows.Input.Cursor Cursor
    {
      get
      {
        return default(System.Windows.Input.Cursor);
      }
      set
      {
      }
    }

    public Object DataContext
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    internal protected Object DefaultStyleKey
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public FlowDirection FlowDirection
    {
      get
      {
        Contract.Ensures(((System.Windows.FlowDirection)(0)) <= Contract.Result<System.Windows.FlowDirection>());
        Contract.Ensures(Contract.Result<System.Windows.FlowDirection>() <= ((System.Windows.FlowDirection)(1)));

        return default(FlowDirection);
      }
      set
      {
      }
    }

    public System.Windows.Style FocusVisualStyle
    {
      get
      {
        return default(System.Windows.Style);
      }
      set
      {
      }
    }

    public bool ForceCursor
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public double Height
    {
      get
      {
        Contract.Ensures(Contract.Result<double>() >= 0 || double.IsNaN(Contract.Result<double>()));

        return default(double);
      }
      set
      {
        Contract.Requires(value >= 0 || double.IsNaN(value));
      }
    }

    public HorizontalAlignment HorizontalAlignment
    {
      get
      {
        return default(HorizontalAlignment);
      }
      set
      {
      }
    }

    internal protected InheritanceBehavior InheritanceBehavior
    {
      get
      {
        Contract.Ensures(((System.Windows.InheritanceBehavior)(0)) <= Contract.Result<System.Windows.InheritanceBehavior>());
        Contract.Ensures(Contract.Result<System.Windows.InheritanceBehavior>() <= ((System.Windows.InheritanceBehavior)(7)));

        return default(InheritanceBehavior);
      }
      set
      {
      }
    }

    public System.Windows.Input.InputScope InputScope
    {
      get
      {
        return default(System.Windows.Input.InputScope);
      }
      set
      {
      }
    }

    public bool IsInitialized
    {
      get
      {
        return default(bool);
      }
    }

    public bool IsLoaded
    {
      get
      {
        return default(bool);
      }
    }

    public System.Windows.Markup.XmlLanguage Language
    {
      get
      {
        return default(System.Windows.Markup.XmlLanguage);
      }
      set
      {
      }
    }

    public System.Windows.Media.Transform LayoutTransform
    {
      get
      {
        return default(System.Windows.Media.Transform);
      }
      set
      {
      }
    }

    internal protected virtual new System.Collections.IEnumerator LogicalChildren
    {
      get
      {
        return default(System.Collections.IEnumerator);
      }
    }

    public Thickness Margin
    {
      get
      {
        return default(Thickness);
      }
      set
      {
      }
    }

    public double MaxHeight
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double MaxWidth
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double MinHeight
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public double MinWidth
    {
      get
      {
        return default(double);
      }
      set
      {
      }
    }

    public string Name
    {
      get
      {
        return default(string);
      }
      set
      {
      }
    }

    public bool OverridesDefaultStyle
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public DependencyObject Parent
    {
      get
      {
        return default(DependencyObject);
      }
    }

    public ResourceDictionary Resources
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.ResourceDictionary>() != null);

        return default(ResourceDictionary);
      }
      set
      {
      }
    }

    public Style Style
    {
      get
      {
        return default(Style);
      }
      set
      {
      }
    }

    ResourceDictionary System.Windows.Markup.IHaveResources.Resources
    {
      get
      {
        return default(ResourceDictionary);
      }
      set
      {
      }
    }

    public Object Tag
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public DependencyObject TemplatedParent
    {
      get
      {
        return default(DependencyObject);
      }
    }

    public Object ToolTip
    {
      get
      {
        return default(Object);
      }
      set
      {
      }
    }

    public TriggerCollection Triggers
    {
      get
      {
        Contract.Ensures(Contract.Result<System.Windows.TriggerCollection>() != null);

        return default(TriggerCollection);
      }
    }

    public bool UseLayoutRounding
    {
      get
      {
        return default(bool);
      }
      set
      {
      }
    }

    public VerticalAlignment VerticalAlignment
    {
      get
      {
        return default(VerticalAlignment);
      }
      set
      {
      }
    }

    protected override int VisualChildrenCount
    {
      get
      {
        return default(int);
      }
    }

    public double Width
    {
      get
      {
        Contract.Ensures(Contract.Result<double>() >= 0 || double.IsNaN(Contract.Result<double>()));

        return default(double);
      }
      set
      {
        Contract.Requires(value >= 0 || double.IsNaN(value));
      }
    }
    #endregion

    #region Events
    public event System.Windows.Controls.ContextMenuEventHandler ContextMenuClosing
    {
      add
      {
      }
      remove
      {
      }
    }

    public event System.Windows.Controls.ContextMenuEventHandler ContextMenuOpening
    {
      add
      {
      }
      remove
      {
      }
    }

    public event DependencyPropertyChangedEventHandler DataContextChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler Initialized
    {
      add
      {
      }
      remove
      {
      }
    }

    public event RoutedEventHandler Loaded
    {
      add
      {
      }
      remove
      {
      }
    }

    public event RequestBringIntoViewEventHandler RequestBringIntoView
    {
      add
      {
      }
      remove
      {
      }
    }

    public event SizeChangedEventHandler SizeChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler<System.Windows.Data.DataTransferEventArgs> SourceUpdated
    {
      add
      {
      }
      remove
      {
      }
    }

    public event EventHandler<System.Windows.Data.DataTransferEventArgs> TargetUpdated
    {
      add
      {
      }
      remove
      {
      }
    }

    public event System.Windows.Controls.ToolTipEventHandler ToolTipClosing
    {
      add
      {
      }
      remove
      {
      }
    }

    public event System.Windows.Controls.ToolTipEventHandler ToolTipOpening
    {
      add
      {
      }
      remove
      {
      }
    }

    public event RoutedEventHandler Unloaded
    {
      add
      {
      }
      remove
      {
      }
    }
    #endregion

    #region Fields
    public readonly static DependencyProperty ActualHeightProperty;
    public readonly static DependencyProperty ActualWidthProperty;
    public readonly static DependencyProperty BindingGroupProperty;
    public readonly static RoutedEvent ContextMenuClosingEvent;
    public readonly static RoutedEvent ContextMenuOpeningEvent;
    public readonly static DependencyProperty ContextMenuProperty;
    public readonly static DependencyProperty CursorProperty;
    public readonly static DependencyProperty DataContextProperty;
    internal protected readonly static DependencyProperty DefaultStyleKeyProperty;
    public readonly static DependencyProperty FlowDirectionProperty;
    public readonly static DependencyProperty FocusVisualStyleProperty;
    public readonly static DependencyProperty ForceCursorProperty;
    public readonly static DependencyProperty HeightProperty;
    public readonly static DependencyProperty HorizontalAlignmentProperty;
    public readonly static DependencyProperty InputScopeProperty;
    public readonly static DependencyProperty LanguageProperty;
    public readonly static DependencyProperty LayoutTransformProperty;
    public readonly static RoutedEvent LoadedEvent;
    public readonly static DependencyProperty MarginProperty;
    public readonly static DependencyProperty MaxHeightProperty;
    public readonly static DependencyProperty MaxWidthProperty;
    public readonly static DependencyProperty MinHeightProperty;
    public readonly static DependencyProperty MinWidthProperty;
    public readonly static DependencyProperty NameProperty;
    public readonly static DependencyProperty OverridesDefaultStyleProperty;
    public readonly static RoutedEvent RequestBringIntoViewEvent;
    public readonly static RoutedEvent SizeChangedEvent;
    public readonly static DependencyProperty StyleProperty;
    public readonly static DependencyProperty TagProperty;
    public readonly static RoutedEvent ToolTipClosingEvent;
    public readonly static RoutedEvent ToolTipOpeningEvent;
    public readonly static DependencyProperty ToolTipProperty;
    public readonly static RoutedEvent UnloadedEvent;
    public readonly static DependencyProperty UseLayoutRoundingProperty;
    public readonly static DependencyProperty VerticalAlignmentProperty;
    public readonly static DependencyProperty WidthProperty;
    #endregion
  }
}
