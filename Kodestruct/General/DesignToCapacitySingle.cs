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
using Dynamo.Graph.Nodes;
using Analysis.Section;
using Kodestruct.Common.Entities;
using System;

#endregion

namespace General
{

    /// <summary>
    ///     Demand to capacity
    ///     Category:   General
    /// </summary>
    /// 

    public partial class DemandToCapacity
    {
        /// <summary>
        ///    Calculates demand to capacity for a single limit state / check
        /// </summary>
        ///  <param name="R_u">  Ultimate load (demand) </param>
        /// <param name="phiR_n">  Strength (capacity) </param>
        /// <returns name="DCR"> Demand to capacity ratio </returns>


        [MultiReturn(new[] { "DCR" })]
        public static Dictionary<string, object> DemandToCapacitySingle(double R_u, double phiR_n)
        {

            double DCR = 1.0;
            if (phiR_n == -1)
            {
                DCR = 0.0;
            }
            else
            {
                DCR = R_u / phiR_n;
            }


            return new Dictionary<string, object>
            {
                { "DCR", DCR } 
            };
        }



    }
}


