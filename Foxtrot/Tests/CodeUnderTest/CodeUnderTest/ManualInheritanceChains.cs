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
using System.Diagnostics.Contracts;
using ManualInheritanceChain;

namespace ManualInheritanceChain {
  public class Level1 : BaseOfChain {
    public override void Test(int x)
    {
#if LEGACY
      if (x <= 0) throw new ArgumentException("Level 1: x must be positive");
      Contract.EndContractBlock();
#endif
    }

    // This is here so that the call to Test will get the callsiterequires
    // treatment.
    public void CallTest(int x)
    {
      Test(x);
    }
  }


  [ContractClass(typeof(Level2Contract))]
  public abstract class Level2 : Level1 {
    public override void Test(int x)
    {
#if LEGACY
      if (x <= 0) throw new ArgumentException("Level 2: x must be positive");
      Contract.EndContractBlock();
#endif
    }

    public abstract void Test(string s);

    [Pure]
    protected abstract bool IsNonEmpty(string s);

  }

  [ContractClassFor(typeof(Level2))]
  abstract class Level2Contract : Level2 {
    public override void Test(string s)
    {
      Contract.Requires<ArgumentNullException>(s != null);
    }

    protected override bool IsNonEmpty(string s)
    {
      Contract.Requires(s != null);

      throw new NotImplementedException();
    }
  }
  public class Level3 : Level2 {
    public override void Test(int x)
    {
#if LEGACY
      if (x <= 0) throw new ArgumentException("Level 3: x must be positive");
      Contract.EndContractBlock();
#endif
    }

    // This is here so that the call to Test will get the callsiterequires
    // treatment.
    public new void CallTest(int x)
    {
      Test(x);
    }


    public override void Test(string s)
    {
#if LEGACY
      if (s == null) throw new ArgumentNullException(null, "Precondition failed: s != null");
      Contract.EndContractBlock();
#endif
    }

    protected override bool IsNonEmpty(string s)
    {
      return !String.IsNullOrEmpty(s);
    }

    public bool TestIsNonEmpty(string s)
    {
      return IsNonEmpty(s);
    }
  }

  public class Level4 : Level3 {
    public override void Test(int x)
    {
#if LEGACY
      if (x <= 0) throw new ArgumentException("Level 4: x must be positive");
      Contract.EndContractBlock();
#endif
    }

    // This is here so that the call to Test will get the callsiterequires
    // treatment.
    public new void CallTest(int x)
    {
      Test(x);
    }

    public override void Test(string s)
    {
#if LEGACY
      if (s == null) throw new ArgumentNullException("Level 4: s");
      Contract.EndContractBlock();
#endif
    }
    // This is here so that the call to Test will get the callsiterequires
    // treatment.
    public void CallTest(string s)
    {
      Test(s);
    }
  }



}

namespace ManualInheritanceChainWithHelpers
{
  class HelperClass
  {
#if LEGACY
    [ContractArgumentValidator]
#else
    [ContractAbbreviator]
#endif
    public static void MustBePositive(int x, string message, string name)
    {
#if LEGACY
      if (x <= 0) throw new ArgumentException(message, name);
      Contract.EndContractBlock();
#else
      Contract.Requires<ArgumentException>(x > 0, String.Format("{0} must be positive", name));
#endif
    }

#if LEGACY
    [ContractArgumentValidator]
#else
    [ContractAbbreviator]
#endif
    public static void NotNull(string s, string name, string message)
    {
#if LEGACY
      if (s == null) throw new ArgumentNullException(name, message);
      Contract.EndContractBlock();
#else
      Contract.Requires<ArgumentNullException>(s != null, name);
#endif
    }

#if LEGACY
    [ContractArgumentValidator]
#else
    [ContractAbbreviator]
#endif
    public static void NotNull(string s, string name)
    {
#if LEGACY
      if (s == null) throw new ArgumentNullException(name);
      Contract.EndContractBlock();
#else
      Contract.Requires<ArgumentNullException>(s != null, name);
#endif
    }

#if LEGACY
    [ContractArgumentValidator]
#else
    [ContractAbbreviator]
#endif
    internal static void NotNan(double d, string name)
    {
#if LEGACY
      if (double.IsNaN(d)) throw new ArgumentOutOfRangeException(name);
      Contract.EndContractBlock();
#else
      Contract.Requires<ArgumentOutOfRangeException>(!double.IsNaN(d), name);
#endif
    }
  }

  public class Level1 : BaseOfChain
  {
    public override void Test(int x)
    {
#if LEGACY
      HelperClass.MustBePositive(x, "Level 1: x", "x must be positive");
#endif
    }

    public virtual void TestDouble(double d)
    {
      HelperClass.NotNan(d, "d");
    }
  }


  [ContractClass(typeof(Level2Contract))]
  public abstract class Level2 : Level1
  {
    public override void Test(int x)
    {
#if LEGACY
      HelperClass.MustBePositive(x, "Level 2: x", "x must be positive");
#endif
    }

    public abstract void Test(string s);

    [Pure]
    protected abstract bool IsNonEmpty(string s);

    public override void TestDouble(double x) {
#if LEGACY
      HelperClass.NotNan(x, "x");
#endif
    }
  }

  [ContractClassFor(typeof(Level2))]
  abstract class Level2Contract : Level2
  {
    public override void Test(string s)
    {
      Contract.Requires<ArgumentNullException>(s != null, "s");
    }

    protected override bool IsNonEmpty(string s)
    {
      Contract.Requires(s != null);

      throw new NotImplementedException();
    }

  }
  public class Level3 : Level2
  {
    public override void Test(int x)
    {
#if LEGACY
      HelperClass.MustBePositive(x, "Level 3: x", "x must be positive");
#endif
    }


    public override void Test(string s)
    {
#if LEGACY
      HelperClass.NotNull(s, "s", "Precondition failed: s != null");
#endif
    }

    protected override bool IsNonEmpty(string s)
    {
      return !String.IsNullOrEmpty(s);
    }

    public bool TestIsNonEmpty(string s)
    {
      return IsNonEmpty(s);
    }

    public override void TestDouble(double d)
    {
#if LEGACY
      HelperClass.NotNan(d, "d");
#endif
    }
  }

  public class Level4 : Level3
  {
    public override void Test(int x)
    {
#if LEGACY
      HelperClass.MustBePositive(x, "Level 4: x", "x must be positive");
#endif
    }


    public override void Test(string s)
    {
#if LEGACY
      HelperClass.NotNull(s, "s", "Level 4: s");
#endif
    }

    public override void TestDouble(double d)
    {
#if LEGACY
      HelperClass.NotNan(d, "d");
#endif
    }
  }



}
