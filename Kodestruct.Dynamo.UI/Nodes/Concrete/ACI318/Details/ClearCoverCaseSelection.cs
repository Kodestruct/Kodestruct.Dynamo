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
using Kodestruct.Dynamo.Common.Infra.TreeItems;
using System.Xml;
using Dynamo.Graph.Nodes;
using Dynamo.Graph;
using System.Windows;
using Kodestruct.Dynamo.UI.Common.TreeItems;


namespace Kodestruct.Concrete.ACI318.Details
{

    /// <summary>
    ///Rebar material specification selection   
    /// </summary>

    [NodeName("Clear cover Id selection")]
    [NodeCategory("Kodestruct.Concrete.ACI318.Details.Cover")]
    [NodeDescription("Clear cover Id selection")]
    [IsDesignScriptCompatible]
    public class ClearCoverCaseSelection : UiNodeBase
    {

        public ClearCoverCaseSelection()
        {
            
            //OutPortData.Add(new PortData("ReportEntry", "Calculation log entries (for reporting)"));
            OutPortData.Add(new PortData("ClearCoverCaseId", "Concrete member type for clear cover case  selection"));
            RegisterAllPorts();
            SetDefaultParameters();
            //PropertyChanged += NodePropertyChanged;
        }

        private void SetDefaultParameters()
        {
            //ReportEntry="";
            ClearCoverCaseId = "NP-CIP-BEAM-Protected";
            ClearCoverCaseDescription = "";
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

        #region ClearCoverCaseIdProperty

        /// <summary>
        /// ClearCoverCaseId property
		/// </summary>
		/// <value>Concrete member type for clear cover case  selection</value>
        public string _ClearCoverCaseId;
		
		public string ClearCoverCaseId
		{
            get { return _ClearCoverCaseId; }
		    set
		    {
                _ClearCoverCaseId = value;
                RaisePropertyChanged("ClearCoverCaseId");
		        OnNodeModified();
		    }
		}
		#endregion


        #region ClearCoverCaseDescription Property
        private string _ClearCoverCaseDescription;
        public string ClearCoverCaseDescription
        {
            get { return _ClearCoverCaseDescription; }
            set
            {
                _ClearCoverCaseDescription = value;
                RaisePropertyChanged("ClearCoverCaseDescription");
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
        #endregion

        #region Serialization

        /// <summary>
        ///Saves property values to be retained when opening the node     
        /// </summary>
        protected override void SerializeCore(XmlElement nodeElement, SaveContext context)
        {
            base.SerializeCore(nodeElement, context);
            nodeElement.SetAttribute("ClearCoverCaseId", ClearCoverCaseId);
        }

        /// <summary>
        ///Retrieved property values when opening the node     
        /// </summary>
        protected override void DeserializeCore(XmlElement nodeElement, SaveContext context)
        {
            base.DeserializeCore(nodeElement, context);
            var attrib = nodeElement.Attributes["ClearCoverCaseId"];
            if (attrib == null)
                return;

            ClearCoverCaseId = attrib.Value;
            //SetComponentDescription();

        }


        public void UpdateSelectionEvents()
        {
            if (TreeViewControl != null)
            {
                TreeViewControl.SelectedItemChanged += OnTreeViewSelectionChanged;
            }
        }
        private void OnTreeViewSelectionChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            OnSelectedItemChanged(e.NewValue);
        }

        private void SetComponentDescription()
        {
            Uri uri = new Uri("pack://application:,,,/Wosad.Dynamo.UI;component/Concrete/ACI318/Details/ClearCoverCaseSelectionTreeData.xml");
            XmlTreeHelper treeHelper = new XmlTreeHelper();
            treeHelper.ExamineXmlTreeFile(uri, new EvaluateXmlNodeDelegate(FindDescription));
        }

        private void FindDescription(XmlNode node)
        {
            //check if attribute "Id" exists
            if (null != node.Attributes["Id"])
            {
                if (node.Attributes["Id"].Value == ClearCoverCaseId)
                   {
                       ClearCoverCaseDescription = node.Attributes["Description"].Value;
                   }
            }
        }

        #endregion

        #region TreeView elements

        public TreeView TreeViewControl { get; set; }

        //private ICommand selectedItemChanged;
        //public ICommand SelectedItemChanged
        //{
        //    get
        //    {

        //        if (ClearCoverCaseSelectionDescription == null)
        //        {
        //            selectedItemChanged = new RelayCommand<object>((i) =>
        //            {
        //                OnSelectedItemChanged(i);
        //            });
        //        }

        //        return selectedItemChanged;
        //    }

        //}



        public void DisplayComponentUI(XTreeItem selectedComponent)
        {

            //Example of parsing a string and creating a control from a string


            //if (selectedComponent != null && selectedComponent.Tag != "X" && selectedComponent.ResourcePath != null)
            //{
            //    Assembly execAssembly = Assembly.GetExecutingAssembly();
            //    AssemblyName assemblyName = new AssemblyName(execAssembly.FullName);
            //    string execAssemblyName = assemblyName.Name;
            //    string typeStr =execAssemblyName +".Views.Loads.ASCE7_10." + selectedComponent.ResourcePath;
            //    try
            //    {
            //        Type subMenuType = execAssembly.GetType(typeStr);
            //        UserControl subMenu = (UserControl)Activator.CreateInstance(subMenuType);
            //        AdditionalUI = subMenu;

            //        if (selectedComponent.Id != "X") //parse default values
            //        {
            //            int ind1, ind2;
            //            double numeric;

            //            string DefaultValues = selectedComponent.Id;
            //            string[] Vals = DefaultValues.Split(',');

            //            if (Vals.Length == 3)
            //            {
            //                bool ind1Res = int.TryParse(Vals[0], out ind1); if (ind1Res == true) ComponentOption1 = ind1;
            //                bool ind2Res = int.TryParse(Vals[1], out ind2); if (ind2Res == true) ComponentOption2 = ind2;
            //                bool numRes = double.TryParse(Vals[2], out numeric); if (numRes == true) ComponentValue = numeric;
            //            }
            //            else
            //            {
            //                ComponentOption1 = -1;
            //                ComponentOption2 = -1;
            //                ComponentValue = 0;
            //            }
            //        }

            //    }
            //    catch (Exception)
            //    {

            //        AdditionalUI = null;
            //    }
            //}
            //else
            //{
            //    AdditionalUI = null;
            //}
        }


        private XTreeItem selectedItem;

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

                string id = xtreeItem.Id;
                if (id != "X")
                {
                    ClearCoverCaseId = xtreeItem.Id;
                    ClearCoverCaseDescription = xtreeItem.Description;
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
        public class ClearCoverCaseSelectionViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<ClearCoverCaseSelection>
        {
            public void CustomizeView(ClearCoverCaseSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                ClearCoverCaseSelectionView control = new ClearCoverCaseSelectionView();
                control.DataContext = model;
                
                //remove this part if control does not contain tree
                TreeView tv = control.FindName("selectionTree") as TreeView;
                if (tv!=null)
                {
                    model.TreeViewControl = tv;
                    model.UpdateSelectionEvents();
                }
                
                nodeView.inputGrid.Children.Add(control);
                base.CustomizeView(model, nodeView);
            }

        }
    }
}
