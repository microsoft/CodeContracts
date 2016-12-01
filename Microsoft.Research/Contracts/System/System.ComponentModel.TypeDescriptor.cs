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

#if !SILVERLIGHT

using System;
using System.Collections;
using System.Reflection;
using System.Diagnostics.Contracts;

namespace System.ComponentModel
{
  // Summary:
  //     Provides information about the characteristics for a component, such as its
  //     attributes, properties, and events. This class cannot be inherited.
  public sealed class TypeDescriptor
  {
    private TypeDescriptor() { }

    // Summary:
    //     Gets or sets the provider for the Component Object Model (COM) type information
    //     for the target component.
    //
    // Returns:
    //     An System.ComponentModel.IComNativeDescriptorHandler instance representing
    //     the COM type information provider.
    //[Obsolete("This property has been deprecated.  Use a type description provider to supply type information for COM types instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
    //public static IComNativeDescriptorHandler ComNativeDescriptorHandler { get; set; }
    //
    // Summary:
    //     Gets the type of the Component Object Model (COM) object represented by the
    //     target component.
    //
    // Returns:
    //     The System.Type of the COM object represented by this component, or null
    //     for non-COM objects.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    public static Type ComObjectType
    {
      get
      {
        Contract.Ensures(Contract.Result<Type>() != null);

        return default(Type);
      }
    }

    // Summary:
    //     Occurs when the cache for a component is cleared.
    //public static event RefreshEventHandler Refreshed;

    // Summary:
    //     Adds class-level attributes to the target component instance.
    //
    // Parameters:
    //   instance:
    //     An instance of the target component.
    //
    //   attributes:
    //     An array of System.Attribute objects to add to the component's class.
    //
    // Returns:
    //     The newly created System.ComponentModel.TypeDescriptionProvider that was
    //     used to add the specified attributes.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     One or both of the parameters is null.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    public static TypeDescriptionProvider AddAttributes(object instance, params Attribute[] attributes)
    {
      Contract.Requires(instance != null);
      Contract.Requires(attributes != null);

      Contract.Ensures(Contract.Result<TypeDescriptionProvider>() != null);

      return default(TypeDescriptionProvider);
    }
    //
    // Summary:
    //     Adds class-level attributes to the target component type.
    //
    // Parameters:
    //   type:
    //     The System.Type of the target component.
    //
    //   attributes:
    //     An array of System.Attribute objects to add to the component's class.
    //
    // Returns:
    //     The newly created System.ComponentModel.TypeDescriptionProvider that was
    //     used to add the specified attributes.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     One or both of the parameters is null.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    public static TypeDescriptionProvider AddAttributes(Type type, params Attribute[] attributes)
    {
      Contract.Requires(type != null);
      Contract.Requires(attributes != null);

      Contract.Ensures(Contract.Result<TypeDescriptionProvider>() != null);

      return default(TypeDescriptionProvider);
    }
    //
    // Summary:
    //     Adds an editor table for the given editor base type.
    //
    // Parameters:
    //   editorBaseType:
    //     The editor base type to add the editor table for. If a table already exists
    //     for this type, this method will do nothing.
    //
    //   table:
    //     The System.Collections.Hashtable to add.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    // F: table can be null
    public static void AddEditorTable(Type editorBaseType, Hashtable table)
    {
      Contract.Requires(editorBaseType != null);

    }
    //
    // Summary:
    //     Adds a type description provider for a single instance of a component.
    //
    // Parameters:
    //   provider:
    //     The System.ComponentModel.TypeDescriptionProvider to add.
    //
    //   instance:
    //     An instance of the target component.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     One or both of the parameters are null.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    public static void AddProvider(TypeDescriptionProvider provider, object instance)
    {
      Contract.Requires(provider != null);
      Contract.Requires(instance != null);
    }
    //
    // Summary:
    //     Adds a type description provider for a component class.
    //
    // Parameters:
    //   provider:
    //     The System.ComponentModel.TypeDescriptionProvider to add.
    //
    //   type:
    //     The System.Type of the target component.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     One or both of the parameters are null.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    public static void AddProvider(TypeDescriptionProvider provider, Type type)
    {
      Contract.Requires(provider != null);
      Contract.Requires(type != null);

    }
    //
    // Summary:
    //     Creates a primary-secondary association between two objects.
    //
    // Parameters:
    //   primary:
    //     The primary System.Object.
    //
    //   secondary:
    //     The secondary System.Object.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     One or both of the parameters are null.
    //
    //   System.ArgumentException:
    //     primary is equal to secondary.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    public static void CreateAssociation(object primary, object secondary)
    {
      Contract.Requires(primary != null);
      Contract.Requires(secondary != null);

    }

