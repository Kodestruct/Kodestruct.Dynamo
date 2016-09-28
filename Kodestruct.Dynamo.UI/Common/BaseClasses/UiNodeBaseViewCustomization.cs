#region Copyright
   /*Copyright (C) 2015 Kodestruct Inc

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
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using Dynamo.Controls;
using Autodesk.DesignScript.Runtime;

namespace Kodestruct.Dynamo.Common
{
    /// <summary>
    ///     View customizer for HelloDynamo Node Model.
    /// </summary>
    public abstract class UiNodeBaseViewCustomization 
    {
        public IUiNodeBase viewModel { get; set; }

        /// <summary>
        /// At run-time, this method is called during the node 
        /// creation. Here you can create custom UI elements and
        /// add them to the node view, but we recommend designing
        /// your UI declaratively using xaml, and binding it to
        /// properties on this node as the DataContext.
        /// </summary>
        /// <param name="model">The NodeModel representing the node's core logic.</param>
        /// <param name="nodeView">The NodeView representing the node in the graph.</param>
       //[IsVisibleInDynamoLibrary(false)]
        public void CustomizeView(IUiNodeBase model, NodeView nodeView)
        {

            //// The view variable is a reference to the node's view.
            //// In the middle of the node is a grid called the InputGrid.
            //// We reccommend putting your custom UI in this grid, as it has
            //// been designed for this purpose.

            //// Create an instance of our custom UI class (defined in xaml),
            //// and put it into the input grid.
           

            //var dm = nodeView.ViewModel.DynamoViewModel.Model;

            //model.RequestOutputChange += delegate
            //{
            //    model.DispatchOnUIThread(delegate
            //    {
            //        if (model.InPorts.Any(x => x.Connectors.Count == 0)) return;
            //        for (int i = 0; i < model.InPorts.Count(); i++)
            //        {
            //            var thisNode = model.InPorts[i].Connectors[0].Start.Owner;
            //            var thisNodeIndex = model.InPorts[i].Connectors[0].Start.Index;
            //            var thisNodeId = thisNode.GetAstIdentifierForOutputIndex(thisNodeIndex).Name;
            //            var thisElementMirror = dm.EngineController.GetMirror(thisNodeId);
                        
            //            object objectVal = null;

            //            if (thisElementMirror == null)
            //            {
            //                objectVal = 0;
            //            }
            //            else
            //            {
            //                if (thisElementMirror.GetData().IsCollection)
            //                {
            //                    objectVal = thisElementMirror.GetData().GetElements().
            //                        Select(x => x.Data).FirstOrDefault();
            //                }
            //                else
            //                {
            //                    objectVal = thisElementMirror.GetData().Data;
            //                }
            //            }
            //            if (objectVal!=null)
            //            {
            //                var props = model.GetType().GetProperties();
            //                var i1 = i;
            //                var outProp = props.FirstOrDefault(p => p.Name == model.InPorts[i1].PortName);
            //                if (outProp != null && outProp.PropertyType == typeof(Double))
            //                {
            //                    outProp.SetValue(model, Double.Parse(objectVal.ToString()));
            //                    model. OnNodeModified(false);
            //                }
            //                else if (outProp != null && outProp.PropertyType == typeof(String))
            //                {
            //                    outProp.SetValue(model, objectVal.ToString());
            //                    model.OnNodeModified(false);
            //                }
            //                else if (outProp != null && outProp.PropertyType == typeof(bool))
            //                {
            //                    outProp.SetValue(model, Boolean.Parse(objectVal.ToString()));
            //                    model.OnNodeModified(false);
            //                }
                            
            //            }
            //        }

            //    });
            //};
           
        }

        /// <summary>
        /// Here you can do any cleanup you require if you've assigned callbacks for particular 
        /// UI events on your node.
        /// </summary>
        public void Dispose() 
        {
            if (viewModel != null)
            viewModel.DisposeWindow();
        }

    }
}
