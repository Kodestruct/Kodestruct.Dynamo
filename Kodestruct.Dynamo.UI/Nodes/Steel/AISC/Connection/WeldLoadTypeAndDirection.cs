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
using Kodestruct.Loads.ASCE7.Entities;
using System.Xml;
using Dynamo.Nodes;
using Dynamo.Graph;
using Dynamo.Graph.Nodes;
using Newtonsoft.Json;

namespace Kodestruct.Steel.AISC.Connection
{

    /// <summary>
    ///Load type and direction  
    /// </summary>

    [NodeName("Weld load type and direction")]
    [NodeCategory("Kodestruct.Steel.AISC.Connection.Welded")]
    [NodeDescription("Weld load type and direction for strength calculation")]
    [IsDesignScriptCompatible]
    public class LoadTypeAndDirection : UiNodeBase
    {
        [JsonConstructor]
        public LoadTypeAndDirection(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
        {

        }
        public LoadTypeAndDirection()
        {
            
            //OutPorts.Add(new PortData("ReportEntry", "Calculation log entries (for reporting)"));
            OutPorts.Add(new PortModel(PortType.Output, this, new PortData("WeldLoadTypeId", "Type of load on weld  under consideration")));
            RegisterAllPorts();
            //PropertyChanged += NodePropertyChanged;
            SetDefaultParameters();

        }

        private void SetDefaultParameters()
        {
            ReportEntry = "";
            WeldLoadTypeId = "WeldShear";
            CompressionSpliceType = "WeldCompressionSpliceFinishedToBear";
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

		#region WeldLoadTypeIdProperty
		
		/// <summary>
		/// WeldLoadTypeId property
		/// </summary>
		/// <value>Type of load on weld  under consideration</value>
		public string _WeldLoadTypeId;
		
		public string WeldLoadTypeId
		{
		    get { return _WeldLoadTypeId; }
		    set
		    {
		        _WeldLoadTypeId = value;
		        RaisePropertyChanged("WeldLoadTypeId");
		        OnNodeModified(true); 
                UpdateViewAndValues();
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
            nodeElement.SetAttribute("WeldLoadTypeId", WeldLoadTypeId);
        }

        /// <summary>
        ///Retrieved property values when opening the node     
        /// </summary>
        protected override void DeserializeCore(XmlElement nodeElement, SaveContext context)
        {
            base.DeserializeCore(nodeElement, context);
            var attrib = nodeElement.Attributes["WeldLoadTypeId"];
            if (attrib == null)
                return;
           
            WeldLoadTypeId = attrib.Value;
 
        }


        #endregion


        #region Display parameters


        #region IsCompressionSplice Property
        private bool _IsCompressionSplice;
        public bool IsCompressionSplice
        {
            get { return _IsCompressionSplice; }
            set
            {
                _IsCompressionSplice = value;
                RaisePropertyChanged("IsCompressionSplice");
            }
        }
        #endregion


        #region CompressionSpliceType Property
        private string _CompressionSpliceType;
        public string CompressionSpliceType
        {
            get { return _CompressionSpliceType; }
            set
            {
                _CompressionSpliceType = value;
                RaisePropertyChanged("CompressionSpliceType");
            }
        }
        #endregion

        private void UpdateViewAndValues()
        {

            if (WeldLoadTypeId == "WeldCompressionNormal")
            {
                IsCompressionSplice = true;
                WeldLoadTypeId = CompressionSpliceType;
            }
            else
            {
                if (WeldLoadTypeId != "WeldCompressionSpliceFinishedToBear" &&
                    WeldLoadTypeId != "WeldCompressionSpliceNotFinishedToBear")
                {
                    IsCompressionSplice = false;
                }

            }

        }

        #endregion




        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class LoadTypeAndDirectionViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<LoadTypeAndDirection>
        {
            public void CustomizeView(LoadTypeAndDirection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                WeldLoadTypeIdView control = new WeldLoadTypeIdView();
                control.DataContext = model;
                
                
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }


        }


    }
}
