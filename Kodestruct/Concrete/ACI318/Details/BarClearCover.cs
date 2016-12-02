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
using Kodestruct.Concrete.ACI.ACI318_14.Durability.Cover;
using Kodestruct.Concrete.ACI.Entities;
using System;

#endregion

namespace Concrete.ACI318.Details
{

/// <summary>
///     Bar clear cover
///     Category:   Concrete.ACI318.Details
/// </summary>
/// 



    public partial class Cover 
    {
        /// <summary>
        ///     Bar clear cover
        /// </summary>
        /// <param name="CoverCaseId">  Concrete member type for clear cover case  selection </param>
        /// <param name="RebarSizeId">  Rebar designation (number) indicating the size of the bar </param>
        /// <param name="CheckBarDiameter">  Indicates if a minimum dimension of one bar diameter is enforced for clear cover </param>
        /// <returns name="c_c">  Clear cover of reinforcement  </returns>

        [MultiReturn(new[] { "c_c" })]
        public static Dictionary<string, object> BarClearCover(string CoverCaseId,string RebarSizeId,
            bool CheckBarDiameter=true)
        {
            //Default values
            double c_c = 0;


            //Calculation logic:

            RebarDesignation des;
            bool IsValidString = Enum.TryParse(RebarSizeId, true, out des);
            if (IsValidString == false)
            {
                throw new Exception("Rebar size is not recognized. Check input.");
            }


            RebarCoverFactory rcf = new RebarCoverFactory();
            c_c = rcf.GetRebarCover(CoverCaseId, des, CheckBarDiameter);

            return new Dictionary<string, object>
            {
                { "c_c", c_c }
 
            };
        }


    }
}


