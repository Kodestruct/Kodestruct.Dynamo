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
using Kodestruct.Dynamo.UI.Views.Steel.AISC10.Shear;
using Newtonsoft.Json;

namespace Kodestruct.Steel.AISC.Shear
{

    /// <summary>
    ///Shear case noncircular  
    /// </summary>

    [NodeName("Shear case selection for noncircular member")]
    [NodeCategory("Kodestruct.Steel.AISC.Shear")]
    [NodeDescription("Shear case noncircular")]
    [IsDesignScriptCompatible]
    public class NonCircularShearCaseSelection : UiNodeBase
    {

        [JsonConstructor]
        public NonCircularShearCaseSelection(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
        {

        }
        public NonCircularShearCaseSelection()
        {

            //InPorts.Add(new PortModel(PortType.Input, this, new PortData("Port Name", "Port Description")));
            OutPorts.Add(new PortModel(PortType.Output,this, new PortData("NonCircularShearCase", "Case type for shear checks")));
            RegisterAllPorts();
            SetDefaultParameters();
            //PropertyChanged += NodePropertyChanged;
        }

        private void SetDefaultParameters()
        {
            //ReportEntry="";
            NonCircularShearCase = "MemberWithoutStiffeners";
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

		#region NonCircularShearCaseProperty
		
		/// <summary>
		/// NonCircularShearCase property
		/// </summary>
		/// <value>Shape type for shear</value>
		public string _NonCircularShearCase;
		
		public string NonCircularShearCase
		{
		    get { return _NonCircularShearCase; }
		    set
		    {
		        _NonCircularShearCase = value;
		        RaisePropertyChanged("NonCircularShearCase");
		        OnNodeModified(true); 
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
            nodeElement.SetAttribute("NonCircularShearCase", NonCircularShearCase);
        }

        /// <summary>
        ///Retrieved property values when opening the node     
        /// </summary>
        protected override void DeserializeCore(XmlElement nodeElement, SaveContext context)
        {
            base.DeserializeCore(nodeElement, context);
            var attrib = nodeElement.Attributes["NonCircularShearCase"];
            if (attrib == null)
                return;
           
            NonCircularShearCase = attrib.Value;

        }


        #endregion




        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class NonCircularShearCaseSelectionViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<NonCircularShearCaseSelection>
        {
            public void CustomizeView(NonCircularShearCaseSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                ShearNonCircularCaseView control = new ShearNonCircularCaseView();
                control.DataContext = model;
                
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

        }
    }
}
