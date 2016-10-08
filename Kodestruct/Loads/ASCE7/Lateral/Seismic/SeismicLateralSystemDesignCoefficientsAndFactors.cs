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
using Kodestruct.Loads.ASCE7.Entities;
using System;
using Kodestruct.Loads.ASCE.ASCE7_10.SeismicLoads;
using Kodestruct.Common.CalculationLogger;

#endregion

namespace Loads.ASCE7.Lateral.Seismic
{

/// <summary>
///     Seismic lateral system design coefficients and factors
///     Category:   Loads.ASCE7v10.Lateral.Seismic
/// </summary>
/// 


    public partial class Building 
    {
        /// <summary>
        ///     Seismic design Coefficients and Factors
        /// </summary>
        /// <param name="SeismicLateralSystemId">  Id of the lateral system from ASCE7-10 table 12.2-1 </param>
        /// <param name="SeismicDesignCategory"> Seismic design category (SDC)</param>
        /// <param name="CheckSystemApplicabilityForSDC">Indicates if applicability of selected system for a given SDC is checked</param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="R"> Resonant response factor </returns>
        /// <returns name="C_d"> Deflection amplification factor </returns>
        /// <returns name="Omega_0"> Overstrength factor </returns>

        [MultiReturn(new[] { "R","C_d","Omega_0" })]
        public static Dictionary<string, object> SeismicLateralSystemDesignCoefficientsAndFactors(string SeismicLateralSystemId,
            string SeismicDesignCategory = "B", bool CheckSystemApplicabilityForSDC = false, string Code = "ASCE7-10")
        {
            //Default values
            double R = 0;
            double C_d = 0;
            double Omega_0 = 0;


            //Calculation logic:
            BuildingLateralSystem bls = null;
            CalcLog log = new CalcLog();

            if (CheckSystemApplicabilityForSDC == true)
            {
                SeismicDesignCategory _SeismicDesignCategory;
                bool IsValidSDC = Enum.TryParse(SeismicDesignCategory, out _SeismicDesignCategory);

                bls = new BuildingLateralSystem(SeismicLateralSystemId, _SeismicDesignCategory, log);
            }
            else
            {
                bls = new BuildingLateralSystem(SeismicLateralSystemId, log);
            }
            R = bls.R;
            C_d = bls.Cd;
            Omega_0 = bls.Omega_0;

            return new Dictionary<string, object>
            {
                { "R", R }
                ,{ "C_d", C_d }
                ,{ "Omega_0", Omega_0 }
 
                };
        }


    }
}


