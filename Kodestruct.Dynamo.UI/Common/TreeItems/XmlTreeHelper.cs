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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Resources;
using System.Xml;

namespace Kodestruct.Dynamo.UI.Common.TreeItems
{
    public class XmlTreeHelper
    {
        public void ExamineXmlTreeFile(Uri uri, EvaluateXmlNodeDelegate nodeEvaluator)
        {
            StreamResourceInfo info = Application.GetResourceStream(uri);
            Stream stream = info.Stream;
            XmlDocument doc = new XmlDocument();
            doc.Load(stream);
            XmlNode root = doc.SelectSingleNode("*");
            ReadXml(root, nodeEvaluator);

        }

        private void ReadXml(XmlNode node, EvaluateXmlNodeDelegate nodeEvaluator)
        {
            if (node is XmlElement)
            {
                //invoke delegate here
                nodeEvaluator(node);

                if (node.HasChildNodes)
                {
                    ReadXml(node.FirstChild, nodeEvaluator);
                }
                if (node.NextSibling!=null)
                {
                    ReadXml(node.NextSibling, nodeEvaluator);
                }
            }
        }
    }

    public delegate void EvaluateXmlNodeDelegate (XmlNode node);
}
