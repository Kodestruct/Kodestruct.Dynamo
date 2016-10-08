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
using System.Reflection;
using Dynamo.Controls;
using Dynamo.Models;
using Dynamo.Wpf;
using Kodestruct.Dynamo.Common;
using System.Xml;
using System.Windows;
using Kodestruct.Dynamo.Common.Infra;
using System.Collections.ObjectModel;
using System.Windows.Resources;
using System.IO;
using System.Linq;
using Dynamo.Nodes;
using Dynamo.Graph;
using Dynamo.Graph.Nodes;


namespace Kodestruct.Wood.NDS.General


{

    /// <summary>
    ///Selection of species, commercial grade and size class for determining NDS reference values
    /// </summary>
    [NodeName("Wood species and grade")]
    [NodeCategory("Kodestruct.Wood.NDS.General")]
    [NodeDescription("Wood species and grade")]
    [IsDesignScriptCompatible]
    public class WoodSpeciesAndGradeSelection : UiNodeBase
    {

        public WoodSpeciesAndGradeSelection()
        {
            
            
            //OutPortData.Add(new PortData("ReportEntry", "Calculation log entries (for reporting)"));
            OutPortData.Add(new PortData("WoodSpeciesId", "Wood species for reference value selection"));
            OutPortData.Add(new PortData("CommercialGradeId", "Wood commercial grade"));
            OutPortData.Add(new PortData("SizeClassId", "Wood species size classification for reference value selection"));
            OutPortData.Add(new PortData("WoodMemberType", "Wood member type"));
            RegisterAllPorts();
            this.WoodMemberType = "SawnDimensionLumber"; //initial member type assignment to get species list
            SetDefaultParams();
            //PropertyChanged += NodePropertyChanged;
        }

        private void PopulateSpeciesList()
        {
            if (WoodMemberType == "SawnDimensionLumber")
            {
                AvailableWoodSpecies = FetchSpeciesList(ResourceFileName);
            }
            else
            {
                ClearAllAvailableData();
            }
            
        }



        /// <summary>
        ///     Gets the type of this class, to be used in base class for reflection
        /// </summary>
        protected override Type GetModelType()
        {
            return GetType();
        }

        #region properties

        #region OutputProperties

		#region WoodSpeciesIdProperty
		
		/// <summary>
		/// WoodSpeciesId property
		/// </summary>
		/// <value>Section name from steel shape database</value>
		public string _WoodSpeciesId;
		
		public string WoodSpeciesId
		{
		    get { return _WoodSpeciesId; }
		    set
		    {
                if (value!=null)
                {
                    _WoodSpeciesId = value;
                    RaisePropertyChanged("WoodSpeciesId");
                    UpdateViewForSelectedSpecies();
                    OnNodeModified(true);  
                }

		    }
		}


		#endregion

        #region CommercialGradeIdProperty

        /// <summary>
        /// CommercialGradeId property
        /// </summary>
        /// <value>Section name from steel shape database</value>
        public string _CommercialGradeId;

        public string CommercialGradeId
        {
            get { return _CommercialGradeId; }
            set
            {
                if (value != null)
                {
                    _CommercialGradeId = value;
                    RaisePropertyChanged("CommercialGradeId");
                    UpdateViewForSelectedGrade();
                    OnNodeModified(true);
                }

            }
        }
        #endregion

        #region SizeClassIdProperty

        /// <summary>
        /// SizeClassId property
        /// </summary>
        /// <value>Section name from steel shape database</value>
        public string _SizeClassId;

        public string SizeClassId
        {
            get { return _SizeClassId; }
            set
            {
                if (value != null)
                {
                    _SizeClassId = value;
                    RaisePropertyChanged("SizeClassId");
                    OnNodeModified(true);
                }

            }
        }
        #endregion

        #region WoodMemberTypeProperty

        /// <summary>
        /// WoodMemberType property
        /// </summary>
        /// <value>Section name from steel shape database</value>
        public string _WoodMemberType;

        public string WoodMemberType
        {
            get { return _WoodMemberType; }
            set
            {
                if (value != null)
                {
                    _WoodMemberType = value;
                    RaisePropertyChanged("WoodMemberType");
                    SetResourceFileName(_WoodMemberType);
                    PopulateSpeciesList();
                    OnNodeModified(true);
                }

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


        #region SelectedItem Property
        private EnumDataElement selectedItem;
        public EnumDataElement SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; RaisePropertyChanged("SelectedItem"); }
        }
        #endregion


