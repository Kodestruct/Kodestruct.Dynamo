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


namespace Kodestruct.Aluminum.AA.General
{

    /// <summary>
    ///Material selection   
    /// </summary>

    [NodeName("Material selection ")]
    [NodeCategory("Kodestruct.Aluminum.AA.General")]
    [NodeDescription("Material selection ")]
    [IsDesignScriptCompatible]
    public class MaterialSelection : UiNodeBase
    {

        public MaterialSelection()
        {
            
            //OutPortData.Add(new PortData("ReportEntry", "Calculation log entries (for reporting)"));
            OutPortData.Add(new PortData("AluminumAlloyId", "Aluminum alloy"));OutPortData.Add(new PortData("AluminumTemperId", "Aluminum temper"));OutPortData.Add(new PortData("AluminumProductId", "Aluminum product type"));OutPortData.Add(new PortData("ThicknessRangeId", "Range of aluminum material thicknesses"));
            RegisterAllPorts();
            SetDefaultParameters();
            //PropertyChanged += NodePropertyChanged;
        }

        private void SetDefaultParameters()
        {
            //ReportEntry="";

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

		#region AluminumAlloyIdProperty
		
		/// <summary>
		/// AluminumAlloyId property
		/// </summary>
		/// <value>Aluminum alloy</value>
		public string _AluminumAlloyId;
		
		public string AluminumAlloyId
		{
		    get { return _AluminumAlloyId; }
		    set
		    {
		        _AluminumAlloyId = value;
		        RaisePropertyChanged("AluminumAlloyId");
		        OnNodeModified();
		    }
		}
		#endregion

		#region AluminumTemperIdProperty
		
		/// <summary>
		/// AluminumTemperId property
		/// </summary>
		/// <value>Aluminum temper</value>
		public string _AluminumTemperId;
		
		public string AluminumTemperId
		{
		    get { return _AluminumTemperId; }
		    set
		    {
		        _AluminumTemperId = value;
		        RaisePropertyChanged("AluminumTemperId");
		        OnNodeModified();
		    }
		}
		#endregion

		#region AluminumProductIdProperty
		
		/// <summary>
		/// AluminumProductId property
		/// </summary>
		/// <value>Aluminum product type</value>
		public string _AluminumProductId;
		
		public string AluminumProductId
		{
		    get { return _AluminumProductId; }
		    set
		    {
		        _AluminumProductId = value;
		        RaisePropertyChanged("AluminumProductId");
		        OnNodeModified();
		    }
		}
		#endregion

		#region ThicknessRangeIdProperty
		
		/// <summary>
		/// ThicknessRangeId property
		/// </summary>
		/// <value>Range of aluminum material thicknesses</value>
		public string _ThicknessRangeId;
		
		public string ThicknessRangeId
		{
		    get { return _ThicknessRangeId; }
		    set
		    {
		        _ThicknessRangeId = value;
		        RaisePropertyChanged("ThicknessRangeId");
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
            //nodeElement.SetAttribute("Material", Material);
        }

        /// <summary>
        ///Retrieved property values when opening the node     
        /// </summary>
        protected override void DeserializeCore(XmlElement nodeElement, SaveContext context)
        {
            //base.DeserializeCore(nodeElement, context);
            //var attrib = nodeElement.Attributes["Material"];
            //if (attrib == null)
            //    return;
           
            //MaterialSelection = attrib.Value;
            //SetComponentDescription();

        }



        #endregion



        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class MaterialSelectionViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<MaterialSelection>
        {
            public void CustomizeView(MaterialSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                MaterialSelectionView control = new MaterialSelectionView();
                control.DataContext = model;
                
               
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

        }
    }
}
