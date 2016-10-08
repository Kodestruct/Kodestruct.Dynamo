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
using System.Linq;
using System.Xml;
using System.Collections.ObjectModel;
using System.Windows.Resources;
using System.IO;
using System.Windows;
using Dynamo.Nodes;
using System.Reflection;
using Dynamo.Graph;
using Dynamo.Graph.Nodes;


namespace Kodestruct.Steel.AISC.General
{

    /// <summary>
    ///Selection of material and grade for structural steel  
    /// </summary>

    [NodeName("Recommended material selection ")]
    [NodeCategory("Kodestruct.Steel.AISC.General.MaterialProperties")]
    [NodeDescription("Selection of material and grade for structural steel")]
    [IsDesignScriptCompatible]
    public class RecommendedMaterialSelection : UiNodeBase
    {

        public RecommendedMaterialSelection()
        {

            //InPortData.Add(new PortData("d_b", "Bolt diameter required for recommended material filtering"));
            //OutPortData.Add(new PortData("ReportEntry", "Calculation log entries (for reporting)"));
            OutPortData.Add(new PortData("SteelMaterialId", "Steel material"));
            //OutPortData.Add(new PortData("d_b", "Bolt diameter required for recommended material filtering"));
            RegisterAllPorts();
            SetDefaultParameters();

            //PropertyChanged += NodePropertyChanged;
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


        #region OutputProperties

		#region SteelMaterialIdProperty
		
		/// <summary>
		/// SteelMaterialId property
		/// </summary>
		/// <value>Steel material</value>
		public string _SteelMaterialId;
		
		public string SteelMaterialId
		{
		    get { return _SteelMaterialId; }
		    set
		    {
                if (value!=null)
                {
                    _SteelMaterialId = value;
                    RaisePropertyChanged("SteelMaterialId");
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

        #region Serialization

        /// <summary>
        ///Saves property values to be retained when opening the node     
        /// </summary>
        protected override void SerializeCore(XmlElement nodeElement, SaveContext context)
        {
            base.SerializeCore(nodeElement, context);
            

            nodeElement.SetAttribute("MaterialGroup",MaterialGroup);
            nodeElement.SetAttribute("HighStrengthBoltType",HighStrengthBoltType);
            nodeElement.SetAttribute("AnchorRodType",AnchorRodType);
            nodeElement.SetAttribute("PlateThicknessRange",PlateThicknessRange);
            nodeElement.SetAttribute("CatalogShapeType",CatalogShapeType);
            nodeElement.SetAttribute("IShapeType",IShapeType);
            nodeElement.SetAttribute("CShapeType",CShapeType);
            nodeElement.SetAttribute("TShapeType",TShapeType);
            nodeElement.SetAttribute("CHSType",CHSType);
            nodeElement.SetAttribute("d_b",d_b.ToString());
            nodeElement.SetAttribute("IsShapeI",IsShapeI.ToString());
            nodeElement.SetAttribute("IsMaterialAnchorRod", IsMaterialAnchorRod.ToString());

            nodeElement.SetAttribute("SteelMaterialId", SteelMaterialId);
        }

        /// <summary>
        ///Retrieved property values when opening the node     
        /// </summary>
        protected override void DeserializeCore(XmlElement nodeElement, SaveContext context)
        {
            base.DeserializeCore(nodeElement, context);



            var  MaterialGroupAttrib=nodeElement.Attributes["MaterialGroup"];               if(MaterialGroupAttrib!=null){MaterialGroup=MaterialGroupAttrib.Value;}
            var  HighStrengthBoltTypeAttrib=nodeElement.Attributes["HighStrengthBoltType"]; if(HighStrengthBoltTypeAttrib!=null){HighStrengthBoltType=HighStrengthBoltTypeAttrib.Value;}
            var  AnchorRodTypeAttrib=nodeElement.Attributes["AnchorRodType"];               if(AnchorRodTypeAttrib!=null){AnchorRodType=AnchorRodTypeAttrib.Value;}
            var  PlateThicknessRangeAttrib=nodeElement.Attributes["PlateThicknessRange"];   if(PlateThicknessRangeAttrib!=null){PlateThicknessRange=PlateThicknessRangeAttrib.Value;}
            var  CatalogShapeTypeAttrib=nodeElement.Attributes["CatalogShapeType"];         if(CatalogShapeTypeAttrib!=null){CatalogShapeType=CatalogShapeTypeAttrib.Value;}
            var  IShapeTypeAttrib=nodeElement.Attributes["IShapeType"];                     if(IShapeTypeAttrib!=null){IShapeType=IShapeTypeAttrib.Value;}
            var  CShapeTypeAttrib=nodeElement.Attributes["CShapeType"];                     if(CShapeTypeAttrib!=null){CShapeType=CShapeTypeAttrib.Value;}
            var  TShapeTypeAttrib=nodeElement.Attributes["TShapeType"];                     if(TShapeTypeAttrib!=null){TShapeType=TShapeTypeAttrib.Value;}
            var  CHSTypeAttrib=nodeElement.Attributes["CHSType"];                           if(CHSTypeAttrib!=null){CHSType=CHSTypeAttrib.Value;}
            var  d_bAttrib=nodeElement.Attributes["d_b"];                                   if(d_bAttrib!=null){d_b=double.Parse(d_bAttrib.Value);}
            var  IsShapeIAttrib=nodeElement.Attributes["IsShapeI"];                         if(IsShapeIAttrib!=null){IsShapeI=bool.Parse(IsShapeIAttrib.Value);}
            var  IsMaterialAnchorRodAttrib=nodeElement.Attributes["IsMaterialAnchorRod"];   if(IsMaterialAnchorRodAttrib!=null){IsMaterialAnchorRod=bool.Parse(IsMaterialAnchorRodAttrib.Value);}
   


            var attrib = nodeElement.Attributes["SteelMaterialId"];
            if (attrib == null)
                return;
           
            SteelMaterialId = attrib.Value;

        }


        #endregion


        public const string SHAPEFILE = "AiscMaterialShapeMaterial";
        public const string PLATEFILE = "AiscMaterialPlateMaterial";
        public const string FASTENERFILE = "AiscMaterialFastenerMaterial";

        private void SetDefaultParameters()
        {
            ReportEntry = "";
            SteelMaterialId = "A992";
            RecommendedSpecificationUsed = true;
            MaterialGroup = "Shape";
            HighStrengthBoltType = "Conventional";
            AnchorRodType = "ThreadedAndNutted";
            PlateThicknessRange = "PL_0_75less_t_lessEq1_25";
            CatalogShapeType = "IShape";
            IShapeType = "W";
            CShapeType = "C";
            TShapeType = "WT";
            CHSType = "CHS";
            d_b = 0.75;
            IsShapeI = true;
            IsMaterialAnchorRod = false;
            FetchAllAvailableMaterialsList();
            UpdateView();
        }

        #region All materials
        public ObservableCollection<string> AllMaterials { get; set; }
        private void FetchAllAvailableMaterialsList()
        {
            this.FetchList(null, "AiscMaterialsAll");
        }

        #endregion

        #region MaterialGroup


        //String backup property for backend and storing parameter data
        private string _MaterialGroup;

        public string MaterialGroup
        {
            get { return _MaterialGroup; }
            set
            {
                //No validation is performed on Enum
                if (value != null)
                {
                    _MaterialGroup = value;
                    RaisePropertyChanged("MaterialGroup");
                    UpdateView();
                }
            }
        }



        #endregion

        #region d_b Property
        private double _db;

        public double d_b
        {
            get { return _db; }
            set
            {
                _db = value;
                RaisePropertyChanged("d_b");
                //FetchAllAvailableMaterialsList();
                UpdateView();
            }
        }
        #endregion

        #region Shapes

        #region CatalogShapeType


        //String backup property for backend and storing parameter data
        private string _CatalogShapeType;

        public string CatalogShapeType
        {
            get { return _CatalogShapeType; }
            set
            {
                //No validation is performed on Enum
                if (value != null)
                {
                    _CatalogShapeType = value;
                    RaisePropertyChanged("CatalogShapeType");
                    UpdateView();
                }
            }
        }


        #endregion

        #region RecommendedSpecificationUsed Property
        private bool recommendedSpecificationUsed;

        public bool RecommendedSpecificationUsed
        {
            get { return recommendedSpecificationUsed; }
            set
            {
                recommendedSpecificationUsed = value;
                RaisePropertyChanged("RecommendedSpecificationUsed");
            }
        }
        #endregion



        #region IShapeTypeView


        //String backup property for backend and storing parameter data
        private string _IShapeType;


        public string IShapeType
        {
            get { return _IShapeType; }
            set
            {
                //No validation is performed on Enum
                if (value != null)
                {
                    _IShapeType = value;
                    RaisePropertyChanged("IShapeType");
                    UpdateView();
                }
            }
        }


        #endregion

        #region CShapeType


        //String backup property for backend and storing parameter data
        private string _CShapeType;

        public string CShapeType
        {
            get { return _CShapeType; }
            set
            {

                _CShapeType = value;
                RaisePropertyChanged("CShapeType");
                UpdateView();
            }
        }


        #endregion

        #region TShapeType


        //String backup property for backend and storing parameter data
        private string _TShapeType;

        public string TShapeType
        {
            get { return _TShapeType; }
            set
            {
                _TShapeType = value;
                RaisePropertyChanged("TShapeType");
                UpdateView();
            }
        }



        #endregion


        #region CHSTypeView Property (with string backup CHSType property)


        //String backup property for backend and storing parameter data
        private string _CHSType;

        public string CHSType
        {
            get { return _CHSType; }
            set
            {
                _CHSType = value;
                RaisePropertyChanged("CHSType");
                UpdateView();
            }
        }



        #endregion
        #endregion

        #region Plates


        #region PlateThicknessRangeView Property (with string backup PlateThicknessRange property)


        //String backup property for backend and storing parameter data
        private string _PlateThicknessRange;

        public string PlateThicknessRange
        {
            get { return _PlateThicknessRange; }
            set
            {

                _PlateThicknessRange = value;
                RaisePropertyChanged("PlateThicknessRange");
                UpdateView();
            }
        }


        #endregion




        #endregion

        #region Fasteners


        #region BoltType


        private string _BoltType;

        public string BoltType
        {
            get { return _BoltType; }
            set
            {
                _BoltType = value;
                RaisePropertyChanged("BoltType");
                UpdateView();
            }
        }



        #endregion



        #region HighStrengthBoltType


        private string _HighStrengthBoltType;

        public string HighStrengthBoltType
        {
            get { return _HighStrengthBoltType; }
            set
            {
                _HighStrengthBoltType = value;
                RaisePropertyChanged("HighStrengthBoltType");
                UpdateView();
            }
        }



        #endregion



        #region AnchorRodType


        private string _AnchorRodType;

        public string AnchorRodType
        {
            get { return _AnchorRodType; }
            set
            {

                _AnchorRodType = value;
                RaisePropertyChanged("AnchorRodType");
                UpdateView();
            }
        }


        #endregion



        #endregion

        #region AvailableMaterials Property
        private ObservableCollection<string> availableMaterials;
        public ObservableCollection<string> AvailableMaterials
        {
            get
            {
                if (availableMaterials == null)
                {
                    availableMaterials = new ObservableCollection<string>();
                }
                return availableMaterials;
            }
            set
            {
                availableMaterials = value;
                RaisePropertyChanged("AvailableMaterials");
            }
        }
        #endregion

        #region Display parameters


        #region VisibilityParameters


        #region IsMaterialShape Property
        private bool _IsMaterialShape;
        public bool IsMaterialShape
        {
            get { return _IsMaterialShape; }
            set { _IsMaterialShape = value; RaisePropertyChanged("IsMaterialShape"); }
        }
        #endregion


        #region IsMaterialPlate Property
        private bool _IsMaterialPlate;
        public bool IsMaterialPlate
        {
            get { return _IsMaterialPlate; }
            set { _IsMaterialPlate = value; RaisePropertyChanged("IsMaterialPlate"); }
        }
        #endregion


        #region IsMaterialFastener Property
        private bool _IsMaterialFastener;
        public bool IsMaterialFastener
        {
            get { return _IsMaterialFastener; }
            set { _IsMaterialFastener = value; RaisePropertyChanged("IsMaterialFastener"); }
        }
        #endregion


        #region IsMaterialHighStrengthBolt Property
        private bool _IsMaterialHighStrengthBolt;
        public bool IsMaterialHighStrengthBolt
        {
            get { return _IsMaterialHighStrengthBolt; }
            set { _IsMaterialHighStrengthBolt = value; RaisePropertyChanged("IsMaterialHighStrengthBolt"); }
        }
        #endregion


        #region IsMaterialAnchorRod Property
        private bool _IsMaterialAnchorRod;
        public bool IsMaterialAnchorRod
        {
            get { return _IsMaterialAnchorRod; }
            set
            {
                _IsMaterialAnchorRod = value;
                RaisePropertyChanged("IsMaterialAnchorRod");
            }
        }
        #endregion

        #endregion

        #region Lists


        #region Shape specific update methods

        void UpdateViewShapeI()
        {

            switch (IShapeType)
            {
                case "W":
                    FetchList("W", SHAPEFILE);
                    break;
                case "S":
                    FetchList("S", SHAPEFILE);
                    break;
                case "M":
                    FetchList("M", SHAPEFILE);
                    break;
            }
        }


        private void UpdateViewShapeChannel()
        {
            switch (CShapeType)
            {
                case "C":
                    FetchList("C", SHAPEFILE);
                    break;
                case "MC":
                    FetchList("MC", SHAPEFILE);
                    break;
            }

        }


        private void UpdateViewShapeTube()
        {
            FetchList("RHS", SHAPEFILE);

        }

        private void UpdateViewShapePipe()
        {
            switch (CHSType)
            {
                case "CHS":
                    FetchList("CHS", SHAPEFILE);
                    break;
                case "Pipe":
                    FetchList("Pipe", SHAPEFILE);
                    break;
            }

        }

        private void UpdateViewShapeDoubleAngle()
        {
            FetchList("L2", SHAPEFILE);
        }

        private void UpdateViewShapeTee()
        {
            switch (TShapeType)
            {
                case "WT":
                    FetchList("WT", SHAPEFILE);
                    break;
                case "MT":
                    FetchList("MT", SHAPEFILE);
                    break;
                case "ST":
                    FetchList("ST", SHAPEFILE);
                    break;

            }
        }

        private void UpdateViewShapeAngle()
        {
            FetchList("L", SHAPEFILE);
        }

        #endregion

        private void FetchList(string ElementTypeId, string MaterialGroupId)
        {
            AvailableMaterials = null;

                string resourceName = string.Format("KodestructDynamoUI.Resources.{0}.txt", MaterialGroupId);
                var assembly = Assembly.GetExecutingAssembly();

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {

                    string line;
                    using (TextReader tr = new StreamReader(stream))
                    {
                        List<string> AllMaterialEntries = new List<string>();
                        while ((line = tr.ReadLine()) != null)
                        {
                            AllMaterialEntries.Add(line);
                        }


                        if (ElementTypeId!=null)
                        {
                            parseMaterialsToList(AllMaterialEntries, ElementTypeId);
                        }
                        else
                        {
                            AllMaterials = new ObservableCollection<string>(AllMaterialEntries);
                        }

                    }
                }
            //set the first item in list as default recommended material

            if (AvailableMaterials != null && AvailableMaterials.Count != 0)
            {
                SteelMaterialId = AvailableMaterials[0];
            }
        }



        private void parseMaterialsToList(List<string> AllMaterialEntries, string ElementTypeId)
        {

            //find row in the data file
            var FoundItem = AllMaterialEntries.Where(sh =>
            {
                string[] subsStr = sh.Split(new string[] { "," }, StringSplitOptions.None);
                if (subsStr[0] == ElementTypeId)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
                 ).FirstOrDefault();

            //split row into individual materials
            if (FoundItem != null)
            {
                AvailableMaterials = new ObservableCollection<string>();
                string[] Materials = FoundItem.Split(new string[] { "," }, StringSplitOptions.None);
                if (Materials.Length > 1)
                {
                    for (int i = 1; i < Materials.Length; i++)
                    {
                        string MatName = Materials[i];
                        //modify list if this is anchor bolt to account for diameter
                        if (IsMaterialAnchorRod == true)
                        {
                            if (d_b > 0)
                            {
                                bool MaterialAvailable = CheckIfMaterialAvailableForBoltDiameter(MatName, d_b);
                                if (MaterialAvailable == true)
                                {
                                    AvailableMaterials.Add(MatName);
                                }
                            }
                            else
                            {
                                AvailableMaterials.Add(MatName);
                            }
                        }
                        else
                        {
                            AvailableMaterials.Add(MatName);
                        }
                    }

                }
            }
        }

        private List<FastenerLimitEntry> fastenerDiameterList;

        private List<FastenerLimitEntry> FastenerDiameterList
        {
            get
            {
                if (fastenerDiameterList == null)
                {
                    fastenerDiameterList = FetchFastenerLimitList();
                }
                return fastenerDiameterList;
            }
            set { fastenerDiameterList = value; }
        }

        private List<FastenerLimitEntry> FetchFastenerLimitList()
        {
            List<FastenerLimitEntry> FastenerLimitEntries = new List<FastenerLimitEntry>();


            string resourceName = "KodestructDynamoUI.Resources.AiscMaterialFastenerDiameterLimits.txt";
                var assembly = Assembly.GetExecutingAssembly();

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {

                    string line;
                    using (TextReader tr = new StreamReader(stream))
                    {
                        List<string> AllMaterialEntries = new List<string>();
                        while ((line = tr.ReadLine()) != null)
                        {
                            string[] me = line.Split(new string[] { "," }, StringSplitOptions.None);
                            if (me.Length == 3)
                            {
                                double MinDiam = double.Parse(me[1]);
                                double MaxDiam = double.Parse(me[2]);
                                FastenerLimitEntries.Add(new FastenerLimitEntry() { MaterialName = me[0], MinDiameter = MinDiam, MaxDiameter = MaxDiam });
                            }

                        }
                    }
                }
                if (FastenerLimitEntries.Count > 0)
                {
                    return FastenerLimitEntries;
                }
                else
                {
                    return null;
                }

        }
        private bool CheckIfMaterialAvailableForBoltDiameter(string MatName, double db)
        {
            bool MaterialAvailableForThisDiameter = false;
            bool MaterialHasLimits = false;

            if (FastenerDiameterList != null)
            {
                foreach (var m in FastenerDiameterList)
                {
                    if (m.MaterialName == MatName)
                    {
                        MaterialHasLimits = true;
                        if (db > m.MinDiameter && db <= m.MaxDiameter)
                        {
                            MaterialAvailableForThisDiameter = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                MaterialAvailableForThisDiameter = true;
            }

            if (MaterialHasLimits == true)
            {
                return MaterialAvailableForThisDiameter;
            }
            else
            {
                return true;
            }

        }

        #endregion

        private void ClearAllIsShapeProperties()
        {

            var props = this.GetType().GetProperties();

            foreach (var p in props)
            {
                if (p.PropertyType == typeof(bool))
                {
                    if (p.Name.Contains("IsShape"))
                    {
                        p.SetValue(this, false, null);
                    }
                }
                if (p.PropertyType == typeof(bool))
                {
                    if (p.Name.Contains("IsMaterial"))
                    {
                        p.SetValue(this, false, null);
                    }
                }
                if (p.PropertyType == typeof(string))
                {
                    if (p.Name.Contains("Type"))
                    {
                        //ShowOrHideParameterInParameterViewer(p.Name, true);
                    }
                }
            }

        }


        void UpdateView()
        {
            ClearAllIsShapeProperties();
            switch (MaterialGroup)
            {
                case "Shape":
                    IsMaterialShape = true;
                    switch (CatalogShapeType)
                    {
                        case "IShape":
                            IsShapeI = true;
                            UpdateViewShapeI();
                            break;
                        case "Channel":
                            IsShapeChannel = true;
                            UpdateViewShapeChannel();
                            break;
                        case "Angle":
                            IsShapeAngle = true;
                            UpdateViewShapeAngle();
                            break;
                        case "Tee":
                            IsShapeTee = true;
                            UpdateViewShapeTee();
                            break;
                        case "DoubleAngle":
                            IsShapeDoubleAngle = true;
                            UpdateViewShapeDoubleAngle();
                            break;
                        case "CircularHSS":
                            IsShapeCHS = true;
                            UpdateViewShapePipe();
                            break;
                        case "RectangularHSS":
                            IsShapeTube = true;
                            UpdateViewShapeTube();
                            break;

                    }
                    break;
                case "Plate":
                    IsMaterialPlate = true;
                    UpdateViewPlate();
                    break;
                case "Fastener":
                    IsMaterialFastener = true;
                    switch (BoltType)
                    {
                        case "HighStrengthBolt":
                            IsMaterialHighStrengthBolt = true;
                            switch (HighStrengthBoltType)
                            {
                                case "Conventional":
                                    FetchList("ConventionalHighStrengthBolt", FASTENERFILE);
                                    break;
                                case "TwistOff":
                                    FetchList("TwistOffHighStrengthBolt", FASTENERFILE);
                                    break;
                            }
                            break;
                        case "CommonBolt":
                            FetchList("ConventionalBolt", FASTENERFILE);
                            //do nothing since common bolts do not have option
                            break;
                        case "AnchorRod":
                            IsMaterialAnchorRod = true;
                            switch (AnchorRodType)
                            {
                                case "ThreadedAndNutted":
                                    FetchList("ThreadedAndNuttedAnchorBolt", FASTENERFILE);
                                    break;
                                case "Headed":
                                    FetchList("HeadedAnchorBolt", FASTENERFILE);
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
            if (AvailableMaterials.Count > 0)
            {
                SteelMaterialId = AvailableMaterials[0];
            }

        }

        private void UpdateViewPlate()
        {
            FetchList(PlateThicknessRange.ToString(), PLATEFILE);
        }


        #region IsShapeI Property
        private bool _IsShapeI;
        public bool IsShapeI
        {
            get { return _IsShapeI; }
            set
            {
                _IsShapeI = value;
                RaisePropertyChanged("IsShapeI");
            }

        }
        #endregion

        #region IsShapeChannel Property
        private bool _IsShapeChannel;
        public bool IsShapeChannel
        {
            get { return _IsShapeChannel; }
            set { _IsShapeChannel = value; RaisePropertyChanged("IsShapeChannel"); }
        }
        #endregion

        #region IsShapeTee Property
        private bool _IsShapeTee;
        public bool IsShapeTee
        {
            get { return _IsShapeTee; }
            set { _IsShapeTee = value; RaisePropertyChanged("IsShapeTee"); }
        }
        #endregion

        #region IsShapeAngle Property
        private bool _IsShapeAngle;
        public bool IsShapeAngle
        {
            get { return _IsShapeAngle; }
            set { _IsShapeAngle = value; RaisePropertyChanged("IsShapeAngle"); }
        }
        #endregion

        #region IsShapeDoubleAngle Property
        private bool _IsShapeDoubleAngle;
        public bool IsShapeDoubleAngle
        {
            get { return _IsShapeDoubleAngle; }
            set { _IsShapeDoubleAngle = value; RaisePropertyChanged("IsShapeDoubleAngle"); }
        }
        #endregion

        #region IsShapeTube Property
        private bool _IsShapeTube;
        public bool IsShapeTube
        {
            get { return _IsShapeTube; }
            set { _IsShapeTube = value; RaisePropertyChanged("IsShapeTube"); }
        }
        #endregion

        #region IsShapeCHS Property
        private bool _IsShapeCHS;
        public bool IsShapeCHS
        {
            get { return _IsShapeCHS; }
            set { _IsShapeCHS = value; RaisePropertyChanged("IsShapeCHS"); }
        }
        #endregion


        #endregion

        class FastenerLimitEntry
        {
            public string MaterialName { get; set; }
            public double MinDiameter { get; set; }
            public double MaxDiameter { get; set; }

        }

        #endregion

        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class RecommendedMaterialSelectionViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<RecommendedMaterialSelection>
        {
            public void CustomizeView(RecommendedMaterialSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                RecommendedMaterialSelectionView control = new RecommendedMaterialSelectionView();
                control.DataContext = model;
                
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }


        }
    }
}
