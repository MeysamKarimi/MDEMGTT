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
    /// The default implementation of the Connection class
    /// </summary>
    [XmlNamespaceAttribute("http://grafcet/1.0")]
    [XmlNamespacePrefixAttribute("grafcet")]
    [ModelRepresentationClassAttribute("http://grafcet/1.0#//Connection")]
    [DebuggerDisplayAttribute("Connection {Name}")]
    public abstract partial class Connection : NamedElement, IConnection, IModelElement
    {
        
        private static Lazy<ITypedElement> _grafcetReference = new Lazy<ITypedElement>(RetrieveGrafcetReference);
        
        private static IClass _classInstance;
        
        /// <summary>
        /// The grafcet property
        /// </summary>
        [BrowsableAttribute(false)]
        [XmlElementNameAttribute("grafcet")]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        [XmlAttributeAttribute(true)]
        [XmlOppositeAttribute("connections")]
        public IGrafcet Grafcet
        {
            get
            {
                return ModelHelper.CastAs<IGrafcet>(this.Parent);
            }
            set
            {
                this.Parent = value;
            }
        }
        
        /// <summary>
        /// Gets the referenced model elements of this model element
        /// </summary>
        public override IEnumerableExpression<IModelElement> ReferencedElements
        {
            get
            {
                return base.ReferencedElements.Concat(new ConnectionReferencedElementsCollection(this));
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
                    _classInstance = ((IClass)(MetaRepository.Instance.Resolve("http://grafcet/1.0#//Connection")));
                }
                return _classInstance;
            }
        }
        
        /// <summary>
        /// Gets fired before the Grafcet property changes its value
        /// </summary>
        public event System.EventHandler<ValueChangedEventArgs> GrafcetChanging;
        
        /// <summary>
        /// Gets fired when the Grafcet property changed its value
        /// </summary>
        public event System.EventHandler<ValueChangedEventArgs> GrafcetChanged;
        
        private static ITypedElement RetrieveGrafcetReference()
        {
            return ((ITypedElement)(((ModelElement)(MDEMGTT.Grafcet.Connection.ClassInstance)).Resolve("grafcet")));
        }
        
        /// <summary>
        /// Raises the GrafcetChanging event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnGrafcetChanging(ValueChangedEventArgs eventArgs)
        {
            System.EventHandler<ValueChangedEventArgs> handler = this.GrafcetChanging;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Gets called when the parent model element of the current model element is about to change
        /// </summary>
        /// <param name="oldParent">The old parent model element</param>
        /// <param name="newParent">The new parent model element</param>
        protected override void OnParentChanging(IModelElement newParent, IModelElement oldParent)
        {
            IGrafcet oldGrafcet = ModelHelper.CastAs<IGrafcet>(oldParent);
            IGrafcet newGrafcet = ModelHelper.CastAs<IGrafcet>(newParent);
            ValueChangedEventArgs e = new ValueChangedEventArgs(oldGrafcet, newGrafcet);
            this.OnGrafcetChanging(e);
            this.OnPropertyChanging("Grafcet", e, _grafcetReference);
        }
        
        /// <summary>
        /// Raises the GrafcetChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnGrafcetChanged(ValueChangedEventArgs eventArgs)
        {
            System.EventHandler<ValueChangedEventArgs> handler = this.GrafcetChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Gets called when the parent model element of the current model element changes
        /// </summary>
        /// <param name="oldParent">The old parent model element</param>
        /// <param name="newParent">The new parent model element</param>
        protected override void OnParentChanged(IModelElement newParent, IModelElement oldParent)
        {
            IGrafcet oldGrafcet = ModelHelper.CastAs<IGrafcet>(oldParent);
            IGrafcet newGrafcet = ModelHelper.CastAs<IGrafcet>(newParent);
            if ((oldGrafcet != null))
            {
                oldGrafcet.Connections.Remove(this);
            }
            if ((newGrafcet != null))
            {
                newGrafcet.Connections.Add(this);
            }
            ValueChangedEventArgs e = new ValueChangedEventArgs(oldGrafcet, newGrafcet);
            this.OnGrafcetChanged(e);
            this.OnPropertyChanged("Grafcet", e, _grafcetReference);
            base.OnParentChanged(newParent, oldParent);
        }
        
        /// <summary>
        /// Resolves the given URI to a child model element
        /// </summary>
        /// <returns>The model element or null if it could not be found</returns>
        /// <param name="reference">The requested reference name</param>
        /// <param name="index">The index of this reference</param>
        protected override IModelElement GetModelElementForReference(string reference, int index)
        {
            if ((reference == "GRAFCET"))
            {
                return this.Grafcet;
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
            if ((feature == "GRAFCET"))
            {
                this.Grafcet = ((IGrafcet)(value));
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
            if ((reference == "GRAFCET"))
            {
                return new GrafcetProxy(this);
            }
            return base.GetExpressionForReference(reference);
        }
        
        /// <summary>
        /// Gets the Class for this model element
        /// </summary>
        public override IClass GetClass()
        {
            if ((_classInstance == null))
            {
                _classInstance = ((IClass)(MetaRepository.Instance.Resolve("http://grafcet/1.0#//Connection")));
            }
            return _classInstance;
        }
        
        /// <summary>
        /// The collection class to to represent the children of the Connection class
        /// </summary>
        public class ConnectionReferencedElementsCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private Connection _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public ConnectionReferencedElementsCollection(Connection parent)
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
                    if ((this._parent.Grafcet != null))
                    {
                        count = (count + 1);
                    }
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.GrafcetChanged += this.PropagateValueChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.GrafcetChanged -= this.PropagateValueChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                if ((this._parent.Grafcet == null))
                {
                    IGrafcet grafcetCasted = item.As<IGrafcet>();
                    if ((grafcetCasted != null))
                    {
                        this._parent.Grafcet = grafcetCasted;
                        return;
                    }
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.Grafcet = null;
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if ((item == this._parent.Grafcet))
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
                if ((this._parent.Grafcet != null))
                {
                    array[arrayIndex] = this._parent.Grafcet;
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
                if ((this._parent.Grafcet == item))
                {
                    this._parent.Grafcet = null;
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
                return Enumerable.Empty<IModelElement>().Concat(this._parent.Grafcet).GetEnumerator();
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the grafcet property
        /// </summary>
        private sealed class GrafcetProxy : ModelPropertyChange<IConnection, IGrafcet>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public GrafcetProxy(IConnection modelElement) : 
                    base(modelElement, "grafcet")
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override IGrafcet Value
            {
                get
                {
                    return this.ModelElement.Grafcet;
                }
                set
                {
                    this.ModelElement.Grafcet = value;
                }
            }
        }
    }
}

