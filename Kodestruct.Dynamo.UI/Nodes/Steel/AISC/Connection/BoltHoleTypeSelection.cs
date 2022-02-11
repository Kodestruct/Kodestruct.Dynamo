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
using System.Xml;
using Dynamo.Nodes;
using Dynamo.Graph;
using Dynamo.Graph.Nodes;
using Newtonsoft.Json;

namespace Kodestruct.Steel.AISC.Connection
{

    /// <summary>
    ///Bolt hole type  
    /// </summary>

    [NodeName("Bolt hole type selection")]
    [NodeCategory("Kodestruct.Steel.AISC.Connection.Bolted")]
    [NodeDescription("Bolt hole type")]
    [IsDesignScriptCompatible]
    public class BoltHoleTypeSelection : UiNodeBase
    {
        [JsonConstructor]
        public BoltHoleTypeSelection(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
        {

        }
        public BoltHoleTypeSelection()
        {

            //OutPorts.Add(new PortData("ReportEntry", "Calculation log entries (for reporting)"));
            OutPorts.Add(new PortModel(PortType.Output, this, new PortData("BoltHoleType", "Type of bolt hole")));
            RegisterAllPorts();
            //PropertyChanged += NodePropertyChanged;
            SetDefaultParameters();

        }

        private void SetDefaultParameters()
        {

            BoltHoleType = "STD";
        }



        /// <summary>
        ///     Gets the type of this class, to be used in base class for reflection
        /// </summary>
        protected override Type GetModelType()
        {
            return GetType();
        }

        #region properties

        #region InputProperties



	    #endregion

        #region OutputProperties

		#region BoltHoleTypeProperty
		
		/// <summary>
		/// BoltHoleType property
		/// </summary>
		/// <value>Type of bolt hole</value>
		public string _BoltHoleType;
		
		public string BoltHoleType
		{
		    get { return _BoltHoleType; }
		    set
		    {
		        _BoltHoleType = value;
		        RaisePropertyChanged("BoltHoleType");
		        OnNodeModified(true); 
		    }
		}
		#endregion



        #endregion
        #endregion



        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class BoltHoleTypeSelectionViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<BoltHoleTypeSelection>
        {
            public void CustomizeView(BoltHoleTypeSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                BoltHoleTypeView control = new BoltHoleTypeView();
                control.DataContext = model;
                
                
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

        }
    }
}
