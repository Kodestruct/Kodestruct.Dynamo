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
using Dynamo.Nodes;
using Dynamo.Graph;
using Dynamo.Graph.Nodes;


namespace Kodestruct.Steel.AISC.Connection.AffectedElements
{

    /// <summary>
    ///Beam cope case selection  
    /// </summary>

    [NodeName("Beam cope case selection")]
    [NodeCategory("Kodestruct.Steel.AISC.Connection.AffectedElements")]
    [NodeDescription("Beam cope case selection")]
    [IsDesignScriptCompatible]
    public class BeamCopeCaseSelection : UiNodeBase
    {

        public BeamCopeCaseSelection()
        {
            
            
            //OutPortData.Add(new PortData("ReportEntry", "Calculation log entries (for reporting)"));
            OutPortData.Add(new PortData("BeamCopeCase", "Identifies beam cope condition for stability calculations: single cope vs double cope"));
            RegisterAllPorts();
            SetDefaultParameters();
            //PropertyChanged += NodePropertyChanged;
        }

        private void SetDefaultParameters()
        {
            ReportEntry="";
            BeamCopeCase = "Uncoped";
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

		#region BeamCopeCaseProperty
		
		/// <summary>
		/// BeamCopeCase property
		/// </summary>
		/// <value>Identifies beam cope condition for stability calculations: single cope vs double cope</value>
		public string _BeamCopeCase;
		
		public string BeamCopeCase
		{
		    get { return _BeamCopeCase; }
		    set
		    {
		        _BeamCopeCase = value;
		        RaisePropertyChanged("BeamCopeCase");
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
            nodeElement.SetAttribute("BeamCopeCase", BeamCopeCase);
        }

        /// <summary>
        ///Retrieved property values when opening the node     
        /// </summary>
        protected override void DeserializeCore(XmlElement nodeElement, SaveContext context)
        {
            base.DeserializeCore(nodeElement, context);
            var attrib = nodeElement.Attributes["BeamCopeCase"];
            if (attrib == null)
                return;
           
            BeamCopeCase = attrib.Value;

        }


        #endregion



        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class BeamCopeCaseSelectionViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<BeamCopeCaseSelection>
        {
            public void CustomizeView(BeamCopeCaseSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                BeamCopeCaseView control = new BeamCopeCaseView();
                control.DataContext = model;
                
               
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

        }
    }
}
