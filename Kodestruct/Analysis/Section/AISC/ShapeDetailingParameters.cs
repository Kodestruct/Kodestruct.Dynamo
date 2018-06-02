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
using Kodestruct.Common.CalculationLogger;
using Kodestruct.Common.Section.Predefined;

#endregion

namespace Analysis.Section.AISC
{

/// <summary>
///     AISC shape detailing parameters
///     Category:   Steel.AISC.General
/// </summary>
/// 

    public partial class StandardShapeProperties 
    {
        /// <summary>
        ///    Calculates AISC shape geometric properties
        /// </summary>
        /// <param name="SteelShapeId">  Section name from steel shape database </param>
        /// <returns name="WG"> The workable gage for the inner fastener holes in the flange that provides for entering and tightening clearances and edge distance and spacing requirements.</returns>
        /// <returns name="T"> Distance between web toes of fillets at top and bottom of web, in. </returns>
        /// <returns name="k_det"> Design distance from outer face of flange to web toe of fillet, in. </returns>
 

        [MultiReturn(new[] { "WG", "T", "k_det" })]
        public static Dictionary<string, object> ShapeDetailingParameters(string SteelShapeId)
        {
            //Default values
            double WG = 0;
            double T= 0;
            double k_det = 0;


            //Calculation logic
            CalcLog cl = new CalcLog();
            SteelShapeId = SteelShapeId.ToUpper();
            AiscCatalogShape shape = new AiscCatalogShape(SteelShapeId, cl);
            WG = shape.WG;
            T = shape.T;
            k_det = shape.k_det;

            return new Dictionary<string, object>
            {
                { "WG", WG }
                ,{ "T", T }
                ,{ "k_det", k_det }
            };
        }


    }
}


