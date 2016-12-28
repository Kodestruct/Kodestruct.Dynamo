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
using Kodestruct.Steel.AISC.AISC360v10.Connections.Weld;

#endregion

namespace Steel.AISC.Connection
{

/// <summary>
///     Fillet weld effective length
///     Category:   Steel.AISC.Connection
/// </summary>
/// 



    public partial class Welded 
    {
        /// <summary>
        ///     Fillet weld effective length
        /// </summary>
        /// <param name="w_weld">  Size of weld leg </param>
        /// <param name="l">Weld actual length</param>
        /// <param name="IsEndLoaded">Indicates if weld is end loaded</param>
        /// <returns name="l_eff"> Effective weld length </returns>

        [MultiReturn(new[] { "l_eff" })]
        public static Dictionary<string, object> FilletWeldEffectiveLength(double w_weld, double l, bool IsEndLoaded)
        {
            //Default values
            double l_eff = 0;


            //Calculation logic:
            l_eff = FilletWeldLimits.GetEffectiveLegth(w_weld, l, IsEndLoaded);


            return new Dictionary<string, object>
            {
                { "l_eff", l_eff }
 
            };
        }



    }
}


