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
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using Microsoft.Research.ClousotRegression;

namespace ReferenceAllOOBC {


  class TestMicrosoftVisualBasic
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 8, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 15, MethodILOffset = 0)]
    public static void Test1(string str)
    {
      Contract.Assert(Microsoft.VisualBasic.Strings.Len(str) == str.Length);
    }
  }

  class TestMscorlib
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possibly calling a method on a null reference \'array\'", PrimaryILOffset = 2, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 13, MethodILOffset = 0)]
    public static void Test1(Array array)
    {
      Contract.Assert(array.Rank >= 0);
      //Contract.Assert(((System.Collections.ICollection)array).Count == array.Length);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possibly calling a method on a null reference 'e'", PrimaryILOffset = 2, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 13, MethodILOffset = 0)]
    public static void TestExceptionGetType(Exception e)
    {
      Contract.Assert(e.GetType() != null);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = @"assert is valid", PrimaryILOffset = 12, MethodILOffset = 0)]
    public static void Test2()
    {
      Contract.Assert(System.Collections.Generic.EqualityComparer<string>.Default != null);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=18,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=28,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=38,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=51,MethodILOffset=0)]
    public static void TestPureLookup(Dictionary<int,string> dict, int key)
    {
      Contract.Requires(dict != null);

      string result1;
      var found1 = dict.TryGetValue(key, out result1);

      string result2;
      var found2 = dict.TryGetValue(key, out result2);
      Contract.Assert(found1 == found2);
      Contract.Assert(result1 == result2);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=18,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=34,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=54,MethodILOffset=0)]
    public static void TestOutByRef() {
      var d = new Dictionary<string,object>();
      d[""] = new object();
      object o = null;
      d.TryGetValue("", out o);
      Contract.Assume(o != null);
      Contract.Assert(true); // make sure this is reachable
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=29,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=53,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=59,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=13,MethodILOffset=29)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=13,MethodILOffset=59)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=36,MethodILOffset=59)]
    public static string TryGetTail(string value, string divider)
    {
      Contract.Requires(value != null);
      Contract.Requires(divider != null);
      var p = value.IndexOf(divider);
      if (p == -1) return null;
      return value.Substring(p + divider.Length);
    }

#if NETFRAMEWORK_4_0 && NETFRAMEWORK_4_0_CONTRACTS || SILVERLIGHT_4_0 && SILVERLIGHT_4_0_CONTRACTS
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=22,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=15,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=43,MethodILOffset=0)]
    public static void TestTuple(int x)
    {
      var p = Tuple.Create(x);
      Contract.Assert(p != null);
      Contract.Assert(object.Equals(p.Item1, x));
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=22,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=15,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=43,MethodILOffset=0)]
    public static void TestTuple2(int x)
    {
      var p = new Tuple<int>(x);
      Contract.Assert(p != null);
      Contract.Assert(object.Equals(p.Item1, x));
    }

