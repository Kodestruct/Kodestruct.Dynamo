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
    public partial class BasePlateRhsShape : BasePlateShapeObject
    {
        /// <summary>
        ///     Base plate object required for calculations of minimum thickness etc.
        /// </summary>
        /// <param name="B_bp">  Base plate width (for I-shaped column measured parallel to the weak-axis of the shape) </param>
        /// <param name="N_bp">  Base plate width (for I-shaped column measured parallel to the strong-axis of the shape) </param>
        /// <param name="A_2">  Maximum area of the portion of the supporting surface that is geometrically similar to and concentric with the loaded area  </param>
        /// <param name="F_y">  Specified minimum yield stress </param>
        /// <param name="fc_prime"> </param>
        /// <param name="B">HSS column width </param>
        /// <param name="H">HSS column depth</param>
        /// <returns name="BasePlateRhsShape"> Base plate Rectangular HSS shape object , created from input parameters </returns>



         [IsVisibleInDynamoLibrary(false)]
        internal BasePlateRhsShape(double B_bp,double N_bp,double A_2,double F_y, double fc_prime,double B, double H )
        {

                    this.Plate = new bp.BasePlateRectangularHss( B_bp,N_bp,B,H,fc_prime,F_y,A_2); //remove 0 for P_u

        }

         public static BasePlateRhsShape FromGeometry(double B_bp, double N_bp, double A_2, double F_y, double fc_prime, double B, double H)
        {

            return new BasePlateRhsShape(B_bp, N_bp, A_2, F_y,fc_prime,B,H);
        }


    }
}


