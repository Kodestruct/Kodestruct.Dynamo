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
using Kodestruct.Common.CalculationLogger;

#endregion

namespace Loads.ASCE7.Lateral.Seismic
{

/// <summary>
///     Seismic fundamental period upper  limit coefficient
///     Category:   Loads.ASCE7v10.Lateral.Seismic
/// </summary>
/// 


    public partial class BuildingFundamentalPeriod 
    {
        /// <summary>
        ///     Coefficient  for upper limit on  calculated period  
        /// </summary>
        /// <param name="S_D1">  Design, 5 percent damped, spectral response acceleration parameter at a period of 1 s </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="C_u"> Coefficient for upper limit on  calculated period </returns>

        [MultiReturn(new[] { "C_u" })]
        public static Dictionary<string, object> SeismicFundamentalPeriodUpperLimitCoefficient(double S_D1, string Code = "ASCE7-10")
        {
            //Default values
            double C_u = 0;


            //Calculation logic:

            CalcLog log = new CalcLog();
            Kodestruct.Loads.ASCE.ASCE7_10.SeismicLoads.Building building = new Kodestruct.Loads.ASCE.ASCE7_10.SeismicLoads.Building(null, log);
            C_u = building.GetCoefficientForUpperBoundOnCalculatedPeriod(S_D1);

            return new Dictionary<string, object>
            {
                { "C_u", C_u }
 
            };
        }



    }
}


