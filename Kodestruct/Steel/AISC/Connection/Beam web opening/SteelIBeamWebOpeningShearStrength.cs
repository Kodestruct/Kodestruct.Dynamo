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
using Analysis.Section;
using Kodestruct.Common.Section.Interfaces;
using Kodestruct.Steel.AISC.AISC360v10.Connections.WebOpenings;
using System;

#endregion

namespace Steel.AISC.Connection
{

/// <summary>
///     Steel I-Beam web opening shear strength
///     Category:   Steel.AISC
/// </summary>
/// 


    public partial class BeamWebOpening 
    {
    /// <summary>
    ///     Steel I-Beam web opening shear strength
    /// </summary>
    /// <param name="IShape">  Shape object  </param>
    /// <param name="F_y">  Specified minimum yield stress </param>
    /// <param name="a_o">  Length of opening </param>
    /// <param name="h_op">  Height of opening </param>
    /// <param name="e_op">  Eccentriciy of opening with repect to neutral axis </param>
    /// <param name="t_r">  Thickness of reinforcing plate </param>
    /// <param name="b_r">  Length of a projection of reinforcing plate from web </param>
    /// <param name="IsSingleSideReinforcement">  Identifies whether web reinforcing plate is placed on one side of web </param>
    /// <returns name="phiV_n"> Shear strength </returns>

        [MultiReturn(new[] { "phiV_n" })]
        public static Dictionary<string, object> SteelIBeamWebOpeningShearStrength(CustomProfile IShape,double F_y,double a_o,double h_op,
            double e_op,double t_r,double b_r,bool IsSingleSideReinforcement)
        {
            //Default values
            double phiV_n = 0;


            //Calculation logic:

            ISectionI sectionI = IShape.Section as ISectionI;
            if (sectionI == null)
            {
                throw new Exception("Specified shape type is not supported. Provide I-shape object as inputparameter");
            }
            SteelIBeamWebOpening op = new SteelIBeamWebOpening(sectionI, F_y, a_o, h_op, e_op, t_r, b_r, IsSingleSideReinforcement);
            phiV_n = op.GetShearStrength();

            return new Dictionary<string, object>
            {
                { "phiV_n", phiV_n }
 
            };
        }



    }
}


