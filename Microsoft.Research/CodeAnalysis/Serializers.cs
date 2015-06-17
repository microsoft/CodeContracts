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
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Research.DataStructures;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis.Caching
{
  /// <summary>
  /// A specialized list to serialize so that we can skip non-deserializable elements while keeping the rest
  /// </summary>
  /// <typeparam name="Method"></typeparam>
  public struct ConditionList<Method> : IEnumerable<Pair<BoxedExpression, Method>>
  {
    IEnumerable<Pair<BoxedExpression, Method>> data;

    public ConditionList(IEnumerable<Pair<BoxedExpression, Method>> data)
    {
      this.data = data;
    }

    public IEnumerator<Pair<BoxedExpression, Method>> GetEnumerator()
    {
      if (data == null) return new EmptyIndexable<Pair<BoxedExpression,Method>>().AsEnumerable().GetEnumerator();
      return data.GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }


  }


  /// <summary>
  /// Provides a surrogate selector and serialization surrogates for abstract types Type, Method, Parameter, Field and IDecodeMetaData
  /// </summary>
  /*
   * Other types usually just need the [Serializable] attribute
   * and a [NonSerialized] attribute on some fields
   * 
   * Be careful with the .net serializer and the deserialization order.
   * 
   * If you use a SerializationInfo structure, you can expect the objects returned by GetValue to be deserialized (i.e. they won't be null)
   * but not their fields if the objet uses the automatic serialization (with [Serializable] attribute).
   * The same problem happens for arrays, that's why I added an extension AddArray, GetArray
   * 
   * These serialization surrogates have been designed for serializing (BoxedExpression) inferred method pre/post conditions.
   * Using them for another purpose might require (or not) small changes.
   */
  public static class Serializers
  {
    static public ISurrogateSelector SurrogateSelectorFor<Local, Parameter, Method, Field, Property, Event, Typ, Attribute, Assembly>(
      StreamingContext context,
      Method currentMethod,
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Typ, Attribute, Assembly> mdDecoder,
      IDecodeContracts<Local, Parameter, Method, Field, Typ> contractDecoder,
      bool trace)
    {
      Contract.Requires(currentMethod != null);
      Contract.Requires(mdDecoder != null);

      return Serializers<Local, Parameter, Method, Field, Property, Event, Typ, Attribute, Assembly>.SurrogateSelectorFor(context, currentMethod, mdDecoder, contractDecoder, trace);
    }
  }

  static class Serializers<Local, Parameter, Method, Field, Property, Event, Typ, Attribute, Assembly>
  {
    #region Surrogate selector
    static public ISurrogateSelector SurrogateSelectorFor(
      StreamingContext context,
      Method currentMethod,
      IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Typ, Attribute, Assembly> mdDecoder,
      IDecodeContracts<Local, Parameter, Method, Field, Typ> contractDecoder,
      bool trace)
    {
      Contract.Requires(currentMethod != null);

      // Possible optimization: instead of creating new object each time we want to serialize something,
      // reuse them and use the Context.context to store mdDecoder and currentMethod

      // Using a SimpleSubTypeSurrogateSelector is necessary for CCI2 because its type parameter Parameter is an interface
      var selector = new SimpleSubTypeSurrogateSelector();

      selector.AddSurrogate(context, TypeSerializationSurrogate.For(currentMethod, mdDecoder, contractDecoder));
      selector.AddSurrogate(context, MethodSerializationSurrogate.For(currentMethod, mdDecoder, contractDecoder));
      selector.AddSurrogate(context, ParameterSerializationSurrogate.For(currentMethod, mdDecoder, contractDecoder));
      selector.AddSurrogate(context, FieldSerializationSurrogate.For(currentMethod, mdDecoder, contractDecoder));
      selector.AddSurrogate(context, LocalSerializationSurrogate.For(currentMethod, mdDecoder, contractDecoder));
      selector.AddSurrogate(context, ConditionListSerializationSurrogate.For(currentMethod, mdDecoder, contractDecoder, trace));

      // We want to serialize the mdDecoder *by ourselves*, that's why we do the special case
      // use mdDecoder.GetType() and not typeof(IDecodeMetaData), because SubTypeSurrogateSelector doesn't work with interfaces
      selector.AddSurrogate(mdDecoder.GetType(), context, new DecodeMetaDataSerializationSurrogate(mdDecoder));

      // Optimization: The serializer asks every single time if we have a surrogate. But we know that the answer will not change, so we cache the results
      // Using a SubTypeSurrogateSelector is necessary for abstract types since we want to (de)serialize all derived types in the same way using a metadata decoder
      ISurrogateSelector resultSelector = new CachingSurrogateSelector(selector); //new SubTypeSurrogateSelector(selector));
#if DEBUG
      TextWriter tw = context.Context as TextWriter;
      if (tw != null)
        resultSelector = new SpySurrogateSelector(tw, resultSelector);
#endif
      return resultSelector;
    }

    #region SurrogateSelector helpers

    /// <summary>
    /// Allow to use a serialization surrogate for all *subtypes* of a class
    /// and all classes implementing an interface.
    /// Not context-sensitive.
    /// </summary>
    internal class SimpleSubTypeSurrogateSelector : ISurrogateSelector
    {
      ISurrogateSelector nextSelector;
      List<Pair<Type, ISerializationSurrogate>> classes;      // map type -> ISerializationSurrogate
      List<Pair<Type, ISerializationSurrogate>> interfaces;   // map type -> ISerializationSurrogate

      public SimpleSubTypeSurrogateSelector()
      {
      }

      public void ChainSelector(ISurrogateSelector selector)
      {
        if (this.nextSelector != null)
        {
          this.nextSelector.ChainSelector(selector);
        }
        else
        {
          this.nextSelector = selector;
        }
      }

      public ISurrogateSelector GetNextSelector() { return this.nextSelector; }

      public ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector)
      {
        selector = this;
        if (classes != null)
          foreach(var c in classes)
            if (type == c.One || type.IsSubclassOf(c.One))
              return c.Two;
        if (interfaces != null)
        {
          var typeInterfaces = type.GetInterfaces();
          foreach (var i in interfaces)
            foreach (var ti in typeInterfaces)
              if (ti == i.One)
                return i.Two;
        }
        return null;
      }

      public void AddSurrogateForClass(Type type, StreamingContext context, ISerializationSurrogate surrogate)
      {
        if (classes == null)
          classes = new List<Pair<Type, ISerializationSurrogate>>();
        classes.Add(Pair.For(type, surrogate));
      }

      public void AddSurrogateForInterface(Type type, StreamingContext context, ISerializationSurrogate surrogate)
      {
        if (interfaces == null)
          interfaces = new List<Pair<Type, ISerializationSurrogate>>();
        interfaces.Add(Pair.For(type, surrogate));
      }

      public void AddSurrogate(Type type, StreamingContext context, ISerializationSurrogate surrogate)
      {
        if (type.IsInterface)
          this.AddSurrogateForInterface(type, context, surrogate);
        else
          this.AddSurrogateForClass(type, context, surrogate);
      }
    }

    private abstract class BoxedSurrogateSelector : ISurrogateSelector
    {
      protected readonly ISurrogateSelector underlyingSelector;

      protected BoxedSurrogateSelector(ISurrogateSelector selector)
      {
        if (selector == null)
          throw new ArgumentNullException();
        this.underlyingSelector = selector;
      }

      public void ChainSelector(ISurrogateSelector selector)
      {
        this.underlyingSelector.ChainSelector(selector);
      }
      public ISurrogateSelector GetNextSelector()
      {
        return this.underlyingSelector.GetNextSelector();
      }

      public abstract ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector);
    }

    /// <summary>
    /// A surrogate selector that falls back to the base type surrogate
    /// </summary>
    private class SubTypeSurrogateSelector : BoxedSurrogateSelector
    {
      public SubTypeSurrogateSelector(ISurrogateSelector selector) : base(selector) { }

      public override ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector)
      {
        for (; type != null; type = type.BaseType)
        {
          var surrogate = this.underlyingSelector.GetSurrogate(type, context, out selector);
          if (surrogate != null)
            return surrogate;
        }
        selector = null;
        return null;
      }
    }

    private class CachingSurrogateSelector : BoxedSurrogateSelector
    {
      private readonly Dictionary<Pair<Type, StreamingContext>, Pair<ISerializationSurrogate, ISurrogateSelector>> selectionCache = new Dictionary<Pair<Type, StreamingContext>, Pair<ISerializationSurrogate, ISurrogateSelector>>();

      public CachingSurrogateSelector(ISurrogateSelector selector) : base(selector) { }

      public override ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector)
      {
        var key = Pair.For(type, context);
        Pair<ISerializationSurrogate, ISurrogateSelector> val;
        if (selectionCache.TryGetValue(key, out val))
        {
          selector = val.Two;
        }
        else
        {
          var one = this.underlyingSelector.GetSurrogate(type, context, out selector);
          val = Pair.For(one, selector);
          selectionCache[key] = val;
        }
        return val.One;
      }
    }

    private class SimpleCachingSurrogateSelector : BoxedSurrogateSelector
    {
      private readonly Dictionary<Type, ISerializationSurrogate> selectionCache = new Dictionary<Type, ISerializationSurrogate>();

      public SimpleCachingSurrogateSelector(ISurrogateSelector selector) : base(selector) { }

      public override ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector)
      {
        ISerializationSurrogate surrogate;
        if (selectionCache.TryGetValue(type, out surrogate))
        {
          selector = this; // who really uses this value?
          return surrogate;
        }
        return selectionCache[type] = this.underlyingSelector.GetSurrogate(type, context, out selector);
      }
    }

