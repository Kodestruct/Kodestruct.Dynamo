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

namespace Kodestruct.Dynamo.Common
{
    [IsDesignScriptCompatible]
    public abstract class UiNodeBase : NodeModel, IUiNodeBase
    {


        #region Commands

        /// <summary>
        ///     DelegateCommand objects allow you to bind
        ///     UI interaction to methods on your data context.
        /// </summary>
        [IsVisibleInDynamoLibrary(false)]
        public DelegateCommand ShowUI { get; set; }

        #endregion

        #region command methods

        private void RenderUI(object obj)
        {
        }

        #endregion

        #region Logics

        //public abstract void Calculate();

        #endregion

        [IsVisibleInDynamoLibrary(false)]
        public void DisposeWindow()
        {
            // unsubscribe from possible events and etc in the Window.
        }

        #region Events

        public event EventHandler RequestOutputChange;

        protected virtual void OnRequestOutputChange(object sender, EventArgs e)
        {
         
            if (RequestOutputChange != null)
            {
                RequestOutputChange(sender, e);
            }
                
            //Calculate();
        }

        #endregion

        #region constructor

        
 
        /// <summary>
        ///     The constructor for a NodeModel is used to create
        ///     the input and output ports and specify the argument
        ///     lacing.
        /// </summary>
        [JsonConstructor]
        protected UiNodeBase(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
        {
            PropertyChanged += PropertyChangedHandler;
            // The arugment lacing is the way in which Dynamo handles
            // inputs of lists. If you don't want your node to
            // support argument lacing, you can set this to LacingStrategy.Disabled.
            ArgumentLacing = LacingStrategy.CrossProduct;
            // We create a DelegateCommand object which will be 
            // bound to our button in our custom UI. 
            ShowUI = new DelegateCommand(RenderUI);
        }
        public UiNodeBase()
        {
            PropertyChanged += PropertyChangedHandler;
            // The arugment lacing is the way in which Dynamo handles
            // inputs of lists. If you don't want your node to
            // support argument lacing, you can set this to LacingStrategy.Disabled.
            ArgumentLacing = LacingStrategy.CrossProduct;
            // We create a DelegateCommand object which will be 
            // bound to our button in our custom UI. 
            ShowUI = new DelegateCommand(RenderUI);
        }


        protected void PropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "IsUpdated")
                return;

            if (InPorts.Any(x => x.Connectors.Count == 0))
                return;
            //Calculate();
            OnRequestOutputChange(this, EventArgs.Empty);

        }

        //protected void NodePropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName != "IsUpdated")
        //        return;

        //    if (InPorts.Any(x => x.Connectors.Count == 0))
        //        return;

        //    OnRequestOutputChange(this, EventArgs.Empty);
        //}

        #endregion

        #region public methods

        /// <summary>
        ///     If this method is not overriden, Dynamo will, by default
        ///     pass data through this node. But we wouldn't be here if
        ///     we just wanted to pass data through the node, so let's
        ///     try using the data.
        /// </summary>
        /// <param name="inputAstNodes"></param>
        /// <returns></returns>
        [IsVisibleInDynamoLibrary(false)]
        public override IEnumerable<AssociativeNode> BuildOutputAst(List<AssociativeNode> inputAstNodes)
        {
            //Calculate();
            // When you create your own UI node you are responsible
            // for generating the abstract syntax tree (AST) nodes which
            // specify what methods are called, or how your data is passed
            // when execution occurs.

            // WARNING!!!
            // Do not throw an exception during AST creation. If you
            // need to convey a failure of this node, then use
            // AstFactory.BuildNullNode to pass out null.

            // Using the AstFactory class, we can build AstNode objects
            // that assign doubles, assign function calls, build expression lists, etc.

            //KU:
            //Search the model for properties matching the names of 
            //Outports:

            var props = GetModelType().GetProperties();
            //var outProps =
            //    props.Where(prop => OutPortData.Any(item => item.NickName == prop.Name)).ToList();
            
            List<PropertyInfo> outProps = new List<PropertyInfo>();
           
            
            foreach (var outP in OutPorts)
            {
                foreach (var gp in props)
                {
                    if (outP.Name==gp.Name)
                    {
                        outProps.Add(gp);
                    }
                }
            }
            
            var outAstNodes = new List<AssociativeNode>();

            for (var i = 0; i < outProps.Count; i++)
            {
                var nd = outProps[i];

                if (nd.PropertyType == typeof (Double))
                {
                    var propVal = (double) nd.GetValue(this, null);
                    outAstNodes.Add(AstFactory.BuildAssignment(
                        GetAstIdentifierForOutputIndex(i),
                        AstFactory.BuildDoubleNode(propVal)));
                }
                else if (nd.PropertyType == typeof (String))
                {
                    var propVal = (string) nd.GetValue(this, null);
                    outAstNodes.Add(AstFactory.BuildAssignment(
                        GetAstIdentifierForOutputIndex(i),
                        AstFactory.BuildStringNode(propVal)));
                }
                else if (nd.PropertyType == typeof (bool))
                {
                    var propVal = (bool) nd.GetValue(this, null);
                    outAstNodes.Add(AstFactory.BuildAssignment(
                        GetAstIdentifierForOutputIndex(i),
                        AstFactory.BuildBooleanNode(propVal)));
                }
                else if (nd.PropertyType == typeof(CalcLog))
                {
                    var propVal = nd.GetValue(this);
                    if (propVal != null)
                    {
                        // create loggin function
                        //CreateLogginFunction();

                        //if (LoggingFunction != null)
                        //{
                        //    // by invoking function we should receive AssociativeNode that corresponds to Log data
                        //    var identifierNode = GetAstIdentifierForOutputIndex(0);

                        //    // invoke function
                        //    var functionValue = LoggingFunction.Invoke(inputAstNodes, identifierNode);

                        //    // add result to outAstNodes
                        //    outAstNodes.Add(functionValue);
                        //}

                        //var functionCall =
                        // AstFactory.BuildFunctionCall(
                        //     new Func<Color, Color, double, Color>(Color.BuildColorFromRange),
                        //     inputAstNodes);

                        //outAstNodes.Add(AstFactory.BuildAssignment(GetAstIdentifierForOutputIndex(0), functionCall));

                    }
                }
                else
                {
                    outAstNodes.Add(AstFactory.BuildAssignment(
                        GetAstIdentifierForOutputIndex(i),
                        AstFactory.BuildNullNode()));
                }
            }
            //Calculate();
            return outAstNodes;
        }



        protected abstract Type GetModelType();

        //You need to implement this method, because statuc methods for Func<> are not inhereited.
        //protected abstract AssociativeNode GetCalcLogAssociativeNode(List<AssociativeNode> inputAstNodes);

        //protected abstract Func<List<AssociativeNode>, IdentifierNode, AssociativeNode> LoggingFunction { get; set; }

        //protected abstract void CreateLogginFunction();

        #endregion
    }
}
