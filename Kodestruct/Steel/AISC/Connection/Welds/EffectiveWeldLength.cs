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
///     Weld effective length
///     Category:   Steel.AISC_10.Connection
/// </summary>
/// 


    [IsDesignScriptCompatible]
    public partial class Welded 
    {
/// <summary>
///    Calculates Weld effective length
/// </summary>
        /// <param name="l_weld">  Weld length </param>

        /// <returns name="l_eff"> Effective weld length </returns>

        [MultiReturn(new[] { "l_eff" })]
        public static Dictionary<string, object> EffectiveWeldLength(double l_weld)
        {
            //Default values
            double l_eff = 0;


            //Calculation logic:


            return new Dictionary<string, object>
            {
                { "l_eff", l_eff }
 
            };
        }


        //internal Welded (double l_weld)
        //{

        //}
        //[IsVisibleInDynamoLibrary(false)]
        //public static Welded  ByInputParameters(double l_weld)
        //{
        //    return new Welded(l_weld );
        //}

    }
}


