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
using System;
using Kodestruct.Common.CalculationLogger;
using Kodestruct.Loads.ASCE7.Entities;

#endregion

namespace Loads.ASCE7.Lateral.Seismic
{

/// <summary>
///     Seismic fundamental period (General procedure)
///     Category:   Loads.ASCE7v10.Lateral.Seismic
/// </summary>
/// 

    public partial class BuildingFundamentalPeriod 
    {
        /// <summary>
        ///     Approximate fundamental period of the building used to account for building dynamic response to base accelerations (s). Procedure applicable to any building structure . 
        /// </summary>
        /// <param name="h_n">  Structural height defined as  the vertical distance  from the base to the highest level of the seismic force-resisting system of the structure.   </param>
        /// <param name="SeismicSystemTypeGeneralProcedure">Seismic lateral system type (used in approximate procedure for fundamental period determination)</param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="T_a"> Approximate fundamental period of the building </returns>

        [MultiReturn(new[] { "T_a" })]
        public static Dictionary<string, object> SeismicFundamentalPeriodGeneralMethod(double h_n, string SeismicSystemTypeGeneralProcedure, string Code = "ASCE7-10")
        {
            //Default values
            double T_a = 0;


            //Calculation logic:
            CalcLog log = new CalcLog();

            SeismicSystemTypeForApproximateAnalysis SeismicSystemType;
            bool IsValidSystemType = Enum.TryParse(SeismicSystemTypeGeneralProcedure, out SeismicSystemType);
            if (IsValidSystemType==false)
            {
                throw new Exception("Lateral system not recognized");  
            }

            Kodestruct.Loads.ASCE.ASCE7_10.SeismicLoads.Building b = new Kodestruct.Loads.ASCE.ASCE7_10.SeismicLoads.Building(null,
                Kodestruct.Loads.ASCE7.Entities.SeismicDesignCategory.None, log);
            T_a = b.GetApproximatePeriodGeneral(h_n, SeismicSystemType);
         

            return new Dictionary<string, object>
            {
                { "T_a", T_a }
 
            };
        }

    }
}


