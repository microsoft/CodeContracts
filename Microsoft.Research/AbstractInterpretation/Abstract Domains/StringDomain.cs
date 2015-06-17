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
using System.Text;
using Microsoft.Research.AbstractDomains.Expressions;
using System.Diagnostics;
using Microsoft.Research.DataStructures;

namespace Microsoft.Research.AbstractDomains
{
  public interface IStringAbstraction : IAbstractDomain
  {    
    IStringAbstraction ConcatWith(IStringAbstraction what);
    IStringAbstraction Insert(int where, IStringAbstraction what);
    IStringAbstraction Trim();

    FlatAbstractDomain<int> CompareTo(IStringAbstraction who);
    FlatAbstractDomain<bool> Contains(IStringAbstraction what);
    FlatAbstractDomain<bool> StartsWith(IStringAbstraction what);
  }

  /// <summary>
  /// A simple abstract domain abstracting (environments of) strings
  /// </summary>
  /// <typeparam name="Expression"></typeparam>
  public class SimpleStringAbstractDomain<Variable, Expression> :
    FunctionalAbstractDomain<SimpleStringAbstractDomain<Variable, Expression>, Variable, IStringAbstraction>,
    IStringAbstractDomain<IStringAbstraction, Variable, Expression>
  {
    #region Static
    private static readonly IStringAbstraction topString = new StringAbstraction(); // We cache it, as we will use very often top (Thread-safe)
    #endregion

    #region Private state
    IExpressionDecoder<Variable, Expression>/*!*/ decoder;
    #endregion

    #region Constructor

    public SimpleStringAbstractDomain(IExpressionDecoder<Variable, Expression>/*!*/ decoder)
    {
      this.decoder = decoder;
    }

    private SimpleStringAbstractDomain(SimpleStringAbstractDomain<Variable, Expression>/*!*/ source)
    {
      this.decoder = source.decoder;
      foreach (var x in source.Keys)
      {
        var cloned = source[x].Clone() as IStringAbstraction;
        this[x] = cloned;
      }
    }
    #endregion

    #region From FunctionalAbstractDomain
    public override object Clone()
    {
      return new SimpleStringAbstractDomain<Variable, Expression>(this); 
    }

    protected override SimpleStringAbstractDomain<Variable, Expression> Factory()
    {
      return new SimpleStringAbstractDomain<Variable, Expression>(this.decoder);
    }
    #endregion

    #region IPureExpressionAssignmentsWithForward<Expression> Members

    public void Assign(Expression x, Expression exp)
    {
      this.State = AbstractState.Normal;
      this[this.decoder.UnderlyingVariable(x)] = Eval(exp);
    }

    #endregion

    #region IPureExpressionAssignments<Expression> Members

    public List<Variable> Variables
    {
      get 
      {
        return new List<Variable>(this.Keys);
      }
    }

    public void AddVariable(Variable var)
    {
      // do nothing
    }

    public void ProjectVariable(Variable var)
    {
      this.RemoveVariable(var);
    }

    public void RemoveVariable(Variable var)
    {
      this.RemoveElement(var);
    }

    public void RenameVariable(Variable OldName, Variable NewName)
    {
      this[NewName] = this[OldName];
      this.RemoveVariable(OldName);
    }

    #endregion

    #region IPureExpressionTest<Expression> Members

    public IAbstractDomainForEnvironments<Variable, Expression>/*!*/ TestTrue(Expression/*!*/ guard)
    {
      return this;
    }

    public IAbstractDomainForEnvironments<Variable, Expression>/*!*/ TestFalse(Expression/*!*/ guard)
    {
      return this;
    }

    public FlatAbstractDomain<bool> CheckIfHolds(Expression/*!*/ exp)
    {
      return  new FlatAbstractDomain<bool>(true).Top;
    }

    void IPureExpressionTest<Variable, Expression>.AssumeDomainSpecificFact(DomainSpecificFact fact)
    {
      this.AssumeDomainSpecificFact(fact);
    }
    #endregion

    #region IAssignInParallel<Expression> Members

    public void AssignInParallel(Dictionary<Variable, FList<Variable>> sourcesToTargets, Converter<Variable, Expression> convert)
    {

      this.State = AbstractState.Normal;

      if (sourcesToTargets.Count == 0)
      {
        // do nothing...
      }
      else
      {
        // Evaluate the values in the pre-state
        var values = new Dictionary<Variable, IStringAbstraction>();

        foreach (var exp in sourcesToTargets.Keys)
        {
          values[exp] = Eval(convert(exp));
        }

        // Update the values
        foreach (var exp in sourcesToTargets.Keys)
        {
          var value = values[exp];   // The new value in the pre-state

          foreach (var target in sourcesToTargets[exp].GetEnumerable())
          {
            if (!value.IsTop)
            {
              this[target] = value;
            }
            else
            {
              if (this.ContainsKey(target))
              {
                this.RemoveVariable(target);
              }
            }
          }

        }
      }
    }

    #endregion

    #region IStringAbstractDomain<IStringAbstraction,Expression> Members

    public void Concat(Expression/*!*/ target, Expression/*!*/ left, Expression/*!*/ right)
    {
      var leftVal = Eval(left);
      var rightVal = Eval(right);

      this[this.decoder.UnderlyingVariable(target)] = leftVal.ConcatWith(rightVal);
    }

    public void Insert(Expression/*!*/ target, Expression/*!*/ which, Expression/*!*/ where, Expression/*!*/ what)
    {
      if (this.decoder.IsConstant(where))
      {
        Int32 index;

        if (this.decoder.TryValueOf<Int32>(where, ExpressionType.Int32, out index))
        {
          var left = Eval(which);
          var right = Eval(what);

          this[this.decoder.UnderlyingVariable(target)] = left.Insert(index, right);
        }
        else
        {
          this.RemoveElement(this.decoder.UnderlyingVariable(target));
        }
      }
      else
      {
        this.RemoveElement(this.decoder.UnderlyingVariable(target));
      }
    }

    public void Trim(Expression/*!*/ who, Expression/*!*/ what)
    {
      var value = Eval(what);
      this[this.decoder.UnderlyingVariable(who)] = value.Trim();
    }

    public FlatAbstractDomain<int> CompareTo(Expression/*!*/ left, Expression/*!*/ right)
    {
      var leftVal = Eval(left);
      var rightVal = Eval(right);

      return leftVal.CompareTo(rightVal);
    }

    public FlatAbstractDomain<bool> Contains(Expression/*!*/ who, Expression/*!*/ what)
    {
      var leftVal = Eval(who);
      var rightVal = Eval(what);

      return leftVal.Contains(rightVal);
    }

    public FlatAbstractDomain<bool> StartsWith(Expression/*!*/ who, Expression/*!*/ what)
    {
      var leftVal = Eval(who);
      var rightVal = Eval(what);
      return leftVal.StartsWith(rightVal);
    }

    #endregion

    #region Eval
    private IStringAbstraction Eval(Expression/*!*/ exp)
    {
      IStringAbstraction result;
      switch (this.decoder.OperatorFor(exp))
      {
        case ExpressionOperator.Constant:
          result = EvalConstant(exp);
          break;

        case ExpressionOperator.Variable:
          var v = this.decoder.UnderlyingVariable(exp);
          result = EvalVariable(v);
          break;

        default:
          result = topString;
          break;
      }

      return result;
    }

    private IStringAbstraction EvalConstant(Expression/*!*/ exp)
    //^ this.decoder.IsConstant(exp);
    {
      IStringAbstraction result;
      string val;

      if (this.decoder.TryValueOf<string>(exp, ExpressionType.String, out val))
      {
        result = new StringAbstraction(val);
      }
      else
      {
        result = topString;
      }

      return result;
    }

    private IStringAbstraction EvalVariable(Variable/*!*/ var)
    {
      IStringAbstraction result;

      if (this.ContainsKey(var))
      {
        result = this[var];
      }
      else
      {
        result = topString;
      }

      return result;
    }

    #endregion

    #region ToString
    public override string ToString()
    {
      string result;

      if (this.IsBottom)
      {
        result = "_|_";
      }
      else if (this.IsTop)
      {
        result = "Top";
      }
      else
      {
        var tempStr = new StringBuilder();

        foreach (var x in this.Keys)
        {
          string xAsString = this.decoder != null ? this.decoder.NameOf(x) : x.ToString();
          tempStr.Append(xAsString + ": " + this[x] + ", ");
        }

        result = tempStr.ToString();
        int indexOfLastComma = result.LastIndexOf(",");
        if (indexOfLastComma > 0)
        {
          result = result.Remove(indexOfLastComma);
        }
      }

      return result;
    }

    public string ToString(Expression exp)
    {
      if (this.decoder != null)
      {
        return ExpressionPrinter.ToString(exp, this.decoder);
      }
      else
      {
        return "< missing expression decoder >";
      }
    }
    #endregion

    protected override string ToLogicalFormula(Variable d, IStringAbstraction c)
    {
      return null;
    }

    protected override T To<T>(Variable d, IStringAbstraction c, IFactory<T> factory)
    {
      return factory.Constant(true);
    }
  }

