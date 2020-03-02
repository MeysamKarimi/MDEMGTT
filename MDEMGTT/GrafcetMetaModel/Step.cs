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
    /// The default implementation of the Step class
    /// </summary>
    [XmlNamespaceAttribute("http://grafcet/1.0")]
    [XmlNamespacePrefixAttribute("grafcet")]
    [ModelRepresentationClassAttribute("http://grafcet/1.0#//Step")]
    [DebuggerDisplayAttribute("Step {Name}")]
    public partial class Step : Element, IStep, IModelElement
    {
        
        /// <summary>
        /// The backing field for the IsInitial property
        /// </summary>
        [DebuggerBrowsableAttribute(DebuggerBrowsableState.Never)]
        private bool _isInitial;
        
        private static Lazy<ITypedElement> _isInitialAttribute = new Lazy<ITypedElement>(RetrieveIsInitialAttribute);
        
        /// <summary>
        /// The backing field for the IsActive property
        /// </summary>
        [DebuggerBrowsableAttribute(DebuggerBrowsableState.Never)]
        private bool _isActive;
        
        private static Lazy<ITypedElement> _isActiveAttribute = new Lazy<ITypedElement>(RetrieveIsActiveAttribute);
        
        /// <summary>
        /// The backing field for the Action property
        /// </summary>
        [DebuggerBrowsableAttribute(DebuggerBrowsableState.Never)]
        private string _action;
        
        private static Lazy<ITypedElement> _actionAttribute = new Lazy<ITypedElement>(RetrieveActionAttribute);
        
        private static Lazy<ITypedElement> _incomingConnectionsReference = new Lazy<ITypedElement>(RetrieveIncomingConnectionsReference);
        
        /// <summary>
        /// The backing field for the IncomingConnections property
        /// </summary>
        [DebuggerBrowsableAttribute(DebuggerBrowsableState.Never)]
        private StepIncomingConnectionsCollection _incomingConnections;
        
        private static Lazy<ITypedElement> _outgoingConnectionsReference = new Lazy<ITypedElement>(RetrieveOutgoingConnectionsReference);
        
        /// <summary>
        /// The backing field for the OutgoingConnections property
        /// </summary>
        [DebuggerBrowsableAttribute(DebuggerBrowsableState.Never)]
        private StepOutgoingConnectionsCollection _outgoingConnections;
        
        private static IClass _classInstance;
        
        public Step()
        {
            this._incomingConnections = new StepIncomingConnectionsCollection(this);
            this._incomingConnections.CollectionChanging += this.IncomingConnectionsCollectionChanging;
            this._incomingConnections.CollectionChanged += this.IncomingConnectionsCollectionChanged;
            this._outgoingConnections = new StepOutgoingConnectionsCollection(this);
            this._outgoingConnections.CollectionChanging += this.OutgoingConnectionsCollectionChanging;
            this._outgoingConnections.CollectionChanged += this.OutgoingConnectionsCollectionChanged;
        }
        
        /// <summary>
        /// The isInitial property
        /// </summary>
        [DisplayNameAttribute("isInitial")]
        [CategoryAttribute("Step")]
        [XmlElementNameAttribute("isInitial")]
        [XmlAttributeAttribute(true)]
        public bool IsInitial
        {
            get
            {
                return this._isInitial;
            }
            set
            {
                if ((this._isInitial != value))
                {
                    bool old = this._isInitial;
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnIsInitialChanging(e);
                    this.OnPropertyChanging("IsInitial", e, _isInitialAttribute);
                    this._isInitial = value;
                    this.OnIsInitialChanged(e);
                    this.OnPropertyChanged("IsInitial", e, _isInitialAttribute);
                }
            }
        }
        
        /// <summary>
        /// The isActive property
        /// </summary>
        [DisplayNameAttribute("isActive")]
        [CategoryAttribute("Step")]
        [XmlElementNameAttribute("isActive")]
        [XmlAttributeAttribute(true)]
        public bool IsActive
        {
            get
            {
                return this._isActive;
            }
            set
            {
                if ((this._isActive != value))
                {
                    bool old = this._isActive;
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnIsActiveChanging(e);
                    this.OnPropertyChanging("IsActive", e, _isActiveAttribute);
                    this._isActive = value;
                    this.OnIsActiveChanged(e);
                    this.OnPropertyChanged("IsActive", e, _isActiveAttribute);
                }
            }
        }
        
        /// <summary>
        /// The action property
        /// </summary>
        [DisplayNameAttribute("action")]
        [CategoryAttribute("Step")]
        [XmlElementNameAttribute("action")]
        [XmlAttributeAttribute(true)]
        public string Action
        {
            get
            {
                return this._action;
            }
            set
            {
                if ((this._action != value))
                {
                    string old = this._action;
                    ValueChangedEventArgs e = new ValueChangedEventArgs(old, value);
                    this.OnActionChanging(e);
                    this.OnPropertyChanging("Action", e, _actionAttribute);
                    this._action = value;
                    this.OnActionChanged(e);
                    this.OnPropertyChanged("Action", e, _actionAttribute);
                }
            }
        }
        
        /// <summary>
        /// The incomingConnections property
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content)]
        [DisplayNameAttribute("incomingConnections")]
        [CategoryAttribute("Step")]
        [XmlElementNameAttribute("incomingConnections")]
        [XmlAttributeAttribute(true)]
        [XmlOppositeAttribute("to")]
        [ConstantAttribute()]
        public ISetExpression<ITransitionToStep> IncomingConnections
        {
            get
            {
                return this._incomingConnections;
            }
        }
        
        /// <summary>
        /// The outgoingConnections property
        /// </summary>
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content)]
        [DisplayNameAttribute("outgoingConnections")]
        [CategoryAttribute("Step")]
        [XmlElementNameAttribute("outgoingConnections")]
        [XmlAttributeAttribute(true)]
        [XmlOppositeAttribute("from")]
        [ConstantAttribute()]
        public ISetExpression<IStepToTransition> OutgoingConnections
        {
            get
            {
                return this._outgoingConnections;
            }
        }
        
        /// <summary>
        /// Gets the referenced model elements of this model element
        /// </summary>
        public override IEnumerableExpression<IModelElement> ReferencedElements
        {
            get
            {
                return base.ReferencedElements.Concat(new StepReferencedElementsCollection(this));
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
                    _classInstance = ((IClass)(MetaRepository.Instance.Resolve("http://grafcet/1.0#//Step")));
                }
                return _classInstance;
            }
        }
        
        /// <summary>
        /// Gets fired before the IsInitial property changes its value
        /// </summary>
        public event System.EventHandler<ValueChangedEventArgs> IsInitialChanging;
        
        /// <summary>
        /// Gets fired when the IsInitial property changed its value
        /// </summary>
        public event System.EventHandler<ValueChangedEventArgs> IsInitialChanged;
        
        /// <summary>
        /// Gets fired before the IsActive property changes its value
        /// </summary>
        public event System.EventHandler<ValueChangedEventArgs> IsActiveChanging;
        
        /// <summary>
        /// Gets fired when the IsActive property changed its value
        /// </summary>
        public event System.EventHandler<ValueChangedEventArgs> IsActiveChanged;
        
        /// <summary>
        /// Gets fired before the Action property changes its value
        /// </summary>
        public event System.EventHandler<ValueChangedEventArgs> ActionChanging;
        
        /// <summary>
        /// Gets fired when the Action property changed its value
        /// </summary>
        public event System.EventHandler<ValueChangedEventArgs> ActionChanged;
        
        private static ITypedElement RetrieveIsInitialAttribute()
        {
            return ((ITypedElement)(((ModelElement)(MDEMGTT.Grafcet.Step.ClassInstance)).Resolve("isInitial")));
        }
        
        /// <summary>
        /// Raises the IsInitialChanging event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnIsInitialChanging(ValueChangedEventArgs eventArgs)
        {
            System.EventHandler<ValueChangedEventArgs> handler = this.IsInitialChanging;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the IsInitialChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnIsInitialChanged(ValueChangedEventArgs eventArgs)
        {
            System.EventHandler<ValueChangedEventArgs> handler = this.IsInitialChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        private static ITypedElement RetrieveIsActiveAttribute()
        {
            return ((ITypedElement)(((ModelElement)(MDEMGTT.Grafcet.Step.ClassInstance)).Resolve("isActive")));
        }
        
        /// <summary>
        /// Raises the IsActiveChanging event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnIsActiveChanging(ValueChangedEventArgs eventArgs)
        {
            System.EventHandler<ValueChangedEventArgs> handler = this.IsActiveChanging;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the IsActiveChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnIsActiveChanged(ValueChangedEventArgs eventArgs)
        {
            System.EventHandler<ValueChangedEventArgs> handler = this.IsActiveChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        private static ITypedElement RetrieveActionAttribute()
        {
            return ((ITypedElement)(((ModelElement)(MDEMGTT.Grafcet.Step.ClassInstance)).Resolve("action")));
        }
        
        /// <summary>
        /// Raises the ActionChanging event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnActionChanging(ValueChangedEventArgs eventArgs)
        {
            System.EventHandler<ValueChangedEventArgs> handler = this.ActionChanging;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        /// <summary>
        /// Raises the ActionChanged event
        /// </summary>
        /// <param name="eventArgs">The event data</param>
        protected virtual void OnActionChanged(ValueChangedEventArgs eventArgs)
        {
            System.EventHandler<ValueChangedEventArgs> handler = this.ActionChanged;
            if ((handler != null))
            {
                handler.Invoke(this, eventArgs);
            }
        }
        
        private static ITypedElement RetrieveIncomingConnectionsReference()
        {
            return ((ITypedElement)(((ModelElement)(MDEMGTT.Grafcet.Step.ClassInstance)).Resolve("incomingConnections")));
        }
        
        /// <summary>
        /// Forwards CollectionChanging notifications for the IncomingConnections property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void IncomingConnectionsCollectionChanging(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanging("IncomingConnections", e, _incomingConnectionsReference);
        }
        
        /// <summary>
        /// Forwards CollectionChanged notifications for the IncomingConnections property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void IncomingConnectionsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanged("IncomingConnections", e, _incomingConnectionsReference);
        }
        
        private static ITypedElement RetrieveOutgoingConnectionsReference()
        {
            return ((ITypedElement)(((ModelElement)(MDEMGTT.Grafcet.Step.ClassInstance)).Resolve("outgoingConnections")));
        }
        
        /// <summary>
        /// Forwards CollectionChanging notifications for the OutgoingConnections property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void OutgoingConnectionsCollectionChanging(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanging("OutgoingConnections", e, _outgoingConnectionsReference);
        }
        
        /// <summary>
        /// Forwards CollectionChanged notifications for the OutgoingConnections property to the parent model element
        /// </summary>
        /// <param name="sender">The collection that raised the change</param>
        /// <param name="e">The original event data</param>
        private void OutgoingConnectionsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.OnCollectionChanged("OutgoingConnections", e, _outgoingConnectionsReference);
        }
        
        /// <summary>
        /// Resolves the given attribute name
        /// </summary>
        /// <returns>The attribute value or null if it could not be found</returns>
        /// <param name="attribute">The requested attribute name</param>
        /// <param name="index">The index of this attribute</param>
        protected override object GetAttributeValue(string attribute, int index)
        {
            if ((attribute == "ISINITIAL"))
            {
                return this.IsInitial;
            }
            if ((attribute == "ISACTIVE"))
            {
                return this.IsActive;
            }
            if ((attribute == "ACTION"))
            {
                return this.Action;
            }
            return base.GetAttributeValue(attribute, index);
        }
        
        /// <summary>
        /// Gets the Model element collection for the given feature
        /// </summary>
        /// <returns>A non-generic list of elements</returns>
        /// <param name="feature">The requested feature</param>
        protected override System.Collections.IList GetCollectionForFeature(string feature)
        {
            if ((feature == "INCOMINGCONNECTIONS"))
            {
                return this._incomingConnections;
            }
            if ((feature == "OUTGOINGCONNECTIONS"))
            {
                return this._outgoingConnections;
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
            if ((feature == "ISINITIAL"))
            {
                this.IsInitial = ((bool)(value));
                return;
            }
            if ((feature == "ISACTIVE"))
            {
                this.IsActive = ((bool)(value));
                return;
            }
            if ((feature == "ACTION"))
            {
                this.Action = ((string)(value));
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
            if ((attribute == "ISINITIAL"))
            {
                return Observable.Box(new IsInitialProxy(this));
            }
            if ((attribute == "ISACTIVE"))
            {
                return Observable.Box(new IsActiveProxy(this));
            }
            if ((attribute == "ACTION"))
            {
                return new ActionProxy(this);
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
                _classInstance = ((IClass)(MetaRepository.Instance.Resolve("http://grafcet/1.0#//Step")));
            }
            return _classInstance;
        }
        
        /// <summary>
        /// The collection class to to represent the children of the Step class
        /// </summary>
        public class StepReferencedElementsCollection : ReferenceCollection, ICollectionExpression<IModelElement>, ICollection<IModelElement>
        {
            
            private Step _parent;
            
            /// <summary>
            /// Creates a new instance
            /// </summary>
            public StepReferencedElementsCollection(Step parent)
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
                    count = (count + this._parent.IncomingConnections.Count);
                    count = (count + this._parent.OutgoingConnections.Count);
                    return count;
                }
            }
            
            protected override void AttachCore()
            {
                this._parent.IncomingConnections.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
                this._parent.OutgoingConnections.AsNotifiable().CollectionChanged += this.PropagateCollectionChanges;
            }
            
            protected override void DetachCore()
            {
                this._parent.IncomingConnections.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
                this._parent.OutgoingConnections.AsNotifiable().CollectionChanged -= this.PropagateCollectionChanges;
            }
            
            /// <summary>
            /// Adds the given element to the collection
            /// </summary>
            /// <param name="item">The item to add</param>
            public override void Add(IModelElement item)
            {
                ITransitionToStep incomingConnectionsCasted = item.As<ITransitionToStep>();
                if ((incomingConnectionsCasted != null))
                {
                    this._parent.IncomingConnections.Add(incomingConnectionsCasted);
                }
                IStepToTransition outgoingConnectionsCasted = item.As<IStepToTransition>();
                if ((outgoingConnectionsCasted != null))
                {
                    this._parent.OutgoingConnections.Add(outgoingConnectionsCasted);
                }
            }
            
            /// <summary>
            /// Clears the collection and resets all references that implement it.
            /// </summary>
            public override void Clear()
            {
                this._parent.IncomingConnections.Clear();
                this._parent.OutgoingConnections.Clear();
            }
            
            /// <summary>
            /// Gets a value indicating whether the given element is contained in the collection
            /// </summary>
            /// <returns>True, if it is contained, otherwise False</returns>
            /// <param name="item">The item that should be looked out for</param>
            public override bool Contains(IModelElement item)
            {
                if (this._parent.IncomingConnections.Contains(item))
                {
                    return true;
                }
                if (this._parent.OutgoingConnections.Contains(item))
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
                IEnumerator<IModelElement> incomingConnectionsEnumerator = this._parent.IncomingConnections.GetEnumerator();
                try
                {
                    for (
                    ; incomingConnectionsEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = incomingConnectionsEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    incomingConnectionsEnumerator.Dispose();
                }
                IEnumerator<IModelElement> outgoingConnectionsEnumerator = this._parent.OutgoingConnections.GetEnumerator();
                try
                {
                    for (
                    ; outgoingConnectionsEnumerator.MoveNext(); 
                    )
                    {
                        array[arrayIndex] = outgoingConnectionsEnumerator.Current;
                        arrayIndex = (arrayIndex + 1);
                    }
                }
                finally
                {
                    outgoingConnectionsEnumerator.Dispose();
                }
            }
            
            /// <summary>
            /// Removes the given item from the collection
            /// </summary>
            /// <returns>True, if the item was removed, otherwise False</returns>
            /// <param name="item">The item that should be removed</param>
            public override bool Remove(IModelElement item)
            {
                ITransitionToStep transitionToStepItem = item.As<ITransitionToStep>();
                if (((transitionToStepItem != null) 
                            && this._parent.IncomingConnections.Remove(transitionToStepItem)))
                {
                    return true;
                }
                IStepToTransition stepToTransitionItem = item.As<IStepToTransition>();
                if (((stepToTransitionItem != null) 
                            && this._parent.OutgoingConnections.Remove(stepToTransitionItem)))
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
                return Enumerable.Empty<IModelElement>().Concat(this._parent.IncomingConnections).Concat(this._parent.OutgoingConnections).GetEnumerator();
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the isInitial property
        /// </summary>
        private sealed class IsInitialProxy : ModelPropertyChange<IStep, bool>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public IsInitialProxy(IStep modelElement) : 
                    base(modelElement, "isInitial")
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override bool Value
            {
                get
                {
                    return this.ModelElement.IsInitial;
                }
                set
                {
                    this.ModelElement.IsInitial = value;
                }
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the isActive property
        /// </summary>
        private sealed class IsActiveProxy : ModelPropertyChange<IStep, bool>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public IsActiveProxy(IStep modelElement) : 
                    base(modelElement, "isActive")
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override bool Value
            {
                get
                {
                    return this.ModelElement.IsActive;
                }
                set
                {
                    this.ModelElement.IsActive = value;
                }
            }
        }
        
        /// <summary>
        /// Represents a proxy to represent an incremental access to the action property
        /// </summary>
        private sealed class ActionProxy : ModelPropertyChange<IStep, string>
        {
            
            /// <summary>
            /// Creates a new observable property access proxy
            /// </summary>
            /// <param name="modelElement">The model instance element for which to create the property access proxy</param>
            public ActionProxy(IStep modelElement) : 
                    base(modelElement, "action")
            {
            }
            
            /// <summary>
            /// Gets or sets the value of this expression
            /// </summary>
            public override string Value
            {
                get
                {
                    return this.ModelElement.Action;
                }
                set
                {
                    this.ModelElement.Action = value;
                }
            }
        }
    }
}

