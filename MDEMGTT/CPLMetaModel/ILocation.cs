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
    /// The public interface for Location
    /// </summary>
    [DefaultImplementationTypeAttribute(typeof(Location))]
    [XmlDefaultImplementationTypeAttribute(typeof(Location))]
    [ModelRepresentationClassAttribute("http://cpl/1.0#//Location")]
    public interface ILocation : IModelElement, INodeContainer, INode
    {
        
        /// <summary>
        /// The url property
        /// </summary>
        [DisplayNameAttribute("url")]
        [CategoryAttribute("Location")]
        [XmlElementNameAttribute("url")]
        [XmlAttributeAttribute(true)]
        string Url
        {
            get;
            set;
        }
        
        /// <summary>
        /// The priority property
        /// </summary>
        [DisplayNameAttribute("priority")]
        [CategoryAttribute("Location")]
        [XmlElementNameAttribute("priority")]
        [XmlAttributeAttribute(true)]
        string Priority
        {
            get;
            set;
        }
        
        /// <summary>
        /// The clear property
        /// </summary>
        [DisplayNameAttribute("clear")]
        [CategoryAttribute("Location")]
        [XmlElementNameAttribute("clear")]
        [XmlAttributeAttribute(true)]
        string Clear
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets fired before the Url property changes its value
        /// </summary>
        event System.EventHandler<ValueChangedEventArgs> UrlChanging;
        
        /// <summary>
        /// Gets fired when the Url property changed its value
        /// </summary>
        event System.EventHandler<ValueChangedEventArgs> UrlChanged;
        
        /// <summary>
        /// Gets fired before the Priority property changes its value
        /// </summary>
        event System.EventHandler<ValueChangedEventArgs> PriorityChanging;
        
        /// <summary>
        /// Gets fired when the Priority property changed its value
        /// </summary>
        event System.EventHandler<ValueChangedEventArgs> PriorityChanged;
        
        /// <summary>
        /// Gets fired before the Clear property changes its value
        /// </summary>
        event System.EventHandler<ValueChangedEventArgs> ClearChanging;
        
        /// <summary>
        /// Gets fired when the Clear property changed its value
        /// </summary>
        event System.EventHandler<ValueChangedEventArgs> ClearChanged;
    }
}

