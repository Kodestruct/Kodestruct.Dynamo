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
///     AISC shape properties about X axis
///     Category:   Steel.AISC.General
/// </summary>
/// 


    public partial class StandardShapeProperties 
    {
    /// <summary>
    ///    Calculates AISC shape properties about X axis
    /// </summary>
    /// <param name="SteelShapeId">  Section name from steel shape database </param>
    /// <returns name="x_e"> Horizontal distance from designated member edge to member elastic centroidal axis </returns>
    /// <returns name="x_p"> Horizontal distance from designated member edge  to member plastic neutral axis </returns>
    /// <returns name="I_x"> Moment of inertia about the principal x-axis </returns>
    /// <returns name="Z_x"> Plastic section modulus about the x-axis  </returns>
    /// <returns name="S_x"> Elastic section modulus taken about the x-axis  </returns>
    /// <returns name="r_x"> Radius of gyration about the x-axis  </returns>


        [MultiReturn(new[] { "x_e","x_p","I_x","Z_x","S_x","r_x" })]
        public static Dictionary<string, object> XAxisProperties(string SteelShapeId)
        {
            //Default values
            double x_e = 0;
double x_p = 0;
double I_x = 0;
double Z_x = 0;
double S_x = 0;
double r_x = 0;


            //Calculation logic:

CalcLog cl = new CalcLog();
AiscCatalogShape shape = new AiscCatalogShape(SteelShapeId, cl);

x_e = shape.x;
x_p = shape.xp;
I_x = shape.Ix;
Z_x = shape.Zx;
S_x = shape.Sx;
r_x = shape.rx;

            return new Dictionary<string, object>
            {
                { "x_e", x_e }
,{ "x_p", x_p }
,{ "I_x", I_x }
,{ "Z_x", Z_x }
,{ "S_x", S_x }
,{ "r_x", r_x }
 
            };
        }


        //internal CatalogShapeProperties (string SteelShapeId)
        //{

        //}
        //[IsVisibleInDynamoLibrary(false)]
        //public static CatalogShapeProperties  ByInputParameters(string SteelShapeId)
        //{
        //    return new CatalogShapeProperties(SteelShapeId );
        //}

    }
}


