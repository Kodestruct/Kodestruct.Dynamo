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

#endregion

namespace Loads.ASCE7v10.Lateral.Wind
{

/// <summary>
///     Wind pressure (Facade)
///     Category:   Loads.ASCE7v10.Lateral.Wind
/// </summary>
/// 


    [IsDesignScriptCompatible]
    public partial class WindPressure 
    {
/// <summary>
///     Wind pressure on Components and Cladding
/// </summary>
        /// <param name="q">  Velocity pressure </param>
/// <param name="GC_p_Pos">  Product of positive external pressure coefficient and gust-effect factor to be used in determination of wind loads </param>
/// <param name="GC_p_Neg">  Product of negative external pressure coefficient and gust-effect factor to be used in determination of wind loads </param>
/// <param name="GC_pi">  Product of internal pressure coefficient and gust-effect factor to be used in determination of wind loads for buildings </param>
/// <param name="h">  Mean roof height of a building or height of other structure </param>

        /// <returns name="p"> Design pressure to be used in determination of wind loads for buildings </returns>

        [MultiReturn(new[] { "p" })]
        public static Dictionary<string, object> WindPressureForComponentsAndCladding(double q,double GC_p_Pos,double GC_p_Neg,double GC_pi,double h)
        {
            //Default values
            double p = 0;


            //Calculation logic:


            return new Dictionary<string, object>
            {
                { "p", p }
 
            };
        }


        //internal WindPressure (double q,double GC_p_Pos,double GC_p_Neg,double GC_pi,double h)
        //{

        //}
        //[IsVisibleInDynamoLibrary(false)]
        //public static WindPressure  ByInputParameters(double q,double GC_p_Pos,double GC_p_Neg,double GC_pi,double h)
        //{
        //    return new WindPressure(q ,GC_p_Pos ,GC_p_Neg ,GC_pi ,h );
        //}

    }
}


