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

namespace Loads.ASCE7v10.Lateral.Seismic
{

/// <summary>
///     Seismic site mapped acceleration parameters
///     Category:   Loads.ASCE7v10.Lateral.Seismic
/// </summary>
/// 


    [IsDesignScriptCompatible]
    public partial class AccelerationParameters 
    {
/// <summary>
///     Mapped spectral response acceleration parameters and long-period transition period for Maximum Considered Earthquake, 5 percent damped structure) 
/// </summary>
        /// <param name="Latitude">  Location latitude </param>
/// <param name="Longitude">  Location longitude </param>

        /// <returns name="S_S"> Mapped mcer, 5 percent damped, spectral response acceleration parameter at short periods </returns>
/// <returns name="S_1"> Mapped mcer, 5 percent damped, spectral response acceleration parameter at a period of 1 s </returns>
/// <returns name="T_L"> Long-period transition period </returns>

        [MultiReturn(new[] { "S_S","S_1","T_L" })]
        public static Dictionary<string, object> SeismicSiteMappedAccelerationParameters(double Latitude,double Longitude)
        {
            //Default values
            double S_S = 0;
double S_1 = 0;
double T_L = 0;


            //Calculation logic:


            return new Dictionary<string, object>
            {
                { "S_S", S_S }
,{ "S_1", S_1 }
,{ "T_L", T_L }
 
            };
        }


        //internal AccelerationParameters (double Latitude,double Longitude)
        //{

        //}
        //[IsVisibleInDynamoLibrary(false)]
        //public static AccelerationParameters  ByInputParameters(double Latitude,double Longitude)
        //{
        //    return new AccelerationParameters(Latitude ,Longitude );
        //}

    }
}


