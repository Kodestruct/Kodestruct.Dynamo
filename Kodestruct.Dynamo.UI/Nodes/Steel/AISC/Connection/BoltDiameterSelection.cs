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


namespace Kodestruct.Steel.AISC.Connection
{

    /// <summary>
    ///Selection of bolt diameter  
    /// </summary>

    [NodeName("Bolt diameter selection")]
    [NodeCategory("Kodestruct.Steel.AISC.Connection.Bolted")]
    [NodeDescription("Selection of bolt diameter")]
    [IsDesignScriptCompatible]
    public class BoltDiameterSelection : UiNodeBase
    {

        public BoltDiameterSelection()
        {
            
            InPortData.Add(new PortData("BoltMaterialId", "Bolt material specification"));
            //OutPortData.Add(new PortData("ReportEntry", "Calculation log entries (for reporting)"));
            OutPortData.Add(new PortData("d_b", "Nominal fastener diameter"));
            RegisterAllPorts();
            SetDefaultParameters();
            //PropertyChanged += NodePropertyChanged;
        }

        private void SetDefaultParameters()
        {
            ReportEntry = "";
            d_b = 0.75;
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

		#region d_bProperty
		
		/// <summary>
		/// d_b property
		/// </summary>
		/// <value>Nominal fastener diameter</value>
		public double _d_b;
		
		public double d_b
		{
		    get { return _d_b; }
		    set
		    {
		        _d_b = value;
		        RaisePropertyChanged("d_b");
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
            nodeElement.SetAttribute("d_b", d_b.ToString());
        }

        /// <summary>
        ///Retrieved property values when opening the node     
        /// </summary>
        protected override void DeserializeCore(XmlElement nodeElement, SaveContext context)
        {
            base.DeserializeCore(nodeElement, context);
            var attrib = nodeElement.Attributes["d_b"];
            if (attrib == null)
                return;
           
            d_b = double.Parse(attrib.Value);

        }

        #endregion



        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class BoltDiameterSelectionViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<BoltDiameterSelection>
        {
            public void CustomizeView(BoltDiameterSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                BoltDiameterSelectionView control = new BoltDiameterSelectionView();
                control.DataContext = model;
                
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }


        }
    }
}
