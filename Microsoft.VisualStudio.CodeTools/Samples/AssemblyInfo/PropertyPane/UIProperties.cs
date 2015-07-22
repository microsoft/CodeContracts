// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.CodeTools;

namespace AssemblyInfo
{
    /// <summary>
    /// Convenience class to associated msbuild properties with UI elements.
    /// This makes it easy to put all the project properties in a homogeneous set:
    /// <code>
    /// ProjectProperty[] properties;
    /// properties = new ProjectProperty[] {
    ///   new CheckBoxProperty( "MyToolEnable", this.enableCheckBox, false ),
    ///   new TextBoxProperty( "MyToolOptions", this.optionsTextBox, "" )
    /// };
    /// </code>
    /// and use it inside LoadProperties and SaveProperties as:
    /// <code>
    /// public void LoadProperties(string[] configNames, IPropertyStorage storage) {
    ///   foreach (var prop in this.properties) {
    ///     prop.Load(configNames, storage);
    ///   }
    /// }  
    /// </code>
    /// </summary>
    public abstract class ProjectProperty
    {
        /// <summary>
        /// The msbuild property name
        /// </summary>
        protected readonly string propertyName;

        /// <summary>
        /// Should this property be saved per user (or per project)?
        /// </summary>
        protected readonly bool perUser;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="propertyName">The msbuild property name</param>\
        protected ProjectProperty(string propertyName)
            : this(false, propertyName)
        {
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="perUser">Is this property saved per user?</param>
        /// <param name="propertyName">The msbuild property name</param>
        protected ProjectProperty(bool perUser, string propertyName)
        {
            this.propertyName = propertyName;
            this.perUser = perUser;
        }

        /// <summary>
        /// Save the property to the project file. Override this in derived classes.
        /// </summary>
        /// <param name="configNames">The set of configuration names</param>
        /// <param name="storage">The property storage (i.e. the project file)</param>
        public abstract void Save(string[] configNames, IPropertyStorage storage);


        /// <summary>
        /// Load the property to the project file. Override this in derived classes.
        /// </summary>
        /// <param name="configNames">The set of configuration names</param>
        /// <param name="storage">The property storage (i.e. the project file)</param>
        public abstract void Load(string[] configNames, IPropertyStorage storage);
    }

    /// <summary>
    /// A checkbox property associates a boolean msbuild property with a checkbox.
    /// </summary>
    public class CheckBoxProperty : ProjectProperty
    {
        private readonly CheckBox box;
        private readonly bool defaultValue;

        /// <summary>
        /// Create a new checkbox property
        /// </summary>
        /// <param name="propertyName">The msbuild boolean property name</param>
        /// <param name="box">The associated checkbox</param>
        /// <param name="defaultValue">The default value</param>
        public CheckBoxProperty(string propertyName, CheckBox box, bool defaultValue)
            : this(false, propertyName, box, defaultValue)
        {
        }

        /// <summary>
        /// Create a new checkbox property
        /// </summary>
        /// <param name="perUser">Associated per user?</param>
        /// <param name="propertyName">The msbuild boolean property name</param>
        /// <param name="box">The associated checkbox</param>
        /// <param name="defaultValue">The default value</param>
        public CheckBoxProperty(bool perUser, string propertyName, CheckBox box, bool defaultValue) :
            base(perUser, propertyName)
        {
            this.box = box;
            this.defaultValue = defaultValue;
        }

        /// <summary>
        /// Convert a boolean to a checkbox state.
        /// </summary>
        /// <param name="checkedBool">The boolean, true is checked</param>
        /// <returns>A checkstate, either Checked or Unchecked</returns>
        public static CheckState CheckStateFromBool(bool checkedBool)
        {
            return (checkedBool ? CheckState.Checked : CheckState.Unchecked);
        }

        /// <summary>
        /// Save the checkbox state to its associated msbuild property
        /// </summary>
        /// <param name="configNames">The set of configurations</param>
        /// <param name="storage">The property storage</param>
        public override void Save(string[] configNames, IPropertyStorage storage)
        {
            if (box.CheckState != CheckState.Indeterminate)
            {
                storage.SetProperties(perUser, configNames, propertyName, box.Checked);
            }
        }

        /// <summary>
        /// Load the checkbox state from its associated msbuild property
        /// </summary>
        /// <param name="configNames">The set of configurations</param>
        /// <param name="storage">The property storage</param>
        public override void Load(string[] configNames, IPropertyStorage storage)
        {
            bool anyFound;
            bool indeterminate;
            bool result = storage.GetProperties<bool>(perUser, configNames, propertyName, defaultValue, out anyFound, out indeterminate);

            if (!indeterminate)
            {
                box.CheckState = CheckStateFromBool(result);
            }
            else
            {
                box.CheckState = CheckState.Indeterminate;
            }
        }
    }


