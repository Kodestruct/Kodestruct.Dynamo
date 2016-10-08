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
using Kodestruct.Steel.AISC.AISC360v10.Composite;

#endregion

namespace Steel.AISC.Composite
{

/// <summary>
///     Beam effective slab width
///     Category:   Kodestruct.Steel.AISC_10.Composite
/// </summary>
/// 


    public partial class Flexure 
    {
        /// <summary>
        ///    Calculates Beam effective slab width
        /// </summary>
        /// <param name="L">  Length of member length of span or unbraced length of member    </param>
        /// <param name="L_centerLeft">  Beam spacing  measured normal to beam span (left side of beam) </param>
        /// <param name="L_centerRight">  Beam spacing  measured normal to beam span (right side of beam) </param>
        /// <param name="L_edgeLeft">  Distance between slab edges measured normal to beam span (left side of beam) </param>
        /// <param name="L_edgeRight">  Distance between slab edges measured normal to beam span (right side of beam) </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="b_eff"> Effective width of concrete slab in composite beam design </returns>

        [MultiReturn(new[] { "b_eff" })]
        public static Dictionary<string, object> BeamEffectiveSlabWidth(double L, double L_centerLeft, double L_centerRight, double L_edgeLeft, 
            double L_edgeRight, string Code = "AISC360-10")
        {
            //Default values
            double b_eff = 0;


            //Calculation logic:
             CompositeBeamSection cs = new CompositeBeamSection() ;
             b_eff= cs.GetEffectiveSlabWidth(L, L_centerLeft, L_centerRight, L_edgeLeft, L_edgeRight);


            return new Dictionary<string, object>
            {
                { "b_eff", b_eff }
 
            };
        }


    }
}


