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
using Kodestruct.Steel.AISC360v10.Connections.AffectedElements;

#endregion

namespace Steel.AISC.Connection
{

/// <summary>
///     Connected element strength in tension 
///     Category:   Steel.AISC.Connection
/// </summary>
/// 



    public partial class AffectedElements 
    {
        /// <summary>
        ///    Calculates Connected element strength in tension 
        /// </summary>
        /// <param name="A_g">  Gross cross-sectional area of member </param>
        /// <param name="F_y">  Specified minimum yield stress </param>
        /// <param name="F_u">  Specified minimum tensile strength   </param>
        /// <param name="A_e">  Effective net area </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="phiR_n"> Strength of member or connection </returns>

        [MultiReturn(new[] { "phiR_n" })]
        public static Dictionary<string, object> ConnectedElementStrengthInTension(double A_g, double F_y, double F_u, double A_e, string Code = "AISC360-10")
        {
            //Default values
            double phiR_n = 0;


            //Calculation logic:
            AffectedElementInTension element = new AffectedElementInTension(F_y, F_u);
            phiR_n = element.GetTensileCapacity(A_g, A_e);

            return new Dictionary<string, object>
            {
                { "phiR_n", phiR_n }
 
            };
        }


    }
}


