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
    /// The default implementation of the CPL class
    /// </summary>
    [XmlNamespaceAttribute("http://cpl/1.0")]
    [XmlNamespacePrefixAttribute("CPL")]
    [ModelRepresentationClassAttribute("http://cpl/1.0#//CPL")]
    public partial class CPL : Element, ICPL, IModelElement
    {
        
        private static Lazy<ITypedElement> _subActionsReference = new Lazy<ITypedElement>(RetrieveSubActionsReference);
        
        /// <summary>
        /// The backing field for the SubActions property
        /// </summary>
        [DebuggerBrowsableAttribute(DebuggerBrowsableState.Never)]
        private ObservableCompositionOrderedSet<ISubAction> _subActions;
        
        private static Lazy<ITypedElement> _outgoingReference = new Lazy<ITypedElement>(RetrieveOutgoingReference);
        
        /// <summary>
        /// The backing field for the Outgoing property
        /// </summary>
        [DebuggerBrowsableAttribute(DebuggerBrowsableState.Never)]
        private IOutgoing _outgoing;
        
        private static Lazy<ITypedElement> _incomingReference = new Lazy<ITypedElement>(RetrieveIncomingReference);
        
        /// <summary>
        /// The backing field for the Incoming property
        /// </summary>
        [DebuggerBrowsableAttribute(DebuggerBrowsableState.Never)]
        private IIncoming _incoming;
        
        private static IClass _classInstance;
        
        public CPL()
        {
            this._subActions = new ObservableCompositionOrderedSet<ISubAction>(this);
            this._subActions.CollectionChanging += this.SubActionsCollectionChanging;
            this._subActions.CollectionChanged += this.SubActionsCollectionChanged;
        }
        
        /// <summary>
        /// The subActions property
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content)]
        [BrowsableAttribute(false)]
        [XmlElementNameAttribute("subActions")]
        [XmlAttributeAttribute(false)]
        [ContainmentAttribute()]
        [ConstantAttribute()]
        public IOrderedSetExpression<ISubAction> SubActions
        {
            get
            {
                return this._subActions;
            }
        }
        
        /// <summary>
        /// The outgoing property
        /// </summary>
        [BrowsableAttribute(false)]
        [XmlElementNameAttribute("outgoing")]
        [XmlAttributeAttribute(false)]
        [ContainmentAttribute()]
        public IOutgoing Outgoing
        {
            get
            {
                return this._outgoing;
            }
            set
            {
                if ((this._outgoing != value))
                {
                    IOutgoing old = this._outgoing;
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnOutgoingChanging(e);
                    this.OnPropertyChanging("Outgoing", e, _outgoingReference);
                    this._outgoing = value;
                    if ((old != null))
                    {
                        old.Parent = null;
                        old.ParentChanged -= this.OnResetOutgoing;
                    }
                    if ((value != null))
                    {
                        value.Parent = this;
                        value.ParentChanged += this.OnResetOutgoing;
                    }
                    this.OnOutgoingChanged(e);
                    this.OnPropertyChanged("Outgoing", e, _outgoingReference);
                }
            }
        }
        
        /// <summary>
        /// The incoming property
        /// </summary>
        [BrowsableAttribute(false)]
        [XmlElementNameAttribute("incoming")]
        [XmlAttributeAttribute(false)]
        [ContainmentAttribute()]
        public IIncoming Incoming
        {
            get
            {
                return this._incoming;
            }
            set
            {
                if ((this._incoming != value))
                {
                    IIncoming old = this._incoming;
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnIncomingChanging(e);
                    this.OnPropertyChanging("Incoming", e, _incomingReference);
                    this._incoming = value;
                    if ((old != null))
                    {
                        old.Parent = null;
                        old.ParentChanged -= this.OnResetIncoming;
                    }
                    if ((value != null))
                    {
                        value.Parent = this;
                        value.ParentChanged += this.OnResetIncoming;
                    }
                    this.OnIncomingChanged(e);
                    this.OnPropertyChanged("Incoming", e, _incomingReference);
                }
            }
        }
        
        /// <summary>
        /// Gets the child model elements of this model element
        /// </summary>
        public override IEnumerableExpression<IModelElement> Children
        {
            get
            {
                return base.Children.Concat(new CPLChildrenCollection(this));
            }
        }
        
        /// <summary>
        /// Gets the referenced model elements of this model element
        /// </summary>
        public override IEnumerableExpression<IModelElement> ReferencedElements
        {
            get
            {
                return base.ReferencedElements.Concat(new CPLReferencedElementsCollection(this));
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
                    _classInstance = ((IClass)(MetaRepository.Instance.Resolve("http://cpl/1.0#//CPL")));
                }
                return _classInstance;
            }
        }
        
        /// <summary>
        /// Gets fired before the Outgoing property changes its value
        /// </summary>
        public event System.EventHandler<ValueChangedEventArgs> OutgoingChanging;
        
        /// <summary>
        /// Gets fired when the Outgoing property changed its value
        /// </summary>
        public event System.EventHandler<ValueChangedEventArgs> OutgoingChanged;
        
        /// <summary>
        /// Gets fired before the Incoming property changes its value
        /// </summary>
        public event System.EventHandler<ValueChangedEventArgs> IncomingChanging;
        
        /// <summary>
        /// Gets fired when the Incoming property changed its value
        /// </summary>
        public event System.EventHandler<ValueChangedEventArgs> IncomingChanged;
        
        private static ITypedElement RetrieveSubActionsReference()
        {
            return ((ITypedElement)(((ModelElement)(MDEMGTT.CPL.CPL.ClassInstance)).Resolve("subActions")));
        }
        
        /// <summary>
        /// Forwards CollectionChanging notifications for the SubActions property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void SubActionsCollectionChanging(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanging("SubActions", e, _subActionsReference);
        }
        
        /// <summary>
        /// Forwards CollectionChanged notifications for the SubActions property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void SubActionsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanged("SubActions", e, _subActionsReference);
        }
        
        private static ITypedElement RetrieveOutgoingReference()
        {
            return ((ITypedElement)(((ModelElement)(MDEMGTT.CPL.CPL.ClassInstance)).Resolve("outgoing")));
        }
        
        /// <summary>
        /// Raises the OutgoingChanging event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnOutgoingChanging(ValueChangedEventArgs eventArgs)
        {
            System.EventHandler<ValueChangedEventArgs> handler = this.OutgoingChanging;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the OutgoingChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnOutgoingChanged(ValueChangedEventArgs eventArgs)
        {
            System.EventHandler<ValueChangedEventArgs> handler = this.OutgoingChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Handles the event that the Outgoing property must reset
        /// </summary>
        /// <param name="sender">The object that sent this reset request</param>
        /// <param name="eventArgs">The event data for the reset event</param>
        private void OnResetOutgoing(object sender, System.EventArgs eventArgs)
        {
            this.Outgoing = null;
        }
        
        private static ITypedElement RetrieveIncomingReference()
        {
            return ((ITypedElement)(((ModelElement)(MDEMGTT.CPL.CPL.ClassInstance)).Resolve("incoming")));
        }
        
        /// <summary>
        /// Raises the IncomingChanging event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnIncomingChanging(ValueChangedEventArgs eventArgs)
        {
            System.EventHandler<ValueChangedEventArgs> handler = this.IncomingChanging;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the IncomingChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnIncomingChanged(ValueChangedEventArgs eventArgs)
        {
            System.EventHandler<ValueChangedEventArgs> handler = this.IncomingChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Handles the event that the Incoming property must reset
        /// </summary>
        /// <param name="sender">The object that sent this reset request</param>
        /// <param name="eventArgs">The event data for the reset event</param>
        private void OnResetIncoming(object sender, System.EventArgs eventArgs)
        {
            this.Incoming = null;
        }
        
        /// <summary>
        /// Gets the relative URI fragment for the given child model element
        /// </summary>
        /// <returns>A fragment of the relative URI</returns>
        /// <param name="element">The element that should be looked for</param>
        protected override string GetRelativePathForNonIdentifiedChild(IModelElement element)
        {
            int subActionsIndex = ModelHelper.IndexOfReference(this.SubActions, element);
            if ((subActionsIndex != -1))
            {
                return ModelHelper.CreatePath("subActions", subActionsIndex);
            }
            if ((element == this.Outgoing))
            {
                return ModelHelper.CreatePath("Outgoing");
            }
            if ((element == this.Incoming))
            {
                return ModelHelper.CreatePath("Incoming");
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
            if ((reference == "SUBACTIONS"))
            {
                if ((index < this.SubActions.Count))
                {
                    return this.SubActions[index];
                }
                else
                {
                    return null;
                }
            }
            if ((reference == "OUTGOING"))
            {
                return this.Outgoing;
            }
            if ((reference == "INCOMING"))
            {
                return this.Incoming;
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
            if ((feature == "SUBACTIONS"))
            {
                return this._subActions;
            }
            return base.GetCollectionForFeature(feature);
        }
        
        /// <summary>
        /// Sets a value to the given feature
        /// </summary>
        /// <param name="feature">The requested feature</param>
        /// <param name="value">The value that should be set to that feature</param>
        protected override void SetFeature(string feature, object value)
        {
            if ((feature == "OUTGOING"))
            {
                this.Outgoing = ((IOutgoing)(value));
                return;
            }
            if ((feature == "INCOMING"))
            {
                this.Incoming = ((IIncoming)(value));
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
            if ((reference == "OUTGOING"))
            {
                return new OutgoingProxy(this);
            }
            if ((reference == "INCOMING"))
            {
                return new IncomingProxy(this);
            }
            return base.GetExpressionForReference(reference);
        }
        
        /// <summary>
        /// Gets the property name for the given container
        /// </summary>
        /// <returns>The name of the respective container reference</returns>
        /// <param name="container">The container object</param>
        protected override string GetCompositionName(object container)
        {
            if ((container == this._subActions))
            {
                return "subActions";
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
                _classInstance = ((IClass)(MetaRepository.Instance.Resolve("http://cpl/1.0#//CPL")));
            }
            return _classInstance;
        }
        
        /// <summary>
        /// The collection class to to represent the children of the CPL class
        /// </summary>
        public class CPLChildrenCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private CPL _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public CPLChildrenCollection(CPL parent)
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
                    count = (count + this._parent.SubActions.Count);
                    if ((this._parent.Outgoing != null))
                    {
                        count = (count + 1);
                    }
                    if ((this._parent.Incoming != null))
                    {
                        count = (count + 1);
                    }
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.SubActions.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
                this._parent.OutgoingChanged += this.PropagateValueChanges;
                this._parent.IncomingChanged += this.PropagateValueChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.SubActions.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
                this._parent.OutgoingChanged -= this.PropagateValueChanges;
                this._parent.IncomingChanged -= this.PropagateValueChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                ISubAction subActionsCasted = item.As<ISubAction>();
                if ((subActionsCasted != null))
                {
                    this._parent.SubActions.Add(subActionsCasted);
                }
                if ((this._parent.Outgoing == null))
                {
                    IOutgoing outgoingCasted = item.As<IOutgoing>();
                    if ((outgoingCasted != null))
                    {
                        this._parent.Outgoing = outgoingCasted;
                        return;
                    }
                }
                if ((this._parent.Incoming == null))
                {
                    IIncoming incomingCasted = item.As<IIncoming>();
                    if ((incomingCasted != null))
                    {
                        this._parent.Incoming = incomingCasted;
                        return;
                    }
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.SubActions.Clear();
                this._parent.Outgoing = null;
                this._parent.Incoming = null;
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if (this._parent.SubActions.Contains(item))
                {
                    return true;
                }
                if ((item == this._parent.Outgoing))
                {
                    return true;
                }
                if ((item == this._parent.Incoming))
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
                IEnumerator<IModelElement> subActionsEnumerator = this._parent.SubActions.GetEnumerator();
                try
                {
                    for (
                    ; subActionsEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = subActionsEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    subActionsEnumerator.Dispose();
                }
                if ((this._parent.Outgoing != null))
                {
                    array[arrayIndex] = this._parent.Outgoing;
                    arrayIndex = (arrayIndex + 1);
                }
                if ((this._parent.Incoming != null))
                {
                    array[arrayIndex] = this._parent.Incoming;
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
                ISubAction subActionItem = item.As<ISubAction>();
                if (((subActionItem != null) 
                            && this._parent.SubActions.Remove(subActionItem)))
                {
                    return true;
                }
                if ((this._parent.Outgoing == item))
                {
                    this._parent.Outgoing = null;
                    return true;
                }
                if ((this._parent.Incoming == item))
                {
                    this._parent.Incoming = null;
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
                return Enumerable.Empty<IModelElement>().Concat(this._parent.SubActions).Concat(this._parent.Outgoing).Concat(this._parent.Incoming).GetEnumerator();
            }
        }
        
        /// <summary>
        /// The collection class to to represent the children of the CPL class
        /// </summary>
        public class CPLReferencedElementsCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private CPL _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public CPLReferencedElementsCollection(CPL parent)
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
                    count = (count + this._parent.SubActions.Count);
                    if ((this._parent.Outgoing != null))
                    {
                        count = (count + 1);
                    }
                    if ((this._parent.Incoming != null))
                    {
                        count = (count + 1);
                    }
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.SubActions.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
                this._parent.OutgoingChanged += this.PropagateValueChanges;
                this._parent.IncomingChanged += this.PropagateValueChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.SubActions.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
                this._parent.OutgoingChanged -= this.PropagateValueChanges;
                this._parent.IncomingChanged -= this.PropagateValueChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                ISubAction subActionsCasted = item.As<ISubAction>();
                if ((subActionsCasted != null))
                {
                    this._parent.SubActions.Add(subActionsCasted);
                }
                if ((this._parent.Outgoing == null))
                {
                    IOutgoing outgoingCasted = item.As<IOutgoing>();
                    if ((outgoingCasted != null))
                    {
                        this._parent.Outgoing = outgoingCasted;
                        return;
                    }
                }
                if ((this._parent.Incoming == null))
                {
                    IIncoming incomingCasted = item.As<IIncoming>();
                    if ((incomingCasted != null))
                    {
                        this._parent.Incoming = incomingCasted;
                        return;
                    }
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.SubActions.Clear();
                this._parent.Outgoing = null;
                this._parent.Incoming = null;
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if (this._parent.SubActions.Contains(item))
                {
                    return true;
                }
                if ((item == this._parent.Outgoing))
                {
                    return true;
                }
                if ((item == this._parent.Incoming))
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
                IEnumerator<IModelElement> subActionsEnumerator = this._parent.SubActions.GetEnumerator();
                try
                {
                    for (
                    ; subActionsEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = subActionsEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    subActionsEnumerator.Dispose();
                }
                if ((this._parent.Outgoing != null))
                {
                    array[arrayIndex] = this._parent.Outgoing;
                    arrayIndex = (arrayIndex + 1);
                }
                if ((this._parent.Incoming != null))
                {
                    array[arrayIndex] = this._parent.Incoming;
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
                ISubAction subActionItem = item.As<ISubAction>();
                if (((subActionItem != null) 
                            && this._parent.SubActions.Remove(subActionItem)))
                {
                    return true;
                }
                if ((this._parent.Outgoing == item))
                {
                    this._parent.Outgoing = null;
                    return true;
                }
                if ((this._parent.Incoming == item))
                {
                    this._parent.Incoming = null;
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
                return Enumerable.Empty<IModelElement>().Concat(this._parent.SubActions).Concat(this._parent.Outgoing).Concat(this._parent.Incoming).GetEnumerator();
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the outgoing property
        /// </summary>
        private sealed class OutgoingProxy : ModelPropertyChange<ICPL, IOutgoing>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public OutgoingProxy(ICPL modelElement) : 
                    base(modelElement, "outgoing")
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override IOutgoing Value
            {
                get
                {
                    return this.ModelElement.Outgoing;
                }
                set
                {
                    this.ModelElement.Outgoing = value;
                }
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the incoming property
        /// </summary>
        private sealed class IncomingProxy : ModelPropertyChange<ICPL, IIncoming>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public IncomingProxy(ICPL modelElement) : 
                    base(modelElement, "incoming")
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override IIncoming Value
            {
                get
                {
                    return this.ModelElement.Incoming;
                }
                set
                {
                    this.ModelElement.Incoming = value;
                }
            }
        }
    }
}

