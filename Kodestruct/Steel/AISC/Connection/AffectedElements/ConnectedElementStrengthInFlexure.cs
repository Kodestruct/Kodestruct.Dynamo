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
using Analysis.Section;
using Kodestruct.Steel.AISC.Interfaces;
using Kodestruct.Steel.AISC.SteelEntities.Materials;
using Kodestruct.Steel.AISC.SteelEntities;
using Kodestruct.Steel.AISC.AISC360v10.Connections.AffectedMembers;
using Kodestruct.Common.CalculationLogger.Interfaces;
using Kodestruct.Common.CalculationLogger;
using Kodestruct.Common.Section.Interfaces;
using System;

#endregion

namespace Steel.AISC.Connection
{

    /// <summary>
    ///     Connected element strength in flexure 
    ///     Category:   Steel.AISC.Connection
    /// </summary>
    /// 


    public partial class AffectedElements
    {
        /// <summary>
        ///     Connected element strength in flexure. Checks gross section yielding, net section fracture and plate Stability if L_b >0  (kip - in unit system for all inputs and outputs)
        /// </summary>
        /// <param name="GrossShape"> Cross section shape (gross). Use Rectangular section or I-section   </param>
        /// <param name="NetShape"> Cross section shape (net)  Use SectionIWithFlangeHoles or SectionOfPlateWithHoles </param>
        /// <param name="F_y">  Specified minimum yield stress </param>
        /// <param name="F_u">  Specified minimum tensile strength   </param>
        /// <param name="L_b">  Length between points that are either braced against lateral displacement of compression flange or braced against twist of the cross section   </param>
        /// <param name="IsCompactDoublySymmetricForFlexure">  Indicates whether shape is compact for flexure and doubly symmetric </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="phiM_n"> Moment strength </returns>

        [MultiReturn(new[] { "phiM_n" })]
        public static Dictionary<string, object> ConnectedElementStrengthInFlexure(CustomProfile GrossShape,
            CustomProfile NetShape,
             double F_y, double F_u, double L_b = 0,
            bool IsCompactDoublySymmetricForFlexure = true, double C_b = 1,
            string Code = "AISC360-10")
        {
            //Default values
            double phiM_n = 0;


            ICalcLog log = new CalcLog();
            bool grossShapeValid=false, netShapeValid=false;


            AffectedElementInFlexure element = new AffectedElementInFlexure(GrossShape.Section, NetShape.Section, F_y, F_u, IsCompactDoublySymmetricForFlexure);
            phiM_n = element.GetFlexuralStrength(L_b);

            return new Dictionary<string, object>
            {
                { "phiM_n", phiM_n }
 
            };
        }



    }
}


