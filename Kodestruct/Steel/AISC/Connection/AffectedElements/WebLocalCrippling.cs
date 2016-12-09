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
///     Concentrated force web local crippling 
///     Category:   Steel.AISC.Connection
/// </summary>
/// 


    public partial class AffectedElements 
    {
        /// <summary>
        ///    alculates concentrated force web local crippling (kip - in unit system for all inputs and outputs)
        /// </summary>
        /// <param name="t_w">  Thickness of web  </param>
        /// <param name="t_f">  Thickness of flange   </param>
        /// <param name="l_b">  Length of bearing   </param>
        /// <param name="d">  Full nominal depth of the section    </param>
        /// <param name="F_yw">  Specified minimum yield stress of the web material  </param>
        /// <param name="l_edge">  Edge distance </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="phiR_n"> Strength of member or connection </returns>

        [MultiReturn(new[] { "phiR_n" })]
        public static Dictionary<string, object> WebLocalCrippling(double t_w,double t_f,double l_b,double d,
            double F_yw, double l_edge, string Code = "AISC360-10")
        {
            //Default values
            double phiR_n = 0;


            //Calculation logic:
            phiR_n = FlangeOrWebWithConcentratedForces.GetWebLocalCripplingStrength(t_w, t_f, d, l_b, l_edge, F_yw);

            return new Dictionary<string, object>
            {
                { "phiR_n", phiR_n }
 
            };
        }


    }
}


