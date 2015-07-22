// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.CodeTools;
using System.IO;

namespace Microsoft.Contracts.VisualStudio
{
    internal abstract class ProjectProperty
    {
        /// <summary>
        /// The value of this string is used in the .targets files to set options
        /// for the command line.
        /// </summary>
        protected readonly string propertyName;
        protected ProjectProperty(string propertyName)
        {
            this.propertyName = propertyName;
        }

        public abstract void Save(string[] configNames, IPropertyStorage storage);
        public abstract void Load(string[] configNames, IPropertyStorage storage);
    }

    internal class CheckBoxProperty : ProjectProperty
    {
        private readonly CheckBox box;
        private readonly bool defaultValue;

        public CheckBoxProperty(string propertyName, CheckBox box, bool defaultValue) :
          base(propertyName)
        {
            this.box = box;
            this.defaultValue = defaultValue;
        }

        public override void Save(string[] configNames, IPropertyStorage storage)
        {
            if (box.CheckState != CheckState.Indeterminate)
            {
                storage.SetProperties(false, configNames, propertyName, box.Checked);
                // TODO: avoid saving default values, once removing works
#if false
                if (box.Checked != defaultValue)
                {
                    storage.SetProperties(false, configNames, propertyName, box.Checked);
                }
                else
                {
                    // don't clutter csproj file
                    foreach (var config in configNames)
                    {
                        storage.RemoveProperty(false, config, propertyName);
                    }
                }
#endif
            }
        }

        private CheckState CheckStateOfBool(bool checkedBool)
        {
            if (checkedBool) { return CheckState.Checked; }
            return CheckState.Unchecked;
        }

        public override void Load(string[] configNames, IPropertyStorage storage)
        {
            object result = storage.GetProperties(false, configNames, propertyName, defaultValue);
            if (result != null)
            {
                box.CheckState = CheckStateOfBool((bool)result);
            }
            else
            {
                box.CheckState = CheckState.Indeterminate;
            }
        }
    }

    internal class NegatedCheckBoxProperty : ProjectProperty
    {
        private readonly CheckBox box;
        private readonly bool defaultValue;

        public NegatedCheckBoxProperty(string propertyName, CheckBox box, bool defaultValue) :
          base(propertyName)
        {
            this.box = box;
            this.defaultValue = defaultValue;
        }

        public override void Save(string[] configNames, IPropertyStorage storage)
        {
            if (box.CheckState != CheckState.Indeterminate)
            {
                storage.SetProperties(false, configNames, propertyName, !box.Checked);
#if false
                if (box.Checked != defaultValue)
                {
                    storage.SetProperties(false, configNames, propertyName, !box.Checked);
                }
                else
                {
                    // don't clutter csproj file
                    foreach (var config in configNames)
                    {
                        storage.RemoveProperty(false, config, propertyName);
                    }
                }
#endif
            }
        }

        private CheckState CheckStateOfBool(bool checkedBool)
        {
            if (checkedBool) { return CheckState.Checked; }
            return CheckState.Unchecked;
        }

        public override void Load(string[] configNames, IPropertyStorage storage)
        {
            object result = storage.GetProperties(false, configNames, propertyName, !defaultValue);
            if (result != null)
            {
                box.CheckState = CheckStateOfBool(!(bool)result);
            }
            else
            {
                box.CheckState = CheckState.Indeterminate;
            }
        }
    }

    internal class TextBoxProperty : ProjectProperty
    {
        private readonly TextBox box;
        private readonly string defaultValue;

        public TextBoxProperty(string propertyName, TextBox box, string defaultValue)
          : base(propertyName)
        {
            this.box = box;
            this.defaultValue = defaultValue;
        }

        public override void Save(string[] configNames, IPropertyStorage storage)
        {
            if (box.Text != null)
            {
                storage.SetProperties(false, configNames, propertyName, box.Text);
#if false
                if (box.Text != defaultValue)
                {
                    storage.SetProperties(false, configNames, propertyName, box.Text);
                }
                else
                {
                    // don't clutter csproj file
                    foreach (var config in configNames)
                    {
                        storage.RemoveProperty(false, config, propertyName);
                    }
                }
#endif
            }
        }

        public override void Load(string[] configNames, IPropertyStorage storage)
        {
            string result = storage.GetProperties(false, configNames, propertyName, defaultValue) as string;
            box.Text = result;
        }
    }
}