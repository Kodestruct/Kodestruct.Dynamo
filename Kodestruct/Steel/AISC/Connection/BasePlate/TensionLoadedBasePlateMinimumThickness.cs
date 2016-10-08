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
///     Tension loaded base plate minimum thickness
///     Category:   Steel.AISC10.Connection
/// </summary>
/// 


    public partial class MinimumThickness 
    {
        /// <summary>
        ///     Tension loaded base plate minimum thickness
        /// </summary>
        /// <param name="BasePlateShape">  Base plate shape object , created from inut parameters </param>
        /// <param name="T_uAnchor">  Required tension strength in single anchor </param>
        /// <param name="x_anchor">  Distance from anchor centerline to plate face, flange face or web face </param>
        /// <param name="b_effPlate">  Effective width of base plate (per anchor) resiting flexure from anchor tension. </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="t_min"> Minimum thickness of connection material </returns>

        [MultiReturn(new[] { "t_min" })]
        public static Dictionary<string, object> TensionLoadedBasePlateMinimumThickness(BasePlateShapeObject BasePlateShape, double T_uAnchor, double x_anchor, double b_effPlate
            , string Code = "AISC360-10")
        {
            //Default values
            double t_min = 0;


            //Calculation logic:
            BasePlateTensionLoaded bp = new BasePlateTensionLoaded(BasePlateShape.Plate);
            t_min = bp.GetMinimumBasePlateBasedOnBoltTension(T_uAnchor, x_anchor, b_effPlate);

            return new Dictionary<string, object>
            {
                { "t_min", t_min }
 
            };
        }

    }
}


