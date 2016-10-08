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
using steel = Kodestruct.Steel.AISC;
using Kodestruct.Steel.AISC.AISC360v10.Composite;
using System;

#endregion

namespace Steel.AISC.Composite
{

/// <summary>
///     Headed anchor position and group factors
///     Category:   Kodestruct.Steel.AISC_10.Composite
/// </summary>
/// 


    public partial class Anchor 
    {
        /// <summary>
        ///    Calculates Strength of headed stud anchor
        /// </summary>
        /// <param name="HeadedAnchorDeckCondition">  Identifies whether deck runs parallel or perpendicular to beam or there is no deck </param>
        /// <param name="HeadedAnchorWeldCase">  Identifies the type of welding between the anchor and shape (through deck or not) </param>
        /// <param name="N_saRib">  Number of steel headed stud anchors occupying the same decking rib </param>
        /// <param name="e_mid_ht">  Distance from the edge of steel headed stud anchor shank to the steel deck web  </param>
        /// <param name="h_r">  Nominal height of rib   </param>
        /// <param name="w_r">  Average width of concrete rib or haunch  </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="R_g"> Coefficient to account for group effect   </returns>
        /// <returns name="R_p"> Position effect factor for shear anchors   </returns>

        [MultiReturn(new[] { "R_g","R_p" })]
        public static Dictionary<string, object> HeadedAnchorPositionAndGroupFactors(string HeadedAnchorDeckCondition,string HeadedAnchorWeldCase,double N_saRib,double e_mid_ht,double h_r,
            double w_r, string Code = "AISC360-10")
        {
            //Default values
            double R_g = 0;
            double R_p = 0;


            //Calculation logic:
            steel.DeckAtBeamCondition deckCondition;
            bool IsValidHeadedAnchorDeckCondition = Enum.TryParse(HeadedAnchorDeckCondition, true, out deckCondition);
            if (IsValidHeadedAnchorDeckCondition == false)
            {

                throw new Exception("Headed anchor position and group factor calculation failed. Invalid string provided for HeadedAnchorDeckCondition.");
            }

            steel.HeadedAnchorWeldCase studWeld;
            bool IsValidHeadedAnchorWeldCase = Enum.TryParse(HeadedAnchorWeldCase, true, out studWeld);
            if (IsValidHeadedAnchorWeldCase == false)
            {
                throw new Exception("Headed anchor position and group factor calculation failed. Invalid string provided for HeadedAnchorWeldCase.");
            }

            HeadedAnchor a = new HeadedAnchor();
            R_g = a.GetGroupFactorR_g(deckCondition, studWeld, N_saRib, h_r, w_r);
            R_p = a.GetPlacementFactorR_p(deckCondition, studWeld, e_mid_ht);

            return new Dictionary<string, object>
                {
                    { "R_g", R_g }
                    ,{ "R_p", R_p }
                };
                }



    }
}


