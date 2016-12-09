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
using Concrete.ACI318.General.Reinforcement;
using Kodestruct.Concrete.ACI;
using Kodestruct.Common.CalculationLogger;
using Kodestruct.Concrete.ACI318_14;
using System;

#endregion

namespace Concrete.ACI318.Details
{

/// <summary>
///     Tension lap splice length Detailed
///     Category:   Concrete.ACI318.Details
/// </summary>
/// 

    public partial class LapSplice
    {
        /// <summary>
        ///     Tension lap splice length (detailed) (kip - in unit system for all inputs and outputs)
        /// </summary>
        /// <param name="ConcreteMaterial">  Concrete material object used to extract material properties, create the object using input parameters first </param>
        /// <param name="d_b1">   Nominal diameter of first bar </param>
        /// <param name="d_b2">   Nominal diameter of second bar </param>
        /// <param name="RebarMaterial">   Reinforcement material </param>
        /// <param name="RebarSpliceClass">  Identifies if splice is class A or class B </param>
        /// <param name="RebarCoatingType">  Type of rebar surface coating (epoxy coated or black) </param>
        /// <param name="RebarCastingPosition">  Indicates if rebar is  a horizontal bar placed over 12 in. of concrete. </param>
        /// <param name="s_clear">  Clear spacing between bars </param>
        /// <param name="c_c">  Clear spacing between bars </param>
        /// <param name="A_tr">   Total cross-sectional area of all transverse reinforcement within spacing s that crosses the potential  plane of splitting through the reinforcement being  developed  </param>
        /// <param name="s_tr">  Transverse reinforcement spacing </param>
        /// <param name="n">   Number of items, such as, bars, wires, monostrand  anchorage devices, anchors, or shearhead arms </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="l_st">  Tension lap splice length  </returns>

        [MultiReturn(new[] { "l_st" })]
        public static Dictionary<string, object> StraightBarTensionLapSpliceLengthDetailed(Concrete.ACI318.General.Concrete.ConcreteMaterial ConcreteMaterial, double d_b1, double d_b2,
            RebarMaterial RebarMaterial, string RebarSpliceClass = "B", string RebarCoatingType = "Uncoated", string RebarCastingPosition = "Top", double s_clear=3, double c_c=1.5,
            double A_tr=0.44, double s_tr=12, double n=5, string Code = "ACI318-14")
        {
            //Default values
            double l_st = 0;


            //Calculation logic:
            IRebarMaterial mat = RebarMaterial.Material;

            bool IsEpoxyCoated = false;
            switch (RebarCoatingType)
            {
                case "Uncoated": IsEpoxyCoated = false; break;
                case "EpoxyCoated": IsEpoxyCoated = true; break;

                default: throw new Exception("Unrecognized rebar coating. Please check string input"); break;
            }

            Rebar rebar1 = new Rebar(d_b1, IsEpoxyCoated, mat);
            Rebar rebar2 = new Rebar(d_b2, IsEpoxyCoated, mat);

            bool IsTopRebar = false;
            switch (RebarCastingPosition)
            {
                case "Other": IsTopRebar = false; break;
                case "Top": IsTopRebar = true; break;

                default: throw new Exception("Unrecognized rebar casting position. Please check string input"); break;
            }

            
            TensionLapSpliceClass _RebarSpliceClass;
            bool IsValidRebarSpliceClass = Enum.TryParse(RebarSpliceClass, true, out _RebarSpliceClass);
            if (IsValidRebarSpliceClass == false)
            {
                throw new Exception("Failed to convert string. RebarSpliceClass not recognzed (A and B are acceptable inputs). Please check input");
            }


            CalcLog log = new CalcLog();

            TensionLapSplice ls = new TensionLapSplice(ConcreteMaterial.Concrete,rebar1,rebar2, s_clear, c_c, IsTopRebar,A_tr,s_tr,n, _RebarSpliceClass, log);
            l_st = ls.Length;

            return new Dictionary<string, object>
            {
                { "l_st", l_st }
 
            };
        }



    }
}


