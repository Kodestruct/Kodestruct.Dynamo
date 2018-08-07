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
using Dynamo.Graph.Nodes;
using Kodestruct.Steel.AISC;
using System;
using Kodestruct.Steel.AISC.AISC360v10.Connections.BasePlate;
using bp = Kodestruct.Steel.AISC.AISC360v10.Connections.BasePlate;

#endregion

namespace Steel.AISC.Connection.BasePlate.Shapes
{

/// <summary>
///     Base plate
///     Category:   Steel.AISC10.Connection
/// </summary>
/// 


    [IsDesignScriptCompatible]
    public partial class BasePlateChsShape : BasePlateShapeObject
    {




         [IsVisibleInDynamoLibrary(false)]
        internal BasePlateChsShape(double B_bp, double N_bp, double A_2, double F_y, double fc_prime,  double D,  double f_anchor)
        {

                    this.Plate = new bp.BasePlateCircularHss( B_bp,N_bp,D,fc_prime,F_y,A_2,f_anchor); //remove 0 for P_u

        }
        /// <summary>
        /// Circular HSS base plate object (kip - in unit system for all inputs and outputs)
        /// </summary>
        /// <param name="B_bp">Base plate dimension</param>
        /// <param name="N_bp">Base plate dimension</param>
        /// <param name="A_2">Area of the lower base of the largest frustum of a pyramid, cone, or tapered wedge contained wholly within the support and having its upper base equal to the loaded area </param>
        /// <param name="F_y">Plate material yield stress</param>
        /// <param name="fc_prime">Concrete minimum specified strength</param>
        /// <param name="D">HSS diameter</param>
        /// <param name="f_anchor">  Distance from column centerline to the plane of tension anchors </param>
        /// <returns></returns>
        public static BasePlateChsShape FromGeometry(double B_bp, double N_bp, double A_2, double F_y, double fc_prime, double D, double t, double f_anchor)
        {

            return new BasePlateChsShape(B_bp, N_bp, A_2, F_y, fc_prime,D,f_anchor);
        }


    }
}


