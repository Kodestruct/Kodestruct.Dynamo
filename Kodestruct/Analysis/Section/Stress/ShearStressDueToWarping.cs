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
using Kodestruct.Analysis.Section;

#endregion

namespace Analysis.Section
{

/// <summary>
///     Shear stress due to warping in open cross section
///     Category:   Analysis.Beam
/// </summary>
/// 

    public partial class TorsionalStress 
    {
        /// <summary>
        ///    Calculates Shear stress due to warping in open cross section
        /// </summary>
        /// <param name="E">  Modulus of elasticity </param>
        /// <param name="S_ws">  Warping statical moment at point s </param>
        /// <param name="t_el">  Thickness of element </param>
        /// <param name="theta_3der">  Third derivative of angle of rotation with respect to z </param>
        /// <returns name="tau_w"> Shear stress due to warping </returns>

        [MultiReturn(new[] { "tau_w" })]
        public static Dictionary<string, object> ShearStressDueToWarping(double E, double S_ws,double t_el, double theta_3der)
        {
            //Default values
            double tau_w = 0;


            //Calculation logic:
            SectionStressAnalysis analysis = new SectionStressAnalysis();
            tau_w = analysis.GetShearStressDueToWarpingOpenSecton(E, S_ws, t_el, theta_3der);

            return new Dictionary<string, object>
            {
                { "tau_w", tau_w }
 
            };
        }


    }
}


