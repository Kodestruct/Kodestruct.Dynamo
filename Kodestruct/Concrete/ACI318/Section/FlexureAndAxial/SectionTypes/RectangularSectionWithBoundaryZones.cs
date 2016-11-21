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
using Kodestruct.Concrete.ACI318_14;
using Concrete.ACI318.General;
using Concrete.ACI318.General.Reinforcement;
using Concrete.ACI318.General.Concrete;
using Dynamo.Graph.Nodes;
using KodestructAci = Kodestruct.Concrete.ACI;
using KodestructAci14 = Kodestruct.Concrete.ACI318_14;
using Kodestruct.Common.CalculationLogger;

#endregion

namespace Concrete.ACI318.Section.SectionTypes
{

    /// <summary>
    ///   Rectangular section four sides
    ///   Category:   Concrete.ACI318_14.General
    /// </summary>



    [IsDesignScriptCompatible]
    public partial class RectangularSectionWithBoundaryZones : ConcreteFlexureAndAxiaSection
    {



        [IsVisibleInDynamoLibrary(false)]
        internal RectangularSectionWithBoundaryZones(double b, double h, double A_sTopBottom, double A_sLeftRight,
         double c_cntrTopBottom, double c_cntrLeftRight, ConcreteMaterial ConcreteMaterial, RebarMaterial LongitudinalRebarMaterial, 
            bool hasTies = false)
        {


            KodestructAci.ConfinementReinforcementType ConfinementReinforcementType;
            if (hasTies == true)
            {
                ConfinementReinforcementType = KodestructAci.ConfinementReinforcementType.Ties;
            }
            else
            {
                ConfinementReinforcementType = KodestructAci.ConfinementReinforcementType.NoReinforcement;
            }

            base.ConcreteMaterial = ConcreteMaterial; //duplicate save of concrete material into base Dynamo class

            FlexuralSectionFactory flexureFactory = new FlexuralSectionFactory();
            ConcreteSectionFlexure fs = flexureFactory.GetRectangularSectionFourSidesDistributed(b, h,A_sTopBottom, A_sLeftRight,
                c_cntrTopBottom, c_cntrLeftRight,
                ConcreteMaterial.Concrete, LongitudinalRebarMaterial.Material,ConfinementReinforcementType);


            this.FlexuralSection = fs;
        }

        /// <summary>
        /// Rectangular section with reinforcement on 4-sides
        /// </summary>
        /// <param name="b">Width of compression face of member</param>
        /// <param name="h">Overall thickness, height, or depth of member</param>
        /// <param name="A_sTopBottom">Total area of top/bottom reinforcement (per face)</param>
        /// <param name="A_sLeftRight">Total area of reinforcement on left-hand/right-hand side face (per face)</param>
        /// <param name="c_cntrTopBottom">Concrete cover to tension rebar centroid (top or bottom face)</param>
        /// <param name="c_cntrLeftRight">Concrete cover to tension rebar centroid (side faces)</param>
        /// <param name="ConcreteMaterial">Concrete material</param>
        /// <param name="LongitudinalRebarMaterial">Rebar material for longitudinal bars</param>
        /// <param name="hasTies">Identifies if member has ties around longitudinal reinforcement</param>
        /// <returns name="RectangularSectionWithBoundaryZones">  Section [OBJECT] </returns>

        public static RectangularSectionWithBoundaryZones ByWidthHeigthAndReinforcementArea(double b, double h, double A_sTopBottom,  double A_sLeftRight,
         double c_cntrTopBottom, double c_cntrLeftRight, 
            ConcreteMaterial ConcreteMaterial, RebarMaterial LongitudinalRebarMaterial, bool HasTies = true)
        {
            return new RectangularSectionWithBoundaryZones(b, h, A_sTopBottom, A_sLeftRight, c_cntrTopBottom, c_cntrLeftRight, 
                ConcreteMaterial, LongitudinalRebarMaterial,HasTies);
        }


    }
}


