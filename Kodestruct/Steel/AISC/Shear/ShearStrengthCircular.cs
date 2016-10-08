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

namespace Steel.AISC
{
/// <summary>
///     Shear strength circular member
///     Category:   Steel.AISC.Shear
/// </summary>
/// 


    public partial class Shear 
    {
        /// <summary>
        ///     Shear strength circular member
        /// </summary>
        /// <param name="D">  Outside diameter of round HSS  </param>
        /// <param name="t_nom">  HSS and pipe nominal wall thickness </param>
        /// <param name="Is_SAW_member">  Indicates whether HSS is a ERW or SAW </param>
        /// <param name="L_v">  Distance from maximum to zero shear force   </param>
        /// <param name="F_y">  Specified minimum yield stress </param>
        /// <returns name="phiV_n"> Shear strength </returns>

        [MultiReturn(new[] { "phiV_n" })]
        public static Dictionary<string, object> ShearStrengthCircular(double D, double t_nom, bool Is_SAW_member, double L_v, double F_y, string Code = "AISC360-10")
        {
            //Default values
            double phiV_n = 0;


            //Calculation logic:


            return new Dictionary<string, object>
            {
                { "phiV_n", phiV_n }
 
            };
        }



    }
}


