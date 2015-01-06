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
using System.Collections;
using System.Runtime.InteropServices;
using System.Diagnostics.Contracts;

namespace System.ComponentModel
{
  // Summary:
  //     Provides an abstraction of a property on a class.
  //[ComVisible(true)]
  public abstract class PropertyDescriptor //: MemberDescriptor
  {
#if SILVERLIGHT_4_0_WP
    internal
#elif SILVERLIGHT
    private
#else
    protected
#endif
    PropertyDescriptor() { }

    // Summary:
    //     Initializes a new instance of the System.ComponentModel.PropertyDescriptor
    //     class with the name and attributes in the specified System.ComponentModel.MemberDescriptor.
    //
    // Parameters:
    //   descr:
    //     A System.ComponentModel.MemberDescriptor that contains the name of the property
    //     and its attributes.
    //protected PropertyDescriptor(MemberDescriptor descr);
    //
    // Summary:
    //     Initializes a new instance of the System.ComponentModel.PropertyDescriptor
    //     class with the name in the specified System.ComponentModel.MemberDescriptor
    //     and the attributes in both the System.ComponentModel.MemberDescriptor and
    //     the System.Attribute array.
    //
    // Parameters:
    //   descr:
    //     A System.ComponentModel.MemberDescriptor containing the name of the member
    //     and its attributes.
    //
    //   attrs:
    //     An System.Attribute array containing the attributes you want to associate
    //     with the property.
    //protected PropertyDescriptor(MemberDescriptor descr, Attribute[] attrs);
    //
    // Summary:
    //     Initializes a new instance of the System.ComponentModel.PropertyDescriptor
    //     class with the specified name and attributes.
    //
    // Parameters:
    //   name:
    //     The name of the property.
    //
    //   attrs:
    //     An array of type System.Attribute that contains the property attributes.
    //protected PropertyDescriptor(string name, Attribute[] attrs);

    // Summary:
    //     When overridden in a derived class, gets the type of the component this property
    //     is bound to.
    //
    // Returns:
    //     A System.Type that represents the type of component this property is bound
    //     to. When the System.ComponentModel.PropertyDescriptor.GetValue(System.Object)
    //     or System.ComponentModel.PropertyDescriptor.SetValue(System.Object,System.Object)
    //     methods are invoked, the object specified might be an instance of this type.
    //public abstract Type ComponentType { get; }
    //
    // Summary:
    //     Gets the type converter for this property.
    //
    // Returns:
    //     A System.ComponentModel.TypeConverter that is used to convert the System.Type
    //     of this property.
    //public virtual TypeConverter Converter { get; }
    //
    // Summary:
    //     Gets a value indicating whether this property should be localized, as specified
    //     in the System.ComponentModel.LocalizableAttribute.
    //
    // Returns:
    //     true if the member is marked with the System.ComponentModel.LocalizableAttribute
    //     set to true; otherwise, false.
    //public virtual bool IsLocalizable { get; }
    //
    // Summary:
    //     When overridden in a derived class, gets a value indicating whether this
    //     property is read-only.
    //
    // Returns:
    //     true if the property is read-only; otherwise, false.
    //public abstract bool IsReadOnly { get; }
    //
    // Summary:
    //     When overridden in a derived class, gets the type of the property.
    //
    // Returns:
    //     A System.Type that represents the type of the property.
    //public abstract Type PropertyType { get; }
    //
    // Summary:
    //     Gets a value indicating whether this property should be serialized, as specified
    //     in the System.ComponentModel.DesignerSerializationVisibilityAttribute.
    //
    // Returns:
    //     One of the System.ComponentModel.DesignerSerializationVisibility enumeration
    //     values that specifies whether this property should be serialized.
    //public DesignerSerializationVisibility SerializationVisibility { get; }
    //
    // Summary:
    //     Gets a value indicating whether value change notifications for this property
    //     may originate from outside the property descriptor.
    //
    // Returns:
    //     true if value change notifications may originate from outside the property
    //     descriptor; otherwise, false.
    //public virtual bool SupportsChangeEvents { get; }


#if !SILVERLIGHT
    // Summary:
    //     Enables other objects to be notified when this property changes.
    //
    // Parameters:
    //   component:
    //     The component to add the handler for.
    //
    //   handler:
    //     The delegate to add as a listener.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     component or handler is null.
    public virtual void AddValueChanged(object component, EventHandler handler)
    {
      Contract.Requires(component != null);
      Contract.Requires(handler != null);
    }

    //
    // Summary:
    //     When overridden in a derived class, returns whether resetting an object changes
    //     its value.
    //
    // Parameters:
    //   component:
    //     The component to test for reset capability.
    //
    // Returns:
    //     true if resetting the component changes its value; otherwise, false.
    //public abstract bool CanResetValue(object component);
    //
    // Summary:
    //     Creates an instance of the specified type.
    //
    // Parameters:
    //   type:
    //     A System.Type that represents the type to create.
    //
    // Returns:
    //     A new instance of the type.
    protected object CreateInstance(Type type)
    {
      Contract.Ensures(Contract.Result<object>() != null);

