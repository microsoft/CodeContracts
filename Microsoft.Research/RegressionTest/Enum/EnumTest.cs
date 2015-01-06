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
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Microsoft.Research.ClousotRegression;

namespace EnumInfo
{
  public class UseMyEnum
  {
    public enum ThreeValues
    {
      One, Three, Four
    }
    
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=42,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=8,MethodILOffset=38)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=8,MethodILOffset=40)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=8,MethodILOffset=36)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Assignment to an enum value ok",PrimaryILOffset=14,MethodILOffset=0)]
    static public int  NoCaseForgotten(ThreeValues t)
    {
      Contract.Ensures(Contract.Result<int>() > 0);
      switch(t)
      {
        case ThreeValues.One:
          return 1;

        case ThreeValues.Four:
          return 4;

        case ThreeValues.Three:
          return 3;

        default:
          Contract.Assert(false); // unreachable, but we report it as valid as it is "false"
          break;
      }

      return -1; // unreachable
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=8,MethodILOffset=36)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=8,MethodILOffset=38)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"ensures is valid",PrimaryILOffset=8,MethodILOffset=40)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Assignment to an enum value ok",PrimaryILOffset=14,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Bottom,Message=@"ensures unreachable",PrimaryILOffset=8,MethodILOffset=42)]
    static public int NoCaseForgottenNoDefault(ThreeValues t)
    {
      Contract.Ensures(Contract.Result<int>() > 0);
      switch (t)
      {
        case ThreeValues.One:
          return 1;

        case ThreeValues.Four:
          return 4;

        case ThreeValues.Three:
          return 3;
      }

      return -1; // unreachable
    }

    public enum Days { Sun = 0, Mon = 1, Tue = 2, Wed = 3, Thu = 4, Fri = 5, Sat = 6}

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message=@"assert is false",PrimaryILOffset=51,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Assignment to an enum value ok",PrimaryILOffset=1,MethodILOffset=0)]    
    public string Translate(Days d)
    {
      switch (d)
      {
        case Days.Sun:
        case Days.Tue:
        case Days.Thu:
        case Days.Fri:
          return "Even";

        case Days.Mon:
        case Days.Sat:
          return "Odd";

        default:
          Contract.Assert(false);     // we forgot one case!
          return null;
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=7,MethodILOffset=0)]
    public void Silly(Days s)
    {
      Contract.Assert(((int)s) >= 0); // true as we assume that enums can only take their initial values
    }

    [Flags] public enum State { Read= 0x1, Write = 0x10, Close=0x11, Open=0x100}

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=58,MethodILOffset=0)]   
    public int SillyCount(State s)
    {
      int count = 0;

      if ((s & State.Read) == State.Read)
        count++;
      if ((s & State.Write) == State.Write)
        count++;
      if ((s & State.Close) == State.Close)
        count++;
      if ((s & State.Open) == State.Open)
        count++;

      Contract.Assert((int)s > 0); // it is a bitmask, it may be false

      return count;
    }

   public enum MachineState
    {
      PowerOff = 0,
      Running = 5,
      Sleeping = 10,
      Hibernating = Sleeping + 5
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=17,MethodILOffset=0)]
    public void AssertOnNonConsecutiveEnums(MachineState f)
    {
      if (f != MachineState.PowerOff && f != MachineState.Running && f != MachineState.Hibernating)
      {
        Contract.Assert(f == MachineState.Sleeping); // True
      }
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven. The static checker determined that the condition 'f == 10' should hold on entry. Nevertheless, the condition may be too strong for the callers. If you think it is ok, add a precondition to document it: Contract.Requires(f == 10);",PrimaryILOffset=12,MethodILOffset=0)]
    public void AssertOnNonConsecutiveEnumsWrong(MachineState f)
    {
      if (f != MachineState.PowerOff && f != MachineState.Running )
      {
        Contract.Assert(f == MachineState.Sleeping);
      }
    }

    [ClousotRegressionTest]
     [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=20,MethodILOffset=0)]
    public void AssertOnNonConsecutiveEnums_Disjunction(MachineState f)
    {
      if (f != MachineState.PowerOff && f != MachineState.Running)
      {
        Contract.Assert(f == MachineState.Sleeping || f == MachineState.Hibernating);
      }
    }
  }
}


 namespace EnumIsDefined
  {
    using System;

    [Flags]
    public enum PetType
    {
      None = 0, Dog = 1, Cat = 2, Rodent = 4, Bird = 8, Reptile = 16, Other = 32
    };

    public class Example
    {
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=23,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=55,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=82,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=110,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=137,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=167,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=198,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=230,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"assert unproven",PrimaryILOffset=266,MethodILOffset=0)]
      public static void Test()
      {
        object value;

        // Call IsDefined with underlying integral value of member.
        value = 1;
        Contract.Assert(Enum.IsDefined(typeof(PetType), value));
        
        // Call IsDefined with invalid underlying integral value.
        value = 64;
        Contract.Assert(!Enum.IsDefined(typeof(PetType), value));
       
	// Call IsDefined with string containing member name.
        value = "Rodent";
        Contract.Assert(Enum.IsDefined(typeof(PetType), value));
        
        // Call IsDefined with a variable of type PetType.
        value = PetType.Dog;
        Contract.Assert(Enum.IsDefined(typeof(PetType), value));

        // Call IsDefined with uppercase member name.      
        value = "None";
        Contract.Assert(Enum.IsDefined(typeof(PetType), value));

        value = "NONE";
        Contract.Assert(!Enum.IsDefined(typeof(PetType), value));


        value = PetType.Dog | PetType.Cat;
        Contract.Assert(!Enum.IsDefined(typeof(PetType), value));

        value = PetType.Dog | PetType.Bird;
        Contract.Assert(!Enum.IsDefined(typeof(PetType), value));

        // TODO F: We need to propagate strings for this
        value = PetType.Reptile;
        value = value.ToString();
        Contract.Assert(Enum.IsDefined(typeof(PetType), value));
      }
    }

    public class Examples
    {
      
      public enum ItalianBikeBrand { DeRosa, Colnago, Pinarello, Daccordi }

      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=30,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Assignment to an enum value ok",PrimaryILOffset=4,MethodILOffset=0)]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Assignment to an enum value ok",PrimaryILOffset=8,MethodILOffset=0)]
      public void CityFor(bool b)
      {
	ItalianBikeBrand bike;
	if (b)
	  {
	    bike = ItalianBikeBrand.Colnago;
	  }
	else
	  {
	    bike = ItalianBikeBrand.DeRosa;
	  }
	
	Contract.Assert(Enum.IsDefined(typeof(ItalianBikeBrand), bike));
      }
  
      [ClousotRegressionTest]
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"assert is valid",PrimaryILOffset=41,MethodILOffset=0)]      
      [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Assignment to an enum value ok",PrimaryILOffset=7,MethodILOffset=0)]
      public int BikeFor(int x)
      {
	switch (Foo(x)) // We should materialize the DisInterval with the values of ItalianBikeBrand for the return value
	  {
	  case ItalianBikeBrand.Colnago:
	    return 1;
	    
	  case ItalianBikeBrand.Daccordi:
	    return 2;
	    
	  case ItalianBikeBrand.DeRosa:
	    return 3;

	  case ItalianBikeBrand.Pinarello:
	    return 4;
	  }
	
	Contract.Assert(false); // Should be unreachable, and we report it as "valid" because it is an "assert false"
	return -1;
      }
      
      public ItalianBikeBrand Foo(int x)
      {
	return (ItalianBikeBrand)x;
      }
      
    }
 }

