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
///     Normal stress due to axial load
///     Category:   Analysis.Section
/// </summary>
/// 


    public partial class ElasticStress 
    {
        /// <summary>
        ///    Calculates Normal stress due to axial load
        /// </summary>
        /// <param name="P">  Concentrated load in beam, or axial load in compression member </param>
        /// <param name="A">  Area of cross section </param>
        /// <returns name="sigma_a"> Normal stress due to axial load </returns>

        [MultiReturn(new[] { "sigma_a" })]
        public static Dictionary<string, object> NormalStressDueToAxialLoad(double P,double A)
        {
            //Default values
            double sigma_a = 0;


            //Calculation logic:
            SectionStressAnalysis analysis = new SectionStressAnalysis();
            sigma_a = analysis.GetNormalStressDueToAxialLoad(P, A);

            return new Dictionary<string, object>
            {
                { "sigma_a", sigma_a }
 
            };
        }


    }
}


