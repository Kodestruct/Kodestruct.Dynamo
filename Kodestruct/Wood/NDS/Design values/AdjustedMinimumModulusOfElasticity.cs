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
using Kodestruct.Wood.NDS.NDS2015;
using System;

#endregion

namespace Wood.NDS
{

/// <summary>
///     Adjusted minimum modulus of elasticity
///     Category:   Wood.NDS
/// </summary>
/// 



    public partial class DesignValues
    {
        /// <summary>
        ///     Adjusted minimum modulus of elasticity (kip - in unit system for all inputs and outputs)
        /// </summary>
        /// <param name="E_min">  Reference modulus of elasticity for stability calculations  </param>
        /// <param name="C_M_E">  Wet service factor for modulus of elasticity E and minimum modulus of elasticity E_min </param>
        /// <param name="C_t">  Temperature factor </param>
        /// <param name="C_i_E">  Incising factor for dimension lumber </param>
        /// <param name="C_T">  Buckling stiffness factor for 2 × 4 and smaller dimension lumber in trusses </param>
        /// <param name="WoodMemberType">  Distinguishes between dimensional lumber, timber,glulam etc. </param>
        /// <param name="Code">  Identifies the code or standard used for calculations </param>
        /// <returns name="E_min_prime"> Adjusted modulus of elasticity for stability calculations  </returns>

        [MultiReturn(new[] { "E_min_prime" })]
        public static Dictionary<string, object> AdjustedMinimumModulusOfElasticity(double E_min,double C_M_E,double C_t,double C_i_E,double C_T,
            string WoodMemberType = "SawnDimensionLumber", string Code = "NDS2015")
        {
            //Default values
            double E_min_prime = 0;


            //Calculation logic:
            if (WoodMemberType.Contains("Sawn") && WoodMemberType.Contains("Lumber"))
            {
                DimensionalLumber m = new DimensionalLumber();
                E_min_prime = m.GetAdjustedMinimumModulusOfElasticityForStability(E_min, C_M_E, C_t, C_i_E, C_T);
            }
            else
            {
                throw new Exception("Wood member type not supported.");
            }

            return new Dictionary<string, object>
            {
                { "E_min_prime", E_min_prime }
 
            };
        }


    }
}


