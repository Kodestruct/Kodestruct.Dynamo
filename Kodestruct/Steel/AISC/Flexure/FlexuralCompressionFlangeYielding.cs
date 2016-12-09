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
using Dynamo.Graph.Nodes;
using Analysis.Section;
using Kodestruct.Steel.AISC.AISC360v10.Flexure;
using Kodestruct.Steel.AISC.SteelEntities.Materials;
using Kodestruct.Steel.AISC.SteelEntities;
using Kodestruct.Common.Section.Interfaces;
using Kodestruct.Common.Entities;
using Kodestruct.Steel.AISC.Interfaces;
using System;
using Kodestruct.Steel.AISC.Steel.Entities;

#endregion

namespace Steel.AISC
{

/// <summary>
///     Flexural compression flange yielding 
///     Category:   Steel.AISC10
/// </summary>
/// 

    public partial class Flexure 
    {
        /// <summary>
        ///     Flexural compression flange yielding (kip - in unit system for all inputs and outputs)
        /// </summary>
        /// <param name="Shape">  Shape object </param>
        /// <param name="BendingAxis">  Distinguishes between bending with respect to section x-axis vs x-axis </param>
        /// <param name="F_y">  Specified minimum yield stress </param>
        /// <param name="FlexuralCompressionLocation">  Identifies whether top or bottom fiber of the section are subject to flexural compression (depending on the sign of moment) </param>
        /// <param name="E">  Modulus of elasticity of steel </param>
        ///  <param name="IsRolledMember">  Identifies if member is rolled or built up from individual plates or shapes </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="phiM_n"> Moment strength </returns>
        /// <returns name="IsApplicableLimitState"> Identifies whether the selected limit state is applicable </returns>

        [MultiReturn(new[] { "phiM_n","IsApplicableLimitState" })]
        public static Dictionary<string, object> FlexuralCompressionFlangeYielding(CustomProfile Shape, double F_y, string BendingAxis="XAxis",
            string FlexuralCompressionLocation = "Top", double E = 29000, bool IsRolledMember = true, string Code = "AISC360-10")
        {
            //Default values
            double phiM_n = 0;
            bool IsApplicableLimitState = false;


            //Calculation logic:

            MomentAxis Axis;
            //Calculation logic:
            bool IsValidStringAxis = Enum.TryParse(BendingAxis, true, out Axis);
            if (IsValidStringAxis == false)
            {
                throw new Exception("Axis selection not recognized. Check input string.");
            }

            FlexuralCompressionFiberPosition FlexuralCompression;
            //Calculation logic:
            bool IsValidStringCompressionLoc = Enum.TryParse(FlexuralCompressionLocation, true, out FlexuralCompression);
            if (IsValidStringCompressionLoc == false)
            {
                throw new Exception("Flexural compression location selection not recognized. Check input string.");
            }


            SteelMaterial mat = new SteelMaterial(F_y, E);
            FlexuralMemberFactory factory = new FlexuralMemberFactory();
            ISteelBeamFlexure beam = factory.GetBeam(Shape.Section, mat, null, Axis, FlexuralCompression, IsRolledMember);

            SteelLimitStateValue CFY =
            beam.GetFlexuralCompressionFlangeYieldingStrength(FlexuralCompression);
            phiM_n = CFY.Value;

            IsApplicableLimitState = CFY.IsApplicable;

            return new Dictionary<string, object>
            {
                { "phiM_n", phiM_n }
                ,{ "IsApplicableLimitState", IsApplicableLimitState }
 
            };
        }



    }
}


