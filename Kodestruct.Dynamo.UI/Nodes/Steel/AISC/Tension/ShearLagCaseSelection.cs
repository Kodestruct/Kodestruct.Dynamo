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
using Kodestruct.Dynamo.Common.Infra.TreeItems;
using System.Xml;
using System.Windows;
using Kodestruct.Dynamo.UI.Common.TreeItems;
using System.Reflection;
using Dynamo.Nodes;
using Dynamo.Graph;
using Dynamo.Graph.Nodes;
using Newtonsoft.Json;

namespace Kodestruct.Steel.AISC.Tension
{

    /// <summary>
    ///Selection of occupancy for determination of Risk Category  
    /// </summary>

    [NodeName("Shear lag case selection")]
    [NodeCategory("Kodestruct.Steel.AISC.Tension")]
    [NodeDescription("Selection of case for calculation of shear lag factor")]
    [IsDesignScriptCompatible]
    public class ShearLagCaseIdSelection : UiNodeBase
    {
        [JsonConstructor]
        public ShearLagCaseIdSelection(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
        {

        }
        public ShearLagCaseIdSelection()
        {
            ShearLagCaseIdDescription ="General case";
            ShearLagCaseId = "Case2";
            //InPorts.Add(new PortModel(PortType.Input, this, new PortData("Port Name", "Port Description")));
            OutPorts.Add(new PortModel(PortType.Output, this, new PortData("ShearLagCaseId",  "Case of shear lag condition")));
            RegisterAllPorts();
            //PropertyChanged += NodePropertyChanged;
        }

        #region ShearLagCaseIdDescription Property
        private string _ShearLagCaseIdDescription;
        public string ShearLagCaseIdDescription
        {
            get { return _ShearLagCaseIdDescription; }
            set
            {
                _ShearLagCaseIdDescription = value;
                RaisePropertyChanged("ShearLagCaseIdDescription");
            }
        }
        #endregion

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

		#region ShearLagCaseIdProperty
		
		/// <summary>
		/// ShearLagCaseId property
		/// </summary>
		/// <value>Occupancy description</value>
		public string _ShearLagCaseId;
		
		public string ShearLagCaseId
		{
		    get { return _ShearLagCaseId; }
		    set
		    {
		        _ShearLagCaseId = value;
                RaisePropertyChanged("ShearLagCaseId");
		        OnNodeModified(true); 
		    }
		}
		#endregion

        #region lProperty

        /// <summary>
        /// l property
        /// </summary>
        /// <value>Connection length</value>
        public double _l;

        public double l
        {
            get { return _l; }
            set
            {
                _l = value;
                RaisePropertyChanged("l");
                OnNodeModified(true); 
            }
        }
        #endregion

        #region x_barProperty

        /// <summary>
        /// x_bar property
        /// </summary>
        /// <value>Element eccentricity</value>
        public double _x_bar;

        public double x_bar
        {
            get { return _x_bar; }
            set
            {
                _x_bar = value;
                RaisePropertyChanged("x_bar");
                OnNodeModified(true); 
            }
        }
        #endregion

        #region wProperty

        /// <summary>
        /// w property
        /// </summary>
        /// <value>Width of plate</value>
        public double _w;

        public double w
        {
            get { return _w; }
            set
            {
                _w = value;
                RaisePropertyChanged("w");
                OnNodeModified(true); 
            }
        }
        #endregion

        #region BProperty

        /// <summary>
        /// B property
        /// </summary>
        /// <value>HSS width</value>
        public double _B;

        public double B
        {
            get { return _B; }
            set
            {
                _B = value;
                RaisePropertyChanged("B");
                OnNodeModified(true); 
            }
        }
        #endregion

        #region HProperty

        /// <summary>
        /// H property
        /// </summary>
        /// <value>HSS height</value>
        public double _H;

        public double H
        {
            get { return _H; }
            set
            {
                _H = value;
                RaisePropertyChanged("H");
                OnNodeModified(true); 
            }
        }
        #endregion

        #region b_fProperty

        /// <summary>
        /// b_f property
        /// </summary>
        /// <value>Flange width</value>
        public double _b_f;

        public double b_f
        {
            get { return _b_f; }
            set
            {
                _b_f = value;
                RaisePropertyChanged("b_f");
                OnNodeModified(true); 
            }
        }
        #endregion

        #region dProperty

        /// <summary>
        /// d property
        /// </summary>
        /// <value>Member height</value>
        public double _d;

        public double d
        {
            get { return _d; }
            set
            {
                _d = value;
                RaisePropertyChanged("d");
                OnNodeModified(true); 
            }
        }
        #endregion


        #endregion
        #endregion


        #region treeView elements
        [JsonIgnore]
        public TreeView TreeViewControl { get; set; }




        public void DisplayComponentUI(XTreeItem selectedComponent)
        {
            if (selectedComponent != null && selectedComponent.Tag != "X" && selectedComponent.Id != null)
            {
                Assembly execAssembly = Assembly.GetExecutingAssembly();
                AssemblyName assemblyName = new AssemblyName(execAssembly.FullName);
                string execAssemblyName = assemblyName.Name;
                string UIPath = GetUIControlName(selectedComponent.Id);
                string typeStr = execAssemblyName + ".Views.AISC.Tension." + UIPath;
                try
                {
                    Type subMenuType = execAssembly.GetType(typeStr);
                    UserControl subMenu = (UserControl)Activator.CreateInstance(subMenuType);
                    AdditionalUI = subMenu;

                    if (selectedComponent.Id != "X") //set default values
                    {
                        SetDefaultValues(selectedComponent.Id);
                       
                    }

                }
                catch (Exception)
                {

                    AdditionalUI = null;
                }
            }
            else
            {
                AdditionalUI = null;
            }
          
        }

        private void SetDefaultValues(string Id)
        {
            switch (Id)
            {
                case "Case1":
                    l = 0;
                    x_bar = 0;
                    w = 0;
                    B = 0;
                    H = 0;
                    b_f = 0;
                    d = 0;
                    break;
                case "Case2":
                    l = 0;
                    x_bar = 0;
                    w = 0;
                    B = 0;
                    H = 0;
                    b_f = 0;
                    d = 0;
                    break;
                case "Case3":
                    l = 0;
                    x_bar = 0;
                    w = 0;
                    B = 0;
                    H = 0;
                    b_f = 0;
                    d = 0;
                    break;
                case "Case4":
                    l = 0;
                    x_bar = 0;
                    w = 0;
                    B = 0;
                    H = 0;
                    b_f = 0;
                    d = 0;
                    break;
                case "Case5":
                    l = 0;
                    x_bar = 0;
                    w = 0;
                    B = 0;
                    H = 0;
                    b_f = 0;
                    d = 0;
                    break;
                case "Case6":
                    l = 0;
                    x_bar = 0;
                    w = 0;
                    B = 0;
                    H = 0;
                    b_f = 0;
                    d = 0;
                    break;
                case "Case7a":
                    l = 0;
                    x_bar = 0;
                    w = 0;
                    B = 0;
                    H = 0;
                    b_f = 0;
                    d = 0;
                    break;
                case "Case7b":
                    l = 0;
                    x_bar = 0;
                    w = 0;
                    B = 0;
                    H = 0;
                    b_f = 0;
                    d = 0;
                    break;
                case "Case8a":
                    l = 0;
                    x_bar = 0;
                    w = 0;
                    B = 0;
                    H = 0;
                    b_f = 0;
                    d = 0;
                    break;
                case "Case8b":
                    l = 0;
                    x_bar = 0;
                    w = 0;
                    B = 0;
                    H = 0;
                    b_f = 0;
                    d = 0;
                    break;

            }
        }

        private string GetUIControlName(string Id)
        {
            string ControlName = null;
            switch (Id)
            {
                case "Case2":
                    ControlName = "ShearLagCase2Control";
                    break;
                case "Case4":
                    ControlName = "ShearLagCase4Control";
                    break;
                case "Case5":
                    ControlName = "ShearLagCase5Control";
                    break;
                case "Case6":
                    ControlName = "ShearLagCase6Control";
                    break;
                case "Case7a":
                    ControlName = "ShearLagCase7Control";
                    break;
                case "Case7b":
                    ControlName = "ShearLagCase7Control";
                    break;
                default:
                    ControlName = null;
                    break;
            }
            if (ControlName!=null)
            {
                ControlName = ControlName + ".xaml";
            }
            return ControlName;
        }


        private XTreeItem selectedItem;
        [JsonIgnore]
        public XTreeItem SelectedItem
        {
            get { return selectedItem; }
            set 
            { 
                
                selectedItem = value; 
            }
        }


        private void OnSelectedItemChanged(object i)
        {
            XmlElement item = i as XmlElement;
            XTreeItem xtreeItem = new XTreeItem()
            {
                Header = item.GetAttribute("Header"),
                Description = item.GetAttribute("Description"),
                Id = item.GetAttribute("Id"),
                ResourcePath = item.GetAttribute("ResourcePath"),
                Tag = item.GetAttribute("Tag"),
                TemplateName = item.GetAttribute("TemplateName")
            };

            if (item != null)
            {


                string Id = xtreeItem.Id;
                if (Id != "X")
                {
                    ShearLagCaseId = xtreeItem.Id;
                    ShearLagCaseIdDescription = xtreeItem.Description;
                    SelectedItem = xtreeItem;
                    //DisplayComponentUI(xtreeItem);
                }
            }

        }



        #endregion

        //Additional UI is a user control which is based on the user selection
        // can remove this property

        #region Additional UI
        private UserControl additionalUI;
        [JsonIgnore]
        public UserControl AdditionalUI
        {
            get { return additionalUI; }
            set
            {
                additionalUI = value;
                RaisePropertyChanged("AdditionalUI");
            }
        }
        #endregion




        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class ShearLagCaseIdViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<ShearLagCaseIdSelection>
        {
            public void CustomizeView(ShearLagCaseIdSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                ShearLagFactorIdView control = new ShearLagFactorIdView();
                control.DataContext = model;
                
                TreeView tv = control.FindName("selectionTree") as TreeView;
                if (tv!=null)
                {
                    model.TreeViewControl = tv;
                    //model.UpdateSelectionEvents();
                }
                
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

        }
    }
}
