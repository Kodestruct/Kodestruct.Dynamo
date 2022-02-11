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
using System.IO;
using System.Reflection;
using System.Linq;
using MoreLinq;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace Kodestruct.Aluminum.AA.Material.MaterialParameters
{

    /// <summary>
    ///Alumimum material selection (alloy, temper, product and thickness range 
    /// </summary>

    [NodeName("Alumimum material selection")]
    [NodeCategory("Kodestruct.Aluminum.AA.Material.MaterialParameters")]
    [NodeDescription("Alumimum material selection (alloy, temper, product and thickness range")]
    [IsDesignScriptCompatible]
    public class AluminumMaterialSelection : UiNodeBase
    {

        [JsonConstructor]
        public AluminumMaterialSelection(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
        {

        }
        public AluminumMaterialSelection()
        {

            //InPorts.Add(new PortModel(PortType.Input, this, new PortData("Port Name", "Port Description")));
            OutPorts.Add(new PortModel(PortType.Output, this, new PortData("AluminumAlloyId", "Aluminum alloy")));
            OutPorts.Add(new PortModel(PortType.Output, this, new PortData("AluminumTemperId", "Aluminum temper")));
            OutPorts.Add(new PortModel(PortType.Output, this, new PortData("AluminumProductId", "Aluminum product type")));
            OutPorts.Add(new PortModel(PortType.Output, this, new PortData("ThicknessRangeId", "Range of aluminum material thicknesses")));

            ReadMaterialInfo();
            SetDefaultParameters();
            RegisterAllPorts();
           
            
            //PropertyChanged += NodePropertyChanged;
        }

        List<MaterialInfo> MaterialInfoList;

        private void ReadMaterialInfo()
        {
            MaterialInfoList = new List<MaterialInfo>();

            List<string> AllMaterialData = new List<string>();
            #region Read Material Data


            string ResourceFileName = "AAMaterialPropertiesTableA3_3_3";
             string resourceName = string.Format("KodestructDynamoUI.Resources.{0}.txt", ResourceFileName);
                var assembly = Assembly.GetExecutingAssembly();
   
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    string line;
                    //using (TextReader tr = new StreamReader(sri.Stream))
                    using (TextReader tr = new StreamReader(stream))
                    {
                        
                        while ((line = tr.ReadLine()) != null)
                        {
                            AllMaterialData.Add(line);
                        }
                    }
                }

                foreach (var m in AllMaterialData)
                {
                        string[] Vals = m.Split(',');
                        if (Vals.Length == 4)
                        {

                            string _Alloy = (string)Vals[0];
                            string _Temper = (string)Vals[1];
                            string _ThicknessRange = (string)Vals[2];
                            string _Product = (string)Vals[3];

                            MaterialInfoList.Add(new MaterialInfo(_Alloy, _Temper, _Product, _ThicknessRange));

                        }
                }



            #endregion
        
        }

           

        

        private void SetDefaultParameters()
        {
            //ReportEntry="";
            AvailableAlloys = new ObservableCollection<string>();
            AvailableTempers = new ObservableCollection<string>();
            AvailableProducts = new ObservableCollection<string>();
            AvailableThicknessRanges = new ObservableCollection<string>();

            AluminumAlloyId ="6063";
            AluminumTemperId="T6";
            AluminumProductId ="extrusion";
            ThicknessRangeId = "Up to 1";


            AvailableAlloys = new ObservableCollection<string>(MaterialInfoList.DistinctBy(m => m.Alloy).Select(s => s.Alloy).ToList());

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

		#region AluminumAlloyIdProperty
		
		/// <summary>
		/// AluminumAlloyId property
		/// </summary>
		/// <value>Aluminum alloy</value>
		public string _AluminumAlloyId;
		
		public string AluminumAlloyId
		{
		    get { return _AluminumAlloyId; }
		    set
		    {
		        _AluminumAlloyId = value;
		        RaisePropertyChanged("AluminumAlloyId");
                UpdateAvalableTempers();
		        OnNodeModified();
		    }
		}

        private void UpdateAvalableTempers()
        {

            if (MaterialInfoList!=null)
            {
                var _tempers = MaterialInfoList.Where(m => m.Alloy == _AluminumAlloyId).Select(s => s.Temper).Distinct().ToList();
                if (_tempers!=null)
                {

                        availableTempers = new ObservableCollection<string>(_tempers);
                        AluminumTemperId = _tempers.First();
                        RaisePropertyChanged("AvailableTempers");
                       
                }
                
            }

        }
		#endregion

		#region AluminumTemperIdProperty
		
		/// <summary>
		/// AluminumTemperId property
		/// </summary>
		/// <value>Aluminum temper</value>
		public string _AluminumTemperId;
		
		public string AluminumTemperId
		{
		    get { return _AluminumTemperId; }
		    set
		    {
		        _AluminumTemperId = value;
		        RaisePropertyChanged("AluminumTemperId");
                UpdateAvailableProducts();
		        OnNodeModified();
		    }
		}

        private void UpdateAvailableProducts()
        {

            if (MaterialInfoList != null)
            {
                var _products = MaterialInfoList.Where(m => m.Alloy == _AluminumAlloyId && m.Temper == _AluminumTemperId).Select(s => s.Product).Distinct().ToList();
                if (_products != null)
                {

                    availableProducts = new ObservableCollection<string>(_products);
                    AluminumProductId = _products.First();
                    RaisePropertyChanged("AvailableProducts");

                }

            }

        }
		#endregion

		#region AluminumProductIdProperty
		
		/// <summary>
		/// AluminumProductId property
		/// </summary>
		/// <value>Aluminum product type</value>
		public string _AluminumProductId;
		
		public string AluminumProductId
		{
		    get { return _AluminumProductId; }
		    set
		    {
		        _AluminumProductId = value;
		        RaisePropertyChanged("AluminumProductId");
                UpdateAvailableThicknesses();
		        OnNodeModified();
		    }
		}

        private void UpdateAvailableThicknesses()
        {
            //throw new NotImplementedException();
            if (MaterialInfoList != null)
            {
                var _thicknesses = MaterialInfoList.Where(m => m.Alloy == _AluminumAlloyId && m.Temper == _AluminumTemperId && m.Product == _AluminumProductId).Select(s => s.ThicknessRange).Distinct().ToList();
                if (_thicknesses != null)
                {

                    availableThicknessRanges = new ObservableCollection<string>(_thicknesses);
                    ThicknessRangeId = _thicknesses.First();
                    RaisePropertyChanged("AvailableThicknessRanges");

                }

            }
        }
		#endregion

		#region ThicknessRangeIdProperty
		
		/// <summary>
		/// ThicknessRangeId property
		/// </summary>
		/// <value>Range of aluminum material thicknesses</value>
		public string _ThicknessRangeId;
		
		public string ThicknessRangeId
		{
		    get { return _ThicknessRangeId; }
		    set
		    {
		        _ThicknessRangeId = value;
		        RaisePropertyChanged("ThicknessRangeId");
		        OnNodeModified();
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
                OnNodeModified();
            }
        }




        #endregion

        #endregion

        #region Visibility properties

        #region AvailableAlloys Property
        private ObservableCollection<string> availableAlloys;
        public ObservableCollection<string> AvailableAlloys
        {
            get { return availableAlloys; }
            set
            {
                availableAlloys = value;
                RaisePropertyChanged("AvailableAlloys");
            }
        }
        #endregion

        #region AvailableTempers Property
        private ObservableCollection<string> availableTempers;
        public ObservableCollection<string> AvailableTempers
        {
            get { return availableTempers; }
            set
            {
                availableTempers = value;
                RaisePropertyChanged("AvailableTempers");
            }
        }
        #endregion

        #region AvailableProducts Property
        private ObservableCollection<string> availableProducts;
        public ObservableCollection<string> AvailableProducts
        {
            get { return availableProducts; }
            set
            {
                availableProducts = value;
                RaisePropertyChanged("AvailableProducts");
            }
        }
        #endregion

        #region AvailableThicknessRanges Property
        private ObservableCollection<string> availableThicknessRanges;
        public ObservableCollection<string> AvailableThicknessRanges
        {
            get { return availableThicknessRanges; }
            set
            {
                availableThicknessRanges = value;
                RaisePropertyChanged("AvailableThicknessRanges");
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
            nodeElement.SetAttribute("AluminumAlloyId", AluminumAlloyId);
            nodeElement.SetAttribute("AluminumTemperId", AluminumTemperId);
            nodeElement.SetAttribute("AluminumProductId", AluminumProductId);
            nodeElement.SetAttribute("ThicknessRangeId", ThicknessRangeId);
        }

        /// <summary>
        ///Retrieved property values when opening the node     
        /// </summary>
        protected override void DeserializeCore(XmlElement nodeElement, SaveContext context)
        {
            base.DeserializeCore(nodeElement, context);
            var AluminumAlloyId_attrib = nodeElement.Attributes["AluminumAlloyId"];
            if (AluminumAlloyId_attrib != null) { AluminumAlloyId = AluminumAlloyId_attrib.Value; }

            var AluminumTemperId_attrib = nodeElement.Attributes["AluminumTemperId"];
            if (AluminumTemperId_attrib != null) { AluminumTemperId = AluminumTemperId_attrib.Value; }

            var AluminumProductId_attrib = nodeElement.Attributes["AluminumProductId"];
            if (AluminumProductId_attrib != null) { AluminumProductId = AluminumProductId_attrib.Value; }

            var ThicknessRangeId_attrib = nodeElement.Attributes["ThicknessRangeId"];
            if (ThicknessRangeId_attrib != null) { ThicknessRangeId = ThicknessRangeId_attrib.Value; }

            //base.DeserializeCore(nodeElement, context);
            //var attrib = nodeElement.Attributes["Material"];
            //if (attrib == null)
            //    return;
           
            //MaterialSelection = attrib.Value;


        }



        #endregion



        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class MaterialSelectionViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<AluminumMaterialSelection>
        {
            public void CustomizeView(AluminumMaterialSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                AluminumMaterialSelectionView control = new AluminumMaterialSelectionView();
                control.DataContext = model;
                
               
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

        }

        class MaterialInfo
        {
            public MaterialInfo(string Alloy,string Temper, string Product, string ThicknessRange)
            {

                    this.Alloy 			=Alloy; 			
                    this.Temper 			=Temper; 			
                    this.Product 		=Product;
                    this.ThicknessRange = ThicknessRange;



            }
            public string Alloy { get; set; }
            public string Temper { get; set; }
            public string Product { get; set; }
            public string  ThicknessRange { get; set; }
        }
    }
}
