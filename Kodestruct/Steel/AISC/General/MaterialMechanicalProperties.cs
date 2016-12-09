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
using Kodestruct.Common.CalculationLogger;
using Kodestruct.Steel.AISC.SteelEntities.Materials;

#endregion

namespace Steel.AISC.General
{

/// <summary>
///     Material mechanical properties
///     Category:   Steel.AISC.General
/// </summary>
/// 


    public partial class MaterialProperties 
    {
        /// <summary>
        ///    Calculates yield and ultimate stress for slected steel material as well as modulus of elasticity and shear modulus (kip - in unit system for all inputs and outputs)
        /// </summary>
        /// <param name="SteelMaterialId">  Steel material </param>
        /// <param name="d_b">  Bolt diameter (if applicable) </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="F_y"> Specified minimum yield stress </returns>
        /// <returns name="F_u"> Specified minimum tensile strength   </returns>
        /// <returns name="E"> Modulus of elasticity of steel </returns>
        /// <returns name="G"> Shear modulus of elasticity of steel </returns>
        /// 

        [MultiReturn(new[] { "F_y","F_u","E","G" })]
        public static Dictionary<string, object> MaterialMechanicalProperties(string SteelMaterialId, double d_b = 0, string Code = "AISC360-10")
        {
            //Default values
            double F_y = 0;
            double F_u = 0;
            double E = 0;
            double G = 0;


            //Calculation logic:
            CalcLog cl = new CalcLog();
            SteelMaterialCatalog sm = new SteelMaterialCatalog(SteelMaterialId, d_b, cl);
            F_y = sm.YieldStress;
            F_u = sm.UltimateStress;
            E = sm.ModulusOfElasticity;
            G = sm.ShearModulus;


            return new Dictionary<string, object>
            {
                { "F_y", F_y }
                ,{ "F_u", F_u }
                ,{ "E", E }
                ,{ "G", G }
 
            };
        }


        //internal MaterialProperties (string SteelMaterialId)
        //{

        //}
        //[IsVisibleInDynamoLibrary(false)]
        //public static MaterialProperties  ByInputParameters(string SteelMaterialId)
        //{
        //    return new MaterialProperties(SteelMaterialId );
        //}

    }
}


