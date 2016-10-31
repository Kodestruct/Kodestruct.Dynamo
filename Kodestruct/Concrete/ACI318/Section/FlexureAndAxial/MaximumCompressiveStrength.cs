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
using Concrete.ACI318.Section.SectionTypes;
using Kodestruct.Common.CalculationLogger;
using Kodestruct.Concrete.ACI;
using Kodestruct.Concrete.ACI318_14;
using System;

#endregion

namespace Concrete.ACI318.Section
{

/// <summary>
///     Maximum compressive strength
///     Category:   Concrete.ACI318.Section
/// </summary>
/// 


    public partial class FlexureAndAxialForce 
    {
/// <summary>
///     Maximum compressive strength
/// </summary>
        /// <param name="ConcreteSection">  Reinforced concrete section </param>
/// <param name="ConfinementReinforcementType">  Type of confinement reinforcement (spiral, ties or none) </param>
/// <param name="FlexuralCompressionFiberLocation">  Indicates whether the section in flexure has top or bottom in compression due to stresses from bending moment </param>
/// <param name="IsPrestressed">  Indicates if member is prestressed </param>
/// <param name="Code">  Applicable version of code/standard </param>
        /// <returns name="phiP_o">  Axial strength at zero eccentricity  (maximum compressive strength) </returns>

        [MultiReturn(new[] { "phiP_o" })]
        public static Dictionary<string, object> MaximumCompressiveStrength(ConcreteFlexureAndAxiaSection ConcreteSection, 
            string ConfinementReinforcementType="Ties", 
            string FlexuralCompressionFiberLocation="Top",
            bool IsPrestressed = false, string Code = "ACI318-14")
        {
            //Default values
            double phiP_o = 0;


            //Calculation logic:

            ConfinementReinforcementType ConfinementReinforcement;
            bool IsValidConfinementReinforcementType = Enum.TryParse(ConfinementReinforcementType, true, out ConfinementReinforcement);
            if (IsValidConfinementReinforcementType == false)
            {
                throw new Exception("Failed to convert string. ConfinementReinforcementType. Please check input");
            }
                double P_o = 0.0;
                CalcLog log = new CalcLog();
                IConcreteSectionWithLongitudinalRebar Section = (IConcreteSectionWithLongitudinalRebar)ConcreteSection.FlexuralSection;
                ConcreteSectionCompression column = new ConcreteSectionCompression(Section, ConfinementReinforcement, log);
                //double P_o = column.GetMaximumForce() / 1000.0; // convert to kip inch units;
                double phi = 0.65;
                phiP_o = phi * P_o;

            return new Dictionary<string, object>
            {
                { "phiP_o", phiP_o }
 
            };
        }



    }
}


