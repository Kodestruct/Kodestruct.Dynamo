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
using Kodestruct.Wood.NDS.NDS2015;
using System;

#endregion

namespace Wood.NDS
{

/// <summary>
///     Adjusted shear design value
///     Category:   Wood.NDS
/// </summary>
/// 


    public partial class DesignValues
    {
        /// <summary>
        ///     Adjusted shear design value (kip - in unit system for all inputs and outputs)
        /// </summary>
        /// <param name="F_v">  Velocity-based seismic site coefficient at 1.0 second period </param>
        /// <param name="C_M_Fv">  Wet service factor </param>
        /// <param name="C_t">  Temperature factor </param>
        /// <param name="C_i_Fv">  Incising factor for dimension lumber </param>
        /// <param name="lambda">  Time effect factor  </param>
        /// <param name="WoodMemberType">  Distinguishes between dimensional lumber, timber,glulam etc. </param>
        /// <param name="Code">  Identifies the code or standard used for calculations </param>
        /// <returns name="F_v_prime"> Adjusted shear design value parallel to grain (horizontal shear) in a beam  </returns>

        [MultiReturn(new[] { "F_v_prime" })]
        public static Dictionary<string, object> AdjustedShearDesignValue(double F_v,double C_M_Fv,double C_t,double C_i_Fv,double lambda,
            string WoodMemberType = "SawnDimensionLumber", string Code = "NDS2015")
        {
            //Default values
            double F_v_prime = 0;


            //Calculation logic:
            if (WoodMemberType.Contains("Sawn") && WoodMemberType.Contains("Lumber"))
            {
                DimensionalLumber m = new DimensionalLumber();
                F_v_prime = m.GetAdjustedShearDesignValue(F_v, C_M_Fv, C_t, C_i_Fv, lambda);
            }
            else
            {
                throw new Exception("Wood member type not supported.");
            }

            return new Dictionary<string, object>
            {
                { "F_v_prime", F_v_prime }
 
            };
        }



    }
}


