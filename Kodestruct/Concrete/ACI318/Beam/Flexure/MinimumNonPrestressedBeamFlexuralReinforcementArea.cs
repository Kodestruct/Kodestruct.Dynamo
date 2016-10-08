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
///     Minimum non-prestressed beam flexural reinforcement area
///     Category:   Concrete.ACI318_14.Beam.Flexure
/// </summary>
/// 


    [IsDesignScriptCompatible]
    public partial class Flexure 
    {
/// <summary>
///     Minimum non-prestressed beam flexural reinforcement area
/// </summary>
        /// <param name="ConcreteSection">  Reinforced concrete section </param>
/// <param name="d">   Distance from extreme compression fiber to centroid  of longitudinal tension reinforcement  </param>
/// <param name="f_y">   Specified yield strength for nonprestressed reinforcement  </param>

        /// <returns name="A_s_min">  Minimum area of flexural reinforcement  </returns>

        [MultiReturn(new[] { "A_s_min" })]
        public static Dictionary<string, object> MinimumNonPrestressedBeamFlexuralReinforcementArea(ConcreteSection ConcreteSection,double d,double f_y)
        {
            //Default values
            double A_s_min = 0;


            //Calculation logic:


            return new Dictionary<string, object>
            {
                { "A_s_min", A_s_min }
 
            };
        }


        //internal Flexure (ConcreteSection ConcreteSection,double d,double f_y)
        //{

        //}
        //[IsVisibleInDynamoLibrary(false)]
        //public static Flexure  ByInputParameters(ConcreteSection ConcreteSection,double d,double f_y)
        //{
        //    return new Flexure(ConcreteSection ,d ,f_y );
        //}

    }
}


