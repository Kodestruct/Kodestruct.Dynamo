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

#endregion

namespace Analysis.Section.SectionTypes
{


    public partial class SectionIWithFlangeHoles : CustomProfile
    {

        [IsVisibleInDynamoLibrary(false)]
        internal SectionIWithFlangeHoles(double d, double b_f, double t_f, double t_w, double b_hole,
            double N_holes, bool IsRolled = false)
        {
            ds.SectionIWithFlangeHoles r = new ds.SectionIWithFlangeHoles("", d, b_f, t_f, t_w, b_hole, N_holes,
             IsRolled);
            Section = r;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d">Overall depth</param>
        /// <param name="b_f">Flange width</param>
        /// <param name="t_f">Flange thickness</param>
        /// <param name="t_w">Web thickness</param>
        /// <param name="b_hole">Hole width</param>
        /// <param name="N_holes">Number of holes per flange (typically 2)</param>
        /// <param name="IsRolled">Indicates whether the shape is rolled</param>
        /// <returns></returns>
        public static SectionIWithFlangeHoles ByFlangeWebAndHoleDimensions(double d, double b_f, double t_f, double t_w, double b_hole, double N_holes=2,
            bool IsRolled = false)
        {
            return new SectionIWithFlangeHoles(d, b_f, t_f, t_w, b_hole, N_holes, IsRolled);
        }

    }
}
