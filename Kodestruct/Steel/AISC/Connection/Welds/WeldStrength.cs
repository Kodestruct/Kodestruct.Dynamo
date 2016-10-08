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
using Kodestruct.Steel.AISC.AISC360v10.Connections.Weld;
using System;
using Kodestruct.Steel.AISC;
using Kodestruct.Steel.AISC.Entities.Welds.Interfaces;

#endregion

namespace Steel.AISC.Connection
{

/// <summary>
///     Weld strength
///     Category:   Steel.AISC.Connection
/// </summary>
/// 


    public partial class Welded 
    {
        /// <summary>
        ///    Calculates Weld strength
        /// </summary>
        /// <param name="l_weld">  Weld length </param>
        /// <param name="WeldType">  Weld type </param>
        /// <param name="WeldLoadTypeId">  Type of load on weld  under consideration </param>
        /// <param name="t_weld">  Weld throat thickness </param>
        /// <param name="F_EXX">  Filler metal classification strength </param>
        /// <param name="A_nBase">  Area of base metal in a welded connection </param>
        /// <param name="F_y"> Base metal specified minimum yield stress </param>
        /// <param name="F_u"> Base metal tensile strength </param>
        /// <param name="theta">  Angle of loading for eccentric bolt or weld group </param>
        /// <param name="IgnoreBaseMetalChecks">  Indicates whether weld strength calculation should include base metal checks </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="phiR_n"> Strength of member or connection </returns>

        [MultiReturn(new[] { "phiR_n" })]
        public static Dictionary<string, object> WeldStrength(double l_weld, string WeldType, string WeldLoadTypeId, double t_weld, double F_EXX=70, double A_nBase = 0,
            double F_y = 36, double F_u = 58, double theta = 0, bool IgnoreBaseMetalChecks = false, string Code = "AISC360-10")
        {
            //Default values
            double phiR_n = 0;


            //Calculation logic:

            WeldType weldType;
            bool IsValidString = Enum.TryParse(WeldType, true, out weldType);
            if (IsValidString == true)
            {
                WeldLoadType weldLoadType;
                bool IsValidStringLoadType = Enum.TryParse(WeldLoadTypeId, true, out weldLoadType);
                if (IsValidStringLoadType == true)
                {
                    WeldFactory wf = new WeldFactory();
                    IWeld weld = wf.GetWeld(weldType, F_y, F_u, F_EXX, t_weld, A_nBase, l_weld);
                    phiR_n = weld.GetStrength(weldLoadType, theta, IgnoreBaseMetalChecks);
                }
                else
                {
                    throw new Exception("Weld strength calculation failed. Invalid weld load case (type) designation.");
                }

            }
            else
            {
                throw new Exception("Weld strength calculation failed. Invalid weld type designation.");
            }

            return new Dictionary<string, object>
            {
                { "phiR_n", phiR_n }
 
            };
        }



    }
}


