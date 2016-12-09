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
using Kodestruct.Steel.AISC.AISC360v10.Connections.Bolted;
using Kodestruct.Steel.AISC.Interfaces;
using Kodestruct.Steel.AISC;
using b = Kodestruct.Steel.AISC.SteelEntities.Bolts;

#endregion

namespace Steel.AISC.Connection
{

/// <summary>
///     Slip-critical bolt combined tension and shear
///     Category:   Steel.AISC.Connection
/// </summary>
/// 



    public partial class Bolted 
    {
        /// <summary>
        ///    Calculates Modified shear strength slip-critical bolt combined tension and shear (kip - in unit system for all inputs and outputs)
        /// </summary>
        /// <param name="d_b">  Nominal fastener diameter </param>
        /// <param name="T_u">  Required tension force </param>
        /// <param name="BoltMaterialId">  Bolt material specification </param>
        /// <param name="BoltHoleType">  Type of bolt hole </param>
        /// <param name="BoltFillerCase">  Distinguishes between filler cases for slip-critical bolt capacity calculations </param>
        /// <param name="BoltFayingSurfaceClass">  Identifies the type of faying surface for a slip critical bolt </param>
        /// <param name="NumberShearPlanes">  Number of shear planes </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="phiR_nModified"> Modified shear strength of bolt subjected to tension </returns>

        [MultiReturn(new[] { "phiR_nModified" })]
        public static Dictionary<string, object> SlipCriticalBoltCombinedTensionAndShear(double d_b, double T_u, string BoltMaterialId, string BoltHoleType, string BoltFillerCase = "One",
            string BoltFayingSurfaceClass = "ClassA", double NumberShearPlanes = 1, string Code = "AISC360-10")
        {
            //Default values
            double phiR_nModified = 0;

            BoltFayingSurfaceClass SurfaceClass = ParseSurfaceClass(BoltFayingSurfaceClass);
            BoltFillerCase FillerCase = ParseFillerCase(BoltFillerCase);
            b.BoltHoleType HoleType = ParseBoltHoleType(BoltHoleType); 

            //Calculation logic:
            BoltFactory bf = new BoltFactory(BoltMaterialId);
            IBoltSlipCritical bolt = bf.GetSlipCriticalBolt(d_b, BoltThreadCase.Included, SurfaceClass, HoleType, FillerCase, NumberShearPlanes);

            phiR_nModified = bolt.GetReducedSlipResistance(T_u);

            return new Dictionary<string, object>
            {
                { "phiR_nModified", phiR_nModified }
 
            };
        }


    }
}