  /// <summary>
  /// The class abstracting strings
  /// </summary>
  internal class StringAbstraction : IStringAbstraction
  {
    enum ContentType {String, Prefix, SetOfCharacters, Top, Bottom}

    private readonly ContentType content;
    private readonly string fullstring = null;
    private readonly string prefix = null;
    private readonly Set<SimpleCharacterAbstraction> characters = null;

    private static readonly StringAbstraction CachedTop = new StringAbstraction();
    private static readonly StringAbstraction CachedBottom = new StringAbstraction(ContentType.Bottom);

    #region Constructors

    private StringAbstraction(ContentType content)
    {
      this.content = content;
    }

    /// <summary>
    /// The top element
    /// </summary>
    public StringAbstraction()
      : this(ContentType.Top)
    { }

    /// <summary>
    /// The abstract element containing just <code>s</code>
    /// </summary>
    public StringAbstraction(string/*!*/ s)
    {
      this.content = ContentType.String;
      this.fullstring = s;
    }

    /// <summary>
    /// 
    /// </summary>
    public StringAbstraction(Set<SimpleCharacterAbstraction>/*!*/ characters)
    {
      this.content = ContentType.SetOfCharacters;
      this.characters = characters;
    }

    /// <summary>
    /// Builds a prefix
    /// </summary>
    /// <param name="b">It is ignored</param>
    private StringAbstraction(string/*!*/ prefix, bool b)
    {
      this.content = ContentType.Prefix;
      this.prefix = prefix;
    }

