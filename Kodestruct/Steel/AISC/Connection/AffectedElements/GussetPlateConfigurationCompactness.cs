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
using Kodestruct.Steel.AISC360v10.Connections.AffectedElements;

#endregion

namespace Steel.AISC.Connection
{

/// <summary>
///     Gusset plate configuration compactness
///     Category:   Steel.AISC.Connection
/// </summary>
/// 


    public partial class AffectedElements 
    {
        /// <summary>
        ///    Calculates Gusset plate configuration compactness
        /// </summary>
        /// <param name="t_g">  Gusset plate thickness </param>
        /// <param name="c_Gusset">  Shortest distance between closest bolt and beam flange </param>
        /// <param name="F_y">  Specified minimum yield stress </param>
        /// <param name="E">  Modulus of elasticity of steel </param>
        /// <param name="l_1">  Gusset plate distance from beam to nearest row of bolts </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="IsGussetCompactConfiguration"> Distinguishes between compact and noncompact configuration for gusset effective length factor </returns>

        [MultiReturn(new[] { "IsGussetCompactConfiguration" })]
        public static Dictionary<string, object> GussetPlateConfigurationCompactness(double t_g, double c_Gusset, double F_y, double l_1, double E=29000, string Code = "AISC360-10")
        {
            //Default values
            bool IsGussetCompactConfiguration = false;


            //Calculation logic:
            AffectedElement el = new AffectedElement();
            IsGussetCompactConfiguration = el.IsGussetPlateConfigurationCompact(t_g, c_Gusset, F_y, E, l_1);

            return new Dictionary<string, object>
            {
                { "GussetPlateConfigurationCompactness", IsGussetCompactConfiguration }
 
            };
        }



    }
}


