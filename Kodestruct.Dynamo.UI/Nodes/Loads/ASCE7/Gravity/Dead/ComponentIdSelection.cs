#region Copyright
   /*Copyright (C) 2015 Kodestruct Inc

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
using Kodestruct.Loads.ASCE7.Entities;
using System.Xml;
using System.Windows.Input;
using KodestructDynamoUI.Views.Loads.ASCE7;
using GalaSoft.MvvmLight.Command;
using System.Reflection;
using Kodestruct.Dynamo.Common.Infra.TreeItems;
using System.Windows.Resources;
using System.Windows;
using System.IO;
using Kodestruct.Dynamo.UI.Common.TreeItems;
using Dynamo.Nodes;
using Dynamo.Graph.Nodes;
using Dynamo.Graph;


namespace Kodestruct.Loads.ASCE7.Gravity.Dead
{

    /// <summary>
    ///Building component ID (name) used for calculation of dead weight  
    /// </summary>

    [NodeName("Component ID selection")]
    [NodeCategory("Kodestruct.Loads.ASCE7.Gravity.Dead")]
    [NodeDescription("Building component ID (name) used for calculation of dead weight")]
    [IsDesignScriptCompatible]
    public class ComponentIdSelection : UiNodeBase
    {

        public ComponentIdSelection()
        {
            ReportEntry="";
            ComponentId = "Deck3InLWFill";
            ComponentOption1 = 1;
            ComponentOption2 = 0;
            ComponentValue = 0;
            //OutPortData.Add(new PortData("ReportEntry", "calculation log entries (for reporting)"));
            OutPortData.Add(new PortData("ComponentId", "building component id (name)"));
            OutPortData.Add(new PortData("ComponentOption1", "building component subtype (option1)"));
            OutPortData.Add(new PortData("ComponentOption2", "building component subtype (option2)"));
            OutPortData.Add(new PortData("ComponentValue", "building component numerical value"));
            RegisterAllPorts();
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



	    #endregion

        #region OutputProperties

		#region ComponentIdProperty
		
		/// <summary>
		/// ComponentId property
		/// </summary>
		/// <value>building component id (name)</value>
		public string _ComponentId;
		
		public string ComponentId
		{
		    get { return _ComponentId; }
		    set
		    {
		        _ComponentId = value;
		        OnNodeModified(true); 
                RaisePropertyChanged("ComponentId");
		    }
		}
		#endregion

		#region ComponentOption1Property
		
		/// <summary>
		/// ComponentOption1 property
		/// </summary>
		/// <value>building component subtype (option1)</value>
		public double _ComponentOption1;
		
		public double ComponentOption1
		{
		    get { return _ComponentOption1; }
		    set
		    {
		        _ComponentOption1 = value;
		        RaisePropertyChanged("ComponentOption1");
		        OnNodeModified(true); 
		    }
		}
		#endregion

		#region ComponentOption2Property
		
		/// <summary>
		/// ComponentOption2 property
		/// </summary>
		/// <value>building component subtype (option2)</value>
		public double _ComponentOption2;
		
		public double ComponentOption2
		{
		    get { return _ComponentOption2; }
		    set
		    {
		        _ComponentOption2 = value;
		        RaisePropertyChanged("ComponentOption2");
		        OnNodeModified(true); 
		    }
		}
		#endregion

		#region ComponentValueProperty
		
		/// <summary>
		/// ComponentValue property
		/// </summary>
		/// <value>building component numerical value</value>
		public double _ComponentValue;
		
		public double ComponentValue
		{
		    get { return _ComponentValue; }
		    set
		    {
		        _ComponentValue = value;
		        RaisePropertyChanged("ComponentValue");
		        OnNodeModified(true); 
		    }
		}
		#endregion

        #region ComponentDescriptionProperty
        private string componentDescription;

        public string ComponentDescription
        {
            get { return componentDescription; }
            set
            {

                componentDescription = value;
                RaisePropertyChanged("ComponentDescription");
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
            nodeElement.SetAttribute("ComponentId", ComponentId);
            nodeElement.SetAttribute("ComponentOption1", ComponentOption1.ToString());
            nodeElement.SetAttribute("ComponentOption2", ComponentOption2.ToString());
            nodeElement.SetAttribute("ComponentValue", ComponentValue.ToString());
        }

        /// <summary>
        ///Retrieved property values when opening the node     
        /// </summary>
        protected override void DeserializeCore(XmlElement nodeElement, SaveContext context)
        {
            base.DeserializeCore(nodeElement, context);
            var attribComponentId = nodeElement.Attributes["ComponentId"];
            if (attribComponentId != null)
            {
                ComponentId = attribComponentId.Value;
                SetComponentDescription();
            }
            var attribComponentOption1 = nodeElement.Attributes["ComponentOption1"];
            var attribComponentOption2 = nodeElement.Attributes["ComponentOption2"];
            var attribComponentValue = nodeElement.Attributes["ComponentValue"];

            try
            {
                this.ComponentOption1 = double.Parse(attribComponentOption1.Value);
                this.ComponentOption2 = double.Parse(attribComponentOption2.Value);
                this.ComponentValue = double.Parse(attribComponentValue.Value);
            }
            catch (Exception)
            {

            }
            


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
            Uri uri = new Uri("pack://application:,,,/KodestructDynamoUI;component/Views/Loads/ASCE7/Dead/ComponentDeadWeightTreeData.xml");
            XmlTreeHelper treeHelper = new XmlTreeHelper();
            treeHelper.ExamineXmlTreeFile(uri, new EvaluateXmlNodeDelegate(FindDescription));
        }

        private void FindDescription(XmlNode node)
        {
            if (null != node.Attributes["Tag"])
            {
                   if (node.Attributes["Tag"].Value== ComponentId)
                   {
                       ComponentDescription = node.Attributes["Description"].Value;
                   }
            }
        }

        #endregion

        #region treeView elements

        public TreeView TreeViewControl { get; set; }

        private ICommand selectedItemChanged;
        public ICommand SelectedItemChanged
        {
            get
            {

                if (ComponentDescription == null)
                {
                    selectedItemChanged = new RelayCommand<object>((i) =>
                    {
                        OnSelectedItemChanged(i);
                    });
                }

                return selectedItemChanged;
            }

        }


        public void DisplayComponentUI(XTreeItem selectedComponent)
        {
            if (selectedComponent != null && selectedComponent.Tag != "X" && selectedComponent.ResourcePath != null)
            {
                Assembly execAssembly = Assembly.GetExecutingAssembly();
                AssemblyName assemblyName = new AssemblyName(execAssembly.FullName);
                string execAssemblyName = assemblyName.Name;
                string typeStr =execAssemblyName +".Views.Loads.ASCE7." + selectedComponent.ResourcePath;
                try
                {
                    Type subMenuType = execAssembly.GetType(typeStr);
                    UserControl subMenu = (UserControl)Activator.CreateInstance(subMenuType);
                    AdditionalUI = subMenu;

                    if (selectedComponent.Id != "X") //parse default values
                    {
                        int ind1, ind2;
                        double numeric;

                        string DefaultValues = selectedComponent.Id;
                        string[] Vals = DefaultValues.Split(',');

                        if (Vals.Length == 3)
                        {
                            bool ind1Res = int.TryParse(Vals[0], out ind1); if (ind1Res == true) ComponentOption1 = ind1;
                            bool ind2Res = int.TryParse(Vals[1], out ind2); if (ind2Res == true) ComponentOption2 = ind2;
                            bool numRes = double.TryParse(Vals[2], out numeric); if (numRes == true) ComponentValue = numeric;
                        }
                        else
                        {
                            ComponentOption1 = -1;
                            ComponentOption2 = -1;
                            ComponentValue = 0;
                        }
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
                Description = item.GetAttribute("Description"),
                Header = item.GetAttribute("Header"),
                Id = item.GetAttribute("Id"),
                ResourcePath = item.GetAttribute("ResourcePath"),
                Tag = item.GetAttribute("Tag"),
                TemplateName = item.GetAttribute("TemplateName")
            };

            if (item != null)
            {
                //string id = item.GetAttribute("Tag");
                string id =xtreeItem.Tag;
                if (id != "X")
                {
                    ComponentId = id;
                    //ComponentDescription = item.GetAttribute("Description");
                    ComponentDescription = xtreeItem.Description;
                    SelectedItem = xtreeItem;
                    DisplayComponentUI(xtreeItem);
                }
            }
        }

        #endregion

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
        public class ComponentIdViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<ComponentIdSelection>
        {
            public void CustomizeView(ComponentIdSelection model, NodeView nodeView)
            {
                ComponentIdView control = new ComponentIdView();
                control.DataContext = model;
                
                
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
