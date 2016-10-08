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

#endregion

namespace Concrete.ACI318.General.Reinforcement
{

/// <summary>
///     Rebar material properties
///     Category:   Concrete.ACI318_14.General.Rebar
/// </summary>
/// 


    public partial class SizeAreaAndProperties 
    {
        /// <summary>
        ///     Rebar material properties
        /// </summary>
        /// <param name="RebarSpecificationId">  Reinforcement specification  </param>
        /// <returns name="f_y">  Specified yield strength for nonprestressed reinforcement  </returns>

        [MultiReturn(new[] { "f_y" })]
        public static Dictionary<string, object> RebarMaterialProperties(string RebarSpecificationId)
        {
            //Default values
            double f_y = 0;


            //Calculation logic:
            RebarMaterial mat = RebarMaterial.ByRebarSpecificationId(RebarSpecificationId);
            f_y = mat.Material.YieldStress;

            return new Dictionary<string, object>
            {
                { "f_y", f_y }
            };
        }


    }
}


