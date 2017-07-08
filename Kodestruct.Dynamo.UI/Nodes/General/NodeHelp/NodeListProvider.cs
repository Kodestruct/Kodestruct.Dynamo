//using Kodestruct.Common.Data;
//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Reflection;

//namespace KodestructDynamoUI.Nodes.General
//{
//    public class NodeListProvider
//    {
//        public NodeListProvider()
//        {
//            NodeItems = new List<NodeData>();
//            NodeItems = ReadAllNodeData();

//        }

//        #region NodeItems Property
//        private List<NodeData> _NodeItems;
//        public List<NodeData> NodeItems
//        {
//            get { return _NodeItems; }
//            set
//            {
//                _NodeItems = value;
//            }
//        }

//        #endregion

//        private List<NodeData> ReadAllNodeData()
//        {

//            List<NodeData> AllNodes = new List<NodeData>();
//            #region Read Table Data

//            //<Diameter><Standard>< Oversize><Short-Slot Width><Short-Slot Length><Long-Slot Width><Long-Slot Length>
//            var Tv11 = new { Name="", FullName="",Uri="" }; // sample
//            var AllNodesList = ListFactory.MakeList(Tv11);

//            string resourceName = "KodestructDynamoUI.Resources.AllNodeHelp.txt";
//            var assembly = Assembly.GetExecutingAssembly();

//            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
//            {
//                string line;


//                using (TextReader tr = new StreamReader(stream))
//                {

//                    while ((line = tr.ReadLine()) != null)
//                    {
//                        string[] Vals = line.Split(',');
//                        if (Vals.Count() == 3)
//                        {
//                            string V0 = Vals[0];
//                            string V1 = Vals[1];
//                            string V2 = Vals[2];

//                            AllNodes.Add(new NodeData(V0, V1, V2));
//                        }
//                    }


//                }
//            }

//            #endregion

//            return AllNodes;
//        }
//    }
//}
