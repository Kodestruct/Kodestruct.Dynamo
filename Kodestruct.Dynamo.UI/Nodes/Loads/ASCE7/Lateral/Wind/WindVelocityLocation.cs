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

namespace Kodestruct.Loads.ASCE7.Lateral.Wind.General
{

    /// <summary>
    ///Selection between wall (vertical surface) and roof (horizontal surface)  type for wind load calculation   
    /// </summary>

    [NodeName("Wind velocity location")]
    [NodeCategory("Kodestruct.Loads.ASCE7.Lateral.Wind.General")]
    [NodeDescription("Selection between wall (vertical surface) and roof (horizontal surface)  type for wind load calculation ")]
    [IsDesignScriptCompatible]
    public class WindVelocityLocationSelection : UiNodeBase
    {

        [JsonConstructor]
        public WindVelocityLocationSelection(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
        {

        }
        public WindVelocityLocationSelection()
        {
            
            //OutPorts.Add(new PortData("ReportEntry", "Calculation log entries (for reporting)"));
            OutPorts.Add(new PortModel(PortType.Output, this, new PortData("WindVelocityLocation", "Location type for wind velocity used in pressure calculations")));
            RegisterAllPorts();
            SetDefaultParameters();
            //PropertyChanged += NodePropertyChanged;
        }

        private void SetDefaultParameters()
        {
            //ReportEntry="";
            WindVelocityLocation = "Wall";
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

		#region WindVelocityLocationProperty
		
		/// <summary>
		/// WindVelocityLocation property
		/// </summary>
		/// <value>Location type for wind velocity used in pressure calculations</value>
		public string _WindVelocityLocation;
		
		public string WindVelocityLocation
		{
		    get { return _WindVelocityLocation; }
		    set
		    {
		        _WindVelocityLocation = value;
		        RaisePropertyChanged("WindVelocityLocation");
		        OnNodeModified();
		    }
		}
		#endregion



        #region ReportEntryProperty

        // /// <summary>
        // /// log property
        // /// </summary>
        // /// <value>Calculation entries that can be converted into a report.</value>

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
            nodeElement.SetAttribute("WindVelocityLocation", WindVelocityLocation);
        }

        /// <summary>
        ///Retrieved property values when opening the node     
        /// </summary>
        protected override void DeserializeCore(XmlElement nodeElement, SaveContext context)
        {
            base.DeserializeCore(nodeElement, context);
            var attrib = nodeElement.Attributes["WindVelocityLocation"];
            if (attrib == null)
                return;
           
            WindVelocityLocation = attrib.Value;
            //SetComponentDescription();

        }



        #endregion




        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class WindVelocityLocationViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<WindVelocityLocationSelection>
        {
            public void CustomizeView(WindVelocityLocationSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                WindVelocityLocationView control = new WindVelocityLocationView();
                control.DataContext = model;
                
               
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

        }
    }
}
