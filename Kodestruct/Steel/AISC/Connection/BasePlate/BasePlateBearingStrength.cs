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


#endregion

namespace Steel.AISC.Connection.BasePlate
{

/// <summary>
///     Base plate bearing strength
///     Category:   Steel.AISC10.Connection
/// </summary>
/// 



    public partial class Bearing
    {
        /// <summary>
        ///     Base plate bearing strength
        /// </summary>
        /// <param name="BasePlateShape">  Base plate shape object , created from inut parameters </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="phiP_p"> Bearing strength   </returns>

        [MultiReturn(new[] { "phiP_p" })]
        public static Dictionary<string, object> BasePlateBearingStrength(BasePlateShapeObject BasePlateShape, string Code = "AISC360-10")
        {
            //Default values
            double phiP_p = 0;


            //Calculation logic:
            phiP_p = BasePlateShape.Plate.GetphiP_p();

            return new Dictionary<string, object>
            {
                { "phiP_p", phiP_p }
 
            };
        }


    }
}


