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
using Kodestruct.Concrete.ACI318_14;
using Kodestruct.Concrete.ACI;
using System;
using Kodestruct.Common.CalculationLogger;

#endregion

namespace Concrete.ACI318.Details
{

/// <summary>
///     Tension development length Basic
///     Category:   Concrete.ACI318.Details
/// </summary>
/// 

    public partial class DevelopmentLength
    {
        /// <summary>
        ///     Tension development length (basic)
        /// </summary>
        /// <param name="ConcreteMaterial">  Concrete material object used to extract material properties, create the object using input parameters first </param>
        /// <param name="d_b">   Nominal diameter of bar, wire, or prestressing  strand  </param>
        /// <param name="RebarMaterial">   Reinforcement material </param>
        /// <param name="RebarCoatingType">  Type of rebar surface coating (epoxy coated or black) </param>
        /// <param name="RebarCastingPosition">  Indicates if rebar is  a horizontal bar placed over 12 in. of concrete. </param>
        /// <param name="ExcessRebarRatio">  Indicates the ration of areas of required reinforcement and provided renforcement. This value must be less than 1 </param>
        /// <param name="MeetsRebarSpacingAndEdgeDistance">  Identifies if clear spacing of bars being developed or lap spliced is at least 2d_b and clear cover at least d_b </param>
        /// <param name="HasMinimumTransverseReinforcement">  Identifies if member meets minimum code requirements for transverse reinforcement. Many current practical construction cases use spacing and cover values along with confining reinforcement, such as stirrups or ties,that result in a value of (cb + Ktr)/db of at least 1.5. See ACI 318-04 section 25.4.2.2  </param>
        /// <returns name="l_d">  Development length in tension of deformed bar,  deformed wire, plain and deformed welded wire  reinforcement, or pretensioned strand  </returns>

        [MultiReturn(new[] { "l_d" })]
        public static Dictionary<string, object> StraightBarTensionDevelopmentLengthBasic(Concrete.ACI318.General.Concrete.ConcreteMaterial ConcreteMaterial, double d_b, 
            Concrete.ACI318.General.Reinforcement.RebarMaterial RebarMaterial,
            string RebarCoatingType, string RebarCastingPosition, double ExcessRebarRatio, bool MeetsRebarSpacingAndEdgeDistance,
            bool HasMinimumTransverseReinforcement = false, string Code = "ACI318-14")
        {
            //Default values
            double l_d = 0;


            //Calculation logic:

            IRebarMaterial mat = RebarMaterial.Material;
            
            bool IsEpoxyCoated = false;
            switch (RebarCoatingType)
            {
                case "Uncoated"   :  IsEpoxyCoated= false; break;
                case "EpoxyCoated":  IsEpoxyCoated=true; break;

                default: throw new Exception("Unrecognized rebar coating. Please check string input"); break;
            }

            Rebar rebar = new Rebar(d_b, IsEpoxyCoated, mat);
            

            bool IsTopRebar = false;
            switch (RebarCastingPosition)
            {
                case "Other"   :  IsTopRebar= false; break;
                case "Top":  IsTopRebar=true; break;

                default: throw new Exception("Unrecognized rebar casting position. Please check string input"); break;
            }

            CalcLog log = new CalcLog();

            DevelopmentTension d = new DevelopmentTension(ConcreteMaterial.Concrete, rebar, MeetsRebarSpacingAndEdgeDistance, IsTopRebar, ExcessRebarRatio, true, log);
            l_d = d.GetTensionDevelopmentLength(HasMinimumTransverseReinforcement);

            return new Dictionary<string, object>
            {
                { "l_d", l_d }
 
            };
        }



    }


}


