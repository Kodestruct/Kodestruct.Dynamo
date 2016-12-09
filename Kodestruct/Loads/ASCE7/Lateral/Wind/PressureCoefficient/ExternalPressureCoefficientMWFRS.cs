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
using Kodestruct.Loads.ASCE7.Entities;
using Kodestruct.Loads.ASCE.ASCE7_10.WindLoads.Building.DirectionalProcedure.MWFRS;
using Kodestruct.Common.CalculationLogger;
using Dynamo.Graph.Nodes;
using System;

#endregion

namespace Loads.ASCE7.Lateral.Wind
{

/// <summary>
///     External pressure coefficient (MWFRS)
///     Category:   Loads.ASCE7v10.Lateral.Wind
/// </summary>
/// 



    public partial class PressureCoefficient 
    {
        /// <summary>
        ///     External pressure coefficient  used in calculation of wind loads on Main Wind-Force Resisting System (MWFRS) elements. (kip - ft unit system for all inputs and outputs)
        /// </summary>
        /// <param name="B">  Horizontal dimension of building measured normal to wind direction (ft)</param>
        /// <param name="L">  Horizontal dimension of a building measured parallel to the wind direction (ft) </param>
        /// <param name="WindFaceType">  Type of face relative to wind direction (windward, leeward or side)  </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="C_p"> External pressure coefficient to be used in determination of wind loads for buildings </returns>

        [MultiReturn(new[] { "C_p" })]
        public static Dictionary<string, object> ExternalPressureCoefficientMWFRS(double B, double L, string WindFaceType, string Code = "ASCE7-10")
        {
            //Default values
            double C_p = 0;


            //Calculation logic:
            WindFace face;

            bool IsValidStringFaceType = Enum.TryParse(WindFaceType, true, out face);
            if (IsValidStringFaceType == false)
            {
                throw new Exception("Wind face type is not recognized. Use Windward, Leeward or Side. Check input string.");
            }

            CalcLog Log = new CalcLog();
            Mwfrs Mwfrs = new Mwfrs(Log);
            C_p = Mwfrs.GetWallPressureCoefficient(face, B, L);

            return new Dictionary<string, object>
            {
                { "C_p", C_p }
 
            };
        }


    }
}


