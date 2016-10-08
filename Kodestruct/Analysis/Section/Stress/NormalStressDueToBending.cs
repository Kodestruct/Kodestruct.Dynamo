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
///     Normal stress due to bending
///     Category:   Analysis.Section
/// </summary>
/// 


    public partial class ElasticStress 
    {
        /// <summary>
        ///    Calculates Normal stress due to bending
        /// </summary>
        /// <param name="M">  Concentrated moment </param>
        /// <param name="y">  Vertical distance from horizontal neutral axis to section point under consideration </param>
        /// <param name="I">  Moment of inertia (I_x or I_y where applicable) </param>
        /// <returns name="sigma_b"> Normal stress due to bending about either the x or y </returns>

        [MultiReturn(new[] { "sigma_b" })]
        public static Dictionary<string, object> NormalStressDueToBending(double M,double y,double I)
        {
            //Default values
            double sigma_b = 0;


            //Calculation logic:
            SectionStressAnalysis analysis = new SectionStressAnalysis();
            sigma_b = analysis.GetNormalStressDueToBending(M, y, I);


            return new Dictionary<string, object>
            {
                { "sigma_b", sigma_b }
 
            };
        }



    }
}


