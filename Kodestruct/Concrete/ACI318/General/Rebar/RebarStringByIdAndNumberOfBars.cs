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

#endregion

namespace Concrete.ACI318.General.Reinforcement
{

/// <summary>
///     Rebar string by Id and number of bars
///     Category:   Concrete.ACI318.General.Reinforcement
/// </summary>
/// 



    public partial class SizeAreaAndProperties 
    {
        /// <summary>
        ///     Rebar string by number of bars
        /// </summary>
        /// <param name="RebarSizeId">  Rebar designation (number) indicating the size of the bar </param>
        /// <param name="N_bars">  Number of bars </param>
        /// <returns name="RebarText">  Text representing reinforcement pattern  </returns>

        [MultiReturn(new[] { "RebarText" })]
        public static Dictionary<string, object> RebarStringByIdAndNumberOfBars(string RebarSizeId,double N_bars)
        {
            //Default values
            string RebarText;


            //Calculation logic:
            string BarText = RebarSizeId.Substring(2);

            if (N_bars>1)
            {
                RebarText = "("+N_bars+")"+"#"+BarText;
            }
            else
            {
                RebarText = "#"+BarText;
            }

            return new Dictionary<string, object>
            {
                { "RebarText", RebarText }
 
            };
        }



    }
}


