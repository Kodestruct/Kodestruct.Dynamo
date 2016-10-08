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
///     Adjusted compression design value
///     Category:   Wood.NDS
/// </summary>
/// 


    public partial class DesignValues
    {
        /// <summary>
        ///     Adjusted compression design value
        /// </summary>
        /// <param name="F_c">  Out-of-plane seismic forces for concrete and masonry walls  </param>
        /// <param name="C_M_Fc">  Wet service factor </param>
        /// <param name="C_t">  Temperature factor </param>
        /// <param name="C_F_Fc">  Size factor </param>
        /// <param name="C_i_Fc">  Incising factor for dimension lumber </param>
        /// <param name="C_P">  Column stability factor </param>
        /// <param name="lambda">  Time effect factor  </param>
        /// <param name="WoodMemberType">  Distinguishes between dimensional lumber, timber,glulam etc. </param>
        /// <param name="Code">  Identifies the code or standard used for calculations </param>
        /// <returns name="F_c_prime"> Adjusted LRFD compression design value parallel to grain  </returns>

        [MultiReturn(new[] { "F_c_prime" })]
        public static Dictionary<string, object> AdjustedCompressionDesignValue(double F_c,double C_M_Fc,double C_t,double C_F_Fc,double C_i_Fc,double C_P,
            double lambda, string WoodMemberType = "SawnDimensionLumber", string Code = "NDS2015")
        {
            //Default values
            double F_c_prime = 0;


            //Calculation logic:
            if (WoodMemberType.Contains("Sawn") && WoodMemberType.Contains("Lumber"))
            {
                DimensionalLumber m = new DimensionalLumber();
                F_c_prime = m.GetAdjustedCompressionDesignValue(F_c, C_M_Fc, C_t, C_F_Fc, C_i_Fc, C_P, lambda);
            }
            else
            {
                throw new Exception("Wood member type not supported.");
            }

            return new Dictionary<string, object>
            {
                { "F_c_prime", F_c_prime }
 
            };
        }


    }
}


