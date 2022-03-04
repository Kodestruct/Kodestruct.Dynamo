using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Autodesk.DesignScript.Runtime;
using Dynamo.Models;
using Dynamo.UI.Commands;
using ProtoCore.AST.AssociativeAST;
using Kodestruct.Common.CalculationLogger;
using Dynamo.Nodes;
using Dynamo.Graph.Nodes;
using Newtonsoft.Json;
using System.Windows.Controls;
using System.Windows;
using Kodestruct.Dynamo.Common;

namespace KodestructDynamoUI.Common.BaseClasses
{
    public abstract class UiNodeTreeBase: UiNodeBase
    {

        [JsonConstructor]
        protected UiNodeTreeBase(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
        {

        }

        public UiNodeTreeBase()
        {

        }
        public void UpdateSelectionEvents()
        {
            if (TreeViewControl != null)
            {
                TreeViewControl.SelectedItemChanged += OnTreeViewSelectionChanged;
            }
        }
        private void OnTreeViewSelectionChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            OnSelectedItemChanged(e.NewValue);
        }

        protected abstract void OnSelectedItemChanged(object newValue);

        [JsonIgnore]
        public TreeView TreeViewControl { get; set; }
    }
}
