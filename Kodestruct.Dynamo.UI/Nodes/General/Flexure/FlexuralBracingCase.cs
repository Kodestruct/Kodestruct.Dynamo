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
using Newtonsoft.Json;

namespace Kodestruct.General.Flexure
{

    /// <summary>
    ///Flexural bracing case selection  
    /// </summary>

    [NodeName("Flexural bracing case selection")]
    [NodeCategory("Kodestruct.General.Flexure")]
    [NodeDescription("Flexural bracing case selection")]
    [IsDesignScriptCompatible]
    public class FlexuralBracingCaseSelection : UiNodeBase
    {

        [JsonConstructor]
        public FlexuralBracingCaseSelection(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
        {

        }
        public FlexuralBracingCaseSelection()
        {

            //InPorts.Add(new PortModel(PortType.Input, this, new PortData("Port Name", "Port Description")));
            OutPorts.Add(new PortModel(PortType.Output, this, new PortData("FlexuralBracingCase", "Identifies the type of lateral bracing for a flexural member")));
            RegisterAllPorts();
            SetDefaultParameters();
            //PropertyChanged += NodePropertyChanged;
        }

        private void SetDefaultParameters()
        {
            //ReportEntry="";
            FlexuralBracingCase = "NoLateralBracing";

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

		#region FlexuralBracingCaseProperty
		
		/// <summary>
		/// FlexuralBracingCase property
		/// </summary>
		/// <value>Identifies the type of lateral bracing for a flexural member</value>
		public string _FlexuralBracingCase;
		
		public string FlexuralBracingCase
		{
		    get { return _FlexuralBracingCase; }
		    set
		    {
		        _FlexuralBracingCase = value;
		        RaisePropertyChanged("FlexuralBracingCase");
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


        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class FlexuralBracingCaseViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<FlexuralBracingCaseSelection>
        {
            public void CustomizeView(FlexuralBracingCaseSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                FlexuralBracingCaseView control = new FlexuralBracingCaseView();
                control.DataContext = model;
                
                
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

        }
    }
}
