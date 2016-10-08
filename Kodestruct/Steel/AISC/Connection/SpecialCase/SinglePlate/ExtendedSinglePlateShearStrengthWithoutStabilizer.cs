#region Copyright
   /*Copyright (C) 2015 Kodestruct Inc

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
///     Extended single plate shear strength without stabilizer
///     Category:   Steel.AISC.Connection.SpecialCase
/// </summary>
/// 


    public partial class ExtendedSinglePlate 
    {
        /// <summary>
        ///    Extended single plate shear strength if no stabilizer plates are added per AISC SCM Chapter 10
        /// </summary>
        /// <param name="d_pl">  Depth of plate </param>
        /// <param name="t_p">  Thickness of plate   </param>
        /// <param name="a_bolts">  Distance from support to first line of bolts </param>
        /// <param name="F_y">  Specified minimum yield stress </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="phiR_n"> Strength of member or connection </returns>

        [MultiReturn(new[] { "phiR_n" })]
        public static Dictionary<string, object> ExtendedSinglePlateShearStrengthWithoutStabilizer(double d_pl, double t_p, double a_bolts, double F_y, string Code = "AISC360-10")
        {
            //Default values
            double phiR_n = 0;


            //Calculation logic:
            conx.ExtendedSinglePlate sp = new conx.ExtendedSinglePlate();
            phiR_n = sp.GetShearStrengthWithoutStabilizerPlate(d_pl, t_p, a_bolts, F_y);

            return new Dictionary<string, object>
            {
                { "phiR_n", phiR_n }
 
            };
        }


    }
}


