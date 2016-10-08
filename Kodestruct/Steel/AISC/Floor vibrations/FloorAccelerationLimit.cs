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

#endregion

namespace Steel.AISC.FloorVibrations
{

/// <summary>
///     Floor acceleration limit
///     Category:   Steel.AISC10
/// </summary>
/// 



    public partial class Acceleration 
    {
        /// <summary>
        ///     Floor acceleration limit
        /// </summary>
        /// <param name="FloorSeviceOccupancyId">  Indicates type of floor occupancy used for vibration checks </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="a_g_RatioMax"> Walking human tolerance criteria. Ratio of maximum acceleration  and acceleration of gravity </returns>

        [MultiReturn(new[] { "a_g_RatioMax" })]
        public static Dictionary<string, object> FloorAccelerationLimit(string FloorSeviceOccupancyId, string Code = "AISC. Design Guide 11. 1st Ed")
        {
            //Default values
            double a_g_RatioMax = 0;


            //Calculation logic:
            FloorVibrationBeamGirderPanel bgPanel = new FloorVibrationBeamGirderPanel();
            a_g_RatioMax = bgPanel.GetAccelerationLimit(FloorSeviceOccupancyId);

            return new Dictionary<string, object>
            {
                { "a_g_RatioMax", a_g_RatioMax }
 
            };
        }

    }
}


