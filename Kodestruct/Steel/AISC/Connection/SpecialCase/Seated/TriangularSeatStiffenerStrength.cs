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
using Kodestruct.Steel.AISC.AISC360v10.Connections;

#endregion

namespace Steel.AISC.Connection.SpecialCase
{

/// <summary>
///     Triangular seat stiffener strength
///     Category:   Steel.AISC.Connection.SpecialCase
/// </summary>
/// 


    public partial class Seated 
    {
        /// <summary>
        ///     Triangular seat stiffener strength
        /// </summary>
        /// <param name="a_seat">  Vertical dimension (height) of stiffened seat </param>
        /// <param name="b_seat">  Horizontal dimension of stiffened seat </param>
        /// <param name="t_p">  Thickness of plate   </param>
        /// <param name="F_y">  Specified minimum yield stress </param>
        /// <param name="E">  Modulus of elasticity of steel </param>
        /// <returns name="phiN_n"> Element axial strength </returns>
        /// <returns name="phiV_n"> Shear strength </returns>
        /// <returns name="phiM_n"> Moment strength </returns>

        [MultiReturn(new[] { "phiN_n","phiV_n","phiM_n" })]
        public static Dictionary<string, object> TriangularSeatStiffenerStrength(double a_seat,double b_seat,double t_p,double F_y,double E)
        {
            //Default values
            double phiN_n = 0;
            double phiV_n = 0;
            double phiM_n = 0;


            //Calculation logic:
            SeatedConnection sc = new SeatedConnection(a_seat, b_seat, F_y, E);
            phiN_n = sc.GetTriangularSeatStiffenerAxialStrength(t_p);
            phiV_n = sc.GetTriangularSeatStiffenerShearStrength(t_p);
            phiM_n = sc.GetTriangularSeatStiffenerMomentStrength(t_p);

            return new Dictionary<string, object>
            {
                { "phiN_n", phiN_n }
                ,{ "phiV_n", phiV_n }
                ,{ "phiM_n", phiM_n }
 
            };
        }

    }
}


