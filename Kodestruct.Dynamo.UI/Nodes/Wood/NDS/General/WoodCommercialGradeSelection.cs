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

namespace Kodestruct.Wood.NDS.General
{

    /// <summary>
    ///Wood commercial grade  
    /// </summary>

    [NodeName("Wood commercial grade")]
    [NodeCategory("Kodestruct.Wood.NDS.General")]
    [NodeDescription("Wood commercial grade")]
    [IsDesignScriptCompatible]
    public class WoodCommercialGradeSelection : UiNodeBase
    {
        [JsonConstructor]
        public WoodCommercialGradeSelection(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
        {

        }
        public WoodCommercialGradeSelection()
        {

            //InPorts.Add(new PortModel(PortType.Input, this, new PortData("Port Name", "Port Description")));//OutPorts.Add(new PortData("ReportEntry", "Calculation log entries (for reporting)"));
            OutPorts.Add(new PortModel(PortType.Output, this, new PortData("WoodCommercialGrade", "Identifies commercial grade of wood being considered")));
            RegisterAllPorts();
            SetDefaultParameters();
            //PropertyChanged += NodePropertyChanged;
        }

        private void SetDefaultParameters()
        {
            //ReportEntry="";
            WoodCommercialGrade = "No1";
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

		#region WoodCommercialGradeProperty
		
		/// <summary>
		/// WoodCommercialGrade property
		/// </summary>
		/// <value>Identifies commercial grade of wood being considered</value>
		public string _WoodCommercialGrade;
		
		public string WoodCommercialGrade
		{
		    get { return _WoodCommercialGrade; }
		    set
		    {
		        _WoodCommercialGrade = value;
		        RaisePropertyChanged("WoodCommercialGrade");
		        OnNodeModified();
		    }
		}
		#endregion


        #endregion
        #endregion




        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class WoodCommercialGradeSelectionViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<WoodCommercialGradeSelection>
        {
            public void CustomizeView(WoodCommercialGradeSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                WoodCommercialGradeSelectionView control = new WoodCommercialGradeSelectionView();
                control.DataContext = model;
                
                
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

        }
    }
}
