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

namespace Loads.ASCE7.Lateral.Seismic
{

/// <summary>
///     Seismic base shear
///     Category:   Loads.ASCE7v10.Lateral.Seismic
/// </summary>
/// 


    public partial class Building 
    {
        /// <summary>
        ///     Seismic base shear (kip)
        /// </summary>
        /// <param name="W_e">  Effective seismic weight of the building </param>
        /// <param name="C_s">  Seismic response coefficient which multiplied by the building seismic weight, gives the building seismic base shear (lateral pseudo-acceleration, expressed in units of gravity) </param>
        /// <returns name="V_b"> Seismic base shear </returns>

        [MultiReturn(new[] { "V_b" })]
        public static Dictionary<string, object> SeismicBaseShear(double W_e,double C_s)
        {
            //Default values
            double V_b = 0;


            //Calculation logic:
            V_b = W_e * C_s;

            return new Dictionary<string, object>
            {
                { "V_b", V_b }
 
            };
        }



    }
}


