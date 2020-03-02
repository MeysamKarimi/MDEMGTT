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

namespace MDEMGTT.Grafcet
{
    
    
    /// <summary>
    /// The public interface for Element
    /// </summary>
    [DefaultImplementationTypeAttribute(typeof(Element))]
    [XmlDefaultImplementationTypeAttribute(typeof(Element))]
    [ModelRepresentationClassAttribute("http://grafcet/1.0#//Element")]
    public interface IElement : IModelElement, INamedElement
    {
        
        /// <summary>
        /// The grafcet property
        /// </summary>
        [BrowsableAttribute(false)]
        [XmlElementNameAttribute("grafcet")]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        [XmlAttributeAttribute(true)]
        [XmlOppositeAttribute("elements")]
        IGrafcet Grafcet
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets fired before the Grafcet property changes its value
        /// </summary>
        event System.EventHandler<ValueChangedEventArgs> GrafcetChanging;
        
        /// <summary>
        /// Gets fired when the Grafcet property changed its value
        /// </summary>
        event System.EventHandler<ValueChangedEventArgs> GrafcetChanged;
    }
}

