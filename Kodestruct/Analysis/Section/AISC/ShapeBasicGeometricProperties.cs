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
using Kodestruct.Common.CalculationLogger;
using Kodestruct.Common.Section.Predefined;

#endregion

namespace Analysis.Section.AISC
{

/// <summary>
///     AISC shape basic geometric properties
///     Category:   Steel.AISC.General
/// </summary>
/// 

    public partial class StandardShapeProperties 
    {
        /// <summary>
        ///    Calculates AISC shape geometric properties
        /// </summary>
        /// <param name="SteelShapeId">  Section name from steel shape database </param>
        /// <returns name="d"> Full nominal depth of the section    </returns>
        /// <returns name="b_f"> Width of flange  </returns>
        /// <returns name="t_f"> Thickness of flange   </returns>
        /// <returns name="t_w"> Thickness of web  </returns>
        /// <returns name="k"> Distance from outer face of flange to the web toe of fillet  </returns>
        /// <returns name="D"> Outside diameter of round HSS  </returns>
        /// <returns name="B"> Overall width of rectangular steel section along face transferring load or overall width of rectangular HSS member  </returns>
        /// <returns name="H"> Overall depth of square or rectangular HSS </returns>
        /// <returns name="t"> Thickness of element plate or element wall  </returns>
        /// <returns name="t_nom"> HSS and pipe nominal wall thickness </returns>
        /// <returns name="t_des"> HSS and pipe design wall thickness </returns>
        /// <returns name="b"> Width of flat for HSS or leg length for angle </returns>
        ///  <returns name="A"> Cross-sectional area </returns>

        [MultiReturn(new[] { "d", "b_f", "t_f", "t_w", "k", "D", "B", "H", "t", "t_nom", "t_des", "b", "A" })]
        public static Dictionary<string, object> ShapeBasicGeometricProperties(string SteelShapeId)
        {
            //Default values
            double d = 0;
            double b= 0;
            double b_f = 0;
            double t_f = 0;
            double t_w = 0;
            double k = 0;
            double D = 0;
            double B = 0;
            double H = 0;
            double t = 0;
            double t_nom = 0;
            double t_des = 0;
            double A = 0;

            //Calculation logic
            CalcLog cl = new CalcLog();
            AiscCatalogShape shape = new AiscCatalogShape(SteelShapeId, cl);
            d = shape.d;
            b_f = shape.bf;
            t_f = shape.tf;
            t_w = shape.tw;
            k = shape.kdes;
            D = shape.OD;
            b = shape.b;
            B = shape.B;
            H = shape.Ht;
            t = shape.t;
            t_nom = shape.tnom;
            t_des = shape.tdes;
            A = shape.A;

            return new Dictionary<string, object>
            {
                { "d", d }
                ,{ "b_f", b_f }
                ,{ "t_f", t_f }
                ,{ "t_w", t_w }
                ,{ "k", k }
                ,{ "D", D }
                ,{ "B", B }
                ,{ "H", H }
                ,{ "t", t }
                ,{ "t_nom", t_nom }
                ,{ "t_des", t_des }
                ,{ "b", b }
                 ,{ "A", A }
            };
        }


    }
}


