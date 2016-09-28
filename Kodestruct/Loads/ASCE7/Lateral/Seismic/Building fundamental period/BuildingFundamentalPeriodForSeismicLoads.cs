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
using Kodestruct.Common.CalculationLogger;

#endregion

namespace Loads.ASCE7.Lateral.Seismic
{

/// <summary>
///     Building fundamental period for seismic loads
///     Category:   Loads.ASCE7v10.Lateral.Seismic
/// </summary>
/// 

    public partial class BuildingFundamentalPeriod 
    {
        /// <summary>
        ///     Fundamental period of the building  used to account for building dynamic response to base accelerations
        /// </summary>
        /// <param name="C_u">  Coefficient for upper limit on  calculated period </param>
        /// <param name="T_a">  Approximate fundamental period of the building </param>
        /// <param name="T_calc">Calculated fundamental period of the building</param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="T"> Fundamental period of the building </returns>

        [MultiReturn(new[] { "T" })]
        public static Dictionary<string, object> BuildingFundamentalPeriodForSeismicLoads(double C_u, double T_a, double T_calc, string Code = "ASCE7-10")
        {
            //Default values
            double T = 0;


            //Calculation logic:

            CalcLog log = new CalcLog();
            Kodestruct.Loads.ASCE.ASCE7_10.SeismicLoads.Building building = new Kodestruct.Loads.ASCE.ASCE7_10.SeismicLoads.Building(null, log);
            double Tmax = building.GetMaximumPeriod(T_a, C_u);
            T = building.GetFundamentalPeriod(T_calc, Tmax);

            return new Dictionary<string, object>
            {
                { "T", T }
 
            };
        }



    }
}


