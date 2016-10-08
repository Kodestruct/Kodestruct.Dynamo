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
using Concrete.ACI318.General.Reinforcement;
using Kodestruct.Concrete.ACI;

#endregion

namespace Concrete.ACI318.Details
{

/// <summary>
///     Compression lap splice length
///     Category:   Concrete.ACI318.Details
/// </summary>
/// 


    public partial class LapSplice 
    {
        /// <summary>
        ///     Compression lap splice length
        /// </summary>
        /// <param name="ConcreteMaterial">  Concrete material object used to extract material properties, create the object using input parameters first </param>
        /// <param name="d_b">   Nominal diameter of bar, wire, or prestressing  strand  </param>
        /// <param name="RebarMaterial">   Reinforcement material </param>
        /// <param name="HasConfiningReinforcement">  Indicates if  closely spaced ties  per ACI code section 25.4.3.2  at or near the bend portion of a hooked bar or per 25.4.9.3  at the compression splice are present </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="l_sc">  Compression lap splice length  </returns>

        [MultiReturn(new[] { "l_sc" })]
        public static Dictionary<string, object> CompressionLapSpliceLength(Concrete.ACI318.General.Concrete.ConcreteMaterial ConcreteMaterial, double d_b,
            RebarMaterial RebarMaterial, bool HasConfiningReinforcement = false, string Code = "ACI318-14")
        {
            //Default values
            double l_sc = 0;


            //Calculation logic:
            IRebarMaterial mat = RebarMaterial.Material;
            Rebar rebar = new Rebar(d_b, false, mat);

            return new Dictionary<string, object>
            {
                { "l_sc", l_sc }
 
            };
        }


    }
}


