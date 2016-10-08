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
using System;

#endregion

namespace Loads.ASCE7.Lateral.Seismic
{

/// <summary>
///     Seismic importance factor
///     Category:   Loads.ASCE7v10.Lateral.Seismic
/// </summary>
/// 


    public partial class General 
    {
        /// <summary>
        ///     Importance factor for seismic loads 
        /// </summary>
        /// <param name="BuildingRiskCategory">  Building risk category </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="I_e"> Seismic importance factor  </returns>

        [MultiReturn(new[] { "I_e" })]
        public static Dictionary<string, object> SeismicImportanceFactor(string BuildingRiskCategory, string Code = "ASCE7-10")
        {
            //Default values
            double I_e = 0;


            //Calculation logic:
            CalcLog log = new CalcLog();

            Kodestruct.Loads.ASCE7.Entities.BuildingRiskCategory _BuildingRiskCategory;
            bool IsValidCat = Enum.TryParse(BuildingRiskCategory, out _BuildingRiskCategory);
            if (IsValidCat == false)
            {
                throw new Exception("Building risk category not recognized. Check input.");
            }
            //= (BuildingRiskCategory)Enum.Parse(typeof(BuildingRiskCategory), (string)GetParameterValue("RiskCategory", inputData));

            Kodestruct.Loads.ASCE.ASCE7_10.SeismicLoads.General gen = new Kodestruct.Loads.ASCE.ASCE7_10.SeismicLoads.General(log);
            I_e = gen.GetImportanceFactor(_BuildingRiskCategory);

            return new Dictionary<string, object>
            {
                { "I_e", I_e }
 
            };
        }


    }
}


