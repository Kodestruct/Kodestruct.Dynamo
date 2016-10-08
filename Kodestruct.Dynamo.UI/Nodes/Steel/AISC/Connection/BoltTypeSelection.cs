#region Copyright
   /*Copyright (C) 2015 Kodestruct Inc

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
using System.Windows;
using Dynamo.Nodes;
using Dynamo.Graph;
using Dynamo.Graph.Nodes;


namespace Kodestruct.Steel.AISC10.Connection
{

    /// <summary>
    ///Bolt type (bearing vs slip-critical)  
    /// </summary>

    [NodeName("Bolt type selection")]
    [NodeCategory("Kodestruct.Steel.AISC10.Connection.Bolted")]
    [NodeDescription("Bolt type (bearing vs slip-critical)")]
    [IsDesignScriptCompatible]
    public class BoltTypeSelection : UiNodeBase
    {

        public BoltTypeSelection()
        {
            //OutPortData.Add(new PortData("ReportEntry", "Calculation log entries (for reporting)"));
            OutPortData.Add(new PortData("BoltType", "Distinguishes between bearing and slip-critical bolts"));
            RegisterAllPorts();
            SetDefaultParameters();
            //PropertyChanged += NodePropertyChanged;
        }

        private void SetDefaultParameters()
        {
            ReportEntry = "";
            BoltType = "Bearing";
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

		#region BoltTypeProperty
		
		/// <summary>
		/// BoltType property
		/// </summary>
		/// <value>Distinguishes between bearing and slip-critical bolts</value>
		public string _BoltType;
		
		public string BoltType
		{
		    get { return _BoltType; }
		    set
		    {
		        _BoltType = value;
		        RaisePropertyChanged("BoltType");
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
            nodeElement.SetAttribute("BoltType", BoltType);
        }

        /// <summary>
        ///Retrieved property values when opening the node     
        /// </summary>
        protected override void DeserializeCore(XmlElement nodeElement, SaveContext context)
        {
            base.DeserializeCore(nodeElement, context);
            var attrib = nodeElement.Attributes["BoltType"];
            if (attrib == null)
                return;
           
            BoltType = attrib.Value;

        }


        #endregion



        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class BoltTypeSelectionViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<BoltTypeSelection>
        {
            public void CustomizeView(BoltTypeSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                BoltTypeView control = new BoltTypeView();
                control.DataContext = model;
                
                
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

        }
    }
}
