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
using Kodestruct.Common.Entities;
using Kodestruct.Common.Section.Interfaces;
using Kodestruct.Aluminum.AA.Entities;
using Analysis.Section;
using System;
using Kodestruct.Aluminum.AA.AA2015.Flexure;
using Kodestruct.Aluminum.AA.Entities.Section;
using Aluminum.AA.Material;

#endregion

namespace Aluminum.AA
{

/// <summary>
///     Flexural yielding and rupture
///     Category:   Aluminum.AA
/// </summary>
/// 



    public partial class Flexure 
    {
    /// <summary>
    ///     Flexural yielding and rupture
    /// </summary>
    /// <param name="Shape">  Shape object  </param>
    /// <param name="AluminumMaterial">Material object</param>
    /// <param name="FlexuralCompressionLocation">  Identifies whether top or bottom fiber of the section are subject to flexural compression (depending on the sign of moment) </param>
    /// <param name="Code">  Version of design code or standard </param>
    /// <returns name="phiM_n"> Moment strength </returns>

        [MultiReturn(new[] { "phiM_n" })]
        public static Dictionary<string, object> FlexuralYieldingAndRupture(CustomProfile Shape, AluminumMaterial AluminumMaterial,
           string FlexuralCompressionLocation = "Top", string Code = "AA2015")
        {
            //Default values
            double phiM_n = 0;


            ////Calculation logic:
            //MomentAxis Axis;
            ////Calculation logic:
            //bool IsValidStringAxis = Enum.TryParse(BendingAxis, true, out Axis);
            //if (IsValidStringAxis == false)
            //{
            //    throw new Exception("Axis selection not recognized. Check input string.");
            //}

            FlexuralCompressionFiberPosition FlexuralCompression;
            //Calculation logic:
            bool IsValidStringCompressionLoc = Enum.TryParse(FlexuralCompressionLocation, true, out FlexuralCompression);
            if (IsValidStringCompressionLoc == false)
            {
                throw new Exception("Flexural compression location selection not recognized. Check input string.");
            }

            AluminumFlexuralMember m = new AluminumFlexuralMember();

            Kodestruct.Aluminum.AA.AA2015.AluminumMaterial a = new Kodestruct.Aluminum.AA.AA2015.AluminumMaterial(
            AluminumMaterial.Alloy, AluminumMaterial.Temper, AluminumMaterial.ThicknessRange, AluminumMaterial.ProductType);


            m.Section = new AluminumSection(a,Shape.Section);
            AluminumLimitStateValue ls_Y = m.GetFlexuralYieldingStrength(FlexuralCompression);

            AluminumLimitStateValue ls_R = m.GetFlexuralRuptureStrength();

            phiM_n = Math.Min(ls_Y.Value, ls_R.Value);

            return new Dictionary<string, object>
            {
                { "phiM_n", phiM_n }
 
            };
        }


   }
    
}


