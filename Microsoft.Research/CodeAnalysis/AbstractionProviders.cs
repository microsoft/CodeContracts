// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

//#define DEBUGSTACK

using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;

using Microsoft.Research.DataStructures;

using StackTemp = System.Int32;

namespace Microsoft.Research.CodeAnalysis
{
    using SubroutineContext = FList<Microsoft.Research.DataStructures.STuple<CFGBlock, CFGBlock, string>>;
    using SubroutineEdge = Microsoft.Research.DataStructures.STuple<CFGBlock, CFGBlock, string>;
    using System.Diagnostics.Contracts;


    /// <summary>
    /// Turns any type T that is Equatable into a flat abstract domain
    /// </summary>
    /// <typeparam name="T">Elements in the domain besides Top and Bottom</typeparam>
    public struct FlatDomain<T> : IEquatable<FlatDomain<T>>, IAbstractValue<FlatDomain<T>>
      where T : IEquatable<T>
    {
        /// <summary>
        /// Make sure that Top = 0 so default(FlatDomain) is Top
        /// </summary>
        private enum State { Top = 0, Bottom, Normal }

        private FlatDomain(State state)
        {
            this.state = state;
            this.Value = default(T);
        }

        public FlatDomain(T/*!*/ value)
        {
            this.Value = value;
            state = State.Normal;
        }

        private readonly State state;
        public readonly T Value;

        public static readonly FlatDomain<T> BottomValue = new FlatDomain<T>(State.Bottom); // Thread-safe if T is thread-safe
        public static readonly FlatDomain<T> TopValue = new FlatDomain<T>(State.Top); // Thread-safe if T is thread-safe


        public static implicit operator FlatDomain<T>(T/*!*/ value) { return new FlatDomain<T>(value); }

        public bool IsNormal { get { return state == State.Normal; } }

        #region IAbstractValue<LiftedDomain<T>> Members

        public FlatDomain<T> Top
        {
            get { return FlatDomain<T>.TopValue; }
        }

        public FlatDomain<T> Bottom
        {
            get { return FlatDomain<T>.BottomValue; }
        }

        public bool IsTop
        {
            get
            {
                return state == State.Top;
            }
        }

        public bool IsBottom
        {
            get
            {
                return state == State.Bottom;
            }
        }

        public FlatDomain<T> ImmutableVersion()
        {
            return this;
        }

        public FlatDomain<T> Clone()
        {
            return this;
        }

        public FlatDomain<T> Join(FlatDomain<T> newState, out bool weaker, bool widen)
        {
            if (this.IsTop) { weaker = false; return this; }
            if (newState.IsTop) { weaker = !this.IsTop; return newState; }
            if (this.IsBottom) { weaker = !newState.IsBottom; return newState; }
            if (newState.IsBottom) { weaker = false; return this; }

            if (this.Value.Equals(newState.Value)) { weaker = false; return newState; }
            weaker = true;
            return TopValue;
        }

        public FlatDomain<T> Meet(FlatDomain<T> that)
        {
            if (this.IsTop) return that;
            if (that.IsTop) return this;
            if (this.IsBottom) return this;
            if (that.IsBottom) return that;

            if (this.Value.Equals(that.Value)) return that;
            return BottomValue;
        }

        public void Dump(TextWriter tw)
        {
            if (this.IsTop) { tw.WriteLine("Top"); }
            else if (this.IsBottom) { tw.WriteLine("Bot"); }
            else
            {
                tw.WriteLine("<{0}>", Value);
            }
        }

        public bool LessEqual(FlatDomain<T> that)
        {
            if (that.IsTop) return true;
            if (this.IsBottom) return true;
            if (this.IsTop) return false;
            if (that.IsBottom) return false;
            return this.Value.Equals(that.Value);
        }

        #endregion

        public override string ToString()
        {
            if (this.IsTop) { return ("Top"); }
            else if (this.IsBottom) { return ("Bot"); }
            else
            {
                // invariant IsNormal => Value != null
                //^ assume Value != null;
                return Value.ToString();
            }
        }
        #region IEquatable<LiftedDomain<T>> Members

        //^ [StateIndependent]
        public bool Equals(FlatDomain<T> other)
        {
            return state == other.state && (!this.IsNormal || this.Value.Equals(other.Value));
        }

        #endregion
    }