    private StringAbstraction(StringAbstraction/*!*/ a)
    {
      this.content = a.content;
      this.fullstring = a.fullstring;
      this.characters = a.characters;
      this.prefix = a.prefix;
    }
    #endregion

    public T To<T>(IFactory<T> factory)
    {
      return factory.Constant(true);
    }

    #region IStringAbstraction Members

    /// <summary>
    /// Do the concatenation
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    public IStringAbstraction ConcatWith(IStringAbstraction/*!*/ param)
    {
      IStringAbstraction result;
      if (this.IsTop || param.IsTop)
      {
        result = CachedTop;
      }
      else
      {
        StringAbstraction what = param as StringAbstraction; //^ assert what != null;
        Debug.Assert(what != null);

        if (this.content == what.content)
        {
          switch (this.content)
          {
            #region All the cases
            case ContentType.Prefix:
              result = this;          // prefix1 \cdot prefix2 = prefix1
              break;

            case ContentType.SetOfCharacters:
              result = new StringAbstraction(this.characters.Union(what.characters)); // { bla1 bla12 } \code {bla2 bla22} = {bla1, bla12, bla2, bla22} 
              break;

            case ContentType.String:  // Simple concatenation
              result = new StringAbstraction(this.fullstring + what.fullstring);
              break;

            default:
              throw new AbstractInterpretationException("Impossible case?");
            #endregion
          }
        }
        else
        {
          switch (this.content)
          {
            #region All the cases
            case ContentType.Prefix:
              if (what.content == ContentType.SetOfCharacters)
              {
                result = this;       // prefix \cdot {bla bla} = prefix
              }
              else if (what.content == ContentType.String)
              {                     // prefix \cdot string = prefix
                result = this;
              }
              else
              {
                goto default;
              }
              break;

            case ContentType.SetOfCharacters:
              if (what.content == ContentType.Prefix)
              {
                result = (StringAbstraction) this.Top;    
              }
              else if (what.content == ContentType.String)
              {
                result = new StringAbstraction(this.characters.Union(AlphaToSetOfCharacters(what.fullstring)));
              }
              else
              {
                goto default;
              }
              break;

            case ContentType.String:
              if (what.content == ContentType.SetOfCharacters)
              {
                result = new StringAbstraction(AlphaToSetOfCharacters(this.fullstring).Union(what.characters));
              }
              else if (what.content == ContentType.Prefix)
              {
                result = new StringAbstraction(this.fullstring + what.prefix, true);
              }
              else
              {
                goto default;
              }
              break;

            default:
              throw new AbstractInterpretationException("Impossible case?");
            #endregion
          }
        }
      }
      return result;
    }

