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

namespace Kodestruct.General.Flexure
{

    /// <summary>
    ///Bending axis selection  
    /// </summary>

    [NodeName("Bending axis selection")]
    [NodeCategory("Kodestruct.General.Flexure")]
    [NodeDescription("Bending axis selection")]
    [IsDesignScriptCompatible]
    public class BendingAxisSelection : UiNodeBase
    {

        [JsonConstructor]
        public BendingAxisSelection(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
        {

        }
        public BendingAxisSelection()
        {
            ReportEntry="";

            //InPorts.Add(new PortModel(PortType.Input, this, new PortData("Port Name", "Port Description")));
            OutPorts.Add(new PortModel(PortType.Output, this, new PortData("BendingAxis", "Distinguishes between bending with respect to section x-axis vs x-axis")));
            RegisterAllPorts();
            //PropertyChanged += NodePropertyChanged;
            SetDefaultParameters();
        }

        private void SetDefaultParameters()
        {
            ReportEntry = "";
            BendingAxis = "XAxis";
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

		#region BendingAxisProperty
		
		/// <summary>
		/// BendingAxis property
		/// </summary>
		/// <value>Distinguishes between bending with respect to section x-axis vs x-axis</value>
		public string _BendingAxis;
		
		public string BendingAxis
		{
		    get { return _BendingAxis; }
		    set
		    {
		        _BendingAxis = value;
		        RaisePropertyChanged("BendingAxis");
		        OnNodeModified(true); 
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
                OnNodeModified(true); 
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
            nodeElement.SetAttribute("BendingAxis", BendingAxis);
        }

        /// <summary>
        ///Retrieved property values when opening the node     
        /// </summary>
        protected override void DeserializeCore(XmlElement nodeElement, SaveContext context)
        {
            base.DeserializeCore(nodeElement, context);
            var attrib = nodeElement.Attributes["BendingAxis"];
            if (attrib == null)
                return;
           
            BendingAxis = attrib.Value;

        }



        #endregion



        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class BendingAxisSelectionViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<BendingAxisSelection>
        {
            public void CustomizeView(BendingAxisSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                BendingAxisSelectionView control = new BendingAxisSelectionView();
                control.DataContext = model;
                

                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

        }
    }
}
