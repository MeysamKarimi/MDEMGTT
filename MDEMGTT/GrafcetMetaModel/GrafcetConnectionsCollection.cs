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
    
    
    public class GrafcetConnectionsCollection : ObservableOppositeOrderedSet<IGrafcet, IConnection>
    {
        
        public GrafcetConnectionsCollection(IGrafcet parent) : 
                base(parent)
        {
        }
        
        private void OnItemParentChanged(object sender, ValueChangedEventArgs e)
        {
            if ((e.NewValue != this.Parent))
            {
                this.Remove(((IConnection)(sender)));
            }
        }
        
        protected override void SetOpposite(IConnection item, IGrafcet parent)
        {
            if ((parent != null))
            {
                item.ParentChanged += this.OnItemParentChanged;
                item.Grafcet = parent;
            }
            else
            {
                item.ParentChanged -= this.OnItemParentChanged;
                if ((item.Grafcet == this.Parent))
                {
                    item.Grafcet = parent;
                }
            }
        }
    }
}

