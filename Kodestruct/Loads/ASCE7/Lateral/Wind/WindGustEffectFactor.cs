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
using System;
using Kodestruct.Loads.ASCE.ASCE7_10.WindLoads;
using Kodestruct.Common.CalculationLogger;

#endregion

namespace Loads.ASCE7.Lateral.Wind
{

/// <summary>
///     Wind gust effect factor
///     Category:   Loads.ASCE7v10.Lateral.Wind
/// </summary>
/// 



    public partial class StructureParameters 
    {
        /// <summary>
        ///     Wind gust effect factor accounting for the dynamic interaction between the building and the structure (FT MPH UNITS). 
        /// </summary>
        /// <param name="B">  Horizontal dimension of building measured normal to wind direction (FT) </param>
        /// <param name="h">  Mean roof height of a building or height of other structure (FT) </param>
        /// <param name="L">  Horizontal dimension of a building measured parallel to the wind direction (FT) </param>
        /// <param name="beta">  Damping ratio, percent critical for buildings or other structures </param>
        /// <param name="n_1">  Fundamental natural frequency (HZ)</param>
        /// <param name="V">  Basic wind speed (MPH) </param>
        /// <param name="WindExposureCategory">  Exposure category for wind calculation </param>
        /// <param name="WindStructureDynamicResponseType">  Type of wind dynamic response (flexible or rigid) </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="G"> Gust-effect factor </returns>

        [MultiReturn(new[] { "G" })]
        public static Dictionary<string, object> WindGustEffectFactor(double B,double h,double L,double beta,double n_1,double V,
            string WindExposureCategory, string WindStructureDynamicResponseType = "Flexible", string Code = "ASCE7-10")
        {
            //Default values
            double G = 0;

            //Calculation logic:

            WindExposureCategory Exposure;

            bool IsValidStringExposure = Enum.TryParse(WindExposureCategory, true, out Exposure);
            if (IsValidStringExposure == false)
            {
                throw new Exception("Exposure category is not recognized. Check input string.");
            }

            WindStructureDynamicResponseType response;
            bool IsValidStringResponse = Enum.TryParse(WindStructureDynamicResponseType, true, out response);
            if (IsValidStringResponse == false)
            {
                throw new Exception("Dynamic response type is not recognized. Specify Rigid or Flexible. Check input string.");
            }

            CalcLog Log = new CalcLog();
            WindStructure structure = new WindStructure(Log);
            G = structure.GetGustFacor(response,B,h,L,beta,n_1,V,Exposure);

            return new Dictionary<string, object>
            {
                { "G", G }
 
            };
        }


        //internal StructureParameters (string WindStructureDynamicResponseType,double B,double h,double L,double beta,double n_1,double V,string WindExposureCategory)
        //{

        //}
        //[IsVisibleInDynamoLibrary(false)]
        //public static StructureParameters  ByInputParameters(string WindStructureDynamicResponseType,double B,double h,double L,double beta,double n_1,double V,string WindExposureCategory)
        //{
        //    return new StructureParameters(WindStructureDynamicResponseType ,B ,h ,L ,beta ,n_1 ,V ,WindExposureCategory );
        //}

    }
}


