// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

public class HashSet<Element> : MiniSet<Element> where Element : class, MiniValue
{
    private System.Collections.Generic.HashSet<Element> hashSet = new System.Collections.Generic.HashSet<Element>();

    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
        Contract.Invariant(hashSet != null);
    }
    /// <summary>
    /// Returns true if the set contains the given element. If the element is null or undefined, the result is always false.
    /// </summary>
    [ClousotRegressionTest]
    public bool Contains(Element element)
    {
        if (element == null || !element.IsDefined) return false;
        var result = hashSet.Contains(element);
        return result;
    }

    [ClousotRegressionTest]
    public bool Contains1(Element element)
    {
        if (element == null) return false;
        //var result = this.hashSet.Contains(element);
        return true;
    }

    [ClousotRegressionTest]
    public bool Contains2(Element element)
    {
        if (!element.IsDefined) return false;
        // var result = this.hashSet.Contains(element);
        return true;
    }

    /// <summary>
    /// True if the value is not the special undefined value for its type.
    /// Every type has an undefined value and all operations involving one or more undefined arguments result in undefined.
    /// </summary>
    public bool IsDefined
    {
        get { return true; }
    }
}

/// <summary>
/// A set of elements of type Element.
/// </summary>
/// <typeparam name="Element"></typeparam>
[ContractClass(typeof(MiniSetContract<>))]
public interface MiniSet<Element> : MiniValue where Element : class, MiniValue
{
    /// <summary>
    /// Returns true if the set contains the given element. If the element is null or undefined, the result is always false.
    /// </summary>
    bool Contains(Element element);

    bool Contains1(Element element);

    bool Contains2(Element element);
}

#region MiniSet contract binding
[ContractClassFor(typeof(MiniSet<>))]
internal abstract class MiniSetContract<Element> : MiniSet<Element> where Element : class, MiniValue
{
    public bool Contains(Element element)
    {
        Contract.Ensures(element != null && element.IsDefined || !Contract.Result<bool>());

        throw new NotImplementedException();
    }

    public bool Contains1(Element element)
    {
        Contract.Ensures(element != null || !Contract.Result<bool>());

        throw new NotImplementedException();
    }

    public bool Contains2(Element element)
    {
        Contract.Ensures(element.IsDefined || !Contract.Result<bool>());

        throw new NotImplementedException();
    }

    public bool IsDefined
    {
        get { throw new NotImplementedException(); }
    }
}
#endregion

/// <summary>
/// All values in Mini implement this interface.
/// </summary>
public interface MiniValue
{
    /// <summary>
    /// True if the value is not the special undefined value for its type.
    /// Every type has an undefined value and all operations involving one or more undefined arguments result in undefined.
    /// </summary>
    bool IsDefined { get; }
}
