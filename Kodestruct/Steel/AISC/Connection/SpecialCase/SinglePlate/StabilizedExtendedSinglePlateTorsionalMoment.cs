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
using conx = Kodestruct.Steel.AISC.AISC360v10.Connections;

#endregion

namespace Steel.AISC.Connection.SpecialCase
{

/// <summary>
///     Stabilized extended single plate design moment
///     Category:   Steel.AISC.Connection.SpecialCase
/// </summary>
/// 


    public partial class ExtendedSinglePlate 
    {
        /// <summary>
        ///    Extended single plate design torsional moment, to be compared with flexural strength of stabilized single plate per AISC SCM Chapter 10
        /// </summary>
        /// <param name="R_u">  Required strength </param>
        /// <param name="t_w">  Thickness of web  </param>
        /// <param name="t_p">  Thickness of plate   </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="M_tu"> Stabilized extended shear tab design moment </returns>

        [MultiReturn(new[] { "M_tu" })]
        public static Dictionary<string, object> StabilizedExtendedSinglePlateTorsionalMoment(double R_u, double t_w, double t_p, string Code = "AISC360-10")
        {
            //Default values
            double M_tu = 0;


            //Calculation logic:
            conx.ExtendedSinglePlate sp = new conx.ExtendedSinglePlate();
            M_tu = sp.StabilizedExtendedSinglePlateTorsionalMoment(R_u, t_p, t_w);

            return new Dictionary<string, object>
            {
                { "M_tu", M_tu }
 
            };
        }


    }
}


