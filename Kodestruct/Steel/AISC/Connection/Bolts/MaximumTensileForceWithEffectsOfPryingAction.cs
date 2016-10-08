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
///     Prying action maximum tension force
///     Category:   Steel.AISC.Connection
/// </summary>
/// 



    public partial class Bolted 
    {
        /// <summary>
        ///    Calculates Prying action maximum tension force
        /// </summary>
        /// <param name="d_b">  Nominal fastener diameter </param>
        /// <param name="d_hole">   Bolt hole diameter (in the direction of prying action) </param>
        /// <param name="t_p">  Thickness of plate   </param>
        /// <param name="a_edge">  Distance from edge of flange or leg of tensile element to bolt centerline, for prying action calculation </param>
        /// <param name="b_stem">  Distance from tensile element to bolt centerline, for prying action calculation taken as distance to face of stem for tee and to centerline of leg for angle </param>
        /// <param name="p">  Pitch </param>
        /// <param name="B_bolt">  Design bolt tension using nominal tensile strength of bolt or modified to include effects of shear stress </param>
        /// <param name="F_u">  Specified minimum tensile strength   </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="phiT_n"> Tensile strength </returns>

        [MultiReturn(new[] { "phiT_n" })]
        public static Dictionary<string, object> MaximumTensileForceWithEffectsOfPryingAction(double d_b,double d_hole,double t_p,double a_edge,double b_stem,double p,double B_bolt,
            double F_u, string Code = "AISC360-10")
        {
            //Default values
            double phiT_n = 0;


            //Calculation logic:
            PryingActionElement pac = new PryingActionElement(d_b, d_hole, b_stem, a_edge, p, B_bolt, F_u);
            phiT_n = pac.GetMaximumBoltTensionForce(t_p);

            return new Dictionary<string, object>
            {
                { "phiT_n", phiT_n }
 
            };
        }




    }
}


