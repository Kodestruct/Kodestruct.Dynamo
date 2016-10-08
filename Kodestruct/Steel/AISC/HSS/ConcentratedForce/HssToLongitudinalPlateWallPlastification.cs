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
using Kodestruct.Steel.AISC360v10.HSS.ConcentratedForces;
using Kodestruct.Steel.AISC.SteelEntities.Sections;
using Kodestruct.Common.Section.Interfaces;
using Kodestruct.Steel.AISC.SteelEntities.Materials;
using Analysis.Section.SectionTypes;
using Kodestruct.Common.CalculationLogger;
using Kodestruct.Steel.AISC.Steel.Entities;
using Kodestruct.Steel.AISC.Steel.Entities.Sections;
using KodestructSection = Kodestruct.Common.Section.SectionTypes;
using System;

#endregion

namespace Steel.AISC.HSS.ConcentratedForce
{

/// <summary>
    ///     HSS to longitudinal plate wall plastification of member
///     Category:   Steel.AISC.HSS.ConcentratedForce
/// </summary>
/// 



    public partial class LongitudinalPlate 
    {
        /// <summary>
        ///     HSS to longitudinal plate wall plastification of member
        /// </summary>
        /// <param name="HssSection">  Section object (Tube or Pipe) </param>
        /// <param name="PlateSection">  Section object (Rectangle) </param>
        /// <param name="F_y">  Specified minimum yield stress of HSS</param>
        /// <param name="F_yp">  Specified minimum yield stress of plate    </param>
        /// <param name="P_uHss">  Required axial strength of HSS member </param>
        /// <param name="M_uHss">  Required flexural strength of HSS member </param>
        /// <param name="theta"> Angle between HSS member and plate </param>
        /// <param name="IsTensionHss">  Indicates if HSS member is in tension  </param>
        /// <param name="IsThroughPlate">Indicates if longitudinal HSS plate is a through plate welded on both faces of member</param>
        /// <returns name="phiR_n"> Strength of member or connection </returns>
        /// <returns name="IsApplicableLimitState"> Identifies whether the selected limit state is applicable </returns>

        [MultiReturn(new[] { "phiR_n","IsApplicableLimitState" })]
        public static Dictionary<string, object> HssToLongitudinalPlateWallPlastification(CustomProfile HssSection,CustomProfile PlateSection,
            double F_y,double F_yp, double P_uHss,  double M_uHss,double theta=90.0, bool IsTensionHss=false, bool IsThroughPlate=false)
        {
            //Default values
            double phiR_n = 0;
            bool IsApplicableLimitState = false;


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
                KodestructSection.SectionRectangular rSect;
                if (rect.B <= rect.H)
                {
                    rSect = new KodestructSection.SectionRectangular(rect.B, rect.H);
                }
                else
                {
                    rSect = new KodestructSection.SectionRectangular(rect.H, rect.B);
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
                    ChsLongitudinalPlate longitudinalPlateChs = new ChsLongitudinalPlate(sec, pl, log, IsTensionHss,theta, P_uHss, M_uHss);

                    limitState = longitudinalPlateChs.GetHssWallPlastificationStrengthUnderAxialLoad();
                    if (IsThroughPlate == false)
                    {
                        phiR_n = limitState.Value;
                    }
                    else
                    {
                        phiR_n = 2.0* limitState.Value; //analogous to Rectangular HSS provsions
                    }
                    
                    IsApplicableLimitState = limitState.IsApplicable;
                    

                }
                else if (HssSection.Section is ISectionTube)
                {
                    SteelRhsSection sec = new SteelRhsSection(HssSection.Section as ISectionTube, mat);
                    if (IsThroughPlate == false)
                    {
                        RhsLongitudinalPlate longitudinalPlateRhs = new RhsLongitudinalPlate(sec, pl,  log, IsTensionHss,theta, P_uHss, M_uHss);

                        limitState = longitudinalPlateRhs.GetHssWallPlastificationStrengthUnderAxialLoad();
                        phiR_n = limitState.Value;
                        IsApplicableLimitState = limitState.IsApplicable;  
                    }
                    else
                    {
                        RhsLongitudinalThroughPlate longitudinalPlateRhs = new RhsLongitudinalThroughPlate(sec, pl, log, IsTensionHss,theta, P_uHss, M_uHss);

                        limitState = longitudinalPlateRhs.GetHssWallPlastificationStrengthUnderAxialLoad();
                        phiR_n = limitState.Value;
                        IsApplicableLimitState = limitState.IsApplicable;
                    }
                   
                }
                else
                {
                    throw new Exception("Unsupported type of hollow section. Please use Tube or Pipe.");
                }
            }

            return new Dictionary<string, object>
            {
                { "phiR_n", phiR_n }
,{ "IsApplicableLimitState", IsApplicableLimitState }
 
            };
        }




    }
}