#endif

    class CollectionWrapper<T> : ICollection<T>
    {
      private readonly ICollection<T> mBackend = new List<T>();

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 6, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 12, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 20, MethodILOffset = 35)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 13, MethodILOffset = 35)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 38, MethodILOffset = 35)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 28, MethodILOffset = 35)]
      public CollectionWrapper()
      {
        Contract.Ensures(((ICollection<T>)this).Count == 0);

      }

      [ContractInvariantMethod]
      private void Invariant()
      {
        Contract.Invariant(mBackend != null);
        Contract.Invariant(mBackend.Count == this.Count);
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 2, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 8, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 14)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 13, MethodILOffset = 14)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 38, MethodILOffset = 14)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 28, MethodILOffset = 14)]
      void ICollection<T>.Add(T item)
      {
        mBackend.Add(item); // performs mod of mBackend.Count and implictly this.Count
      }


      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 2, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 7, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 1, MethodILOffset = 13)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 13, MethodILOffset = 13)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 38, MethodILOffset = 13)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 15, MethodILOffset = 13)]
      void ICollection<T>.Clear()
      {
        this.mBackend.Clear();
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 2, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 8, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 11, MethodILOffset = 17)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 31, MethodILOffset = 17)]
      bool ICollection<T>.Contains(T item)
      {
        return this.mBackend.Contains(item);
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as array)", PrimaryILOffset = 38, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 41, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 2, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 9, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 9)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 31, MethodILOffset = 9)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 58, MethodILOffset = 9)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 13, MethodILOffset = 15)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 38, MethodILOffset = 15)]
      void ICollection<T>.CopyTo(T[] array, int arrayIndex)
      {
        this.mBackend.CopyTo(array, arrayIndex);
      }

      public int Count
      {
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 26, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 31, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 7, MethodILOffset = 40)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 12, MethodILOffset = 40)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 17, MethodILOffset = 40)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 19, MethodILOffset = 40)]
        get
        {
          Contract.Ensures(Contract.Result<int>() == mBackend.Count);

          return mBackend.Count;
        }
      }

      bool ICollection<T>.IsReadOnly
      {
        [ClousotRegressionTest]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 2, MethodILOffset = 0)]
        [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 7, MethodILOffset = 0)]
        get
        {
          return this.mBackend.IsReadOnly;
        }
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 2, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 8, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 13, MethodILOffset = 17)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "invariant is valid", PrimaryILOffset = 38, MethodILOffset = 17)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=1,MethodILOffset=17)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=44,MethodILOffset=17)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=28,MethodILOffset=17)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message="ensures is valid",PrimaryILOffset=79,MethodILOffset=17)]
      bool ICollection<T>.Remove(T item)
      {
        return mBackend.Remove(item);
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 2, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 7, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 17, MethodILOffset = 16)]
      IEnumerator<T> IEnumerable<T>.GetEnumerator()
      {
        return mBackend.GetEnumerator();
      }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as field receiver)", PrimaryILOffset = 2, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 7, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "ensures is valid", PrimaryILOffset = 17, MethodILOffset = 16)]
      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      {
        return mBackend.GetEnumerator();
      }

    }

    class MyCollection : ReadOnlyCollection<object>
    {
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 7, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Array creation : ok", PrimaryILOffset = 2, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 19, MethodILOffset = 7)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"No overflow (caused by a negative array size)",PrimaryILOffset=2,MethodILOffset=0)]
      public MyCollection() : base(new object[0]) { }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 3, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.False, Message = "requires is false: index >= 0", PrimaryILOffset = 13, MethodILOffset = 3)]
      [RegressionOutcome(Outcome = ProofOutcome.Bottom, Message = "requires unreachable", PrimaryILOffset = 33, MethodILOffset = 3)]
      public object GetItem()
      {
        return this[-1];
      }


      [ClousotRegressionTest]
      [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 16, MethodILOffset = 0)]
      [RegressionOutcome(Outcome = ProofOutcome.False, Message = "requires is false: index >= 0", PrimaryILOffset = 13, MethodILOffset = 16)]
      [RegressionOutcome(Outcome = ProofOutcome.Bottom, Message = "requires unreachable", PrimaryILOffset = 33, MethodILOffset = 16)]
      public static T Test<T>(ReadOnlyCollection<T> x)
      {
        Contract.Requires(x != null);
        return x[-1];
      }
    }
  }


  class TestSystem
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 12, MethodILOffset = 0)]
    public static void Test1()
    {
      Contract.Assert(System.Diagnostics.Process.GetCurrentProcess() != null);
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=15,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=35,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=60,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=28,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=48,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=13,MethodILOffset=60)]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message="requires is false: value <= 0xFFFF",PrimaryILOffset=35,MethodILOffset=60)]
    public static void Test2(SmtpClient mailClient)
    {
      Contract.Requires(mailClient != null);

      X509CertificateCollection certs = mailClient.ClientCertificates;
      Contract.Assert(certs != null);
      ServicePoint sp = mailClient.ServicePoint;
      Contract.Assert(sp != null);
      mailClient.Port = 0x10000;
    }

[ClousotRegressionTest]   
     [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=8,MethodILOffset=0)]
