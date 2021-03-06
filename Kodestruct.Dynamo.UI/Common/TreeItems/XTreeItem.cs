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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Kodestruct.Dynamo.Common.Infra.TreeItems
{

    /// <summary>
    ///     
    ///     Item element in XML tree view selection
    ///     
    /// </summary>
    public class XTreeItem 
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public string ResourcePath { get; set; }
        public string Tag { get; set; }
        public string TemplateName { get; set; }
    }
}