namespace IsDefinedAnalysis
{
  public enum Bikes { DeRosa, Specialized, Trek, Daccordi, Colnago }

  public class MyClass
  {
    public Bikes myBike;

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Assignment to an enum value ok",PrimaryILOffset=28,MethodILOffset=0)]
    public void SetBikeField(int x)
    {
      Contract.Requires(Enum.IsDefined(typeof(Bikes), x));

      this.myBike = (Bikes) x;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"The assigned value may not be in the range defined for this enum value",PrimaryILOffset=2,MethodILOffset=0)]
    public void SetBikeFieldNoPre(int x)
    {
      this.myBike = (Bikes)x;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Assignment to an enum value ok",PrimaryILOffset=27,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Returned value is in the enum range",PrimaryILOffset=0,MethodILOffset=0)]
	public static Bikes SetBikeLocal(int x)
    {
      Contract.Requires(Enum.IsDefined(typeof(Bikes), x));
      
      var myBike = (Bikes)x;
      
      return myBike;
    }

	// Ok to have a warning, because we have no guarantees that we return a value in Bikes
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"The assigned value may not be in the range defined for this enum value",PrimaryILOffset=1,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="The returned value may not be in the range defined for this enum value",PrimaryILOffset=0,MethodILOffset=0)]
    public static Bikes SetBikeLocalNoPre(int x)
    {
      var myBike = (Bikes)x;

      return myBike;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"The assigned value may not be in the range defined for this enum value",PrimaryILOffset=1,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Returned value is in the enum range",PrimaryILOffset=0,MethodILOffset=0)]
    public static Bikes SetBikeLocalNoPreAndAssumption(int x)
    {
      var myBike = (Bikes)x;

	  Contract.Assume(Enum.IsDefined(typeof(Bikes), myBike));
	  
      return myBike;
    }
	
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=29,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Array access might be above the upper bound",PrimaryILOffset=29,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Assignment to an enum value ok",PrimaryILOffset=29,MethodILOffset=0)]
    public static void SetBikeArray(Bikes[] bikes, int x)
    {
      Contract.Requires(Enum.IsDefined(typeof(Bikes), x));

      bikes[0] = (Bikes)x;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Lower bound access ok",PrimaryILOffset=3,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"Array access might be above the upper bound",PrimaryILOffset=3,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"The assigned value may not be in the range defined for this enum value",PrimaryILOffset=3,MethodILOffset=0)]
    public static void SetBikeArrayNoPre(Bikes[] bikes, int x)
    {
      bikes[0] = (Bikes)x;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Assignment to an enum value ok",PrimaryILOffset=32,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Assignment to an enum value ok",PrimaryILOffset=35,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=21,MethodILOffset=27)]
    public void SetBikeReturnValue(int x)
    {
      Contract.Requires(Enum.IsDefined(typeof(Bikes), x));
      
      var z = SetBikeLocal(x);

      this.myBike = z;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Assignment to an enum value ok",PrimaryILOffset=6,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Assignment to an enum value ok",PrimaryILOffset=9,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.Top,Message=@"requires unproven: Enum.IsDefined(typeof(Bikes), x)",PrimaryILOffset=21,MethodILOffset=1)]
    public void SetBikeReturnValueNoPre(int x)
    {
      var z = SetBikeLocal(x);

      this.myBike = z;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Assignment to an enum value ok",PrimaryILOffset=2,MethodILOffset=0)]
    public void SetBikeFromArgument(Bikes bike)
    {
      this.myBike = bike;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Assignment to an enum value ok",PrimaryILOffset=7,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Assignment to an enum value ok",PrimaryILOffset=10,MethodILOffset=0)]
    public void SetBikeFromAnotherBike(bool b)
    {
      Bikes tmpBike = b ? Bikes.Daccordi : Bikes.Specialized;
 
      this.myBike = tmpBike;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Assignment to an enum value ok",PrimaryILOffset=1,MethodILOffset=0)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Assignment to an enum value ok",PrimaryILOffset=4,MethodILOffset=0)]
    public void SetBikeFromAnotherBike()
    {
      var tmpBike = Bikes.Daccordi;
      this.myBike = tmpBike;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Assignment to an enum value ok",PrimaryILOffset=2,MethodILOffset=0)]
    public void SetBikeFromConstant()
    {
      this.myBike = (Bikes)2;
    }

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"Assignment to an enum value ok",PrimaryILOffset=2,MethodILOffset=0)]
    public void SetBikeFromEnumConstant()
    {
      this.myBike = Bikes.Daccordi;
    }
  }
}