[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=28,MethodILOffset=0)]
[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"valid non-null reference (as receiver)",PrimaryILOffset=35,MethodILOffset=0)]
[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=16,MethodILOffset=0)]
[RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=43,MethodILOffset=0)]
    public static void Test3()
    {
      var l = new LinkedList<int>();
      Contract.Assert(l.Count == 0);

      l.AddFirst(1111);

      Contract.Assert(l.Count == 1);
    }
  }

  class TestSystemConfiguration
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possibly calling a method on a null reference \'elem\'", PrimaryILOffset = 2, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 13, MethodILOffset = 0)]
    public static void Test1(System.Configuration.ConfigurationElement elem)
    {
      Contract.Assert(elem.ElementInformation != null);
    }
  }

  class TestSystemConfigurationInstall
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possibly calling a method on a null reference \'installer\'", PrimaryILOffset = 2, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 13, MethodILOffset = 0)]
    public static void Test1(System.Configuration.Install.Installer installer)
    {
      Contract.Assert(installer.Installers != null);
    }
  }

  class TestSystemCore
  {
    [ClousotRegressionTest] // CCI2 is not seeing Requires of Cast
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "requires unproven: source != null", PrimaryILOffset = 13, MethodILOffset = 2)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 13, MethodILOffset = 0)]
    public static void Test1(System.Collections.IEnumerable coll)
    {
      Contract.Assert(coll.Cast<string>() != null);
    }
    
  }

  class TestSystemData
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possibly calling a method on a null reference \'constraint\'", PrimaryILOffset = 2, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 13, MethodILOffset = 0)]
    public static void Test1(System.Data.Constraint constraint)
    {
      Contract.Assert(constraint.ExtendedProperties != null);
    }
  }

  class TestSystemDrawing
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=13,MethodILOffset=0)]
    public static void Test1(IntPtr ptr)
    {
      Contract.Assert(System.Drawing.Bitmap.FromHicon(ptr) != null);
    }
  }

  class TestSystemSecurity
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message="requires unproven: userData != null",PrimaryILOffset=13,MethodILOffset=4)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 17, MethodILOffset = 0)]
    public static void Test1(byte[] userData, byte[] entropy, System.Security.Cryptography.DataProtectionScope scope)
    {
      var result = System.Security.Cryptography.ProtectedData.Protect(userData, entropy, scope);
      Contract.Assert(result != null);
    }
  }

  class TestSystemWeb
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 26, MethodILOffset = 0)]
    public static void Test1(string s)
    {
      Contract.Requires(s != null);
      Contract.Assert(System.Web.HttpUtility.HtmlAttributeEncode(s) != null);
    }
  }

  class TestSystemWindows
  {
    //requires silverlight
  }

  class TestSystemWindowsBrowser
  {
    //requires silverlight
  }

  class TestSystemWindowsForms
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 12, MethodILOffset = 0)]
    public static void Test1()
    {
      Contract.Assert(System.Windows.Forms.Application.OpenForms != null);
    }
  }

  class TestSystemXml
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possibly calling a method on a null reference \'doc\'", PrimaryILOffset = 2, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 13, MethodILOffset = 0)]
    public static void Test1(System.Xml.XmlDocument doc)
    {
      Contract.Assert(doc.Schemas != null);
    }
  }

  class TestSystemXmlLinq
  {
    [ClousotRegressionTest] // CCI2 is not seeing contracts on Annotations
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = "Possibly calling a method on a null reference 'doc'", PrimaryILOffset = 16, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 13, MethodILOffset = 16)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 27, MethodILOffset = 0)]
    static void Test1(System.Xml.Linq.XDocument doc, System.Type type)
    {
      Contract.Requires(type != null);

      Contract.Assert(doc.Annotations(type) != null);
    }

    [ClousotRegressionTest] // CCI2 is lacking requires of XName implicit converter
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 15, MethodILOffset = 6)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 19, MethodILOffset = 0)]
    static void Test2(IEnumerable<string> elements)
    {
      System.Xml.Linq.XName xname1 = "hello";
      Contract.Assert(xname1 != null);
    }

    [ClousotRegressionTest] // CCI2 is not seeing some requires contracts 
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as array)", PrimaryILOffset = 118, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as array)", PrimaryILOffset = 60, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 80, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 88, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 100, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 141, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 154, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 166, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Lower bound access ok", PrimaryILOffset = 60, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "Upper bound access ok", PrimaryILOffset = 60, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 15, MethodILOffset = 133)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "requires is valid", PrimaryILOffset = 19, MethodILOffset = 146)]
    void WriteContractElementToSummary(System.Xml.Linq.XElement summaryElement, string contractElement, params string[] info)
    {
      Contract.Requires(summaryElement != null);
      Contract.Requires(contractElement != null);
      Contract.Requires(info != null);

      System.Text.StringBuilder infoBuilder = new System.Text.StringBuilder(contractElement);
      foreach (string infoString in info)
      {
        if (infoString != null)
        {
          infoBuilder.Append(" (");
          infoBuilder.Append(infoString);
          infoBuilder.Append(")");
        }
      }
      System.Xml.Linq.XName xname = "para";
      System.Xml.Linq.XElement contractXElement = new System.Xml.Linq.XElement(xname, infoBuilder.ToString());

      summaryElement.Add(contractXElement);

      Console.WriteLine("\t\t" + infoBuilder.ToString());
    }

  }

  class TestWindowsBase
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 5, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 13, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "valid non-null reference (as receiver)", PrimaryILOffset = 29, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 21, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.True, Message = "assert is valid", PrimaryILOffset = 37, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possible precision mismatch for the arguments of ==", PrimaryILOffset = 19, MethodILOffset = 0)]
    [RegressionOutcome(Outcome = ProofOutcome.Top, Message = @"Possible precision mismatch for the arguments of ==", PrimaryILOffset = 35, MethodILOffset = 0)]
    public static void Test1(double x, double y)
    {
      var p = new Point(x, y);
      Contract.Assert(p.X == x);
      Contract.Assert(p.Y == y);
    }
  }

  class TestMicrosoftVisualBasicCompatibility
  {
    [ClousotRegressionTest]// CCI2 is lacking some contracts
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="valid non-null reference (as receiver)",PrimaryILOffset=15,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message="assert is valid",PrimaryILOffset=26,MethodILOffset=0)]
    public static void Test1(Microsoft.VisualBasic.Compatibility.VB6.BaseControlArray bca)
    {
      Contract.Requires(bca != null);

      Contract.Assert(bca.Count() >= 0);
    }
  }
}
