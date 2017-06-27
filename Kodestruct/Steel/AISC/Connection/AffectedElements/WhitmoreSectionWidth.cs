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
using Kodestruct.Steel.AISC360v10.Connections.AffectedElements;


#endregion

namespace Steel.AISC.Connection
{

/// <summary>
///     Whitmore section width
///     Category:   Steel.AISC.Connection
/// </summary>
/// 


    public partial class AffectedElements 
    {
        /// <summary>
        ///    Calculates Width of Whitmore section
        /// </summary>
        /// <param name="l">  Length of gusset   </param>
        /// <param name="b_con">  Connected element (Whitmore section) width </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="b_Whitmore"> Whitmore section width </returns>

        [MultiReturn(new[] { "b_Whitmore" })]
        public static Dictionary<string, object> WhitmoreSectionWidth(double l, double b_con, string Code = "AISC360-10")
        {
            //Default values
            double b_Whitmore = 0;


            //Calculation logic:
            AffectedElement el = new AffectedElement();
            b_Whitmore = el.GetWhitmoreSectionWidth(l, b_con);

            return new Dictionary<string, object>
            {
                { "b_Whitmore", b_Whitmore }
 
            };
        }


    }
}


