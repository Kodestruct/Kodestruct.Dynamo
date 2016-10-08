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
///     Seismic response coefficient
///     Category:   Loads.ASCE7v10.Lateral.Seismic
/// </summary>
/// 


    public partial class Building 
    {
        /// <summary>
        ///     Seismic response coefficient which multiplied by the building seismic weight, gives the building seismic base shear (lateral pseudo-acceleration, expressed in units of gravity) 
        /// </summary>
        /// <param name="T">  Fundamental period of the building </param>
        /// <param name="S_DS">  Design, 5 percent damped, spectral response acceleration parameter at short periods </param>
        /// <param name="S_D1">  Design, 5 percent damped, spectral response acceleration parameter at a period of 1 s </param>
        /// <param name="T_L">  Long-period transition period </param>
        /// <param name="R">  Resonant response factor </param>
        /// <param name="I_e">  Seismic importance factor  </param>
        /// <param name="S_1">  Mapped MCER, 5 percent damped, spectral response acceleration parameter at a period of 1 s </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="C_s"> Seismic response coefficient which multiplied by the building seismic weight, gives the building seismic base shear (lateral pseudo-acceleration, expressed in units of gravity) </returns>

        [MultiReturn(new[] { "C_s" })]
        public static Dictionary<string, object> SeismicResponseCoefficient(double T, double S_DS, double S_D1, double T_L, double R, double I_e, double S_1, string Code = "ASCE7-10")
        {
            //Default values
            double C_s = 0;


            //Calculation logic:
            CalcLog log = new CalcLog();
            SeismicLateralForceResistingStructure structure = new SeismicLateralForceResistingStructure(log);
            C_s = structure.GetResponseCoefficientCs(T, S_DS, S_D1, T_L, R, I_e, S_1);

            return new Dictionary<string, object>
            {
                { "C_s", C_s }
 
            };
        }


    }
}


