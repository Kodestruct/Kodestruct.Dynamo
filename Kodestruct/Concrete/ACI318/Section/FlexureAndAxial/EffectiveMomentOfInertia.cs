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
using Dynamo.Graph.Nodes;
using Kodestruct.Concrete.ACI.Entities.FlexuralMember;

#endregion

namespace Concrete.ACI318.Section
{

/// <summary>
///     Effective moment of inertia
///     Category:   Concrete.ACI318.Section.FlexureAndAxialForce
/// </summary>
/// 


    public partial class FlexureAndAxialForce 
    {
        /// <summary>
        ///     Effective moment of inertia
        /// </summary>
        /// <param name="I_cr">   Moment of inertia of cracked section transformed  to concrete  </param>
        /// <param name="I_g">   Moment of inertia of gross concrete section about  centroidal axis, neglecting reinforcement  </param>
        /// <param name="M_cr">   Cracking moment   </param>
        /// <param name="M_a">   Maximum moment in member due to service loads  at stage deflection is calculated   </param>
        /// <returns name="I_e"> Effective moment of inertia </returns>

        

        public static double EffectiveMomentOfInertia(double I_cr,double I_g,double M_cr,double M_a)
        {
            //Default values
            double I_e = 0.0;

            //Calculation logic:
            EffectiveMomentOfInertiaCalculator emic = new EffectiveMomentOfInertiaCalculator();
            I_e = emic.GetI_e(I_g, I_cr, M_cr, M_a);

            return I_e;
        }



    }
}


