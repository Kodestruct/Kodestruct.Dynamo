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
using Kodestruct.Loads.ASCE7.Entities;
using Kodestruct.Dynamo.Common.Infra.TreeItems;
using System.Xml;
using System.Windows;
using Kodestruct.Dynamo.UI.Common.TreeItems;
using Dynamo.Nodes;
using Dynamo.Graph;
using Dynamo.Graph.Nodes;
using KodestructDynamoUI.Views.Loads.ASCE7;
using Newtonsoft.Json;

namespace Kodestruct.Loads.ASCE7.General
{

    /// <summary>
    ///Selection of occupancy for determination of Risk Category  
    /// </summary>

    [NodeName("Building occupancy ID selection")]
    [NodeCategory("Kodestruct.Loads.ASCE7.General")]
    [NodeDescription("Selection of occupancy for determination of Risk Category")]
    [IsDesignScriptCompatible]
    public class BuildingOccupancyIdSelection : UiNodeBase
    {

        [JsonConstructor]
        public BuildingOccupancyIdSelection(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
        {

        }
        public BuildingOccupancyIdSelection()
        {

            BuildingOccupancyDescription ="Commercial building";
            BuildingOccupancyId = "Commercial building";
            //InPorts.Add(new PortModel(PortType.Input, this, new PortData("Port Name", "Port Description")));
            OutPorts.Add(new PortModel(PortType.Output, this, new PortData("BuildingOccupancyId", "Occupancy description")));
            RegisterAllPorts();
            //PropertyChanged += NodePropertyChanged;
        }

        #region BuildingOccupancyDescription Property
        private string _BuildingOccupancyDescription;
        public string BuildingOccupancyDescription
        {
            get { return _BuildingOccupancyDescription; }
            set
            {
                _BuildingOccupancyDescription = value;
                RaisePropertyChanged("BuildingOccupancyDescription");
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

		#region BuildingOccupancyIdProperty
		
		/// <summary>
		/// BuildingOccupancy property
		/// </summary>
		/// <value>Occupancy description</value>
		public string _BuildingOccupancyId;
		
		public string BuildingOccupancyId
		{
		    get { return _BuildingOccupancyId; }
		    set
		    {
		        _BuildingOccupancyId = value;
                RaisePropertyChanged("BuildingOccupancyId");
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
                    BuildingOccupancyId = xtreeItem.Id;
                    BuildingOccupancyDescription = xtreeItem.Description;
                    SelectedItem = xtreeItem;
                    DisplayComponentUI(xtreeItem);
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
        public class BuildingOccupancyViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<BuildingOccupancyIdSelection>
        {
            public void CustomizeView(BuildingOccupancyIdSelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                BuildingOccupancyIdView control = new BuildingOccupancyIdView();
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
