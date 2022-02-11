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
using Dynamo.Graph;
using System.Xml;
using Newtonsoft.Json;

namespace Kodestruct.Steel.AISC.FloorVibrations.EffectiveProperties
{

    /// <summary>
    ///Joist to girder connection type selection  
    /// </summary>

    [NodeName("Joist to girder connection type selection")]
    [NodeCategory("Kodestruct.Steel.AISC.FloorVibrations.EffectiveProperties")]
    [NodeDescription("Joist to girder connection type selection")]
    [IsDesignScriptCompatible]
    public class JoistToGirderConnectionTypeSelection : UiNodeBase
    {

        [JsonConstructor]
        public JoistToGirderConnectionTypeSelection(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
        {

        }
        public JoistToGirderConnectionTypeSelection()
        {

            //InPorts.Add(new PortModel(PortType.Input, this, new PortData("Port Name", "Port Description")));
            OutPorts.Add(new PortModel(PortType.Output, this, new PortData("JoistToGirderConnectionType", "Differentiates between beams having connection to girder flange versus connection to girder web")));
            RegisterAllPorts();
            SetDefaultParameters();
            //PropertyChanged += NodePropertyChanged;
        }

        private void SetDefaultParameters()
        {
            //ReportEntry="";
            JoistToGirderConnectionType="ConnectionToWeb";
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

		#region JoistToGirderConnectionTypeProperty
		
		/// <summary>
		/// JoistToGirderConnectionType property
		/// </summary>
		/// <value>Differentiates between beams having connection to girder flange versus connection to girder web</value>
		public string _JoistToGirderConnectionType;
		
		public string JoistToGirderConnectionType
		{
		    get { return _JoistToGirderConnectionType; }
		    set
		    {
		        _JoistToGirderConnectionType = value;
		        RaisePropertyChanged("JoistToGirderConnectionType");
		        OnNodeModified();
		    }
		}
		#endregion


        #endregion
        #endregion



        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class JoistToGirderConnectionTypeSelectionViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<JoistToGirderConnectionTypeSelection>
        {
            public void CustomizeView(JoistToGirderConnectionTypeSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                JoistToGirderConnectionTypeSelectionView control = new JoistToGirderConnectionTypeSelectionView();
                control.DataContext = model;
                
                
                
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

        }
    }
}
