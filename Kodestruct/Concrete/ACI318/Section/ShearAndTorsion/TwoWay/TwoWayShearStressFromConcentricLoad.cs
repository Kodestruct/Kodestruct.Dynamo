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
using Kodestruct.Concrete.ACI.ACI318_14.C22_SectionalStrength.Shear.TwoWay;
using Concrete.ACI318.Section.ShearAndTorsion.TwoWayShear;
using Concrete.ACI318.Section.ShearAndTorsion.TwoWayShear.Perimeter;
using System;

#endregion

namespace Concrete.ACI318.Section.ShearAndTorsion.TwoWayShear
{

/// <summary>
///     Two way shear  stress from concentric  load
///     Category:   Concrete.ACI318.Section.ShearAndTorsion.TwoWayShear
/// </summary>
/// 



    public partial class Stresses 
    {
        /// <summary>
        ///     Two way shear  stress from concentric  load (kip - in unit system for all inputs and outputs)
        /// </summary>
        /// <param name="V_u">   Factored punching shear force at section  </param>
        /// <param name="PunchingShearPerimeter">  Punching shear (two-way shear) perimeter object. Create the object using input parameters first </param>
        /// <returns name="v_u">  Maximum factored two-way shear stress calculated  around the perimeter of a given critical section  </returns>

        [MultiReturn(new[] { "v_u" })]
        public static Dictionary<string, object> TwoWayShearStressFromConcentricLoad(double V_u, PunchingShearPerimeter PunchingShearPerimeter)
        {
            //Default values
            double v_u = 0;


            //Calculation logic:
            PunchingShearPerimeter p = PunchingShearPerimeter;

            PunchingPerimeterConfiguration Configuration;
            bool IsValidInputString = Enum.TryParse(p.Configuration, true, out Configuration);
            if (IsValidInputString == false)
            {
                throw new Exception("Failed to convert string. Examples of acceptable values are Interior, EdgeLeft, CornerLeftTop. Please check input");
            }


            ConcreteSectionTwoWayShear sec = new ConcreteSectionTwoWayShear(p.PerimeterData,p.d,p.c_x,p.c_y,Configuration);
            v_u = sec.GetConcentricShearStress(V_u);

            return new Dictionary<string, object>
            {
                { "v_u", v_u }
 
            };
        }



    }
}


