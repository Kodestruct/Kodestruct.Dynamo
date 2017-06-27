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
using Concrete.ACI318.Section.ShearAndTorsion.TwoWayShear.Perimeter;
using Kodestruct.Concrete.ACI.ACI318_14.C22_SectionalStrength.Shear.TwoWay;
using System;

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
        ///     Factors used to determine unbalanced moment to be transferred by shear
        /// </summary>
        /// <param name="PunchingShearPerimeter">  Punching shear (two-way shear) perimeter object. Create the object using input parameters first </param>
        /// <returns name="gamma_vx">  Factor used to determine the fraction of moment about X-axis transferred by eccentricity of shear at slab-column  connections </returns>
        /// <returns name="gamma_vy">  Factor used to determine the fraction of moment about Y-axis transferred by eccentricity of shear at slab-column  connections </returns>

        [MultiReturn(new[] { "gamma_vx","gamma_vy" })]
        public static Dictionary<string, object> UnbalancedMomentFactors(PunchingShearPerimeter PunchingShearPerimeter)
        {
            //Default values
            double gamma_vx = 0;
            double gamma_vy = 0;


            //Calculation logic:

            PunchingShearPerimeter p = PunchingShearPerimeter;

            PunchingPerimeterConfiguration Configuration;
            bool IsValidInputString = Enum.TryParse(p.Configuration, true, out Configuration);
            if (IsValidInputString == false)
            {
                throw new Exception("Failed to convert string. Examples of acceptable values are Interior, EdgeLeft, CornerLeftTop. Please check input");
            }
            ConcreteSectionTwoWayShear sec = new ConcreteSectionTwoWayShear(p.PerimeterData, p.d, p.c_x, p.c_y, Configuration);
            gamma_vx = sec.Get_gamma_vx();
            gamma_vy = sec.Get_gamma_vy();

            return new Dictionary<string, object>
            {
                { "gamma_vx", gamma_vx }
                ,{ "gamma_vy", gamma_vy }
 
            };
        }


    }
}


