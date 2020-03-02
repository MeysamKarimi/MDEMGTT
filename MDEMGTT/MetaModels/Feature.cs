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

namespace MDEMGTT.ArchitectureCRA
{
    
    
    /// <summary>
    /// The default implementation of the Feature class
    /// </summary>
    [XmlNamespaceAttribute("http://momot.big.tuwien.ac.at/architectureCRA/1.0")]
    [XmlNamespacePrefixAttribute("architectureCRA")]
    [ModelRepresentationClassAttribute("http://momot.big.tuwien.ac.at/architectureCRA/1.0#//Feature")]
    [DebuggerDisplayAttribute("Feature {Name}")]
    public abstract partial class Feature : NamedElement, IFeature, IModelElement
    {
        
        private static Lazy<ITypedElement> _isEncapsulatedByReference = new Lazy<ITypedElement>(RetrieveIsEncapsulatedByReference);
        
        /// <summary>
        /// The backing field for the IsEncapsulatedBy property
        /// </summary>
        [DebuggerBrowsableAttribute(DebuggerBrowsableState.Never)]
        private MDEMGTT.ArchitectureCRA.IClass _isEncapsulatedBy;
        
        private static NMF.Models.Meta.IClass _classInstance;
        
        /// <summary>
        /// The isEncapsulatedBy property
        /// </summary>
        [DisplayNameAttribute("isEncapsulatedBy")]
        [CategoryAttribute("Feature")]
        [XmlElementNameAttribute("isEncapsulatedBy")]
        [XmlAttributeAttribute(true)]
        [XmlOppositeAttribute("encapsulates")]
        public MDEMGTT.ArchitectureCRA.IClass IsEncapsulatedBy
        {
            get
            {
                return this._isEncapsulatedBy;
            }
            set
            {
                if ((this._isEncapsulatedBy != value))
                {
                    MDEMGTT.ArchitectureCRA.IClass old = this._isEncapsulatedBy;
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnIsEncapsulatedByChanging(e);
                    this.OnPropertyChanging("IsEncapsulatedBy", e, _isEncapsulatedByReference);
                    this._isEncapsulatedBy = value;
                    if ((old != null))
                    {
                        old.Encapsulates.Remove(this);
                        old.Deleted -= this.OnResetIsEncapsulatedBy;
                    }
                    if ((value != null))
                    {
                        value.Encapsulates.Add(this);
                        value.Deleted += this.OnResetIsEncapsulatedBy;
                    }
                    this.OnIsEncapsulatedByChanged(e);
                    this.OnPropertyChanged("IsEncapsulatedBy", e, _isEncapsulatedByReference);
                }
            }
        }
        
        /// <summary>
        /// Gets the referenced model elements of this model element
        /// </summary>
        public override IEnumerableExpression<IModelElement> ReferencedElements
        {
            get
            {
                return base.ReferencedElements.Concat(new FeatureReferencedElementsCollection(this));
            }
        }
        
        /// <summary>
        /// Gets the Class model for this type
        /// </summary>
        public new static NMF.Models.Meta.IClass ClassInstance
        {
            get
            {
                if ((_classInstance == null))
                {
                    _classInstance = ((NMF.Models.Meta.IClass)(MetaRepository.Instance.Resolve("http://momot.big.tuwien.ac.at/architectureCRA/1.0#//Feature")));
                }
                return _classInstance;
            }
        }
        
        /// <summary>
        /// Gets fired before the IsEncapsulatedBy property changes its value
        /// </summary>
        public event System.EventHandler<ValueChangedEventArgs> IsEncapsulatedByChanging;
        
        /// <summary>
        /// Gets fired when the IsEncapsulatedBy property changed its value
        /// </summary>
        public event System.EventHandler<ValueChangedEventArgs> IsEncapsulatedByChanged;
        
        private static ITypedElement RetrieveIsEncapsulatedByReference()
        {
            return ((ITypedElement)(((ModelElement)(MDEMGTT.ArchitectureCRA.Feature.ClassInstance)).Resolve("isEncapsulatedBy")));
        }
        
