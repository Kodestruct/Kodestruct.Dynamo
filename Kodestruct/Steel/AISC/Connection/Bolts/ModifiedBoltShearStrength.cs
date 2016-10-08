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
using Kodestruct.Steel.AISC.AISC360v10.Connections.Bolted;
using Kodestruct.Steel.AISC.Interfaces;

#endregion

namespace Steel.AISC.Connection
{

/// <summary>
///     Bearing bolt combined tension and shear
///     Category:   Kodestruct.Steel.AISC_10.Connection
/// </summary>
/// 

    public partial class Bolted 
    {
        /// <summary>
        ///    Calculates Bearing bolt combined tension and shear
        /// </summary>
        /// <param name="V_u">  Required shear strength </param>
        /// <param name="d_b">  Nominal fastener diameter </param>
        /// <param name="BoltMaterialId">  Bolt material specification </param>
        /// <param name="BoltThreadCase">  Identifies whether threads are included or excluded from shear planes </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="phiR_nt_modified"> Modified shear strength of bolt subjected to tension  </returns>

        [MultiReturn(new[] { "phiR_nt_modified" })]
        public static Dictionary<string, object> ModifiedBoltShearStrength(double V_u,double d_b,string BoltMaterialId,
            string BoltThreadCase, string Code = "AISC360-10")
        {
            //Default values
            double phiR_nt_modified = 0.0;

            //Calculation logic:
            BoltFactory bf = new BoltFactory(BoltMaterialId);
            IBoltBearing bolt = bf.GetBearingBolt(d_b, BoltThreadCase);
            phiR_nt_modified = bolt.GetAvailableTensileStrength(V_u);

            return new Dictionary<string, object>
            {
                {"phiR_nt_modified", phiR_nt_modified}  
            };
        }



    }
}


