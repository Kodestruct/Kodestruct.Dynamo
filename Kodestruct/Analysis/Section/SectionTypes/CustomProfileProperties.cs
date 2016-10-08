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
using Kodestruct.Common.Section.Interfaces;

#endregion

namespace Analysis.Section
{

    /// <summary>
    ///     Shape properties about X axis
    ///     Category:    Analysis.Section
    /// </summary>
    /// 


    public partial class CustomProfileProperties
    {
        /// <summary>
        ///    Calculates custom shape properties about X axis
        /// </summary>
        /// <param name="Shape">  Custom profile object</param>
        /// <returns name="x_e"> Horizontal distance from designated member edge to member elastic centroidal axis </returns>
        /// <returns name="x_p"> Horizontal distance from designated member edge  to member plastic neutral axis </returns>
        /// <returns name="I_x"> Moment of inertia about the principal x-axis </returns>
        /// <returns name="Z_x"> Plastic section modulus about the x-axis  </returns>
        /// <returns name="S_xBot"> Elastic section modulus taken about the x-axis, with respect to bottom fiber  </returns>
        /// <returns name="S_xTop"> Elastic section modulus taken about the x-axis, with respect to top fiber  </returns>
        /// <returns name="r_x"> Radius of gyration about the x-axis  </returns>
        /// <returns name="A"> Area  </returns>
        /// 

        [MultiReturn(new[] { "x_e", "x_p", "I_x", "Z_x", "S_xBot", "S_xTop", "r_x" , "A"})]
        public static Dictionary<string, object> XAxisProperties(CustomProfile Shape)
        {
            //Default values
            double x_e = 0;
            double x_p = 0;
            double I_x = 0;
            double Z_x = 0;
            double S_xBot = 0;
            double S_xTop = 0;
            double r_x = 0;
            double A = 0;


            x_e = Shape.Section.x_Bar;
            x_p = Shape.Section.x_pBar;
            I_x = Shape.Section.I_x;
            Z_x = Shape.Section.Z_x;
            S_xBot = Shape.Section.S_xBot;
            S_xTop = Shape.Section.S_xTop;
            r_x = Shape.Section.r_x;
            A = Shape.Section.A;

            return new Dictionary<string, object>
            {
            { "x_e", x_e }
            ,{ "x_p", x_p }
            ,{ "I_x", I_x }
            ,{ "Z_x", Z_x }
            ,{ "S_xBot", S_xBot }
            ,{ "S_xTop", S_xTop }
            ,{ "r_x", r_x }
            ,{ "A", A }
            };
            }


        /// <summary>
        ///    Calculates custom shape properties about Y axis
        /// </summary>
        /// <param name="Shape"> Custom profile object</param>
        /// <returns name="y_e"> Vertical distance from designated member edge to member elastic centroidal axis </returns>
        /// <returns name="y_p"> Vertical distance from designated member edge  to member plastic neutral axis </returns>
        /// <returns name="I_y"> Moment of inertia about the principal y-axis </returns>
        /// <returns name="Z_y"> Plastic section modulus about the y-axis  </returns>
        /// <returns name="S_yLeft"> Elastic section modulus taken about the y-axis, with respect to left fiber  </returns>
        /// <returns name="S_yRight"> Elastic section modulus taken about the y-axis, with respect to right fiber  </returns>
        /// <returns name="r_y"> Radius of gyration about the y-axis  </returns>
        /// <returns name="A"> Area  </returns>


        [MultiReturn(new[] { "y_e", "y_p", "I_y", "Z_y", "S_yLeft", "S_yRight", "r_y", "A" })]
        public static Dictionary<string, object> YAxisProperties(CustomProfile Shape)
        {
            //Default values
            double y_e = 0;
            double y_p = 0;
            double I_y = 0;
            double Z_y = 0;
            double S_yLeft = 0;
            double S_yRight = 0;
            double r_y = 0;
            double A = 0;


            y_e = Shape.Section.y_Bar;
            y_p = Shape.Section.y_pBar;
            I_y = Shape.Section.I_y;
            Z_y = Shape.Section.Z_y;
            S_yLeft = Shape.Section.S_yLeft;
            S_yRight = Shape.Section.S_yRight;
            r_y = Shape.Section.r_y;
            A = Shape.Section.A;

            return new Dictionary<string, object>
            {
            { "y_e", y_e }
            ,{ "y_p", y_p }
            ,{ "I_y", I_y }
            ,{ "Z_y", Z_y }
            ,{ "S_yLeft", S_yLeft }
            ,{ "S_yRight", S_yRight }
            ,{ "r_y", r_y }
            ,{ "A", A }
            };
        }



        /// <summary>
        ///    Calculates custom shape torsional propertiesabout Y axis
        /// </summary>
        /// <param name="Shape">  Custom profile object </param>
        /// <returns name="J"> Torsional constant  (Torsional moment of inertia) </returns>
        /// <returns name="C_w"> Warping constant </returns>

        [MultiReturn(new[] { "J", "C_w" })]
        public static Dictionary<string, object> TorsionalProperties(CustomProfile Shape)
        {
            //Default values
            double J = 0;
            double C_w = 0;



            J = Shape.Section.J;
            C_w = Shape.Section.C_w;


            return new Dictionary<string, object>
            {
            { "J", J }
            ,{ "C_w", C_w }

            };
        }

        [IsVisibleInDynamoLibrary(false)]
        internal CustomProfileProperties()
        {

        }
        [IsVisibleInDynamoLibrary(false)]
        public static CustomProfileProperties ByInputParameters()
        {
            return new CustomProfileProperties();
        }

    }
}


