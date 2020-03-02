//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using NMF.Collections.Generic;
using NMF.Collections.ObjectModel;
using NMF.Expressions;
using NMF.Expressions.Linq;
using NMF.Models;
using NMF.Models.Collections;
using NMF.Models.Expressions;
using NMF.Models.Meta;
using NMF.Models.Repository;
using NMF.Serialization;
using NMF.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace MDEMGTT.CPL
{
    
    
    /// <summary>
    /// The public interface for AddressSwitch
    /// </summary>
    [DefaultImplementationTypeAttribute(typeof(AddressSwitch))]
    [XmlDefaultImplementationTypeAttribute(typeof(AddressSwitch))]
    [ModelRepresentationClassAttribute("http://cpl/1.0#//AddressSwitch")]
    public interface IAddressSwitch : IModelElement, ISwitch
    {
        
        /// <summary>
        /// The field property
        /// </summary>
        [DisplayNameAttribute("field")]
        [CategoryAttribute("AddressSwitch")]
        [XmlElementNameAttribute("field")]
        [XmlAttributeAttribute(true)]
        string Field
        {
            get;
            set;
        }
        
        /// <summary>
        /// The subField property
        /// </summary>
        [DisplayNameAttribute("subField")]
        [CategoryAttribute("AddressSwitch")]
        [XmlElementNameAttribute("subField")]
        [XmlAttributeAttribute(true)]
        string SubField
        {
            get;
            set;
        }
        
        /// <summary>
        /// The addresses property
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content)]
        [BrowsableAttribute(false)]
        [XmlElementNameAttribute("addresses")]
        [XmlAttributeAttribute(false)]
        [ContainmentAttribute()]
        [ConstantAttribute()]
        IOrderedSetExpression<ISwitchedAddress> Addresses
        {
            get;
        }
        
        /// <summary>
        /// Gets fired before the Field property changes its value
        /// </summary>
        event System.EventHandler<ValueChangedEventArgs> FieldChanging;
        
        /// <summary>
        /// Gets fired when the Field property changed its value
        /// </summary>
        event System.EventHandler<ValueChangedEventArgs> FieldChanged;
        
        /// <summary>
        /// Gets fired before the SubField property changes its value
        /// </summary>
        event System.EventHandler<ValueChangedEventArgs> SubFieldChanging;
        
        /// <summary>
        /// Gets fired when the SubField property changed its value
        /// </summary>
        event System.EventHandler<ValueChangedEventArgs> SubFieldChanged;
    }
}
