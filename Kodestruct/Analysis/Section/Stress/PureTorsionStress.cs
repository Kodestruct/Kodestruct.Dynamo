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
using Kodestruct.Analysis.Section;

#endregion

namespace Analysis.Section
{

/// <summary>
///     Pure torsion stress
///     Category:   Analysis.Beam
/// </summary>


    public partial class TorsionalStress 
    {
        /// <summary>
        /// Calculates pure torsion stress
        /// </summary>
        /// <param name="G">  Shear modulus of elasticity </param>
        /// <param name="t_el">  Thickness of element </param>
        /// <param name="theta_1der">  First derivative of angle of rotation with respect to z </param>
        /// <returns name="tau_t"> Pure torsional shear stress </returns>

        [MultiReturn(new[] { "tau_t" })]
        public static Dictionary<string, object> PureTorsionStress(double G, double t_el, double theta_1der)
        {
            //Default values
            double tau_t = 0;


            //Calculation logic:
            SectionStressAnalysis analysis = new SectionStressAnalysis();
            tau_t = analysis.GetPureTorsionStressOpenSection(G, t_el, theta_1der);

            return new Dictionary<string, object>
            {
                { "tau_t", tau_t }
 
            };

        }

    }
}