        /// <summary>
        /// Raises the IsEncapsulatedByChanging event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnIsEncapsulatedByChanging(ValueChangedEventArgs eventArgs)
        {
            System.EventHandler<ValueChangedEventArgs> handler = this.IsEncapsulatedByChanging;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the IsEncapsulatedByChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnIsEncapsulatedByChanged(ValueChangedEventArgs eventArgs)
        {
            System.EventHandler<ValueChangedEventArgs> handler = this.IsEncapsulatedByChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Handles the event that the IsEncapsulatedBy property must reset
        /// </summary>
        /// <param name="sender">The object that sent this reset request</param>
        /// <param name="eventArgs">The event data for the reset event</param>
        private void OnResetIsEncapsulatedBy(object sender, System.EventArgs eventArgs)
        {
            this.IsEncapsulatedBy = null;
        }
        
        /// <summary>
        /// Resolves the given URI to a child model element
        /// </summary>
        /// <returns>The model element or null if it could not be found</returns>
        /// <param name="reference">The requested reference name</param>
        /// <param name="index">The index of this reference</param>
        protected override IModelElement GetModelElementForReference(string reference, int index)
        {
            if ((reference == "ISENCAPSULATEDBY"))
            {
                return this.IsEncapsulatedBy;
            }
            return base.GetModelElementForReference(reference, index);
        }
        
        /// <summary>
        /// Sets a value to the given feature
        /// </summary>
        /// <param name="feature">The requested feature</param>
        /// <param name="value">The value that should be set to that feature</param>
        protected override void SetFeature(string feature, object value)
        {
            if ((feature == "ISENCAPSULATEDBY"))
            {
                this.IsEncapsulatedBy = ((MDEMGTT.ArchitectureCRA.IClass)(value));
                return;
            }
            base.SetFeature(feature, value);
        }
        
        /// <summary>
        /// Gets the property expression for the given reference
        /// </summary>
        /// <returns>An incremental property expression</returns>
        /// <param name="reference">The requested reference in upper case</param>
        protected override NMF.Expressions.INotifyExpression<NMF.Models.IModelElement> GetExpressionForReference(string reference)
        {
            if ((reference == "ISENCAPSULATEDBY"))
            {
                return new IsEncapsulatedByProxy(this);
            }
            return base.GetExpressionForReference(reference);
        }
        
        /// <summary>
        /// Gets the Class for this model element
        /// </summary>
        public override NMF.Models.Meta.IClass GetClass()
        {
            if ((_classInstance == null))
            {
                _classInstance = ((NMF.Models.Meta.IClass)(MetaRepository.Instance.Resolve("http://momot.big.tuwien.ac.at/architectureCRA/1.0#//Feature")));
            }
            return _classInstance;
        }
        
        /// <summary>
        /// The collection class to to represent the children of the Feature class
        /// </summary>
        public class FeatureReferencedElementsCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private Feature _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public FeatureReferencedElementsCollection(Feature parent)
            {
                this._parent = parent;
            }
            
            /// <summary>
            /// Gets the amount of elements contained in this collection
            /// </summary>
            public override int Count
            {
                get
                {
                    int count = 0;
                    if ((this._parent.IsEncapsulatedBy != null))
                    {
                        count = (count + 1);
                    }
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.IsEncapsulatedByChanged += this.PropagateValueChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.IsEncapsulatedByChanged -= this.PropagateValueChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                if ((this._parent.IsEncapsulatedBy == null))
                {
                    MDEMGTT.ArchitectureCRA.IClass isEncapsulatedByCasted = item.As<MDEMGTT.ArchitectureCRA.IClass>();
                    if ((isEncapsulatedByCasted != null))
                    {
                        this._parent.IsEncapsulatedBy = isEncapsulatedByCasted;
                        return;
                    }
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.IsEncapsulatedBy = null;
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if ((item == this._parent.IsEncapsulatedBy))
                {
                    return true;
                }
                return false;
            }
            
            /// <summary>
            /// Copies the contents of the collection to the given array starting from the given array index
            /// </summary>
            /// <param name="array">The array in which the elements should be copied</param>
            /// <param name="arrayIndex">The starting index</param>
            public override void CopyTo(IModelElement[] array, int arrayIndex)
            {
                if ((this._parent.IsEncapsulatedBy != null))
                {
                    array[arrayIndex] = this._parent.IsEncapsulatedBy;
                    arrayIndex = (arrayIndex + 1);
                }
            }
            
            /// <summary>
            /// Removes the given item from the collection
            /// </summary>
            /// <returns>True, if the item was removed, otherwise False</returns>
            /// <param name="item">The item that should be removed</param>
            public override bool Remove(IModelElement item)
            {
                if ((this._parent.IsEncapsulatedBy == item))
                {
                    this._parent.IsEncapsulatedBy = null;
                    return true;
                }
                return false;
            }
            
            /// <summary>
            /// Gets an enumerator that enumerates the collection
            /// </summary>
            /// <returns>A generic enumerator</returns>
            public override IEnumerator<IModelElement> GetEnumerator()
            {
                return Enumerable.Empty<IModelElement>().Concat(this._parent.IsEncapsulatedBy).GetEnumerator();
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the isEncapsulatedBy property
        /// </summary>
        private sealed class IsEncapsulatedByProxy : ModelPropertyChange<IFeature, MDEMGTT.ArchitectureCRA.IClass>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public IsEncapsulatedByProxy(IFeature modelElement) : 
                    base(modelElement, "isEncapsulatedBy")
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override MDEMGTT.ArchitectureCRA.IClass Value
            {
                get
                {
                    return this.ModelElement.IsEncapsulatedBy;
                }
                set
                {
                    this.ModelElement.IsEncapsulatedBy = value;
                }
            }
        }
    }
}
