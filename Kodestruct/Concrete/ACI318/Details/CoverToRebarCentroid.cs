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
using Kodestruct.Concrete.ACI.Entities;
using System;
using Kodestruct.Concrete.ACI;

#endregion

namespace Concrete.ACI318.Details
{

/// <summary>
///     Cover to reinforcement centroid
///     Category:   Concrete.ACI318.Details
/// </summary>
/// 



    public partial class Cover 
    {
        /// <summary>
        ///     Cover to reinforcement centroid
        /// </summary>
        /// <param name="c_c">   Clear cover of reinforcement  </param>
        /// <param name="RebarSizeId">  Rebar designation (number) indicating the size of the bar </param>
        /// <param name="s_clear">  Clear spacing between bars (if 0 is specified, then bar diameter is used) </param>
        /// <param name="N_layers">  Number of bars </param>
        /// <param name="Setback">  Setback dimension (additional distance to minimum calculated cover to centroid) </param>
        /// <returns name="c_cntr"> Concrete cover to rebar centroid </returns>

        [MultiReturn(new[] { "c_cntr" })]
        public static Dictionary<string, object> CoverToRebarCentroid(double c_c,string RebarSizeId,double s_clear=0,double N_layers=1,double Setback=0)
        {
            //Default values
            double c_cntr = 0;


            //Calculation logic:
            double d_b = 0;

            RebarDesignation des;
            bool IsValidString = Enum.TryParse(RebarSizeId, true, out des);
            if (IsValidString == false)
            {
                throw new Exception("Rebar size is not recognized. Check input.");
            }
            RebarSection sec = new RebarSection(des);
            d_b = sec.Diameter;

            if (N_layers <=0)
            {
                throw new Exception("Number of layers cannot be equal or less than zero.");
            }

            double sclr = s_clear == 0 ? d_b : s_clear;

            c_cntr = c_c + (N_layers * d_b + (N_layers - 1) * sclr) / 2.0 + Setback;

            return new Dictionary<string, object>
            {
                { "c_cntr", c_cntr }
 
            };
        }



    }
}


