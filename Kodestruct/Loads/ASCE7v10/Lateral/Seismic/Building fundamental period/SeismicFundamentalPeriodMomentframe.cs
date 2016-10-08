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

namespace Loads.ASCE7v10.Lateral.Seismic
{

/// <summary>
///     Seismic fundamental period (MF procedure)
///     Category:   Loads.ASCE7v10.Lateral.Seismic
/// </summary>
/// 


    [IsDesignScriptCompatible]
    public partial class Building fundamental period 
    {
/// <summary>
///     Approximate fundamental period of the building used to account for building dynamic response to base accelerations (sec). Procedure applicable to low-rise moment frames 
/// </summary>
        /// <param name="N">  Number of stories above the base  </param>

        /// <returns name="T_a"> Approximate fundamental period of the building </returns>

        [MultiReturn(new[] { "T_a" })]
        public static Dictionary<string, object> SeismicFundamentalPeriodMomentframe(double N)
        {
            //Default values
            double T_a = 0;


            //Calculation logic:


            return new Dictionary<string, object>
            {
                { "T_a", T_a }
 
            };
        }


        //internal Building fundamental period (double N)
        //{

        //}
        //[IsVisibleInDynamoLibrary(false)]
        //public static Building fundamental period  ByInputParameters(double N)
        //{
        //    return new Building fundamental period(N );
        //}

    }
}


