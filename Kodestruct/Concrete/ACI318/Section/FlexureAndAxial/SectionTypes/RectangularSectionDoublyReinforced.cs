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
    ///   Rectangular section doubly reinforced
    ///   Category:   Concrete.ACI318_14.General
    /// </summary>



    [IsDesignScriptCompatible]
    public partial class RectangularSectionDoublyReinforced : ConcreteFlexureAndAxiaSection
    {

        //Default values
        //double b;
        //double h;
        //double A_s;
        //double c_cntr;
        //ConcreteMaterial ConcreteMaterial;
        //RebarMaterial LongitudinalRebarMaterial;


        [IsVisibleInDynamoLibrary(false)]
        internal RectangularSectionDoublyReinforced(double b, double h, double A_sTop, double A_sBot,
         double c_cntr, ConcreteMaterial ConcreteMaterial, RebarMaterial LongitudinalRebarMaterial, bool hasTies = false)
        {

            CrossSectionRectangularShape shape = new CrossSectionRectangularShape(ConcreteMaterial.Concrete, null, b, h);
            base.ConcreteMaterial = ConcreteMaterial; //duplicate save of concrete material into base Dynamo class

            List<KodestructAci.RebarPoint> LongitudinalBars = new List<KodestructAci.RebarPoint>();

            KodestructAci.Rebar TopRebar = new KodestructAci.Rebar(A_sTop, LongitudinalRebarMaterial.Material);
            KodestructAci.RebarPoint TopPoint = new KodestructAci.RebarPoint(TopRebar, new KodestructAci.RebarCoordinate() { X = 0, Y = h / 2.0 - c_cntr });
            LongitudinalBars.Add(TopPoint);

            KodestructAci.Rebar BottomRebar = new KodestructAci.Rebar(A_sBot, LongitudinalRebarMaterial.Material);
            KodestructAci.RebarPoint BottomPoint = new KodestructAci.RebarPoint(BottomRebar, new KodestructAci.RebarCoordinate() { X = 0, Y = -h / 2.0 + c_cntr });
            LongitudinalBars.Add(BottomPoint);

            KodestructAci.IConcreteFlexuralMember fs = new KodestructAci14.ConcreteSectionFlexure(shape, LongitudinalBars, new CalcLog());
            this.FlexuralSection = fs;
        }

        /// <summary>
        /// Rectangular section with reinforcement doubly reinforced
        /// </summary>
        /// <param name="b">Width of compression face of member</param>
        /// <param name="h">Overall thickness, height, or depth of member</param>
        /// <param name="A_sTop">Total area of top reinforcement</param>
        /// <param name="A_sBot">Total area of bottom reinforcement</param>
        /// <param name="c_cntr">Concrete cover to tension rebar centroid</param>
        /// <param name="ConcreteMaterial">Concrete material</param>
        /// <param name="LongitudinalRebarMaterial">Rebar material for longitudinal bars</param>
        /// <returns name="RectangularSectionDoublyReinforced">  Section [OBJECT] </returns>

        public static RectangularSectionDoublyReinforced ByWidthHeigthAndReinforcementArea(double b, double h, double A_sTop, double A_sBot, 
         double c_cntr, ConcreteMaterial ConcreteMaterial, RebarMaterial LongitudinalRebarMaterial, bool hasTies = true)
        {
            return new RectangularSectionDoublyReinforced(b, h, A_sTop, A_sBot, c_cntr, ConcreteMaterial, LongitudinalRebarMaterial);
        }


    }
}


