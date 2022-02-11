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
using Dynamo.Graph;
using System.Xml;
using Dynamo.Graph.Nodes;
using Newtonsoft.Json;

namespace Kodestruct.Concrete.ACI318.Details.General
{

    /// <summary>
    ///Enclosing rebar direction  
    /// </summary>

    [NodeName("Enclosing rebar direction")]
    [NodeCategory("Kodestruct.Concrete.ACI318.Details.General")]
    [NodeDescription("Enclosing rebar direction")]
    [IsDesignScriptCompatible]
    public class EnclosingRebarDirectionSelection : UiNodeBase
    {

        [JsonConstructor]
        public EnclosingRebarDirectionSelection(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
        {

        }
        public EnclosingRebarDirectionSelection()
        {

            //InPorts.Add(new PortModel(PortType.Input, this, new PortData("Port Name", "Port Description")));
            OutPorts.Add(new PortModel(PortType.Output, this, new PortData("EnclosingRebarDirection", "Indicates if enclosing reinforcement is perpendicular or parallel to bar")));
            RegisterAllPorts();
            SetDefaultParameters();
            //PropertyChanged += NodePropertyChanged;
        }

        private void SetDefaultParameters()
        {
            //ReportEntry="";
            this.EnclosingRebarDirection = "Perpendicular";
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

		#region EnclosingRebarDirectionProperty
		
		/// <summary>
		/// EnclosingRebarDirection property
		/// </summary>
		/// <value>Indicates if enclosing reinforcement is perpendicular or parallel to bar</value>
		public string _EnclosingRebarDirection;
		
		public string EnclosingRebarDirection
		{
		    get { return _EnclosingRebarDirection; }
		    set
		    {
		        _EnclosingRebarDirection = value;
		        RaisePropertyChanged("EnclosingRebarDirection");
		        OnNodeModified();
		    }
		}
		#endregion



        #endregion
        #endregion



        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class EnclosingRebarDirectionSelectionViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<EnclosingRebarDirectionSelection>
        {
            public void CustomizeView(EnclosingRebarDirectionSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                EnclosingRebarDirectionSelectionView control = new EnclosingRebarDirectionSelectionView();
                control.DataContext = model;
                
                
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

        }
    }
}
