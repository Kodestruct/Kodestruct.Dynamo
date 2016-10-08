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

#endregion

namespace Steel.AISC.Connection.BasePlate
{

/// <summary>
///     Concentrically loaded  base plate minimum thickness
///     Category:   Steel.AISC10.Connection
/// </summary>
/// 


    public partial class MinimumThickness 
    {
        /// <summary>
        ///     Concentrically loaded  base plate minimum thickness
        /// </summary>
        /// <param name="BasePlateShape">  Base plate shape object , created from inut parameters </param>
        /// <param name="P_u">  Required axial strength </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="t_min"> Minimum thickness of connection material </returns>

        [MultiReturn(new[] { "t_min" })]
        public static Dictionary<string, object> ConcentricallyLoadedBasePlateMinimumThickness(BasePlateShapeObject BasePlateShape, double P_u, string Code = "AISC360-10")
        {
            //Default values
            double t_min = 0;


            //Calculation logic:
            BasePlateConcentricallyLoaded bp = new BasePlateConcentricallyLoaded(BasePlateShape.Plate);
            t_min = bp.GetMinimumThicknessConcentricLoad(P_u);

            return new Dictionary<string, object>
            {
                { "t_min", t_min }
 
            };
        }

    }
}


