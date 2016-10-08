#region Copyright
   /*Copyright (C) 2015 Kodestruct Inc

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

namespace Concrete.ACI318_14.Section.Flexure
{

/// <summary>
///     Cracked moment of inertia
///     Category:   Concrete.ACI318_14.Section.Flexure
/// </summary>
/// 


    [IsDesignScriptCompatible]
    public partial class Flexure 
    {
/// <summary>
///     Cracked moment of inertia
/// </summary>
        /// <param name="ConcreteSection">  Reinforced concrete section </param>
/// <param name="FlexuralCompressionFiberLocation">  Indicates whether the section in flexure has top or bottom in compression due to stresses from bending moment </param>
/// <param name="c">   Distance from extreme compression fiber to neutral  axis  </param>

        /// <returns name="I_cr">  Moment of inertia of cracked section transformed  to concrete  </returns>

        [MultiReturn(new[] { "I_cr" })]
        public static Dictionary<string, object> CrackedMomentOfInertia(ConcreteSection ConcreteSection,string FlexuralCompressionFiberLocation,double c)
        {
            //Default values
            double I_cr = 0;


            //Calculation logic:


            return new Dictionary<string, object>
            {
                { "I_cr", I_cr }
 
            };
        }


        //internal Flexure (ConcreteSection ConcreteSection,string FlexuralCompressionFiberLocation,double c)
        //{

        //}
        //[IsVisibleInDynamoLibrary(false)]
        //public static Flexure  ByInputParameters(ConcreteSection ConcreteSection,string FlexuralCompressionFiberLocation,double c)
        //{
        //    return new Flexure(ConcreteSection ,FlexuralCompressionFiberLocation ,c );
        //}

    }
}


