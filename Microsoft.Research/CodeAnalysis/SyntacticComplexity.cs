// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Research.CodeAnalysis
{
    public class SyntacticComplexity
    {
        #region Thresholds
        private const uint MAXLOOPS = 100;
        private const uint MAXJOINS = 200;
        private const uint MAXJOINS_BACKWARDSCHECKING = 75;
        private const uint HUGENUMBEROFJOINS = 500;
        private const uint MAXINSTRUCTIONS = 10000000;
        private const double MAXINSTRUCTIONSJOINRATION = 10000d;
        #endregion

        #region Statistics
        [ThreadStatic]
        private static uint maxLoops;
        [ThreadStatic]
        private static uint maxJoins;
        [ThreadStatic]
        private static uint maxInstructions;
        [ThreadStatic]
        private static uint totalLoops;
        [ThreadStatic]
        private static uint totalJoins;
        [ThreadStatic]
        private static uint totalInstructions;

        static public uint MaxLoops { private set { maxLoops = value; } get { return maxLoops; } }
        static public uint MaxJoins { private set { maxJoins = value; } get { return maxJoins; } }
        static public uint MaxInstructions { private set { maxInstructions = value; } get { return maxInstructions; } }
        static public uint TotalLoops { private set { totalLoops = value; } get { return totalLoops; } }
        static public uint TotalJoins { private set { totalJoins = value; } get { return totalJoins; } }
        static public uint TotalInstructions { private set { totalInstructions = value; } get { return totalInstructions; } }

        static private void UpdateStatistics(SyntacticComplexity next)
        {
            // update maxs
            if (MaxLoops < next.Loops)
                MaxLoops = next.Loops;

            if (MaxJoins < next.JoinPoints)
                MaxJoins = next.JoinPoints;

            if (MaxInstructions < next.Instructions)
                MaxInstructions = next.Instructions;

            // update totals
            TotalLoops += next.Loops;
            TotalJoins += next.JoinPoints;
            TotalInstructions += next.Instructions;
        }

        #endregion

        #region State
        public readonly bool IsValid;
        private bool messageAlreadyPrinted;

        public readonly uint JoinPoints;
        public readonly uint Loops;
        public readonly uint Instructions;
        public static SyntacticComplexity Dummy = new SyntacticComplexity();

        #endregion

        private SyntacticComplexity()
        {
            this.IsValid = false;
            this.JoinPoints = this.Instructions = this.Loops = 0;
        }

        public SyntacticComplexity(uint joinPoints, uint instructions, uint Loops)
        {
            this.IsValid = true;

            this.JoinPoints = joinPoints;
            this.Instructions = instructions;
            this.Loops = Loops;
            messageAlreadyPrinted = false;

            UpdateStatistics(this);
        }

        public bool TooManyLoops
        {
            get
            {
                return this.Loops > MAXLOOPS;
            }
        }

        public bool TooManyJoins
        {
            get
            {
                return this.JoinPoints > MAXJOINS;
            }
        }

        public bool TooManyJoinsForBackwardsChecking
        {
            get
            {
                return this.JoinPoints > MAXJOINS_BACKWARDSCHECKING;
            }
        }

        public bool WayTooManyJoins
        {
            get
            {
                return this.TooManyJoins && this.JoinPoints > HUGENUMBEROFJOINS;
            }
        }

        public bool TooManyInstructionsPerJoin
        {
            get
            {
                if (this.JoinPoints == 0)
                {
                    return this.Instructions > 10000;
                }

                var ratio = ((double)this.Instructions) / this.JoinPoints;
                return ratio > MAXINSTRUCTIONSJOINRATION;
            }
        }

        public override string ToString()
        {
            if (this.IsValid)
            {
                string format = "#Inst: {0}, #joins: {1}, #loops: {2}";
                return string.Format(format, Instructions, JoinPoints, Loops);
            }
            else
            {
                return "<no stats>";
            }
        }

        public bool SuggestACheapAnalysis
        {
            get
            {
                return this.TooManyLoops || this.TooManyJoins || this.TooManyInstructionsPerJoin;
            }
        }

        public bool SuggestAVeryCheapAnalysis
        {
            get
            {
                return this.SuggestACheapAnalysis && this.WayTooManyJoins;
            }
        }


        public bool ShouldAvoidWPComputation(out bool messageAlreadyPrinted)
        {
            if (this.WayTooManyJoins)
            {
                messageAlreadyPrinted = this.messageAlreadyPrinted;
                this.messageAlreadyPrinted = true;
                return true;
            }
            else
            {
                messageAlreadyPrinted = false;
                return false;
            }
        }
    }
}
