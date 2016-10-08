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


namespace Kodestruct.Wood.NDS.General
{

    /// <summary>
    ///Wood member type  
    /// </summary>

    [NodeName("Wood member type")]
    [NodeCategory("Kodestruct.Wood.NDS.General")]
    [NodeDescription("Wood member type")]
    [IsDesignScriptCompatible]
    public class WoodMemberTypeSelection : UiNodeBase
    {

        public WoodMemberTypeSelection()
        {
            
            //OutPortData.Add(new PortData("ReportEntry", "Calculation log entries (for reporting)"));
            OutPortData.Add(new PortData("WoodMemberType", "Distinguishes between dimensional lumber, timber,glulam etc."));
            RegisterAllPorts();
            SetDefaultParameters();
            //PropertyChanged += NodePropertyChanged;
        }

        private void SetDefaultParameters()
        {
            //ReportEntry="";
            WoodMemberType = "SawnDimensionLumber";
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

		#region WoodMemberTypeProperty
		
		/// <summary>
		/// WoodMemberType property
		/// </summary>
		/// <value>Distinguishes between dimensional lumber, timber,glulam etc.</value>
		public string _WoodMemberType;
		
		public string WoodMemberType
		{
		    get { return _WoodMemberType; }
		    set
		    {
		        _WoodMemberType = value;
		        RaisePropertyChanged("WoodMemberType");
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
            nodeElement.SetAttribute("WoodMemberType", WoodMemberType);
        }

        /// <summary>
        ///Retrieved property values when opening the node     
        /// </summary>
        protected override void DeserializeCore(XmlElement nodeElement, SaveContext context)
        {
            base.DeserializeCore(nodeElement, context);
            var attrib = nodeElement.Attributes["WoodMemberType"];
            if (attrib == null)
                return;
           
            WoodMemberType = attrib.Value;

        }




        #endregion






        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class WoodMemberTypeSelectionViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<WoodMemberTypeSelection>
        {
            public void CustomizeView(WoodMemberTypeSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                WoodMemberTypeSelectionView control = new WoodMemberTypeSelectionView();
                control.DataContext = model;
                
                
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

        }
    }
}
