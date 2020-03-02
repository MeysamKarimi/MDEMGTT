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
    /// The public interface for SubAction
    /// </summary>
    [DefaultImplementationTypeAttribute(typeof(SubAction))]
    [XmlDefaultImplementationTypeAttribute(typeof(SubAction))]
    [ModelRepresentationClassAttribute("http://cpl/1.0#//SubAction")]
    public interface ISubAction : IModelElement, INodeContainer
    {
        
        /// <summary>
        /// The id property
        /// </summary>
        [DisplayNameAttribute("id")]
        [CategoryAttribute("SubAction")]
        [XmlElementNameAttribute("id")]
        [XmlAttributeAttribute(true)]
        string Id
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets fired before the Id property changes its value
        /// </summary>
        event System.EventHandler<ValueChangedEventArgs> IdChanging;
        
        /// <summary>
        /// Gets fired when the Id property changed its value
        /// </summary>
        event System.EventHandler<ValueChangedEventArgs> IdChanged;
    }
}
