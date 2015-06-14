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
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.CodeAnalysis
{
  public class FieldsDB<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly>
  {
    readonly private IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder;
    readonly private List<Tuple<Type, Method, IEnumerable<Field>>> db;
    readonly private List<Type> typesForWhichWeAlreadyEmittedASuggestion;
    readonly private Dictionary<Method, IEnumerable<Method>> setters;
    private IEnumerable<Field> fieldsOnlyModifiedInConstructors;

    public FieldsDB(IDecodeMetaData<Local, Parameter, Method, Field, Property, Event, Type, Attribute, Assembly> mdDecoder)
    {
      Contract.Requires(mdDecoder != null);

      this.mdDecoder = mdDecoder;
      this.db = new List<Tuple<Type,Method,IEnumerable<Field>>>();
      this.typesForWhichWeAlreadyEmittedASuggestion = new List<Type>();
      this.setters = new Dictionary<Method, IEnumerable<Method>>();
    }

    public void NotifyPossiblyModifiedFields(Method m, IEnumerable<Field> fields)
    {
      var type = this.mdDecoder.DeclaringType(m);
      db.Add(new Tuple<Type, Method, IEnumerable<Field>>(type, m, fields));
    }

    public void NotifySettersCalledFromTheMethod(Method m, IEnumerable<Method> methods)
    {
      this.setters[m] = methods;
    }

    public bool IsCalledInANonConstructor(Method m)
    {
      return this.setters.Where(pair => !mdDecoder.IsConstructor(pair.Key) && pair.Value.Contains(m)).Any();
    }

    public void EmitFieldsAssignedOnlyInConstructors(APC pc, Method m, IOutput output)
    {
      var type = this.mdDecoder.DeclaringType(m);
      if (!this.typesForWhichWeAlreadyEmittedASuggestion.Contains(type))
      {
        this.typesForWhichWeAlreadyEmittedASuggestion.Add(type);

        Emit(pc, ReadOnlyFieldsCandidates().Where(f => type.Equals(this.mdDecoder.DeclaringType(f))), output);
      }
    }

    public bool IsACandidateReadonly(Field f)
    {
      return this.ReadOnlyFieldsCandidates().Contains(f);
    }

    private  IEnumerable<Field> ReadOnlyFieldsCandidates()
    {
      if (this.fieldsOnlyModifiedInConstructors == null)
      {
        var constructors = this.db.Where(tuple => this.mdDecoder.IsConstructor(tuple.Item2));
        var methods = this.db.Where(tuple => !this.mdDecoder.IsConstructor(tuple.Item2));

        var fieldsModifiedInConstructors = constructors.SelectMany(tuple => tuple.Item3).Where(f => !this.mdDecoder.IsReadonly(f));
        var fieldsModifiedInMethods = methods.SelectMany(tuple => tuple.Item3);
        fieldsOnlyModifiedInConstructors = fieldsModifiedInConstructors.Except(fieldsModifiedInMethods);
      }
      return fieldsOnlyModifiedInConstructors;
    }

    private void Emit(APC pc, IEnumerable<Field> fieldsOnlyModifiedInConstructors, IOutput output)
    {
      Contract.Requires(fieldsOnlyModifiedInConstructors != null);
      Contract.Requires(output != null);

      var md = this.mdDecoder;

      // Suggest only for fields that we can see in the analysis
      foreach (var f in fieldsOnlyModifiedInConstructors.Where(field => !md.IsVisibleOutsideAssembly(field)))
      {
        var declaringType = md.DeclaringType(f);

        var extraInfo = new ClousotSuggestion.ExtraSuggestionInfo()
        {
          CalleeDocumentId = md.DocumentationId(f),
          TypeDocumentId = md.DocumentationId(declaringType)
        };

        var suggestion = string.Format("Field {0}, declared in type {1}, is only updated in constructors. Consider marking it as readonly", 
          md.Name(f),
          md.Name(declaringType));

        output.Suggestion(ClousotSuggestion.Kind.ReadonlyField, ClousotSuggestion.Kind.ReadonlyField.Message(), pc, suggestion, null, extraInfo);
      }
    }
  }
}
