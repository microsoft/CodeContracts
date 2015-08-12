// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Research.AbstractDomains.Expressions;
using System.Diagnostics.Contracts;

namespace Microsoft.Research.AbstractDomains.Numerical
{
    public class LinearEquations<Variable, Expression>
    {
        private SparseRationalArray[] matrix;                                               // The equations
        private List<LinearEqualitiesEnvironment<Variable, Expression>> sharinglist;        // The list of linearequalities environments sharing those equations

        public LinearEquations(LinearEqualitiesEnvironment<Variable, Expression> parent, int equationsCount)
        {
            Contract.Requires(equationsCount >= 0);

            matrix = new SparseRationalArray[equationsCount];
            sharinglist = new List<LinearEqualitiesEnvironment<Variable, Expression>>() { parent };
        }

        public LinearEquations(LinearEqualitiesEnvironment<Variable, Expression> parent, SparseRationalArray[] matrix)
        {
            this.matrix = matrix;
            sharinglist = new List<LinearEqualitiesEnvironment<Variable, Expression>>() { parent };
        }

        [Pure]
        public Rational At(int i, int j)
        {
            Contract.Requires(i >= 0);
            Contract.Requires(j >= 0);

            return matrix[i][j];
        }

        [Pure]
        public IEnumerable<KeyValuePair<int, Rational>> GetElementsForRow(int i)
        {
            Contract.Requires(i >= 0);

            return matrix[i].GetElements();
        }

        public void CopyRowTo(int from, SparseRationalArray to, int len)
        {
            to.CopyFrom(matrix[from], len);
        }

        [Pure]
        public int RowLength(int row)
        {
            return matrix[row].Length;
        }

        [Pure]
        public int RowCount(int row)
        {
            return matrix[row].Count;
        }

        [Pure]
        public bool IsNullRow(int row)
        {
            return matrix[row] == null;
        }

        [Pure]
        public bool IsConstantRow(int row)
        {
            return matrix[row].IsConstantRow();
        }

        [Pure]
        public bool IsConstantRow(int rowIndex, out int dim)
        {
            dim = -1;

            var row = matrix[rowIndex];

            foreach (var pair in row.GetElements())
            {
                if (pair.Key < row.Length - 1)
                {
                    if (dim >= 0)
                    { // We already saw a coefficient
                        return false;
                    }
                    else
                    {
                        dim = pair.Key;
                    }
                }
            }

            return dim >= 0;
        }

        [Pure]
        public bool IsXeqYRow(int rowIndex)
        {
            var row = matrix[rowIndex];

            if (row.NonDefaultElementsCount != 2)
            {
                return false;
            }

            var k1 = default(Rational);
            var k2 = default(Rational);
            var k1Set = false;
            var k2Set = false;

            foreach (var pair in row.GetElements())
            {
                if (pair.Key == row.Length - 1)
                {
                    // To make the code more robust
                    if (pair.Value != 0)
                        return false;
                }

                if (pair.Value.IsMinValue || Rational.Abs(pair.Value) != 1)
                {
                    return false;
                }
                if (!k1Set)
                {
                    k1 = pair.Value;
                    k1Set = true;
                    continue;
                }

                if (!k2Set)
                {
                    k2 = pair.Value;
                    k2Set = true;
                    break;
                }
            }

            return (k1 * k2) == -1;
        }

        [Pure]
        public bool IsIndexOfNonDefaultElement(int row, int col, out Rational v)
        {
            return matrix[row].IsIndexOfNonDefaultElement(col, out v);
        }

        [Pure]
        public bool IsZeroCol(int rows, int col)
        {
            for (int row = 0; row < rows; row++)
            {
                if (matrix[row] == null)
                {
                    continue;
                }

                if (matrix[row].IsIndexOfNonDefaultElement(col))
                {
                    return false;
                }
            }
            return true;
        }


        public void ZeroCol(LinearEqualitiesEnvironment<Variable, Expression> env, int col, ref LinearEquations<Variable, Expression> equations)
        {
            if (sharinglist.Count == 1)
            {
                ZeroCol(matrix, col);

                equations = this;
            }
            else
            {
                var result = this.Unshare(env, ref equations);
                ZeroCol(result, col);

                equations = new LinearEquations<Variable, Expression>(env, result);
            }
        }

        private static void ZeroCol(SparseRationalArray[] who, int col)
        {
            for (int row = 0; row < who.Length; row++)
            {
                var currRow = who[row];
                if (currRow == null)
                {
                    continue;
                }

                // This is just a quick way of doing: matrix[row][col] = 0;
                currRow.ResetToDefaultElement(col);
            }
        }

        public bool AreSharingTheEquations(LinearEqualitiesEnvironment<Variable, Expression> left, LinearEqualitiesEnvironment<Variable, Expression> right)
        {
            return sharinglist.Contains(left) && sharinglist.Contains(right);
        }

        public LinearEquations<Variable, Expression> AddSharing(LinearEqualitiesEnvironment<Variable, Expression> newSharing)
        {
            Contract.Ensures(Contract.Result<LinearEquations<Variable, Expression>>() != null);

            sharinglist.Add(newSharing);

            return this;
        }

        /// <summary>
        /// If <code>who</code>is the only one sharing this set of equations, then we simply return the set of equations
        /// Otherwise, we create a copy of the set of equations, we remove <code>who</code> from the sharing list of those equations, and
        /// we update equations
        /// </summary>
        /// <param name="equations">The new set of linear equations</param>
        public SparseRationalArray[] Unshare(LinearEqualitiesEnvironment<Variable, Expression> who, ref LinearEquations<Variable, Expression> equations)
        {
            Contract.Ensures(Contract.Result<SparseRationalArray[]>() != null);
            Contract.Ensures(Contract.Result<SparseRationalArray[]>().Length == matrix.Length);

            lock (sharinglist)
            {
                if (sharinglist.Count > 1)
                {
                    sharinglist.Remove(who);

                    var result = this.GetClonedEquations();

                    equations = new LinearEquations<Variable, Expression>(who, result);

                    return result;
                }
                else
                { // Nothing to do, we just return the previous ones 
                    equations = this;
                    return matrix;
                }
            }
        }

        public SparseRationalArray[] GetClonedEquations()
        {
            var result = new SparseRationalArray[matrix.Length];

            for (int row = 0; row < matrix.Length; row++)
            {
                var currRow = matrix[row];
                if (currRow != null)
                {
                    result[row] = SparseRationalArray.New(currRow.Length, 0);
                    result[row].CopyFrom(currRow, currRow.Length);
                }
                else
                {
                    result[row] = null;
                }
            }

            return result;
        }

        #region Statistics

        public string Statistics()
        {
            return string.Format("matrix length: {0}", matrix.Length);
        }

        #endregion
    }
}
