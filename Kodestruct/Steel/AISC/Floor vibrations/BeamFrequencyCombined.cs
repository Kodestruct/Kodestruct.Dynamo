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
using Kodestruct.Steel.AISC.Entities.FloorVibrations;

#endregion

namespace Steel.AISC.FloorVibrations
{

/// <summary>
///     Beam frequency combined
///     Category:   Steel.AISC10
/// </summary>
/// 



    public partial class FundamentalFrequency 
    {
        /// <summary>
        ///     Combined beam-girder system frequency
        /// </summary>
        /// <param name="Delta_j">  Joist deflection </param>
        /// <param name="Delta_g">  Girder deflection </param>
        /// <param name="Delta_c">Column deflection</param>
        /// <returns name="f_n"> Combined beam-girder system fundamental frequency </returns>

        [MultiReturn(new[] { "f_n" })]
        public static Dictionary<string, object> BeamSystemFrequencyCombined(double Delta_j, double Delta_g, double Delta_c = 0)
        {
            //Default values
            double f = 0;


            //Calculation logic:
            FloorVibrationBeamGirderPanel bgPanel = new FloorVibrationBeamGirderPanel();
            f = bgPanel.GetCombinedModeFundamentalFrequency(Delta_j, Delta_g, Delta_c);

            return new Dictionary<string, object>
            {
                { "f_n", f }
 
            };
        }


    }
}


