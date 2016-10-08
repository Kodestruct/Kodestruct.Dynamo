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
using Dynamo.Nodes;
using System.Xml;
using Dynamo.Graph;
using Dynamo.Graph.Nodes;


namespace Kodestruct.Steel.AISC.Composite.Anchor
{

    /// <summary>
    ///Strength of headed stud anchor  
    /// </summary>

    [NodeName("Headed anchor weld case selection")]
    [NodeCategory("Kodestruct.Steel.AISC.Composite.Anchor")]
    [NodeDescription("Strength of headed stud anchor")]
    [IsDesignScriptCompatible]
    public class HeadedAnchorWeldCaseSelection : UiNodeBase
    {

        public HeadedAnchorWeldCaseSelection()
        {
            

            //OutPortData.Add(new PortData("ReportEntry", "Calculation log entries (for reporting)"));
            OutPortData.Add(new PortData("HeadedAnchorWeldCase", "Identifies the type of welding between the anchor and shape (through deck or not)"));
            RegisterAllPorts();
            SetDefaultPrameters();
            //PropertyChanged += NodePropertyChanged;
        }

        private void SetDefaultPrameters()
        {
            //ReportEntry="";
            HeadedAnchorWeldCase = "WeldedThroughDeck";
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

		#region HeadedAnchorWeldCaseProperty
		
		/// <summary>
		/// HeadedAnchorWeldCase property
		/// </summary>
		/// <value>Identifies the type of welding between the anchor and shape (through deck or not)</value>
		public string _HeadedAnchorWeldCase;
		
		public string HeadedAnchorWeldCase
		{
		    get { return _HeadedAnchorWeldCase; }
		    set
		    {
		        _HeadedAnchorWeldCase = value;
		        RaisePropertyChanged("HeadedAnchorWeldCase");
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
            nodeElement.SetAttribute("HeadedAnchorWeldCase", HeadedAnchorWeldCase);
        }

        /// <summary>
        ///Retrieved property values when opening the node     
        /// </summary>
        protected override void DeserializeCore(XmlElement nodeElement, SaveContext context)
        {
            base.DeserializeCore(nodeElement, context);
            var attrib = nodeElement.Attributes["HeadedAnchorWeldCase"];
            if (attrib == null)
                return;

            HeadedAnchorWeldCase = attrib.Value;

        }




        #endregion


        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class HeadedAnchorWeldCaseSelectionViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<HeadedAnchorWeldCaseSelection>
        {
            public void CustomizeView(HeadedAnchorWeldCaseSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                HeadedAnchorWeldCaseView control = new HeadedAnchorWeldCaseView();
                control.DataContext = model;
                
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

        }
    }
}
