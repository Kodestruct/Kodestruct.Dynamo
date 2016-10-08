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
///     Combined mode effective weight
///     Category:   Steel.AISC10
/// </summary>
/// 


    public partial class EffectiveProperties 
    {
        /// <summary>
        ///     Combined mode effective weight
        /// </summary>
        /// <param name="W_j">  Beam mode effective panel weight </param>
        /// <param name="W_g">  Girder mode effective panel weight </param>
        /// <param name="Delta_j">  Joist deflection </param>
        /// <param name="Delta_g">  Girder deflection </param>
        /// <returns name="W_c"> Equivalent combined mode panel weight </returns>

        [MultiReturn(new[] { "W_c" })]
        public static Dictionary<string, object> CombinedModeEffectiveWeight(double W_j, double W_g, double Delta_j, double Delta_g)
        {
            //Default values
            double W_c = 0;


            //Calculation logic:
            FloorVibrationBeamGirderPanel bgPanel = new FloorVibrationBeamGirderPanel();
            W_c = bgPanel.GetCombinedModeEffectiveWeight(Delta_j, Delta_g, W_j, W_g);

            return new Dictionary<string, object>
            {
                { "W_c", W_c }
 
            };
        }




    }
}


