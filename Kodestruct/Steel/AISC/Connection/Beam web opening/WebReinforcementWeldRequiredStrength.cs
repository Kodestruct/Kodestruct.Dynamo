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
using Kodestruct.Steel.AISC.AISC360v10.Connections.WebOpenings;

#endregion

namespace Steel.AISC.Connection
{

/// <summary>
///     Web opening reinforcement weld required strength
///     Category:   Steel.AISC.Connection
/// </summary>
/// 



    public partial class BeamWebOpening 
    {
        /// <summary>
        ///     Web opening reinforcement weld required strength
        /// </summary>
        /// <param name="a_o">  Length of opening </param>
        /// <param name="t_w">  Thickness of web  </param>
        /// <param name="b_r">  Length of a projection of reinforcing plate from web </param>
        /// <param name="t_r">  Thickness of reinforcing plate </param>
        /// <param name="F_y">  Specified minimum yield stress </param>
        /// <param name="IsSingleSideReinforcement">  Identifies whether web reinforcing plate is placed on one side of web </param>
        /// <param name="IsCompositeBeam">  Identifies whether member is composite </param>
        /// <returns name="phiR_n"> Strength of member or connection </returns>

        [MultiReturn(new[] { "phiR_n" })]
        public static Dictionary<string, object> WebReinforcementWeldRequiredStrength(double a_o,double t_w,double b_r,double t_r,double F_y,
            bool IsSingleSideReinforcement=false,bool IsCompositeBeam=true)
        {
            //Default values
            double phiR_n = 0;
            phiR_n = 0.9 * WebOpeningGeneral.GetReinforcementWeldRequiredStrength(a_o, t_w, b_r, t_r, F_y,
                IsSingleSideReinforcement, IsCompositeBeam);

            //Calculation logic:


            return new Dictionary<string, object>
            {
                { "phiR_n", phiR_n }
 
            };
        }



    }
}


