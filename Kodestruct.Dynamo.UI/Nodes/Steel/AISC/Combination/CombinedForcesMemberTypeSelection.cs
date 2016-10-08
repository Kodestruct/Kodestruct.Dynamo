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

        public CombinedForcesMemberTypeSelection()
        {
            //OutPortData.Add(new PortData("ReportEntry", "Calculation log entries (for reporting)"));
            OutPortData.Add(new PortData("CombinationCaseId", "Defines a type of interaction equation to be used"));
            RegisterAllPorts();
            SetDefaultParameters();
            //PropertyChanged += NodePropertyChanged;
        }

        private void SetDefaultParameters()
        {
            ReportEntry                 = "";
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
            
            nodeElement.SetAttribute("ReportEntry",ReportEntry);
            nodeElement.SetAttribute("MemberForceCase",MemberForceCase);
            nodeElement.SetAttribute("MemberSectionType",MemberSectionType);
            nodeElement.SetAttribute("ElementType",ElementType);
            nodeElement.SetAttribute("ConnectionCombinationType",ConnectionCombinationType);

            nodeElement.SetAttribute("CombinationCaseId", CombinationCaseId);

        }

        /// <summary>
        ///Retrieved property values when opening the node     
        /// </summary>
        protected override void DeserializeCore(XmlElement nodeElement, SaveContext context)
        {
            base.DeserializeCore(nodeElement, context);
            
            var MemberForceCase_attrib = nodeElement.Attributes["MemberForceCase"]; if (MemberForceCase_attrib != null) { MemberForceCase = MemberForceCase_attrib.Value; }
            var MemberSectionType_attrib = nodeElement.Attributes["MemberSectionType"]; if (MemberSectionType_attrib != null) { MemberSectionType = MemberSectionType_attrib.Value; }
            var ElementType_attrib = nodeElement.Attributes["ElementType"]; if (ElementType_attrib != null) { ElementType = ElementType_attrib.Value; }
            var ConnectionCombinationType_attrib = nodeElement.Attributes["ConnectionCombinationType"]; if (ConnectionCombinationType_attrib != null) { ConnectionCombinationType = ConnectionCombinationType_attrib.Value; }
            var attrib = nodeElement.Attributes["CombinationCaseId"]; if (attrib != null) { CombinationCaseId = attrib.Value; }
        }


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
