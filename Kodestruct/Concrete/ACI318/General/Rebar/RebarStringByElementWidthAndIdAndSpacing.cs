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

#endregion

namespace Concrete.ACI318.General.Reinforcement
{

/// <summary>
///     Rebar string by element width and Id and spacing
///     Category:   Concrete.ACI318.General.Reinforcement
/// </summary>
/// 


    public partial class SizeAreaAndProperties 
    {
        /// <summary>
        ///     Rebar string by width and spacing
        /// </summary>
        /// <param name="RebarSizeId">  Rebar designation (number) indicating the size of the bar </param>
        /// <param name="s">   Center-to-center spacing of items, such as longitudinal reinforcement, transverse reinforcement,  tendons, or anchors  </param>
        /// <param name="N_faces">  Number of faces (layers) of reinforcement </param>
        /// <returns name="RebarText">  Text representing reinforcement pattern  </returns>

        [MultiReturn(new[] { "RebarText" })]
        public static Dictionary<string, object> RebarStringByElementWidthAndIdAndSpacing(string RebarSizeId,double s,double N_faces)
        {
            //Default values
            string RebarText;
            char atSymb=   '\u0040';
            string BarText = RebarSizeId.Substring(2);


            //Calculation logic:
            if (N_faces>1)
            {
                RebarText = "#" + BarText + "\u0040" + s + "\u0022" + "O.C.(" + N_faces + "FACES)";
            }
            else
            {
                RebarText = "#" + BarText + "\u0040" + s + "\u0022" + "OC";
            }

            return new Dictionary<string, object>
            {
                { "RebarText", RebarText }
 
            };
        }



    }
}


