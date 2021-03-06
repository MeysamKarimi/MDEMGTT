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

namespace MDEMGTT.BibTeX
{
    
    
    /// <summary>
    /// The public interface for DatedEntry
    /// </summary>
    [DefaultImplementationTypeAttribute(typeof(DatedEntry))]
    [XmlDefaultImplementationTypeAttribute(typeof(DatedEntry))]
    [ModelRepresentationClassAttribute("http://bibtex/1.0#//DatedEntry")]
    public interface IDatedEntry : IModelElement, IBibTeXEntry
    {
        
        /// <summary>
        /// The year property
        /// </summary>
        [DisplayNameAttribute("year")]
        [CategoryAttribute("DatedEntry")]
        [XmlElementNameAttribute("year")]
        [XmlAttributeAttribute(true)]
        string Year
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets fired before the Year property changes its value
        /// </summary>
        event System.EventHandler<ValueChangedEventArgs> YearChanging;
        
        /// <summary>
        /// Gets fired when the Year property changed its value
        /// </summary>
        event System.EventHandler<ValueChangedEventArgs> YearChanged;
    }
}

