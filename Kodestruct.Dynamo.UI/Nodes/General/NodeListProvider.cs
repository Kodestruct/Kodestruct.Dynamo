using KodestructDynamoUI.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodestructDynamoUI.Nodes.General
{
    public class NodeListProvider
    {
        public NodeListProvider()
        {
            NodeItems = new List<NodeData>()
            {
                new NodeData("Abc","BAbc","Web"),
                new NodeData("Abc","BAbc","Web")
            };

        }
        #region NodeItems Property
        private List<NodeData> _NodeItems;
        public List<NodeData> NodeItems
        {
            get { return _NodeItems; }
            set
            {
                _NodeItems = value;
            }
        }

        #endregion
    }
}
