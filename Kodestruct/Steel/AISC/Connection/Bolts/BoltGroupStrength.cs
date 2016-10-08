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

#endregion

namespace Steel.AISC.Connection
{

/// <summary>
///     Eccentrically loaded bolt group strength
///     Category:   Steel.AISC.Connection
/// </summary>
/// 


    public partial class Bolted 
    {
        /// <summary>
        ///    Calculates Eccentrically loaded bolt group strength
        /// </summary>
        /// <param name="C_BoltGroup">  Coefficient for eccentrically loaded bolt group </param>
        /// <param name="phiR_nv">  Connection shear strength </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="phiR_n"> Strength of member or connection </returns>

        [MultiReturn(new[] { "phiR_n" })]
        public static Dictionary<string, object> BoltGroupStrength(double C_BoltGroup, double phiR_nv, string Code = "AISC360-10")
        {
            //Default values
            double phiR_n = 0;


            //Calculation logic:
            phiR_n = C_BoltGroup * phiR_nv;

            return new Dictionary<string, object>
            {
                { "phiR_n", phiR_n }
 
            };
        }




    }
}


