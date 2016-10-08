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

#endregion

namespace Steel.AISC.Connection
{

/// <summary>
///     Bolt clear distance
///     Category:   Steel.AISC.Connection
/// </summary>
/// 


    public partial class Bolted 
    {
        /// <summary>
        ///    Calculates bolt edge and center-to-center clear distances for bolt bearing checks
        /// </summary>
        /// <param name="l_BoltEdge">  Distance from bolt centerline to connected material edge </param>
        /// <param name="l_BoltCenter">  Bolt centerline spacing </param>
        /// <param name="d_hole">  Bolt hole diameter </param>
        /// <returns name="l_cEdge"> Clear distance from bolt centerline to connected material edge </returns>
        /// <returns name="l_cCenter"> Bolt clear centerline spacing </returns>

        [MultiReturn(new[] { "l_cEdge","l_cCenter" })]
        public static Dictionary<string, object> BoltClearDistance(double l_BoltEdge,double l_BoltCenter,double d_hole)
        {
            //Default values
        double l_cEdge = l_BoltEdge-d_hole/2.0;
        double l_cCenter = l_BoltCenter-d_hole;


            //Calculation logic:


            return new Dictionary<string, object>
            {
                { "l_cEdge", l_cEdge }
                ,{ "l_cCenter", l_cCenter }
 
            };
        }



    }
}