    public IStringAbstraction Insert(int where, IStringAbstraction/*!*/ param)
        //^ requires where >= 0;
    {
      Debug.Assert(where >= 0);
      IStringAbstraction result;
      if (this.IsTop || param.IsTop)
      {
        result = CachedTop;
      }
      else
      {
        StringAbstraction what = param as StringAbstraction; //^ assert what != null;
        Debug.Assert(what != null);

        if (this.content == what.content)
        {
          switch (this.content)
          {
            #region All the cases
            case ContentType.Prefix:
              if (where < this.prefix.Length)
              {
                string tmp = this.prefix.Substring(0, where);
                result = new StringAbstraction(tmp + prefix, true);
              }
              else
              {
                result = CachedTop;
              }
              break;

            case ContentType.SetOfCharacters:
              result = new StringAbstraction(this.characters.Union(what.characters));
              break;

            case ContentType.String:
              result = new StringAbstraction(this.fullstring.Insert(where, what.fullstring));
              break;

            default:
              throw new AbstractInterpretationException("Impossible case?");
            #endregion
          }
        }
        else
        {
          switch (this.content)
          {
            #region All the cases
            case ContentType.Prefix:
              if (what.content == ContentType.SetOfCharacters)
              {
                result = CachedTop;
              }
              else if (what.content == ContentType.String)
              {
                if (where < this.fullstring.Length)
                {
                  result = new StringAbstraction(this.prefix.Insert(where, what.fullstring));
                }
                else
                {
                  result = this;
                }
              }
              else
              {
                goto default;
              }
              break;

            case ContentType.SetOfCharacters:
              if (what.content == ContentType.Prefix)
              {
                result = CachedTop;
              }
              else if (what.content == ContentType.String)
              {
                result = new StringAbstraction(this.characters.Union(AlphaToSetOfCharacters(what.fullstring)));
              }
              else
              {
                goto default;
              }
              break;

            case ContentType.String:
              if (what.content == ContentType.SetOfCharacters)
              {
                result = new StringAbstraction(AlphaToSetOfCharacters(this.fullstring).Union(what.characters));
              }
              else if (what.content == ContentType.Prefix)
              {
                if (where < this.fullstring.Length)
                {
                  string tmp = this.fullstring.Substring(0, where);
                  result = new StringAbstraction(tmp + what.prefix, true);
                }
                else
                {
                  result = this;
                }
              }
              else
              {
                goto default;
              }
              break;

            default:
              throw new AbstractInterpretationException("Impossible case?");
            #endregion
          }
        }
      }
      return result;
    }

    public IStringAbstraction Trim()
    {
      StringAbstraction result;
      switch (this.content)
      {
        case ContentType.Bottom:
          result = this;
          break;

        case ContentType.Prefix:
          result = new StringAbstraction(this.prefix.Trim(), true);
          break;

        case ContentType.SetOfCharacters:
          Set<SimpleCharacterAbstraction> tmp = RemoveWhiteSpaces(this.characters);
          result = new StringAbstraction(tmp);
          break;

        case ContentType.String:
          result = new StringAbstraction(this.fullstring.Trim());
          break;

        case ContentType.Top:
          result = this;
          break;

        default:
          throw new AbstractInterpretationException("Impossible case?");
      }
      return result;
    }

