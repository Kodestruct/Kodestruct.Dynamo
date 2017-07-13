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

#endregion

namespace Concrete.ACI318.General.Concrete
{

/// <summary>
///     Material mechanical properties
    ///     Category:   Concrete.ACI318.General.Concrete
/// </summary>
/// 



    public partial class MaterialParameters
    {

        /// <summary>
        ///     Material mechanical properties
        /// </summary>
        /// <param name="ConcreteMaterial">  Material object  </param>
        /// <param name="Density"> Material density (unit weight) USE PCF UNITS </param>
        /// <param name="Code">  Version of design code or standard </param>
        /// <returns name="E"> Modulus of elasticity </returns>
        /// <returns name="f_r"> Modulus of rupture </returns>



        [MultiReturn(new[] { "E","f_r" })]
        public static Dictionary<string, object> ConcreteMaterialMechanicalProperties(ConcreteMaterial ConcreteMaterial, 
            double Density=0, string Code = "ACI318-14")
        {
            //Default values
            double E = 0;
            double f_r = 0;
            ConcreteMaterial cm = ConcreteMaterial;
            

            //Calculation logic:

            Kodestruct.Concrete.ACI318_14.Materials.ConcreteMaterial mat =
                new Kodestruct.Concrete.ACI318_14.Materials.ConcreteMaterial(cm.Concrete.SpecifiedCompressiveStrength, cm.Concrete.TypeByWeight, Density, null);

            E =     mat.ModulusOfElasticity / 1000.0    ;
            f_r = mat.ModulusOfRupture / 1000.0;


            return new Dictionary<string, object>
            {
                { "E", E }
                ,{ "f_r", f_r }
 
            };
        }



    }
}


