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
using Kodestruct.Steel.AISC.AISC360v10.Connections.BasePlate;
using System;


#endregion

namespace Steel.AISC.Connection.BasePlate
{

/// <summary>
///     Eccentically  loaded  base plate minimum thickness
///     Category:   Steel.AISC10.Connection
/// </summary>
/// 


    public partial class MinimumThickness 
    {
        /// <summary>
        ///     Eccentrically  loaded  base plate minimum thickness
        /// </summary>
        /// <param name="BasePlateShape">  Base plate shape object, created from inut parameters </param>
        /// <param name="P_u">  Required axial strength </param>
        /// <param name="M_u">  Required flexural strength </param>
        /// <param name="BendingAxis">  Distinguishes between bending with respect to section x-axis vs x-axis </param>
        /// <param name="f_anchor">  Distance from column centerline to the plane of tension anchors </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="t_min"> Minimum thickness of connection material </returns>

        [MultiReturn(new[] { "t_min" })]
        public static Dictionary<string, object> EccentricallyLoadedBasePlateMinimumThickness(BasePlateShapeObject BasePlateShape, double P_u, double M_u, string BendingAxis, double f_anchor
            , string Code = "AISC360-10")
        {
            //Default values
            double t_min = 0;

            
            Kodestruct.Steel.AISC.BendingAxis axis;
            bool IsValidAxisString = Enum.TryParse(BendingAxis, true, out axis);
            if (IsValidAxisString == false)
            {
                throw new Exception("Failed to convert string. Specify X or Y axis. Please check input");
            }

            //Calculation logic:
            BasePlateEccentricallyLoaded bp = new BasePlateEccentricallyLoaded(BasePlateShape.Plate);
            t_min = bp.GetMinimumThicknessEccentricLoadStrongAxis(P_u, M_u, axis, f_anchor);

            return new Dictionary<string, object>
            {
                { "t_min", t_min }
 
            };
        }


    }
}


