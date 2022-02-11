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
using Dynamo.Graph.Nodes;
using Kodestruct.Dynamo.Common.Infra.TreeItems;
using System.Xml;
using Dynamo.Graph;
using System.Windows;
using System.Windows.Input;
using Kodestruct.Dynamo.UI.Common.TreeItems;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;

namespace Kodestruct.Loads.ASCE7.Lateral.Wind.StructureParameters
{

    /// <summary>
    ///Structure type for directionality factor calculation  
    /// </summary>

    [NodeName("Wind directionality case")]
    [NodeCategory("Kodestruct.Loads.ASCE7.Lateral.Wind.StructureParameters")]
    [NodeDescription("Structure type for directionality factor calculation")]
    [IsDesignScriptCompatible]
    public class WindDirectionalityCaseIdSelection : UiNodeBase
    {

        [JsonConstructor]
        public WindDirectionalityCaseIdSelection(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
        {

        }
        public WindDirectionalityCaseIdSelection()
        {

            //InPorts.Add(new PortModel(PortType.Input, this, new PortData("Port Name", "Port Description")));
            OutPorts.Add(new PortModel(PortType.Output, this, new PortData("WindDirectionalityCaseId", "Structure type for directionality factor calculation")));
            RegisterAllPorts();
            SetDefaultParameters();
            //PropertyChanged += NodePropertyChanged;
        }

        private void SetDefaultParameters()
        {
            //ReportEntry="";
            this.WindDirectionalityCaseId = "MWFRS";
            this.WindDirectionalityCaseIdDescription = "Main Wind Force Resisting System (MWFRS)";
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

		#region WindDirectionalityCaseIdProperty
		
		/// <summary>
		/// WindDirectionalityCaseId property
		/// </summary>
		/// <value></value>
        public  string _WindDirectionalityCaseId;
		
        public string  WindDirectionalityCaseId
        {
            get { return _WindDirectionalityCaseId; }
            set
            {
                _WindDirectionalityCaseId = value;
                RaisePropertyChanged("WindDirectionalityCaseId");
                OnNodeModified();
            }
        }
		#endregion


        #region WindDirectionalityCaseDescriptionProperty

        /// <summary>
        /// WindDirectionalityCaseDescription property
        /// </summary>
        /// <value></value>
        public string _WindDirectionalityCaseIdDescription;

        public string WindDirectionalityCaseIdDescription
        {
            get { return _WindDirectionalityCaseIdDescription; }
            set
            {
                _WindDirectionalityCaseIdDescription = value;
                RaisePropertyChanged("WindDirectionalityCaseIdDescription");
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
        #endregion

        #region Serialization

        /// <summary>
        ///Saves property values to be retained when opening the node     
        /// </summary>
        protected override void SerializeCore(XmlElement nodeElement, SaveContext context)
        {
            base.SerializeCore(nodeElement, context);
            nodeElement.SetAttribute("WindDirectionalityCaseId", WindDirectionalityCaseId);
        }

        /// <summary>
        ///Retrieved property values when opening the node     
        /// </summary>
        protected override void DeserializeCore(XmlElement nodeElement, SaveContext context)
        {
            base.DeserializeCore(nodeElement, context);
            var attrib = nodeElement.Attributes["WindDirectionalityCaseId"];
            if (attrib == null)
                return;
           
            WindDirectionalityCaseId = attrib.Value;
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
            Uri uri = new Uri("pack://application:,,,/Kodestruct.Dynamo.UI;component/Loads/ASCE7/Lateral/Wind/DirectionalityTreeData.xml");
            XmlTreeHelper treeHelper = new XmlTreeHelper();
            treeHelper.ExamineXmlTreeFile(uri, new EvaluateXmlNodeDelegate(FindDescription));
        }

        private void FindDescription(XmlNode node)
        {
            //check if attribute "Id" exists
            if (null != node.Attributes["Tag"])
            {
                   if (node.Attributes["Tag"].Value== WindDirectionalityCaseId)
                   {
                       WindDirectionalityCaseIdDescription = node.Attributes["Description"].Value;
                   }
            }
        }

        #endregion

        #region TreeView elements

        public TreeView TreeViewControl { get; set; }

        private ICommand selectedItemChanged;
        public ICommand SelectedItemChanged
        {
            get
            {

                if (WindDirectionalityCaseIdDescription == null)
                {
                    selectedItemChanged = new RelayCommand<object>((i) =>
                    {
                        OnSelectedItemChanged(i);
                    });
                }

                return selectedItemChanged;
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
                Header = item.GetAttribute("Header"),
                Description = item.GetAttribute("Description"),
                Id = item.GetAttribute("Id"),
                ResourcePath = item.GetAttribute("ResourcePath"),
                Tag = item.GetAttribute("Tag"),
                TemplateName = item.GetAttribute("TemplateName")
            };

            if (item != null)
            {

                string id = xtreeItem.Tag;
                if (id != "X")
                {
                    WindDirectionalityCaseId = id;
                    WindDirectionalityCaseIdDescription = xtreeItem.Description;
                    SelectedItem = xtreeItem;
                }
            }
        }

        #endregion




        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class WindDirectionalityCaseIdSelectionViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<WindDirectionalityCaseIdSelection>
        {
            public void CustomizeView(WindDirectionalityCaseIdSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                WindDirectionalityCaseIdSelectionView control = new WindDirectionalityCaseIdSelectionView();
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
