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
 
#region

using Autodesk.DesignScript.Runtime;
using Dynamo.Models;
using System.Collections.Generic;
using Dynamo.Nodes;
using Kodestruct.Common.Section.Interfaces;
using ds = Kodestruct.Common.Section.SectionTypes;
using dm = Kodestruct.Common.Mathematics;

#endregion

namespace Analysis.Section.SectionTypes
{


    public partial class SectionI : CustomProfile
    {

        [IsVisibleInDynamoLibrary(false)]
        internal SectionI(double d, double b_f, double t_f, double t_w)
        {
            ISection r = new ds.SectionI("", d, b_f, t_f, t_w);
            Section = r;
        }
        /// <summary>
        /// Creates an insance of I-shape based on shape geometry
        /// </summary>
        /// <param name="d">Depth</param>
        /// <param name="b_f">Flange width</param>
        /// <param name="t_f">Flange thickness</param>
        /// <param name="t_w">Web thisckness</param>
        /// <returns></returns>
        public static SectionI ByFlangeAndWebDimensions(double d, double b_f, double t_f, double t_w)
        {
            return new SectionI(d, b_f, t_f, t_w);
        }

    }
}
