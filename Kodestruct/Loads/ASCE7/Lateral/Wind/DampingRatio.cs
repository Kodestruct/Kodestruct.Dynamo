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
using System;
using Kodestruct.Loads.ASCE7.Entities;
using Kodestruct.Loads.ASCE.ASCE7_10.WindLoads;

#endregion

namespace Loads.ASCE7.Lateral.Wind
{

/// <summary>
///     Damping ratio
///     Category:   Loads.ASCE7v10.Lateral.Wind
/// </summary>
/// 


    public partial class StructureParameters 
    {
        /// <summary>
        ///     Wind gust effect factor accounting for the dynamic interaction between the building and the structure . 
        /// </summary>
        /// <param name="WindMaterialDampingType">  Type of material for determining inherent structural damping </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="beta"> Damping ratio, percent critical for buildings or other structures </returns>

        [MultiReturn(new[] { "beta" })]
        public static Dictionary<string, object> DampingRatio(string WindMaterialDampingType = "Steel", string Code = "ASCE7-10")
        {
            //Default values
            double beta = 0;


            //Calculation logic:

            WindMaterialDampingType Material;

            bool IsValidStringMaterial = Enum.TryParse(WindMaterialDampingType, true, out Material);
            if (IsValidStringMaterial == false)
            {
                throw new Exception("Material type is not recognized. Use Steel or Concrete. Check input string.");
            }


            WindDamping wd = new WindDamping();
            beta = wd.GetDampingRatioBeta(Material);

            return new Dictionary<string, object>
            {
                { "beta", beta }
 
            };
        }

    }
}


