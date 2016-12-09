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
using Kodestruct.Steel.AISC.AISC360v10.Flexure;
using Kodestruct.Common.CalculationLogger;
using System;

#endregion

namespace Steel.AISC
{

/// <summary>
///     Buckling modification factor
///     Category:   Steel.AISC10.Flexure
/// </summary>
/// 



    public partial class Flexure 
    {
        /// <summary>
        ///     Buckling modification factor (kip - in unit system for all inputs and outputs)
        /// </summary>
        /// <param name="M_A">  Absolute value of moment at quarter point of the unbraced segment </param>
        /// <param name="M_B">  Absolute value of moment at centerline of the unbraced segment  </param>
        /// <param name="M_C">  Absolute value of moment at three-quarter point of the unbraced segment </param>
        /// <param name="M_max">  Absolute value of maximum moment in the unbraced segment  </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="C_b"> Lateral-torsional buckling modification factor for nonuniform moment diagrams  </returns>

        [MultiReturn(new[] { "C_b" })]
        public static Dictionary<string, object> BucklingModificationFactor(double M_A,double M_B,double M_C,double M_max, string Code = "AISC360-10")
        {
            //Default values
            double C_b = 0;


            //Calculation logic:
            double M_maxAbs =Math.Abs(M_max);
            double M_A_Abs = Math.Abs(M_A);
            double M_B_Abs = Math.Abs(M_B);
            double M_C_Abs = Math.Abs(M_C);


            GeneralFlexuralMember fm = new GeneralFlexuralMember(new CalcLog());
            C_b = fm.GetCb(M_maxAbs, M_A_Abs, M_B_Abs, M_C_Abs);

            return new Dictionary<string, object>
            {
                { "C_b", C_b }
 
            };
        }



    }
}


