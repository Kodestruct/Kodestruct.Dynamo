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
///     Modal damping
///     Category:   Steel.AISC10
/// </summary>
/// 



    public partial class Acceleration
    {
        /// <summary>
        ///     Modal damping
        /// </summary>
        /// <param name="DampingComponents">  List of components contributing to damping </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="beta_floor"> Floor modal damping ratio </returns>

        [MultiReturn(new[] { "beta_floor" })]
        public static Dictionary<string, object> ModalDamping(List<string> DampingComponents, string Code = "AISC. Design Guide 11. 1st Ed")
        {
            //Default values
            double beta_floor = 0;


            //Calculation logic:
            FloorVibrationBeamGirderPanel bgPanel = new FloorVibrationBeamGirderPanel();
            beta_floor = bgPanel.GetFloorModalDampingRatio(DampingComponents);

            return new Dictionary<string, object>
            {
                { "beta_floor", beta_floor }
 
            };
        }




    }
}


