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

#endregion

namespace Concrete.ACI318_14.Beam.Flexure
{

/// <summary>
///     Beam flexural strength
///     Category:   Concrete.ACI318_14.Beam.Flexure
/// </summary>
/// 


    [IsDesignScriptCompatible]
    public partial class Flexure 
    {
/// <summary>
///     Beam flexural strength
/// </summary>
        /// <param name="ConcreteSection">  Reinforced concrete section </param>
/// <param name="FlexuralCompressionFiberLocation">  Indicates whether the section in flexure has top or bottom in compression due to stresses from bending moment </param>
/// <param name="ConfinementReinforcementType">  Type of confinement reinforcement (spiral, ties or none) </param>

        /// <returns name="phiM_n">  Design flexural strength at section   </returns>
/// <returns name="a"> Depth of equivalent rectangular stress block  </returns>
/// <returns name="c">  Distance from extreme compression fiber to neutral  axis  </returns>
/// <returns name="FlexuralFailureModeClassification"> Identifies if section is tension-controlled, transitional or compression-controlled </returns>
/// <returns name="epsilon_t">  Net tensile strain in extreme layer of longitudinal tension reinforcement at nominal strength,  excluding strains due to effective prestress, creep,  shrinkage, and temperature </returns>

        [MultiReturn(new[] { "phiM_n","a","c","FlexuralFailureModeClassification","epsilon_t" })]
        public static Dictionary<string, object> BeamFlexuralStrength(ConcreteSection ConcreteSection,string FlexuralCompressionFiberLocation,string ConfinementReinforcementType)
        {
            //Default values
            double phiM_n = 0;
double a = 0;
double c = 0;
string FlexuralFailureModeClassification = "";
double epsilon_t = 0;


            //Calculation logic:


            return new Dictionary<string, object>
            {
                { "phiM_n", phiM_n }
,{ "a", a }
,{ "c", c }
,{ "FlexuralFailureModeClassification", FlexuralFailureModeClassification }
,{ "epsilon_t", epsilon_t }
 
            };
        }


        //internal Flexure (ConcreteSection ConcreteSection,string FlexuralCompressionFiberLocation,string ConfinementReinforcementType)
        //{

        //}
        //[IsVisibleInDynamoLibrary(false)]
        //public static Flexure  ByInputParameters(ConcreteSection ConcreteSection,string FlexuralCompressionFiberLocation,string ConfinementReinforcementType)
        //{
        //    return new Flexure(ConcreteSection ,FlexuralCompressionFiberLocation ,ConfinementReinforcementType );
        //}

    }
}


