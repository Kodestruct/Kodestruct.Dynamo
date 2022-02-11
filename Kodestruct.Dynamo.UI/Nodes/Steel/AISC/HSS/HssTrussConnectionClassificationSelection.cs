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
using Dynamo.Graph.Nodes;
using Dynamo.Graph;
using Newtonsoft.Json;

namespace Kodestruct.Steel.AISC.HSS
{

    /// <summary>
    ///HSS truss connection classification selection  
    /// </summary>

    [NodeName("HSS truss connection classification selection")]
    [NodeCategory("Kodestruct.Steel.AISC.HSS.Truss")]
    [NodeDescription("HSS truss connection classification selection")]
    [IsDesignScriptCompatible]
    public class HssTrussConnectionClassificationSelection : UiNodeBase
    {

        [JsonConstructor]
        public HssTrussConnectionClassificationSelection(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
        {

        }
        public HssTrussConnectionClassificationSelection()
        {

            //InPorts.Add(new PortModel(PortType.Input, this, new PortData("Port Name", "Port Description")));
            OutPorts.Add(new PortModel(PortType.Output, this, new PortData("HssTrussConnectionClassification", "Distinguishes between T, Y, X, gapped K or overlapped K")));
            RegisterAllPorts();
            SetDefaultParameters();
            //PropertyChanged += NodePropertyChanged;
        }

        private void SetDefaultParameters()
        {

            HssTrussConnectionClassification = "GappedK";
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

		#region HssTrussConnectionClassificationProperty
		
		/// <summary>
		/// HssTrussConnectionClassification property
		/// </summary>
		/// <value>Distinguishes between T, Y, X, gapped K or overlapped K</value>
		public string _HssTrussConnectionClassification;
		
		public string HssTrussConnectionClassification
		{
		    get { return _HssTrussConnectionClassification; }
		    set
		    {
		        _HssTrussConnectionClassification = value;
		        RaisePropertyChanged("HssTrussConnectionClassification");
		        OnNodeModified();
		    }
		}
		#endregion



        #endregion
        #endregion



        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class HssTrussConnectionClassificationSelectionViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<HssTrussConnectionClassificationSelection>
        {
            public void CustomizeView(HssTrussConnectionClassificationSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                HssTrussConnectionClassificationSelectionView control = new HssTrussConnectionClassificationSelectionView();
                control.DataContext = model;
                
                
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

        }
    }
}
