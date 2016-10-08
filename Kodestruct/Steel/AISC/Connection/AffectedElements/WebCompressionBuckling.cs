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
///     Concentrated force web compression buckling
///     Category:   Steel.AISC.Connection
/// </summary>
/// 


    public partial class AffectedElements 
    {
        /// <summary>
        ///    Calculates Concentrated force web compression buckling
        /// </summary>
        /// <param name="t_w">  Thickness of web  </param>
        /// <param name="h_web">  Clear distance between flanges less the fillet or corner radius for rolled shapes </param>
        /// <param name="F_yw">  Specified minimum yield stress of the web material  </param>
        /// <param name="E">  Modulus of elasticity of steel </param>
        /// <param name="d">  Full nominal depth of the section    </param>
        /// <param name="l_edge">  Edge distance </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="phiR_n"> Strength of member or connection </returns>

        [MultiReturn(new[] { "phiR_n" })]
        public static Dictionary<string, object> WebCompressionBuckling(double t_w,double h_web,double F_yw,double E,double d,double l_edge, string Code = "AISC360-10")
        {
            //Default values
            double phiR_n = 0;


            //Calculation logic:

            phiR_n = FlangeOrWebWithConcentratedForces.GetWebCompressionBucklingStrength(t_w, h_web, d, l_edge, F_yw);
            return new Dictionary<string, object>
            {
                { "phiR_n", phiR_n }
 
            };
        }


    }
}


