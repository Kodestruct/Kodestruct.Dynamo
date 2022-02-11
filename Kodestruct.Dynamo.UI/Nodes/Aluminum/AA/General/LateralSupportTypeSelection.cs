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

namespace Kodestruct.Aluminum.AA.General
{

    /// <summary>
    ///Lateral support type selection  
    /// </summary>

    [NodeName("Lateral support type selection")]
    [NodeCategory("Kodestruct.Aluminum.AA.General")]
    [NodeDescription("Lateral support type selection")]
    [IsDesignScriptCompatible]
    public class LateralSupportTypeSelection : UiNodeBase
    {

        [JsonConstructor]
        public LateralSupportTypeSelection(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
        {

        }
        public LateralSupportTypeSelection()
        {

            //InPorts.Add(new PortModel(PortType.Input, this, new PortData("Port Name", "Port Description")));
            OutPorts.Add(new PortModel(PortType.Output, this, new PortData("LateralSupportType", "Type of support for section local and lateral-torsional buckling")));
            RegisterAllPorts();
            SetDefaultParameters();
            //PropertyChanged += NodePropertyChanged;
        }

        private void SetDefaultParameters()
        {
            //ReportEntry="";
            LateralSupportType = "OneEdge";
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

		#region LateralSupportTypeProperty
		
		/// <summary>
		/// LateralSupportType property
		/// </summary>
		/// <value>Type of support for section local and lateral-torsional buckling</value>
		public string _LateralSupportType;
		
		public string LateralSupportType
		{
		    get { return _LateralSupportType; }
		    set
		    {
		        _LateralSupportType = value;
		        RaisePropertyChanged("LateralSupportType");
		        OnNodeModified();
		    }
		}
		#endregion



        #region ReportEntryProperty

        /// <summary>
        /// log property
        /// </summary>
        /// <value>Calculation entries that can be converted into a report.</value>

        public string reportEntry;

        public string ReportEntry
        {
            get { return reportEntry; }
            set
            {
                reportEntry = value;
                RaisePropertyChanged("ReportEntry");
                OnNodeModified();
            }
        }




        #endregion

        #endregion
        #endregion

        #region Serialization

        /// <summary>
        ///Saves property values to be retained when opening the node     
        /// </summary>
        protected override void SerializeCore(XmlElement nodeElement, SaveContext context)
        {
            base.SerializeCore(nodeElement, context);
            nodeElement.SetAttribute("LateralSupportType", LateralSupportType);
        }

        /// <summary>
        ///Retrieved property values when opening the node     
        /// </summary>
        protected override void DeserializeCore(XmlElement nodeElement, SaveContext context)
        {
            base.DeserializeCore(nodeElement, context);
            var attrib = nodeElement.Attributes["LateralSupportType"];
            if (attrib == null)
                return;
           
           // LateralSupportTypeSelection = attrib.Value;
            //SetComponentDescription();

        }





        #endregion




        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class LateralSupportTypeSelectionViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<LateralSupportTypeSelection>
        {
            public void CustomizeView(LateralSupportTypeSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                LateralSupportTypeSelectionView control = new LateralSupportTypeSelectionView();
                control.DataContext = model;
                
                
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

        }
    }
}
