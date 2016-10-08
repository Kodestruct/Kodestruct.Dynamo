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
using Kodestruct.Loads.ASCE.ASCE7_10.LiveLoads;

#endregion

namespace Loads.ASCE7.Gravity
{
    /// <summary>
    ///     Uniform live load 
    ///     Category:   Kodestruct.Loads.ASCE7-10.Gravity.Live
    /// </summary>
    /// 



    public partial class Live 
    {
        /// <summary>
        ///    Calculates Minimum uniformly distributed live load (PSF UNITS)
        /// </summary>
        /// <param name="SpaceOccupancyId">  description of space for calculation of live loads</param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns>  Uniformly distributed live load </returns>
        /// 
        [MultiReturn(new[] { "q_L" })]
        public static Dictionary<string, object> UniformLiveLoad(string SpaceOccupancyId, string Code = "ASCE7-10")
        {
            //Default values
            double q_L = 0;

            //TODO: add possibility to account for partitions
            LiveLoadBuilding lb = new LiveLoadBuilding();
            q_L = lb.GetLiveLoad(SpaceOccupancyId, false);


            return new Dictionary<string, object>
            {
                { "q_L", q_L }
 
            };
        }


        internal Live(string LiveLoadOccupancyId)
        {

        }
        [IsVisibleInDynamoLibrary(false)]
        public static Live  ByInputParameters(string LiveLoadOccupancyId)
        {
            return new Live(LiveLoadOccupancyId);
        }

    }
}


