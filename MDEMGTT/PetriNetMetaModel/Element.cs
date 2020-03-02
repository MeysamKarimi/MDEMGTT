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

namespace MDEMGTT.PetriNet
{
    
    
    /// <summary>
    /// The default implementation of the Element class
    /// </summary>
    [XmlNamespaceAttribute("http://petrinet/1.0")]
    [XmlNamespacePrefixAttribute("petriNet")]
    [ModelRepresentationClassAttribute("http://petrinet/1.0#//Element")]
    [DebuggerDisplayAttribute("Element {Name}")]
    public abstract partial class Element : NamedElement, IElement, IModelElement
    {
        
        private static Lazy<ITypedElement> _netReference = new Lazy<ITypedElement>(RetrieveNetReference);
        
        private static IClass _classInstance;
        
        /// <summary>
        /// The net property
        /// </summary>
        [BrowsableAttribute(false)]
        [XmlElementNameAttribute("net")]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        [XmlAttributeAttribute(true)]
        [XmlOppositeAttribute("elements")]
        public IPetriNet Net
        {
            get
            {
                return ModelHelper.CastAs<IPetriNet>(this.Parent);
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
                return base.ReferencedElements.Concat(new ElementReferencedElementsCollection(this));
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
                    _classInstance = ((IClass)(MetaRepository.Instance.Resolve("http://petrinet/1.0#//Element")));
                }
                return _classInstance;
            }
        }
        
        /// <summary>
        /// Gets fired before the Net property changes its value
        /// </summary>
        public event System.EventHandler<ValueChangedEventArgs> NetChanging;
        
        /// <summary>
        /// Gets fired when the Net property changed its value
        /// </summary>
        public event System.EventHandler<ValueChangedEventArgs> NetChanged;
        
        private static ITypedElement RetrieveNetReference()
        {
            return ((ITypedElement)(((ModelElement)(MDEMGTT.PetriNet.Element.ClassInstance)).Resolve("net")));
        }
        
        /// <summary>
        /// Raises the NetChanging event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnNetChanging(ValueChangedEventArgs eventArgs)
        {
            System.EventHandler<ValueChangedEventArgs> handler = this.NetChanging;
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
            IPetriNet oldNet = ModelHelper.CastAs<IPetriNet>(oldParent);
            IPetriNet newNet = ModelHelper.CastAs<IPetriNet>(newParent);
            ValueChangedEventArgs e = new ValueChangedEventArgs(oldNet, newNet);
            this.OnNetChanging(e);
            this.OnPropertyChanging("Net", e, _netReference);
        }
        
        /// <summary>
        /// Raises the NetChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnNetChanged(ValueChangedEventArgs eventArgs)
        {
            System.EventHandler<ValueChangedEventArgs> handler = this.NetChanged;
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
            IPetriNet oldNet = ModelHelper.CastAs<IPetriNet>(oldParent);
            IPetriNet newNet = ModelHelper.CastAs<IPetriNet>(newParent);
            if ((oldNet != null))
            {
                oldNet.Elements.Remove(this);
            }
            if ((newNet != null))
            {
                newNet.Elements.Add(this);
            }
            ValueChangedEventArgs e = new ValueChangedEventArgs(oldNet, newNet);
            this.OnNetChanged(e);
            this.OnPropertyChanged("Net", e, _netReference);
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
            if ((reference == "NET"))
            {
                return this.Net;
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
            if ((feature == "NET"))
            {
                this.Net = ((IPetriNet)(value));
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
            if ((reference == "NET"))
            {
                return new NetProxy(this);
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
                _classInstance = ((IClass)(MetaRepository.Instance.Resolve("http://petrinet/1.0#//Element")));
            }
            return _classInstance;
        }
        
        /// <summary>
        /// The collection class to to represent the children of the Element class
        /// </summary>
        public class ElementReferencedElementsCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private Element _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public ElementReferencedElementsCollection(Element parent)
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
                    if ((this._parent.Net != null))
                    {
                        count = (count + 1);
                    }
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.NetChanged += this.PropagateValueChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.NetChanged -= this.PropagateValueChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                if ((this._parent.Net == null))
                {
                    IPetriNet netCasted = item.As<IPetriNet>();
                    if ((netCasted != null))
                    {
                        this._parent.Net = netCasted;
                        return;
                    }
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.Net = null;
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if ((item == this._parent.Net))
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
                if ((this._parent.Net != null))
                {
                    array[arrayIndex] = this._parent.Net;
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
                if ((this._parent.Net == item))
                {
                    this._parent.Net = null;
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
                return Enumerable.Empty<IModelElement>().Concat(this._parent.Net).GetEnumerator();
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the net property
        /// </summary>
        private sealed class NetProxy : ModelPropertyChange<IElement, IPetriNet>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public NetProxy(IElement modelElement) : 
                    base(modelElement, "net")
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override IPetriNet Value
            {
                get
                {
                    return this.ModelElement.Net;
                }
                set
                {
                    this.ModelElement.Net = value;
                }
            }
        }
    }
}