    /// <summary>
    /// A negated checkbox property associates a boolean msbuild property with a checkbox.
    /// The checkbox state is the negation of the msbuild property (ie. unchecked is true)
    /// </summary>
    public class NegatedCheckBoxProperty : ProjectProperty
    {
        private readonly CheckBox box;
        private readonly bool defaultValue;

        /// <summary>
        /// Create a new negated checkbox property
        /// </summary>
        /// <param name="propertyName">The msbuild boolean property name</param>
        /// <param name="box">The associated checkbox</param>
        /// <param name="defaultValue">The default value (for the msbuild property)</param>
        public NegatedCheckBoxProperty(string propertyName, CheckBox box, bool defaultValue)
            : this(false, propertyName, box, defaultValue)
        { }

        /// <summary>
        /// Create a new negated checkbox property
        /// </summary>
        /// <param name="perUser">Associated per user?</param>
        /// <param name="propertyName">The msbuild boolean property name</param>
        /// <param name="box">The associated checkbox</param>
        /// <param name="defaultValue">The default value (for the msbuild property)</param>
        public NegatedCheckBoxProperty(bool perUser, string propertyName, CheckBox box, bool defaultValue) :
            base(perUser, propertyName)
        {
            this.box = box;
            this.defaultValue = defaultValue;
        }

        /// <summary>
        /// Save the negated checkbox state to its associated msbuild property
        /// </summary>
        /// <param name="configNames">The set of configurations</param>
        /// <param name="storage">The property storage</param>
        public override void Save(string[] configNames, IPropertyStorage storage)
        {
            if (box.CheckState != CheckState.Indeterminate)
            {
                storage.SetProperties(perUser, configNames, propertyName, !box.Checked);
            }
        }

        /// <summary>
        /// Load the negated checkbox state from its associated msbuild property
        /// </summary>
        /// <param name="configNames">The set of configurations</param>
        /// <param name="storage">The property storage</param>		
        public override void Load(string[] configNames, IPropertyStorage storage)
        {
            bool anyFound;
            bool indeterminate;
            bool result = storage.GetProperties<bool>(perUser, configNames, propertyName, defaultValue, out anyFound, out indeterminate);

            if (!indeterminate)
            {
                box.CheckState = CheckBoxProperty.CheckStateFromBool(!result);
            }
            else
            {
                box.CheckState = CheckState.Indeterminate;
            }
        }
    }


    /// <summary>
    /// A TextBoxProperty associates a string msbuild property with a textbox UI element.
    /// </summary>
    public class TextBoxProperty : ProjectProperty
    {
        private TextBox box;
        private string defaultValue;

        private string textIndeterminate = "";

        public string TextIndeterminate
        {
            get { return textIndeterminate; }
            set { textIndeterminate = value; }
        }

        /// <summary>
        /// Create a new TextBoxProperty.
        /// </summary>
        /// <param name="propertyName">The msbuild property name</param>
        /// <param name="box">The associated UI TextBox</param>
        /// <param name="defaultValue">The default value (of the msbuild property)</param>
        public TextBoxProperty(string propertyName, TextBox box, string defaultValue) : this(false, propertyName, box, defaultValue)
        { }

        /// <summary>
        /// Create a new TextBoxProperty.
        /// </summary>
        /// <param name="perUser">Associated per user?</param>
        /// <param name="propertyName">The msbuild property name</param>
        /// <param name="box">The associated UI TextBox</param>
        /// <param name="defaultValue">The default value (of the msbuild property)</param>
        public TextBoxProperty(bool perUser, string propertyName, TextBox box, string defaultValue)
            : base(perUser, propertyName)
        {
            this.box = box;
            this.defaultValue = defaultValue;
        }

        /// <summary>
        /// Save the textbox state to its associated msbuild property
        /// </summary>
        /// <param name="configNames">The set of configurations</param>
        /// <param name="storage">The property storage</param>
        public override void Save(string[] configNames, IPropertyStorage storage)
        {
            if (box.Text != null && box.Modified)
            { // only save on modification
                storage.SetProperties(perUser, configNames, propertyName, box.Text);
            }
        }


        /// <summary>
        /// Load the textbox string from its associated msbuild property
        /// </summary>
        /// <param name="configNames">The set of configurations</param>
        /// <param name="storage">The property storage</param>
        public override void Load(string[] configNames, IPropertyStorage storage)
        {
            bool anyFound;
            bool indeterminate;
            string text = storage.GetProperties<string>(perUser, configNames, propertyName, defaultValue, out anyFound, out indeterminate);

            if (!indeterminate)
            {
                box.Text = (text == null ? "" : text);
            }
            else
            {
                box.Text = TextIndeterminate;
            }
        }
    }
}