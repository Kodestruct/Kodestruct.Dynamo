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
using Dynamo.Graph.Nodes;
using System.Xml;
using Dynamo.Graph;
using System.Windows;
using Newtonsoft.Json;

namespace Kodestruct.Concrete.ACI318.Details.General
{

    /// <summary>
    ///Rebar coating type  
    /// </summary>

    [NodeName("Rebar coating type")]
    [NodeCategory("Kodestruct.Concrete.ACI318.Details.General")]
    [NodeDescription("Rebar coating type")]
    [IsDesignScriptCompatible]
    public class CoatingTypeSelection : UiNodeBase
    {

        [JsonConstructor]
        public CoatingTypeSelection(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
        {

        }
        public CoatingTypeSelection()
        {

            //InPorts.Add(new PortModel(PortType.Input, this, new PortData("Port Name", "Port Description")));
            OutPorts.Add(new PortModel(PortType.Output, this, new PortData("RebarCoatingType", "Type of rebar surface coating (epoxy coated or black)")));
            RegisterAllPorts();
            SetDefaultParameters();
            //PropertyChanged += NodePropertyChanged;
        }

        private void SetDefaultParameters()
        {
            //ReportEntry="";
            RebarCoatingType = "Uncoated";
        }


        /// <summary>
        ///     Gets the type of this class, to be used in base class for reflection
        /// </summary>
        protected override Type GetModelType()
        {
            return GetType();
        }

        #region Properties

        #region InputProperties



	    #endregion

        #region OutputProperties

		#region RebarCoatingTypeProperty
		
		/// <summary>
		/// RebarCoatingType property
		/// </summary>
		/// <value>Type of rebar surface coating (epoxy coated or black)</value>
		public string _RebarCoatingType;
		
		public string RebarCoatingType
		{
		    get { return _RebarCoatingType; }
		    set
		    {
		        _RebarCoatingType = value;
		        RaisePropertyChanged("RebarCoatingType");
		        OnNodeModified();
		    }
		}
		#endregion



        #endregion
        #endregion


        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class CoatingTypeSelectionViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<CoatingTypeSelection>
        {
            public void CustomizeView(CoatingTypeSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                CoatingTypeSelectionView control = new CoatingTypeSelectionView();
                control.DataContext = model;
                
               
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

        }
    }
}
