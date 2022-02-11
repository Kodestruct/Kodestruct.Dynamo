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

namespace Kodestruct.Steel.AISC.FloorVibrations.EffectiveProperties
{

    /// <summary>
    ///Beam location selection  
    /// </summary>

    [NodeName("Beam location selection")]
    [NodeCategory("Kodestruct.Steel.AISC.FloorVibrations.EffectiveProperties")]
    [NodeDescription("Beam location selection")]
    [IsDesignScriptCompatible]
    public class BeamLocationSelection : UiNodeBase
    {
        [JsonConstructor]
        public BeamLocationSelection(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
        {

        }
        public BeamLocationSelection()
        {
            
            //OutPorts.Add(new PortData("ReportEntry", "Calculation log entries (for reporting)"));
            OutPorts.Add(new PortModel(PortType.Output, this, new PortData("BeamLocation", "Distinguishes between beams located at the floor free edge versus all other  beams")));
            RegisterAllPorts();
            SetDefaultParameters();
            //PropertyChanged += NodePropertyChanged;
        }

        private void SetDefaultParameters()
        {
            //ReportEntry="";
            this.BeamLocation ="Inner";
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

		#region BeamLocationProperty
		
		/// <summary>
		/// BeamLocation property
		/// </summary>
		/// <value>Distinguishes between beams located at the floor free edge versus all other  beams</value>
		public string _BeamLocation;
		
		public string BeamLocation
		{
		    get { return _BeamLocation; }
		    set
		    {
		        _BeamLocation = value;
		        RaisePropertyChanged("BeamLocation");
		        OnNodeModified();
		    }
		}
		#endregion


        #endregion
        #endregion



        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class BeamLocationSelectionViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<BeamLocationSelection>
        {
            public void CustomizeView(BeamLocationSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                BeamLocationSelectionView control = new BeamLocationSelectionView();
                control.DataContext = model;
 
                
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

        }
    }
}
