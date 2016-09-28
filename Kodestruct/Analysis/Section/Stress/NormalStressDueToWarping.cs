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
///     Normal stress due to warping in open cross section
///     Category:   Analysis.Beam
/// </summary>
/// 


    public partial class TorsionalStress 
    {
        /// <summary>
        ///    Calculates Normal stress due to warping in open cross section
        /// </summary>
        ///<param name="E">  Modulus of elasticity </param>
        /// <param name="W_ns">  Normalized warping function at point s </param>
        /// <param name="theta_2der">  Second derivative of angle of rotation with respect to z </param>
        /// <returns name="sigma_ws"> Normal stress at point s due to warping </returns>

        [MultiReturn(new[] { "sigma_ws" })]
        public static Dictionary<string, object> NormalStressDueToWarping(double E, double W_ns,double theta_2der)
        {
            //Default values
            double sigma_ws = 0;


            //Calculation logic:
            SectionStressAnalysis analysis = new SectionStressAnalysis();
            sigma_ws = analysis.GetNormalStressDueToWarpingOpenSection(E, W_ns, theta_2der);
            
            
            return new Dictionary<string, object>
            {
                { "sigma_ws", sigma_ws }
 
            };
        }


    }
}


