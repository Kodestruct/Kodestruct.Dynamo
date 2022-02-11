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
using Kodestruct.Loads.ASCE7.Entities;
using System.Xml;
using Dynamo.Nodes;
using Dynamo.Graph;
using Dynamo.Graph.Nodes;
using Newtonsoft.Json;

namespace Kodestruct.Steel.AISC.Connection
{

    /// <summary>
    ///Weld type selection  
    /// </summary>

    [NodeName("Weld type selection")]
    [NodeCategory("Kodestruct.Steel.AISC.Connection.Welded")]
    [NodeDescription("Weld type selection")]
    [IsDesignScriptCompatible]
    public class WeldTypeSelection : UiNodeBase
    {
        [JsonConstructor]
        public WeldTypeSelection(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
        {

        }
        public WeldTypeSelection()
        {
            
            //OutPorts.Add(new PortData("ReportEntry", "Calculation log entries (for reporting)"));
            OutPorts.Add(new PortModel(PortType.Output, this, new PortData("WeldType", "Weld type")));
            RegisterAllPorts();
            //PropertyChanged += NodePropertyChanged;
            SetDefaultParameters();

        }

        private void SetDefaultParameters()
        {

            WeldType = "Fillet";
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

		#region WeldTypeProperty
		
		/// <summary>
		/// WeldType property
		/// </summary>
		/// <value>Weld type</value>
		public string _WeldType;
		
		public string WeldType
		{
		    get { return _WeldType; }
		    set
		    {
		        _WeldType = value;
		        RaisePropertyChanged("WeldType");
		        OnNodeModified(true); 
		    }
		}
		#endregion



        #endregion
        #endregion


        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class WeldTypeSelectionViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<WeldTypeSelection>
        {
            public void CustomizeView(WeldTypeSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                WeldTypeView control = new WeldTypeView();
                control.DataContext = model;
                
                 
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

        }
    }
}
