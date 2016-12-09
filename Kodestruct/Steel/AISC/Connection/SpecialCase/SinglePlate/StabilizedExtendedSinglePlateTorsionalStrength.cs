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
using conx = Kodestruct.Steel.AISC.AISC360v10.Connections;

#endregion

namespace Steel.AISC.Connection.SpecialCase
{

/// <summary>
///     Stabilized extended single plate flexural strength
///     Category:   Steel.AISC.Connection.SpecialCase
/// </summary>
/// 


    public partial class ExtendedSinglePlate 
    {
        /// <summary>
        ///    Flexural strength of extended single plate per AISC SCM Chapter 10 (kip - in unit system for all inputs and outputs)
        /// </summary>
        /// <param name="R_u">  Required strength </param>
        /// <param name="F_yp">  Specified minimum yield stress of plate    </param>
        /// <param name="d_pl">  Depth of plate </param>
        /// <param name="t_p">  Thickness of plate   </param>
        /// <param name="L_bm">  Span length of beam </param>
        /// <param name="F_ybm">  Specified minimum yield stress of beam    </param>
        /// <param name="b_f">  Width of flange  </param>
        /// <param name="t_w">  Thickness of web  </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="phiM_n"> Moment strength </returns>

        [MultiReturn(new[] { "phiM_n" })]
        public static Dictionary<string, object> StabilizedExtendedSinglePlateTorsionalStrength(double R_u,double F_yp,double d_pl,double t_p,double L_bm,double F_ybm,double b_f,double t_w
            , string Code = "AISC360-10")
        {
            //Default values
            double phiM_n = 0;


            //Calculation logic:
            conx.ExtendedSinglePlate sp = new conx.ExtendedSinglePlate();
            phiM_n = sp.StabilizedExtendedSinglePlateTorsionalStrength(b_f, F_ybm, L_bm, R_u, t_w, d_pl, t_p, F_yp);

            return new Dictionary<string, object>
            {
                { "phiM_n", phiM_n }
 
            };
        }


    }
}


