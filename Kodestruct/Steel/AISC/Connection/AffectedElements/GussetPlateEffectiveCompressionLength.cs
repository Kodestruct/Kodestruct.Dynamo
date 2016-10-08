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
using Kodestruct.Steel.AISC360v10.Connections.AffectedElements;
using Kodestruct.Steel.AISC;
using System;

#endregion

namespace Steel.AISC.Connection
{

/// <summary>
///     GussetPlateEffectiveCompressionLength
///     Category:   Steel.AISC.Connection
/// </summary>
/// 


    public partial class AffectedElements 
    {
        /// <summary>
        ///    Calculates Gusset plate effective compression length
        /// </summary>
        /// <param name="GussetPlateConfigurationId">  Type of gusset plate configuration for calculation of effective length </param>
        /// <param name="l_1">  Gusset plate distance from beam to nearest row of bolts </param>
        /// <param name="l_2">  Gusset plate distance from column to nearest row of bolts </param>
        /// <param name="IsGussetCompactConfiguration">  Indicates whether gusset plate configuration is compact (per Design Guide 29 Appendix C) </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="KL_gusset"> Effective length of gusset plate </returns>

        [MultiReturn(new[] { "KL_gusset" })]
        public static Dictionary<string, object> GussetPlateEffectiveCompressionLength(string GussetPlateConfigurationId, double l_1, double l_2, bool IsGussetCompactConfiguration = false, string Code = "AISC360-10")
        {
            //Default values
            double KL_gusset = 0;


            //Calculation logic:
            AffectedElement el = new AffectedElement();
            GussetPlateConfiguration conf = ParseGussetConfiguration(GussetPlateConfigurationId, IsGussetCompactConfiguration);
            KL_gusset = el.GetGussetPlateEffectiveCompressionLength(conf, l_1, l_2);

            return new Dictionary<string, object>
            {
                { "KL_gusset", KL_gusset }
 
            };
        }

        private static GussetPlateConfiguration ParseGussetConfiguration(string GussetPlateConfigurationId, bool IsGussetCompactConfiguration)
        {
            GussetPlateConfiguration plateConfiguration;
            if (GussetPlateConfigurationId=="Corner")
            {
                if (IsGussetCompactConfiguration == true)
                {
                    return GussetPlateConfiguration.CompactCorner;
                }
                else
                {
                    return GussetPlateConfiguration.NoncompactCorner;
                }
            }
            else
            {
                bool IsValidString = Enum.TryParse(GussetPlateConfigurationId, true, out plateConfiguration);
                if (IsValidString == true)
                {
                    return plateConfiguration;
                }
                else
                {
                    throw new Exception("Gusset effective length calculation failed. Invalid configuration case designation.");
                }
            }
        }




    }
}


