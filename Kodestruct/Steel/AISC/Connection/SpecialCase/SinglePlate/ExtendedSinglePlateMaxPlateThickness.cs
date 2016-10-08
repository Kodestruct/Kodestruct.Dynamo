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
///     Extended single plate maximum plate thickness
///     Category:   Steel.AISC.Connection.SpecialCase
/// </summary>
/// 


    public partial class ExtendedSinglePlate 
    {
        /// <summary>
        ///    Calculates Extended single plate maximum plate thickness
        /// </summary>
        /// <param name="F_nv">  Nominal shear stress </param>
        /// <param name="d_b">  Nominal fastener diameter </param>
        /// <param name="C_prime">  Coefficient for bolt group subjected to pure moment </param>
        /// <param name="F_yp">  Specified minimum yield stress of plate    </param>
        /// <param name="d_pl">  Depth of plate </param>
        /// <param name="t_w">  Thickness of web  </param>
        /// <param name="L_ehBm">  Horizontal edge distance for bolts in connected beam </param>
        /// <param name="L_ehPl">  Horizontal edge distance for bolts in connected plate </param>
        /// <param name="N_cols">  Number of bolt columns in bolt groups </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="t_max"> Maximum plate thickness </returns>

        [MultiReturn(new[] { "t_max" })]
        public static Dictionary<string, object> ExtendedSinglePlateMaxPlateThickness(double F_nv,double d_b,double C_prime,double F_yp,double d_pl,double t_w,double L_ehBm,double L_ehPl,double N_cols
            , string Code = "AISC360-10")
        {
            //Default values
            double t_max = 0;


            //Calculation logic:
            conx.ExtendedSinglePlate sp = new conx.ExtendedSinglePlate();
            t_max = sp.GetMaximumPlateThickness(F_nv, d_b, C_prime, F_yp, d_pl, t_w, L_ehBm, L_ehPl, N_cols);

            return new Dictionary<string, object>
            {
                { "t_max", t_max }
 
            };
        }


    }
}


