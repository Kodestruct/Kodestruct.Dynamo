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
using Kodestruct.Loads.ASCE.ASCE7_10.WindLoads;
using Kodestruct.Common.CalculationLogger;

#endregion

namespace Loads.ASCE7.Lateral.Wind
{

/// <summary>
///     Wind directionality factor
///     Category:   Loads.ASCE7v10.Lateral.Wind
/// </summary>
/// 


    public partial class StructureParameters 
    {
        /// <summary>
        ///     Wind directionality factor
        /// </summary>
        /// <param name="WindStructureDescriptionForExposure">  Description of the structure for exposure category determination </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="K_d"> Wind directionality factor </returns>

        [MultiReturn(new[] { "K_d" })]
        public static Dictionary<string, object> WindDirectionalityFactor(string WindStructureDescriptionForExposure = "MWFRS", string Code = "ASCE7-10")
        {
            //Default values
            double K_d = 0;


            //Calculation logic:
            WindStructure structure = new WindStructure(new CalcLog());
            K_d = structure.GetKd(WindStructureDescriptionForExposure);

            return new Dictionary<string, object>
            {
                { "K_d", K_d }
 
            };
        }

    }
}


