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
using Kodestruct.Steel.AISC360v10.Connections.AffectedElements;
using Kodestruct.Common.Section.Predefined;
using Kodestruct.Common.Section;
using Kodestruct.Common.Section.Interfaces;
using Kodestruct.Common.Entities;
using Kodestruct.Steel.AISC.AISC360v10.AffectedMembers.Splices;

#endregion

namespace Steel.AISC.Connection
{

/// <summary>
///     Whitmore section width
///     Category:   Steel.AISC.Connection
/// </summary>
/// 


    public partial class AffectedElements 
    {
        /// <summary>
        ///    Calculates effective flange force due to biaxial bending and axial force. Use to check flange plate splices. Uses procedure outlined in A.R.Tamboli Handbook ofStructural Steel Connections (2nd Ed.)
        /// </summary>
        /// <param name="ShapeId">  Steel shape Id (use only W-shapes)  </param>
        /// <param name="F_y">  Yield stress (ksi) </param>
        /// <param  name="g"> Bolt gage </param>
        /// <param name="P"> Axial force</param>
        /// <param name="M_x"> Strong-axis bending moment (kip*in) </param>
        /// <param name="M_y"> Weak-axis bending moment (kip*in)</param>
        /// <param name="Code"> Code</param>
        /// <returns name="f_f"> Effective flange force (kip)</returns>

        [MultiReturn(new[] { "f_f" })]
        public static Dictionary<string, object> EffectiveFlangeForce(string ShapeId, double F_y, double g, double P, double M_x, double M_y, string Code = "AISC360-10")
        {
            //Default values
            double f_f = 0;
            AiscShapeFactory AiscShapeFactory = new AiscShapeFactory();
            ISection section = AiscShapeFactory.GetShape(ShapeId, ShapeTypeSteel.IShapeRolled);

            //Calculation logic:
            if (section is PredefinedSectionI)
            {
                ColumnFlangeSplice sp = new ColumnFlangeSplice(section as PredefinedSectionI, F_y, g);
                f_f = sp.GetEffectiveFlangeForce(P, M_x, M_y);
            }

            return new Dictionary<string, object>
            {
                { "f_f", f_f }
 
            };
        }


    }
}


