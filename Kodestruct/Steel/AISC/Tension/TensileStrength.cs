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
using Kodestruct.Steel.AISC.AISC360v10.Tension;

#endregion

namespace Steel.AISC
{

/// <summary>
///     Tensile strength 
///     Category:   Steel.AISC10
/// </summary>
/// 


    public partial class Tension 
    {
        /// <summary>
        ///     Member tensile strength 
        /// </summary>
        /// <param name="F_y">  Specified minimum yield stress </param>
        /// <param name="F_u">  Specified minimum tensile strength   </param>
        /// <param name="A_e">  Effective net area </param>
        /// <param name="A_g">  Gross cross-sectional area of member </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="phiP_n"> Tensile strength </returns>

        [MultiReturn(new[] { "phiP_n" })]
        public static Dictionary<string, object> TensileStrength(double F_y, double F_u, double A_e, double A_g, string Code = "AISC360-10")
        {
            //Default values
            double phiP_n = 0;


            //Calculation logic:
            TensionMember tm = new TensionMember();
            phiP_n = tm.GetDesignTensileCapacity(F_y, F_u, A_g, A_e);

            return new Dictionary<string, object>
            {
                { "phiP_n", phiP_n }
 
            };
        }



    }
}


