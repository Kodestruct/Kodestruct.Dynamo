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
using aisc = Kodestruct.Steel.AISC;
using combo = Kodestruct.Steel.AISC.AISC360v10.Combination;
using System;

#endregion

namespace Steel.AISC
{

/// <summary>
///     Interaction ratio
///     Category:   Steel.AISC.Combination
/// </summary>
/// 


    public partial class Combination 
    {
        /// <summary>
        ///     Interaction ratio for the combination of forces
        /// </summary>
        /// <param name="CombinationCaseId">  Defines a type of interaction equation to be used </param>
        /// <param name="N_u">  Required axial strength </param>
        /// <param name="T_uTorsion">  Required torsional strength </param>
        /// <param name="M_ux">  Required flexural strength with respect to x-axis </param>
        /// <param name="M_uy">  Required flexural strength with respect to x-axis </param>
        /// <param name="V_ur">  Required shear strength resultant </param>
        /// <param name="phiN_n">  Compressive strength </param>
        /// <param name="phiT_nTorsion">  Torsional strength </param>
        /// <param name="phiM_nx">  Moment strength with respect to section x-axis </param>
        /// <param name="phiM_ny">  Moment strength with respect to section y-axis </param>
        /// <param name="phiV_nr">  Shear strength resultant </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="InteractionRatio"> Interaction ratio </returns>

        [MultiReturn(new[] { "InteractionRatio" })]
        public static Dictionary<string, object> InteractionRatio(string CombinationCaseId, double N_u = 0, double T_uTorsion = 0, double M_ux = 0, double M_uy = 0, double V_ur = 0,
            double phiN_n = 0, double phiT_nTorsion = 0, double phiM_nx = 0, double phiM_ny = 0, double phiV_nr = 0, string Code = "AISC360-10")
        {
            //Default values
            double InteractionRatio = 0;


            //Calculation logic:
            

              aisc.CombinationCaseId comboCase;
              bool IsValidStringComboType = Enum.TryParse(CombinationCaseId, true, out comboCase);
                if (IsValidStringComboType == true)
                {
                    combo.Combination combo = new combo.Combination();
                    InteractionRatio = combo.GetInteractionRatio(comboCase,
                        N_u, T_uTorsion, M_ux, M_uy, V_ur, phiN_n, phiT_nTorsion, phiM_nx, phiM_ny, phiV_nr);

                    
                }
                else
                {
                    throw new Exception("Invalid combination case string.");
                }

            return new Dictionary<string, object>
            {
                { "InteractionRatio", InteractionRatio }
 
            };
        }



    }
}


