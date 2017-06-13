#region Copyright
/*Copyright (C) 2015 Konstantin Udilovich

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Dynamo.Controls;
using Dynamo.Models;
using Dynamo.Wpf;
using ProtoCore.AST.AssociativeAST;
using Kodestruct.Common.CalculationLogger;
using Kodestruct.Dynamo.Common;
using Dynamo.Nodes;
using System.Xml;
using Dynamo.Graph;
using Dynamo.Graph.Nodes;
using Kodestruct.General;
using Kodestruct.Dynamo.UI.Views.General;
using KodestructDynamoUI.General;
using KodestructDynamoUI.Nodes.General;
using System.Linq;

namespace Kodestruct.General
{

    /// <summary>
    ///Force type selection  
    /// </summary>

    [NodeName("NodeHelp")]
    [NodeCategory("Kodestruct.General")]
    [NodeDescription("Node Help")]
    [IsDesignScriptCompatible]
    public class NodeHelp : UiNodeBase
    {

        public NodeHelp()
        {

            provider = new NodeListProvider();
            SelectedNodeUri = "https://github.com/Kodestruct/Kodestruct.Dynamo/wiki";
        }
        NodeListProvider provider;

        /// <summary>
        ///     Gets the type of this class, to be used in base class for reflection
        /// </summary>
        protected override Type GetModelType()
        {
            return GetType();
        }


        //NodeData



        #region SelectedNode Property
        private string _SelectedNodeName;
        public string SelectedNodeName
        {
            get { return _SelectedNodeName; }
            set
            {
                _SelectedNodeName = value;
                RaisePropertyChanged("SelectedNodeName");
                UpdateUri(SelectedNodeName);
            }
        }

        #endregion

        private void UpdateUri(string selectedNodeName)
        {
           var uri = provider.NodeItems.Where(nd => nd.FullName == selectedNodeName).Select(d => d.Uri);
            SelectedNodeUri ="https://github.com/Kodestruct/Kodestruct.Dynamo/wiki/Load-nodes";
        }

        #region SelectedNodeUri Property
        private string _SelectedNodeUri;
        public string SelectedNodeUri
        {
            get { return _SelectedNodeUri; }
            set
            {
                _SelectedNodeUri = value;
                RaisePropertyChanged("SelectedNodeUri");
            }
        }

        #endregion

        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class NodeHelpViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<NodeHelp>
        {
            public void CustomizeView(NodeHelp model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                NodeHelpView control = new NodeHelpView();
                control.DataContext = model;
                
                
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

        }
    }
}
