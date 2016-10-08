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

namespace Steel.AISC_10.Connection
{

/// <summary>
///     Bolt minimum edge distance
///     Category:   Steel.AISC_10.Connection
/// </summary>
/// 


    [IsDesignScriptCompatible]
    public partial class Bolted 
    {
/// <summary>
///    Calculates Bolt minimum edge distance
/// </summary>
        /// <param name="d_b">  Nominal fastener diameter </param>

        /// <returns name="l_e"> Total effective weld length of groove and fillet welds to rectangular HSS for weld strength calculations   </returns>

        [MultiReturn(new[] { "l_e" })]
        public static Dictionary<string, object> MinimumBoltEdgeDistance(double d_b)
        {
            //Default values
            double l_e = 0;


            //Calculation logic:


            return new Dictionary<string, object>
            {
                { "l_e", l_e }
 
            };
        }


        //internal Bolted (double d_b)
        //{

        //}
        //[IsVisibleInDynamoLibrary(false)]
        //public static Bolted  ByInputParameters(double d_b)
        //{
        //    return new Bolted(d_b );
        //}

    }
}


