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
using System;

#endregion

namespace Steel.AISC.Connection
{

/// <summary>
///     Weld minimum size
///     Category:   Steel.AISC.Connection
/// </summary>
/// 


    public partial class Welded 
    {
        /// <summary>
        ///     Weld minimum size
        /// </summary>
        /// <param name="WeldType">  Weld type </param>
        /// <param name="t">  Thickness of element plate or element wall  </param>
        /// <returns name="w_weld"> Size of weld leg </returns>

        [MultiReturn(new[] { "w_weld" })]
        public static Dictionary<string, object> MinimumWeldSize(string WeldType = "Fillet", double t=0.25)
        {
            //Default values
            double w_weld = 0;


            //Calculation logic:
            if (WeldType == "Fillet")
            {
                w_weld = FilletWeldLimits.GetMinimumWeldSize(t);
            }

            else if (WeldType == "PJP")
            {
                PJPGrooveWeld pjp = new PJPGrooveWeld(36, 58, 70, 0, 0, 0);
                w_weld = pjp.GetMinimumEffectiveThroat(t);
            }
            else if (WeldType =="CJP" )
            {
                w_weld = -1;
            }
            else
            {
                throw new Exception("Weld type not recognized");
            }

            return new Dictionary<string, object>
            {
                { "w_weld", w_weld }
 
            };
        }




    }
}


