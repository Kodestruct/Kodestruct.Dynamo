#region Copyright
   /*Copyright (C) 2015 Kodestruct Inc

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
using Kodestruct.Loads.ASCE.ASCE7_10.WindLoads.Building.DirectionalProcedure;
using Kodestruct.Common.CalculationLogger;
using Kodestruct.Loads.ASCE7.Entities;
using System;

#endregion

namespace Loads.ASCE7.Lateral.Wind
{

/// <summary>
///     Wind velocity pressure
///     Category:   Loads.ASCE7v10.Lateral.Wind
/// </summary>
/// 


    public partial class WindPressure 
    {
        /// <summary>
        ///     Velocity pressure, evaluated at height z accounting for the basic wind pressure prior to adjustment by the building–specific aerodynamic and dynamic factors (PSF UNITS)
        /// </summary>
        /// <param name="K_z">  Velocity pressure exposure coefficient evaluated at height z (or h) </param>
        /// <param name="K_zt">  Topographic factor </param>
        /// <param name="K_d">  Wind directionality factor </param>
        /// <param name="V">  Basic wind speed (miles per hour) </param>
        /// <param name="WindVelocityLocation">  Location type for wind velocity used in pressure calculations </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="q_z"> Velocity pressure evaluated at height z above ground (psf) </returns>

        [MultiReturn(new[] { "q_z" })]
        public static Dictionary<string, object> WindVelocityPressure(double K_z, double K_zt, double K_d, double V, string WindVelocityLocation = "Wall", string Code = "ASCE7-10")
        {
        //public static double WindVelocityPressure(double K_z, double K_zt, double K_d, double V, string WindVelocityLocation = "Wall")
        //{
            //Default values
            double q_z = 0;


            //Calculation logic:


            WindVelocityLocation Location;

            bool IsValidStringLocation = Enum.TryParse(WindVelocityLocation, true, out Location);
            if (IsValidStringLocation == false)
            {
                throw new Exception("Wind pressure location is not recognized. Check input string.");
            }

            CalcLog Log  = new CalcLog();
            BuildingDirectionalProcedureElement element = new BuildingDirectionalProcedureElement(Log);
            q_z = element.GetVelocityPressure(K_z, K_zt, K_d, V, Location);

            return new Dictionary<string, object>
            {
                { "q_z", q_z }
 
            };

            //return q_z;
        }


    }
}


