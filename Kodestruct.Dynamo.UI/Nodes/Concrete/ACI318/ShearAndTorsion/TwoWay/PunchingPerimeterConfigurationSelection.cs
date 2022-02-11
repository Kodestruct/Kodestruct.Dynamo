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
using Newtonsoft.Json;

namespace Kodestruct.Concrete.ACI318.Section.ShearAndTorsion.TwoWayShear.General
{

    /// <summary>
    ///Punching shear perimeter configuration type selection  
    /// </summary>

    [NodeName("Punching shear perimeter configuration type selection")]
    [NodeCategory("Kodestruct.Concrete.ACI318.Section.ShearAndTorsion.TwoWayShear.General")]
    [NodeDescription("Punching shear perimeter configuration type selection")]
    [IsDesignScriptCompatible]
    public class PunchingPerimeterConfigurationSelection : UiNodeBase
    {

        [JsonConstructor]
        public PunchingPerimeterConfigurationSelection(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
        {

        }
        public PunchingPerimeterConfigurationSelection()
        {

            //InPorts.Add(new PortModel(PortType.Input, this, new PortData("Port Name", "Port Description")));
            OutPorts.Add(new PortModel(PortType.Output, this, new PortData("PunchingPerimeterConfiguration", "Type of punching perimeter (interior, edge, corner etc)")));
            RegisterAllPorts();
            SetDefaultParameters();
            //PropertyChanged += NodePropertyChanged;
        }

        private void SetDefaultParameters()
        {
            //ReportEntry="";
            PunchingPerimeterConfiguration = "Interior";
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

		#region PunchingPerimeterConfigurationProperty
		
		/// <summary>
		/// PunchingPerimeterConfiguration property
		/// </summary>
		/// <value>Type of punching perimeter (interior, edge, corner etc)</value>
		public string _PunchingPerimeterConfiguration;
		
		public string PunchingPerimeterConfiguration
		{
		    get { return _PunchingPerimeterConfiguration; }
		    set
		    {
		        _PunchingPerimeterConfiguration = value;
		        RaisePropertyChanged("PunchingPerimeterConfiguration");
		        OnNodeModified();
		    }
		}
		#endregion


        #endregion
        #endregion





        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class PunchingPerimeterConfigurationSelectionViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<PunchingPerimeterConfigurationSelection>
        {
            public void CustomizeView(PunchingPerimeterConfigurationSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                PunchingPerimeterConfigurationSelectionView control = new PunchingPerimeterConfigurationSelectionView();
                control.DataContext = model;
                
               
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

        }
    }
}