namespace FromSystemDLL
{
  public class CodeTypeMember
  {
    public MemberAttributes attributes;

    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.False,Message=@"The assigned value is not one of those defined for this enum. Forgotten [Flag] in the enum definition?",PrimaryILOffset=12,MethodILOffset=0)]
    public CodeTypeMember()
    {
      this.attributes = MemberAttributes.Private | MemberAttributes.Final;
    }

    public enum MemberAttributes
    {
      Abstract = 1,
      AccessMask = 61440,
      Assembly = 4096,
      Const = 5,
      Family = 12288,
      FamilyAndAssembly = 8192,
      FamilyOrAssembly = 16384,
      Final = 2,
      New = 16,
      Overloaded = 256,
      Override = 4,
      Private = 20480,
      Public = 24576,
      ScopeMask = 15,
      Static = 3,
      VTableMask = 240
    }
  }
}


namespace CallAndReturn
{

public class EnumAtCall
  {
    public enum Language { C, Cpp, CSharp, VB }

	[ClousotRegressionTest]
    public string CallMeWithALanguage(Language l)
    {
      return l.ToString();
    }

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Actual parameter is in the enum range",PrimaryILOffset=2,MethodILOffset=0)]
    public void CallWithCpp()
    {
      CallMeWithALanguage(Language.Cpp);
    }

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Actual parameter is in the enum range",PrimaryILOffset=5,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Actual parameter is in the enum range",PrimaryILOffset=14,MethodILOffset=0)]
    public void CallWithUnsafeLanguage(bool nonDet)
    {
      if (nonDet)
        CallMeWithALanguage(Language.C);
      else
        CallMeWithALanguage(Language.Cpp);
    }

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="The actual value may not be in the range defined for this enum value",PrimaryILOffset=2,MethodILOffset=0)]
    public void CallMeWithSomeInt(int x)
    {
      CallMeWithALanguage((Language)x);
    }

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Returned value is in the enum range",PrimaryILOffset=0,MethodILOffset=0)]
    public Language ReturnVB()
    {
      return Language.VB;
    }

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="The returned value may not be in the range defined for this enum value",PrimaryILOffset=0,MethodILOffset=0)]
    public Language ReturnSomething(bool b, int t)
    {
      if (b)
        return Language.CSharp;

      return (Language)t;
    }
  }
}


