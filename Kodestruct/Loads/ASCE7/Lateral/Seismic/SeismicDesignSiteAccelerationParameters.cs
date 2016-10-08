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
using Kodestruct.Loads.ASCE.ASCE7_10.SeismicLoads;
using Kodestruct.Common.CalculationLogger;

#endregion

namespace Loads.ASCE7.Lateral.Seismic
{

/// <summary>
///     Seismic design site acceleration parameters
///     Category:   Loads.ASCE7v10.Lateral.Seismic
/// </summary>
/// 


    public partial class AccelerationParameters 
    {
        /// <summary>
        ///     Adjusted spectral response acceleration parameters 
        /// </summary>
        /// <param name="S_S">  Mapped MCER, 5 percent damped, spectral response acceleration parameter at short periods </param>
        /// <param name="S_1">  Mapped MCER, 5 percent damped, spectral response acceleration parameter at a period of 1 s </param>
        /// <param name="F_a">  Short-period site coefficient (at 0.2 s-period) </param>
        /// <param name="F_v">  Long-period site coefficient (at 1.0 s-period) </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="S_DS"> Design, 5 percent damped, spectral response acceleration parameter at short periods </returns>
        /// <returns name="S_D1"> Design, 5 percent damped, spectral response acceleration parameter at a period of 1 s </returns>

        [MultiReturn(new[] { "S_DS","S_D1" })]
        public static Dictionary<string, object> SeismicDesignSiteAccelerationParameters(double S_S, double S_1, double F_a, double F_v, string Code = "ASCE7-10")
        {
            //Default values
            double S_DS = 0;
            double S_D1 = 0;


            //Calculation logic:
            CalcLog log = new CalcLog();
            SeismicLocation loc = new SeismicLocation(log);

            S_DS = loc.CalculateSDS(S_S, F_a);
            S_D1 = loc.CalculateSD1(S_1, F_v);

            return new Dictionary<string, object>
                {
                { "S_DS", S_DS }
                ,{ "S_D1", S_D1 }
 
                };
                }

    }
}


