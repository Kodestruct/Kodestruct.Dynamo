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
///     Wind velocity pressure exposure coefficient
///     Category:   Loads.ASCE7v10.Lateral.Wind
/// </summary>
/// 


    public partial class WindPressure 
    {
        /// <summary>
        ///     Velocity pressure exposure coefficient  accounting for the wind pressure variation as a function of  height . 
        /// </summary>
        /// <param name="z">  Height above ground level (ft) </param>
        /// <param name="WindExposureCategory">  Exposure category for wind calculation </param>
        /// <param name="WindVelocityLocation">  Location type for wind velocity used in pressure calculations </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="K_z"> Velocity pressure exposure coefficient evaluated at height z (or h) </returns>

        [MultiReturn(new[] { "K_z" })]
        public static Dictionary<string, object> WindVelocityPressureExposureCoefficient(double z, string WindExposureCategory, string WindVelocityLocation = "Wall", string Code = "ASCE7-10")
        {

        //public static double WindVelocityPressureExposureCoefficient(double z, string WindExposureCategory, string WindVelocityLocation = "Wall")
        //{
            //Default values
            double K_z = 0;


            //Calculation logic:

            WindExposureCategory Exposure;
            //Calculation logic:
            bool IsValidStringExposure = Enum.TryParse(WindExposureCategory, true, out Exposure);
            if (IsValidStringExposure == false)
            {
                throw new Exception("Exposure category is not recognized. Check input string.");
            }

            WindVelocityLocation Location;
            
            bool IsValidStringLocation= Enum.TryParse(WindVelocityLocation, true, out Location);
            if (IsValidStringLocation == false)
            {
                throw new Exception("Wind pressure location is not recognized. Check input string.");
            }
            BuildingDirectionalProcedureElement element = new BuildingDirectionalProcedureElement(new CalcLog());
            K_z = element.GetVelocityPressureExposureCoefficientKz(z,Exposure, Location);

            return new Dictionary<string, object>
            {
                { "K_z", K_z }
 
            };

            //return K_z;
        }

    }
}


