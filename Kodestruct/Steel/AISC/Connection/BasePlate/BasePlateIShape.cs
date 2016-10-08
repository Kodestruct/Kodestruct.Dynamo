#region Copyright
   /*Copyright (C) 2015 Kodestruct Inc

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
    public partial class BasePlateIShape : BasePlateShapeObject
    {




         [IsVisibleInDynamoLibrary(false)]
        internal BasePlateIShape(double B_bp,double N_bp,double A_2,double F_y, double fc_prime,double d_c, double b_f )
        {

            this.Plate = new bp.BasePlateIShape(B_bp, N_bp, d_c, b_f, fc_prime, F_y, A_2); 

        }

         public static BasePlateIShape FromGeometry(double B_bp, double N_bp, double A_2, double F_y, double fc_prime, double d_c, double b_f)
        {

            return new BasePlateIShape( B_bp, N_bp, A_2, F_y, fc_prime,d_c,b_f);
        }


    }
}


