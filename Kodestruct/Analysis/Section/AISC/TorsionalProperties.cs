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
///     AISC shape torsional properties
///     Category:   Steel.AISC.General
/// </summary>
/// 


    public partial class StandardShapeProperties 
    {
        /// <summary>
        ///    Calculates AISC shape torsional properties
        /// </summary>
        /// <param name="SteelShapeId">  Section name from steel shape database </param>
        /// <returns name="J"> Torsional constant  (Torsional moment of inertia) </returns>
        /// <returns name="C"> HSS torsional constant </returns>
        /// <returns name="C_w"> Warping constant </returns>
        /// <returns name="W_no"> Normalized warping function </returns>
        /// <returns name="S_w1"> Warping statical moment at point 1 on cross section </returns>
        /// <returns name="S_w2"> Warping statical moment at point 2 on cross section </returns>
        /// <returns name="S_w3"> Warping statical moment at point 3 on cross section </returns>
        /// <returns name="Q_fl"> Statical moment for a point in the flange directly above the vertical edge of the web </returns>
        /// <returns name="Q_w"> Statical moment for a point at mid-depth of the cross section </returns>
        /// 

        [MultiReturn(new[] { "J","C","C_w","W_no","S_w1","S_w2","S_w3","Q_fl","Q_w" })]
        public static Dictionary<string, object> TorsionalProperties(string SteelShapeId)
        {
            //Default values
                double J = 0;
                double C = 0;
                double C_w = 0;
                double W_no = 0;
                double S_w1 = 0;
                double S_w2 = 0;
                double S_w3 = 0;
                double Q_fl = 0;
                double Q_w = 0;


            //Calculation logic:
                CalcLog cl = new CalcLog();
                SteelShapeId = SteelShapeId.ToUpper();
                AiscCatalogShape shape = new AiscCatalogShape(SteelShapeId, cl);
                
                J = shape.J;
                C = shape.C;
                C_w = shape.Cw;
                C_w  =shape.Cw; 
                W_no =shape.Wno;
                S_w1 =shape.Sw1;
                S_w2 =shape.Sw2;
                S_w3 =shape.Sw3;
                Q_fl =shape.Qf;
                Q_w = shape.Qw; 

            return new Dictionary<string, object>
            {
                { "J", J }
                ,{ "C", C }
                ,{ "C_w", C_w }
                ,{ "W_no", W_no }
                ,{ "S_w1", S_w1 }
                ,{ "S_w2", S_w2 }
                ,{ "S_w3", S_w3 }
                ,{ "Q_fl", Q_fl }
                ,{ "Q_w", Q_w }
 
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