    public struct EnvironmentDomain<Key, Val> : IAbstractValue<EnvironmentDomain<Key, Val>>
      where Val : IAbstractValue<Val>
    {
#if false //unused?
        public static bool Debug;
#endif

        private readonly IFunctionalMap<Key, Val>/*?*/ map;

        public EnvironmentDomain(IFunctionalMap<Key, Val>/*?*/ value)
        {
            map = value;
        }

        [ThreadStatic] // Current implementation: (only) depends on the analysis driver used
        private static Converter<Key/*!*/, int> KeyNumber;

        public static EnvironmentDomain<Key, Val> TopValue(Converter<Key/*!*/, int> keyNumber)
        {
            if (KeyNumber == null) { KeyNumber = keyNumber; }
            return new EnvironmentDomain<Key, Val>(FunctionalIntKeyMap<Key, Val>.Empty(keyNumber));
        }

        public EnvironmentDomain<Key, Val> Add(Key key, Val val)
        {
            if (map == null)
            {
                throw new InvalidOperationException();
            }

            return new EnvironmentDomain<Key, Val>(map.Add(key, val));
        }

        public EnvironmentDomain<Key, Val> Remove(Key key)
        {
            if (map == null)
            {
                throw new InvalidOperationException();
            }

            return new EnvironmentDomain<Key, Val>(map.Remove(key));
        }

        public bool Contains(Key/*!*/ key)
        {
            if (map == null)
            {
                throw new InvalidOperationException();
            }

            return map.Contains(key);
        }

        public Val/*?*/ this[Key/*!*/ key] { get { if (map == null) return default(Val); return map[key]; } }

        public IEnumerable<Key/*!*/> Keys { get { return map.Keys; } }

        public static readonly EnvironmentDomain<Key, Val> BottomValue = new EnvironmentDomain<Key, Val>(null); // Thread-safe

        #region IAbstractValue<EnvironmentDomain<Key,Val>> Members

        public EnvironmentDomain<Key, Val> Top
        {
            get
            {
                return EnvironmentDomain<Key, Val>.TopValue(KeyNumber);
            }
        }

        public EnvironmentDomain<Key, Val> Bottom
        {
            get { return new EnvironmentDomain<Key, Val>(null); }
        }

        public bool IsTop
        {
            get
            {
                return map != null && map.Count == 0;
            }
        }

        public bool IsBottom
        {
            get
            {
                return map == null;
            }
        }

        public EnvironmentDomain<Key, Val> ImmutableVersion()
        {
            return this;
        }

        public EnvironmentDomain<Key, Val> Clone()
        {
            return this;
        }

        public EnvironmentDomain<Key, Val> Join(EnvironmentDomain<Key, Val> newState, out bool weaker, bool widen)
        {
            if (map == newState.map) { weaker = false; return this; }

            bool resultWeaker = false;
            if (this.IsTop) { weaker = false; return this; }
            if (newState.IsTop) { weaker = !this.IsTop; return newState; }
            if (this.IsBottom) { weaker = !newState.IsBottom; return newState; }
            if (newState.IsBottom) { weaker = false; return this; }

            // compare pointwise
            IFunctionalMap<Key, Val> smaller;
            IFunctionalMap<Key, Val> larger;
            if (map.Count < newState.map.Count)
            {
                smaller = map;
                larger = newState.map;
            }
            else
            {
                smaller = newState.map;
                larger = map;
            }
            IFunctionalMap<Key, Val> result = smaller;
            foreach (Key k in smaller.Keys)
            {
                if (!larger.Contains(k))
                {
                    result = result.Remove(k);
                }
                else
                {
                    bool joinWeaker;
                    Val join = smaller[k].Join(larger[k], out joinWeaker, widen);
                    if (joinWeaker)
                    {
                        resultWeaker = true;
                        if (join.IsTop)
                        {
                            result = result.Remove(k);
                        }
                        else
                        {
                            result = result.Add(k, join);
                        }
                    }
                }
            }
            weaker = resultWeaker || (result.Count < map.Count);
            return new EnvironmentDomain<Key, Val>(result);
        }

        public EnvironmentDomain<Key, Val> Meet(EnvironmentDomain<Key, Val> that)
        {
            if (map == that.map) { return this; }

            if (this.IsTop) return that;
            if (that.IsTop) return this;
            if (this.IsBottom) return this;
            if (that.IsBottom) return that;

            Contract.Assume(map != null);

            // compare pointwise
            IFunctionalMap<Key, Val> smaller;
            IFunctionalMap<Key, Val> larger;
            if (map.Count < that.map.Count)
            {
                smaller = map;
                larger = that.map;
            }
            else
            {
                smaller = that.map;
                larger = map;
            }
            IFunctionalMap<Key, Val> result = larger;
            foreach (Key k in smaller.Keys)
            {
                if (larger.Contains(k))
                {
                    // must meet the values
                    Val meet = smaller[k].Meet(larger[k]);
                    result = result.Add(k, meet);
                }
                else
                {
                    // just add the value to the result
                    result = result.Add(k, smaller[k]);
                }
            }

            return new EnvironmentDomain<Key, Val>(result);
        }

        public void Dump(TextWriter tw)
        {
            Contract.Assume(tw != null);
            if (this.IsTop) { tw.WriteLine("Top"); }
            else if (this.IsBottom) { tw.WriteLine("Bot"); }
            else
            {
                map.Visit(delegate (Key/*!*/ k, Val v) { tw.WriteLine("{0} -> {1}", k.ToString(), v.ToString()); return VisitStatus.ContinueVisit; });
            }
        }
        #endregion

        public override string ToString()
        {
            if (this.IsTop) { return ("Top"); }
            else if (this.IsBottom) { return ("Bot"); }
            else
            {
                StringBuilder sb = new StringBuilder();
                map.Visit(delegate (Key/*!*/ k, Val v) { sb.AppendFormat("({0}->{1}),", k.ToString(), v.ToString()); return VisitStatus.ContinueVisit; });
                return sb.ToString();
            }
        }

        public EnvironmentDomain<Key, Val> Empty()
        {
            return new EnvironmentDomain<Key, Val>(map.EmptyMap);
        }

        #region IAbstractValue<EnvironmentDomain<Key,Val>> Members

        public bool LessEqual(EnvironmentDomain<Key, Val> that)
        {
            if (that.IsTop) return true;
            if (this.IsBottom) return true;
            if (this.IsTop) return false;
            if (that.IsBottom) return false;
            if (map.Count < that.map.Count) return false;

            // pointwise
            foreach (Key k in that.map.Keys)
            {
                if (!map.Contains(k) || !map[k].LessEqual(that.map[k])) return false;
            }
            return true;
        }

        #endregion
    }


