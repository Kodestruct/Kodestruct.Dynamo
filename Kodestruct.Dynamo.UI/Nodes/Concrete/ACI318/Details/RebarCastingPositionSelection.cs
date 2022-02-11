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

namespace Kodestruct.Concrete.ACI318.Details.General
{

    /// <summary>
    ///Rebar casting position  
    /// </summary>

    [NodeName("Rebar casting position")]
    [NodeCategory("Kodestruct.Concrete.ACI318.Details.General")]
    [NodeDescription("Rebar casting position")]
    [IsDesignScriptCompatible]
    public class RebarCastingPositionSelection : UiNodeBase
    {

        [JsonConstructor]
        public RebarCastingPositionSelection(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
        {

        }
        public RebarCastingPositionSelection()
        {

            //InPorts.Add(new PortModel(PortType.Input, this, new PortData("Port Name", "Port Description")));
            OutPorts.Add(new PortModel(PortType.Output, this, new PortData("RebarCastingPosition", "Indicates if rebar is  a horizontal bar placed over 12in. Of concrete.")));
            RegisterAllPorts();
            SetDefaultParameters();
            //PropertyChanged += NodePropertyChanged;
        }

        private void SetDefaultParameters()
        {
            //ReportEntry="";
            RebarCastingPosition = "Top";
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

		#region RebarCastingPositionProperty
		
		/// <summary>
		/// RebarCastingPosition property
		/// </summary>
		/// <value>Indicates if rebar is  a horizontal bar placed over 12in. Of concrete.</value>
		public string _RebarCastingPosition;
		
		public string RebarCastingPosition
		{
		    get { return _RebarCastingPosition; }
		    set
		    {
		        _RebarCastingPosition = value;
		        RaisePropertyChanged("RebarCastingPosition");
		        OnNodeModified();
		    }
		}
		#endregion



        #endregion
        #endregion

 



        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class RebarCastingPositionSelectionViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<RebarCastingPositionSelection>
        {
            public void CustomizeView(RebarCastingPositionSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                RebarCastingPositionSelectionView control = new RebarCastingPositionSelectionView();
                control.DataContext = model;
                
                
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

        }
    }
}
