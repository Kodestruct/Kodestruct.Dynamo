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
using Kodestruct.Steel.AISC.AISC360v10;
using Kodestruct.Steel.AISC.AISC360v10.Tension;
using Kodestruct.Steel.AISC.SteelEntities.Bolts;
using b = Kodestruct.Steel.Tests.AISC.AISC360v10.Connections.Bolt;
using System;

#endregion

namespace Steel.AISC
{

/// <summary>
///     Effective net area 
///     Category:   Steel.AISC10
/// </summary>
/// 



    public partial class Tension 
    {
        /// <summary>
        ///     Net area for tensile strength
        /// </summary>
        /// <param name="A_g">Member gross area</param>
        /// <param name="NFlangeBolts">Number of flange holes in a section (or total number of holes for HSS, angles etc)</param>
        /// <param name="d_bFlange">Bolt diameter for flange bolts</param>
        /// <param name="BoltHoleTypeFlange">Bolt hole type for flange bolts</param>
        /// <param name="t_f">Flange thickness (or material thickness for HSS, angles etc)</param>
        /// <param name="NWebBolts">Number of web holes in a section</param>
        /// <param name="d_bWeb">Bolt diameter for web bolts</param>
        /// <param name="BoltHoleTypeWeb">Bolt hole type for web bolts</param>
        /// <param name="t_w">Web thickness</param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="A_n"> Net area </returns>



        public static Dictionary<string, object> ShapeNetArea(double A_g, double NFlangeBolts, double d_bFlange, 
            string BoltHoleTypeFlange, double t_f,
           double NWebBolts=0, double d_bWeb=0, string BoltHoleTypeWeb="", double t_w=0, string Code = "AISC360-10")
        {
            //Default values
            double A_n = 0;
            double b_holeFlange=0, b_holeWeb=0;

            //Calculation logic:
            BoltHoleType holeTypeFlange;
            bool IsValidStringFlange = Enum.TryParse(BoltHoleTypeFlange, true, out holeTypeFlange);
            if (IsValidStringFlange == true)
            {
                b.BoltGeneral b_Flange = new b.BoltGeneral(d_bFlange, 0, 0);
                b_holeFlange = b_Flange.GetBoltHoleLength(holeTypeFlange, true);
            }
            else
            {
                throw new Exception("Bolt hole calculation failed. Invalid hole type designation for flange bolt.");
            }

            if (BoltHoleTypeWeb!="")
            {
                BoltHoleType holeTypeWeb;
                bool IsValidStringWeb = Enum.TryParse(BoltHoleTypeWeb, true, out holeTypeWeb);
                if (IsValidStringWeb == true)
                {
                    b.BoltGeneral b_Web = new b.BoltGeneral(d_bWeb, 0, 0);
                    b_holeWeb = b_Web.GetBoltHoleLength(holeTypeWeb, true);
                }
                else
                {
                    throw new Exception("Bolt hole calculation failed. Invalid hole type designation for web bolt.");
                } 
            }

            A_n = A_g - (NFlangeBolts * t_f * b_holeFlange) - (NWebBolts * t_w * b_holeWeb);

            return new Dictionary<string, object>
            {
                { "A_n", A_n }
 
            };
        }




    }
}


