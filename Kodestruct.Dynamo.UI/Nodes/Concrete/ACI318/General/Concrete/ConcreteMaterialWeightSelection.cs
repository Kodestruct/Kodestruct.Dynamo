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
using Kodestruct.Concrete.ACI318_14.General.Material;


namespace Kodestruct.Concrete.ACI318.General.Concrete.Weight
{

    /// <summary>
    ///Concrete material weight selection  
    /// </summary>

    [NodeName("Concrete material weight selection")]
    [NodeCategory("Kodestruct.Concrete.ACI318.General.Concrete.Weight")]
    [NodeDescription("Concrete material weight selection")]
    [IsDesignScriptCompatible]
    public class ConcreteMaterialWeightSelection : UiNodeBase
    {

        public ConcreteMaterialWeightSelection()
        {
            
            //OutPortData.Add(new PortData("ReportEntry", "Calculation log entries (for reporting)"));
            OutPortData.Add(new PortData("ConcreteMaterialWeightType", "Type of concrete by weight (normalweight vs. lightweight)"));
            RegisterAllPorts();
            SetDefaultParameters();
            //PropertyChanged += NodePropertyChanged;
        }

        private void SetDefaultParameters()
        {
            //ReportEntry="";
            ConcreteMaterialWeightType = "Normalweight";
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

		#region ConcreteMaterialWeightTypeProperty
		
		/// <summary>
		/// ConcreteMaterialWeightType property
		/// </summary>
		/// <value>Type of concrete by weight (normalweight vs. lightweight)</value>
		public string _ConcreteMaterialWeightType;
		
		public string ConcreteMaterialWeightType
		{
		    get { return _ConcreteMaterialWeightType; }
		    set
		    {
		        _ConcreteMaterialWeightType = value;
		        RaisePropertyChanged("ConcreteMaterialWeightType");
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
            nodeElement.SetAttribute("ConcreteMaterialWeightType", ConcreteMaterialWeightType);
        }

        /// <summary>
        ///Retrieved property values when opening the node     
        /// </summary>
        protected override void DeserializeCore(XmlElement nodeElement, SaveContext context)
        {
            base.DeserializeCore(nodeElement, context);
            var attrib = nodeElement.Attributes["ConcreteMaterialWeightType"];
            if (attrib == null)
                return;

            ConcreteMaterialWeightType = attrib.Value;
            //SetComponentDescription();

        }


        #endregion



        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class ConcreteMaterialWeightSelectionViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<ConcreteMaterialWeightSelection>
        {
            public void CustomizeView(ConcreteMaterialWeightSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                ConcreteMaterialWeightSelectionView control = new ConcreteMaterialWeightSelectionView();
                control.DataContext = model;
                
                
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

        }
    }
}