#if DEBUG
    private class SpySurrogateSelector : ISurrogateSelector
    {
      private readonly TextWriter writer;
      private readonly ISurrogateSelector underlyingSelector;

      public SpySurrogateSelector(TextWriter writer, ISurrogateSelector selector)
      {
        if (writer == null || selector == null)
          throw new ArgumentNullException();
        this.writer = writer;
        this.underlyingSelector = selector;
      }

      public void ChainSelector(ISurrogateSelector selector)
      {
        this.underlyingSelector.ChainSelector(selector);
      }
      public ISurrogateSelector GetNextSelector()
      {
        return this.underlyingSelector.GetNextSelector();
      }
      public ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector selector)
      {
        var surrogate = this.underlyingSelector.GetSurrogate(type, context, out selector);
        writer.WriteLine("{0} -> {1}", type, surrogate == null ? "null" : surrogate.ToString());
        return surrogate;
      }
    }
#endif

#if DEBUG
    private class SpySerializationSurrogate : ISerializationSurrogate
    {
      private readonly TextWriter writer;

      public SpySerializationSurrogate(TextWriter writer)
      {
        if (writer == null)
          throw new ArgumentNullException();
        this.writer = writer;
      }

      private void WriteObject(Object obj)
      {
        this.writer.WriteLine("<<<{0}>>>", obj);
      }
      public void GetObjectData(Object obj, SerializationInfo info, StreamingContext context)
      {
        this.WriteObject(obj);
      }
      public Object SetObjectData(Object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
      {
        this.WriteObject(obj);
        return default(Object);
      }
    }
#endif

    #endregion

    #endregion

    #region XSerializationSurrogate
    /// <summary>
    /// common abstract class for serialization surrogates.
    /// Main reason is to be type safe, and using generics to avoid casts to/from objects
    /// </summary>
    [ContractVerification(true)]
    internal abstract class XSerializationSurrogate<X, XSS> : ISerializationSurrogate
      where XSS : XSerializationSurrogate<X, XSS>, new()
    {
      public bool Trace { get; set; }
      protected XSerializationSurrogate() { }

      private Method currentMethod;
      protected Method CurrentMethod {
        get
        {
          Contract.Ensures(Contract.Result<Method>() != null);

          Contract.Assume(this.currentMethod != null);
          return this.currentMethod;
        }
        set
        {
          Contract.Requires(value != null);
          this.currentMethod = value;
        }
      
      }
      protected IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Typ, Attribute, Assembly> mdDecoder { get; private set; }
      protected IDecodeContracts<Local, Parameter, Method, Field, Typ> contractDecoder { get; private set; }
      protected Assembly currentAssembly { get; private set; }
      protected string currentAssemblyName { get; private set; }

      static public XSS For(
        Method currentMethod,
        IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Typ, Attribute, Assembly> mdDecoder,
        IDecodeContracts<Local, Parameter, Method, Field, Typ> contractDecoder,
        bool trace = false)
      {
        Contract.Requires(currentMethod != null);

        var x = new XSS();
        x.CurrentMethod = currentMethod;
        x.mdDecoder = mdDecoder;
        x.contractDecoder = contractDecoder;
        x.currentAssembly = mdDecoder.DeclaringAssembly(currentMethod);
        x.currentAssemblyName = mdDecoder.Name(x.currentAssembly);
        x.Trace = trace;
        return x;
      }
      /// <summary>
      /// Serialization
      /// </summary>
      /// <param name="obj">What we serialize</param>
      /// <param name="info">Manually specify which fields we want to serialize</param>
      public abstract void GetObjectData(X obj, SerializationInfo info);

      /// <summary>
      /// Deserialization
      /// </summary>
      /// <param name="obj">empty object of the good type -- todo, remove it</param>
      /// <param name="info">Manually specify which fields we want to deserialize</param>
      /// <returns></returns>
      public abstract X SetObjectData(X obj, SerializationInfo info);

      public void GetObjectData(Object obj, SerializationInfo info, StreamingContext context)
      {
#if DEBUG
        var writer = context.Context as TextWriter;
        if (writer != null)
        {
          writer.WriteLine("<<<{0}>>>", obj);
          writer.WriteLine();
        }
#endif
        this.GetObjectData((X)obj, info);
      }
      public Object SetObjectData(Object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
      {
        return this.SetObjectData((X)obj, info);
      }
    }
    #endregion

    #region Serialization surrogate for Type
    sealed class TypeSerializationSurrogate : XSerializationSurrogate<Typ, TypeSerializationSurrogate>
    {
      public override void GetObjectData(Typ type, SerializationInfo info)
      {
        Contract.Assume(type != null);
        // we need this because CCI2 uses a struct, so this function is always called, even for "(null)" (however it is not called for null when Typ is a class)
        // note: the Equals of CCI2 has been modified so that the test below makes sense for structs
        if (type.Equals(null))
        {
          info.AddValue("Kind", TypeKind.Default);
          return;
        }

        Typ _t;
        IIndexable<Pair<bool, Typ>> _ipbt;
        IIndexable<Typ> typeArguments;

        if (mdDecoder.IsModified(type, out _t, out _ipbt))
        {
          throw new NotImplementedException();
        }
        else if (mdDecoder.IsVoid(type))
        {
          info.AddValue("Kind", TypeKind.SystemVoid);
        }
        else if (mdDecoder.IsPrimitive(type))
        {
          info.AddValue("Kind", TypeKind.Primitive);
          info.AddValue("FullName", mdDecoder.FullName(type));
        }
        else if (mdDecoder.Equal(type, mdDecoder.System_Object))
        {
          info.AddValue("Kind", TypeKind.SystemObject);
        }
        else if (mdDecoder.IsFormalTypeParameter(type))
        {
          info.AddValue("Kind", TypeKind.FormalTypeParameter);
          info.AddValue("Index", mdDecoder.NormalizedFormalTypeParameterIndex(type));
          info.AddValue("Type", mdDecoder.FormalTypeParameterDefiningType(type));
        }
        else if (mdDecoder.IsMethodFormalTypeParameter(type))
        {
          info.AddValue("Kind", TypeKind.MethodFormalTypeParameter);
          info.AddValue("Index", mdDecoder.MethodFormalTypeParameterIndex(type));
          info.AddValue("Method", mdDecoder.MethodFormalTypeDefiningMethod(type));
        }
        else if (mdDecoder.IsManagedPointer(type))
        {
          info.AddValue("Kind", TypeKind.ManagedPointer);
          info.AddValue("ElementType", mdDecoder.ElementType(type));
        }
        else if (mdDecoder.IsUnmanagedPointer(type))
        {
          info.AddValue("Kind", TypeKind.UnmanagedPointer);
          info.AddValue("ElementType", mdDecoder.ElementType(type));
        }
        else if (mdDecoder.IsArray(type))
        {
          info.AddValue("Kind", TypeKind.Array);
          info.AddValue("ElementType", mdDecoder.ElementType(type));
          info.AddValue("Rank", mdDecoder.Rank(type));
        }
        else if (mdDecoder.NormalizedIsSpecialized(type, out typeArguments))
        {
          // NormalizedIsSpecialized must return false if Unspecialized would return the same type
          // otherwise we would have cycle and an exception would occur on deserialization
          info.AddValue("Kind", TypeKind.Specialized);
          info.AddValue("ElementType", mdDecoder.Unspecialized(type));
          info.AddArray("TypeArguments", typeArguments.Enumerate().ToArray());
        }
        else
        {
          // Not specialized
          info.AddValue("Kind", TypeKind.Path);
          Typ parentType;
          if (mdDecoder.IsNested(type, out parentType))
          {
            info.AddValue("NestedIn", parentType);
          }
          else
          {
            info.AddValue("NestedIn", default(Typ)); // do not use null, won't work with CCI2
            info.AddValue("Module", this.TranslateModuleName(mdDecoder.DeclaringModuleName(type)));
            info.AddValue("Namespace", mdDecoder.Namespace(type));
          }
          info.AddValue("Name", mdDecoder.Name(type)); // since the type is not specialized, there is no generic parameter in the name

          if (mdDecoder.IsGeneric(type, out typeArguments, false))
            info.AddValue("GenericArity", typeArguments.Count);
          else
            info.AddValue("GenericArity", 0);
        }
      }

      [ContractVerification(false)] // Too hard to prove?
      public override Typ SetObjectData(Typ type, SerializationInfo info)
      {
        var typeKind = info.GetValue<TypeKind>("Kind");

        switch (typeKind)
        {
          case TypeKind.Default:
            return default(Typ);

          case TypeKind.SystemVoid:
            return mdDecoder.System_Void;

          case TypeKind.Primitive:
            if (mdDecoder.TryGetSystemType(info.GetString("FullName"), out type))
              return type;
            throw new SerializationException(String.Format("Unable to construct system type \"{0}\".", info.GetString("FullName")));

          case TypeKind.SystemObject:
            return mdDecoder.System_Object;

          case TypeKind.FormalTypeParameter:
            {
              IIndexable<Typ> formals;
              if (!mdDecoder.IsGeneric(info.GetValue<Typ>("Type"), out formals, true))
              {
                throw new SerializationException(String.Format("Trying to get a formal type parameter of \"{0}\", which is not generic", info.GetValue<Typ>("Type")));
              }
              return formals[info.GetInt32("Index")];
            }

          case TypeKind.MethodFormalTypeParameter:
            {
              IIndexable<Typ> formals;
              if (!mdDecoder.IsGeneric(info.GetValue<Method>("Method"), out formals))
              {
                throw new SerializationException(String.Format("Trying to get a method formal type parameter of \"{0}\", which is not generic", info.GetValue<Method>("Method")));
              }
              return formals[info.GetInt32("Index")];
            }

          case TypeKind.ManagedPointer:
            return mdDecoder.ManagedPointer(info.GetValue<Typ>("ElementType"));

          case TypeKind.UnmanagedPointer:
            return mdDecoder.UnmanagedPointer(info.GetValue<Typ>("ElementType"));

          case TypeKind.Array:
            return mdDecoder.ArrayType(info.GetValue<Typ>("ElementType"), info.GetInt32("Rank"));

          case TypeKind.Specialized:
            return mdDecoder.Specialize(info.GetValue<Typ>("ElementType"), info.GetArray<Typ>("TypeArguments"));

          case TypeKind.Path:
            // Essentially we loop among all the types until we found the one we need

            var parentType = info.GetValue<Typ>("NestedIn");
            var name = info.GetString("Name");

            IEnumerable<Typ> types;
            string @namespace;

            if (parentType == null || parentType.Equals(null)) // Must work with CCI1 AND CCI2, one uses a class, the other one uses a struct, thank you!
            {
              var module = info.GetString("Module");
              Assembly assembly;
              if (!this.TryGetAssembly(module, out assembly))
              {
                throw new SerializationException(String.Format("Unable to find module \"{0}\"", module));
              }
              @namespace = info.GetString("Namespace");
              types = mdDecoder.GetTypes(assembly);
            }
            else
            {
              @namespace = null;
              types = mdDecoder.NestedTypes(parentType);
            }
            var genericArity = info.GetInt32("GenericArity");

            if (this.TryGetType(types, @namespace, name, genericArity, out type))
            {
              return type;
            }
            throw new SerializationException(String.Format("Unable to find type {0}", name));

          default:
            throw new SerializationException("Unknown type kind: " + typeKind);
        }
      }

      // We need to use a special name because the current assembly name may have changed
      private const string SpecialNameForCurrentAssembly = "~#@$!";

      private bool TryGetAssembly(string name, out Assembly assembly)
      {
        if (name == SpecialNameForCurrentAssembly || name == this.currentAssemblyName)
        {
          assembly = this.currentAssembly;
          return true;
        }
        // TODO: optim
        foreach (var refAssembly in mdDecoder.AssemblyReferences(this.currentAssembly))
        {
          if (name == mdDecoder.Name(refAssembly))
          {
            assembly = refAssembly;
            return true;
          }
        }
        assembly = default(Assembly);
        return false;
      }

      private string TranslateModuleName(string name)
      {
        if (name == this.currentAssemblyName)
          return SpecialNameForCurrentAssembly;
        return name;
      }

      private bool TryGetType(IEnumerable<Typ> types, string @namespace, string name, int genericArity, out Typ foundType)
      {
        // TODO: optim
        foreach (var type in types)
        {
          if (@namespace != null && @namespace != mdDecoder.Namespace(type))
            continue;
          var _name = mdDecoder.Name(type);
          if (_name == name)
          {
            IIndexable<Typ> formals;
            int arguments = 0;
            if (mdDecoder.IsGeneric(type, out formals, false))
            {
              arguments = formals.Count;
            }
            if (genericArity != arguments)
              continue;
            foundType = type;
            return true;
          }
        }
        foundType = default(Typ);
        return false;
      }
    }
    #endregion

    #region Serialization surrogate for Method
    sealed class MethodSerializationSurrogate : XSerializationSurrogate<Method, MethodSerializationSurrogate>
    {
      public override void GetObjectData(Method method, SerializationInfo info)
      {
        if (mdDecoder.Equal(method, CurrentMethod))
        {
          info.AddValue("Kind", MethodKind.CurrentMethod);
        }
        else
        {
          IIndexable<Typ> methodTypeArguments;
          Method genericMethod;
          if (mdDecoder.IsSpecialized(method, out genericMethod, out methodTypeArguments))
          {
            // I actually haven't encounter any specialized method different from the current method
            info.AddValue("Kind", MethodKind.Specialized);
            info.AddValue("GenericMethod", genericMethod);
            info.AddArray("MethodTypeArguments", methodTypeArguments.Enumerate().ToArray());
          }
          else
          {
            info.AddValue("Kind", MethodKind.Path);
            info.AddValue("Static", mdDecoder.IsStatic(method));
            info.AddValue("FullName", mdDecoder.FullName(method));
            info.AddValue("DeclaringType", mdDecoder.DeclaringType(method));

            IIndexable<Typ> formals;

            if (mdDecoder.IsGeneric(method, out formals))
              info.AddValue("GenericArity", formals.Count);
            else
              info.AddValue("GenericArity", 0);
          }
        }
      }

      public override Method SetObjectData(Method method, SerializationInfo info)
      {
        var methodKind = info.GetValue<MethodKind>("Kind");

        switch (methodKind)
        {
          case MethodKind.CurrentMethod:
            return CurrentMethod;

          case MethodKind.Specialized:
            return mdDecoder.Specialize(info.GetValue<Method>("GenericMethod"), info.GetArray<Typ>("MethodTypeArguments"));

          case MethodKind.Path:
            var isStatic = info.GetBoolean("Static");
            var fullName = info.GetString("FullName");
            var declaringType = info.GetValue<Typ>("DeclaringType");
            var genericArity = info.GetInt32("GenericArity");

            IIndexable<Typ> formals;
            foreach (var m in mdDecoder.Methods(declaringType))
            {
              if (mdDecoder.IsStatic(m) != isStatic)
                continue;
              if (mdDecoder.FullName(m) != fullName)
                continue;
              if (mdDecoder.IsGeneric(m, out formals))
              {
                if (formals.Count != genericArity)
                  continue;
              }
              else if (genericArity != 0)
                continue;
              return m;
            }
            foreach (var m in this.contractDecoder.ModelMethods(declaringType))
            {
              if (mdDecoder.IsStatic(m) != isStatic)
                continue;
              if (mdDecoder.FullName(m) != fullName)
                continue;
              if (mdDecoder.IsGeneric(m, out formals))
              {
                Contract.Assume(formals != null);
                if (formals.Count != genericArity)
                  continue;
              }
              else if (genericArity != 0)
                continue;
              return m;
            }
            throw new SerializationException(String.Format("Unable to find {0}method \"{1}\"", isStatic ? "static " : "", fullName));

          default:
            throw new SerializationException("Unknown method kind: " + methodKind);
        }
      }
    }
    #endregion

    #region Serialization surrogate for Parameter
    sealed class ParameterSerializationSurrogate : XSerializationSurrogate<Parameter, ParameterSerializationSurrogate>
    {
      public override void GetObjectData(Parameter parameter, SerializationInfo info)
      {
        if (!mdDecoder.Equal(CurrentMethod, mdDecoder.DeclaringMethod(parameter)))
        {
          throw new SerializationException("Trying to serialize a parameter of a method different from the current method.");
        }
        info.AddValue("ArgumentIndex", mdDecoder.ArgumentIndex(parameter));
        info.AddValue("Type", mdDecoder.ParameterType(parameter));
      }

      public override Parameter SetObjectData(Parameter parameter, SerializationInfo info)
      {
        var parameterType = info.GetValue<Typ>("Type");
        var argumentIndex = info.GetInt32("ArgumentIndex");

        Contract.Assume(argumentIndex >= 0);

        Parameter candidate;
        if (!mdDecoder.IsStatic(CurrentMethod))
        {
          if (argumentIndex == 0)
          {
            candidate = mdDecoder.This(CurrentMethod);
            if (mdDecoder.Equal(mdDecoder.ParameterType(candidate), parameterType))
            {
                return candidate;
            }
            throw new SerializationException(string.Format("Parameter found, but wrong type. Expected {0} but found {1}", parameterType, mdDecoder.ParameterType(candidate)));
          }
          argumentIndex--;
        }
        var pars = mdDecoder.Parameters(CurrentMethod);
        if (pars.Count > argumentIndex)
        {
            candidate = pars[argumentIndex];
            if (mdDecoder.Equal(mdDecoder.ParameterType(candidate), parameterType))
            {
                return candidate;
            }
            throw new SerializationException(string.Format("Parameter found, but wrong type. Expected {0} but found {1}", parameterType, mdDecoder.ParameterType(candidate)));
        }
        throw new SerializationException("Parameter not found, out of bounds");
      }
    }
    #endregion

    #region Serialization surrogate for Local
    sealed class LocalSerializationSurrogate : XSerializationSurrogate<Local, LocalSerializationSurrogate>
    {
        public override void GetObjectData(Local local, SerializationInfo info)
        {
            bool found = false;
            foreach (var l in mdDecoder.Locals(CurrentMethod).Enumerate())
            {
                if (l.Equals(local)) 
                {
                    found = true;
                    break;
                }

            }
            // TODO: necessary?
            if (!found) 
            {
                throw new SerializationException(
                    String.Format("Could not find local {0}; either trying to serialize a local of a method different from the current method, or the current method no longer contains this local.",
                                  local));
            }
            info.AddValue("Name", mdDecoder.Name(local));
            info.AddValue("LocalType", mdDecoder.LocalType(local));
        }

        public override Local SetObjectData(Local local, SerializationInfo info)
        {
            var localType = info.GetValue<Typ>("LocalType");
            var name = info.GetString("Name");

            foreach (var l in mdDecoder.Locals(CurrentMethod).Enumerate())
            {
                if (mdDecoder.Name(l) == name && mdDecoder.Equal(mdDecoder.LocalType(l), localType))
                { 
                    return l;
                }
            }
            throw new SerializationException(String.Format("Unable to find local \"{0}\" of type \"{1}\"", name, localType));
        }
    }
    #endregion

    #region Serialization surrogate for Field
    sealed class FieldSerializationSurrogate : XSerializationSurrogate<Field, FieldSerializationSurrogate>
    {
      public override void GetObjectData(Field field, SerializationInfo info)
      {
        info.AddValue("Name", mdDecoder.Name(field));
        info.AddValue("DeclaringType", mdDecoder.DeclaringType(field));
      }

      public override Field SetObjectData(Field field, SerializationInfo info)
      {
        var declaringType = info.GetValue<Typ>("DeclaringType");
        var name = info.GetString("Name");

        // optim...
        foreach (var f in mdDecoder.Fields(declaringType))
        {
          if (mdDecoder.Name(f) == name)
          {
            return f;
          }
        }
        foreach (var f in contractDecoder.ModelFields(declaringType))
        {
          if (mdDecoder.Name(f) == name)
          {
            return f;
          }
        }
        throw new SerializationException(String.Format("Unable to find field \"{0}\" of type \"{1}\"", name, declaringType));
      }
    }
    #endregion

    #region Serialize MetaDataDecoders as singletons
    sealed class DecodeMetaDataSerializationSurrogate : ISerializationSurrogate
    {
      private readonly IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Typ, Attribute, Assembly> mdDecoder;

      public DecodeMetaDataSerializationSurrogate(
        IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Typ, Attribute, Assembly> mdDecoder)
      {
        this.mdDecoder = mdDecoder;
      }

      public void GetObjectData(Object obj, SerializationInfo info, StreamingContext context)
      {
        // Global invariant: there exists only one metadata decoder

        if (obj != this.mdDecoder)
        {
          throw new SerializationException("Trying to serialize another IDecodeMetaData");
        }
      }
      public Object SetObjectData(Object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
      {
        return this.mdDecoder;
      }
    }
    #endregion

    #region Serialize ConditionLists
    sealed class ConditionListSerializationSurrogate : XSerializationSurrogate<ConditionList<Method>, ConditionListSerializationSurrogate>
    {
      public override void GetObjectData(ConditionList<Method> list, SerializationInfo info)
      {
        var values = list.ToArray();
        info.AddValue("count", values.Length);
        for (int i = 0; i < values.Length; i++)
        {
          var elem = values[i];
          info.AddValue(i.ToString()+"E", elem.One);
          info.AddValue(i.ToString() + "M", elem.Two);
        }
      }

      public override ConditionList<Method> SetObjectData(ConditionList<Method> list, SerializationInfo info)
      {
        var count = info.GetValue<int>("count");
        var result = new List<Pair<BoxedExpression, Method>>();

        for (int i = 0; i < count; i++)
        {
          try
          {
            var be = info.GetValue<BoxedExpression>(i.ToString()+"E");
            var m  = info.GetValue<Method>(i.ToString() + "M");
            result.Add(new Pair<BoxedExpression,Method>(be, m));
          }
          catch
          {
            if (Trace)
            {
              Console.WriteLine("[cache] skipping failed deserialization of an inferred condition list element");
            }
            // swallow deserialization exceptions
          }

        }
        return new ConditionList<Method>(result);
      }
    }
    #endregion

  }

  /// <summary>
  /// Type constructors for serialization/deserialization
  /// </summary>
  [Serializable]
  enum TypeKind
  {
    Default,
    SystemVoid,
    SystemObject,
    Primitive,
    FormalTypeParameter,
    MethodFormalTypeParameter,
    ManagedPointer,
    UnmanagedPointer,
    Array,
    Specialized,
    Path  // Non-generic type with full namespace
  }

  [Serializable]
  enum MethodKind
  {
    CurrentMethod,
    Specialized,
    Path
  }

  #region Extensions
  static class SurrogateSelectorExtensions
  {
    public static void AddSurrogate<X, XSS, Local, Parameter, Method, Field, Property, Event, Typ, Attribute, Assembly>(
      this SurrogateSelector selector,
      StreamingContext context,
      Serializers<Local, Parameter, Method, Field, Property, Event, Typ, Attribute, Assembly>.XSerializationSurrogate<X, XSS> serializationSurrogate)
      where XSS : Serializers<Local, Parameter, Method, Field, Property, Event, Typ, Attribute, Assembly>.XSerializationSurrogate<X, XSS>, new()
    {
      selector.AddSurrogate(typeof(X), context, serializationSurrogate);
    }

    public static void AddSurrogate<X, XSS, Local, Parameter, Method, Field, Property, Event, Typ, Attribute, Assembly>(
      this Serializers<Local, Parameter, Method, Field, Property, Event, Typ, Attribute, Assembly>.SimpleSubTypeSurrogateSelector selector,
      StreamingContext context,
      Serializers<Local, Parameter, Method, Field, Property, Event, Typ, Attribute, Assembly>.XSerializationSurrogate<X, XSS> serializationSurrogate)
      where XSS : Serializers<Local, Parameter, Method, Field, Property, Event, Typ, Attribute, Assembly>.XSerializationSurrogate<X, XSS>, new()
    {
      selector.AddSurrogate(typeof(X), context, serializationSurrogate);
    }
  }

  internal static class SerializationInfoExtensions
  {
    static public T GetValue<T>(this SerializationInfo info, string name) 
    {
      Contract.Requires(name != null);
      return (T)info.GetValue(name, typeof(T)); 
    }

    // HACK: because arrays are not deserialized in the correct order

    static public void AddArray<T>(this SerializationInfo info, string name, T[] array)
    {
      if (array == null)
      {
        info.AddValue(name + ".Length", -1);
      }
      else
      {
        info.AddValue(name + ".Length", array.Length);
        for (var i = 0; i < array.Length; i++)
        {
          info.AddValue(name + "." + i, array[i]);
        }
      }
    }

    static public T[] GetArray<T>(this SerializationInfo info, string name)
    {
      var length = info.GetInt32(name + ".Length");
      if (length < 0)
      {
        return null;
      }
      else
      {
        var array = new T[length];
        for (int i = 0; i < length; i++)
        {
          array[i] = info.GetValue<T>(name + "." + i);
        }
        return array;
      }
    }
  }
  #endregion
}
