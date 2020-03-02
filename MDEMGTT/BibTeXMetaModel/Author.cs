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
    /// The default implementation of the Author class
    /// </summary>
    [XmlNamespaceAttribute("http://bibtex/1.0")]
    [XmlNamespacePrefixAttribute("BibTeX")]
    [ModelRepresentationClassAttribute("http://bibtex/1.0#//Author")]
    public partial class Author : ModelElement, IAuthor, IModelElement
    {
        
        /// <summary>
        /// The backing field for the Author_ property
        /// </summary>
        [DebuggerBrowsableAttribute(DebuggerBrowsableState.Never)]
        private string _author_;
        
        private static Lazy<ITypedElement> _authorAttribute = new Lazy<ITypedElement>(RetrieveAuthorAttribute);
        
        private static IClass _classInstance;
        
        /// <summary>
        /// The author property
        /// </summary>
        [DisplayNameAttribute("author")]
        [CategoryAttribute("Author")]
        [XmlElementNameAttribute("author")]
        [XmlAttributeAttribute(true)]
        public string Author_
        {
            get
            {
                return this._author_;
            }
            set
            {
                if ((this._author_ != value))
                {
                    string old = this._author_;
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnAuthor_Changing(e);
                    this.OnPropertyChanging("Author_", e, _authorAttribute);
                    this._author_ = value;
                    this.OnAuthor_Changed(e);
                    this.OnPropertyChanged("Author_", e, _authorAttribute);
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
                    _classInstance = ((IClass)(MetaRepository.Instance.Resolve("http://bibtex/1.0#//Author")));
                }
                return _classInstance;
            }
        }
        
        /// <summary>
        /// Gets fired before the Author_ property changes its value
        /// </summary>
        public event System.EventHandler<ValueChangedEventArgs> Author_Changing;
        
        /// <summary>
        /// Gets fired when the Author_ property changed its value
        /// </summary>
        public event System.EventHandler<ValueChangedEventArgs> Author_Changed;
        
        private static ITypedElement RetrieveAuthorAttribute()
        {
            return ((ITypedElement)(((ModelElement)(MDEMGTT.BibTeX.Author.ClassInstance)).Resolve("author")));
        }
        
        /// <summary>
        /// Raises the Author_Changing event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnAuthor_Changing(ValueChangedEventArgs eventArgs)
        {
            System.EventHandler<ValueChangedEventArgs> handler = this.Author_Changing;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the Author_Changed event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnAuthor_Changed(ValueChangedEventArgs eventArgs)
        {
            System.EventHandler<ValueChangedEventArgs> handler = this.Author_Changed;
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
            if ((attribute == "AUTHOR"))
            {
                return this.Author_;
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
            if ((feature == "AUTHOR"))
            {
                this.Author_ = ((string)(value));
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
            if ((attribute == "AUTHOR_"))
            {
                return new AuthorProxy(this);
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
                _classInstance = ((IClass)(MetaRepository.Instance.Resolve("http://bibtex/1.0#//Author")));
            }
            return _classInstance;
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the author property
        /// </summary>
        private sealed class AuthorProxy : ModelPropertyChange<IAuthor, string>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public AuthorProxy(IAuthor modelElement) : 
                    base(modelElement, "author")
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override string Value
            {
                get
                {
                    return this.ModelElement.Author_;
                }
                set
                {
                    this.ModelElement.Author_ = value;
                }
            }
        }
    }
}