        private void SetDefaultParams()
        {
            //this.WoodMemberType = "SawnDimensionLumber";
            this.WoodSpeciesId     = "DOUGLAS FIR-LARCH";
            this.CommercialGradeId = "Stud";

        }

        bool RefreshView;



        #region Display parameters


        private string _resourceFileName;

        public string ResourceFileName
        {
            get { return _resourceFileName; }
            set { _resourceFileName = value; }
        }
        

        private void UpdateViewForSelectedSpecies()
        {
            CommercialGrades = FetchCommercialGradeList(ResourceFileName, WoodSpeciesId);
            if (CommercialGrades.Contains(CommercialGradeId))
            {
                //Leave grade untouched
            }
            else
            {
                if (CommercialGrades.Contains("Stud"))
                {
                    CommercialGradeId = "Stud";
                }
                else
                {
                    CommercialGradeId = CommercialGrades[4];
                }
            }
        }

        private void UpdateViewForSelectedGrade()
        {
            SizeClasses = FetchSizeClassList(ResourceFileName, WoodSpeciesId, CommercialGradeId);
            if (SizeClasses.Contains(SizeClassId))
            {
                //can keep current size class
            }
            else
            {
                SizeClassId = SizeClasses[0];
            }
        }

        private void SetResourceFileName(string _WoodMemberType)
        {
            //NDS2015Table4A
            if (WoodMemberType == "SawnDimensionLumber")
            {
                ResourceFileName = "NDS2015Table4A";
            }
            else
            {
                ResourceFileName = null;
            }

        }
        private ObservableCollection<string> FetchSpeciesList(string ResourceFileName)
        {

            IEnumerable<string> distValues = null;


            if (ResourceFileName != null)
            {

                string resourceName = string.Format("KodestructDynamoUI.Resources.{0}.txt", ResourceFileName);
                var assembly = Assembly.GetExecutingAssembly();

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    string line;
                    using (TextReader tr = new StreamReader(stream))
                    {
                        //read full list of reference values
                        List<string> AllReferenceValues = new List<string>();
                        while ((line = tr.ReadLine()) != null)
                        {
                            AllReferenceValues.Add(line);
                        }

                         distValues = AllReferenceValues.Select(
                            v => 
                                {
                                    string[] Vals = v.Split(',');
                                    if (Vals.Length > 1)
                                    {
                                        return Vals[0];
                                    }
                                    else
                                    {
                                        return null;
                                    }
	                        }
                            ).Distinct();


                    }
                }
            }

            ObservableCollection<string> availabilitytList = new ObservableCollection<string>(distValues);
            return availabilitytList; 
        }

