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
using Kodestruct.Concrete.ACI.Entities;
using System;
using Kodestruct.Concrete.ACI;


#endregion

namespace Concrete.ACI318.General.Reinforcement
{

/// <summary>
///     Rebar area by element width and Id and spacing
///     Category:   Concrete.ACI318_14.General
/// </summary>
/// 


    public partial class SizeAreaAndProperties 
    {
        /// <summary>
        ///     Rebar area by width and spacing
        /// </summary>
        /// <param name="RebarSizeId">  Rebar designation (number) indicating the size of the bar </param>
        /// <param name="b_element">  Element width </param>
        /// <param name="s">   Center-to-center spacing of reinforcement  </param>
        /// <param name="N_faces">  Number of faces (layers) of reinforcement </param>
        /// <returns name="A_s">  Area of nonprestressed longitudinal tension reinforcement  </returns>

        [MultiReturn(new[] { "A_s" })]
        public static Dictionary<string, object> RebarAreaByElementWidthAndIdAndSpacing(string RebarSizeId,double b_element,double s,double N_faces)
        {
            //Default values
            double A_s = 0;


            //Calculation logic:

            RebarDesignation des;
            bool IsValidString = Enum.TryParse(RebarSizeId, true, out des);
            if (IsValidString == false)
            {
                throw new Exception("Rebar size is not recognized. Check input.");
            }
            RebarSection sec = new RebarSection(des);
            double A_b = sec.Area;
            A_s = b_element / s * N_faces * A_b;

            return new Dictionary<string, object>
            {
                { "A_s", A_s }
 
            };
        }


    }
}


