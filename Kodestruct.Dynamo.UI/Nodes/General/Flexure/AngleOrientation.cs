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
using Dynamo.Graph;
using System.Xml;
using Newtonsoft.Json;

namespace Kodestruct.General.Flexure
{

    /// <summary>
    ///Angle orientation  
    /// </summary>

    [NodeName("Angle orientation")]
    [NodeCategory("Kodestruct.General.Flexure")]
    [NodeDescription("Angle orientation")]
    [IsDesignScriptCompatible]
    public class AngleOrientationSelection : UiNodeBase
    {

        [JsonConstructor]
        public AngleOrientationSelection(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
        {

        }
        public AngleOrientationSelection()
        {

            //InPorts.Add(new PortModel(PortType.Input, this, new PortData("Port Name", "Port Description")));
            OutPorts.Add(new PortModel(PortType.Output, this, new PortData("AngleOrientation", "Ondicates whether the long leg of unequal leg angle is vertical or horizontal")));
            RegisterAllPorts();
            SetDefaultParameters();
            //PropertyChanged += NodePropertyChanged;
        }

        private void SetDefaultParameters()
        {
            //ReportEntry="";
            AngleOrientation = "LongLegVertical";
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

		#region AngleOrientationProperty
		
		/// <summary>
		/// AngleOrientation property
		/// </summary>
		/// <value>Ondicates whether the long leg of unequal leg angle is vertical or horizontal</value>
		public string _AngleOrientation;
		
		public string AngleOrientation
		{
		    get { return _AngleOrientation; }
		    set
		    {
		        _AngleOrientation = value;
		        RaisePropertyChanged("AngleOrientation");
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
            nodeElement.SetAttribute("AngleOrientation", AngleOrientation);
        }

        /// <summary>
        ///Retrieved property values when opening the node     
        /// </summary>
        protected override void DeserializeCore(XmlElement nodeElement, SaveContext context)
        {
            base.DeserializeCore(nodeElement, context);
            var attrib = nodeElement.Attributes["AngleOrientation"];
            if (attrib == null)
                return;
           
            AngleOrientation = attrib.Value;
            //SetComponentDescription();

        }


        #endregion



        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class AngleOrientationViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<AngleOrientationSelection>
        {
            public void CustomizeView(AngleOrientationSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                AngleOrientationView control = new AngleOrientationView();
                control.DataContext = model;
                
                
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

        }
    }
}
