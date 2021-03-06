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
    /// The default implementation of the PrioritySwitch class
    /// </summary>
    [XmlNamespaceAttribute("http://cpl/1.0")]
    [XmlNamespacePrefixAttribute("CPL")]
    [ModelRepresentationClassAttribute("http://cpl/1.0#//PrioritySwitch")]
    public partial class PrioritySwitch : Switch, IPrioritySwitch, IModelElement
    {
        
        private static Lazy<ITypedElement> _prioritiesReference = new Lazy<ITypedElement>(RetrievePrioritiesReference);
        
        /// <summary>
        /// The backing field for the Priorities property
        /// </summary>
        [DebuggerBrowsableAttribute(DebuggerBrowsableState.Never)]
        private ObservableCompositionOrderedSet<ISwitchedPriority> _priorities;
        
        private static IClass _classInstance;
        
        public PrioritySwitch()
        {
            this._priorities = new ObservableCompositionOrderedSet<ISwitchedPriority>(this);
            this._priorities.CollectionChanging += this.PrioritiesCollectionChanging;
            this._priorities.CollectionChanged += this.PrioritiesCollectionChanged;
        }
        
        /// <summary>
        /// The priorities property
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content)]
        [BrowsableAttribute(false)]
        [XmlElementNameAttribute("priorities")]
        [XmlAttributeAttribute(false)]
        [ContainmentAttribute()]
        [ConstantAttribute()]
        public IOrderedSetExpression<ISwitchedPriority> Priorities
        {
            get
            {
                return this._priorities;
            }
        }
        
        /// <summary>
        /// Gets the child model elements of this model element
        /// </summary>
        public override IEnumerableExpression<IModelElement> Children
        {
            get
            {
                return base.Children.Concat(new PrioritySwitchChildrenCollection(this));
            }
        }
        
        /// <summary>
        /// Gets the referenced model elements of this model element
        /// </summary>
        public override IEnumerableExpression<IModelElement> ReferencedElements
        {
            get
            {
                return base.ReferencedElements.Concat(new PrioritySwitchReferencedElementsCollection(this));
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
                    _classInstance = ((IClass)(MetaRepository.Instance.Resolve("http://cpl/1.0#//PrioritySwitch")));
                }
                return _classInstance;
            }
        }
        
        private static ITypedElement RetrievePrioritiesReference()
        {
            return ((ITypedElement)(((ModelElement)(MDEMGTT.CPL.PrioritySwitch.ClassInstance)).Resolve("priorities")));
        }
        
        /// <summary>
        /// Forwards CollectionChanging notifications for the Priorities property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void PrioritiesCollectionChanging(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanging("Priorities", e, _prioritiesReference);
        }
        
        /// <summary>
        /// Forwards CollectionChanged notifications for the Priorities property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void PrioritiesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanged("Priorities", e, _prioritiesReference);
        }
        
        /// <summary>
        /// Gets the relative URI fragment for the given child model element
        /// </summary>
        /// <returns>A fragment of the relative URI</returns>
        /// <param name="element">The element that should be looked for</param>
        protected override string GetRelativePathForNonIdentifiedChild(IModelElement element)
        {
            int prioritiesIndex = ModelHelper.IndexOfReference(this.Priorities, element);
            if ((prioritiesIndex != -1))
            {
                return ModelHelper.CreatePath("priorities", prioritiesIndex);
            }
            return base.GetRelativePathForNonIdentifiedChild(element);
        }
        
        /// <summary>
        /// Resolves the given URI to a child model element
        /// </summary>
        /// <returns>The model element or null if it could not be found</returns>
        /// <param name="reference">The requested reference name</param>
        /// <param name="index">The index of this reference</param>
        protected override IModelElement GetModelElementForReference(string reference, int index)
        {
            if ((reference == "PRIORITIES"))
            {
                if ((index < this.Priorities.Count))
                {
                    return this.Priorities[index];
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
            if ((feature == "PRIORITIES"))
            {
                return this._priorities;
            }
            return base.GetCollectionForFeature(feature);
        }
        
        /// <summary>
        /// Gets the property name for the given container
        /// </summary>
        /// <returns>The name of the respective container reference</returns>
        /// <param name="container">The container object</param>
        protected override string GetCompositionName(object container)
        {
            if ((container == this._priorities))
            {
                return "priorities";
            }
            return base.GetCompositionName(container);
        }
        
        /// <summary>
        /// Gets the Class for this model element
        /// </summary>
        public override IClass GetClass()
        {
            if ((_classInstance == null))
            {
                _classInstance = ((IClass)(MetaRepository.Instance.Resolve("http://cpl/1.0#//PrioritySwitch")));
            }
            return _classInstance;
        }
        
        /// <summary>
        /// The collection class to to represent the children of the PrioritySwitch class
        /// </summary>
        public class PrioritySwitchChildrenCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private PrioritySwitch _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public PrioritySwitchChildrenCollection(PrioritySwitch parent)
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
                    count = (count + this._parent.Priorities.Count);
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.Priorities.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.Priorities.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                ISwitchedPriority prioritiesCasted = item.As<ISwitchedPriority>();
                if ((prioritiesCasted != null))
                {
                    this._parent.Priorities.Add(prioritiesCasted);
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.Priorities.Clear();
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if (this._parent.Priorities.Contains(item))
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
                IEnumerator<IModelElement> prioritiesEnumerator = this._parent.Priorities.GetEnumerator();
                try
                {
                    for (
                    ; prioritiesEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = prioritiesEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    prioritiesEnumerator.Dispose();
                }
            }
            
            /// <summary>
            /// Removes the given item from the collection
            /// </summary>
            /// <returns>True, if the item was removed, otherwise False</returns>
            /// <param name="item">The item that should be removed</param>
            public override bool Remove(IModelElement item)
            {
                ISwitchedPriority switchedPriorityItem = item.As<ISwitchedPriority>();
                if (((switchedPriorityItem != null) 
                            && this._parent.Priorities.Remove(switchedPriorityItem)))
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
                return Enumerable.Empty<IModelElement>().Concat(this._parent.Priorities).GetEnumerator();
            }
        }
        
        /// <summary>
        /// The collection class to to represent the children of the PrioritySwitch class
        /// </summary>
        public class PrioritySwitchReferencedElementsCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private PrioritySwitch _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public PrioritySwitchReferencedElementsCollection(PrioritySwitch parent)
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
                    count = (count + this._parent.Priorities.Count);
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.Priorities.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.Priorities.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                ISwitchedPriority prioritiesCasted = item.As<ISwitchedPriority>();
                if ((prioritiesCasted != null))
                {
                    this._parent.Priorities.Add(prioritiesCasted);
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.Priorities.Clear();
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if (this._parent.Priorities.Contains(item))
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
                IEnumerator<IModelElement> prioritiesEnumerator = this._parent.Priorities.GetEnumerator();
                try
                {
                    for (
                    ; prioritiesEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = prioritiesEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    prioritiesEnumerator.Dispose();
                }
            }
            
            /// <summary>
            /// Removes the given item from the collection
            /// </summary>
            /// <returns>True, if the item was removed, otherwise False</returns>
            /// <param name="item">The item that should be removed</param>
            public override bool Remove(IModelElement item)
            {
                ISwitchedPriority switchedPriorityItem = item.As<ISwitchedPriority>();
                if (((switchedPriorityItem != null) 
                            && this._parent.Priorities.Remove(switchedPriorityItem)))
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
                return Enumerable.Empty<IModelElement>().Concat(this._parent.Priorities).GetEnumerator();
            }
        }
    }
}

