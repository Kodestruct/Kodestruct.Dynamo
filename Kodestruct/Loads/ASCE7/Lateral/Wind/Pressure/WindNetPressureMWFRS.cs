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
using Kodestruct.Common.CalculationLogger;
using Kodestruct.Loads.ASCE.ASCE7_10.WindLoads.Building.DirectionalProcedure;

#endregion

namespace Loads.ASCE7.Lateral.Wind
{

/// <summary>
///     Wind net pressure (MWFRS)
///     Category:   Loads.ASCE7v10.Lateral.Wind
/// </summary>
/// 



    public partial class WindPressure 
    {
        /// <summary>
        ///     Wind net pressure on Main Wind-Force Resisting System (MWFRS) elements excluding effects of internal pressure (PSF UNITS)
        /// </summary>
        /// <param name="q_z">  Velocity pressure evaluated at height z above ground </param>
        /// <param name="q_h">  Velocity pressure evaluated at height z=h </param>
        /// <param name="G">  Gust-effect factor </param>
        /// <param name="C_p_l">  Leeward external pressure coefficient for determination of wind loads </param>
        /// <param name="C_p_w">  Windward external pressure coefficient for determination of wind loads</param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="p"> Design pressure to be used in determination of wind loads for buildings (psf)</returns>

        [MultiReturn(new[] { "p" })]
        public static Dictionary<string, object> WindNetPressureMWFRS(double q_z, double q_h, double G, double C_p_l, double C_p_w, string Code = "ASCE7-10")
        {


        //public static double WindNetPressureMWFRS(double q_z,double q_h,double G,double C_p_l,double C_p_w)
        //{
            //Default values
            double p = 0;


            //Calculation logic:
            CalcLog Log = new CalcLog();
            BuildingDirectionalProcedureElement element = new BuildingDirectionalProcedureElement(Log);
            p = element.GetWindPressureMWFRSNet(q_z, q_h, G, C_p_l, C_p_w);


            return new Dictionary<string, object>
            {
                { "p", p }
 
            };
            //return p;
        }


        //internal WindPressure (double q_z,double q_h,double G,double C_p_l,double C_p_w)
        //{

        //}
        //[IsVisibleInDynamoLibrary(false)]
        //public static WindPressure  ByInputParameters(double q_z,double q_h,double G,double C_p_l,double C_p_w)
        //{
        //    return new WindPressure(q_z ,q_h ,G ,C_p_l ,C_p_w );
        //}

    }
}


