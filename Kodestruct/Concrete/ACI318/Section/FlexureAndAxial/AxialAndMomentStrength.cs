//#region Copyright
//   /*Copyright (C) 2015 Kodestruct Inc

//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at

//   http://www.apache.org/licenses/LICENSE-2.0

//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
//   */
//#endregion
 
//#region

//using Autodesk.DesignScript.Runtime;
//using Dynamo.Models;
//using System.Collections.Generic;
//using Dynamo.Nodes;
//using Kodestruct.Concrete.ACI318_14;
//using Kodestruct.Concrete.ACI;
//using Concrete.ACI318.General;
//using Kodestruct.Common.Section.Interfaces;
//using System;
//using Concrete.ACI318.Section.SectionTypes;
//using KodestructAci = Kodestruct.Concrete.ACI;

//#endregion

//namespace Concrete.ACI318.Section //.FlexureAndAxialForce
//{

///// <summary>
/////     SectionFlexural strength
/////     Category:   Concrete.ACI318_14.Section.Flexure
///// </summary>
///// 


//    public partial class FlexureAndAxialForce
//    {
//        /// <summary>
//        ///     Section flexural strength
//        /// </summary>
//        /// <param name="ConcreteSection">  Reinforced concrete section </param>
//        /// <param name="FlexuralCompressionFiberLocation">  Indicates whether the section in flexure has top or bottom in compression due to stresses from bending moment </param>
//        /// <param name="ConfinementReinforcementType">  Type of confinement reinforcement (spiral, ties or none) </param>
//        /// <param name="Code"> Applicable version of code/standard</param>
//        /// <returns name="phiM_n">  Design flexural strength at section   </returns>
//        /// <returns name="a"> Depth of equivalent rectangular stress block  </returns>
//        /// <returns name="c">  Distance from extreme compression fiber to neutral  axis  </returns>
//        /// <returns name="FlexuralFailureModeClassification"> Identifies if section is tension-controlled, transitional or compression-controlled </returns>
//        /// <returns name="epsilon_t">  Net tensile strain in extreme layer of longitudinal tension reinforcement at nominal strength,  excluding strains due to effective prestress, creep,  shrinkage, and temperature </returns>

//        [MultiReturn(new[] { "phiM_n", "a", "c", "FlexuralFailureModeClassification", "epsilon_t" })]
//        public static Dictionary<string, object> FlexuralStrength(ConcreteFlexureAndAxiaSection ConcreteSection,
//            string FlexuralCompressionFiberLocation = "Top", string ConfinementReinforcementType = "Ties", string Code = "ACI318-14")
//        {
//            //Default values
//            double phiM_n = 0;
//            double a = 0;
//            double c = 0;
//            string FlexuralFailureModeClassification = "";
//            double epsilon_t = 0;



//            //Calculation logic:

//            FlexuralCompressionFiberPosition p;
//            bool IsValidStringFiber = Enum.TryParse(FlexuralCompressionFiberLocation, true, out p);
//            if (IsValidStringFiber == false)
//            {
//                throw new Exception("Flexural compression fiber location is not recognized. Check input.");
//            }

//            ConfinementReinforcementType co;
//            bool IsValidStringConf = Enum.TryParse(ConfinementReinforcementType, true, out co);
//            if (IsValidStringConf == false)
//            {
//                throw new Exception("Confinement reinforcement type is not recognized. Check input.");
//            }


//             KodestructAci.IConcreteFlexuralMember beam = ConcreteSection.FlexuralSection;
//             ConcreteFlexuralStrengthResult result = beam.GetDesignFlexuralStrength(p, co);

//            //note that internally ACI nodes in Kodestruct use psi units consistent with ACI code
//            //convert to ksi units consistent with the rest of Dynamo nodes here

//             phiM_n = result.phiM_n/1000.0;

//             a = result.a;
//             c = result.a / ConcreteSection.ConcreteMaterial.Concrete.beta1;
//             FlexuralFailureModeClassification = result.FlexuralFailureModeClassification.ToString();
//             //epsilon_t = result.epsilon_t;

//            return new Dictionary<string, object>
//            {
//            { "phiM_n", phiM_n }
//            ,{ "a", a }
//            ,{ "c", c }
//            ,{ "FlexuralFailureModeClassification", FlexuralFailureModeClassification }
//            ,{ "epsilon_t", epsilon_t }
 
//            };
//        }




//    }
//}