        private ObservableCollection<string> FetchCommercialGradeList(string ResourceFileName, string WoodSpecies)
        {

            //ObservableCollection<string> availabilitytList = new ObservableCollection<string>();

            List<string> filteredList = new List<string>();

            if ( ResourceFileName != null)
            {

                string resourceName = string.Format("KodestructDynamoUI.Resources.{0}.txt", ResourceFileName);
                var assembly = Assembly.GetExecutingAssembly();

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    string line;
                    using (TextReader tr = new StreamReader(stream))
                    {
                        //read full list of reference values
                        List<string> AllReferenceValues = new List<string>();
                        while ((line = tr.ReadLine()) != null)
                        {
                            AllReferenceValues.Add(line);
                        }

                        //Lookup 

                        foreach (var sh in AllReferenceValues)
                        {


                            string[] Vals = sh.Split(',');
                            if (Vals.Length == 3)
                            {
                                if (Vals[0] == WoodSpecies)
                                {
                                           filteredList.Add( (string)Vals[1]);
                                }
                            }
                        }
                    }
                }
            }
            ObservableCollection<string>  availabilitytList = new ObservableCollection<string>(filteredList.Distinct());
            return availabilitytList;
        }

        private ObservableCollection<string> FetchSizeClassList(string ResourceFileName, string WoodSpecies, string CommercialGradeId)
        {

            List<string> filteredList = new List<string>();

            if ( ResourceFileName != null)
            {

                string resourceName = string.Format("KodestructDynamoUI.Resources.{0}.txt", ResourceFileName);
                var assembly = Assembly.GetExecutingAssembly();

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    string line;
                    using (TextReader tr = new StreamReader(stream))
                    {
                        //read full list of reference values
                        List<string> AllReferenceValues = new List<string>();
                        while ((line = tr.ReadLine()) != null)
                        {
                            AllReferenceValues.Add(line);
                        }

                        //Lookup 

                        foreach (var sh in AllReferenceValues)
                        {


                            string[] Vals = sh.Split(',');
                            if (Vals.Length == 3)
                            {
                                if (Vals[0] == WoodSpecies && Vals[1] == CommercialGradeId)
                                {
                                        filteredList.Add((string)Vals[2]);
                                }
                            }
                        }
                    }
                }
            }
            ObservableCollection<string> availabilitytList = new ObservableCollection<string>(filteredList.Distinct());
            return availabilitytList;
        }

        #region AvailableWoodSpecies Property
        private ObservableCollection<string> _AvailableWoodSpecies;
        public ObservableCollection<string> AvailableWoodSpecies
        {
            get { return _AvailableWoodSpecies; }
            set
            {
                if (_AvailableWoodSpecies == null)
                {
                    _AvailableWoodSpecies = new ObservableCollection<string>();
                }
                _AvailableWoodSpecies = value;
                RaisePropertyChanged("AvailableWoodSpecies");
            }
        }
        #endregion


        #region CommercialGrades Property
        private ObservableCollection<string> _CommercialGrades;
        public ObservableCollection<string> CommercialGrades
        {
            get { return _CommercialGrades; }
            set
            {
                if (_CommercialGrades == null)
                {
                    _CommercialGrades = new ObservableCollection<string>();
                }
                _CommercialGrades = value;
                RaisePropertyChanged("CommercialGrades");
            }
        }
        #endregion

        #region SizeClasses Property
        private ObservableCollection<string> _SizeClasses;
        public ObservableCollection<string> SizeClasses
        {
            get {
                if (_SizeClasses == null)
                {
                    _SizeClasses = new ObservableCollection<string>();
                }
                return _SizeClasses; }
            set
            {
                _SizeClasses = value;
                RaisePropertyChanged("SizeClasses");
            }
        }
        #endregion

        void UpdateView()
        {
            if (RefreshView == true)
            {


                ClearAllAvailableData();
                //TODO search
            }
        }

        private void ClearAllAvailableData()
        {

            AvailableWoodSpecies = null;
            CommercialGrades = null;
            SizeClasses = null;
        }



        #endregion

        #region Serialization

        /// <summary>
        ///Saves property values to be retained when opening the node     
        /// </summary>
        protected override void SerializeCore(XmlElement nodeElement, SaveContext context)
        {
            base.SerializeCore(nodeElement, context);
            
            //nodeElement.SetAttribute("ReportEntry",ReportEntry);
 

            nodeElement.SetAttribute("WoodSpeciesId", WoodSpeciesId);
            nodeElement.SetAttribute("CommercialGradeId", CommercialGradeId);
            nodeElement.SetAttribute("SizeClassId", SizeClassId);
            nodeElement.SetAttribute("WoodMemberType", WoodMemberType);



        }

        /// <summary>
        ///Retrieved property values when opening the node     
        /// </summary>
        protected override void DeserializeCore(XmlElement nodeElement, SaveContext context)
        {
            base.DeserializeCore(nodeElement, context);

            var WoodSpeciesId_attrib = nodeElement.Attributes["WoodSpeciesId"]; if (WoodSpeciesId_attrib != null) { WoodSpeciesId = WoodSpeciesId_attrib.Value; }
            var CommercialGradeId_attrib = nodeElement.Attributes["CommercialGradeId"]; if (CommercialGradeId_attrib != null) { CommercialGradeId = CommercialGradeId_attrib.Value; }
            var SizeClassId_attrib = nodeElement.Attributes["SizeClassId"]; if (SizeClassId_attrib != null) { SizeClassId = SizeClassId_attrib.Value; }
            var WoodMemberType_attrib = nodeElement.Attributes["WoodMemberType"]; if (WoodMemberType_attrib != null) { WoodMemberType = WoodMemberType_attrib.Value; }

        }



        #endregion


        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class AiscShapeSelectionViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<WoodSpeciesAndGradeSelection>
        {
            public void CustomizeView(WoodSpeciesAndGradeSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                WoodSpeciesAndGradeSelectionView control = new WoodSpeciesAndGradeSelectionView();
                control.DataContext = model;
                
               
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

        }
    }



}
