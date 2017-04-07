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

namespace Aluminum.AA
{

/// <summary>
///     Material mechanical properties
///     Category:   Aluminum.AA.General
/// </summary>
/// 



    public partial class MaterialParameters
    {
        /// <summary>
        ///     Material mechanical properties
        /// </summary>
        /// <param name="AluminumMaterial">  Material object  </param>



        /// <summary>
        ///     Material mechanical properties
        /// </summary>
        /// <param name="AluminumMaterial">  Material object  </param>
        /// <param name="Code">  Version of design code or standard </param>
        /// <returns name="E"> Modulus of elasticity </returns>
        /// <returns name="F_cy"> Nominal compression yield stress </returns>
        /// <returns name="F_su"> Nominal shear ultimate stress </returns>
        /// <returns name="F_sy"> Nominal shear yield stress </returns>
        /// <returns name="F_tu"> Nominal tension ultimate stress </returns>
        /// <returns name="F_tuw"> Nominal tension ultimate stress for weld-affected area </returns>
        /// <returns name="F_ty"> Nominal tension yield stress </returns>
        /// <returns name="F_tyw"> Nominal tension yield stress for weld-affected area </returns>
        /// <returns name="k_t"> Tension coefficient </returns>


        [MultiReturn(new[] { "E", "F_cy", "F_su", "F_sy", "F_tu", "F_tuw", "F_ty", "F_tyw", "k_t" })]
        public static Dictionary<string, object> MaterialMechanicalProperties(Aluminum.AA.Material.AluminumMaterial AluminumMaterial, string Code="AA2015")
        {
             // = AluminumMaterial.G
            //Default values
            double E = 0;
            double F_cy = 0;
            double F_su = 0;
            double F_sy = 0;
            double F_tu = 0;
            double F_tuw = 0;
            double F_ty = 0;
            double F_tyw = 0;
            double k_t = 0;

            

            //Calculation logic:
            Kodestruct.Aluminum.AA.AA2015.AluminumMaterial a = new Kodestruct.Aluminum.AA.AA2015.AluminumMaterial(
                AluminumMaterial.Alloy, AluminumMaterial.Temper, AluminumMaterial.ThicknessRange, AluminumMaterial.ProductType);
            E =     a.E     ;
            F_cy =  a.F_cy  ;
            F_su =  a.F_su  ;
            F_sy =  a.F_sy  ;
            F_tu =  a.F_tu  ;
            F_tuw = a.F_tuw ;
            F_ty =  a.F_ty  ;
            F_tyw = a.F_tyw ;
            k_t =   a.k_t   ;

            return new Dictionary<string, object>
            {
                { "E", E }
                ,{ "F_cy", F_cy }
                ,{ "F_su", F_su }
                ,{ "F_sy", F_sy }
                ,{ "F_tu", F_tu }
                ,{ "F_tuw", F_tuw }
                ,{ "F_ty", F_ty }
                ,{ "F_tyw", F_tyw }
                ,{ "k_t", k_t }
 
            };
        }



    }
}


