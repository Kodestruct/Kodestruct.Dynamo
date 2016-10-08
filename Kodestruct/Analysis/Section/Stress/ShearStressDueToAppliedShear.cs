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
///     Shear stress due to applied shear
///     Category:   Analysis.Section
/// </summary>
/// 


    public partial class ElasticStress 
    {
        /// <summary>
        ///    Calculates Shear stress due to applied shear
        /// </summary>
        /// <param name="V">  Internal shear force </param>
        /// <param name="Q">  Statical moment for the point in question </param>
        /// <param name="I">  Moment of inertia (I_x or I_y where applicable) </param>
        /// <param name="b"> Material thickness</param>
        /// <returns name="tau_b"> Shear stress due to applied shear </returns>

        [MultiReturn(new[] { "tau_b" })]
        public static Dictionary<string, object> ShearStressDueToAppliedShear(double V,double Q,double I,double b)
        {
            //Default values
            double tau_b = 0;


            //Calculation logic:
            SectionStressAnalysis analysis = new SectionStressAnalysis();
            tau_b = analysis.GetShearStressDueToAppliedShear(V, Q, I,b);

            return new Dictionary<string, object>
            {
                { "tau_b", tau_b }
 
            };
        }



    }
}


