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
using System.Windows.Input;
using System.Windows;
using System.Xml;
using Kodestruct.Dynamo.Common.Infra.TreeItems;
using Kodestruct.Dynamo.UI.Views.Analysis.Beam.Flexure;
using GalaSoft.MvvmLight.CommandWpf;
using Kodestruct.Dynamo.UI.Common.TreeItems;
using Dynamo.Graph;
using Dynamo.Graph.Nodes;
using Newtonsoft.Json;

namespace Kodestruct.Analysis.Beam.Flexure
{

    /// <summary>
    ///Selection of beam load and boundary condition case for deflection  calculation  
    /// </summary>

    [NodeName("Beam deflection case selection")]
    [NodeCategory("Kodestruct.Analysis.Beam.Flexure")]
    [NodeDescription("Selection of beam load and boundary condition case for deflection  calculation")]
    [IsDesignScriptCompatible]
    public class BeamDeflectionCaseSelection : UiNodeBase
    {

        [JsonConstructor]
        public BeamDeflectionCaseSelection(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
        {

        }
        public BeamDeflectionCaseSelection()
        {
            ReportEntry = "";
            BeamDeflectionCaseId = "C1B_1";
            BeamForcesCaseDescription = "Simply supported. Uniform load on full span. Uniformly distributed load";
            //InPorts.Add(new PortModel(PortType.Input, this, new PortData("Port Name", "Port Description")));
            OutPorts.Add(new PortModel(PortType.Output, this, new PortData("BeamDeflectionCaseId", "Case ID used in calculation of the beam deflection")));
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

        #region Properties

        #region InputProperties



	    #endregion

        #region OutputProperties

		#region BeamDeflectionCaseIdProperty
		
		/// <summary>
		/// BeamDeflectionCaseId property
		/// </summary>
		/// <value>Case ID used in calculation of the beam forces</value>
		public string _BeamDeflectionCaseId;
		
		public string BeamDeflectionCaseId
		{
		    get { return _BeamDeflectionCaseId; }
		    set
		    {
		        _BeamDeflectionCaseId = value;
		        RaisePropertyChanged("BeamDeflectionCaseId");
		        OnNodeModified(true); 
		    }
		}
		#endregion


        #region BeamForcesCaseDescription Property
        private string _BeamForcesCaseDescription;
        public string BeamForcesCaseDescription
        {
            get { return _BeamForcesCaseDescription; }
            set
            {
                _BeamForcesCaseDescription = value;
                RaisePropertyChanged("BeamForcesCaseDescription");
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
            nodeElement.SetAttribute("BeamDeflectionCaseId", BeamDeflectionCaseId);
        }

        /// <summary>
        ///Retrieved property values when opening the node     
        /// </summary>
        protected override void DeserializeCore(XmlElement nodeElement, SaveContext context)
        {
            base.DeserializeCore(nodeElement, context);
            var attrib = nodeElement.Attributes["BeamDeflectionCaseId"];
            if (attrib == null)
                return;

            BeamDeflectionCaseId = attrib.Value;
            SetCaseDescription();
        }

        private void SetCaseDescription()
        {
            Uri uri = new Uri("pack://application:,,,/KodestructDynamoUI;component/Views/Analysis/Beam/Flexure/BeamForceCaseTreeData.xml");
            XmlTreeHelper treeHelper = new XmlTreeHelper();
            treeHelper.ExamineXmlTreeFile(uri, new EvaluateXmlNodeDelegate(FindCaseDescription));
        }



        private void FindCaseDescription(XmlNode node)
        {
            if (null != node.Attributes["Id"])
            {
                if (node.Attributes["Id"].Value == BeamDeflectionCaseId)
                {
                    BeamForcesCaseDescription = node.Attributes["Description"].Value;
                }
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




        #endregion

        #region TreeView elements

        public TreeView TreeViewControl { get; set; }

        private ICommand selectedItemChanged;
        public ICommand SelectedItemChanged
        {
            get
            {


                    selectedItemChanged = new RelayCommand<object>((i) =>
                    {
                        OnSelectedItemChanged(i);
                    });


                return selectedItemChanged;
            }

        }



        public void DisplayComponentUI(XTreeItem selectedComponent)
        {

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
                    BeamDeflectionCaseId = xtreeItem.Id;
                    SelectedItem = xtreeItem;
                    BeamForcesCaseDescription = xtreeItem.Description;
                }
            }
        }

        #endregion



        /// <summary>
        ///Customization of WPF view in Dynamo UI      
        /// </summary>
        public class BeamDeflectionCaseSelectionViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<BeamDeflectionCaseSelection>
        {
            public void CustomizeView(BeamDeflectionCaseSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                BeamDeflectionCaseView control = new BeamDeflectionCaseView();
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
