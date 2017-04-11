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
using Aluminum.AA;
using Aluminum.AA.Material;
using Kodestruct.Aluminum.AA.Entities.Enums;

#endregion

namespace Aluminum.AA
{

/// <summary>
///     Flexural local buckling
///     Category:   Aluminum.AA
/// </summary>
/// 



    public partial class Flexure 
    {
        /// <summary>
        ///     Flexural local buckling
        /// </summary>
        /// <param name="Shape">  Shape object  </param>
        /// <param name="AluminumMaterial">  Material object  </param>
        /// <param name="b">  Width of element </param>
        /// <param name="t">  Thickness of element plate or element wall  </param>
        /// <param name="LateralSupportType">  Type of support for section local and lateral-torsional buckling </param>
        /// <param name="FlexuralCompressionLocation">  Identifies whether top or bottom fiber of the section are subject to flexural compression (depending on the sign of moment) </param>
        /// <param name="WeldCaseId">  Distinguishes between welded and non-welded element type for aluminum </param>
        /// <param name="SectionSubElementType"> Identifies whether section sub-element is flat or curved </param>
        /// <param name="Code">  Version of design code or standard </param>
        /// <returns name="phiM_n"> Moment strength </returns>

        [MultiReturn(new[] { "phiM_n" })]
        public static Dictionary<string, object> FlexuralLocalBuckling(CustomProfile Shape, AluminumMaterial AluminumMaterial,double b,double t, string LateralSupportType,
            string FlexuralCompressionLocation = "Top", string WeldCaseId = "NotAffected", string SectionSubElementType="Flat", string Code = "AA2015")
        {
            //Default values
            double phiM_n = 0;


            //Calculation logic:

            FlexuralCompressionFiberPosition FlexuralCompression;
            //Calculation logic:
            bool IsValidStringCompressionLoc = Enum.TryParse(FlexuralCompressionLocation, true, out FlexuralCompression);
            if (IsValidStringCompressionLoc == false)
            {
                throw new Exception("Flexural compression location selection not recognized. Check input string.");
            }


            Kodestruct.Aluminum.AA.Entities.Enums.LateralSupportType LateralSupportTypeParsed;
            bool IsValidLateralSupportType = Enum.TryParse(LateralSupportType, true, out LateralSupportTypeParsed);
            if (IsValidLateralSupportType == false)
            {
                throw new Exception("Failed to convert string. LateralSupportType shall be OneEdge or BothEdges. Please check input");
            }

            
            WeldCase WeldCaseParsed;
            bool IsValidWeldCase = Enum.TryParse(WeldCaseId, true, out WeldCaseParsed);
            if (IsValidWeldCase == false)
            {
                throw new Exception("Failed to convert string. WeldCase shall be NotAffected or WeldAffected. Please check input");
            }

            
            SubElementType SubElementType;
            bool IsValidInputString = Enum.TryParse(SectionSubElementType, true, out SubElementType);
            if (IsValidInputString == false)
            {
                throw new Exception("Failed to convert string. Currently supported value is Flat. Please check input");
            }



            AluminumFlexuralMember m = new AluminumFlexuralMember();

            Kodestruct.Aluminum.AA.AA2015.AluminumMaterial a = new Kodestruct.Aluminum.AA.AA2015.AluminumMaterial(
            AluminumMaterial.Alloy, AluminumMaterial.Temper, AluminumMaterial.ThicknessRange, AluminumMaterial.ProductType);


            m.Section = new AluminumSection(a, Shape.Section);
            AluminumLimitStateValue ls_LB = m.GetLocalBucklingStrength(b, t, LateralSupportTypeParsed, FlexuralCompression, WeldCaseParsed, SubElementType);

            phiM_n = ls_LB.Value;

            return new Dictionary<string, object>
            {
                { "phiM_n", phiM_n }
 
            };
        }



    }
}


