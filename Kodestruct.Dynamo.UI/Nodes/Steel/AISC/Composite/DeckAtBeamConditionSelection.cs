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

namespace Kodestruct.Steel.AISC.Composite.Anchor
{

    /// <summary>
    ///Strength of headed  anchor  
    /// </summary>

    [NodeName("Deck condition selection")]
    [NodeCategory("Kodestruct.Steel.AISC.Composite.Anchor")]
    [NodeDescription("Strength of headed  anchor")]
    [IsDesignScriptCompatible]
    public class DeckConditionSelection : UiNodeBase
    {
        [JsonConstructor]
        public DeckConditionSelection(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
        {

        }
        public DeckConditionSelection()
        {

            //OutPorts.Add(new PortData("ReportEntry", "Calculation log entries (for reporting)"));
            OutPorts.Add(new PortModel(PortType.Output, this, new PortData("DeckAtBeamCondition", "Identifies whether deck runs parallel or perpendicular to beam or there is no deck")));
            RegisterAllPorts();
            SetDefaultParameters();
            //PropertyChanged += NodePropertyChanged;
        }

        private void SetDefaultParameters()
        {
            //ReportEntry="";
            DeckAtBeamCondition = "Perpendicular";
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

		#region HeadedAnchorDeckConditionProperty
		
		/// <summary>
		/// HeadedAnchorDeckCondition property
		/// </summary>
		/// <value>Identifies whether deck runs parallel or perpendicular to beam or there is no deck</value>
		public string _HeadedAnchorDeckCondition;
		
		public string DeckAtBeamCondition
		{
		    get { return _HeadedAnchorDeckCondition; }
		    set
		    {
		        _HeadedAnchorDeckCondition = value;
		        RaisePropertyChanged("HeadedAnchorDeckCondition");
		        OnNodeModified(true); 
		    }
		}
		#endregion


        #endregion
        #endregion


        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class DeckConditionSelectionViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<DeckConditionSelection>
        {
            public void CustomizeView(DeckConditionSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                DeckConditionSelectionView control = new DeckConditionSelectionView();
                control.DataContext = model;
                
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

        }
    }
}
