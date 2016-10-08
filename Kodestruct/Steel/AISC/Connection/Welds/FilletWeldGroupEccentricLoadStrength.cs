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

#endregion

namespace Steel.AISC.Connection
{

/// <summary>
///     Eccentrically loaded fillet weld group strength
///     Category:   Steel.AISC.Connection
/// </summary>
/// 


    public partial class Welded 
    {
        /// <summary>
        ///    Calculates Eccentrically loaded fillet weld group strength
        /// </summary>
        /// <param name="C_WeldGroup">  Coefficient for eccentrically loaded weld group </param>
        /// <param name="l">  Length of connection or weld   </param>
        /// <param name="w_weld">  Size of fillet weld leg  </param>
        /// <param name="C_1">  Electrode strength coefficient </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="phiR_n"> Strength of member or connection </returns>

        [MultiReturn(new[] { "phiR_n" })]
        public static Dictionary<string, object> FilletWeldGroupEccentricLoadStrength(double C_WeldGroup, double l, double w_weld, double C_1 = 1.0, string Code = "AISC360-10")
        {
            //Default values
            double phiR_n = 0;


            //Calculation logic:
            double D = w_weld / (1.0 / 16.0);
            phiR_n= 0.75*D* C_WeldGroup*C_1*l;

            return new Dictionary<string, object>
            {
                { "phiR_n", phiR_n }
 
            };
        }


    }
}


