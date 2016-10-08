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
using System;

#endregion

namespace Steel.AISC.FloorVibrations
{

/// <summary>
///     Joist mode effective weight
///     Category:   Steel.AISC10
/// </summary>
/// 



    public partial class EffectiveProperties 
    {
        /// <summary>
        ///     Joist mode effective weight
        /// </summary>
        /// <param name="w_j">  Joist uniformly distributed (line) service load  </param>
        /// <param name="B_j">  Joist effective panel width </param>
        /// <param name="L_j">  Joist or secondary beam  span </param>
        /// <param name="S_j">  Spacing of joists or secondary beams </param>
        /// <param name="AdjacentSpanWeightIncreaseType">  Identifies whether the effective joist weight can be incretased due to continuous over the column and adjacent span is greater than 0.7 times the span considered, or for joists whether bottom chord is extended. </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="W_j"> Beam mode effective panel weight </returns>

        [MultiReturn(new[] { "W_j" })]
        public static Dictionary<string, object> JoistModeEffectiveWeight(double w_j, double B_j, double L_j, double S_j, string AdjacentSpanWeightIncreaseType, string Code = "AISC. Design Guide 11. 1st Ed")
        {
            //Default values
            double W_j = 0;


            //Calculation logic:


            Kodestruct.Steel.AISC.Entities.Enums.FloorVibrations.AdjacentSpanWeightIncreaseType _AdjacentSpanWeightIncreaseType;
            bool IsValidInputString = Enum.TryParse(AdjacentSpanWeightIncreaseType, true, out _AdjacentSpanWeightIncreaseType);
            if (IsValidInputString == false)
            {
                throw new Exception("Failed to convert string. Specify adjacent span continuity type. Please check input");
            }

            FloorVibrationBeamGirderPanel bgPanel = new FloorVibrationBeamGirderPanel();
            W_j = bgPanel.GetJoistModeEffectiveWeight(w_j, S_j, B_j, L_j, _AdjacentSpanWeightIncreaseType);

            return new Dictionary<string, object>
            {
                { "W_j", W_j }
 
            };
        }



    }
}


