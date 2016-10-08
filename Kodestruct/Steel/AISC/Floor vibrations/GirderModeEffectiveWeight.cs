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
using Kodestruct.Steel.AISC.Entities.FloorVibrations;

#endregion

namespace Steel.AISC.FloorVibrations
{

/// <summary>
///     Girder mode effective weight
///     Category:   Steel.AISC10
/// </summary>
/// 


    public partial class EffectiveProperties 
    {
        /// <summary>
        ///     Girder mode effective weight
        /// </summary>
        /// <param name="w_g">  Girder uniformly distributed (line) service load  </param>
        /// <param name="B_g">  Girder effective panel width </param>
        /// <param name="L_g">  Girder or primary beam  span </param>
        /// <param name="L_jAverage">  Average joist or secondary beam  span </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="W_g"> Girder mode effective panel weight </returns>


        [MultiReturn(new[] { "W_g" })]
        public static Dictionary<string, object> GirderModeEffectiveWeight(double w_g, double B_g, double L_g, double L_jAverage, 
            string Code = "AISC. Design Guide 11. 1st Ed")
        {
            //Default values
            double W_g = 0;


            //Calculation logic:
            FloorVibrationBeamGirderPanel bgPanel = new FloorVibrationBeamGirderPanel();
            W_g = bgPanel.GetGirderModeEffectiveWeight(w_g, L_g, B_g, L_jAverage);

            return new Dictionary<string, object>
            {
                { "W_g", W_g }
 
            };
        }



    }
}


