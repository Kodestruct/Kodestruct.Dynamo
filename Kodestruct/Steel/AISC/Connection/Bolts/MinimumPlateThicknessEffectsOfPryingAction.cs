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

namespace Steel.AISC.Connection
{

/// <summary>
///     Prying action minimum plate thickness
///     Category:   Steel.AISC.Connection
/// </summary>
/// 



    public partial class Bolted 
    {
        /// <summary>
        ///    Calculates Prying action minimum plate thickness (kip - in unit system for all inputs and outputs)
        /// </summary>
        /// <param name="d_b">  Nominal fastener diameter </param>
        /// <param name="d_hole">  Bolt hole diameter (in the direction of prying action) </param>
        /// <param name="T_bolt">  Bolt tension </param>
        /// <param name="a_edge">  Distance from edge of flange or leg of tensile element to bolt centerline, for prying action calculation </param>
        /// <param name="b_stem">  Distance from tensile element to bolt centerline, for prying action calculation taken as distance to face of stem for tee and to centerline of leg for angle </param>
        /// <param name="p">  Pitch </param>
        /// <param name="B_bolt">  Design bolt tension using nominal tensile strength of bolt or modified to include effects of shear stress </param>
        /// <param name="F_u">  Specified minimum tensile strength   </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="t_min"> Minimum thickness of connection material </returns>

        [MultiReturn(new[] { "t_min" })]
        public static Dictionary<string, object> MinimumPlateThicknessEffectsOfPryingAction(double d_b,double d_hole,double T_bolt,double a_edge,double b_stem,double p,double B_bolt,
            double F_u, string Code = "AISC360-10")
        {
            //Default values
            double t_min = 0;


            //Calculation logic:
            PryingActionElement pac = new PryingActionElement(d_b, d_hole, b_stem, a_edge, p, B_bolt, F_u);
            t_min = pac.GetMinimumThickness(T_bolt);

            return new Dictionary<string, object>
            {
                { "t_min", t_min }
 
            };
        }



    }
}


