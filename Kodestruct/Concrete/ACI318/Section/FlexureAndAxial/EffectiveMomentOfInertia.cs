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
///     Effective moment of inertia
///     Category:   Concrete.ACI318_14.Section.Flexure
/// </summary>
/// 


    [IsDesignScriptCompatible]
    public partial class Flexure 
    {
/// <summary>
///     Effective moment of inertia
/// </summary>
        /// <param name="I_cr">   Moment of inertia of cracked section transformed  to concrete  </param>
/// <param name="I_g">   Moment of inertia of gross concrete section about  centroidal axis, neglecting reinforcement  </param>
/// <param name="M_cr">   Cracking moment   </param>
/// <param name="M_a">   Maximum moment in member due to service loads  at stage deflection is calculated   </param>

        
        [MultiReturn(new[] {  })]
        public static Dictionary<string, object> EffectiveMomentOfInertia(double I_cr,double I_g,double M_cr,double M_a)
        {
            //Default values
            

            //Calculation logic:


            return new Dictionary<string, object>
            {
                 
            };
        }


        //internal Flexure (double I_cr,double I_g,double M_cr,double M_a)
        //{

        //}
        //[IsVisibleInDynamoLibrary(false)]
        //public static Flexure  ByInputParameters(double I_cr,double I_g,double M_cr,double M_a)
        //{
        //    return new Flexure(I_cr ,I_g ,M_cr ,M_a );
        //}

    }
}


