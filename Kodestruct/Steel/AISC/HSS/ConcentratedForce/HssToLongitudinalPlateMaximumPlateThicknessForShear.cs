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
using Kodestruct.Steel.AISC.SteelEntities.Materials;
using Kodestruct.Common.Section.Interfaces;
using Kodestruct.Steel.AISC.SteelEntities.Sections;
using Kodestruct.Steel.AISC.Steel.Entities.Sections;
using System;
using Kodestruct.Steel.AISC360v10.HSS.ConcentratedForces;
using Kodestruct.Common.Section.SectionTypes;
using Kodestruct.Common.CalculationLogger;
using Kodestruct.Steel.AISC.Steel.Entities;

#endregion

namespace Steel.AISC.HSS.ConcentratedForce
{

/// <summary>
///     HSS to longitudinal plate maximum plate thickness
///     Category:   Steel.AISC.HSS.ConcentratedForce
/// </summary>
/// 



    public partial class LongitudinalPlate 
    {
        /// <summary>
        ///     HSS to longitudinal plate maximum plate thickness (kip - in unit system for all inputs and outputs)
        /// </summary>
        /// <param name="HssSection">  Section object (Tube or Pipe) </param>
        /// <param name="PlateSection">  Section object (Rectangle) </param>
        /// <param name="F_y">  Specified minimum yield stress of HSS</param>
        /// <param name="F_yp">  Specified minimum yield stress of plate    </param>
        /// <param name="IsTensionHss">  Indicates if HSS member is in tension  </param>
        /// <param name="P_uHss">  Required axial strength of HSS member </param>
        /// <param name="M_uHss">  Required flexural strength of HSS member </param>
        /// <returns name="t_max"> Maximum plate thickness </returns>

        [MultiReturn(new[] { "t_max" })]
        public static Dictionary<string, object> HssToLongitudinalPlateMaximumPlateThicknessForShear(CustomProfile HssSection,CustomProfile PlateSection,
            double F_y,double F_yp,bool IsTensionHss,double P_uHss,double M_uHss)
        {
            //Default values
            double t_max = 0;


            //Calculation logic:
            CalcLog log = new CalcLog();
            SteelLimitStateValue limitState = null;

            //PLATE

            SteelPlateSection pl = null;

            if (!(PlateSection.Section is ISectionRectangular))
            {
                throw new Exception("Failed to convert section. Section needs to be either a Pipe or a Tube. Please check input.");
            }
            else
            {
                SteelMaterial mat = new SteelMaterial(F_y);
                ISectionRectangular rect = PlateSection.Section as ISectionRectangular;
                double t_pl, b_pl = 0.0;
                SectionRectangular rSect;
                if (rect.B <= rect.H)
                {
                    rSect = new SectionRectangular(rect.B, rect.H);
                }
                else
                {
                    rSect = new SectionRectangular(rect.H, rect.B);
                }
                pl = new SteelPlateSection(rSect, mat);

            }


            //HSS

            if (!(HssSection.Section is ISectionHollow))
            {
                throw new Exception("Failed to convert section. Section needs to be either a Pipe or a Tube. Please check input.");
            }
            else
            {
                SteelMaterial mat = new SteelMaterial(F_y);
                if (HssSection.Section is ISectionPipe)
                {
                    SteelChsSection sec = new SteelChsSection(HssSection.Section as ISectionPipe, mat);
                    ChsLongitudinalPlate longitudinalPlateChs = new ChsLongitudinalPlate(sec, pl, log, IsTensionHss,90.0, P_uHss, M_uHss);

                    //limitState = longitudinalPlateChs.ge
                    //phiR_n = limitState.Value;
                    //IsApplicableLimitState = limitState.IsApplicable;
                    t_max = longitudinalPlateChs.GetHssMaximumPlateThicknessForShearLoad().Value;

                }
                else if (HssSection.Section is ISectionTube)
                {
                    SteelRhsSection sec = new SteelRhsSection(HssSection.Section as ISectionTube, mat);
                    RhsLongitudinalPlate longitudinalPlateRhs = new RhsLongitudinalPlate(sec, pl, log, IsTensionHss,90.0, P_uHss, M_uHss);

                    //limitState = longitudinalPlateRhs.GetHssYieldingOrCrippling();
                    //phiR_n = limitState.Value;
                    //IsApplicableLimitState = limitState.IsApplicable;
                    t_max = longitudinalPlateRhs.GetHssMaximumPlateThicknessForShearLoad().Value;
                }
                else
                {
                    throw new Exception("Unsupported type of hollow section. Please use Tube or Pipe.");
                }
            }

            return new Dictionary<string, object>
            {
                { "t_max", t_max }
 
            };
        }



    }
}


