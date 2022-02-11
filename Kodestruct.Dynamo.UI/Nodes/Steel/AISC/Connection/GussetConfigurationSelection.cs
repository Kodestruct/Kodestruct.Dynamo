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

namespace Kodestruct.Steel.AISC.Connection
{

    /// <summary>
    ///Gusset plate configuration selection  
    /// </summary>

    [NodeName("Gusset plate configuration selection")]
    [NodeCategory("Kodestruct.Steel.AISC.Connection.AffectedElements")]
    [NodeDescription("Gusset plate configuration selection")]
    [IsDesignScriptCompatible]
    public class GussetConfigurationSelection : UiNodeBase
    {
        [JsonConstructor]
        public GussetConfigurationSelection(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
        {

        }
        public GussetConfigurationSelection()
        {

            GussetPlateConfigurationId = "ExtendedCorner";
            //OutPorts.Add(new PortData("ReportEntry", "Calculation log entries (for reporting)"));
            OutPorts.Add(new PortModel(PortType.Output, this, new PortData("GussetPlateConfigurationId", "Type of gusset plate configuration for calculation of effective length")));
            RegisterAllPorts();
            //PropertyChanged += NodePropertyChanged;
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

		#region GussetPlateConfigurationIdProperty
		
		/// <summary>
		/// GussetPlateConfigurationId property
		/// </summary>
		/// <value>Type of gusset plate configuration for calculation of effective length</value>
		public string _GussetPlateConfigurationId;
		
		public string GussetPlateConfigurationId
		{
		    get { return _GussetPlateConfigurationId; }
		    set
		    {
		        _GussetPlateConfigurationId = value;
		        RaisePropertyChanged("GussetPlateConfigurationId");
		        OnNodeModified(true); 
		    }
		}
		#endregion


        #endregion
        #endregion



        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class GussetConfigurationSelectionViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<GussetConfigurationSelection>
        {
            public void CustomizeView(GussetConfigurationSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                GussetConfigurationView control = new GussetConfigurationView();
                control.DataContext = model;
                
                
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }


        }
    }
}
