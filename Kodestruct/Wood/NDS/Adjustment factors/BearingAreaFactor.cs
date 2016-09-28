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

#endregion

namespace Wood.NDS
{

/// <summary>
///     Bearing  area factor
///     Category:   Wood.NDS
/// </summary>
/// 


    [IsDesignScriptCompatible]
    public partial class AdjustmentFactor 
    {
/// <summary>
///     Bearing  area factor
/// </summary>
        /// <param name="l_b">  Bearing length measured parallel to grain, in.  </param>
/// <param name="IsMemberEnd">  Identifies if area under consideration is at the member end </param>

        /// <returns name="C_b"> Bearing area factor </returns>

        [MultiReturn(new[] { "C_b" })]
        public static Dictionary<string, object> BearingAreaFactor(double l_b,bool IsMemberEnd)
        {
            //Default values
            double C_b = 0;


            //Calculation logic:


            return new Dictionary<string, object>
            {
                { "C_b", C_b }
 
            };
        }


        //internal AdjustmentFactor (double l_b,bool IsMemberEnd)
        //{

        //}
        //[IsVisibleInDynamoLibrary(false)]
        //public static AdjustmentFactor  ByInputParameters(double l_b,bool IsMemberEnd)
        //{
        //    return new AdjustmentFactor(l_b ,IsMemberEnd );
        //}

    }
}


