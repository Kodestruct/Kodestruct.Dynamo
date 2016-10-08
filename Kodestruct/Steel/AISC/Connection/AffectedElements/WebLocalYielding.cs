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
using Kodestruct.Steel.AISC.AISC360v10.Connections.AffectedMembers.ConcentratedForces;

#endregion

namespace Steel.AISC.Connection
{

/// <summary>
///     Concentrated force web local yielding
///     Category:   Steel.AISC.Connection
/// </summary>
/// 


    public partial class AffectedElements 
    {
        /// <summary>
        ///    Calculates Concentrated force web local yielding
        /// </summary>
        /// <param name="t_w">  Thickness of web  </param>
        /// <param name="F_yw">  Specified minimum yield stress of the web material </param>
        /// <param name="k">  Distance from outer face of flange to the web toe of fillet  </param>
        /// <param name="l_b">  Length of bearing   </param>
        /// <param name="d">  Full nominal depth of the section    </param>
        /// <param name="l_edge">  Edge distance </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="phiR_n"> Strength of member or connection </returns>

        [MultiReturn(new[] { "phiR_n" })]
        public static Dictionary<string, object> WebLocalYielding(double t_w, double F_yw, double k, double l_b, double d, double l_edge, string Code = "AISC360-10")
        {
            //Default values
            double phiR_n = 0;


            //Calculation logic:
            phiR_n = FlangeOrWebWithConcentratedForces.GetWebLocalYieldingStrength(d, t_w, l_edge, F_yw, k, l_b);

            return new Dictionary<string, object>
            {
                { "phiR_n", phiR_n }
 
            };
        }


    }
}