    public FlatAbstractDomain<int>/*!*/ CompareTo(IStringAbstraction/*!*/ param)
    {
      FlatAbstractDomain<int> result;
      FlatAbstractDomain<int> top = (FlatAbstractDomain<int>)new FlatAbstractDomain<int>(1).Top;

      if (this.IsTop || param.IsTop)
      {
        return top;
      }
      else
      {
        StringAbstraction who = param as StringAbstraction; //^ assert who != null;
        Debug.Assert(who != null);

        if (this.content == ContentType.String && who.content == ContentType.String)
        {
          result = new FlatAbstractDomain<int>(this.fullstring.CompareTo(who.fullstring));
        }
        else
        {
          result = top;
        }
      }

      return result;
    }

    public FlatAbstractDomain<bool>/*!*/ Contains(IStringAbstraction/*!*/ param)
    {
      FlatAbstractDomain<bool> result;
      FlatAbstractDomain<bool> top =  new FlatAbstractDomain<bool>(false).Top;

      if (this.IsTop || param.IsTop)
      {
        result = top;
      }
      else
      {
        StringAbstraction what = param as StringAbstraction; //^ assert what != null;
        Debug.Assert(what != null);

        if (this.content == what.content)
        {
          #region All the cases...
          switch (this.content)
          {
            case ContentType.Prefix:
              result = top;
              break;
            
            case ContentType.SetOfCharacters:
              result = top;
              break;

            case ContentType.String:
              result = new FlatAbstractDomain<bool>(this.fullstring.Contains(what.fullstring));
              break;

            default:
              throw new AbstractInterpretationException("Impossible case?");
          }
          #endregion
        }
        else
        {
          switch (this.content)
          {
            #region All the cases
            case ContentType.Prefix:
              if (what.content == ContentType.String && this.prefix.Contains(what.fullstring))
              {
                result = new FlatAbstractDomain<bool>(true);
              }
              else
              {
                result = top;
              }
              break;

            case ContentType.SetOfCharacters:
            case ContentType.String:
              result = top;
              break;

            default:
              throw new AbstractInterpretationException("Impossible case?");
            #endregion
          }
        }
      }
      return result;
    }

    public FlatAbstractDomain<bool>/*!*/ StartsWith(IStringAbstraction/*!*/ param)
    {
      FlatAbstractDomain<bool> result;
      FlatAbstractDomain<bool> top =  new FlatAbstractDomain<bool>(false).Top;

      if (this.IsTop || param.IsTop)
      {
        result = top;
      }
      else
      {
        StringAbstraction what = param as StringAbstraction; //^ assert what != null;
        Debug.Assert(what != null);

        if (this.content == ContentType.String && what.content == ContentType.String)
        {
          result = new FlatAbstractDomain<bool>(this.fullstring.StartsWith(what.fullstring)); 
        }
        else if (this.content == ContentType.Prefix && what.content == ContentType.String)
        {
          result = new FlatAbstractDomain<bool>(this.prefix.StartsWith(what.fullstring));
        }
        else
        {
          result = top;
        }
      }
      return result;
    }

    #region IAbstractDomain Members

    public bool IsBottom
    {
      get 
      {
        return this.content == ContentType.Bottom;
      }
    }

    public bool IsTop
    {
      get 
      {
        return this.content == ContentType.Top;
      }
    }

    public bool LessEqual(IAbstractDomain/*!*/ a)
    {
      bool tryResult;
      if(AbstractDomainsHelper.TryTrivialLessEqual(this, a, out tryResult))
      {
        return tryResult;
      }

      StringAbstraction right = a as StringAbstraction;
      //^ assert right != null;
      Debug.Assert(right != null, "I was expecting a " + this.GetType().ToString());

      bool result;

      if (this.content == right.content)
      {
        switch (this.content)
        {
          #region All the cases
          case ContentType.Prefix:
            result = this.prefix.StartsWith(right.prefix);    // it is larger if it is a substring
            break;

          case ContentType.SetOfCharacters:
            result = this.characters.IsSubset(right.characters);  // it is larger if it is a subset
            break;

          case ContentType.String:
            result = this.fullstring.CompareTo(right.fullstring) == 0;    // must be the same string
            break;

          case ContentType.Bottom:
          case ContentType.Top:
          default:
            throw new AbstractInterpretationException("Unexpected case : " + this.content);
          #endregion
        }
      }
      else
      {
        switch (this.content)
        {
          #region All the cases
          case ContentType.Prefix:
            if (right.content == ContentType.SetOfCharacters)
            {
              result = false;
            }
            else if (right.content == ContentType.String)
            {
              result = false;
            }
            else
            {
              goto default;
            }
            break;

          case ContentType.SetOfCharacters:
            if (right.content == ContentType.Prefix)
            {
              result = false;
            }
            else if (right.content == ContentType.String)
            {
              result = false;
            }
            else
            {
              goto default;
            }
            break;

          case ContentType.String:
            if (right.content == ContentType.Prefix)
            {
              result = this.fullstring.StartsWith(right.prefix);
            }
            else if (right.content == ContentType.SetOfCharacters)
            {
              Set<SimpleCharacterAbstraction> tmp = AlphaToSetOfCharacters(this.fullstring);
              result = tmp.IsSubset(right.characters);
            }
            else
            {
              goto default;
            }
            break;

          case ContentType.Bottom:
          case ContentType.Top:
          default:
            throw new AbstractInterpretationException("Unexpected case : " + this.content);
          #endregion
        }
      }

      return result;
    }

