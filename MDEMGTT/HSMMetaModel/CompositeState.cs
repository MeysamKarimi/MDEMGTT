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
    /// The default implementation of the CompositeState class
    /// </summary>
    [XmlNamespaceAttribute("http://hsm/1.0")]
    [XmlNamespacePrefixAttribute("hsm")]
    [ModelRepresentationClassAttribute("http://hsm/1.0#//CompositeState")]
    [DebuggerDisplayAttribute("CompositeState {Name}")]
    public partial class CompositeState : AbstractState, ICompositeState, IModelElement
    {
        
        private static Lazy<ITypedElement> _statesReference = new Lazy<ITypedElement>(RetrieveStatesReference);
        
        /// <summary>
        /// The backing field for the States property
        /// </summary>
        [DebuggerBrowsableAttribute(DebuggerBrowsableState.Never)]
        private CompositeStateStatesCollection _states;
        
        private static IClass _classInstance;
        
        public CompositeState()
        {
            this._states = new CompositeStateStatesCollection(this);
            this._states.CollectionChanging += this.StatesCollectionChanging;
            this._states.CollectionChanged += this.StatesCollectionChanged;
        }
        
        /// <summary>
        /// The states property
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content)]
        [DisplayNameAttribute("states")]
        [CategoryAttribute("CompositeState")]
        [XmlElementNameAttribute("states")]
        [XmlAttributeAttribute(true)]
        [XmlOppositeAttribute("compositeStates")]
        [ConstantAttribute()]
        public IOrderedSetExpression<IAbstractState> States
        {
            get
            {
                return this._states;
            }
        }
        
        /// <summary>
        /// Gets the referenced model elements of this model element
        /// </summary>
        public override IEnumerableExpression<IModelElement> ReferencedElements
        {
            get
            {
                return base.ReferencedElements.Concat(new CompositeStateReferencedElementsCollection(this));
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
                    _classInstance = ((IClass)(MetaRepository.Instance.Resolve("http://hsm/1.0#//CompositeState")));
                }
                return _classInstance;
            }
        }
        
        private static ITypedElement RetrieveStatesReference()
        {
            return ((ITypedElement)(((ModelElement)(MDEMGTT.Hsm.CompositeState.ClassInstance)).Resolve("states")));
        }
        
        /// <summary>
        /// Forwards CollectionChanging notifications for the States property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void StatesCollectionChanging(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanging("States", e, _statesReference);
        }
        
        /// <summary>
        /// Forwards CollectionChanged notifications for the States property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void StatesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanged("States", e, _statesReference);
        }
        
        /// <summary>
        /// Resolves the given URI to a child model element
        /// </summary>
        /// <returns>The model element or null if it could not be found</returns>
        /// <param name="reference">The requested reference name</param>
        /// <param name="index">The index of this reference</param>
        protected override IModelElement GetModelElementForReference(string reference, int index)
        {
            if ((reference == "STATES"))
            {
                if ((index < this.States.Count))
                {
                    return this.States[index];
                }
                else
                {
                    return null;
                }
            }
            return base.GetModelElementForReference(reference, index);
        }
        
        /// <summary>
        /// Gets the Model element collection for the given feature
        /// </summary>
        /// <returns>A non-generic list of elements</returns>
        /// <param name="feature">The requested feature</param>
        protected override System.Collections.IList GetCollectionForFeature(string feature)
        {
            if ((feature == "STATES"))
            {
                return this._states;
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
                _classInstance = ((IClass)(MetaRepository.Instance.Resolve("http://hsm/1.0#//CompositeState")));
            }
            return _classInstance;
        }
        
        /// <summary>
        /// The collection class to to represent the children of the CompositeState class
        /// </summary>
        public class CompositeStateReferencedElementsCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private CompositeState _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public CompositeStateReferencedElementsCollection(CompositeState parent)
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
                    count = (count + this._parent.States.Count);
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.States.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.States.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                IAbstractState statesCasted = item.As<IAbstractState>();
                if ((statesCasted != null))
                {
                    this._parent.States.Add(statesCasted);
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.States.Clear();
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if (this._parent.States.Contains(item))
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
                IEnumerator<IModelElement> statesEnumerator = this._parent.States.GetEnumerator();
                try
                {
                    for (
                    ; statesEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = statesEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    statesEnumerator.Dispose();
                }
            }
            
            /// <summary>
            /// Removes the given item from the collection
            /// </summary>
            /// <returns>True, if the item was removed, otherwise False</returns>
            /// <param name="item">The item that should be removed</param>
            public override bool Remove(IModelElement item)
            {
                IAbstractState abstractStateItem = item.As<IAbstractState>();
                if (((abstractStateItem != null) 
                            && this._parent.States.Remove(abstractStateItem)))
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
                return Enumerable.Empty<IModelElement>().Concat(this._parent.States).GetEnumerator();
            }
        }
    }
}
