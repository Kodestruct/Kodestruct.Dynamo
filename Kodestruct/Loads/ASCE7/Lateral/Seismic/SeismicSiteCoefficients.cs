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
using Kodestruct.Loads.ASCE7.Entities;
using System;
using Kodestruct.Common.CalculationLogger;

#endregion

namespace Loads.ASCE7.Lateral.Seismic
{

/// <summary>
///     Seismic site coefficients
///     Category:   Loads.ASCE7v10.Lateral.Seismic
/// </summary>
/// 

    public partial class Site 
    {
        /// <summary>
        ///     Seismic site coefficients  accounting for short-period  and long-period soil amplification of base accelerations 
        /// </summary>
        /// <param name="S_S">  Mapped mcer, 5 percent damped, spectral response acceleration parameter at short periods </param>
        /// <param name="S_1">  Mapped MCER, 5 percent damped, spectral response acceleration parameter at a period of 1 s </param>
        /// <param name="SiteClass">  Seismic site class (as a function of soil type) </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="F_a"> Short-period site coefficient (at 0.2 s-period) </returns>
        /// <returns name="F_v"> Long-period site coefficient (at 1.0 s-period) </returns>

        [MultiReturn(new[] { "F_a","F_v" })]
        public static Dictionary<string, object> SeismicSiteCoefficients(double S_S, double S_1, string SiteClass, string Code = "ASCE7-10")
        {
            //Default values
        double F_a = 0;
        double F_v = 0;


            //Calculation logic:

            SeismicSiteClass _SiteClass;
            bool ValidSiteClass = Enum.TryParse(SiteClass, out _SiteClass);

            if (ValidSiteClass == false)
            {
                throw new Exception("Site class is not recognized. Please check input.");
            }
            else
            {


                CalcLog log = new CalcLog();
                Kodestruct.Loads.ASCE.ASCE7_10.SeismicLoads.Site s = new Kodestruct.Loads.ASCE.ASCE7_10.SeismicLoads.Site(_SiteClass, log);
                F_a = s.SiteCoefficientFa(S_S);
                F_v = s.SiteCoefficientFv(S_1);
            }

            return new Dictionary<string, object>
            {
                { "F_a", F_a }
            ,{ "F_v", F_v }
 
            };
            }


    }
}