    public IAbstractDomain/*!*/ Bottom
    {
      get 
      {
        return CachedBottom;
      }
    }

    public IAbstractDomain/*!*/ Top
    {
      get 
      {
        return CachedTop;
      }
    }

    public IAbstractDomain/*!*/ Join(IAbstractDomain/*!*/ a)
    {
      IAbstractDomain tryResult;
      if(AbstractDomainsHelper.TryTrivialJoin(this, a, out tryResult))
      {
        return tryResult;
      }

      StringAbstraction right = a as StringAbstraction;
      //^ assert right != null;
      Debug.Assert(right != null, "I was expecting a " + this.GetType().ToString());

      IAbstractDomain/*!*/ result;

      if (this.content == right.content)
      {
        switch (this.content)
        {
          #region All the cases
          case ContentType.Prefix:
            result = HelperForPrefixJoin(this.prefix, right.prefix);
            break;

          case ContentType.SetOfCharacters:
            result = HelperForSetOfCharactersJoin(this.characters, right.characters);
            break;

          case ContentType.String:
            result = HelperForStringJoin(this.fullstring, right.fullstring);
            break;

          case ContentType.Top:
          case ContentType.Bottom:
          default:
            // Impossible cases as they have already been checked before
            throw new AbstractInterpretationException("Impossible case?");   
          #endregion
        }
      }
      else
      {
        switch (this.content)
        {
          #region All the cases...
          case ContentType.Prefix:
            if (right.content == ContentType.SetOfCharacters)
            {
              result = this.Top;    
            }
            else if (right.content == ContentType.String)
            {
              result = HelperForStringPrefixJoin(right.fullstring, this.prefix);
            }
            else
            {
              goto default;
            }
            break;

          case ContentType.SetOfCharacters:
            if (right.content == ContentType.Prefix)
            {
              result = this.Top;
            }
            else if (right.content == ContentType.String)
            {
              result = HelperForSetOfCharactersJoin(this.characters, AlphaToSetOfCharacters(right.fullstring));
            }
            else
            {
              goto default;
            }
            break;

          case ContentType.String:
            if(right.content == ContentType.Prefix)
            {
             result = HelperForStringPrefixJoin(this.fullstring, right.prefix);
            }
            else if (right.content == ContentType.SetOfCharacters)
            {
              result = HelperForSetOfCharactersJoin(AlphaToSetOfCharacters(this.fullstring), right.characters);
            }
            else
            {
              goto default;
            }
            break;

          case ContentType.Top:
          case ContentType.Bottom:
          default:
            // Impossible cases as they have already been checked before
            throw new AbstractInterpretationException("Impossible case?");  
          #endregion
        }
      }

      return result;
    }

