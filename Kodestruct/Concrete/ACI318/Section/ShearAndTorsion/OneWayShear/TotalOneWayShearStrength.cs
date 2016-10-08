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

namespace Concrete.ACI318.Section.ShearAndTorsion.OneWayShear
{

/// <summary>
///     Total one way  shear strength
///     Category:   Concrete.ACI318.Section.ShearAndTorsion.OneWayShear
/// </summary>
/// 


    public partial class NonPrestressed 
    {
        /// <summary>
        ///     Total one way  shear strength
        /// </summary>
        /// <param name="phiV_c">   Design shear strength provided by concrete  </param>
        /// <param name="phiV_s">   Design shear strength provided by shear reinforcement  </param>
        /// <param name="CheckUpperLimit"> Indicates if upper limit per ACI is checked </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="phiV_n">  Total shear strength provided by reinforced concte section  </returns>



        [MultiReturn(new[] { "phiV_s" } )]
        public static Dictionary<string, object> TotalOneWayShearStrength(double phiV_c, double phiV_s, bool CheckUpperLimit = true, string Code = "ACI318-14")
        {
            //Default values

            double phiV_n = 0;

            //Calculation logic:
            phiV_n = phiV_c + phiV_s; 

            return new Dictionary<string, object>
            {
                 { "phiV_n", phiV_n }
            };
        }


    }
}


