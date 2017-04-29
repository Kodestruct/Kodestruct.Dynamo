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
using Kodestruct.Concrete.ACI.Entities;
using Concrete.ACI318.Section.ShearAndTorsion.TwoWayShear.Perimeter;
using System;

#endregion

namespace Concrete.ACI318.Section.ShearAndTorsion.TwoWayShear
{

/// <summary>
///     Two way shear stress from moment
///     Category:   Concrete.ACI318.Section.ShearAndTorsion.TwoWayShear
/// </summary>
/// 



    public partial class Stresses 
    {
        /// <summary>
        ///     Two way shear stress from moment (kip - in unit system for all inputs and outputs)
        /// </summary>
        /// <param name="M_ux">   Factored moment with respect to x-axis </param>
        /// <param name="M_uy">   Factored moment with respect to y-axis </param>
        /// <param name="V_u">   Factored punching shear force at section  </param>
        /// <param name="PunchingShearPerimeter">  Punching shear (two-way shear) perimeter object. Create the object using input parameters first </param>
        /// <param name="PunchingPerimeterConfiguration">  Type of punching perimeter (interior, edge, corner etc) </param>
        /// <returns name="v_u_Max">  Maximum factored two-way shear stress </returns>
        /// <returns name="v_u_Min">  Minimum factored two-way shear stress </returns>
        /// <returns name="gamma_vx">  Factor used to determine the fraction of moment about X-axis transferred by eccentricity of shear at slab-column  connections </returns>
        /// <returns name="gamma_vy">  Factor used to determine the fraction of moment about Y-axis transferred by eccentricity of shear at slab-column  connections </returns>

        [MultiReturn(new[] { "v_u_Max","v_u_Min","gamma_vx","gamma_vy" })]
        public static Dictionary<string, object> TwoWayShearStressFromMomentAndShear(double M_ux,double M_uy, double V_u,PunchingShearPerimeter PunchingShearPerimeter,string PunchingPerimeterConfiguration)
        {
            //Default values
            double v_u_Max = 0;
            double v_u_Min = 0;
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
            ResultOfShearStressDueToMoment result = sec.GetCombinedShearStressDueToMomementAndShear(M_ux, M_uy, V_u);

            v_u_Max =  result.v_max ;
            v_u_Min =  result.v_min ;
            gamma_vx     =  result.gamma_vx     ;
            gamma_vy =      result.gamma_vy;

            return new Dictionary<string, object>
            {
                { "v_u_Max", v_u_Max }
                ,{ "v_u_Min", v_u_Min }
                ,{ "gamma_vx", gamma_vx }
                ,{ "gamma_vy", gamma_vy }
 
            };
        }


    }
}


