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
        ///     Two-way shear strength provided by unreinforced concrete (kip - in unit system for all inputs and outputs)
        /// </summary>
        /// <param name="PunchingShearPerimeter">  Punching shear (two-way shear) perimeter object. Create the object using input parameters first </param>
        /// <param name="ConcreteMaterial">  Concrete material object used to extract material properties, create the object using input parameters first </param>
        /// <param name="d">   Effective slab depth </param>
        /// <param name="c_x">   Dimension of rectangular or equivalent rectangular  column, capital, or bracket measured in the direction of X-axis </param>
        /// <param name="c_y">   Dimension of rectangular or equivalent rectangular  column, capital, or bracket measured in the direction of Y-axis </param>
        /// <param name="IsSectionAtColumnFace">  Identifies if section is calculated at column face  </param>
        /// <returns name="phi_v_c"> Two-way shear strength provided by concrete (stress units) </returns>

        [MultiReturn(new[] { "phi_v_c" })]
        public static Dictionary<string, object> TwoWayShearStrengthProvidedByConcrete(PunchingShearPerimeter PunchingShearPerimeter,
            ConcreteMaterial ConcreteMaterial,double d, double c_x,double c_y, bool IsSectionAtColumnFace =true)
        {
            //Default values
            double phi_v_c = 0;


            //Calculation logic:

            PunchingPerimeterConfiguration Configuration;
            bool IsValidInputString = Enum.TryParse(PunchingShearPerimeter.Configuration, true, out Configuration);
            if (IsValidInputString == false)
            {
                throw new Exception("Failed to convert string. Examples of acceptable values are Interior, EdgeLeft, CornerLeftTop. Please check input");
            }

            ConcreteSectionTwoWayShear sec = new ConcreteSectionTwoWayShear(ConcreteMaterial.Concrete,
                PunchingShearPerimeter.PerimeterData, d, c_x, c_y, Configuration);
            phi_v_c = sec.GetTwoWayStrengthForUnreinforcedConcrete() / 1000.0; //convert to kips

            return new Dictionary<string, object>
            {
                { "phi_v_c", phi_v_c }
 
            };
        }




    }
}