    //
    // Summary:
    //     Creates an instance of the designer associated with the specified component
    //     and of the specified type of designer.
    //
    // Parameters:
    //   component:
    //     An System.ComponentModel.IComponent that specifies the component to associate
    //     with the designer.
    //
    //   designerBaseType:
    //     A System.Type that represents the type of designer to create.
    //
    // Returns:
    //     An System.ComponentModel.Design.IDesigner that is an instance of the designer
    //     for the component, or null if no designer can be found.
    //public static IDesigner CreateDesigner(IComponent component, Type designerBaseType)
    //{

    //}
    //
    // Summary:
    //     Creates a new event descriptor that is identical to an existing event descriptor,
    //     when passed the existing System.ComponentModel.EventDescriptor.
    //
    // Parameters:
    //   componentType:
    //     The type of the component for which to create the new event.
    //
    //   oldEventDescriptor:
    //     The existing event information.
    //
    //   attributes:
    //     The new attributes.
    //
    // Returns:
    //     A new System.ComponentModel.EventDescriptor that has merged the specified
    //     metadata attributes with the existing metadata attributes.
    public static EventDescriptor CreateEvent(Type componentType, EventDescriptor oldEventDescriptor, params Attribute[] attributes)
    {
      Contract.Ensures(Contract.Result<EventDescriptor>() != null);

      return default(EventDescriptor);
    }
    //
    // Summary:
    //     Creates a new event descriptor that is identical to an existing event descriptor
    //     by dynamically generating descriptor information from a specified event on
    //     a type.
    //
    // Parameters:
    //   componentType:
    //     The type of the component the event lives on.
    //
    //   name:
    //     The name of the event.
    //
    //   type:
    //     The type of the delegate that handles the event.
    //
    //   attributes:
    //     The attributes for this event.
    //
    // Returns:
    //     An System.ComponentModel.EventDescriptor that is bound to a type.
    public static EventDescriptor CreateEvent(Type componentType, string name, Type type, params Attribute[] attributes)
    {
      Contract.Ensures(Contract.Result<EventDescriptor>() != null);

      return default(EventDescriptor);
    }

    //
    // Summary:
    //     Creates an object that can substitute for another data type.
    //
    // Parameters:
    //   provider:
    //     The service provider that provides a System.ComponentModel.TypeDescriptionProvider
    //     service. This parameter can be null.
    //
    //   objectType:
    //     The System.Type of object to create.
    //
    //   argTypes:
    //     An optional array of parameter types to be passed to the object's constructor.
    //     This parameter can be null or an array of zero length.
    //
    //   args:
    //     An optional array of parameter values to pass to the object's constructor.
    //     If not null, the number of elements must be the same as argTypes.
    //
    // Returns:
    //     An instance of the substitute data type if an associated System.ComponentModel.TypeDescriptionProvider
    //     is found; otherwise, null.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     objectType is null, or args is null when argTypes is not null.
    //
    //   System.ArgumentException:
    //     argTypes and args have different number of elements.
    public static object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
    {
      Contract.Requires(objectType != null);
      Contract.Requires(argTypes.Length == args.Length);

      Contract.Ensures(Contract.Result<object>() != null);

      return default(object);
    }
    //
    // Summary:
    //     Creates a new property descriptor from an existing property descriptor, using
    //     the specified existing System.ComponentModel.PropertyDescriptor and attribute
    //     array.
    //
    // Parameters:
    //   componentType:
    //     The System.Type of the component that the property is a member of.
    //
    //   oldPropertyDescriptor:
    //     The existing property descriptor.
    //
    //   attributes:
    //     The new attributes for this property.
    //
    // Returns:
    //     A new System.ComponentModel.PropertyDescriptor that has the specified metadata
    //     attributes merged with the existing metadata attributes.
    public static PropertyDescriptor CreateProperty(Type componentType, PropertyDescriptor oldPropertyDescriptor, params Attribute[] attributes)
    {
      Contract.Ensures(Contract.Result<PropertyDescriptor>() != null);

      return default(PropertyDescriptor);
    }

