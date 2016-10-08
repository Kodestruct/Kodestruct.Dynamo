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
using Kodestruct.Steel.AISC.AISC360v10.Connections;
using Kodestruct.Steel.AISC;
using System;

#endregion

namespace Steel.AISC.Connection
{

/// <summary>
///     Eccentrically loaded weld group coefficient
///     Category:   Steel.AISC.Connection
/// </summary>
/// 


    public partial class Welded 
    {
            /// <summary>
            ///    Calculates Eccentrically loaded weld group coefficient
            /// </summary>
            /// <param name="WeldGroupPattern">  Weld group pattern type </param>
            /// <param name="l_Weld_horizontal">  Weld group horizontal dimension  </param>
            /// <param name="l_Weld_vertical">  Weld group vertical dimension  </param>
            /// <param name="e_group">  Connection bolt or weld group eccentricity </param>
            /// <param name="theta">  Angle of loading for eccentric bolt or weld group </param>
            /// <param name="w_weld">  Size of weld leg </param>
            /// <param name="F_EXX">  Filler metal classification strength </param>
        /// <param name="IsLoadOutOfPlane">  Indicates whether the load on bolt group is not in the plane of welds. In such case eccentricity is measured normal to the plane of welds. </param>
        /// <param name="Code"> Applicable version of code/standard</param>
            /// <returns name="C_WeldGroup"> Coefficient for eccentrically loaded weld group </returns>
            /// <returns name="phiR_n"> Ultimate load value at given angle and eccentricity </returns>

        [MultiReturn(new[] { "C_WeldGroup", "phiR_n" })]
        public static Dictionary<string, object> FilletWeldGroupCoefficient(string WeldGroupPattern,double l_Weld_horizontal,double l_Weld_vertical,double e_group,
            double theta, double w_weld, double F_EXX, bool IsLoadOutOfPlane = false, string Code = "AISC360-10")
        {
            //Default values
            double C_WeldGroup = 0;
            double phiR_n = 0;

            //Calculation logic:
            WeldGroupPattern pattern;
            bool IsValidString = Enum.TryParse(WeldGroupPattern, true, out pattern);
            if (IsValidString == true)
            {
                FilletWeldGroup wg = new FilletWeldGroup(pattern, l_Weld_horizontal, l_Weld_vertical, w_weld, F_EXX, IsLoadOutOfPlane);
                C_WeldGroup = wg.GetInstantaneousCenterCoefficient(e_group, theta); ;
                phiR_n = 0.75*wg.GetUltimateForce(e_group, theta); //0.75 is the phi factor
            }
            else
            {
                throw new Exception("Weld group strength calculation failed. Invalid weld group pattern designation.");
            }


            return new Dictionary<string, object>
            {
                { "C_WeldGroup", C_WeldGroup },
                { "phiR_n", phiR_n }
 
            };
        }

    }
}


