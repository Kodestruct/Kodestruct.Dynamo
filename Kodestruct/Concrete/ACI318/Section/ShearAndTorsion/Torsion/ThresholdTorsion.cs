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
using Kodestruct.Concrete.ACI;
using Kodestruct.Concrete.ACI318_14;
using System;
using Kodestruct.Common.Section.Interfaces;

#endregion

namespace Concrete.ACI318.Section.ShearAndTorsion.Torsion
{

/// <summary>
///     Treshold torsion
///     Category:   Concrete.ACI318.Section.ShearAndTorsion.Torsion
/// </summary>
/// 


    public partial class Torsion 
    {
        /// <summary>
        ///     Treshold torsion
        /// </summary>
        /// <param name="ConcreteSection">  Reinforced concrete section </param>
        /// <param name="N_u">   Factored axial force normal to cross section occurring simultaneously with vu or tu; to be taken as  positive for compression and negative for tension  </param>
        /// <param name="c_transv_ctr">Concrete cover to closed stirrups</param>
        /// <param name="IsPrestressed">  Indicates if member is prestressed </param>
        /// <param name="Code">Applicable version of code/standard</param>
        /// <returns name="T_th">  Threshold torsional moment   </returns>

        [MultiReturn(new[] { "T_th" })]
        public static Dictionary<string, object> ThresholdTorsion(ConcreteFlexureAndAxiaSection ConcreteSection, double N_u, double c_transv_ctr,
            bool IsPrestressed=false, string Code = "ACI318-14")
        {
            //Default values
            double T_th = 0;


            //Calculation logic:
            TorsionShapeFactory tss = new TorsionShapeFactory();
            ConcreteSectionFlexure cross_Section = ConcreteSection.FlexuralSection as ConcreteSectionFlexure;
            if (cross_Section != null)
            {
                
                if (cross_Section.Section.SliceableShape is ISectionRectangular)
                {
                    IConcreteTorsionalShape shape = tss.GetShape(cross_Section.Section.SliceableShape, ConcreteSection.ConcreteMaterial.Concrete, c_transv_ctr);
                    ConcreteSectionTorsion s = new ConcreteSectionTorsion(shape);
                    T_th = s.GetThreshholdTorsion(N_u)/1000.0; //Conversion from ACI psi units to ksi units
                }
                else
                {
                    throw new Exception("Only rectangular cross-sections are currently supported for torsional calculations");
                }
            }
            else
            {
                throw new Exception("Unrecognized shape type");
            }

            return new Dictionary<string, object>
            {
                { "T_th", T_th }
 
            };
        }


    }
}