    //
    // Summary:
    //     Creates and dynamically binds a property descriptor to a type, using the
    //     specified property name, type, and attribute array.
    //
    // Parameters:
    //   componentType:
    //     The System.Type of the component that the property is a member of.
    //
    //   name:
    //     The name of the property.
    //
    //   type:
    //     The System.Type of the property.
    //
    //   attributes:
    //     The new attributes for this property.
    //
    // Returns:
    //     A System.ComponentModel.PropertyDescriptor that is bound to the specified
    //     type and that has the specified metadata attributes merged with the existing
    //     metadata attributes.
    public static PropertyDescriptor CreateProperty(Type componentType, string name, Type type, params Attribute[] attributes)
    {
      Contract.Ensures(Contract.Result<PropertyDescriptor>() != null);

      return default(PropertyDescriptor);
    }
    //
    // Summary:
    //     Returns an instance of the type associated with the specified primary object.
    //
    // Parameters:
    //   type:
    //     The System.Type of the target component.
    //
    //   primary:
    //     The primary object of the association.
    //
    // Returns:
    //     An instance of the secondary type that has been associated with the primary
    //     object if an association exists; otherwise, primary if no specified association
    //     exists.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     One or both of the parameters are null.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    public static object GetAssociation(Type type, object primary)
    {
      Contract.Requires(type != null);
      Contract.Requires(primary != null);

      Contract.Ensures(Contract.Result<object>() != null);

      return default(object);
    }
    //
    // Summary:
    //     Returns the collection of attributes for the specified component.
    //
    // Parameters:
    //   component:
    //     The component for which you want to get attributes.
    //
    // Returns:
    //     An System.ComponentModel.AttributeCollection containing the attributes for
    //     the component. If component is null, this method returns an empty collection.
    //public static AttributeCollection GetAttributes(object component);
    //
    // Summary:
    //     Returns a collection of attributes for the specified type of component.
    //
    // Parameters:
    //   componentType:
    //     The System.Type of the target component.
    //
    // Returns:
    //     An System.ComponentModel.AttributeCollection with the attributes for the
    //     type of the component. If the component is null, this method returns an empty
    //     collection.
    //public static AttributeCollection GetAttributes(Type componentType);
    //
    // Summary:
    //     Returns a collection of attributes for the specified component and a Boolean
    //     indicating that a custom type descriptor has been created.
    //
    // Parameters:
    //   component:
    //     The component for which you want to get attributes.
    //
    //   noCustomTypeDesc:
    //     true to use a baseline set of attributes from the custom type descriptor
    //     if component is of type System.ComponentModel.ICustomTypeDescriptor; otherwise,
    //     false.
    //
    // Returns:
    //     An System.ComponentModel.AttributeCollection with the attributes for the
    //     component. If the component is null, this method returns an empty collection.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public static AttributeCollection GetAttributes(object component, bool noCustomTypeDesc);
    //
    // Summary:
    //     Returns the name of the class for the specified component using the default
    //     type descriptor.
    //
    // Parameters:
    //   component:
    //     The System.Object for which you want the class name.
    //
    // Returns:
    //     A System.String containing the name of the class for the specified component.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     component is null.
    public static string GetClassName(object component)
    {
      Contract.Requires(component != null);

      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
    //
    // Summary:
    //     Returns the name of the class for the specified type.
    //
    // Parameters:
    //   componentType:
    //     The System.Type of the target component.
    //
    // Returns:
    //     A System.String containing the name of the class for the specified component
    //     type.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     componentType is null.
    public static string GetClassName(Type componentType)
    {
      Contract.Requires(componentType != null);

      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
    //
    // Summary:
    //     Returns the name of the class for the specified component using a custom
    //     type descriptor.
    //
    // Parameters:
    //   component:
    //     The System.Object for which you want the class name.
    //
    //   noCustomTypeDesc:
    //     true to consider custom type description information; otherwise, false.
    //
    // Returns:
    //     A System.String containing the name of the class for the specified component.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     component is null.
    //
    //   System.NotSupportedException:
    //     component is a cross-process remoted object.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    public static string GetClassName(object component, bool noCustomTypeDesc)
    {
      Contract.Requires(component != null);

      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
    //
    // Summary:
    //     Returns the name of the specified component using the default type descriptor.
    //
    // Parameters:
    //   component:
    //     The System.Object for which you want the class name.
    //
    // Returns:
    //     A System.String containing the name of the specified component, or null if
    //     there is no component name.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     component is null.
    //
    //   System.NotSupportedException:
    //     component is a cross-process remoted object.
    public static string GetComponentName(object component)
    {
      Contract.Requires(component != null);

      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
    //
    // Summary:
    //     Returns the name of the specified component using a custom type descriptor.
    //
    // Parameters:
    //   component:
    //     The System.Object for which you want the class name.
    //
    //   noCustomTypeDesc:
    //     true to consider custom type description information; otherwise, false.
    //
    // Returns:
    //     The name of the class for the specified component, or null if there is no
    //     component name.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     component is null.
    //
    //   System.NotSupportedException:
    //     component is a cross-process remoted object.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    public static string GetComponentName(object component, bool noCustomTypeDesc)
    {
      Contract.Requires(component != null);

      Contract.Ensures(Contract.Result<string>() != null);

      return default(string);
    }
    //
    // Summary:
    //     Returns a type converter for the type of the specified component.
    //
    // Parameters:
    //   component:
    //     A component to get the converter for.
    //
    // Returns:
    //     A System.ComponentModel.TypeConverter for the specified component.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     component is null.
    //
    //   System.NotSupportedException:
    //     component is a cross-process remoted object.
    public static TypeConverter GetConverter(object component)
    {
      Contract.Requires(component != null);
      Contract.Ensures(Contract.Result<TypeConverter>() != null);

      return default(TypeConverter);
    }
    //
    // Summary:
    //     Returns a type converter for the specified type.
    //
    // Parameters:
    //   type:
    //     The System.Type of the target component.
    //
    // Returns:
    //     A System.ComponentModel.TypeConverter for the specified type.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     component is null.
    public static TypeConverter GetConverter(Type type)
    {
      Contract.Requires(type != null);
      Contract.Ensures(Contract.Result<TypeConverter>() != null);

      return default(TypeConverter);
    }

    //
    // Summary:
    //     Returns a type converter for the type of the specified component with a custom
    //     type descriptor.
    //
    // Parameters:
    //   component:
    //     A component to get the converter for.
    //
    //   noCustomTypeDesc:
    //     true to consider custom type description information; otherwise, false.
    //
    // Returns:
    //     A System.ComponentModel.TypeConverter for the specified component.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     component is null.
    //
    //   System.NotSupportedException:
    //     component is a cross-process remoted object.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    public static TypeConverter GetConverter(object component, bool noCustomTypeDesc)
    {
      Contract.Requires(component != null);
      Contract.Ensures(Contract.Result<TypeConverter>() != null);

      return default(TypeConverter);
    }
    //
    // Summary:
    //     Returns the default event for the specified component.
    //
    // Parameters:
    //   component:
    //     The component to get the event for.
    //
    // Returns:
    //     An System.ComponentModel.EventDescriptor with the default event, or null
    //     if there are no events.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     component is null.
    //
    //   System.NotSupportedException:
    //     component is a cross-process remoted object.
    // F: The actual implementation does not respect the documentation (it returns null if component is null)
    public static EventDescriptor GetDefaultEvent(object component)
    {
      Contract.Requires(component != null);

      Contract.Ensures(component == null || Contract.Result<EventDescriptor>() != null);


      return default(EventDescriptor);
    }
    //
    // Summary:
    //     Returns the default event for the specified type of component.
    //
    // Parameters:
    //   componentType:
    //     The System.Type of the target component.
    //
    // Returns:
    //     An System.ComponentModel.EventDescriptor with the default event, or null
    //     if there are no events.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     component is null.
    //public static EventDescriptor GetDefaultEvent(Type componentType)
    //
    // Summary:
    //     Returns the default event for a component with a custom type descriptor.
    //
    // Parameters:
    //   component:
    //     The component to get the event for.
    //
    //   noCustomTypeDesc:
    //     true to consider custom type description information; otherwise, false.
    //
    // Returns:
    //     An System.ComponentModel.EventDescriptor with the default event, or null
    //     if there are no events.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     component is null.
    //
    //   System.NotSupportedException:
    //     component is a cross-process remoted object.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    public static EventDescriptor GetDefaultEvent(object component, bool noCustomTypeDesc)
    // F: The actual implementation does not respect the documentation (it returns null if component is null)
    {
      Contract.Requires(component != null);

      Contract.Ensures(component == null || Contract.Result<EventDescriptor>() != null);

      return default(EventDescriptor);
    }
    //
    // Summary:
    //     Returns the default property for the specified component.
    //
    // Parameters:
    //   component:
    //     The component to get the default property for.
    //
    // Returns:
    //     A System.ComponentModel.PropertyDescriptor with the default property, or
    //     null if there are no properties.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     component is a cross-process remoted object.
    //public static PropertyDescriptor GetDefaultProperty(object component);
    //
    // Summary:
    //     Returns the default property for the specified type of component.
    //
    // Parameters:
    //   componentType:
    //     A System.Type that represents the class to get the property for.
    //
    // Returns:
    //     A System.ComponentModel.PropertyDescriptor with the default property, or
    //     null if there are no properties.
    //public static PropertyDescriptor GetDefaultProperty(Type componentType);
    //
    // Summary:
    //     Returns the default property for the specified component with a custom type
    //     descriptor.
    //
    // Parameters:
    //   component:
    //     The component to get the default property for.
    //
    //   noCustomTypeDesc:
    //     true to consider custom type description information; otherwise, false.
    //
    // Returns:
    //     A System.ComponentModel.PropertyDescriptor with the default property, or
    //     null if there are no properties.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     component is a cross-process remoted object.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    //public static PropertyDescriptor GetDefaultProperty(object component, bool noCustomTypeDesc);
    //
    // Summary:
    //     Gets an editor with the specified base type for the specified component.
    //
    // Parameters:
    //   component:
    //     The component to get the editor for.
    //
    //   editorBaseType:
    //     A System.Type that represents the base type of the editor you want to find.
    //
    // Returns:
    //     An instance of the editor that can be cast to the specified editor type,
    //     or null if no editor of the requested type can be found.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     component or editorBaseType is null.
    //
    //   System.NotSupportedException:
    //     component is a cross-process remoted object.
    public static object GetEditor(object component, Type editorBaseType)
    {
      Contract.Requires(component != null);
      Contract.Requires(editorBaseType != null);

      return default(object);
    }
    //
    // Summary:
    //     Returns an editor with the specified base type for the specified type.
    //
    // Parameters:
    //   type:
    //     The System.Type of the target component.
    //
    //   editorBaseType:
    //     A System.Type that represents the base type of the editor you are trying
    //     to find.
    //
    // Returns:
    //     An instance of the editor object that can be cast to the given base type,
    //     or null if no editor of the requested type can be found.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     type or editorBaseType is null.
    public static object GetEditor(Type type, Type editorBaseType)
    {
      Contract.Requires(type != null);
      Contract.Requires(editorBaseType != null);

      return default(object);
    }
    //
    // Summary:
    //     Returns an editor with the specified base type and with a custom type descriptor
    //     for the specified component.
    //
    // Parameters:
    //   component:
    //     The component to get the editor for.
    //
    //   editorBaseType:
    //     A System.Type that represents the base type of the editor you want to find.
    //
    //   noCustomTypeDesc:
    //     A flag indicating whether custom type description information should be considered.
    //
    // Returns:
    //     An instance of the editor that can be cast to the specified editor type,
    //     or null if no editor of the requested type can be found.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     component or editorBaseType is null.
    //
    //   System.NotSupportedException:
    //     component is a cross-process remoted object.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    public static object GetEditor(object component, Type editorBaseType, bool noCustomTypeDesc)
    {
      Contract.Requires(component != null);
      Contract.Requires(editorBaseType != null);

      return default(object);
    }
    //
    // Summary:
    //     Returns the collection of events for the specified component.
    //
    // Parameters:
    //   component:
    //     A component to get the events for.
    //
    // Returns:
    //     An System.ComponentModel.EventDescriptorCollection with the events for this
    //     component.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     component is a cross-process remoted object.
    public static EventDescriptorCollection GetEvents(object component)
    {
      Contract.Ensures(Contract.Result<EventDescriptorCollection>() != null);

      return default(EventDescriptorCollection);
    }

    // Summary:
    //     Returns the collection of events for a specified type of component.
    //
    // Parameters:
    //   componentType:
    //     The System.Type of the target component.
    //
    // Returns:
    //     An System.ComponentModel.EventDescriptorCollection with the events for this
    //     component.
    public static EventDescriptorCollection GetEvents(Type componentType)
    {
      Contract.Ensures(Contract.Result<EventDescriptorCollection>() != null);

      return default(EventDescriptorCollection);
    }

    // Summary:
    //     Returns the collection of events for a specified component using a specified
    //     array of attributes as a filter.
    //
    // Parameters:
    //   component:
    //     A component to get the events for.
    //
    //   attributes:
    //     An array of type System.Attribute that you can use as a filter.
    //
    // Returns:
    //     An System.ComponentModel.EventDescriptorCollection with the events that match
    //     the specified attributes for this component.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     component is a cross-process remoted object.
    public static EventDescriptorCollection GetEvents(object component, Attribute[] attributes)
    {
      Contract.Ensures(Contract.Result<EventDescriptorCollection>() != null);

      return default(EventDescriptorCollection);
    }
    // Summary:
    //     Returns the collection of events for a specified component with a custom
    //     type descriptor.
    //
    // Parameters:
    //   component:
    //     A component to get the events for.
    //
    //   noCustomTypeDesc:
    //     true to consider custom type description information; otherwise, false.
    //
    // Returns:
    //     An System.ComponentModel.EventDescriptorCollection with the events for this
    //     component.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     component is a cross-process remoted object.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    public static EventDescriptorCollection GetEvents(object component, bool noCustomTypeDesc)
    {
      Contract.Ensures(Contract.Result<EventDescriptorCollection>() != null);

      return default(EventDescriptorCollection);
    }

    // Summary:
    //     Returns the collection of events for a specified type of component using
    //     a specified array of attributes as a filter.
    //
    // Parameters:
    //   componentType:
    //     The System.Type of the target component.
    //
    //   attributes:
    //     An array of type System.Attribute that you can use as a filter.
    //
    // Returns:
    //     An System.ComponentModel.EventDescriptorCollection with the events that match
    //     the specified attributes for this component.
    public static EventDescriptorCollection GetEvents(Type componentType, Attribute[] attributes)
    {
      Contract.Ensures(Contract.Result<EventDescriptorCollection>() != null);

      return default(EventDescriptorCollection);
    }


    // Summary:
    //     Returns the collection of events for a specified component using a specified
    //     array of attributes as a filter and using a custom type descriptor.
    //
    // Parameters:
    //   component:
    //     A component to get the events for.
    //
    //   attributes:
    //     An array of type System.Attribute to use as a filter.
    //
    //   noCustomTypeDesc:
    //     true to consider custom type description information; otherwise, false.
    //
    // Returns:
    //     An System.ComponentModel.EventDescriptorCollection with the events that match
    //     the specified attributes for this component.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     component is a cross-process remoted object.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    public static EventDescriptorCollection GetEvents(object component, Attribute[] attributes, bool noCustomTypeDesc)
    {
      Contract.Ensures(Contract.Result<EventDescriptorCollection>() != null);

      return default(EventDescriptorCollection);
    }

    // Summary:
    //     Returns the fully qualified name of the component.
    //
    // Parameters:
    //   component:
    //     The System.ComponentModel.Component to find the name for.
    //
    // Returns:
    //     The fully qualified name of the specified component, or null if the component
    //     has no name.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     component is null.
    public static string GetFullComponentName(object component)
    {
      Contract.Requires(component != null);

      return default(string);
    }
    //
    // Summary:
    //     Returns the collection of properties for a specified component.
    //
    // Parameters:
    //   component:
    //     A component to get the properties for.
    //
    // Returns:
    //     A System.ComponentModel.PropertyDescriptorCollection with the properties
    //     for the specified component.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     component is a cross-process remoted object.
    public static PropertyDescriptorCollection GetProperties(object component)
    {
      Contract.Ensures(Contract.Result<PropertyDescriptorCollection>() != null);

      return default(PropertyDescriptorCollection);
    }
    //
    // Summary:
    //     Returns the collection of properties for a specified type of component.
    //
    // Parameters:
    //   componentType:
    //     A System.Type that represents the component to get properties for.
    //
    // Returns:
    //     A System.ComponentModel.PropertyDescriptorCollection with the properties
    //     for a specified type of component.
    public static PropertyDescriptorCollection GetProperties(Type componentType)
    {
      Contract.Ensures(Contract.Result<PropertyDescriptorCollection>() != null);

      return default(PropertyDescriptorCollection);
    }

    //
    // Summary:
    //     Returns the collection of properties for a specified component using a specified
    //     array of attributes as a filter.
    //
    // Parameters:
    //   component:
    //     A component to get the properties for.
    //
    //   attributes:
    //     An array of type System.Attribute to use as a filter.
    //
    // Returns:
    //     A System.ComponentModel.PropertyDescriptorCollection with the properties
    //     that match the specified attributes for the specified component.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     component is a cross-process remoted object.
    public static PropertyDescriptorCollection GetProperties(object component, Attribute[] attributes)
    {
      Contract.Ensures(Contract.Result<PropertyDescriptorCollection>() != null);

      return default(PropertyDescriptorCollection);
    }
    //
    // Summary:
    //     Returns the collection of properties for a specified component using the
    //     default type descriptor.
    //
    // Parameters:
    //   component:
    //     A component to get the properties for.
    //
    //   noCustomTypeDesc:
    //     true to consider custom type description information; otherwise, false.
    //
    // Returns:
    //     A System.ComponentModel.PropertyDescriptorCollection with the properties
    //     for a specified component.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     component is a cross-process remoted object.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    public static PropertyDescriptorCollection GetProperties(object component, bool noCustomTypeDesc)
    {
      Contract.Ensures(Contract.Result<PropertyDescriptorCollection>() != null);

      return default(PropertyDescriptorCollection);
    }
    //
    // Summary:
    //     Returns the collection of properties for a specified type of component using
    //     a specified array of attributes as a filter.
    //
    // Parameters:
    //   componentType:
    //     The System.Type of the target component.
    //
    //   attributes:
    //     An array of type System.Attribute to use as a filter.
    //
    // Returns:
    //     A System.ComponentModel.PropertyDescriptorCollection with the properties
    //     that match the specified attributes for this type of component.
    public static PropertyDescriptorCollection GetProperties(Type componentType, Attribute[] attributes)
    {
      Contract.Ensures(Contract.Result<PropertyDescriptorCollection>() != null);

      return default(PropertyDescriptorCollection);
    }
    //
    // Summary:
    //     Returns the collection of properties for a specified component using a specified
    //     array of attributes as a filter and using a custom type descriptor.
    //
    // Parameters:
    //   component:
    //     A component to get the properties for.
    //
    //   attributes:
    //     An array of type System.Attribute to use as a filter.
    //
    //   noCustomTypeDesc:
    //     true to consider custom type description information; otherwise, false.
    //
    // Returns:
    //     A System.ComponentModel.PropertyDescriptorCollection with the events that
    //     match the specified attributes for the specified component.
    //
    // Exceptions:
    //   System.NotSupportedException:
    //     component is a cross-process remoted object.
    public static PropertyDescriptorCollection GetProperties(object component, Attribute[] attributes, bool noCustomTypeDesc)
    {
      Contract.Ensures(Contract.Result<PropertyDescriptorCollection>() != null);

      return default(PropertyDescriptorCollection);
    }
    //
    // Summary:
    //     Returns the type description provider for the specified component.
    //
    // Parameters:
    //   instance:
    //     An instance of the target component.
    //
    // Returns:
    //     A System.ComponentModel.TypeDescriptionProvider associated with the specified
    //     component.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     instance is null.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    public static TypeDescriptionProvider GetProvider(object instance)
    {
      Contract.Requires(instance != null);

      return default(TypeDescriptionProvider);
    }
    //
    // Summary:
    //     Returns the type description provider for the specified type.
    //
    // Parameters:
    //   type:
    //     The System.Type of the target component.
    //
    // Returns:
    //     A System.ComponentModel.TypeDescriptionProvider associated with the specified
    //     type.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     type is null.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    public static TypeDescriptionProvider GetProvider(Type type)
    {
      Contract.Requires(type != null);
      Contract.Ensures(Contract.Result<TypeDescriptionProvider>() != null);

      return default(TypeDescriptionProvider);
    }
    //
    // Summary:
    //     Returns a System.Type that can be used to perform reflection, given an object.
    //
    // Parameters:
    //   instance:
    //     An instance of the target component.
    //
    // Returns:
    //     A System.Type for the specified object.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     instance is null.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    public static Type GetReflectionType(object instance)
    {
      Contract.Requires(instance != null);

      return default(Type);
    }
    //
    // Summary:
    //     Returns a System.Type that can be used to perform reflection, given a class
    //     type.
    //
    // Parameters:
    //   type:
    //     The System.Type of the target component.
    //
    // Returns:
    //     A System.Type of the specified class.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     type is null.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    public static Type GetReflectionType(Type type)
    {
      Contract.Requires(type != null);

      return default(Type);
    }
    //
    // Summary:
    //     Clears the properties and events for the specified assembly from the cache.
    //
    // Parameters:
    //   assembly:
    //     The System.Reflection.Assembly that represents the assembly to refresh. Each
    //     System.Type in this assembly will be refreshed.
    //public static void Refresh(Assembly assembly);
    //
    // Summary:
    //     Clears the properties and events for the specified module from the cache.
    //
    // Parameters:
    //   module:
    //     The System.Reflection.Module that represents the module to refresh. Each
    //     System.Type in this module will be refreshed.
    //public static void Refresh(Module module);
    //
    // Summary:
    //     Clears the properties and events for the specified component from the cache.
    //
    // Parameters:
    //   component:
    //     A component for which the properties or events have changed.
    //public static void Refresh(object component);
    //
    // Summary:
    //     Clears the properties and events for the specified type of component from
    //     the cache.
    //
    // Parameters:
    //   type:
    //     The System.Type of the target component.
    //public static void Refresh(Type type);
    //
    // Summary:
    //     Removes an association between two objects.
    //
    // Parameters:
    //   primary:
    //     The primary System.Object.
    //
    //   secondary:
    //     The secondary System.Object.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     One or both of the parameters are null.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    public static void RemoveAssociation(object primary, object secondary)
    {
      Contract.Requires(primary != null);
      Contract.Requires(secondary != null);

    }

    // Summary:
    //     Removes all associations for a primary object.
    //
    // Parameters:
    //   primary:
    //     The primary System.Object in an association.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     primary is null.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    public static void RemoveAssociations(object primary)
    {
      Contract.Requires(primary != null);

    }
    //
    // Summary:
    //     Removes a previously added type description provider that is associated with
    //     the specified object.
    //
    // Parameters:
    //   provider:
    //     The System.ComponentModel.TypeDescriptionProvider to remove.
    //
    //   instance:
    //     An instance of the target component.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     One or both of the parameters are null.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    public static void RemoveProvider(TypeDescriptionProvider provider, object instance)
    {
      Contract.Requires(provider != null);
      Contract.Requires(instance != null);


    }
    //
    // Summary:
    //     Removes a previously added type description provider that is associated with
    //     the specified type.
    //
    // Parameters:
    //   provider:
    //     The System.ComponentModel.TypeDescriptionProvider to remove.
    //
    //   type:
    //     The System.Type of the target component.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     One or both of the parameters are null.
    //[EditorBrowsable(EditorBrowsableState.Advanced)]
    public static void RemoveProvider(TypeDescriptionProvider provider, Type type)
    {
      Contract.Requires(provider != null);
      Contract.Requires(type != null);


    }
    //
    // Summary:
    //     Sorts descriptors using the name of the descriptor.
    //
    // Parameters:
    //   infos:
    //     An System.Collections.IList that contains the descriptors to sort.
    //
    // Exceptions:
    //   System.ArgumentNullException:
    //     infos is null.
    public static void SortDescriptorArray(IList infos)
    {
      Contract.Requires(infos != null);

    }
  }
}

#endif
