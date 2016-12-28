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
using Kodestruct.Steel.AISC.AISC360v10.Connections.WebOpenings;

#endregion

namespace Steel.AISC.Connection
{

/// <summary>
///     Maximum web opening width
///     Category:   Steel.AISC.Connection
/// </summary>
/// 



    public partial class BeamWebOpening 
    {
        /// <summary>
        ///     Maximum web opening width
        /// </summary>
        /// <param name="h_o">  Distance between the flange centroids  </param>
        /// <param name="d">  Full nominal depth of the section    </param>
        /// <param name="t_f">  Thickness of flange   </param>
        /// <param name="t_w">  Thickness of web  </param>
        /// <param name="F_y">  Specified minimum yield stress </param>
        /// <param name="IsCompositeBeam">  Identifies whether member is composite </param>
        /// <param name="IsSingleSideReinforcement">  Identifies whether web reinforcing plate is placed on one side of web </param>
        /// <returns name="a_o"> Length of opening </returns>

        [MultiReturn(new[] { "a_o" })]
        public static Dictionary<string, object> WebOpeningMaximumWidth(double h_o,double d,double t_f,double t_w,double F_y,bool IsCompositeBeam =true,bool IsSingleSideReinforcement=false)
        {
            //Default values
            double a_o = 0;


            //Calculation logic:
            a_o = WebOpeningGeneral.GetMaximumOpeningWidth(h_o, d, t_f, t_w, F_y, IsCompositeBeam, IsSingleSideReinforcement);

            return new Dictionary<string, object>
            {
                { "a_o", a_o }
 
            };
        }



    }
}


