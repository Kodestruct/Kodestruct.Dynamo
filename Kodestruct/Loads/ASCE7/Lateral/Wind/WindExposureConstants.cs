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
using Kodestruct.Loads.ASCE.ASCE7_10.WindLoads;
using Kodestruct.Common.CalculationLogger;
using System;
using Kodestruct.Loads.ASCE7.Entities;

#endregion

namespace Loads.ASCE7.Lateral.Wind
{

/// <summary>
///     Wind  exposure constants
///     Category:   Loads.ASCE7v10.Lateral.Wind
/// </summary>
/// 

    public partial class General 
    {
        /// <summary>
        ///     Terrain exposure constants, as a function of Exposure Category.  (kip - ft unit system for all inputs and outputs)
        /// </summary>
        /// <param name="WindExposureCategory">  Exposure category for wind calculation </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="alpha"> 3-sec gust-speed power law exponent </returns>
        /// <returns name="z_g"> Nominal height of the atmospheric boundary layer (FT) </returns>
        /// <returns name="alpha_"> Mean hourly wind-speed power law exponent </returns>
        /// <returns name="b_"> Mean hourly wind speed factor  </returns>
        /// <returns name="c"> Turbulence intensity factor </returns>
        /// <returns name="l"> Integral length scale factor </returns>
        /// <returns name="epsilon_"> Integral length scale power law exponent </returns>
        /// <returns name="z_min"> Exposure constant </returns>

        [MultiReturn(new[] { "alpha","z_g","alpha_","b_","c","l","epsilon_","z_min" })]
        public static Dictionary<string, object> WindExposureConstants(string WindExposureCategory, string Code = "ASCE7-10")
        {
            //Default values
            double alpha = 0;
            double z_g = 0;
            double alpha_ = 0;
            double b_ = 0;
            double c = 0;
            double l = 0;
            double epsilon_ = 0;
            double z_min = 0;

            WindExposureCategory Exposure;
            //Calculation logic:
            bool IsValidStringExposure = Enum.TryParse(WindExposureCategory, true, out Exposure);
            if (IsValidStringExposure == false)
            {
                throw new Exception("Exposure category is not recognized. Check input string.");
            }

            CalcLog log = new CalcLog();
            WindStructure structure = new WindStructure(new CalcLog());
            //structure.GetTerrainExposureConstant(TerrainExposureConstant.alpha, Exposure);
           alpha =      structure.GetTerrainExposureConstant(TerrainExposureConstant.alpha , Exposure);
           z_g =        structure.GetTerrainExposureConstant(TerrainExposureConstant.zg , Exposure);
           alpha_ =     structure.GetTerrainExposureConstant(TerrainExposureConstant.alpha_ob , Exposure);
           b_ =         structure.GetTerrainExposureConstant(TerrainExposureConstant.b_ob , Exposure);
           c =          structure.GetTerrainExposureConstant(TerrainExposureConstant.c , Exposure);
           l =          structure.GetTerrainExposureConstant(TerrainExposureConstant.l , Exposure);
           epsilon_ =   structure.GetTerrainExposureConstant(TerrainExposureConstant.epsilon_ob , Exposure);
           z_min =      structure.GetTerrainExposureConstant(TerrainExposureConstant.zmin , Exposure);

            return new Dictionary<string, object>
            {
                { "alpha", alpha }
                ,{ "z_g", z_g }
                ,{ "alpha_", alpha_ }
                ,{ "b_", b_ }
                ,{ "c", c }
                ,{ "l", l }
                ,{ "epsilon_", epsilon_ }
                ,{ "z_min", z_min }
 
            };
        }

    }
}