namespace UserRepro
{
  public class PropertyNameAndType
  {
    public string Name { get; set; }
  }

  class BrettShearer
  {
    [ClousotRegressionTest]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=13,MethodILOffset=60)]
    [RegressionOutcome(Outcome=ProofOutcome.True,Message=@"requires is valid",PrimaryILOffset=45,MethodILOffset=60)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Actual parameter is in the enum range",PrimaryILOffset=60,MethodILOffset=0)]
    static void AddColumns(IEnumerable<PropertyNameAndType> properties, string prefix, int depth)
    {
      var tablePrefix = prefix;
      if (properties != null)
      {
        foreach (var property in properties)
        {
          Contract.Assume(property != null);
          Contract.Assume(property.Name != null);
          var x = property.Name.EndsWith("_IsValid", StringComparison.Ordinal);
        }
      }
    }
  }

  public class CloudDevRepro
  {
    public enum EdgeLabels
    {
      Compiler = 0,
      Sources = 1,
      References = 2,
      OutputAssembly = 3,
      OutputPdb = 4,
    }

	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=9,MethodILOffset=7)]
    public void CallExpectPositive()
    {
      ExpectPositive(EdgeLabels.References);
    }

    private void ExpectPositive(object o)
    {
      Contract.Requires((int)o > 0);
    }
	
	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="requires is valid",PrimaryILOffset=14,MethodILOffset=2)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Actual parameter is in the enum range",PrimaryILOffset=2,MethodILOffset=0)]
	public void CallExpectPositiveWithGenerics()
    {
      ExpectPositiveWithGenerics(EdgeLabels.References);
    }

    private void ExpectPositiveWithGenerics<T>(T o)
    {
      Contract.Requires((int)(object)o > 0);
    }
  }  
 
   class ReturnValues
   {

	// TODO: get the right source context for "ret"
	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Returned value is in the enum range",PrimaryILOffset=0,MethodILOffset=0)]
    public FilterSetStatus ConvertWithIf(int value)
    {
      if (Enum.IsDefined(typeof(FilterSetStatus), value))
        return (FilterSetStatus)value;
      else
        return FilterSetStatus.Undefined;
    }

	// TODO: get the right source context for "ret"
	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="The returned value may not be in the range defined for this enum value",PrimaryILOffset=0,MethodILOffset=0)]
    public FilterSetStatus ConvertWithIfWrong(int value)
    {
      if (Enum.IsDefined(typeof(FilterSetStatus), value + 1))
        return (FilterSetStatus)value;
      else
        return FilterSetStatus.Undefined;
    }

	// TODO: get the right source context for "ret"
	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Returned value is in the enum range",PrimaryILOffset=0,MethodILOffset=0)]
    public FilterSetStatus Convert(int value)
    {
      return Enum.IsDefined(typeof(FilterSetStatus), value) ? (FilterSetStatus)value : FilterSetStatus.Undefined;
    }

	// TODO: get the right source context for "ret"
	[ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.Top,Message="The returned value may not be in the range defined for this enum value",PrimaryILOffset=0,MethodILOffset=0)]
    public FilterSetStatus ConvertWrong(int value)
    {
      return Enum.IsDefined(typeof(FilterSetStatus), value + 1) ? (FilterSetStatus)value : FilterSetStatus.Undefined;
    }
  }
 
  public enum FilterSetStatus
  {
     Undefined = 2,
     Foo = 1234,
     Bar = 4567,
  } 
}

