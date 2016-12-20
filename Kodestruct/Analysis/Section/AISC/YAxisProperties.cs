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
///     AISC shape properties about Y axis
///     Category:   Steel.AISC.General
/// </summary>
/// 


    public partial class StandardShapeProperties 
    {
        /// <summary>
        ///    Calculates AISC shape properties about Y axis
        /// </summary>
        /// <param name="SteelShapeId">  Section name from steel shape database </param>
        /// <returns name="y_e"> Vertical distance from designated member edge to member elastic centroidal axis </returns>
        /// <returns name="y_p"> Vertical distance from designated member edge to member plastic neutral axis </returns>
        /// <returns name="I_y"> Moment of inertia about the principal y-axis  </returns>
        /// <returns name="Z_y"> Plastic section modulus about the y-axis  </returns>
        /// <returns name="S_y"> Elastic section modulus taken about the y-axis. For a channel the minimum section modulus  </returns>
        /// <returns name="r_y"> Radius of gyration about y-axis  </returns>


        [MultiReturn(new[] { "y_e","y_p","I_y","Z_y","S_y","r_y" })]
        public static Dictionary<string, object> YAxisProperties(string SteelShapeId)
        {
            //Default values
            double y_e = 0;
            double y_p = 0;
            double I_y = 0;
            double Z_y = 0;
            double S_y = 0;
            double r_y = 0;


                        //Calculation logic:
            CalcLog cl = new CalcLog();
            SteelShapeId = SteelShapeId.ToUpper();
            AiscCatalogShape shape = new AiscCatalogShape(SteelShapeId, cl);

            y_e = shape.y;
            y_p = shape.yp;
            I_y = shape.Iy;
            Z_y = shape.Zy;
            S_y = shape.Sy;
            r_y = shape.ry;


            return new Dictionary<string, object>
            {
                { "y_e", y_e }
,{ "y_p", y_p }
,{ "I_y", I_y }
,{ "Z_y", Z_y }
,{ "S_y", S_y }
,{ "r_y", r_y }
 
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


