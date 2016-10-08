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

#endregion

namespace Loads.ASCE7v10.Lateral.Wind
{

/// <summary>
///     External pressure coefficient (Facade)
///     Category:   Loads.ASCE7v10.Lateral.Wind
/// </summary>
/// 


    [IsDesignScriptCompatible]
    public partial class PressureCoefficient 
    {
/// <summary>
///     External pressure coefficient (GC_p) used in calculation of wind loads on Components and Cladding  
/// </summary>
        /// <param name="WindWallCladdingZone">  Zone of the façade for wind pressure calculation </param>
/// <param name="theta">  Angle of plane of roof from horizontal </param>
/// <param name="B">  Horizontal dimension of building measured normal to wind direction </param>
/// <param name="L">  Horizontal dimension of a building measured parallel to the wind direction </param>
/// <param name="h">  Mean roof height of a building or height of other structure </param>

        /// <returns name="GC_p"> Product of external pressure coefficient and gust-effect factor to be used in determination of wind loads for buildings </returns>

        [MultiReturn(new[] { "GC_p" })]
        public static Dictionary<string, object> ExternalPressureCoefficientFacade(string WindWallCladdingZone,double theta,double B,double L,double h)
        {
            //Default values
            double GC_p = 0;


            //Calculation logic:


            return new Dictionary<string, object>
            {
                { "GC_p", GC_p }
 
            };
        }


        //internal PressureCoefficient (string WindWallCladdingZone,double theta,double B,double L,double h)
        //{

        //}
        //[IsVisibleInDynamoLibrary(false)]
        //public static PressureCoefficient  ByInputParameters(string WindWallCladdingZone,double theta,double B,double L,double h)
        //{
        //    return new PressureCoefficient(WindWallCladdingZone ,theta ,B ,L ,h );
        //}

    }
}


