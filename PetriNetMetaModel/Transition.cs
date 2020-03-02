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
    /// The default implementation of the Transition class
    /// </summary>
    [XmlNamespaceAttribute("http://petrinet/1.0")]
    [XmlNamespacePrefixAttribute("petriNet")]
    [ModelRepresentationClassAttribute("http://petrinet/1.0#//Transition")]
    [DebuggerDisplayAttribute("Transition {Name}")]
    public partial class Transition : Element, ITransition, IModelElement
    {
        
        private static Lazy<ITypedElement> _incomingArcReference = new Lazy<ITypedElement>(RetrieveIncomingArcReference);
        
        /// <summary>
        /// The backing field for the IncomingArc property
        /// </summary>
        [DebuggerBrowsableAttribute(DebuggerBrowsableState.Never)]
        private TransitionIncomingArcCollection _incomingArc;
        
        private static Lazy<ITypedElement> _outgoingArcReference = new Lazy<ITypedElement>(RetrieveOutgoingArcReference);
        
        /// <summary>
        /// The backing field for the OutgoingArc property
        /// </summary>
        [DebuggerBrowsableAttribute(DebuggerBrowsableState.Never)]
        private TransitionOutgoingArcCollection _outgoingArc;
        
        private static IClass _classInstance;
        
        public Transition()
        {
            this._incomingArc = new TransitionIncomingArcCollection(this);
            this._incomingArc.CollectionChanging += this.IncomingArcCollectionChanging;
            this._incomingArc.CollectionChanged += this.IncomingArcCollectionChanged;
            this._outgoingArc = new TransitionOutgoingArcCollection(this);
            this._outgoingArc.CollectionChanging += this.OutgoingArcCollectionChanging;
            this._outgoingArc.CollectionChanged += this.OutgoingArcCollectionChanged;
        }
        
        /// <summary>
        /// The incomingArc property
        /// </summary>
        [LowerBoundAttribute(1)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content)]
        [DisplayNameAttribute("incomingArc")]
        [CategoryAttribute("Transition")]
        [XmlElementNameAttribute("incomingArc")]
        [XmlAttributeAttribute(true)]
        [XmlOppositeAttribute("to")]
        [ConstantAttribute()]
        public ISetExpression<IPlaceToTransition> IncomingArc
        {
            get
            {
                return this._incomingArc;
            }
        }
        
        /// <summary>
        /// The outgoingArc property
        /// </summary>
        [LowerBoundAttribute(1)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content)]
        [DisplayNameAttribute("outgoingArc")]
        [CategoryAttribute("Transition")]
        [XmlElementNameAttribute("outgoingArc")]
        [XmlAttributeAttribute(true)]
        [XmlOppositeAttribute("from")]
        [ConstantAttribute()]
        public ISetExpression<ITransitionToPlace> OutgoingArc
        {
            get
            {
                return this._outgoingArc;
            }
        }
        
        /// <summary>
        /// Gets the referenced model elements of this model element
        /// </summary>
        public override IEnumerableExpression<IModelElement> ReferencedElements
        {
            get
            {
                return base.ReferencedElements.Concat(new TransitionReferencedElementsCollection(this));
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
                    _classInstance = ((IClass)(MetaRepository.Instance.Resolve("http://petrinet/1.0#//Transition")));
                }
                return _classInstance;
            }
        }
        
        private static ITypedElement RetrieveIncomingArcReference()
        {
            return ((ITypedElement)(((ModelElement)(MDEMGTT.PetriNet.Transition.ClassInstance)).Resolve("incomingArc")));
        }
        
        /// <summary>
        /// Forwards CollectionChanging notifications for the IncomingArc property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void IncomingArcCollectionChanging(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanging("IncomingArc", e, _incomingArcReference);
        }
        
        /// <summary>
        /// Forwards CollectionChanged notifications for the IncomingArc property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void IncomingArcCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanged("IncomingArc", e, _incomingArcReference);
        }
        
        private static ITypedElement RetrieveOutgoingArcReference()
        {
            return ((ITypedElement)(((ModelElement)(MDEMGTT.PetriNet.Transition.ClassInstance)).Resolve("outgoingArc")));
        }
        
        /// <summary>
        /// Forwards CollectionChanging notifications for the OutgoingArc property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void OutgoingArcCollectionChanging(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanging("OutgoingArc", e, _outgoingArcReference);
        }
        
        /// <summary>
        /// Forwards CollectionChanged notifications for the OutgoingArc property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void OutgoingArcCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanged("OutgoingArc", e, _outgoingArcReference);
        }
        
        /// <summary>
        /// Gets the Model element collection for the given feature
        /// </summary>
        /// <returns>A non-generic list of elements</returns>
        /// <param name="feature">The requested feature</param>
        protected override System.Collections.IList GetCollectionForFeature(string feature)
        {
            if ((feature == "INCOMINGARC"))
            {
                return this._incomingArc;
            }
            if ((feature == "OUTGOINGARC"))
            {
                return this._outgoingArc;
            }
            return base.GetCollectionForFeature(feature);
        }
        
        /// <summary>
        /// Gets the Class for this model element
        /// </summary>
        public override IClass GetClass()
        {
            if ((_classInstance == null))
            {
                _classInstance = ((IClass)(MetaRepository.Instance.Resolve("http://petrinet/1.0#//Transition")));
            }
            return _classInstance;
        }
        
        /// <summary>
        /// The collection class to to represent the children of the Transition class
        /// </summary>
        public class TransitionReferencedElementsCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private Transition _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public TransitionReferencedElementsCollection(Transition parent)
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
                    count = (count + this._parent.IncomingArc.Count);
                    count = (count + this._parent.OutgoingArc.Count);
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.IncomingArc.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
                this._parent.OutgoingArc.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.IncomingArc.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
                this._parent.OutgoingArc.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                IPlaceToTransition incomingArcCasted = item.As<IPlaceToTransition>();
                if ((incomingArcCasted != null))
                {
                    this._parent.IncomingArc.Add(incomingArcCasted);
                }
                ITransitionToPlace outgoingArcCasted = item.As<ITransitionToPlace>();
                if ((outgoingArcCasted != null))
                {
                    this._parent.OutgoingArc.Add(outgoingArcCasted);
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.IncomingArc.Clear();
                this._parent.OutgoingArc.Clear();
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if (this._parent.IncomingArc.Contains(item))
                {
                    return true;
                }
                if (this._parent.OutgoingArc.Contains(item))
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
                IEnumerator<IModelElement> incomingArcEnumerator = this._parent.IncomingArc.GetEnumerator();
                try
                {
                    for (
                    ; incomingArcEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = incomingArcEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    incomingArcEnumerator.Dispose();
                }
                IEnumerator<IModelElement> outgoingArcEnumerator = this._parent.OutgoingArc.GetEnumerator();
                try
                {
                    for (
                    ; outgoingArcEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = outgoingArcEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    outgoingArcEnumerator.Dispose();
                }
            }
            
            /// <summary>
            /// Removes the given item from the collection
            /// </summary>
            /// <returns>True, if the item was removed, otherwise False</returns>
            /// <param name="item">The item that should be removed</param>
            public override bool Remove(IModelElement item)
            {
                IPlaceToTransition placeToTransitionItem = item.As<IPlaceToTransition>();
                if (((placeToTransitionItem != null) 
                            && this._parent.IncomingArc.Remove(placeToTransitionItem)))
                {
                    return true;
                }
                ITransitionToPlace transitionToPlaceItem = item.As<ITransitionToPlace>();
                if (((transitionToPlaceItem != null) 
                            && this._parent.OutgoingArc.Remove(transitionToPlaceItem)))
                {
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
                return Enumerable.Empty<IModelElement>().Concat(this._parent.IncomingArc).Concat(this._parent.OutgoingArc).GetEnumerator();
            }
        }
    }
}

