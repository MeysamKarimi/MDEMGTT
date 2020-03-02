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
    /// The default implementation of the SwitchedAddress class
    /// </summary>
    [XmlNamespaceAttribute("http://cpl/1.0")]
    [XmlNamespacePrefixAttribute("CPL")]
    [ModelRepresentationClassAttribute("http://cpl/1.0#//SwitchedAddress")]
    public partial class SwitchedAddress : NodeContainer, ISwitchedAddress, IModelElement
    {
        
        /// <summary>
        /// The backing field for the Is property
        /// </summary>
        [DebuggerBrowsableAttribute(DebuggerBrowsableState.Never)]
        private string _is;
        
        private static Lazy<ITypedElement> _isAttribute = new Lazy<ITypedElement>(RetrieveIsAttribute);
        
        /// <summary>
        /// The backing field for the Contains property
        /// </summary>
        [DebuggerBrowsableAttribute(DebuggerBrowsableState.Never)]
        private string _contains;
        
        private static Lazy<ITypedElement> _containsAttribute = new Lazy<ITypedElement>(RetrieveContainsAttribute);
        
        /// <summary>
        /// The backing field for the SubDomainOf property
        /// </summary>
        [DebuggerBrowsableAttribute(DebuggerBrowsableState.Never)]
        private string _subDomainOf;
        
        private static Lazy<ITypedElement> _subDomainOfAttribute = new Lazy<ITypedElement>(RetrieveSubDomainOfAttribute);
        
        private static IClass _classInstance;
        
        /// <summary>
        /// The is property
        /// </summary>
        [DisplayNameAttribute("is")]
        [CategoryAttribute("SwitchedAddress")]
        [XmlElementNameAttribute("is")]
        [XmlAttributeAttribute(true)]
        public string Is
        {
            get
            {
                return this._is;
            }
            set
            {
                if ((this._is != value))
                {
                    string old = this._is;
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnIsChanging(e);
                    this.OnPropertyChanging("Is", e, _isAttribute);
                    this._is = value;
                    this.OnIsChanged(e);
                    this.OnPropertyChanged("Is", e, _isAttribute);
                }
            }
        }
        
        /// <summary>
        /// The contains property
        /// </summary>
        [DisplayNameAttribute("contains")]
        [CategoryAttribute("SwitchedAddress")]
        [XmlElementNameAttribute("contains")]
        [XmlAttributeAttribute(true)]
        public string Contains
        {
            get
            {
                return this._contains;
            }
            set
            {
                if ((this._contains != value))
                {
                    string old = this._contains;
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnContainsChanging(e);
                    this.OnPropertyChanging("Contains", e, _containsAttribute);
                    this._contains = value;
                    this.OnContainsChanged(e);
                    this.OnPropertyChanged("Contains", e, _containsAttribute);
                }
            }
        }
        
        /// <summary>
        /// The subDomainOf property
        /// </summary>
        [DisplayNameAttribute("subDomainOf")]
        [CategoryAttribute("SwitchedAddress")]
        [XmlElementNameAttribute("subDomainOf")]
        [XmlAttributeAttribute(true)]
        public string SubDomainOf
        {
            get
            {
                return this._subDomainOf;
            }
            set
            {
                if ((this._subDomainOf != value))
                {
                    string old = this._subDomainOf;
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnSubDomainOfChanging(e);
                    this.OnPropertyChanging("SubDomainOf", e, _subDomainOfAttribute);
                    this._subDomainOf = value;
                    this.OnSubDomainOfChanged(e);
                    this.OnPropertyChanged("SubDomainOf", e, _subDomainOfAttribute);
                }
            }
        }
        
        /// <summary>
        /// Gets the Class model for this type
        /// </summary>
        public new static IClass ClassInstance
        {
            get
            {
                if ((_classInstance == null))
                {
                    _classInstance = ((IClass)(MetaRepository.Instance.Resolve("http://cpl/1.0#//SwitchedAddress")));
                }
                return _classInstance;
            }
        }
        
        /// <summary>
        /// Gets fired before the Is property changes its value
        /// </summary>
        public event System.EventHandler<ValueChangedEventArgs> IsChanging;
        
        /// <summary>
        /// Gets fired when the Is property changed its value
        /// </summary>
        public event System.EventHandler<ValueChangedEventArgs> IsChanged;
        
        /// <summary>
        /// Gets fired before the Contains property changes its value
        /// </summary>
        public event System.EventHandler<ValueChangedEventArgs> ContainsChanging;
        
        /// <summary>
        /// Gets fired when the Contains property changed its value
        /// </summary>
        public event System.EventHandler<ValueChangedEventArgs> ContainsChanged;
        
        /// <summary>
        /// Gets fired before the SubDomainOf property changes its value
        /// </summary>
        public event System.EventHandler<ValueChangedEventArgs> SubDomainOfChanging;
        
        /// <summary>
        /// Gets fired when the SubDomainOf property changed its value
        /// </summary>
        public event System.EventHandler<ValueChangedEventArgs> SubDomainOfChanged;
        
        private static ITypedElement RetrieveIsAttribute()
        {
            return ((ITypedElement)(((ModelElement)(MDEMGTT.CPL.SwitchedAddress.ClassInstance)).Resolve("is")));
        }
        
        /// <summary>
        /// Raises the IsChanging event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnIsChanging(ValueChangedEventArgs eventArgs)
        {
            System.EventHandler<ValueChangedEventArgs> handler = this.IsChanging;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the IsChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnIsChanged(ValueChangedEventArgs eventArgs)
        {
            System.EventHandler<ValueChangedEventArgs> handler = this.IsChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        private static ITypedElement RetrieveContainsAttribute()
        {
            return ((ITypedElement)(((ModelElement)(MDEMGTT.CPL.SwitchedAddress.ClassInstance)).Resolve("contains")));
        }
        
        /// <summary>
        /// Raises the ContainsChanging event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnContainsChanging(ValueChangedEventArgs eventArgs)
        {
            System.EventHandler<ValueChangedEventArgs> handler = this.ContainsChanging;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the ContainsChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnContainsChanged(ValueChangedEventArgs eventArgs)
        {
            System.EventHandler<ValueChangedEventArgs> handler = this.ContainsChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        private static ITypedElement RetrieveSubDomainOfAttribute()
        {
            return ((ITypedElement)(((ModelElement)(MDEMGTT.CPL.SwitchedAddress.ClassInstance)).Resolve("subDomainOf")));
        }
        
        /// <summary>
        /// Raises the SubDomainOfChanging event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnSubDomainOfChanging(ValueChangedEventArgs eventArgs)
        {
            System.EventHandler<ValueChangedEventArgs> handler = this.SubDomainOfChanging;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the SubDomainOfChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnSubDomainOfChanged(ValueChangedEventArgs eventArgs)
        {
            System.EventHandler<ValueChangedEventArgs> handler = this.SubDomainOfChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Resolves the given attribute name
        /// </summary>
        /// <returns>The attribute value or null if it could not be found</returns>
        /// <param name="attribute">The requested attribute name</param>
        /// <param name="index">The index of this attribute</param>
        protected override object GetAttributeValue(string attribute, int index)
        {
            if ((attribute == "IS"))
            {
                return this.Is;
            }
            if ((attribute == "CONTAINS"))
            {
                return this.Contains;
            }
            if ((attribute == "SUBDOMAINOF"))
            {
                return this.SubDomainOf;
            }
            return base.GetAttributeValue(attribute, index);
        }
        
        /// <summary>
        /// Sets a value to the given feature
        /// </summary>
        /// <param name="feature">The requested feature</param>
        /// <param name="value">The value that should be set to that feature</param>
        protected override void SetFeature(string feature, object value)
        {
            if ((feature == "IS"))
            {
                this.Is = ((string)(value));
                return;
            }
            if ((feature == "CONTAINS"))
            {
                this.Contains = ((string)(value));
                return;
            }
            if ((feature == "SUBDOMAINOF"))
            {
                this.SubDomainOf = ((string)(value));
                return;
            }
            base.SetFeature(feature, value);
        }
        
        /// <summary>
        /// Gets the property expression for the given attribute
        /// </summary>
        /// <returns>An incremental property expression</returns>
        /// <param name="attribute">The requested attribute in upper case</param>
        protected override NMF.Expressions.INotifyExpression<object> GetExpressionForAttribute(string attribute)
        {
            if ((attribute == "IS"))
            {
                return new IsProxy(this);
            }
            if ((attribute == "CONTAINS"))
            {
                return new ContainsProxy(this);
            }
            if ((attribute == "SUBDOMAINOF"))
            {
                return new SubDomainOfProxy(this);
            }
            return base.GetExpressionForAttribute(attribute);
        }
        
        /// <summary>
        /// Gets the Class for this model element
        /// </summary>
        public override IClass GetClass()
        {
            if ((_classInstance == null))
            {
                _classInstance = ((IClass)(MetaRepository.Instance.Resolve("http://cpl/1.0#//SwitchedAddress")));
            }
            return _classInstance;
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the is property
        /// </summary>
        private sealed class IsProxy : ModelPropertyChange<ISwitchedAddress, string>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public IsProxy(ISwitchedAddress modelElement) : 
                    base(modelElement, "is")
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override string Value
            {
                get
                {
                    return this.ModelElement.Is;
                }
                set
                {
                    this.ModelElement.Is = value;
                }
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the contains property
        /// </summary>
        private sealed class ContainsProxy : ModelPropertyChange<ISwitchedAddress, string>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public ContainsProxy(ISwitchedAddress modelElement) : 
                    base(modelElement, "contains")
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override string Value
            {
                get
                {
                    return this.ModelElement.Contains;
                }
                set
                {
                    this.ModelElement.Contains = value;
                }
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the subDomainOf property
        /// </summary>
        private sealed class SubDomainOfProxy : ModelPropertyChange<ISwitchedAddress, string>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public SubDomainOfProxy(ISwitchedAddress modelElement) : 
                    base(modelElement, "subDomainOf")
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override string Value
            {
                get
                {
                    return this.ModelElement.SubDomainOf;
                }
                set
                {
                    this.ModelElement.SubDomainOf = value;
                }
            }
        }
    }
}

