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
using Kodestruct.Wood.NDS.NDS2015;
using System;

#endregion

namespace Wood.NDS
{

/// <summary>
///     Adjusted tension design value
///     Category:   Wood.NDS
/// </summary>
/// 



    public partial class DesignValues
    {
        /// <summary>
        ///     Adjusted tension design value
        /// </summary>
        /// <param name="F_t">  Reference tension design value parallel to grain  </param>
        /// <param name="C_M_Ft">  Wet service factor </param>
        /// <param name="C_t">  Temperature factor </param>
        /// <param name="C_F_Ft">  Size factor </param>
        /// <param name="C_i_Ft">  Incising factor for dimension lumber </param>
        /// <param name="lambda">  Time effect factor  </param>
        /// <param name="WoodMemberType">  Distinguishes between dimensional lumber, timber,glulam etc. </param>
        /// <param name="Code">  Identifies the code or standard used for calculations </param>
        /// <returns name="F_t_prime"> Adjusted tension design value parallel to grain  </returns>

        [MultiReturn(new[] { "F_t_prime" })]
        public static Dictionary<string, object> AdjustedTensionDesignValue(double F_t,double C_M_Ft,double C_t,double C_F_Ft,double C_i_Ft,double lambda,
            string WoodMemberType = "SawnDimensionLumber", string Code = "NDS2015")
        {
            //Default values
            double F_t_prime = 0;


            //Calculation logic:
            if (WoodMemberType.Contains("Sawn") && WoodMemberType.Contains("Lumber"))
            {
                DimensionalLumber m = new DimensionalLumber();
                F_t_prime = m.GetAdjustedTensionValue(F_t, C_M_Ft, C_t, C_F_Ft, C_i_Ft, lambda);
            }
            else
            {
                throw new Exception("Wood member type not supported.");
            }

            return new Dictionary<string, object>
            {
                { "F_t_prime", F_t_prime }
 
            };
        }



    }
}


