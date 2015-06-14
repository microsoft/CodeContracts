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
using System.ComponentModel;
using System.Xml;
using System.Xml.Serialization;
using System.Diagnostics.Contracts;
namespace System.Xml.Schema
{
#if !SILVERLIGHT
  public class XmlSchemaComplexType //: XmlSchemaType
  {
    // Summary:
    //     Initializes a new instance of the System.Xml.Schema.XmlSchemaComplexType
    //     class.
    //public XmlSchemaComplexType();

    // Summary:
    //     Gets or sets the value for the System.Xml.Schema.XmlSchemaAnyAttribute component
    //     of the complex type.
    //
    // Returns:
    //     The System.Xml.Schema.XmlSchemaAnyAttribute component of the complex type.
    //[XmlElement("anyAttribute")]
    //public XmlSchemaAnyAttribute AnyAttribute { get; set; }
    //
    // Summary:
    //     Gets the collection of attributes for the complex type.
    //
    // Returns:
    //     Contains System.Xml.Schema.XmlSchemaAttribute and System.Xml.Schema.XmlSchemaAttributeGroupRef
    //     classes.
    //[XmlElement("attribute", typeof(XmlSchemaAttribute))]
    //XmlElement("attributeGroup", typeof(XmlSchemaAttributeGroupRef))]
    public XmlSchemaObjectCollection Attributes 
    {
      get
      {
        Contract.Ensures(Contract.Result<XmlSchemaObjectCollection>() != null);

        return default(XmlSchemaObjectCollection);
      }
    }
    
    
    //
    // Summary:
    //     Gets the collection of all the complied attributes of this complex type and
    //     its base types.
    //
    // Returns:
    //     The collection of all the attributes from this complex type and its base
    //     types. The post-compilation value of the AttributeUses property.
    //[XmlIgnore]
    /*
    public XmlSchemaObjectTable AttributeUses
    {
      get
      {
        Contract.Ensures(Contract.Result<XmlSchemaObjectTable>() != null);

        return default(XmlSchemaObjectTable);
      }
    }
     */
    //
    // Summary:
    //     Gets the post-compilation value for anyAttribute for this complex type and
    //     its base type(s).
    //
    // Returns:
    //     The post-compilation value of the anyAttribute element.
    //[XmlIgnore]
    //public XmlSchemaAnyAttribute AttributeWildcard { get; }
    //
    // Summary:
    //     Gets or sets the block attribute.
    //
    // Returns:
    //     The block attribute prevents a complex type from being used in the specified
    //     type of derivation. The default is XmlSchemaDerivationMethod.None.  Optional.
    //[XmlAttribute("block")]
    //public XmlSchemaDerivationMethod Block { get; set; }
    //
    // Summary:
    //     Gets the value after the type has been compiled to the post-schema-validation
    //     information set (infoset). This value indicates how the type is enforced
    //     when xsi:type is used in the instance document.
    //
    // Returns:
    //     The post-schema-validated infoset value. The default is BlockDefault value
    //     on the schema element.
    //[XmlIgnore]
    //public XmlSchemaDerivationMethod BlockResolved { get; }
    //
    // Summary:
    //     Gets or sets the post-compilation System.Xml.Schema.XmlSchemaContentModel
    //     of this complex type.
    //
    // Returns:
    //     The content model type that is one of the System.Xml.Schema.XmlSchemaSimpleContent
    //     or System.Xml.Schema.XmlSchemaComplexContent classes.
    //[XmlElement("simpleContent", typeof(XmlSchemaSimpleContent))]
    //[XmlElement("complexContent", typeof(XmlSchemaComplexContent))]
    //public XmlSchemaContentModel ContentModel { get; set; }
    //
    // Summary:
    //     Gets the content model of the complex type which holds the post-compilation
    //     value.
    //
    // Returns:
    //     The post-compilation value of the content model for the complex type.
    //[XmlIgnore]
    //public XmlSchemaContentType ContentType { get; }
    //
    // Summary:
    //     Gets the particle that holds the post-compilation value of the System.Xml.Schema.XmlSchemaComplexType.ContentType
    //     particle.
    //
    // Returns:
    //     The particle for the content type. The post-compilation value of the System.Xml.Schema.XmlSchemaComplexType.ContentType
    //     particle.
    //[XmlIgnore]
    //public XmlSchemaParticle ContentTypeParticle { get; }
    //
    // Summary:
    //     Gets or sets the information that determines if the complexType element can
    //     be used in the instance document.
    //
    // Returns:
    //     If true, an element cannot use this complexType element directly and must
    //     use a complex type that is derived from this complexType element. The default
    //     is false.  Optional.
    //[XmlAttribute("abstract")]
    //[DefaultValue(false)]
    //public bool IsAbstract { get; set; }
    //
    //
    // Summary:
    //     Gets or sets the compositor type as one of the System.Xml.Schema.XmlSchemaGroupRef,
    //     System.Xml.Schema.XmlSchemaChoice, System.Xml.Schema.XmlSchemaAll, or System.Xml.Schema.XmlSchemaSequence
    //     classes.
    //
    // Returns:
    //     The compositor type.
    //  [XmlElement("sequence", typeof(XmlSchemaSequence))]
    //[XmlElement("group", typeof(XmlSchemaGroupRef))]
    //[XmlElement("choice", typeof(XmlSchemaChoice))]
    //[XmlElement("all", typeof(XmlSchemaAll))]
    //public XmlSchemaParticle Particle { get; set; }
  }
#endif
}
