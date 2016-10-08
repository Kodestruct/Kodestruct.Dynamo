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
using Kodestruct.Loads.ASCE.ASCE7_10.SeismicLoads;
using Kodestruct.Common.CalculationLogger;

#endregion

namespace Loads.ASCE7.Lateral.Seismic
{

/// <summary>
///     Seismic story forces
///     Category:   Loads.ASCE7v10.Lateral.Seismic
/// </summary>
/// 


    public partial class Building 
    {
        /// <summary>
        ///     Fundamental period of the building  used to account for building dynamic response to base accelerations
        /// </summary>
        /// <param name="T">Fundamental period of the building used for design</param>
        /// <param name="C_s">Seismic response coefficient which multiplied by the building seismic weight, gives the building seismic base shear (lateral pseudo-acceleration, expressed in units of gravity)</param>
        /// <param name="StoryElevationsFromBase">  List of elevations (ft) fom building base of individual stories or lumped masses </param>
        /// <param name="StoryWeights">  List of story weights  (lumped masses) corresponding to the list of story elevations </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="StoryForces"> List of individual story forces </returns>

        [MultiReturn(new[] { "StoryForces" })]
        public static Dictionary<string, object> SeismicStoryForces(double T, double C_s, List<double> StoryElevationsFromBase, List<double> StoryWeights, string Code = "ASCE7-10")
        {
            //Default values
            List<double> StoryForces = new List<double>();


            //Calculation logic:
            CalcLog log = new CalcLog();
            SeismicLateralForceResistingStructure structure = new SeismicLateralForceResistingStructure(log);
            StoryForces = structure.CalculateSeismicLoads(T, C_s, StoryElevationsFromBase, StoryWeights);

            return new Dictionary<string, object>
            {
                { "StoryForces", StoryForces }
            };
        }

    }
}


