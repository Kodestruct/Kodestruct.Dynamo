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
using Kodestruct.Steel.AISC360v10.Connections.AffectedElements;

#endregion

namespace Steel.AISC.Connection
{

/// <summary>
///     Block shear strength
///     Category:   Steel.AISC.Connection
/// </summary>
/// 


    public partial class AffectedElements 
    {
        /// <summary>
        ///    Calculates Block shear strength
        /// </summary>
        /// <param name="A_gv">  Gross area subject to shear </param>
        /// <param name="A_nv">  Net area subject to shear </param>
        /// <param name="A_nt">  Net area subject to tension </param>
        /// <param name="F_y">  Specified minimum yield stress </param>
        /// <param name="F_u">  Specified minimum tensile strength   </param>
        /// <param name="StressDistibutionType">  Type of stress distribution in connected element </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="phiR_n"> Strength of member or connection </returns>

        [MultiReturn(new[] { "phiR_n" })]
        public static Dictionary<string, object> BlockShearStrength(double A_gv, double A_nv, double A_nt, double F_y, double F_u, string StressDistibutionType, 
            string Code = "AISC360-10")
        {
            //Default values

            bool StressIsUniform=false;
            if (StressDistibutionType=="Uniform")
            {
                StressIsUniform = true;
            }

            //Calculation logic:
            AffectedElement element = new AffectedElement(F_y, F_u);
            double phiR_n = element.GetBlockShearStrength(A_gv, A_nv, A_nt, StressIsUniform);

            return new Dictionary<string, object>
            {
                { "phiR_n", phiR_n }
 
            };
        }



    }
}


