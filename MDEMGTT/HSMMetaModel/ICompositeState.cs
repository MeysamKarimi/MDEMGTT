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

namespace MDEMGTT.Hsm
{
    
    
    /// <summary>
    /// The public interface for CompositeState
    /// </summary>
    [DefaultImplementationTypeAttribute(typeof(CompositeState))]
    [XmlDefaultImplementationTypeAttribute(typeof(CompositeState))]
    [ModelRepresentationClassAttribute("http://hsm/1.0#//CompositeState")]
    public interface ICompositeState : IModelElement, IAbstractState
    {
        
        /// <summary>
        /// The states property
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content)]
        [DisplayNameAttribute("states")]
        [CategoryAttribute("CompositeState")]
        [XmlElementNameAttribute("states")]
        [XmlAttributeAttribute(true)]
        [XmlOppositeAttribute("compositeStates")]
        [ConstantAttribute()]
        IOrderedSetExpression<IAbstractState> States
        {
            get;
        }
    }
}