    public struct SetDomain<T> : IAbstractValue<SetDomain<T>>
      where T : IEquatable<T>
    {
        #region Privates
        // null represents bottom
        private readonly IFunctionalSet<T>/*?*/ set;

        #endregion

        public SetDomain(IFunctionalSet<T>/*?*/ value)
        {
            set = value;
        }

        public SetDomain(Converter<T, int> keyNumber)
        {
            set = FunctionalSet<T>.Empty(keyNumber);
        }

        public static readonly SetDomain<T> TopValue = new SetDomain<T>(Microsoft.Research.DataStructures.FunctionalSet<T>.Empty()); // Thread-safe
        public static readonly SetDomain<T> BottomValue = new SetDomain<T>((IFunctionalSet<T>)null); // Thread-safe

        public SetDomain<T> Add(T elem)
        {
            if (set == null)
            {
                throw new InvalidOperationException("The set is null");
            }
            return new SetDomain<T>(set.Add(elem));
        }

        public SetDomain<T> Remove(T elem)
        {
            if (set == null)
            {
                throw new InvalidOperationException("The set is null");
            }

            return new SetDomain<T>(set.Remove(elem));
        }

        public bool Contains(T elem)
        {
            if (set == null)
            {
                throw new InvalidOperationException("The set is null");
            }

            return set.Contains(elem);
        }

        public IEnumerable<T> Elements
        {
            get
            {
                if (set == null)
                {
                    throw new InvalidOperationException("The set is null");
                }

                return set.Elements;
            }
        }

        public override string ToString()
        {
            if (IsBottom) { return "Bot"; }
            if (IsTop) { return ("Top"); }

            Contract.Assert(set != null);

            StringBuilder sb = new StringBuilder();
            set.Visit(delegate (T/*!*/ elem) { sb.AppendFormat("{0},", elem.ToString()); });
            return sb.ToString();
        }

        #region IAbstractValue<SetDomain<T>> Members

        public SetDomain<T> Top
        {
            get { return TopValue; }
        }

        public SetDomain<T> Bottom
        {
            get { return BottomValue; }
        }

        public bool IsTop
        {
            get { return set != null && set.Count == 0; }
        }

        public bool IsBottom
        {
            get
            {
                Contract.Ensures(Contract.Result<bool>() == (set == null));

                return set == null;
            }
        }

        public SetDomain<T> ImmutableVersion()
        {
            // pure
            return this;
        }

        public SetDomain<T> Clone()
        {
            // pure
            return this;
        }

        /// <summary>
        /// Implemented as Intersection
        /// </summary>
        [ContractVerification(false)]
        public SetDomain<T> Join(SetDomain<T> newState, out bool weaker, bool widen)
        {
            if (set == newState.set) { weaker = false; return this; }
            if (this.IsBottom) { weaker = !newState.IsBottom; return newState; }
            if (newState.IsBottom || this.IsTop) { weaker = false; return this; }
            if (newState.IsTop) { weaker = !this.IsTop; return newState; }

            Contract.Assert(set != null);

            IFunctionalSet<T> result = set.Intersect(newState.set);
            weaker = result.Count < set.Count;
            return new SetDomain<T>(result);
        }

        /// <summary>
        /// Implemented as Union
        /// </summary>
        [ContractVerification(false)]
        public SetDomain<T> Meet(SetDomain<T> that)
        {
            if (set == that.set) { return this; }
            if (this.IsBottom || that.IsTop) { return this; }
            if (that.IsBottom || this.IsTop) { return that; }

            Contract.Assert(set != null);

            IFunctionalSet<T> result = set.Union(that.set);
            return new SetDomain<T>(result);
        }


        public bool LessEqual(SetDomain<T> that)
        {
            if (this.IsBottom) return true;
            if (that.IsBottom) return false;

            Contract.Assert(set != null);

            return that.set.Contained(set);
        }

        public void Dump(TextWriter tw)
        {
            if (this.IsBottom) { tw.WriteLine("Bot"); return; }
            if (this.IsTop) { tw.WriteLine("Top"); return; }

            Contract.Assert(set != null);

            set.Dump(tw);
        }

        #endregion
    }
}

