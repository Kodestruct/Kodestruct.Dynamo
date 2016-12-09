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
using System;
using dcf = Kodestruct.Steel.AISC.AISC360v10.Connections.AffectedMembers.ConcentratedForces;

#endregion

namespace Steel.AISC.Connection
{

/// <summary>
///     Concentrated force web panel zone shear
///     Category:   Steel.AISC.Connection
/// </summary>
/// 



    public partial class AffectedElements 
    {
            /// <summary>
            ///    Calculates concentrated force web panel zone shear (kip - in unit system for all inputs and outputs)
            /// </summary>
            /// <param name="t_w">  Thickness of web  </param>
            /// <param name="t_cf">  Thickness of column flange   </param>
            /// <param name="b_cf">  Width of column flange  </param>
            /// <param name="d_b">  Nominal fastener diameter </param>
            /// <param name="d_c">  Depth of column  </param>
            /// <param name="F_y">  Specified minimum yield stress </param>
            /// <param name="P_u">  Required axial strength </param>
            /// <param name="A_g">  Gross cross-sectional area of member </param>
            /// <param name="PanelDeformationConsideredInAnalysis">  Identifies whether the effect of panel-zone deformation on frame stability is  considered in the analysis </param>
            /// <param name="Code"> Applicable version of code/standard</param>
            /// <returns name="phiR_n"> Strength of member or connection </returns>

        [MultiReturn(new[] { "phiR_n" })]
        public static Dictionary<string, object> WebPanelZoneShear(double t_w,double t_cf,double b_cf,double d_b,double d_c,double F_y,double P_u,double A_g,
            bool PanelDeformationConsideredInAnalysis, string Code = "AISC360-10")
        {
            //Default values
            double phiR_n = 0;


            //Calculation logic:
            phiR_n = FlangeOrWebWithConcentratedForces.WebPanelZoneShear(t_w, t_cf, b_cf, d_b, d_c, F_y, P_u, A_g, PanelDeformationConsideredInAnalysis);


            return new Dictionary<string, object>
            {
                { "phiR_n", phiR_n }
 
            };
        }

    }
}


