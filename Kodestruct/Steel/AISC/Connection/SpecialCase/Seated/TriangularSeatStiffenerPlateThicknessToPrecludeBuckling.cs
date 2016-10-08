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
///     TriangularSeatStiffenerPlateThicknessToPrecludeBuckling
///     Category:   Steel.AISC.Connection.SpecialCase
/// </summary>
/// 


    public partial class Seated 
    {
        /// <summary>
        ///     Triangular seat stiffener plate thickness to preclude buckling
        /// </summary>
        /// <param name="a_seat">  Vertical dimension (height) of stiffened seat </param>
        /// <param name="b_seat">  Horizontal dimension of stiffened seat </param>
        /// <param name="F_y">  Specified minimum yield stress </param>
        /// <param name="E">  Modulus of elasticity of steel </param>
        /// <returns name="t_p"> Thickness of plate   </returns>

        [MultiReturn(new[] { "t_p" })]
        public static Dictionary<string, object> TriangularSeatStiffenerPlateThicknessToPrecludeBuckling(double a_seat,double b_seat,double F_y,double E)
        {
            //Default values
            double t_p = 0;


            //Calculation logic:
            SeatedConnection sc = new SeatedConnection(a_seat, b_seat, F_y, E);
            t_p = sc.TriangularSeatStiffenerPlateThicknessToPrecludeBuckling(a_seat, b_seat, F_y, E);

            return new Dictionary<string, object>
            {
                { "t_p", t_p }
 
            };
        }


    }
}


