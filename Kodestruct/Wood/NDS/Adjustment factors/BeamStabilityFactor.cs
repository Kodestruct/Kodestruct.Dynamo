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
///     Beam stability factor
///     Category:   Wood.NDS
/// </summary>
/// 


    public partial class AdjustmentFactor 
    {
        /// <summary>
        ///     Beam stability factor (kip - in unit system for all inputs and outputs)
        /// </summary>
        /// <param name="b">  Length of shearwall parallel to lateral force; distance between chords of shearwall  </param>
        /// <param name="d">  Depth of rectangular beam cross section  </param>
        /// <param name="F_b">  Reference bending design value  </param>
        /// <param name="E_min">  Reference modulus of elasticity for stability calculations  </param>
        /// <param name="l_e">  Effective unbraced length of beam or column  </param>
        /// <param name="C_M_Fb">  Wet service factor for adjusted bending value </param>
        /// <param name="C_M_E">  Wet service factor for modulus of elasticity E and minimum modulus of elasticity E_min </param>
        /// <param name="C_t_Fb">  Temperature factor for adjusted bending value </param>
        /// <param name="C_t_E">  Temperature factor for modulus of elasticity E and minimum modulus of elasticity E_min </param>
        /// <param name="C_F_Fb">  Size factor for adjusted bending value </param>
        /// <param name="C_i_Fb">  Incising factor for adjusted bending value </param>
        /// <param name="C_i_E">  Incising factor for modulus of elasticity E and minimum modulus of elasticity E_min </param>
        /// <param name="C_r">  Repetitive-member factor for bending design values </param>
        /// <param name="C_T">  Buckling stiffness factor for 2 × 4 and smaller dimension lumber in trusses </param>
        /// <param name="lambda">  Time effect factor  </param>
        /// <param name="WoodMemberType">  Distinguishes between dimensional lumber, timber,glulam etc. </param>
        /// <param name="Code">  Identifies the code or standard used for calculations </param>
        /// <returns name="C_L"> Beam stability factor </returns>

        [MultiReturn(new[] { "C_L" })]
        public static Dictionary<string, object> BeamStabilityFactor(double b,double d,double F_b,double E_min,double l_e,double C_M_Fb,double C_M_E,double C_t_Fb,double C_t_E,double C_F_Fb,double C_i_Fb,double C_i_E,double C_r,double C_T,double lambda,string WoodMemberType,string Code)
        {
            //Default values
            double C_L = 0;


            //Calculation logic:

            if (WoodMemberType.Contains("Sawn") && WoodMemberType.Contains("Lumber"))
            {
                DimensionalLumber m = new DimensionalLumber();
                C_L = m.GetStabilityFactor(b, d, F_b, E_min, l_e, C_M_Fb, C_M_E, C_t_Fb, C_t_Fb, C_F_Fb, C_i_Fb, C_i_E, C_r, C_T, lambda);
            }
            else
            {
                throw new Exception("Wood member type not supported.");
            }


            return new Dictionary<string, object>
            {
                { "C_L", C_L }
 
            };
        }




    }
}


