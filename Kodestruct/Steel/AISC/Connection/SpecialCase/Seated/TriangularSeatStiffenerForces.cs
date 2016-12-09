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
///     Triangular seat stiffener forces
///     Category:   Steel.AISC.Connection.SpecialCase
/// </summary>
/// 


    public partial class Seated 
    {
        /// <summary>
        ///     Triangular seat stiffener forces (kip - in unit system for all inputs and outputs)
        /// </summary>
        /// <param name="a_seat">  Vertical dimension (height) of stiffened seat </param>
        /// <param name="b_seat">  Horizontal dimension of stiffened seat </param>
        /// <param name="P_uSeat">  Ultimate force on seated connection </param>
        /// <param name="e_1">  Eccentricity of seated connection, defined as distance from load to face of support </param>
        /// <returns name="N_u"> Required shear strength </returns>
        /// <returns name="V_u"> Required axial strength </returns>
        /// <returns name="M_u"> Required flexural strength </returns>

        [MultiReturn(new[] { "N_u","V_u","M_u" })]
        public static Dictionary<string, object> TriangularSeatStiffenerForces(double a_seat,double b_seat,double P_uSeat,double e_1)
        {
            //Default values
            double N_u = 0;
            double V_u = 0;
            double M_u = 0;


            //Calculation logic:

            double F_y = 0.0; //F_y not used in this calculation
            double E = 0; //E not used in this calculation


            SeatedConnection sc = new SeatedConnection(a_seat, b_seat, F_y, E);
            N_u = sc.GetTriangularSeatStiffenerDesignAxialForce(P_uSeat);
            V_u = sc.GetTriangularSeatStiffenerDesignShear(P_uSeat);
            M_u = sc.GetTriangularSeatStiffenerDesignMoment(P_uSeat, e_1);

            return new Dictionary<string, object>
            {
                { "N_u", N_u }
                ,{ "V_u", V_u }
                ,{ "M_u", M_u }
 
            };
        }


    }
}


