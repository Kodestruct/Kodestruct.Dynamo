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
using Kodestruct.Steel.AISC.AISC360v10.Connections;

#endregion

namespace Steel.AISC.Connection
{

/// <summary>
///     Weld group elastic centroid
///     Category:   Steel.AISC.Connection
/// </summary>
/// 



    public partial class Welded 
    {
        /// <summary>
        ///     Weld group elastic centroid
        /// </summary>
        /// <param name="WeldGroupPattern">  Weld group pattern type </param>
        /// <param name="l_Weld_horizontal">  Weld group horizontal dimension  </param>
        /// <param name="l_Weld_vertical">  Weld group vertical dimension  </param>
        /// <returns name="CG_X_Left"> Center of gravity X-offset from left edge </returns>
        /// <returns name="CG_X_Right"> Center of gravity X-offset from right edge </returns>
        /// <returns name="CG_Y_Bottom"> Center of gravity Y-offset from bottom edge </returns>
        /// <returns name="CG_Y_Top"> Center of gravity Y-offset from top edge </returns>

        [MultiReturn(new[] { "CG_X_Left","CG_X_Right","CG_Y_Bottom","CG_Y_Top" })]
        public static Dictionary<string, object> WeldGroupElasticCentroid(string WeldGroupPattern,double l_Weld_horizontal,double l_Weld_vertical)
        {
            //Default values
            double CG_X_Left = 0;
            double CG_X_Right = 0;
            double CG_Y_Bottom = 0;
            double CG_Y_Top = 0;


            //Calculation logic:
            FilletWeldGroup wg = new FilletWeldGroup(WeldGroupPattern, l_Weld_horizontal, l_Weld_vertical, 1.0 / 16.0, 70.0);
 
             CG_X_Left   =wg.CG_X_Left   ;
             CG_X_Right  =wg.CG_X_Right  ;
             CG_Y_Bottom =wg.CG_Y_Bottom ;
             CG_Y_Top = wg.CG_Y_Top;

            return new Dictionary<string, object>
            {
                { "CG_X_Left", CG_X_Left }
                ,{ "CG_X_Right", CG_X_Right }
                ,{ "CG_Y_Bottom", CG_Y_Bottom }
                ,{ "CG_Y_Top", CG_Y_Top }
 
            };
        }




    }
}


