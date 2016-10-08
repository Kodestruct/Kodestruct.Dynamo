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
using Kodestruct.Steel.AISC.AISC360v10.J_Connections.AffectedMembers;
using Kodestruct.Steel.AISC;
using System;

#endregion

namespace Steel.AISC.Connection
{

/// <summary>
///     Bolt group shear and tension areas
///     Category:   Steel.AISC.Connection
/// </summary>
/// 



    public partial class AffectedElements 
    {
        /// <summary>
        ///    Calculates Net and gross shear and tension areas for block shear, shear yielding and shear rupture calculations
        /// </summary>
        /// <param name="ShearAreaCaseId">  Case selection for shear area calculations in affected elements in connections (block shear, shear yielding, shear rupture).Values are: StraightLine,TBlock,UBlock,Lblock </param>
        /// <param name="N_BoltRowParallel">  Number of bolt rows parallel  to direction of load (for example number of rows when load is vertical) </param>
        /// <param name="N_BoltRowPerpendicular">  Number of bolt columns perpendicular to direction of load (for example number of columns when the load is vertical) </param>
        /// <param name="p_parallel">  Bolt spacing in the direction of load </param>
        /// <param name="p_perpendicular">  Bolt spacing perpendicular to the direction of load </param>
        /// <param name="d_hole">  Bolt hole diameter </param>
        /// <param name="t_p">  Thickness of plate   </param>
        /// <param name="l_edgeParallel">  Edge distance measured parallel to direction of load (for example verical edge distance when the load is vertical) </param>
        /// <param name="l_edgePerpendicular">  Edge distance measured perpendicular to direction of load  (for example horizontal edge distance when the load is vertical)</param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="A_gv"> Gross area subject to shear </returns>
        /// <returns name="A_nv"> Net area subject to shear </returns>
        /// <returns name="A_nt"> Net area subject to tension </returns>

        [MultiReturn(new[] { "A_gv","A_nv","A_nt" })]
        public static Dictionary<string, object> BoltGroupShearAndTensionAreas(string ShearAreaCaseId,double N_BoltRowParallel,double N_BoltRowPerpendicular,
            double p_parallel,double p_perpendicular,double d_hole, double t_p,
            double l_edgeParallel, double l_edgePerpendicular, string Code = "AISC360-10")
        {
            //Default values
            double A_gv = 0;
            double A_nv = 0;
            double A_nt = 0;


            //Calculation logic:
            ShearAreaCase shearCase = ShearCaseParse(ShearAreaCaseId);
            ShearAreaCalculator c = new ShearAreaCalculator(shearCase, N_BoltRowParallel, N_BoltRowPerpendicular, 
                p_parallel, p_perpendicular, d_hole, t_p, l_edgeParallel, l_edgePerpendicular);
            A_gv = c.GetGrossAreaShear();
            A_nv = c.GetNetAreaShear();
            A_nt = c.GetNetAreaTension();

            return new Dictionary<string, object>
            {
                { "A_gv", A_gv }
                ,{ "A_nv", A_nv }
                ,{ "A_nt", A_nt }
 
            };
        }

        private static ShearAreaCase ShearCaseParse(string ShearAreaCaseId)
        {
            ShearAreaCase shearAreaCase;
            bool IsValidString = Enum.TryParse(ShearAreaCaseId, true, out shearAreaCase);
            if (IsValidString == true)
            {
                return shearAreaCase;
            }
            else
            {
                throw new Exception("Group shear and tension area calculation failed. Invalid group case designation.");
            }
        }



    }
}


