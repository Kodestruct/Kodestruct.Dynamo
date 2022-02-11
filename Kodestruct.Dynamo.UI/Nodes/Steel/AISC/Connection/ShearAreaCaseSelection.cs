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

namespace Kodestruct.Steel.AISC.Connection
{

    /// <summary>
    ///Case selection for shear area calculations in affected elements in connections (block shear, shear yielding, shear rupture).  
    /// </summary>

    [NodeName("Shear area case selection")]
    [NodeCategory("Kodestruct.Steel.AISC.Connection.AffectedElements")]
    [NodeDescription("Case selection for shear area calculations in affected elements in connections (block shear, shear yielding, shear rupture).")]
    [IsDesignScriptCompatible]
    public class ShearAreaCaseSelection : UiNodeBase
    {
        [JsonConstructor]
        public ShearAreaCaseSelection(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
        {

        }
        public ShearAreaCaseSelection()
        {

            ShearAreaCaseId = "TBlock";
            //OutPorts.Add(new PortData("ReportEntry", "Calculation log entries (for reporting)"));
            OutPorts.Add(new PortModel(PortType.Output, this, new PortData("ShearAreaCaseId", "Case selection for shear area calculations in affected elements in connections (block shear, shear yielding, shear rupture).Values are: StraightLine,TBlock,UBlock,Lblock")));
            RegisterAllPorts();
            //PropertyChanged += NodePropertyChanged;
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

		#region ShearAreaCaseIdProperty
		
		/// <summary>
		/// ShearAreaCaseId property
		/// </summary>
		/// <value>Case selection for shear area calculations in affected elements in connections (block shear, shear yielding, shear rupture).Values are: StraightLine,TBlock,UBlock,Lblock</value>
		public string _ShearAreaCaseId;
		
		public string ShearAreaCaseId
		{
		    get { return _ShearAreaCaseId; }
		    set
		    {
		        _ShearAreaCaseId = value;
		        RaisePropertyChanged("ShearAreaCaseId");
		        OnNodeModified(true); 
		    }
		}
		#endregion

 
        #endregion
        #endregion


        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class GroupShearAreaPatternSelectionViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<ShearAreaCaseSelection>
        {
            public void CustomizeView(ShearAreaCaseSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                ShearAreaCaseView control = new ShearAreaCaseView();
                control.DataContext = model;
                
                
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

            
        }
    }
}
