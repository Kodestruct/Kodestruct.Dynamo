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
using System.Windows.Input;
using System.Windows;
using Kodestruct.Dynamo.UI.Common.TreeItems;
using GalaSoft.MvvmLight.Command;
using KodestructDynamoUI.Views.Loads.ASCE7;
using Dynamo.Nodes;
using Dynamo.Graph;
using Dynamo.Graph.Nodes;
using Newtonsoft.Json;

namespace Kodestruct.Loads.ASCE7.Gravity.Live
{

    /// <summary>
    ///Occupancy or use  for selection of uniformly distributed loads - ASCE7-10  
    /// </summary>

    [NodeName("Live Load occupancy selection")]
    [NodeCategory("Kodestruct.Loads.ASCE7.Gravity.Live")]
    [NodeDescription("Occupancy or use  for selection of uniformly distributed loads")]
    [IsDesignScriptCompatible]
    public class LiveLoadOccupancySelection : UiNodeBase
    {

        [JsonConstructor]
        public LiveLoadOccupancySelection(IEnumerable<PortModel> inPorts, IEnumerable<PortModel> outPorts) : base(inPorts, outPorts)
        {

        }
        public LiveLoadOccupancySelection()
        {

            LiveLoadOccupancyId = "Office";
            LiveLoadOccupancyDescription = "Office space";
            //InPorts.Add(new PortModel(PortType.Input, this, new PortData("Port Name", "Port Description")));
            OutPorts.Add(new PortModel(PortType.Output, this, new PortData("LiveLoadOccupancyId", "description of space for calculation of live loads")));
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

        

        #region OutputProperties

		#region LiveLoadOccupancyIdProperty
		
		/// <summary>
		/// LiveLoadOccupancyId property
		/// </summary>
		/// <value>description of space for calculation of live loads</value>
		public string _LiveLoadOccupancyId;
		
		public string LiveLoadOccupancyId
		{
		    get { return _LiveLoadOccupancyId; }
		    set
		    {
		        _LiveLoadOccupancyId = value;
		        RaisePropertyChanged("LiveLoadOccupancyId");
		        OnNodeModified(true); 
		    }
		}
		#endregion

        
        #region LiveLoadOccupancyDescription Property
        private string liveLoadOccupancyDescription;
        public string LiveLoadOccupancyDescription
        {
            get { return liveLoadOccupancyDescription; }
            set
            {
                liveLoadOccupancyDescription = value;
                RaisePropertyChanged("LiveLoadOccupancyDescription"); 
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

            //TODO: Add partition allowance here
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

                
                string id =xtreeItem.Id;
                if (id != "X")
                {
                    LiveLoadOccupancyId = xtreeItem.Id;
                    LiveLoadOccupancyDescription = xtreeItem.Description;
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
        public class LiveLoadOccupancyIdViewCustomization : UiNodeBaseViewCustomization,
            INodeViewCustomization<LiveLoadOccupancySelection>
        {
            public void CustomizeView(LiveLoadOccupancySelection model, NodeView nodeView)
            {
                base.CustomizeView(model, nodeView);

                LiveLoadOccupancyIdView control = new LiveLoadOccupancyIdView();
                control.DataContext = model;
                
                //remove this part if control does not contain tree
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
