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
using System;
using Kodestruct.Common.CalculationLogger;
using Kodestruct.Wood.NDS.NDS2015.Material;

#endregion

namespace Wood.NDS
{

/// <summary>
///     ReferenceValues
///     Category:   Wood.NDS
/// </summary>
/// 



    public partial class General
    {
    /// <summary>
    ///     ReferenceValues
    /// </summary>
        /// <param name="WoodSpeciesId">  Identifies  wood species </param>
        /// <param name="CommercialGradeId">  Identifies commercial grade of wood being considered </param>
        /// <param name="SizeClassId">Wood member size classification, per NDS Supplement reference tables</param>
        /// <param name="WoodMemberType">  Distinguishes between dimensional lumber, timber,glulam etc. </param>
    /// <param name="Code">  Identifies the code or standard used for calculations </param>
    /// <returns name="F_b"> Reference bending design value  </returns>
    /// <returns name="F_c"> Out-of-plane seismic forces for concrete and masonry walls  </returns>
    /// <returns name="F_t"> Reference tension design value parallel to grain  </returns>
    /// <returns name="F_v"> Velocity-based seismic site coefficient at 1.0 second period </returns>
    /// <returns name="E"> Modulus of elasticity  </returns>
    /// <returns name="E_min"> Reference modulus of elasticity for stability calculations  </returns>
    /// <returns name="F_cPerp"> Reference compression design value perpendicular to grain  </returns>
    /// <returns name="G"> Specific gravity of wood or a wood-based member  </returns>

        [MultiReturn(new[] { "F_b","F_c","F_t","F_v","E","E_min","F_cPerp","G" })]
        public static Dictionary<string, object> ReferenceValues(string WoodSpeciesId, string CommercialGradeId, string SizeClassId="2 in. & wider",
            string WoodMemberType = "SawnDimensionLumber",
            string Code = "NDS2015")
        {
            //Default values
            double F_b = 0;
            double F_c = 0;
            double F_t = 0;
            double F_v = 0;
            double E = 0;
            double E_min = 0;
            double F_cPerp = 0;
            double G = 0.0;

            //Calculation logic:
            if (WoodMemberType.Contains("Sawn") && WoodMemberType.Contains("Lumber"))
            {
                CalcLog log = new CalcLog();
                VisuallyGradedDimensionLumber dl = new VisuallyGradedDimensionLumber(WoodSpeciesId,
                    CommercialGradeId, SizeClassId, log);

                F_b = dl.F_b/1000.0; //All Dynamo nodes use ksi units by default
                F_c = dl.F_cParal / 1000.0; //All Dynamo nodes use ksi units by default
                F_t = dl.F_t / 1000.0; //All Dynamo nodes use ksi units by default
                F_v = dl.F_v / 1000.0; //All Dynamo nodes use ksi units by default
                E = dl.E / 1000.0; //All Dynamo nodes use ksi units by default
                E_min = dl.E_min / 1000.0; //All Dynamo nodes use ksi units by default
                F_cPerp = dl.F_cPerp / 1000.0; //All Dynamo nodes use ksi units by default
                G = dl.G;
            }
            else
            {
                throw new Exception("Wood member type not supported.");
            }

            return new Dictionary<string, object>
                        {
                            { "F_b", F_b }
            ,{ "F_c", F_c }
            ,{ "F_t", F_t }
            ,{ "F_v", F_v }
            ,{ "E", E }
            ,{ "E_min", E_min }
            ,{ "F_cPerp", F_cPerp }
            ,{ "G", G }
                        };
                    }



    }
}