      return default(object);
    }
#endif
    //
    // Summary:
    //     Compares this to another object to see if they are equivalent.
    //
    // Parameters:
    //   obj:
    //     The object to compare to this System.ComponentModel.PropertyDescriptor.
    //
    // Returns:
    //     true if the values are equivalent; otherwise, false.
    //public override bool Equals(object obj);
    //
    // Summary:
    //     Adds the attributes of the System.ComponentModel.PropertyDescriptor to the
    //     specified list of attributes in the parent class.
    //
    // Parameters:
    //   attributeList:
    //     An System.Collections.IList that lists the attributes in the parent class.
    //     Initially, this is empty.
    //protected override void FillAttributes(IList attributeList);
    //
    // Summary:
    //     Returns the default System.ComponentModel.PropertyDescriptorCollection.
    //
    // Returns:
    //     A System.ComponentModel.PropertyDescriptorCollection.
    //public PropertyDescriptorCollection GetChildProperties();
    //
    // Summary:
    //     Returns a System.ComponentModel.PropertyDescriptorCollection using a specified
    //     array of attributes as a filter.
    //
    // Parameters:
    //   filter:
    //     An array of type System.Attribute to use as a filter.
    //
    // Returns:
    //     A System.ComponentModel.PropertyDescriptorCollection with the properties
    //     that match the specified attributes.
    //public PropertyDescriptorCollection GetChildProperties(Attribute[] filter);
    //
    // Summary:
    //     Returns a System.ComponentModel.PropertyDescriptorCollection for a given
    //     object.
    //
    // Parameters:
    //   instance:
    //     A component to get the properties for.
    //
    // Returns:
    //     A System.ComponentModel.PropertyDescriptorCollection with the properties
    //     for the specified component.
    //public PropertyDescriptorCollection GetChildProperties(object instance);
    //
    // Summary:
    //     Returns a System.ComponentModel.PropertyDescriptorCollection for a given
    //     object using a specified array of attributes as a filter.
    //
    // Parameters:
    //   instance:
    //     A component to get the properties for.
    //
    //   filter:
    //     An array of type System.Attribute to use as a filter.
    //
    // Returns:
    //     A System.ComponentModel.PropertyDescriptorCollection with the properties
    //     that match the specified attributes for the specified component.
    //public virtual PropertyDescriptorCollection GetChildProperties(object instance, Attribute[] filter);
    //
    // Summary:
    //     Gets an editor of the specified type.
    //
    // Parameters:
    //   editorBaseType:
    //     The base type of editor, which is used to differentiate between multiple
    //     editors that a property supports.
    //
    // Returns:
    //     An instance of the requested editor type, or null if an editor cannot be
    //     found.
    //public virtual object GetEditor(Type editorBaseType);
    //
    // Summary:
    //     Returns the hash code for this object.
    //
    // Returns:
    //     The hash code for this object.
    //public override int GetHashCode();
    //
    // Summary:
    //     This method returns the object that should be used during invocation of members.
    //
    // Parameters:
    //   type:
    //     The System.Type of the invocation target.
    //
    //   instance:
    //     The potential invocation target.
    //
    // Returns:
    //     The System.Object that should be used during invocation of members.
    //protected override object GetInvocationTarget(Type type, object instance);
    //
    // Summary:
    //     Returns a type using its name.
    //
    // Parameters:
    //   typeName:
    //     The assembly-qualified name of the type to retrieve.
    //
    // Returns:
    //     A System.Type that matches the given type name, or null if a match cannot
    //     be found.
    //protected Type GetTypeFromName(string typeName);
    //
    // Summary:
    //     When overridden in a derived class, gets the current value of the property
    //     on a component.
    //
    // Parameters:
    //   component:
    //     The component with the property for which to retrieve the value.
    //
    // Returns:
    //     The value of a property for a given component.
    //public abstract object GetValue(object component);
    //
    // Summary:
    //     Retrieves the current set of ValueChanged event handlers for a specific component
    //
    // Parameters:
    //   component:
    //     The component for which to retrieve event handlers.
    //
    // Returns:
    //     A combined multicast event handler, or null if no event handlers are currently
    //     assigned to component.
    //protected internal EventHandler GetValueChangedHandler(object component);
    //
    // Summary:
    //     Raises the ValueChanged event that you implemented.
    //
    // Parameters:
    //   component:
    //     The object that raises the event.
    //
    //   e:
    //     An System.EventArgs that contains the event data.
    //protected virtual void OnValueChanged(object component, EventArgs e);

#if !SILVERLIGHT
    //
    // Summary:
    //     Enables other objects to be notified when this property changes.
    //
    // Parameters:
    //   component:
    //     The component to remove the handler for.
    //
    //   handler:
    //     The delegate to remove as a listener.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     component or handler is null.
    public virtual void RemoveValueChanged(object component, EventHandler handler)
    {
      Contract.Requires(component != null);
      Contract.Requires(handler != null);
    }
#endif
    //
    // Summary:
    //     When overridden in a derived class, resets the value for this property of
    //     the component to the default value.
    //
    // Parameters:
    //   component:
    //     The component with the property value that is to be reset to the default
    //     value.
    //public abstract void ResetValue(object component);
    //
    // Summary:
    //     When overridden in a derived class, sets the value of the component to a
    //     different value.
    //
    // Parameters:
    //   component:
    //     The component with the property value that is to be set.
    //
    //   value:
    //     The new value.
    //public abstract void SetValue(object component, object value);
    //
    // Summary:
    //     When overridden in a derived class, determines a value indicating whether
    //     the value of this property needs to be persisted.
    //
    // Parameters:
    //   component:
    //     The component with the property to be examined for persistence.
    //
    // Returns:
    //     true if the property should be persisted; otherwise, false.
    //public abstract bool ShouldSerializeValue(object component);
  }
}
