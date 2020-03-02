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
    /// The public interface for AbstractState
    /// </summary>
    [DefaultImplementationTypeAttribute(typeof(AbstractState))]
    [XmlDefaultImplementationTypeAttribute(typeof(AbstractState))]
    [ModelRepresentationClassAttribute("http://hsm/1.0#//AbstractState")]
    public interface IAbstractState : IModelElement
    {
        
        /// <summary>
        /// The name property
        /// </summary>
        [DisplayNameAttribute("name")]
        [CategoryAttribute("AbstractState")]
        [XmlElementNameAttribute("name")]
        [IdAttribute()]
        [XmlAttributeAttribute(true)]
        string Name
        {
            get;
            set;
        }
        
        /// <summary>
        /// The stateMachine property
        /// </summary>
        [BrowsableAttribute(false)]
        [XmlElementNameAttribute("stateMachine")]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        [XmlAttributeAttribute(true)]
        [XmlOppositeAttribute("states")]
        IStateMachine StateMachine
        {
            get;
            set;
        }
        
        /// <summary>
        /// The compositeStates property
        /// </summary>
        [DisplayNameAttribute("compositeStates")]
        [CategoryAttribute("AbstractState")]
        [XmlElementNameAttribute("compositeStates")]
        [XmlAttributeAttribute(true)]
        [XmlOppositeAttribute("states")]
        ICompositeState CompositeStates
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets fired before the Name property changes its value
        /// </summary>
        event System.EventHandler<ValueChangedEventArgs> NameChanging;
        
        /// <summary>
        /// Gets fired when the Name property changed its value
        /// </summary>
        event System.EventHandler<ValueChangedEventArgs> NameChanged;
        
        /// <summary>
        /// Gets fired before the StateMachine property changes its value
        /// </summary>
        event System.EventHandler<ValueChangedEventArgs> StateMachineChanging;
        
        /// <summary>
        /// Gets fired when the StateMachine property changed its value
        /// </summary>
        event System.EventHandler<ValueChangedEventArgs> StateMachineChanged;
        
        /// <summary>
        /// Gets fired before the CompositeStates property changes its value
        /// </summary>
        event System.EventHandler<ValueChangedEventArgs> CompositeStatesChanging;
        
        /// <summary>
        /// Gets fired when the CompositeStates property changed its value
        /// </summary>
        event System.EventHandler<ValueChangedEventArgs> CompositeStatesChanged;
    }
}
