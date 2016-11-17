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
using Dynamo.Graph.Nodes;
using System;
using Kodestruct.Concrete.ACI.Entities.ShearAndTorsion;
using Concrete.ACI318.General.Concrete;
using Concrete.ACI318.General.Reinforcement;
using Kodestruct.Concrete.ACI.ACI318_14.C22_SectionalStrength.ShearFriction;

#endregion

namespace Concrete.ACI318.Section.ShearFriction
{

/// <summary>
///     Shear friction strength
///     Category:   Concrete.ACI318.Section.ShearFriction
/// </summary>
/// 


    [IsDesignScriptCompatible]
    public partial class ShearFriction 
    {
        /// <summary>
        ///     Shear friction strength
        /// </summary>
        /// <param name="ShearFrictionSurfaceTypeId">  Type of contact surface for shear friction calculations </param>
        /// <param name="ConcreteMaterial">  Concrete material object used to extract material properties, create the object using input parameters first </param>
        /// <param name="A_c">   Area of concrete section resisting shear transfer  </param>
        /// <param name="RebarMaterial">  Reinforcement material object. Create the object using input parameters first </param>
        /// <param name="A_v">   Area of shear reinforcement within spacing s  </param>
        /// <param name="alpha">   Angle defining the orientation of reinforcement </param>
        /// <param name="F_comp">  Permanent net compression across the shearplane  </param>
        /// <returns name="phiV_n"> Shear friction strength</returns>


       [MultiReturn(new[] { "phiV_n" })]
        public static Dictionary<string, object> ShearFrictionStrength(string ShearFrictionSurfaceTypeId,ConcreteMaterial ConcreteMaterial,
            double A_c,RebarMaterial RebarMaterial,double A_v,double alpha=90,double F_comp=0)
        {
            //Default values
            

            //Calculation logic:
            double phiV_n = 0.0;

            ShearFrictionSurfaceType _ShearFrictionSurfaceType;
            bool IsValidShearFrictionSurfaceTypeString = Enum.TryParse(ShearFrictionSurfaceTypeId, true, out _ShearFrictionSurfaceType);
            if (IsValidShearFrictionSurfaceTypeString == false)
            {
                throw new Exception("Failed to convert string. ShearFrictionSurfaceType variable needs to be MonolithicConcrete, HardenedRoughenedConcrete, HardenedNonRoughenedConcrete or ConcreteAgainstSteel. Please check input");
            }
            ConcreteSectionShearFriction sec = new ConcreteSectionShearFriction(ConcreteMaterial.Concrete, A_c,RebarMaterial.Material, A_v, alpha, F_comp);
            phiV_n = sec.GetShearFrictionStrength()/1000.0; //convert back to ksi units
            return new Dictionary<string, object>
            {
                { "phiV_n", phiV_n }
 
            };
        }


        internal ShearFriction()
        {

        }
        [IsVisibleInDynamoLibrary(false)]
        public static ShearFriction ByInputParameters()
        {
            return new ShearFriction();
        }

    }
}


