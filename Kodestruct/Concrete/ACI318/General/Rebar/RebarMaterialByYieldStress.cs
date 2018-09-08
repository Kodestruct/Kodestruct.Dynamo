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
using Kodestruct.Concrete.ACI;
using Dynamo.Graph.Nodes;

#endregion

namespace Concrete.ACI318.General.Reinforcement
{

/// <summary>
///     Rebar material
///     Category:   Concrete.ACI318_14.General
/// </summary>
/// 


    public partial class RebarMaterial 
    {

        [IsVisibleInDynamoLibrary(false)]
        internal RebarMaterial(double f_y)
        {
            RebarMaterialGeneral remat = new RebarMaterialGeneral(f_y);
            Material = remat;
        }
        /// <summary>
        ///     Rebar material 
        /// </summary>
        /// <param name="f_y">   Specified yield strength for nonprestressed reinforcement   </param>
        /// <returns name="RebarMaterial"> Reinforcement material object, create the object using input parameters first. This material assumes steel elasto-plastic behavior </returns>
        public static RebarMaterial ByYieldStress(double f_y)
        {
            return new RebarMaterial(f_y);
        }



    }
}


