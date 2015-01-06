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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Glee;
using Microsoft.Glee.Optimization;

namespace SimplexTest {
    class Program {
        static void Main(string[] args) {
            {
//                for (int i = 0; i < 30000; i++){
//                    var str = new FileStream("c:\\tmp\\lp",
//                                             FileMode.Open);
//
//                    var bf = new BinaryFormatter();
//
//                    var lp = (RevisedSimplexMethod) bf.Deserialize(str);
//                    var sol = lp.MinimalSolution();
//                    Console.WriteLine("lp status is {0}", lp.Status);
//                    foreach (var x in sol)
//                        Console.Write(x + ",");
//                    Console.WriteLine("\n==========================");
//                    str.Close();
//                }
            }
            {
//                var lp = LpFactory.CreateLP();
//
//                lp.AddConstraint(new double[] { 3,  }, Relation.GreaterOrEqual, 1);
//                
//                lp.InitCosts(new double[] { 1 });
//                var sol = lp.MinimalSolution();
//                Console.WriteLine("lp status is {0}", lp.Status);
//                foreach (var x in sol)
//                    Console.Write(x + ",");
//                Console.WriteLine("\n==========================");
//               
            }
            {
                var lp = LpFactory.CreateLP();

                lp.AddConstraint(new double[] { 1, 3, 1 }, Relation.LessOrEqual, 3);
                lp.AddConstraint(new double[] { -1, 0, 3 }, Relation.LessOrEqual, 2);
                lp.AddConstraint(new double[] { 2, -1, 2 }, Relation.LessOrEqual, 4);
                lp.AddConstraint(new double[] { 2, 3, -1 }, Relation.LessOrEqual, 2);
                for (int i = 0; i < 3; i++)
                    lp.LimitVariableFromBelow(i, 0);

                lp.InitCosts(new double[] { -5, -5, -3 });
                var sol = lp.MinimalSolution();
                Console.WriteLine("lp status is {0}", lp.Status);
                foreach (var x in sol)
                    Console.Write(x + ",");
                Console.WriteLine("\n==========================");

                lp.InitCosts(new double[] { -1, -4, -3 });
                sol = lp.MinimalSolution();
                foreach (var x in sol)
                    Console.Write(x + ",");
                Console.WriteLine("\n==========================");


                lp = LpFactory.CreateLP();

                lp.AddConstraint(new double[] { 1, 3, 1 }, Relation.LessOrEqual, 3);
                lp.AddConstraint(new double[] { -1, 0, 3 }, Relation.LessOrEqual, 2);
                lp.AddConstraint(new double[] { 2, -1, 2 }, Relation.LessOrEqual, 4);
                lp.AddConstraint(new double[] { 2, 3, -1 }, Relation.LessOrEqual, 2);
                for (int i = 0; i < 3; i++)
                    lp.LimitVariableFromBelow(i, 0);

                lp.InitCosts(new double[] { -1, -4, -3 });
                foreach (var x in sol)
                    Console.Write(x + ",");
                Console.WriteLine("\n==========================");

            }
        }
    }
}
