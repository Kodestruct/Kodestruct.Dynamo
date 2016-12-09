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
using Kodestruct.Steel.AISC.AISC360v10.Connections;

#endregion

namespace Steel.AISC.Connection
{

/// <summary>
///     Eccentrically loaded bolt group coefficient
///     Category:   Kodestruct.Steel.AISC_10.Connection
/// </summary>
/// 


    public partial class Bolted 
    {
        /// <summary>
        ///    Calculates Eccentrically loaded bolt group coefficient (kip - in unit system for all inputs and outputs)
        /// </summary>
        /// <param name="N_rows">  Number of bolt rows in bolt groups </param>
        /// <param name="N_cols">  Number of bolt columns in bolt groups </param>
        /// <param name="p_h">	Horizontal bolt spacing  </param>
        /// <param name="p_v"> 	Vertical bolt spacing    </param>
        /// <param name="e_group">  Connection bolt or weld group eccentricity </param>
        /// <param name="theta">  Angle of loading for eccentric bolt or weld group </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="C_BoltGroup"> Coefficient for eccentrically loaded bolt group </returns>
        /// <returns name="C_prime"> Coefficient for bolt group subjected to pure moment </returns>

        [MultiReturn(new[] { "C_BoltGroup", "C_prime" })]
        public static Dictionary<string, object> BoltGroupCoefficient(double N_rows, double N_cols, double p_h, double p_v, double e_group, double theta, string Code = "AISC360-10")
        {
            //Default values
            double C_BoltGroup = 0;
            double C_prime = 0;

            //Calculation logic:
            int N_r = (int) N_rows;
            int N_c = (int)N_cols;

            BoltGroup bg = new BoltGroup(N_r, N_c, p_h, p_v);
            C_BoltGroup = bg.GetInstantaneousCenterCoefficient(e_group, theta);
            C_prime = bg.GetPureMomentCoefficient();

            return new Dictionary<string, object>
            {
                { "C_BoltGroup", C_BoltGroup },
                { "C_prime", C_prime }
            };
        }

    }
}


