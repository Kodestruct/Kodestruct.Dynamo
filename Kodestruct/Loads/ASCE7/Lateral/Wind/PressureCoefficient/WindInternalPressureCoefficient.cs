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
using Kodestruct.Loads.ASCE7.Entities;
using Kodestruct.Loads.ASCE.ASCE7_10.WindLoads.Building;
using System;
using Kodestruct.Common.CalculationLogger;

#endregion

namespace Loads.ASCE7.Lateral.Wind
{

/// <summary>
///     Wind internal pressure coefficient
///     Category:   Loads.ASCE7v10.Lateral.Wind
/// </summary>
/// 

    public partial class PressureCoefficient 
    {
        /// <summary>
        ///     Wind internal pressure coefficient  accounting for the effects of the internal pressure on the component wind pressures (kip - ft unit system for all inputs and outputs)
        /// </summary>
        /// <param name="WindEnclosureType">  Type of enclosure (open, partially-enclosed or enclosed)  </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="GC_pi"> Product of internal pressure coefficient and gust-effect factor to be used in determination of wind loads for buildings </returns>

        [MultiReturn(new[] { "GC_pi" })]
        public static Dictionary<string, object> WindInternalPressureCoefficient(string WindEnclosureType = "Enclosed", string Code = "ASCE7-10")
        {
            //Default values
            double GC_pi = 0;


            //Calculation logic:

            WindEnclosureType Enclosure;
            
            bool IsValidStringEnclosure = Enum.TryParse(WindEnclosureType, true, out Enclosure);
            if (IsValidStringEnclosure == false)
            {
                throw new Exception("Wind enclosure type is not recognized. Check input string.");
            }

            CalcLog Log  = new CalcLog();
            WindBuilding  building  = new WindBuilding(Log);


            GC_pi = building.GetInternalPressureCoefficient(Enclosure);

            return new Dictionary<string, object>
            {
                { "GC_pi", GC_pi }
 
            };
        }



    }
}


