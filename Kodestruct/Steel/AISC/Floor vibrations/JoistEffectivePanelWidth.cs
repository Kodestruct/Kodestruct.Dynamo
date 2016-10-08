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
using Kodestruct.Steel.AISC.Entities.FloorVibrations;
using System;
using KodestructEnums = Kodestruct.Steel.AISC;
using Kodestruct.Steel.AISC.Entities.Enums.FloorVibrations;


#endregion

namespace Steel.AISC.FloorVibrations
{

/// <summary>
///     Joist effective panel width
///     Category:   Steel.AISC10
/// </summary>
/// 



    public partial class EffectiveProperties 
    {
        /// <summary>
        ///     Joist effective panel width
        /// </summary>
        /// <param name="fc_prime">  Specified compressive strength of concrete   </param>
        /// <param name="w_c">Weight of concrete per unit volume (pcf)</param>
        /// <param name="h_solid">  Depth of solid portion of concrete slab on metal deck (fill above deck) </param>
        /// <param name="h_rib">  Depth of ribs for corrugated metal deck </param>
        /// <param name="w_r">  Average width of concrete rib or haunch  </param>
        /// <param name="s_r">  Metal deck rib spacing (center to center) </param>
        /// <param name="DeckAtBeamCondition">  Identifies whether deck runs parallel or perpendicular to beam or there is no deck </param>
        /// <param name="L_j">  Joist or secondary beam  span </param>
        /// <param name="I_j">  Moment of inertia of joist or secondary beam </param>
        /// <param name="S_j">  Spacing of joists or secondary beams </param>
        /// <param name="L_floor">  Full uninterrupted length of floor </param>
        /// <param name="BeamLocation">  Distinguishes between beams located at the floor free edge versus all other  beams </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="B_j"> Joist effective panel width </returns>


        [MultiReturn(new[] { "B_j" })]
        public static Dictionary<string, object> JoistEffectivePanelWidth(double fc_prime, double w_c, double h_solid, double h_rib, double w_r, double s_r, string DeckAtBeamCondition, 
            double L_j, double I_j, double S_j, double L_floor, string BeamLocation, string Code = "AISC. Design Guide 11. 1st Ed")
        {
            //Default values
            double B_j = 0;


            //Calculation logic:
            FloorVibrationBeamGirderPanel bgPanel = new FloorVibrationBeamGirderPanel();


            KodestructEnums.DeckAtBeamCondition _DeckAtBeamCondition;
            bool IsValidInputString = Enum.TryParse(DeckAtBeamCondition, true, out _DeckAtBeamCondition);
            if (IsValidInputString == false)
            {
                throw new Exception("Failed to convert string. Need to specify deck direction with respect to beam being considered. Please check input");
            }

            BeamFloorLocationType _BeamFloorLocationType;
            bool IsValidLocation = Enum.TryParse(BeamLocation, true, out _BeamFloorLocationType);
            if (IsValidLocation == false)
            {
                throw new Exception("Failed to convert string. Need to specify beam location as Inner or AtFreeEdge. Please check input");
            }

            B_j = bgPanel.GetEffectiveJoistWidth(fc_prime, w_c, h_solid, h_rib, w_r, s_r, _DeckAtBeamCondition, L_j, I_j, S_j, L_floor, _BeamFloorLocationType);

            return new Dictionary<string, object>
            {
                { "B_j", B_j }
 
            };
        }



    }
}


