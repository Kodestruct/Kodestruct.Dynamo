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
using Kodestruct.Concrete.ACI318_14.General.Material;
using Newtonsoft.Json;

namespace Kodestruct.Concrete.ACI318.General.Concrete.Weight
{

    /// <summary>
    ///Lightweight concrete type selection  
    /// </summary>

    [NodeName("Lightweight concrete type selection")]
    [NodeCategory("Kodestruct.Concrete.ACI318.General.Concrete.Weight")]
    [NodeDescription("Lightweight concrete type selection")]
    [IsDesignScriptCompatible]
    public class LightweightConcreteTypeSelection : UiNodeBase
    {

        [JsonConstructor]
        public LightweightConcreteTypeSelection(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
        {

        }
        public LightweightConcreteTypeSelection()
        {

            //InPorts.Add(new PortModel(PortType.Input, this, new PortData("Port Name", "Port Description")));
            OutPorts.Add(new PortModel(PortType.Output, this, new PortData("LightweightConcreteType", "Type of lightweight concrete material")));
            RegisterAllPorts();
            SetDefaultParameters();
            //PropertyChanged += NodePropertyChanged;
        }

        private void SetDefaultParameters()
        {
            //ReportEntry="";
            LightweightConcreteType = "AllLightweightConcrete";
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

		#region LightweightConcreteTypeProperty
		
		/// <summary>
		/// LightweightConcreteType property
		/// </summary>
		/// <value>Type of lightweight concrete material</value>
		public string _LightweightConcreteType;
		
		public string LightweightConcreteType
		{
		    get { return _LightweightConcreteType; }
		    set
		    {
		        _LightweightConcreteType = value;
		        RaisePropertyChanged("LightweightConcreteType");
		        OnNodeModified();
		    }
		}
		#endregion


        #endregion
        #endregion


        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class LightweightConcreteTypeSelectionViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<LightweightConcreteTypeSelection>
        {
            public void CustomizeView(LightweightConcreteTypeSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                LightweightConcreteTypeSelectionView control = new LightweightConcreteTypeSelectionView();
                control.DataContext = model;
                
               
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

        }
    }
}
