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

#define CONTRACTS_FULL
using System;
using System.Diagnostics.Contracts;

public class Foo {
  private int foo;
  public void Accessfoo() {
    Contract.Requires(foo > 5);
    Contract.Ensures(foo > Contract.OldValue(foo)+1);
  }
  private class Bar {
    internal void AccessFoofoo(Foo foo) {
      Contract.Requires(foo.foo > 5);
    }
  }
}
[ContractClass(typeof(CJ))]
public interface J{
  int M(int x);
}
//[ContractClassFor(typeof(J))] // Error: Need back link
public class CJ{
  public int M(int x){ return x; }
}
public class SpecPublicField{
  [ContractPublicPropertyName("P")]
  private int x;
  public bool P { get { return true; } }
}
public class CantThrowNonVisibleException{
  private class PrivateException : Exception {}
  public int M(int x){
    if (x == 0) throw new PrivateException();
    Contract.EndContractBlock();
    return x;
  }
}
public class InvalidMethodsInPrecondition{
  public int M(int x, out int y){
    Contract.Requires(Contract.Result<int>() == x);        // bad
    Contract.Requires(Contract.ValueAtReturn(out y) == x); // bad but not caught
    Contract.Requires(Contract.OldValue(x) == x);          // bad
    y = 3;
    return x;
  }
}

public class InvalidMethodsInPostcondition{
  private class MyException : Exception {}
  public int M(int x, out int y){
    Contract.Ensures(Contract.Result<bool>() == true); // Error: wrong type
    Contract.Ensures(Contract.OldValue(3) == x); // Error: literal arg to Old
    Contract.EnsuresOnThrow<Exception>(Contract.Result<int>() > 0); // error result in exception ensures

    y = 3;
    return x;
  }

  int z;
  [ContractInvariantMethod]
  void ObjectInvariant() {
    z = 5;
    Contract.Invariant(z == 5);
  }
}








class Internal{
  int x;
  public int M(int y){
    Contract.Requires(x < y); // Error: x is not visible outside of this class
    return y;
  }
}

class BaseClassWithPrecondition{
  public virtual int M(int x){
    Contract.Requires(x != 3);
    return 3;
  }
}
class DerivedClassWithPrecondition : BaseClassWithPrecondition{
  public override int M(int y){
    Contract.Requires(0 < y);
    return 27;
  }
}
public class NonSealedClassWithPrivateInvariant{
  bool b = true;
  private void ObjectInvariant(){
    Contract.Invariant(b);
  }
}
[Pure]
public class PureClass {
  public static int MethodInPureClass(int y) { return y; }
}
public class NotPureClass {
  public static int MethodInNotPureClass(int y) { return y; }
}
public class PurityTests{
  public int P { get { return 3; } }
  [Pure] public int MarkedPure(int y) { return y; }
  public int NotMarkedPure(int y) { return y; }
  public int M(int x){
    Contract.Requires(x <= MarkedPure(x)); // OK
    Contract.Requires(x <= PureClass.MethodInPureClass(x)); // OK
    Contract.Requires(x <= P); // OK: properties are pure by default
    Contract.Requires(x <= NotMarkedPure(x)); // Error
    Contract.Requires(x <= NotPureClass.MethodInNotPureClass(x)); // Error
    return x;
  }
}

public class CA1004 {
  public int x;
  public void M() {
    x = 5;
    Contract.Requires(x > 5);
  }
}

public class CA1005 {
  public int x;
  public void M() {
    x = 5;
    Contract.Ensures(x > 5);
  }

  [ContractModel]
  public int X { get { return x; } }

  public void N() {
    x = 5;
    Contract.Ensures(X > 5); // not warned about because we skip Model extraction in rewriter
  }
}

public class CA1006 {
  [ContractClass(typeof(CI))]
  interface I {
  }
  struct CI : I {
  }
}

class CA1008 {
  [ContractClass(typeof(CK))]
  public interface K{
    int M(int x);
  }
  [ContractClassFor(typeof(K))]
  abstract public class CK{ // Error: Need to implement K
    public int M(int x){ return x; }
  }
}

class CA1009 {
  [ContractClass(typeof(Bad))]
  abstract class Abstract {
  }
  [ContractClassFor(typeof(Abstract))]
  abstract class Bad {
  }
}

class CA1010 {
  [ContractPublicPropertyName("F")]
  private int f;
}

class CA1020 {
  [ContractClass(typeof(CK))]
  public interface K{
    int M(int x);
  }
  [ContractClassFor(typeof(K))]
  public class CK : K {
    public int M(int x){ return x; }
  }
}

class CA1042
{
  public class Invariant_NoFlags
  {
    bool pass1 = true;
    bool pass2 = true;
    bool pass3 = true;
    bool pass4 = true;

    [ContractInvariantMethod]
    void ObjectInvariant()
    {
      Contract.Invariant(pass1 == true);
      Contract.Invariant(pass2 == true, "pass2 must be true");
      Contract.Invariant(pass3 == true, String.Empty);
      Contract.Invariant(pass4 == true, null);
    }

    public int CheckInvariant(bool shouldPass)
    {
      pass1 = shouldPass;
      pass2 = true;
      pass3 = true;
      pass4 = true;
      return 10;
    }

    public int CheckInvariantString(bool shouldPass)
    {
      pass1 = true;
      pass2 = shouldPass;
      pass3 = true;
      pass4 = true;
      return 10;
    }

    public int CheckInvariantStringEmpty(bool shouldPass)
    {
      pass1 = true;
      pass2 = true;
      pass3 = shouldPass;
      pass4 = true;
      return 10;
    }

    public int CheckInvariantStringNull(bool shouldPass)
    {
      pass1 = true;
      pass2 = true;
      pass3 = true;
      pass4 = shouldPass;
      return 10;
    }

    // Does not make sense; only call in non-rewritten binary
    [ContractInvariantMethod]
    public void CallContractInvariantMethod()
    {
      Contract.Invariant(pass1 == true);
    }
    public void runTest()
    {
      Console.WriteLine("Contract_class\\InvariantTest.cs");

      try
      {
        Console.WriteLine("Assembly compiled with no flags");
        Invariant_NoFlags test1 = new Invariant_NoFlags();
        VerifyPass("001 CheckInvariant(true)", delegate { test1.CheckInvariant(true); });
        VerifyPass("002 CheckInvariant(false)", delegate { test1.CheckInvariant(false); });
        VerifyPass("003 CheckInvariantString(true)", delegate { test1.CheckInvariantString(true); });
        VerifyPass("004 CheckInvariantString(false)", delegate { test1.CheckInvariantString(false); });
        VerifyPass("005 CallContractInvariantMethod()", delegate { test1.CallContractInvariantMethod(); });
        VerifyPass("006 CheckInvariant(false), SetHandled", delegate { test1.CheckInvariant(false); });
        VerifyPass("007 CheckInvariantString(false), SetHandled", delegate { test1.CheckInvariantString(false); });
      }
      finally { }
    }

    public void VerifyPass(string s, Action action) { }
  }


}

public class ContractModelAttribute : Attribute {}