namespace TestAssumptionsInTheEnumAnalysis
{
   class Test
   {
    public enum SomeEnum { A = -12, B = 0, C = 3, D = 7 }

    [ClousotRegressionTest]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Lower bound access ok",PrimaryILOffset=39,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Upper bound access ok",PrimaryILOffset=39,MethodILOffset=0)]
	[RegressionOutcome(Outcome=ProofOutcome.True,Message="Assignment to an enum value ok",PrimaryILOffset=40,MethodILOffset=0)]
    public int Switch(SomeEnum[] a, int i)
    {
      Contract.Requires(a != null);
      Contract.Requires(i >= 0);
      Contract.Requires(i < a.Length);

      var count = 0;

      //for (var i = 0; i < a.Length; i++)
      {
        switch (a[i])
        {
          case SomeEnum.A:
          case SomeEnum.B:
            count += 1;
            break;

          case SomeEnum.C:
          case SomeEnum.D:
            count += 2;
            break;

          default:
            break;
        }
      }
      return count;
    }
	}
	
	  public class RedudantPre
  {
    public enum SomeValue { One, Two, Three }

	[ClousotRegressionTest]
	// No output, as we avoid checking the precondition, if it is redundant
    public static int GetSomeValue(SomeValue s)
    {
      Contract.Requires(Enum.IsDefined(typeof(SomeValue), s));

      return s.GetHashCode();
    }
  }

}