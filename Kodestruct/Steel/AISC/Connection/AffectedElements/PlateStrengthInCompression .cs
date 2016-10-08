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
using Kodestruct.Steel.AISC360v10.Connections.AffectedElements;

#endregion

namespace Steel.AISC.Connection
{

/// <summary>
///     Plate strength in compression 
///     Category:   Steel.AISC.Connection
/// </summary>
/// 


    public partial class AffectedElements 
    {
        /// <summary>
        ///    Calculates Connected element strength in compression 
        /// </summary>
        /// <param name="F_y">  Specified minimum yield stress </param>
        /// <param name="KL">  Effective length of element in compression </param>
        /// <param name="b">  Width of stiffened or unstiffened compression element </param>
        /// <param name="t">  Thickness of element plate or element wall  </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="phiR_n"> Strength of member or connection </returns>

        [MultiReturn(new[] { "phiR_n" })]
        public static Dictionary<string, object> PlateStrengthInCompression(double F_y, double KL, double b, double t, string Code = "AISC360-10")
        {
            //Default values
            double phiR_n = 0;


            //Calculation logic:
            AffectedElementInCompression compMember = new AffectedElementInCompression(F_y, b, t);
            phiR_n = compMember.GetCompressionCapacity(KL);

            return new Dictionary<string, object>
            {
                { "phiR_n", phiR_n }
 
            };
        }


    }
}


