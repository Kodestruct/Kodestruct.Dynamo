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
using Kodestruct.Steel.AISC;
using System;
using Dynamo.Graph.Nodes;

#endregion

namespace Steel.AISC.Connection
{

/// <summary>
///     Concentrically loaded fillet weld group strength
///     Category:   Steel.AISC.Connection
/// </summary>
/// 


    [IsDesignScriptCompatible]
    public partial class Welded 
    {
        /// <summary>
        ///    Calculates Concentrically loaded fillet weld group strength (kip - in unit system for all inputs and outputs)
        /// </summary>
        /// <param name="WeldGroupPattern">  Weld group pattern type </param>
        /// <param name="l_transv">  Length of transversely loaded welds </param>
        /// <param name="l_longit">  Length of longitudinally loaded welds </param>
        /// <param name="w_weld">  Size of weld leg </param>
        /// <param name="F_EXX">  Filler metal classification strength </param>
        /// <param name="theta">  Angle of load from vertical </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="phiR_n"> Strength of member or connection </returns>

        [MultiReturn(new[] { "phiR_n" })]
        public static Dictionary<string, object> FilletWeldGroupConcentricLoadStrength(string WeldGroupPattern,double l_transv,double l_longit,
            double w_weld, double F_EXX=70, double theta=0, string Code = "AISC360-10")
        {
            //Default values
            double phiR_n = 0;


            //Calculation logic:
            WeldGroupPattern pattern;
            bool IsValidString = Enum.TryParse(WeldGroupPattern, true, out pattern);
            if (IsValidString == true)
            {
                FilletWeldGroup wg = new FilletWeldGroup(pattern, l_transv, l_longit, w_weld, F_EXX);
                phiR_n = wg.GetConcentricLoadStrenth(theta);
            }
            else
            {
                throw new Exception("Weld group strength calculation failed. Invalid weld group pattern designation.");
            }


            return new Dictionary<string, object>
            {
                { "phiR_n", phiR_n }
 
            };
        }




    }
}


