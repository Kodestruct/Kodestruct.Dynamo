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
using Newtonsoft.Json;

namespace Kodestruct.Analysis.Section.AISC

    //Kodestruct.Analysis.Section.AISC.ShapeSelection
{

    /// <summary>
    ///Selection of shape type from AISC v14 database
    /// </summary>
    [NodeName("AISC shape selection")]
    [NodeCategory("Kodestruct.Analysis.Section.AISC")]
    [NodeDescription("Selection of shape type from AISC v14 database")]
    [IsDesignScriptCompatible]
    public class AiscShapeSelection : UiNodeBase
    {

        [JsonConstructor]
        public AiscShapeSelection(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
        {

        }
        public AiscShapeSelection()
        {


            //InPorts.Add(new PortModel(PortType.Input, this, new PortData("Port Name", "Port Description")));
            OutPorts.Add(new PortModel(PortType.Output, this, new PortData("SteelShapeId", "Section name from steel shape database")));
            RegisterAllPorts();
            SetDefaultParams();
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

        #region OutputProperties

		#region SteelShapeIdProperty
		
		/// <summary>
		/// SteelShapeId property
		/// </summary>
		/// <value>Section name from steel shape database</value>
		string _SteelShapeId;
		
		public string SteelShapeId
		{
		    get { return _SteelShapeId; }
		    set
		    {
                if (value!=null)
                {
                    _SteelShapeId = value;
                    RaisePropertyChanged("SteelShapeId");
                    OnNodeModified(true);  
                }

		    }
		}
		#endregion


        #endregion
        #endregion


        #region SelectedItem Property
        private EnumDataElement selectedItem;
        [JsonIgnore]
        public EnumDataElement SelectedItem
        {
            get { return selectedItem; }
            set { selectedItem = value; RaisePropertyChanged("SelectedItem"); }
        }
        #endregion


        private void SetDefaultParams()
        {

            RefreshView = false;
            CatalogShapeType = "IShape";
            ShapeTypeSteel = "IShapeRolled";
            IShapeType = "W";
            CShapeType = "C";
            TShapeType = "WT";
            CHSType = "CHS";
            LDoubleShapeType = "Equal";
            RefreshView = true;
            
            AnchorRodType = "ThreadedAndNutted";
            //Groups
            WShapeGroup = "W18";
            SShapeGroup = "S12";
            MShapeGroup = "M8";
            CShapeGroup = "C8";
            MCShapeGroup = "MC8";
            WTShapeGroup = "WT9";
            STShapeGroup = "ST6";
            LShapeGroup = "L8";
            LDoubleEqualGroup = "Angle2L4";
            LDoubleLLBBGroup = "Angle2L4";
            LDoubleSLBBGroup = "Angle2L4";
            RHSShapeGroup = "HSS6";
            CHSShapeGroup = "HSS6";
            PipeGroup = "Pipe6";
            LDoubleGapType = "NoGap";
            SteelShapeId = "W18X35";

        }

        bool RefreshView;


        #region Selection parameters


        #region CatalogShapeType Property


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


        private void SetDefaultParameterValuesForGivenShapeSeries()
        {
            if (AvailableShapes != null)
            {
                if (AvailableShapes.Count > 5)
                {
                    SteelShapeId = AvailableShapes[4];
                }
                else
                {
                    SteelShapeId = AvailableShapes[AvailableShapes.Count - 1];
                }
            }
        }


        #region ShapeTypeSteel Property


        private string _ShapeTypeSteel;

        public string ShapeTypeSteel
        {
            get { return _ShapeTypeSteel; }
            set
            {
                if (value != null)
                {

                    _ShapeTypeSteel = value;
                    RaisePropertyChanged("ShapeTypeSteel");
                }
            }
        }

        #endregion


        #endregion

        #region I-Shapes


        #region IShapeType Property


        //String backup property for backend and storing parameter data
        private string _IShapeType;

        public string IShapeType
        {
            get { return _IShapeType; }
            set //No validation is performed on Enum
            {
                _IShapeType = value;
                RaisePropertyChanged("IShapeType");
                UpdateView();
            }
        }

        #endregion

        #region WShapeGroup Property
        private string _WShapeGroup;
        public string WShapeGroup
        {
            get { return _WShapeGroup; }
            set
            {
                _WShapeGroup = value;
                UpdateView();
                RaisePropertyChanged("WShapeGroup");

            }
        }
        #endregion

        #region MShapeGroup Property
        private string _MShapeGroup;
        public string MShapeGroup
        {
            get { return _MShapeGroup; }
            set
            {
                _MShapeGroup = value;
                UpdateView();
                RaisePropertyChanged("MShapeGroup");
            }
        }
        #endregion

        #region SShapeGroup Property
        private string _SShapeGroup;
        public string SShapeGroup
        {
            get { return _SShapeGroup; }
            set
            {
                _SShapeGroup = value;
                UpdateView();
                RaisePropertyChanged("SShapeGroup");
            }
        }
        #endregion
        #endregion

        #region Channels


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



        #region CShapeGroup Property
        private string _CShapeGroup;
        public string CShapeGroup
        {
            get { return _CShapeGroup; }
            set
            {
                _CShapeGroup = value;
                UpdateView();
                RaisePropertyChanged("CShapeGroup");
            }
        }
        #endregion

        #region MCShapeGroup Property
        private string _MCShapeGroup;
        public string MCShapeGroup
        {
            get { return _MCShapeGroup; }
            set
            {
                _MCShapeGroup = value;
                UpdateView();
                RaisePropertyChanged("MCShapeGroup");
            }
        }
        #endregion
        #endregion

        #region Tees



        #region TShapeTypeProperty


        //String backup property for backend and storing parameter data
        private string _TShapeType;


        public string TShapeType
        {
            get { return _TShapeType; }
            set //No validation is performed on Enum
            {
                _TShapeType = value;
                RaisePropertyChanged("TShapeType");
                UpdateView();
            }
        }

        #endregion


        #region WTShapeGroup Property
        private string _WTShapeGroup;
        public string WTShapeGroup
        {
            get { return _WTShapeGroup; }
            set
            {
                _WTShapeGroup = value;
                UpdateView();
                RaisePropertyChanged("WTShapeGroup");
            }
        }
        #endregion


        #region MTShapeGroup Property
        private string _MTShapeGroup;
        public string MTShapeGroup
        {
            get { return _MTShapeGroup; }
            set
            {
                _MTShapeGroup = value;
                UpdateView();
                RaisePropertyChanged("MTShapeGroup");
            }
        }
        #endregion


        #region STShapeGroup Property
        private string _STShapeGroup;
        public string STShapeGroup
        {
            get { return _STShapeGroup; }
            set
            {
                _STShapeGroup = value;
                UpdateView();
                RaisePropertyChanged("STShapeGroup");
            }
        }
        #endregion
        #endregion

        #region Angles



        #region LShapeGroup Property
        private string _LShapeGroup;
        public string LShapeGroup
        {
            get { return _LShapeGroup; }
            set
            {
                _LShapeGroup = value;
                UpdateView();
                RaisePropertyChanged("LShapeGroup");
            }
        }
        #endregion

        #endregion

        #region DoubleAngles


        #region LDoubleShapeTypeProperty


        private string _LDoubleShapeType;


        public string LDoubleShapeType
        {
            get { return _LDoubleShapeType; }
            set //No validation is performed on Enum
            {
                _LDoubleShapeType = value; //send string version of value for parameter storing
                RaisePropertyChanged("LDoubleShapeType");
                UpdateView();
            }
        }

        #endregion


        #region LDoubleEqualGroup Property
        private string _LDoubleEqualGroup;
        public string LDoubleEqualGroup
        {
            get { return _LDoubleEqualGroup; }
            set
            {
                _LDoubleEqualGroup = value;
                UpdateView();
                RaisePropertyChanged("LDoubleEqualGroup");
            }
        }
        #endregion


        #region LDoubleLLBBGroup Property
        private string _LDoubleLLBBGroup;
        public string LDoubleLLBBGroup
        {
            get { return _LDoubleLLBBGroup; }
            set
            {
                _LDoubleLLBBGroup = value;
                UpdateView();
                RaisePropertyChanged("LDoubleLLBBGroup");
            }
        }
        #endregion


        #region LDoubleSLBBGroup Property
        private string _LDoubleSLBBGroup;
        public string LDoubleSLBBGroup
        {
            get { return _LDoubleSLBBGroup; }
            set
            {
                _LDoubleSLBBGroup = value;
                UpdateView();
                RaisePropertyChanged("LDoubleSLBBGroup");
            }
        }
        #endregion


        #region LDoubleGapType Property
        private string _LDoubleGapType;
        public string LDoubleGapType
        {
            get { return _LDoubleGapType; }
            set
            {
                _LDoubleGapType = value;
                UpdateView();
                RaisePropertyChanged("LDoubleGapType");
            }
        }
        #endregion

        #endregion

        #region Tubes


        #region RHSShapeGroup Property
        private string _RHSShapeGroup;
        public string RHSShapeGroup
        {
            get { return _RHSShapeGroup; }
            set
            {
                _RHSShapeGroup = value;
                UpdateView();
                RaisePropertyChanged("RHSShapeGroup");
            }
        }
        #endregion

        #endregion

        #region Pipes


        #region CHSType Property


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



        #region CHSShapeGroup Property
        private string _CHSShapeGroup;
        public string CHSShapeGroup
        {
            get { return _CHSShapeGroup; }
            set
            {
                _CHSShapeGroup = value;
                UpdateView();
                RaisePropertyChanged("CHSShapeGroup");
            }
        }
        #endregion

        #region PipeGroup Property
        private string _PipeGroup;
        public string PipeGroup
        {
            get { return _PipeGroup; }
            set
            {
                _PipeGroup = value;
                UpdateView();
                RaisePropertyChanged("PipeGroup");
            }
        }
        #endregion





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

        #region Output parameters


        //#region A Property
        //private double _A;
        //public double A
        //{
        //    get { return _A; }
        //    set
        //    {
        //        _A = value;
        //    }
        //}


        //#endregion

        //#region d Property
        //private double _d;

        //public double d
        //{
        //    get { return _d; }
        //    set
        //    {
        //        _d = value;


        //    }
        //}
        //#endregion

        //#region Ht Property
        //private double _Ht;

        //public double Ht
        //{
        //    get { return _Ht; }
        //    set
        //    {
        //        _Ht = value;


        //    }
        //}
        //#endregion

        //#region D Property
        //private double _D;

        //public double D
        //{
        //    get { return _D; }
        //    set
        //    {
        //        _D = value;


        //    }
        //}
        //#endregion

        //#region bf Property
        //private double _bf;

        //public double bf
        //{
        //    get { return _bf; }
        //    set
        //    {
        //        _bf = value;

        //    }
        //}
        //#endregion

        //#region B Property
        //private double _B;

        //public double B
        //{
        //    get { return _B; }
        //    set
        //    {
        //        _B = value;
        //    }
        //}
        //#endregion

        //#region b Property
        //private double _b;

        //public double b
        //{
        //    get { return _b; }
        //    set
        //    {
        //        _b = value;
        //    }
        //}
        //#endregion

        //#region tw Property
        //private double _tw;

        //public double tw
        //{
        //    get { return _tw; }
        //    set
        //    {
        //        _tw = value;
        //    }
        //}
        //#endregion

        //#region tf Property
        //private double _tf;

        //public double tf
        //{
        //    get { return _tf; }
        //    set
        //    {
        //        _tf = value;
        //    }
        //}
        //#endregion

        //#region tnom Property
        //private double _tnom;

        //public double tnom
        //{
        //    get { return _tnom; }
        //    set
        //    {
        //        _tnom = value;
        //    }
        //}
        //#endregion

        //#region t Property
        //private double _t;

        //public double t
        //{
        //    get { return _t; }
        //    set
        //    {
        //        _t = value;
        //    }
        //}
        //#endregion

        //#region x Property
        //private double _x;

        //public double x
        //{
        //    get { return _x; }
        //    set
        //    {
        //        _x = value;
        //    }
        //}
        //#endregion

        //#region y Property
        //private double _y;

        //public double y
        //{
        //    get { return _y; }
        //    set
        //    {
        //        _y = value;

        //    }
        //}
        //#endregion

        //#region eo Property
        //private double _eo;

        //public double eo
        //{
        //    get { return _eo; }
        //    set
        //    {
        //        _eo = value;
        //    }
        //}
        //#endregion

        //#region xp Property
        //private double _xp;

        //public double xp
        //{
        //    get { return _xp; }
        //    set
        //    {
        //        _xp = value;
        //    }
        //}
        //#endregion

        //#region yp Property
        //private double _yp;

        //public double yp
        //{
        //    get { return _yp; }
        //    set
        //    {
        //        _yp = value;

        //    }
        //}
        //#endregion

        //#region X-axis

        //#region Ix Property
        //private double _Ix;

        //public double Ix
        //{
        //    get { return _Ix; }
        //    set
        //    {
        //        _Ix = value;
        //    }
        //}
        //#endregion


        //#region Zx Property
        //private double _Zx;

        //public double Zx
        //{
        //    get { return _Zx; }
        //    set
        //    {
        //        _Zx = value;
        //    }
        //}
        //#endregion


        //#region Sx Property
        //private double _Sx;

        //public double Sx
        //{
        //    get { return _Sx; }
        //    set
        //    {
        //        _Sx = value;
        //    }
        //}
        //#endregion


        //#region rx Property
        //private double _rx;

        //public double rx
        //{
        //    get { return _rx; }
        //    set
        //    {
        //        _rx = value;
        //    }
        //}
        //#endregion


        //#endregion

        //#region Y-axis


        //#region Iy Property
        //private double _Iy;

        //public double Iy
        //{
        //    get { return _Iy; }
        //    set
        //    {
        //        _Iy = value;
        //    }
        //}
        //#endregion


        //#region Zy Property
        //private double _Zy;

        //public double Zy
        //{
        //    get { return _Zy; }
        //    set
        //    {
        //        _Zy = value;
        //    }
        //}
        //#endregion


        //#region Sy Property
        //private double _Sy;

        //public double Sy
        //{
        //    get { return _Sy; }
        //    set
        //    {
        //        _Sy = value;
        //    }
        //}
        //#endregion


        //#region ry Property
        //private double _ry;

        //public double ry
        //{
        //    get { return _ry; }
        //    set
        //    {
        //        _ry = value;
        //    }
        //}
        //#endregion

        //#endregion

        //#region Z-axis

        //#region Iz Property
        //private double _Iz;

        //public double Iz
        //{
        //    get { return _Iz; }
        //    set
        //    {
        //        _Iz = value;

        //    }
        //}
        //#endregion


        //#region Sz Property
        //private double _Sz;

        //public double Sz
        //{
        //    get { return _Sz; }
        //    set
        //    {
        //        _Sz = value;
        //    }
        //}
        //#endregion


        //#region rz Property
        //private double _rz;

        //public double rz
        //{
        //    get { return _rz; }
        //    set
        //    {
        //        _rz = value;
        //    }
        //}
        //#endregion


        //#endregion

        //#region Torsional properties


        //#region J Property
        //private double _J;

        //public double J
        //{
        //    get { return _J; }
        //    set
        //    {
        //        _J = value;

        //    }
        //}
        //#endregion


        //#region Cw Property
        //private double _Cw;

        //public double Cw
        //{
        //    get { return _Cw; }
        //    set
        //    {
        //        _Cw = value;
        //    }
        //}
        //#endregion

        //#endregion

        #endregion

        #region Display parameters



        private void FetchList(string ResourceFileName, string FilterCriteriaString, string ShapeGapString = null)
        {
            if (FilterCriteriaString != null && ResourceFileName != null)
            {

                if (AvailableShapes ==null)
                {
                    AvailableShapes = new ObservableCollection<string>();
                }
                else
                {
                    AvailableShapes.Clear();
                }
                

                //Replace "_" and "I" in the filter criteria
                string FilterCriteria1 = FilterCriteriaString.Replace("_", "-");
                string FilterCriteria2 = FilterCriteria1.Replace("I", "/");
                string FilterCriteria3 = FilterCriteria2.Replace("z", ".");
                string FilterCriteria = FilterCriteria3.Replace("Angle", "");

                string Gap = null;

                if (ShapeGapString != null)
                {

                    string Gap1 = ShapeGapString.Replace("_", "-");
                    string Gap2 = Gap1.Replace("I", "/");
                    string Gap3 = Gap2.Replace("z", ".");
                    Gap = Gap3.Replace("Angle", "");
                }



                //string UriString = string.Format("pack://application:,,,/Resources/{0}.txt", ResourceFileName);
                //StreamResourceInfo sri = Application.GetResourceStream(new Uri(UriString));

                string resourceName = string.Format("KodestructDynamoUI.Resources.{0}.txt", ResourceFileName);
                var assembly = Assembly.GetExecutingAssembly();
   
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    string line;
                    //using (TextReader tr = new StreamReader(sri.Stream))
                    using (TextReader tr = new StreamReader(stream))
                    {
                        List<string> AllShapes = new List<string>();
                        while ((line = tr.ReadLine()) != null)
                        {
                            //AvailableShapes.Add(line);
                            AllShapes.Add(line);
                        }

                        if (FilterCriteria != null)
                        {
                            if (Gap == null) //no shape gap
                            {
                                if (FilterCriteria.Contains("Pipe"))
                                {
                                    var FilteredList = AllShapes.Where(sh =>
                                    {
                                        if (sh.Contains(FilterCriteria))
                                        {
                                            return true;
                                        }
                                        else
                                        {
                                            return false;
                                        }
                                    }
                                    ).ToList();
                                    foreach (var s in FilteredList)
                                    {
                                        AvailableShapes.Add(s);
                                    }
                                }
                                else
                                {

                                    var FilteredList = AllShapes.Where(sh =>
                                    {
                                        string[] subsStr = sh.Split(new string[] { "X" }, StringSplitOptions.None);
                                        if (subsStr[0] == FilterCriteria)
                                        {
                                            return true;
                                        }
                                        else
                                        {
                                            return false;
                                        }
                                        //sh.Contains(FilterCriteria)
                                    }
                                         ).ToList();
                                    foreach (var s in FilteredList)
                                    {
                                        AvailableShapes.Add(s);
                                    }
                                }
                            }
                            else //if gap is defined
                            {
                                var FilteredListWithGap = AllShapes.Where(sh =>
                                {
                                    string[] subsStr = sh.Split(new string[] { "X" }, StringSplitOptions.None);
                                    if (subsStr[0] == FilterCriteria)
                                    {
                                        if (subsStr.Length == 3) //no gap is defined
                                        {
                                            return false;
                                        }
                                        else
                                        {
                                            if (subsStr.Length == 4)
                                            {
                                                string GapStr = "";
                                                if (subsStr[3].Contains("SLBB") || subsStr[3].Contains("LLBB"))
                                                {
                                                    GapStr = subsStr[3].Substring(0, subsStr[3].Length - 4);
                                                }
                                                else
                                                {
                                                    GapStr = subsStr[3];
                                                }
                                                if (GapStr == Gap)
                                                {
                                                    return true;
                                                }
                                                else
                                                {
                                                    return false;
                                                }
                                            }
                                            else
                                            {
                                                return false;
                                            }
                                        }

                                    }
                                    else
                                    {
                                        return false;
                                    }
                                    //sh.Contains(FilterCriteria)
                                }
                                ).ToList();
                                foreach (var s in FilteredListWithGap)
                                {
                                    AvailableShapes.Add(s);
                                }
                            }
                        }
                        else
                        {
                            foreach (var s in AllShapes)
                            {
                                AvailableShapes.Add(s);
                            }
                        }
                    }
                }
            }
        }

        #region AvailableShapes Property
        private ObservableCollection<string> availableShapes;
        public ObservableCollection<string> AvailableShapes
        {
            get { return availableShapes; }
            set
            {
                availableShapes = value;
                RaisePropertyChanged("AvailableShapes");
            }
        }
        #endregion


        void UpdateView()
        {
            if (RefreshView == true)
            {


                ClearAllIsShapeProperties();
                switch (CatalogShapeType)
                {
                    case "IShape":
                        IsShapeI = true;
                        ShapeTypeSteel = "IShapeRolled";
                        UpdateViewShapeI();
                        break;
                    case "Channel":
                        IsShapeChannel = true;
                        ShapeTypeSteel = "Channel";
                        UpdateViewShapeChannel();
                        break;
                    case "Angle":
                        IsShapeAngle = true;
                        ShapeTypeSteel = "Angle";
                        UpdateViewShapeAngle();
                        break;
                    case "Tee":
                        ShapeTypeSteel = "TeeRolled";
                        IsShapeTee = true;
                        UpdateViewShapeTee();
                        break;
                    case "DoubleAngle":
                        IsShapeDoubleAngle = true;
                        ShapeTypeSteel = "DoubleAngle";
                        UpdateViewShapeDoubleAngle();
                        break;
                    case "CircularHSS":
                        IsShapeCHS = true;
                        ShapeTypeSteel = "CircularHSS";
                        UpdateViewShapePipe();
                        break;
                    case "RectangularHSS":
                        IsShapeTube = true;
                        ShapeTypeSteel = "RectangularHSS";
                        UpdateViewShapeTube();
                        break;

                }
                SetDefaultParameterValuesForGivenShapeSeries();
            }
        }

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
                if (p.PropertyType == typeof(string))
                {
                    if (p.Name.Contains("Type"))
                    {
                        //ShowOrHideParameterInParameterViewer(p.Name, true);
                    }
                }
            }

        }


        #region Shape specific update methods

        void UpdateViewShapeI()
        {

            //ShowOrHideParameterInParameterViewer("IShapeType", false);
            switch (IShapeType)
            {
                case "W":
                    IsShapeW = true;
                    FetchList("WShapes", WShapeGroup);
                    break;
                case "S":
                    IsShapeS = true;
                    FetchList("SShapes", SShapeGroup);
                    break;
                case "M":
                    IsShapeM = true;
                    FetchList("MShapes", MShapeGroup);
                    break;
            }
        }
        private void UpdateViewShapeChannel()
        {
            //ShowOrHideParameterInParameterViewer("CShapeType", false);
            switch (CShapeType)
            {
                case "C":
                    IsShapeC = true;
                    FetchList("CShapes", CShapeGroup);
                    break;
                case "MC":
                    IsShapeMC = true;
                    FetchList("MCShapes", MCShapeGroup);
                    break;
            }

        }


        private void UpdateViewShapeTube()
        {

            IsShapeTube = true;
            FetchList("RHS_Shapes", RHSShapeGroup);


        }

        private void UpdateViewShapePipe()
        {
            //ShowOrHideParameterInParameterViewer("CHSType", false);
            switch (CHSType)
            {
                case "CHS":
                    IsShapeCircularHSS = true;
                    FetchList("CHS_Shapes", CHSShapeGroup.ToString());
                    break;
                case "Pipe":
                    IsShapePipe = true;
                    FetchList("PipeShapes", PipeGroup.ToString());
                    break;
            }

        }

        private void UpdateViewShapeDoubleAngle()
        {
            //ShowOrHideParameterInParameterViewer("LDoubleShapeType", false);

            string GapString = null;
            if (LDoubleGapType != "NoGap")
            {
                GapString = LDoubleGapType.ToString();
            }
            switch (LDoubleShapeType)
            {
                case "Equal":
                    IsShapeAngleEqualLeg = true;
                    FetchList("L2_Equal", LDoubleEqualGroup.ToString(), GapString);
                    break;
                case "SLBB":
                    IsShapeAngleSLBB = true;
                    FetchList("L2_SLBB", LDoubleSLBBGroup.ToString(), GapString);
                    break;
                case "LLBB":
                    IsShapeAngleLLBB = true;
                    FetchList("L2_LLBB", LDoubleLLBBGroup.ToString(), GapString);
                    break;
            }
        }

        private void UpdateViewShapeTee()
        {
            //ShowOrHideParameterInParameterViewer("TShapeType", false);

            switch (TShapeType)
            {
                case "WT":
                    IsShapeWT = true;
                    FetchList("WTShapes", WTShapeGroup.ToString());
                    break;
                case "MT":
                    IsShapeMT = true;
                    FetchList("MTShapes", MTShapeGroup.ToString());
                    break;
                case "ST":
                    IsShapeST = true;
                    FetchList("STShapes", STShapeGroup.ToString());
                    break;

            }
        }

        private void UpdateViewShapeAngle()
        {
            FetchList("LShapes", LShapeGroup.ToString());

        }

        #endregion


        #region I-Shapes

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

        #region IsShapeW Property
        private bool _IsShapeW;
        public bool IsShapeW
        {
            get { return _IsShapeW; }
            set { _IsShapeW = value; RaisePropertyChanged("IsShapeW"); }
        }
        #endregion

        #region IsShapeS Property
        private bool _IsShapeS;
        public bool IsShapeS
        {
            get { return _IsShapeS; }
            set { _IsShapeS = value; RaisePropertyChanged("IsShapeS"); }
        }
        #endregion

        #region IsShapeM Property
        private bool _IsShapeM;
        public bool IsShapeM
        {
            get { return _IsShapeM; }
            set { _IsShapeM = value; RaisePropertyChanged("IsShapeM"); }
        }
        #endregion



        #endregion

        #region Channels


        #region IsShapeChannel Property
        private bool _IsShapeChannel;
        public bool IsShapeChannel
        {
            get { return _IsShapeChannel; }
            set { _IsShapeChannel = value; RaisePropertyChanged("IsShapeChannel"); }
        }
        #endregion


        #region IsShapeC Property
        private bool _IsShapeC;
        public bool IsShapeC
        {
            get { return _IsShapeC; }
            set { _IsShapeC = value; RaisePropertyChanged("IsShapeC"); }
        }
        #endregion


        #region IsShapeMC Property
        private bool _IsShapeMC;
        public bool IsShapeMC
        {
            get { return _IsShapeMC; }
            set { _IsShapeMC = value; RaisePropertyChanged("IsShapeMC"); }
        }
        #endregion

        #endregion

        #region Tees


        #region IsShapeTee Property
        private bool _IsShapeTee;
        public bool IsShapeTee
        {
            get { return _IsShapeTee; }
            set { _IsShapeTee = value; RaisePropertyChanged("IsShapeTee"); }
        }
        #endregion

        #region IsShapeWT Property
        private bool _IsShapeWT;
        public bool IsShapeWT
        {
            get { return _IsShapeWT; }
            set { _IsShapeWT = value; RaisePropertyChanged("IsShapeWT"); }
        }
        #endregion

        #region IsShapeMT Property
        private bool _IsShapeMT;
        public bool IsShapeMT
        {
            get { return _IsShapeMT; }
            set { _IsShapeMT = value; RaisePropertyChanged("IsShapeMT"); }
        }
        #endregion

        #region IsShapeST Property
        private bool _IsSHapeST;
        public bool IsShapeST
        {
            get { return _IsSHapeST; }
            set { _IsSHapeST = value; RaisePropertyChanged("IsShapeST"); }
        }
        #endregion

        #endregion

        #region Angles

        #region IsShapeAngle Property
        private bool _IsShapeAngle;
        public bool IsShapeAngle
        {
            get { return _IsShapeAngle; }
            set { _IsShapeAngle = value; RaisePropertyChanged("IsShapeAngle"); }
        }
        #endregion



        #endregion

        #region DoubleAngles


        #region IsShapeDoubleAngle Property
        private bool _IsShapeDoubleAngle;
        public bool IsShapeDoubleAngle
        {
            get { return _IsShapeDoubleAngle; }
            set { _IsShapeDoubleAngle = value; RaisePropertyChanged("IsShapeDoubleAngle"); }
        }
        #endregion

        #region IsShapeAngleEqualLeg Property
        private bool _IsAngleEqualLeg;
        public bool IsShapeAngleEqualLeg
        {
            get { return _IsAngleEqualLeg; }
            set { _IsAngleEqualLeg = value; RaisePropertyChanged("IsShapeAngleEqualLeg"); }
        }
        #endregion

        #region IsShapeAngleLLBB Property
        private bool _IsAngleLLBB;
        public bool IsShapeAngleLLBB
        {
            get { return _IsAngleLLBB; }
            set { _IsAngleLLBB = value; RaisePropertyChanged("IsShapeAngleLLBB"); }
        }
        #endregion

        #region IsShapeAngleSLBB Property
        private bool _IsAngleSLBB;
        public bool IsShapeAngleSLBB
        {
            get { return _IsAngleSLBB; }
            set { _IsAngleSLBB = value; RaisePropertyChanged("IsShapeAngleSLBB"); }
        }
        #endregion

        #endregion

        #region Tubes


        #region IsShapeTube Property
        private bool _IsShapeTube;
        public bool IsShapeTube
        {
            get { return _IsShapeTube; }
            set { _IsShapeTube = value; RaisePropertyChanged("IsShapeTube"); }
        }
        #endregion

        #endregion

        #region Pipes


        #region IsShapeCHS Property
        private bool _IsShapeCHS;
        public bool IsShapeCHS
        {
            get { return _IsShapeCHS; }
            set { _IsShapeCHS = value; RaisePropertyChanged("IsShapeCHS"); }
        }
        #endregion

        #region IsShapePipe Property
        private bool _IsShapePipe;
        public bool IsShapePipe
        {
            get { return _IsShapePipe; }
            set { _IsShapePipe = value; RaisePropertyChanged("IsShapePipe"); }
        }
        #endregion


        #region IsShapeCircularHSS Property
        private bool _IsShapeCircularHSS;
        public bool IsShapeCircularHSS
        {
            get { return _IsShapeCircularHSS; }
            set { _IsShapeCircularHSS = value; RaisePropertyChanged("IsShapeCircularHSS"); }
        }
        #endregion

        #endregion

        #endregion

 

        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class AiscShapeSelectionViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<AiscShapeSelection>
        {
            public void CustomizeView(AiscShapeSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                AiscShapeSelectionView control = new AiscShapeSelectionView();
                control.DataContext = model;
                
               
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

        }
    }
}
