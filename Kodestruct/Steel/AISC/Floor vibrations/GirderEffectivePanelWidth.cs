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
using Kodestruct.Steel.AISC.Entities.Enums.FloorVibrations;
using System;

#endregion

namespace Steel.AISC.FloorVibrations
{

/// <summary>
///     Girder effective panel width
///     Category:   Steel.AISC10
/// </summary>
/// 



    public partial class EffectiveProperties 
    {
        /// <summary>
        ///     Girder effective panel width
        /// </summary>
        /// <param name="L_g">  Girder or primary beam  span </param>
        /// <param name="L_j">  Joist or secondary beam  span </param>
        /// <param name="I_g">  Moment of inertia of girder or primary beam </param>
        /// <param name="I_j">  Moment of inertia of joist or secondary beam </param>
        /// <param name="S_j">  Spacing of joists or secondary beams </param>
        /// <param name="L_floor">  Full uninterrupted length of floor </param>
        /// <param name="BeamLocation">  Distinguishes between beams located at the floor free edge versus all other  beams </param>
        /// <param name="JoistToGirderConnectionType">  Differentiates between beams having connection to girder flange versus connection to girder web </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="B_g"> Girder effective panel width </returns>


        [MultiReturn(new[] { "B_g" })]
        public static Dictionary<string, object> GirderEffectivePanelWidth(double L_g,double L_j,double I_g,double I_j,double S_j,double L_floor,string BeamLocation="Inner",
            string JoistToGirderConnectionType="ConnectionToWeb", string Code = "AISC. Design Guide 11. 1st Ed")
        {
            //Default values
            double B_g = 0;


            //Calculation logic:
            FloorVibrationBeamGirderPanel bgPanel = new FloorVibrationBeamGirderPanel();
            
            BeamFloorLocationType _BeamFloorLocationType;
            bool IsValidLocation = Enum.TryParse(BeamLocation, true, out _BeamFloorLocationType);
            if (IsValidLocation == false)
            {
                throw new Exception("Failed to convert string. Need to specify beam location as Inner or AtFreeEdge. Please check input");
            }

            
            JoistToGirderConnectionType _JoistToGirderConnectionType;
            bool IsValidConnectionString = Enum.TryParse(JoistToGirderConnectionType, true, out _JoistToGirderConnectionType);
            if (IsValidConnectionString == false)
            {
                throw new Exception("Failed to convert string. Need to specify connection type as ConnectionToWeb or PlacementAtTopFlange. Please check input");
            }


            B_g = bgPanel.GetEffectiveGirderWidth(L_g, L_j, I_g, I_j, S_j, L_floor, _BeamFloorLocationType, _JoistToGirderConnectionType);

            return new Dictionary<string, object>
            {
                { "B_g", B_g }
 
            };
        }



    }
}


