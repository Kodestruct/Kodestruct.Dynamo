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


    [IsDesignScriptCompatible]
    public partial class RebarMaterial 
    {

        [IsVisibleInDynamoLibrary(false)]
        internal RebarMaterial(string RebarSpecificationId)
        {
            RebarMaterialFactory factory = new RebarMaterialFactory();
            Material = factory.GetMaterial(RebarSpecificationId);
        }
        /// <summary>
        ///     Rebar material 
        /// </summary>
        /// <param name="RebarSpecificationId">  Reinforcement specification (applicable ASTM standard)  </param>
        /// <returns name="RebarMaterial"> Reinforcement material object, create the object using input parameters first </returns>
        public static RebarMaterial ByRebarSpecificationId(string RebarSpecificationId = "A615Grade60")
        {
            return new RebarMaterial(RebarSpecificationId);
        }

        private IRebarMaterial material;

         [IsVisibleInDynamoLibrary(false)]
        public IRebarMaterial Material
        {
            get { return material; }
            set { material = value; }
        }
        
       
    }
}