    /// <summary>
    /// The meet works only on the same contents
    /// </summary>
    public IAbstractDomain/*!*/ Meet(IAbstractDomain/*!*/ a)
    {
      IAbstractDomain tryResult;
      if(AbstractDomainsHelper.TryTrivialMeet(this, a, out tryResult))
      {
        return tryResult;
      }

      StringAbstraction right = a as StringAbstraction;
      //^ assert right != null;
      Debug.Assert(right != null, "I was expecting a " + this.GetType().ToString());

      IAbstractDomain/*!*/ result;

      if (this.content == right.content)
      {
        switch (this.content)
        {
          #region All the cases ...
          case ContentType.Prefix:
            if(this.prefix.CompareTo(right.prefix) == 0)
            {
              result = this;
            }
            else
            {
              result = this.Bottom;
            }
            break;

          case ContentType.SetOfCharacters:
            var intersection = this.characters.Intersection(right.characters);
            if (intersection.IsEmpty)
            {
              result = this.Bottom;
            }
            else
            {
              result = new StringAbstraction(intersection);
            }
            break;

          case ContentType.String:
            if (this.fullstring.CompareTo(right.fullstring) == 0)
            {
              result = this;
            }
            else
            {
              result = this.Bottom;
            }
            break;

          case ContentType.Top:
          case ContentType.Bottom:
          default:
            // Impossible cases as they have already been checked before
            throw new AbstractInterpretationException("Impossible case?");
          #endregion
        }
      }
      else
      {
        result = this.Bottom;
      }

      return result;
    }

    public IAbstractDomain/*!*/ Widening(IAbstractDomain/*!*/ prev)
    {
      return this.Join(prev);
    }

    #endregion

    #region ICloneable Members

    public object Clone()
    {
      return new StringAbstraction(this);
    }

    #endregion

    #region Private

    static private IAbstractDomain/*!*/ HelperForStringPrefixJoin(string/*!*/ fullstring, string/*!*/ prefix)
    {
      IAbstractDomain result;

      if (fullstring.StartsWith(prefix))
      {
        result = new StringAbstraction(prefix, true);
      }
      else
      {
        result = new StringAbstraction();   // i.e. top
      }
      return result;
    }

    static private IAbstractDomain/*!*/ HelperForStringJoin(string/*!*/ s1, string/*!*/ s2)
    {
      IAbstractDomain/*!*/ result;
      if (s1.CompareTo(s2) == 0)
      { // If they are the same string, nothing to do
        result = new StringAbstraction(s1);
      }
      else
      { // If they are different strings, try to find a common prefix, or simply abstract away all the characters
        int i;
        StringBuilder commonPrefix = new StringBuilder();
        // we look for the common prefix of the two strings
        for (i = 0; i < Math.Min(s1.Length, s2.Length); i++)
        {
          if (s1[i] == s2[i])
          {
            commonPrefix.Append(s1[i]);
          }
          else
          {
            break;
          }
        }
        if (i == 0)
        { // there is no common prefix
          Set<SimpleCharacterAbstraction> chars1 = AlphaToSetOfCharacters(s1);
          Set<SimpleCharacterAbstraction> chars2 = AlphaToSetOfCharacters(s2);

          result = new StringAbstraction(chars1.Union(chars2));
        }
        else
        {
          result = new StringAbstraction(commonPrefix.ToString(), true);
        }
      }

      return result;
    }

    static private IAbstractDomain/*!*/ HelperForSetOfCharactersJoin(Set<SimpleCharacterAbstraction>/*!*/ iSet1, Set<SimpleCharacterAbstraction>/*!*/ iSet2)
    {
      Set<SimpleCharacterAbstraction> union = iSet1.Union(iSet2);

      return new StringAbstraction(union);
    }

    static private IAbstractDomain/*!*/ HelperForPrefixJoin(string/*!*/ p1, string/*!*/ p2)
    {
      IAbstractDomain/*!*/ result;
      if (p1.CompareTo(p2) == 0)
      { // If they are the same string, nothing to do
        result = new StringAbstraction(p1, true);
      }
      else
      {
        result = HelperForPrefixJoin(p1, p2);
      }

      return result;
    }

    /// <summary>
    /// Abstract the string <code>s</code> to a set of abstract characters
    /// </summary>
    /// <returns></returns>
    static private Set<SimpleCharacterAbstraction>/*!*/ AlphaToSetOfCharacters(string/*!*/ s)
    {
      Set<SimpleCharacterAbstraction> result = new Set<SimpleCharacterAbstraction>();

      for (int i = 0; i < s.Length; i++)
      {
        // This below is maybe not the most optimized version, but it works
        SimpleCharacterAbstraction tmp = new SimpleCharacterAbstraction(s[i]);
        result.Add(tmp);
      }

      return result;
    }

