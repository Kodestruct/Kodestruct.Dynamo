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
using Analysis.Section;
using Kodestruct.Steel.AISC.Interfaces;
using Kodestruct.Steel.AISC.SteelEntities.Materials;
using Kodestruct.Steel.AISC.SteelEntities;
using Kodestruct.Steel.AISC.AISC360v10.Connections.AffectedMembers;
using Kodestruct.Common.CalculationLogger.Interfaces;
using Kodestruct.Common.CalculationLogger;
using Kodestruct.Common.Section.Interfaces;

#endregion

namespace Steel.AISC.Connection
{

/// <summary>
///     Connected element strength in flexure 
///     Category:   Steel.AISC.Connection
/// </summary>
/// 


    public partial class AffectedElements 
    {
        /// <summary>
        ///     Connected element strength in flexure 
        /// </summary>
        /// <param name="Shape"> Cross section shape   </param>
        /// <param name="L_b">  Length between points that are either braced against lateral displacement of compression flange or braced against twist of the cross section   </param>
        /// <param name="F_y">  Specified minimum yield stress </param>
        /// <param name="F_u">  Specified minimum tensile strength   </param>
        /// <param name="HasHolesInTensionFlange">  Identifies if member has holes in tension flange, for checking tension rupture of flange per F13 </param>
        /// <param name="A_fg">  Gross area of tension flange  </param>
        /// <param name="A_fn">  Net area of tension flange  </param>
        /// <param name="IsCompactDoublySymmetricForFlexure">  Indicates whether shape is compact for flexure and doubly symmetric </param>
        /// <param name="C_b">  Lateral-torsional buckling modification factor for nonuniform moment diagrams  </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="phiM_n"> Moment strength </returns>

        [MultiReturn(new[] { "phiM_n" })]
        public static Dictionary<string, object> ConnectedElementStrengthInFlexure(CustomProfile Shape,
            double L_b, double F_y, double F_u, bool HasHolesInTensionFlange = false, double A_fg = 0, double A_fn = 0, bool IsCompactDoublySymmetricForFlexure = true, double C_b = 1,
            string Code = "AISC360-10")
        {
            //Default values
            double phiM_n = 0;


            //Calculation logic:
            ICalcLog log = new CalcLog();
            ISection Isec = Shape.Section as ISection;
            ISteelMaterial Material = new SteelMaterial(F_y, F_u, SteelConstants.ModulusOfElasticity, SteelConstants.ShearModulus);
            AffectedElementInFlexure element = new AffectedElementInFlexure(Isec, F_y, F_u, HasHolesInTensionFlange, A_fg, A_fn, IsCompactDoublySymmetricForFlexure);
            phiM_n = element.GetFlexuralStrength();

            return new Dictionary<string, object>
            {
                { "phiM_n", phiM_n }
 
            };
        }



    }
}


