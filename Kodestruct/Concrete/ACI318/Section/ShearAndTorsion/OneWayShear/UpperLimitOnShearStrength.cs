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
///     Upper limit on shear strength
///     Category:   Concrete.ACI318.Section.ShearAndTorsion.OneWayShear
/// </summary>



    public partial class NonPrestressed 
    {
        /// <summary>
        ///     One way  shear strength provided by concrete
        /// </summary>
        /// <param name="ConcreteMaterial">Concrete material </param>
        /// <param name="b_w">  Web width </param>
        /// <param name="d">   Distance from extreme compression fiber to centroid  of longitudinal tension reinforcement  </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <param name="phiV_c">  Design shear strength provided by concrete  </param>
        /// <returns name="phiV_nMax">  Upper limit on total shear strength of member </returns>

        [MultiReturn(new[] { "phiV_nMax" })]
        public static Dictionary<string, object> UpperLimitOnShearStrength(Concrete.ACI318.General.Concrete.ConcreteMaterial ConcreteMaterial,
            double b_w, double d, double phiV_c, string Code = "ACI318-14")
        {
            //Default values
            double phiV_nMax = 0;


            //Calculation logic:
            IConcreteMaterial mat = ConcreteMaterial.Concrete;
            CrossSectionRectangularShape shape = new CrossSectionRectangularShape(mat,null, b_w, d);
            ConcreteSectionOneWayShearNonPrestressed section = new ConcreteSectionOneWayShearNonPrestressed(d,shape);
            phiV_nMax = section.GetUpperLimitShearStrength(phiV_c*1000.0) / 1000.0; //default ACI units are psi. Convert to ksi, consistent with Dynamo nodes

            return new Dictionary<string, object>
            {
                { "phiV_nMax", phiV_nMax }
 
            };
        }


    }
}


