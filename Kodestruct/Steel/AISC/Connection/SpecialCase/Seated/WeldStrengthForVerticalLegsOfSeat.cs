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
using Kodestruct.Steel.AISC.AISC360v10.Connections;

#endregion

namespace Steel.AISC.Connection.SpecialCase
{

/// <summary>
///     Weld strength for vertical legs of seat
///     Category:   Steel.AISC.Connection.SpecialCase
/// </summary>
/// 

    public partial class Seated 
    {
        /// <summary>
        ///     Weld strength for vertical legs of seat
        /// </summary>
        /// <param name="phiR_nWeld">Total weld strength for a single vertical leg  </param>
        /// <param name="L_vSeat"> Vertical length (height) of seat </param>
        /// <param name="e_1">  Eccentricity of seated connection, defined as distance from load to face of support </param>
        /// <returns name="phiR_n"> Strength of member or connection </returns>

        [MultiReturn(new[] { "phiR_n" })]
        public static Dictionary<string, object> WeldStrengthForVerticalLegsOfSeat(double phiR_nWeld,double L_vSeat,double e_1)
        {
            //Default values
            double phiR_n = 0;


            //Calculation logic:
            double F_y = 0.0; //F_y not used in this calculation
            double E = 0; //E not used in this calculation
            double b_seat = 0.0; //not used in this calculation
            double a_seat = 0.0; //not used in this calculation

            SeatedConnection sc = new SeatedConnection(a_seat, b_seat, F_y, E);
            phiR_n = sc.GetWeldStrengthForVerticalLegsOfSeat(phiR_nWeld, L_vSeat, e_1);

            return new Dictionary<string, object>
            {
                { "phiR_n", phiR_n }
 
            };
        }



    }
}


