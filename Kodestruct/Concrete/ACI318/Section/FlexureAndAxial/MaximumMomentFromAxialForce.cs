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
using Kodestruct.Common.Section.Interfaces;
//using Kodestruct.Concrete.ACI318.Section.FlexureAndAxial;

#endregion

namespace Concrete.ACI318.Section
{

/// <summary>
///     Moment strength with axial force
///     Category:   Concrete.ACI318.Section.FlexureAndAxialForce
/// </summary>
/// 


    public partial class FlexureAndAxialForce
    {
        /// <summary>
        ///     Moment strength with axial force
        /// </summary>
        /// <param name="P_u">   Factored axial force; to be taken as positive for  compression and negative for tension  </param>
        /// <param name="ConcreteSection">  Reinforced concrete section </param>
        /// <param name="ConfinementReinforcementType">  Type of compression member confinement reinforcement (tied, spiral etc) </param>
        /// <returns name="phiM_n">  Design flexural strength at section   </returns>


        public static double MaximumMomentFromAxialForce(double P_u, ConcreteFlexureAndAxiaSection ConcreteSection, 
            string ConfinementReinforcementType="Ties", string FlexuralCompressionFiberLocation = "Top",
            bool IsPrestressed = false, string Code = "ACI318-14")
        {
            //Default values
            double phiM_n = 0.0;

            //Calculation logic:

            
            ConfinementReinforcementType ConfinementReinforcement;
            bool IsValidConfinementReinforcementType = Enum.TryParse(ConfinementReinforcementType, true, out ConfinementReinforcement);
            if (IsValidConfinementReinforcementType == false)
            {
                throw new Exception("Failed to convert string. ConfinementReinforcementType. Please check input");
            }

            FlexuralCompressionFiberPosition p;
            bool IsValidStringFiber = Enum.TryParse(FlexuralCompressionFiberLocation, true, out p);
            if (IsValidStringFiber == false)
            {
                throw new Exception("Flexural compression fiber location is not recognized. Check input.");
            }


            CalcLog log = new CalcLog();
            if (ConcreteSection.FlexuralSection is IConcreteSectionWithLongitudinalRebar)
            {
                IConcreteSectionWithLongitudinalRebar Section = (IConcreteSectionWithLongitudinalRebar)ConcreteSection.FlexuralSection;
                ConcreteSectionCompression column = new ConcreteSectionCompression(Section, ConfinementReinforcement, log);
                //Convert axial force to pounds
                double P_u_pounds = P_u * 1000.0;
                phiM_n = column.GetDesignMomentWithCompressionStrength(P_u_pounds, p).phiM_n/1000.0; // convert to kip inch units
            }
            else
            {
                throw new Exception("Section of wrong type. Create a section type that has longitudinal bars as part of definition.");
            }

            return phiM_n;
        }


    }
}


