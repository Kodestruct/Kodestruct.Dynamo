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
using Kodestruct.Loads.ASCE.ASCE7_10.WindLoads.Building.DirectionalProcedure;
using Kodestruct.Common.CalculationLogger;

#endregion

namespace Loads.ASCE7.Lateral.Wind
{

/// <summary>
///     Wind pressure (MWFRS)
///     Category:   Loads.ASCE7v10.Lateral.Wind
/// </summary>
/// 


    public partial class WindPressure 
    {
        /// <summary>
        ///     Wind pressure on Main Wind-Force Resisting System (MWFRS) elements (PSF UNITS)
        /// </summary>
        /// <param name="q">  Velocity pressure </param>
        /// <param name="G">  Gust-effect factor </param>
        /// <param name="C_p">  External pressure coefficient to be used in determination of wind loads for buildings </param>
        /// <param name="q_i">  Velocity pressure for internal pressure determination </param>
        /// <param name="GC_pi">  Product of internal pressure coefficient and gust-effect factor to be used in determination of wind loads for buildings </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="p"> Design pressure to be used in determination of wind loads for buildings (PSF) </returns>

        [MultiReturn(new[] { "p" })]
        public static Dictionary<string, object> WindPressureMWFRS(double q, double G, double C_p, double q_i, double GC_pi, string Code = "ASCE7-10")
        {


        //public static double WindPressureMWFRS(double q, double G, double C_p, double q_i, double GC_pi)
        //{

            //Default values
            double p = 0;


            //Calculation logic:
            CalcLog Log = new CalcLog();
            BuildingDirectionalProcedureElement element = new BuildingDirectionalProcedureElement(Log);
            p = element.GetWindPressureMWFRS(q, G, C_p, q_i, GC_pi);

            return new Dictionary<string, object>
            {
                { "p", p }
 
            };
            //return p;
        }

    }
}


