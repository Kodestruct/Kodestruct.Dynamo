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


namespace Kodestruct.Loads.ASCE7.Lateral.Wind.PressureCoefficient
{

    /// <summary>
    ///Selection of the type of face relative to wind direction (windward, leeward or side)    
    /// </summary>

    [NodeName("Wind face")]
    [NodeCategory("Kodestruct.Loads.ASCE7.Lateral.Wind.PressureCoefficient")]
    [NodeDescription("Selection of the type of face relative to wind direction (windward, leeward or side)  ")]
    [IsDesignScriptCompatible]
    public class WindFaceSelection : UiNodeBase
    {

        public WindFaceSelection()
        {
            
            //OutPortData.Add(new PortData("ReportEntry", "Calculation log entries (for reporting)"));
            OutPortData.Add(new PortData("WindFaceType", "Type of face relative to wind direction (windward, leeward or side) "));
            RegisterAllPorts();
            SetDefaultParameters();
            //PropertyChanged += NodePropertyChanged;
        }

        private void SetDefaultParameters()
        {
            //ReportEntry="";
            WindFaceType = "Windward";
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

		#region WindFaceTypeProperty
		
		/// <summary>
		/// WindFaceType property
		/// </summary>
		/// <value>Type of face relative to wind direction (windward, leeward or side) </value>
		public string _WindFaceType;
		
		public string WindFaceType
		{
		    get { return _WindFaceType; }
		    set
		    {
		        _WindFaceType = value;
		        RaisePropertyChanged("WindFaceType");
		        OnNodeModified();
		    }
		}
		#endregion



        #region ReportEntryProperty

        ///// <summary>
        ///// log property
        ///// </summary>
        ///// <value>Calculation entries that can be converted into a report.</value>

        //public string reportEntry;

        //public string ReportEntry
        //{
        //    get { return reportEntry; }
        //    set
        //    {
        //        reportEntry = value;
        //        RaisePropertyChanged("ReportEntry");
        //        OnNodeModified();
        //    }
        //}




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
            nodeElement.SetAttribute("WindFaceType", WindFaceType);
        }

        /// <summary>
        ///Retrieved property values when opening the node     
        /// </summary>
        protected override void DeserializeCore(XmlElement nodeElement, SaveContext context)
        {
            base.DeserializeCore(nodeElement, context);
            var attrib = nodeElement.Attributes["WindFaceType"];
            if (attrib == null)
                return;

            WindFaceType = attrib.Value;
            //SetComponentDescription();

        }

        #endregion



        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class WindFaceViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<WindFaceSelection>
        {
            public void CustomizeView(WindFaceSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                WindFaceView control = new WindFaceView();
                control.DataContext = model;
                
               
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

        }
    }
}
