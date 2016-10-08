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

#endregion

namespace Steel.AISC.Connection
{

/// <summary>
///     Bolt nominal tensile stress
///     Category:   Kodestruct.Steel.AISC_10.Connection
/// </summary>
/// 



    public partial class Bolted 
    {
        /// <summary>
        ///    Calculates bolt nominal tensile stress from AISC Table J3.2
        /// </summary>
        /// <param name="BoltMaterialId">  Bolt material specification </param>
        /// <param name="BoltThreadCase">  Identifies whether threads are included or excluded from shear planes </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="F_nt"> Nominal tensile stress </returns>

        [MultiReturn(new[] { "F_nt" })]
        public static Dictionary<string, object> BoltNominalTensileStress(string BoltMaterialId, string BoltThreadCase, string Code = "AISC360-10")
        {
            //Default values
            double F_nt = 0;


            //Calculation logic:
            BoltFactory bf = new BoltFactory(BoltMaterialId);
            IBoltMaterial material = bf.GetBoltMaterial();
            F_nt = material.GetNominalTensileStress(BoltThreadCase);


            return new Dictionary<string, object>
            {
                { "F_nt", F_nt }
 
            };
        }



    }
}


