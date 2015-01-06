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

#if !NETFRAMEWORK_3_5 && !SILVERLIGHT_3_0

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.Contracts;

namespace System
{
  public partial interface ITuple
  {
    int Size
    {
      get;
    }

  }

  #region ITuple contract binding
  [ContractClass(typeof(ITupleContract))]
  public partial interface ITuple {

  } 

  [ContractClassFor(typeof(ITuple))]
  abstract class ITupleContract : ITuple {
    public int Size
    {
      get
      {
        Contract.Ensures(Contract.Result<int>() >= 0);
        return default(int);
      }
    }
  } 
  #endregion

  public static class Tuple
  {
    public static Tuple<T1> Create<T1>(T1 item1)
    {
      Contract.Ensures(Contract.Result<Tuple<T1>>() != null);
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1>>().Item1, item1));
      return default(Tuple<T1>);
    }
    public static Tuple<T1,T2> Create<T1,T2>(T1 item1, T2 item2)
    {
      Contract.Ensures(Contract.Result<Tuple<T1, T2>>() != null);
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2>>().Item1, item1));
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2>>().Item2, item2));
      return default(Tuple<T1, T2>);
    }
    public static Tuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
    {
      Contract.Ensures(Contract.Result<Tuple<T1, T2, T3>>() != null);
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2, T3>>().Item1, item1));
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2, T3>>().Item2, item2));
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2, T3>>().Item3, item3));
      return default(Tuple<T1, T2, T3>);
    }
    public static Tuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
    {
      Contract.Ensures(Contract.Result<Tuple<T1, T2, T3, T4>>() != null);
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2, T3, T4>>().Item1, item1));
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2, T3, T4>>().Item2, item2));
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2, T3, T4>>().Item3, item3));
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2, T3, T4>>().Item4, item4));
      return default(Tuple<T1, T2, T3, T4>);
    }
    public static Tuple<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
    {
      Contract.Ensures(Contract.Result<Tuple<T1, T2, T3, T4, T5>>() != null);
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2, T3, T4, T5>>().Item1, item1));
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2, T3, T4, T5>>().Item2, item2));
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2, T3, T4, T5>>().Item3, item3));
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2, T3, T4, T5>>().Item4, item4));
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2, T3, T4, T5>>().Item5, item5));
      return default(Tuple<T1, T2, T3, T4, T5>);
    }
    public static Tuple<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
    {
      Contract.Ensures(Contract.Result<Tuple<T1, T2, T3, T4, T5, T6>>() != null);
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2, T3, T4, T5, T6>>().Item1, item1));
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2, T3, T4, T5, T6>>().Item2, item2));
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2, T3, T4, T5, T6>>().Item3, item3));
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2, T3, T4, T5, T6>>().Item4, item4));
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2, T3, T4, T5, T6>>().Item5, item5));
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2, T3, T4, T5, T6>>().Item6, item6));
      return default(Tuple<T1, T2, T3, T4, T5, T6>);
    }
    public static Tuple<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
    {
      Contract.Ensures(Contract.Result<Tuple<T1, T2, T3, T4, T5, T6, T7>>() != null);
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2, T3, T4, T5, T6, T7>>().Item1, item1));
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2, T3, T4, T5, T6, T7>>().Item2, item2));
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2, T3, T4, T5, T6, T7>>().Item3, item3));
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2, T3, T4, T5, T6, T7>>().Item4, item4));
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2, T3, T4, T5, T6, T7>>().Item5, item5));
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2, T3, T4, T5, T6, T7>>().Item6, item6));
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2, T3, T4, T5, T6, T7>>().Item7, item7));
      return default(Tuple<T1, T2, T3, T4, T5, T6, T7>);
    }
    public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>> Create<T1, T2, T3, T4, T5, T6, T7, T8>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
    {
      Contract.Ensures(Contract.Result<Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>>>() != null);
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>>>().Item1, item1));
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>>>().Item2, item2));
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>>>().Item3, item3));
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>>>().Item4, item4));
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>>>().Item5, item5));
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>>>().Item6, item6));
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>>>().Item7, item7));
      Contract.Ensures(Object.Equals(Contract.Result<Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>>>().Rest, item8));
      return default(Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>>);
    }
  }

  public class Tuple<T1> : ITuple
  {
    public Tuple(T1 item1)
    {
      Contract.Ensures(Object.Equals(this.Item1, item1));
    }

    extern public T1 Item1
    {
      get;
    }

    extern int ITuple.Size { get; }
  }
  public class Tuple<T1, T2> : ITuple
  {
    public Tuple(T1 item1, T2 item2)
    {
      Contract.Ensures(Object.Equals(this.Item1, item1));
      Contract.Ensures(Object.Equals(this.Item2, item2));
    }

    extern public T1 Item1
    {
      get;
    }
    extern public T2 Item2
    {
      get;
    }

    extern int ITuple.Size { get; }
  }
  public class Tuple<T1, T2, T3> : ITuple
  {
    public Tuple(T1 item1, T2 item2, T3 item3)
    {
      Contract.Ensures(Object.Equals(this.Item1, item1));
      Contract.Ensures(Object.Equals(this.Item2, item2));
      Contract.Ensures(Object.Equals(this.Item3, item3));
    }

    extern public T1 Item1
    {
      get;
    }
    extern public T2 Item2
    {
      get;
    }
    extern public T3 Item3
    {
      get;
    }

    extern int ITuple.Size { get; }
  }
  public class Tuple<T1, T2, T3, T4> : ITuple
  {
    public Tuple(T1 item1, T2 item2, T3 item3, T4 item4)
    {
      Contract.Ensures(Object.Equals(this.Item1, item1));
      Contract.Ensures(Object.Equals(this.Item2, item2));
      Contract.Ensures(Object.Equals(this.Item3, item3));
      Contract.Ensures(Object.Equals(this.Item4, item4));
    }

    extern public T1 Item1
    {
      get;
    }
    extern public T2 Item2
    {
      get;
    }
    extern public T3 Item3
    {
      get;
    }
    extern public T4 Item4
    {
      get;
    }

    extern int ITuple.Size { get; }
  }
  public class Tuple<T1, T2, T3, T4, T5> : ITuple
  {
    public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
    {
      Contract.Ensures(Object.Equals(this.Item1, item1));
      Contract.Ensures(Object.Equals(this.Item2, item2));
      Contract.Ensures(Object.Equals(this.Item3, item3));
      Contract.Ensures(Object.Equals(this.Item4, item4));
      Contract.Ensures(Object.Equals(this.Item5, item5));
    }

    extern public T1 Item1
    {
      get;
    }
    extern public T2 Item2
    {
      get;
    }
    extern public T3 Item3
    {
      get;
    }
    extern public T4 Item4
    {
      get;
    }
    extern public T5 Item5
    {
      get;
    }

    extern int ITuple.Size { get; }
  }
  public class Tuple<T1, T2, T3, T4, T5, T6> : ITuple
  {
    public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
    {
      Contract.Ensures(Object.Equals(this.Item1, item1));
      Contract.Ensures(Object.Equals(this.Item2, item2));
      Contract.Ensures(Object.Equals(this.Item3, item3));
      Contract.Ensures(Object.Equals(this.Item4, item4));
      Contract.Ensures(Object.Equals(this.Item5, item5));
      Contract.Ensures(Object.Equals(this.Item6, item6));
    }

    extern public T1 Item1
    {
      get;
    }
    extern public T2 Item2
    {
      get;
    }
    extern public T3 Item3
    {
      get;
    }
    extern public T4 Item4
    {
      get;
    }
    extern public T5 Item5
    {
      get;
    }
    extern public T6 Item6
    {
      get;
    }

    extern int ITuple.Size { get; }
  }
  public class Tuple<T1, T2, T3, T4, T5, T6, T7> : ITuple
  {
    public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
    {
      Contract.Ensures(Object.Equals(this.Item1, item1));
      Contract.Ensures(Object.Equals(this.Item2, item2));
      Contract.Ensures(Object.Equals(this.Item3, item3));
      Contract.Ensures(Object.Equals(this.Item4, item4));
      Contract.Ensures(Object.Equals(this.Item5, item5));
      Contract.Ensures(Object.Equals(this.Item6, item6));
      Contract.Ensures(Object.Equals(this.Item7, item7));
    }

    extern public T1 Item1
    {
      get;
    }
    extern public T2 Item2
    {
      get;
    }
    extern public T3 Item3
    {
      get;
    }
    extern public T4 Item4
    {
      get;
    }
    extern public T5 Item5
    {
      get;
    }
    extern public T6 Item6
    {
      get;
    }
    extern public T7 Item7
    {
      get;
    }

    extern int ITuple.Size { get; }
  }
  public class Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> : ITuple
  {
    public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, TRest item8)
    {
      Contract.Ensures(Object.Equals(this.Item1, item1));
      Contract.Ensures(Object.Equals(this.Item2, item2));
      Contract.Ensures(Object.Equals(this.Item3, item3));
      Contract.Ensures(Object.Equals(this.Item4, item4));
      Contract.Ensures(Object.Equals(this.Item5, item5));
      Contract.Ensures(Object.Equals(this.Item6, item6));
      Contract.Ensures(Object.Equals(this.Item7, item7));
      Contract.Ensures(Object.Equals(this.Rest, item8));
    }

    extern public T1 Item1
    {
      get;
    }
    extern public T2 Item2
    {
      get;
    }
    extern public T3 Item3
    {
      get;
    }
    extern public T4 Item4
    {
      get;
    }
    extern public T5 Item5
    {
      get;
    }
    extern public T6 Item6
    {
      get;
    }
    extern public T7 Item7
    {
      get;
    }
    extern public TRest Rest
    {
      get;
    }

    extern int ITuple.Size { get; }
  }
}

#endif