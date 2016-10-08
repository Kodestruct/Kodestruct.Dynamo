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

#endregion

namespace Steel.AISC.Composite
{

/// <summary>
///     Sum of anchor strength for beam design
///     Category:   Kodestruct.Steel.AISC_10.Composite
/// </summary>
/// 


    public partial class Anchor 
    {
        /// <summary>
        ///    Calculates Strength of headed stud anchor
        /// </summary>
        /// <param name="N_anchors">  Number of shear anchors from point of maximum moment to point of zero moment </param>
        /// <param name="Q_n">  Nominal strength of one steel headed stud or steel channel anchor  </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="SumQ_n"> Sum of the nominal strengths of steel anchors between the point of maximum positive moment and the point of zero moment to either side </returns>

        [MultiReturn(new[] { "SumQ_n" })]
        public static Dictionary<string, object> DesignSumOfAnchorStrengths(double N_anchors, double Q_n, string Code = "AISC360-10")
        {
            //Default values
            double SumQ_n = 0;


            //Calculation logic:
            SumQ_n = N_anchors * Q_n;

            return new Dictionary<string, object>
            {
                { "SumQ_n", SumQ_n }
 
            };
        }



    }
}


