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


namespace Kodestruct.Concrete.ACI318.Details.General
{

    /// <summary>
    ///Rebar hook type  
    /// </summary>

    [NodeName("Rebar hook type")]
    [NodeCategory("Kodestruct.Concrete.ACI318.Details.General")]
    [NodeDescription("Rebar hook type")]
    [IsDesignScriptCompatible]
    public class HookTypeSelection : UiNodeBase
    {

        public HookTypeSelection()
        {
            
            //OutPortData.Add(new PortData("ReportEntry", "Calculation log entries (for reporting)"));
            OutPortData.Add(new PortData("HookType", "Identifies rebar hook configuration (90-degree versus 180-degree)"));
            RegisterAllPorts();
            SetDefaultParameters();
            //PropertyChanged += NodePropertyChanged;
        }

        private void SetDefaultParameters()
        {
            //ReportEntry="";
            HookType = "Hook90";
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

		#region HookTypeProperty
		
		/// <summary>
		/// HookType property
		/// </summary>
		/// <value>Identifies rebar hook configuration (90-degree versus 180-degree)</value>
		public string _HookType;
		
		public string HookType
		{
		    get { return _HookType; }
		    set
		    {
		        _HookType = value;
		        RaisePropertyChanged("HookType");
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
            nodeElement.SetAttribute("HookType", HookType);
        }

        /// <summary>
        ///Retrieved property values when opening the node     
        /// </summary>
        protected override void DeserializeCore(XmlElement nodeElement, SaveContext context)
        {
            base.DeserializeCore(nodeElement, context);
            var attrib = nodeElement.Attributes["HookType"];
            if (attrib == null)
                return;
           
            HookType = attrib.Value;
            //SetComponentDescription();

        }


        #endregion





        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class HookTypeSelectionViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<HookTypeSelection>
        {
            public void CustomizeView(HookTypeSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                HookTypeSelectionView control = new HookTypeSelectionView();
                control.DataContext = model;
                
                
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

        }
    }
}
