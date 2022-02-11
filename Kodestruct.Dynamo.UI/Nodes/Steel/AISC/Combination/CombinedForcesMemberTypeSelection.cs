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
using System.Xml;
using Dynamo.Nodes;
using Dynamo.Graph;
using Dynamo.Graph.Nodes;
using Newtonsoft.Json;

namespace Kodestruct.Steel.AISC.Combination
{

    /// <summary>
    ///Combined forces member type selection  
    /// </summary>

    [NodeName("Combined forces member type selection")]
    [NodeCategory("Kodestruct.Steel.AISC.Combination")]
    [NodeDescription("Combined forces member type selection")]
    [IsDesignScriptCompatible]
    public class CombinedForcesMemberTypeSelection : UiNodeBase
    {
        [JsonConstructor]
        public CombinedForcesMemberTypeSelection(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
        {

        }
        public CombinedForcesMemberTypeSelection()
        {
            //OutPorts.Add(new PortData("ReportEntry", "Calculation log entries (for reporting)"));
            OutPorts.Add(new PortModel(PortType.Output, this, new PortData("CombinationCaseId", "Defines a type of interaction equation to be used")));
            RegisterAllPorts();
            SetDefaultParameters();
            //PropertyChanged += NodePropertyChanged;
        }

        private void SetDefaultParameters()
        {

            CombinationCaseId           = "H1";
            MemberForceCase             = "FlexureAndAxial";
            MemberSectionType           = "DoublyOrSinglySymmetric";
            ElementType                 = "Member";
            ConnectionCombinationType   = "Plastic";
        }



        /// <summary>
        ///     Gets the type of this class, to be used in base class for reflection
        /// </summary>
        protected override Type GetModelType()
        {
            return GetType();
        }

        #region properties

        #region InputProperties



	    #endregion

        #region OutputProperties

		#region CombinationCaseIdProperty
		
		/// <summary>
		/// CombinationCaseId property
		/// </summary>
		/// <value>Defines a type of interaction equation to be used</value>
		public string _CombinationCaseId;
		
		public string CombinationCaseId
		{
		    get { return _CombinationCaseId; }
		    set
		    {
		        _CombinationCaseId = value;
		        RaisePropertyChanged("CombinationCaseId");
		        OnNodeModified(true); 
		    }
		}
		#endregion

 
        #endregion

        #endregion


        #region Display parameters


        #region IsMember Property
        private bool _IsMember;
        public bool IsMember
        {
            get { return _IsMember; }
            set
            {
                _IsMember = value;
                RaisePropertyChanged("IsMember");
            }
        }
        #endregion


        #region IsAxialAndFlexureMember Property
        private bool _IsAxialAndFlexureMember;
        public bool IsAxialAndFlexureMember
        {
            get { return _IsAxialAndFlexureMember; }
            set
            {
                _IsAxialAndFlexureMember = value;
                RaisePropertyChanged("IsAxialAndFlexureMember");
            }
        }
        #endregion

        #region ElementType Property
        private string _ElementType;
        public string ElementType
        {
            get { return _ElementType; }
            set
            {
                _ElementType = value;
                RaisePropertyChanged("ElementType");
                UpdateValuesAndView();
            }
        }


        #endregion


        #region MemberForceCase Property
        private string _MemberForceCase;
        public string MemberForceCase
        {
            get { return _MemberForceCase; }
            set
            {
                _MemberForceCase = value;
                RaisePropertyChanged("MemberForceCase");
                UpdateValuesAndView();
            }
        }
        #endregion


        #region MemberSectionType Property
        private string _MemberSectionType;
        public string MemberSectionType
        {
            get { return _MemberSectionType; }
            set
            {
                _MemberSectionType = value;
                RaisePropertyChanged("MemberSectionType");
                UpdateValuesAndView();
            }
        }
        #endregion


        #region ConnectionCombinationType Property
        private string _ConnectionCombinationType;
        public string ConnectionCombinationType
        {
            get { return _ConnectionCombinationType; }
            set
            {
                _ConnectionCombinationType = value;
                RaisePropertyChanged("ConnectionCombinationType");
                UpdateValuesAndView();
            }
        }
        #endregion



        private void UpdateValuesAndView()
        {
            if (ElementType == "Member")
            {
                IsMember = true;
                if (MemberForceCase == "FlexureAndAxial")
                {
                    IsAxialAndFlexureMember = true;
                    if (MemberSectionType == "DoublyOrSinglySymmetric")
                    {
                        CombinationCaseId = "H1";
                    }
                    else
                    {
                        CombinationCaseId = "H2";
                    }

                }
                else
                {
                    IsAxialAndFlexureMember = false;
                    CombinationCaseId = "H3";
                }
            }
            else
            {
                IsMember = false;
                switch (ConnectionCombinationType)
                {
                    case "Linear": CombinationCaseId = "Linear"; break;
                    case "Elliptical": CombinationCaseId = "Elliptical"; break;
                    case "Plastic": CombinationCaseId = "Plastic"; break;
                    default:
                        CombinationCaseId = "Linear";
                        break;
                }
            }
        }

        #endregion
    

        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class CombinedForcesMemberTypeSelectionViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<CombinedForcesMemberTypeSelection>
        {
            public void CustomizeView(CombinedForcesMemberTypeSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                CombinationCaseIdView control = new CombinationCaseIdView();
                control.DataContext = model;
                
                
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

        }
    }
}
