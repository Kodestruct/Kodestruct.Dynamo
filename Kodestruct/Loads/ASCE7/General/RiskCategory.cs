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
using Dynamo.Nodes;
using System.Collections.Generic;
using Kodestruct.Loads.ASCE.ASCE7_10.General;
using Kodestruct.Loads.ASCE.ASCE7_10.LiveLoads;

#endregion

namespace Loads.ASCE7
{
    /// <summary>
    ///     Building risk category
    ///     Category:   Kodestruct.Loads.ASCE7-10.General
    /// </summary>
    /// 

    public partial class General 
    {
        /// <summary>
        ///    Calculates Selection of Building risk category - ASCE7-10
        /// </summary>
        /// <param name="BuildingOccupancyId">  Occupancy description</param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns> "Parameter name: BuildingRiskCategory", Building risk category </returns>

        [MultiReturn(new[] { "BuildingRiskCategory" })]
        public static Dictionary<string, object> BuildingRiskCategory(string BuildingOccupancyId, string Code = "ASCE7-10")
        {
            //Default values
            string BuildingRiskCategory = "";
            string buildingOccupancy;

            Structure st = new Structure();
            BuildingRiskCategory = st.GetRiskCategory(BuildingOccupancyId).ToString();


            return new Dictionary<string, object>
            {
                { "BuildingRiskCategory", BuildingRiskCategory }
 
            };
        }



    }
}


