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
using Kodestruct.Common.Section.Interfaces;

#endregion

namespace Concrete.ACI318.Section.ShearAndTorsion.Torsion
{

/// <summary>
///     Maximum torsional and shear strength interaction
///     Category:   Concrete.ACI318.Section.ShearAndTorsion.Torsion
/// </summary>
/// 


    public partial class Torsion 
    {

        /// <summary>
        ///     Maximum torsional and shear strength interaction
        /// </summary>
        /// <param name="ConcreteSection">  Reinforced concrete section </param>
        /// <param name="c_transv_ctr">  Concrete cover to closed stirrups </param>
        /// <param name="V_u">   Factored shear force at section  </param>
        /// <param name="T_u">   Factored torsional moment at section   </param>
        /// <param name="V_c">   Nominal shear strength provided by concrete  </param>
        /// <param name="b">   Width of compression face of member  </param>
        /// <param name="d">   Distance from extreme compression fiber to centroid  of longitudinal tension reinforcement  </param>
        /// <param name="IsPrestressed">  Indicates if member is prestressed </param>
        /// <param name="Code">  Applicable version of code/standard </param>
        /// <returns name="InteractionRatio"> Interaction ratio indicating demand to capacity for a given criterion. Values over 1.0 indicate failure to meet one ore more criteria </returns>

        [MultiReturn(new[] { "InteractionRatio" })]
        public static Dictionary<string, object> MaximumTorsionalAndShearStrengthInteraction(ConcreteFlexureAndAxiaSection ConcreteSection, double c_transv_ctr,
            double V_u,double T_u,double V_c,double b,double d,bool IsPrestressed=false,string Code = "ACI318-14")
        {
            //Default values
            double InteractionRatio = 0;


            //Calculation logic:

            TorsionShapeFactory tss = new TorsionShapeFactory();
            ConcreteSectionFlexure sec = (ConcreteSectionFlexure)ConcreteSection.FlexuralSection;
            IConcreteTorsionalShape shape = tss.GetShape(sec.Section.SliceableShape, ConcreteSection.ConcreteMaterial.Concrete, c_transv_ctr);
            ConcreteSectionTorsion s = new ConcreteSectionTorsion(shape);
            InteractionRatio = s.GetMaximumForceInteractionRatio(V_u, T_u, V_c, b, d);


            return new Dictionary<string, object>
            {
                { "InteractionRatio", InteractionRatio }
 
            };
        }



    }
}


