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
using Analysis.Section;
using Kodestruct.Steel.AISC.AISC360v10.Compression;
using Kodestruct.Steel.AISC.Interfaces;
using Kodestruct.Steel.AISC.SteelEntities;
using Kodestruct.Steel.AISC.Steel.Entities;

#endregion

namespace Steel.AISC
{

    /// <summary>
    ///     Flexural buckling strength
    ///     Category:   Steel.AISC10
    /// </summary>
    /// 



    public partial class Compression
    {
        /// <summary>
        ///     Flexural buckling critical stress (kip - in unit system for all inputs and outputs)
        /// </summary>
        /// <param name="L_e">  Effective unbraced length (KL) for compression buckling </param>
        /// <param name="r">  Radius of gyration </param>
        /// <param name="F_y">  Specified minimum yield stress </param>
        /// <param name="E">  Modulus of elasticity of steel </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="phiF_cr"> Critical stress in compression </returns>


        [MultiReturn(new[] { "phiF_cr" })]
        public static Dictionary<string, object> FlexuralBucklingCriticalStress(double L_e, double r, double F_y, double Q=1.0, double E=29000.0,
            string Code = "AISC360-10")
        {
            //Default values
            double phiF_cr = 0;


            FlexuralBucklingCriticalStressCalculator fcalc = new FlexuralBucklingCriticalStressCalculator();
            double Slenderness = L_e / r;
            double Fe = fcalc.GetF_e(E, Slenderness);
            phiF_cr =0.9*fcalc.GetCriticalStressFcr(Fe, F_y, Q);

            return new Dictionary<string, object>
            {
                { "phiF_cr", phiF_cr }
            };
        }


    }
}


