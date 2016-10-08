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
using Analysis.Section;
using Kodestruct.Steel.AISC.AISC360v10.Compression;
using Kodestruct.Steel.AISC.Interfaces;
using Kodestruct.Steel.AISC.SteelEntities;
using Kodestruct.Steel.AISC.Steel.Entities;

#endregion

namespace Steel.AISC
{

/// <summary>
///     Torsional and flexural torsional buckling strength
///     Category:   Steel.AISC10
/// </summary>
/// 


    public partial class Compression 
    {
        /// <summary>
        ///     Torsional and flexural torsional buckling strength
        /// </summary>
        /// <param name="Shape">  Shape object  </param>
        /// <param name="L_ex">  Effective unbraced length (KL) for major-axis compression buckling </param>
        /// <param name="L_ey">  Effective unbraced length (KL) for minor-axis compression buckling </param>
        /// <param name="L_ez">  Effective unbraced length (KL) for torsional compression buckling </param>
        /// <param name="F_y">  Specified minimum yield stress </param>
        /// <param name="E">  Modulus of elasticity of steel </param>
        /// <param name="IsRolledMember">  Identifies if member is rolled or built up from individual plates or shapes </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="phiP_n"> Compressive strength </returns>
        /// <returns name="IsApplicable"> Identifies whether the selected limit state is applicable </returns>

        [MultiReturn(new[] { "phiP_n", "IsApplicable" })]
        public static Dictionary<string, object> TorsionalAndFlexuralTorsionalBucklingStrength(CustomProfile Shape,double L_ex,double L_ey,double L_ez,double F_y,double E,bool IsRolledMember,
            string Code = "AISC360-10")
        {
            //Default values
            double phiP_n = 0;
            bool IsApplicable = true;

            //Calculation logic:
            CompressionMemberFactory f = new CompressionMemberFactory();
            ISteelCompressionMember compMember = f.GetCompressionMember(Shape.Section, L_ex, L_ey, L_ez, F_y, E, IsRolledMember);


            SteelLimitStateValue FlexuralTorsionalBuckling = compMember.GetTorsionalAndFlexuralTorsionalBucklingStrength();
            phiP_n = FlexuralTorsionalBuckling.Value;
            IsApplicable = FlexuralTorsionalBuckling.IsApplicable;

            return new Dictionary<string, object>
            {
                { "phiP_n", phiP_n },
                { "IsApplicable", IsApplicable }
            };
        }



    }
}


