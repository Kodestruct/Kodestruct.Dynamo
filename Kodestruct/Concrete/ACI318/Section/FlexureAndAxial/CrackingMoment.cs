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
using Kodestruct.Common.Section.Interfaces;
using Kodestruct.Concrete.ACI318_14;
using System;
using Concrete.ACI318.Section.SectionTypes;

#endregion

namespace Concrete.ACI318.Section
{

/// <summary>
///     Cracking moment
///     Category:   Concrete.ACI318.Section.FlexureAndAxialForce
/// </summary>
/// 



    public partial class FlexureAndAxialForce 
    {
        /// <summary>
        ///     Cracking moment
        /// </summary>
        /// <param name="ConcreteSection">  Reinforced concrete section </param>
        /// <param name="FlexuralCompressionFiberLocation">  Indicates whether the section in flexure has top or bottom in compression due to stresses from bending moment </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="M_cr">  Cracking moment   </returns>

        [MultiReturn(new[] { "M_cr" })]
        public static Dictionary<string, object> CrackingMoment(ConcreteFlexureAndAxiaSection ConcreteSection, 
            string FlexuralCompressionFiberLocation = "Top", string Code = "ACI318-14")
        {
            //Default values
            double M_cr = 0;



            //Calculation logic:

            FlexuralCompressionFiberPosition p;
            bool IsValidStringFiber = Enum.TryParse(FlexuralCompressionFiberLocation, true, out p);
            if (IsValidStringFiber == false)
            {
                throw new Exception("Flexural compression fiber location is not recognized. Check input.");
            }


            ConcreteSectionFlexure beam = ConcreteSection.FlexuralSection as ConcreteSectionFlexure;
            M_cr = beam.GetCrackingMoment(p)/1000.0; // Conversion from ACI318 psi units to ksi units


            return new Dictionary<string, object>
            {
                { "M_cr", M_cr }
 
            };
        }




    }
}


