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
using Concrete.ACI318.General.Concrete;
using Kodestruct.Concrete.ACI.ACI318_14.C22_SectionalStrength.Shear.TwoWay;
using Concrete.ACI318.General.Reinforcement;
using System;
using Concrete.ACI318.Section.ShearAndTorsion.TwoWayShear.Perimeter;

#endregion

namespace Concrete.ACI318.Section.ShearAndTorsion.TwoWayShear
{

/// <summary>
///     Two-way shear strength provided by unreinforced concrete
///     Category:   Concrete.ACI318.Section.ShearAndTorsion.TwoWayShear
/// </summary>
/// 



    public partial class Stresses 
    {
        /// <summary>
        ///     Two-way shear strength provided by unreinforced concrete
        /// </summary>
        /// <param name="PunchingShearPerimeter">  Punching shear (two-way shear) perimeter object. Create the object using input parameters first </param>
        /// <param name="ConcreteMaterial">  Concrete material object used to extract material properties, create the object using input parameters first </param>
        /// <param name="RebarMaterial">  Reinforcement material object. Create the object using input parameters first </param>
        /// <param name="A_v">   Area of shear reinforcement within spacing s  </param>
        /// <param name="s">  Spacing of reinforcement </param>
        /// <param name="PunchingReinforcementType">Distinguishes between different types of punching shear reinforcement (studs, stirrups etc)</param>
        /// <returns name="phi_v_n"> Two-way shear strength provided by concrete and reinforcement (stress units) </returns>

        [MultiReturn(new[] { "phi_v_n" })]
        public static Dictionary<string, object> TwoWayShearStrengthProvidedByReinforcedSection(PunchingShearPerimeter PunchingShearPerimeter, 
            ConcreteMaterial ConcreteMaterial, RebarMaterial RebarMaterial,
            double A_v,double s, string PunchingReinforcementType)
        {
            //Default values
            double phi_v_n = 0;


            //Calculation logic:


            Kodestruct.Concrete.ACI.ACI318_14.SectionalStrength.Shear.TwoWay.PunchingReinforcementType ReinforcementType;
            bool IsValidInputString = Enum.TryParse(PunchingReinforcementType, true, out ReinforcementType);
            if (IsValidInputString == false)
            {
                throw new Exception("Failed to convert string. Examples of acceptable values are Interior, EdgeLeft, CornerLeftTop. Please check input");
            }

            ReinforcedSectionTwoWayShear sec = new ReinforcedSectionTwoWayShear( ConcreteMaterial.Concrete, RebarMaterial.Material, PunchingShearPerimeter.PerimeterData, A_v, s, ReinforcementType);
            phi_v_n = sec.GetTwoWayStrength()/1000.0; //convert to ksi

            return new Dictionary<string, object>
            {
                { "phi_v_n", phi_v_n }
 
            };
        }



    }
}


