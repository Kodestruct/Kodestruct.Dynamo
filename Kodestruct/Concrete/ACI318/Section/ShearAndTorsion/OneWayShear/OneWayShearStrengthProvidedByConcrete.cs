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
using Kodestruct.Concrete.ACI318_14;
using Kodestruct.Concrete.ACI;

#endregion

namespace Concrete.ACI318.Section.ShearAndTorsion.OneWayShear
{

/// <summary>
///     One way concrete shear strength
///     Category:   Concrete.ACI318.Section.ShearAndTorsion.OneWayShear
/// </summary>
/// 


    public partial class NonPrestressed 
    {
        /// <summary>
        ///     One way  shear strength provided by concrete (kip - in unit system for all inputs and outputs)
        /// </summary>
        /// <param name="b_w">  Web width </param>
        /// <param name="d">   Distance from extreme compression fiber to centroid  of longitudinal tension reinforcement  </param>
        /// <param name="h">   Overall thickness, height, or depth of member  </param>
        /// <param name="N_u">   Factored axial force normal to cross section occurring simultaneously with vu or tu; to be taken as  positive for compression and negative for tension  </param>
        /// <param name="rho_w">   Ratio of A_s /( b_w*d) </param>
        /// <param name="M_u">   Factored moment at section   </param>
        /// <param name="V_u">   Factored shear force at section  </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="phiV_c">  Design shear strength provided by concrete  </returns>

        [MultiReturn(new[] { "phiV_c" })]
        public static Dictionary<string, object> OneWayShearStrengthProvidedByConcrete(Concrete.ACI318.General.Concrete.ConcreteMaterial ConcreteMaterial,
            double b_w, double d, double h, double N_u = 0.0, double rho_w = 0.0, double M_u = 0.0, double V_u = 0.0, string Code = "ACI318-14")
        {
            //Default values
            double phiV_c = 0;


            //Calculation logic:
            IConcreteMaterial mat = ConcreteMaterial.Concrete;
            CrossSectionRectangularShape shape = new CrossSectionRectangularShape(mat,null, b_w, h);
            ConcreteSectionOneWayShearNonPrestressed section = new ConcreteSectionOneWayShearNonPrestressed(d,shape);
            phiV_c = section.GetConcreteShearStrength(N_u, rho_w, M_u, V_u)/1000.0; //default ACI units are psi. Convert to ksi, consistent with Dynamo nodes

            return new Dictionary<string, object>
            {
                { "phiV_c", phiV_c }
 
            };
        }


    }
}


