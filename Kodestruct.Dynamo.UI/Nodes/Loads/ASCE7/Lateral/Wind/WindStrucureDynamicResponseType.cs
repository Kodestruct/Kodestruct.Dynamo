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
using Newtonsoft.Json;

namespace Kodestruct.Loads.ASCE7.Lateral.Wind.StructureParameters
{

    /// <summary>
    ///Selection of the type of dynamic response of the structure (flexible or rigid)   
    /// </summary>

    [NodeName("Wind structure dynamic response type")]
    [NodeCategory("Kodestruct.Loads.ASCE7.Lateral.Wind.StructureParameters")]
    [NodeDescription("Selection of the type of dynamic response of the structure (flexible or rigid) ")]
    [IsDesignScriptCompatible]
    public class WindStructureDynamicResponseTypeSelection : UiNodeBase
    {
        [JsonConstructor]
        public WindStructureDynamicResponseTypeSelection(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
        {

        }
        public WindStructureDynamicResponseTypeSelection()
        {

            //InPorts.Add(new PortModel(PortType.Input, this, new PortData("Port Name", "Port Description")));
            OutPorts.Add(new PortModel(PortType.Output, this, new PortData("WindStructureDynamicResponseType", "Type of wind dynamic response (flexible or rigid)")));
            RegisterAllPorts();
            SetDefaultParameters();
            //PropertyChanged += NodePropertyChanged;
        }

        private void SetDefaultParameters()
        {
            //ReportEntry="";
            this.WindStructureDynamicResponseType = "Flexible";
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

		#region WindStructureDynamicResponseTypeProperty
		
		/// <summary>
		/// WindStructureDynamicResponseType property
		/// </summary>
		/// <value>Type of wind dynamic response (flexible or rigid)</value>
		public string _WindStructureDynamicResponseType;
		
		public string WindStructureDynamicResponseType
		{
		    get { return _WindStructureDynamicResponseType; }
		    set
		    {
		        _WindStructureDynamicResponseType = value;
		        RaisePropertyChanged("WindStructureDynamicResponseType");
		        OnNodeModified();
		    }
		}
		#endregion



        #endregion
        #endregion




        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class WindStrucureDynamicResponseTypeViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<WindStructureDynamicResponseTypeSelection>
        {
            public void CustomizeView(WindStructureDynamicResponseTypeSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                WindStrucureDynamicResponseTypeView control = new WindStrucureDynamicResponseTypeView();
                control.DataContext = model;
                
                
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

        }
    }
}
