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
using System;

#endregion

namespace Loads.ASCE7.Lateral.Seismic
{

/// <summary>
///     Seismic Design Category
///     Category:   Loads.ASCE7v10.Lateral.Seismic
/// </summary>
/// 


    public partial class General 
    {
        /// <summary>
        ///     Seismic Design Category (SDC)  classification assigned to a structure based on its Risk Category  and the severity of the design earthquake ground motion at the site 
        /// </summary>
        /// <param name="BuildingRiskCategory">  Building risk category </param>
        /// <param name="S_DS">  Design, 5 percent damped, spectral response acceleration parameter at short periods </param>
        /// <param name="S_D1">  Design, 5 percent damped, spectral response acceleration parameter at a period of 1 s </param>
        /// <param name="S_1">  Mapped mcer, 5 percent damped, spectral response acceleration parameter at a period of 1 s </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="SeismicDesignCategory"> Seismic design category (SDC) </returns>

        [MultiReturn(new[] { "SeismicDesignCategory" })]
        public static Dictionary<string, object> SeismicDesignCategory(string BuildingRiskCategory, double S_DS, double S_D1, double S_1, string Code = "ASCE7-10")
        {
            //Default values
            string SeismicDesignCategory = "";

            
            //Calculation logic:
            Kodestruct.Loads.ASCE7.Entities.BuildingRiskCategory RiskCategory;
            bool ValidRiskCategory = Enum.TryParse(BuildingRiskCategory, out RiskCategory);
            CalcLog log = new CalcLog();

            Kodestruct.Loads.ASCE.ASCE7_10.SeismicLoads.General gen = new Kodestruct.Loads.ASCE.ASCE7_10.SeismicLoads.General(log);
            SeismicDesignCategory = gen.GetSeismicDesignCategory(RiskCategory, S_DS, S_D1, S_1).ToString();


            return new Dictionary<string, object>
            {
                { "SeismicDesignCategory", SeismicDesignCategory }
 
            };
        }



    }
}