    private Set<SimpleCharacterAbstraction> RemoveWhiteSpaces(Set<SimpleCharacterAbstraction> iSet)
    {
      Set<SimpleCharacterAbstraction> result = new Set<SimpleCharacterAbstraction>();

      foreach (SimpleCharacterAbstraction c in iSet)
      {
        if (!c.IsSpace)
        {
          result.Add(c);
        }
      }
      return result;
    }

    #endregion

    #region ToString
    public override string ToString()
    {
      string result;
      switch (this.content)
      {
        case ContentType.Prefix:
          result = "Prefix(" + this.prefix + ")";
          break;

        case ContentType.SetOfCharacters:
          result = "Chars(" + this.characters + ")";
          break;

        case ContentType.String:
          result = "String(" + this.fullstring + ")";
          break;

        case ContentType.Top:
          result = "Top";
          break;

        case ContentType.Bottom:
          result = "Bottom";
          break;

        default:
          throw new AbstractInterpretationException("Error!!!");
      }

      return result;
    }
    #endregion

    #endregion
  }

  /// <summary>
  /// A simple abstraction set of characters
  /// </summary>
  internal class SimpleCharacterAbstraction 
  {
    #region Private state
    enum ContentType { Control, Digit, Letter, Punctuation, Space}

    private ContentType content;
    private char? c = null;
    #endregion

    public SimpleCharacterAbstraction(char c)
    {
      Init(c);
    }

    public bool IsSpace
    {
      get
      {
        return this.content == ContentType.Space;
      }
    }

    /// <summary>
    /// Redefine equals
    /// </summary>
    public override bool Equals(object obj)
    {
      SimpleCharacterAbstraction right = obj as SimpleCharacterAbstraction;
      if (right == null)
      {
        return false;
      }
      else
      {
        if (this.content != right.content)
        {
          return false;
        }
        else
        {
          switch (this.content)
          {
            case ContentType.Control:
            case ContentType.Punctuation:
            case ContentType.Space:
              return this.c.Equals(right.c);

            case ContentType.Digit:
              return true;

            case ContentType.Letter:
              return true;

            default:
              throw new AbstractInterpretationException("Unreacheable case? " + this.content);
          }
        }
      }
    }

    public override int GetHashCode()
    {
      int result;

      switch (this.content)
      {
        case ContentType.Control:
          result = this.c.GetHashCode();
          break;

        case ContentType.Digit:
          result = '0'.GetHashCode();
          break;

        case ContentType.Letter:
          result = 'a'.GetHashCode();
          break;

        case ContentType.Punctuation:
          result = this.c.GetHashCode();
          break;

        case ContentType.Space:
          result = this.c.GetHashCode();
          break;

        default:
          throw new AbstractInterpretationException("Unreacheable case? " + this.content);
      }

      return result;
    }

    #region Private

    private void Init(char c)
    {
      if (Char.IsControl(c))
      {
        this.c = c;
        this.content = ContentType.Control;
      }
      else if (Char.IsDigit(c))
      {
        this.c = null;
        this.content = ContentType.Digit;
      }
      else if (Char.IsLetter(c))
      {
        this.c = null;
        this.content = ContentType.Letter;
      }
      else if (Char.IsPunctuation(c))
      {
        this.c = c;
        this.content = ContentType.Punctuation;
      }
      else if (Char.IsWhiteSpace(c))
      {
        this.c = c;
        this.content = ContentType.Space;
      }
      else
      {
        throw new AbstractInterpretationTODOException("Unknown character : " + c);
      }
    }

    #endregion

    #region ToString
    public string ToLogicalFormula()
    {
      return null;
    }

    public override string ToString()
    {
      string result;

      switch (this.content)
      {
        case ContentType.Control:
          result = "Control(" + this.c + ")";
          break;

        case ContentType.Digit:
          result = "Digit(?)";
          break;

        case ContentType.Letter:
          result = "Letter(?)";
          break;

        case ContentType.Punctuation:
          result = "Punctuation(" + this.c + ")";
          break;

        case ContentType.Space:
          result = "Space(" + this.c + ")";
          break;

        default:
          throw new AbstractInterpretationException("Unreacheable case? " + this.content); 
      }
      return result;
    }
    #endregion
  }
}
