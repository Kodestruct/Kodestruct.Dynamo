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
using Analysis.Section;
using op = Kodestruct.Steel.AISC.AISC360v10.Connections.WebOpenings;
using Kodestruct.Steel.AISC.AISC360v10.Connections.WebOpenings;
using Kodestruct.Common.Section.Interfaces;
using System;
using Kodestruct.Steel.AISC;

#endregion

namespace Steel.AISC.Connection
{

/// <summary>
///     Composite I-Beam web opening shear strength
///     Category:   Steel.AISC
/// </summary>
/// 



    public partial class BeamWebOpening 
    {
        /// <summary>
        ///     Composite I-Beam web opening shear strength (kip - in unit system for all inputs and outputs)
        /// </summary>
        /// <param name="IShape">  Shape object  </param>
        /// <param name="b_eff">  Effective width of concrete slab in composite beam design </param>
        /// <param name="h_solid">  Depth of solid portion of concrete slab on metal deck (fill above deck) </param>
        /// <param name="h_rib">  Depth of ribs for corrugated metal deck </param>
        /// <param name="F_y">  Specified minimum yield stress </param>
        /// <param name="fc_prime">  Specified compressive strength of concrete   </param>
        /// <param name="N_anchors">  Total number of studs (between point of zero and maximum moment) </param>
        /// <param name="Q_n">  Nominal strength of one steel headed stud or steel channel anchor  </param>
        /// <param name="N_o">  Number of anchors over opening </param>
        /// <param name="a_o">  Length of opening </param>
        /// <param name="h_op">  Height of opening </param>
        /// <param name="e_op">  Eccentriciy of opening with repect to neutral axis </param>
        /// <param name="t_r">  Thickness of reinforcing plate </param>
        /// <param name="b_r">  Length of a projection of reinforcing plate from web </param>
        /// <param name="HeadedAnchorDeckCondition"> Deck orientation relative to beam direction</param>
        /// <param name="w_rMin">Minimum rib width for deck</param>
        /// <param name="s_r"> Deck rib spacing</param>
        /// <param name="IsSingleSideReinforcement">  Identifies whether web reinforcing plate is placed on one side of web </param>
        /// <param name="M_u">Design moment at opening center (required only if single-side reinforcement is specified, otherwise can be zero</param>
        /// <param name="V_u">Design shear (required only if single-side reinforcement is specified, otherwise can be zero</param>
        /// <returns name="phiV_n"> Shear strength </returns>

        [MultiReturn(new[] { "phiV_n" })]
        public static Dictionary<string, object> CompositeIBeamWebOpeningShearStrength(CustomProfile IShape,double b_eff,double h_solid,double h_rib,
            double F_y, double fc_prime, double N_anchors, double Q_n, double N_o, double a_o, double h_op, double e_op, double t_r, double b_r, 
            string HeadedAnchorDeckCondition ="Perpendicular", double w_rMin=4.0, double s_r=12.0, bool IsSingleSideReinforcement = false,
            double V_u=0, double M_u=0)
        {
            //Default values
            double phiV_n = 0;


            //Calculation logic:
            ISectionI sectionI = IShape.Section as ISectionI;
            if (sectionI == null)
            {
                throw new Exception("Specified shape type is not supported. Provide I-shape object as inputparameter");
            }

            DeckAtBeamCondition deckCondition;
            bool IsValidHeadedAnchorDeckCondition = Enum.TryParse(HeadedAnchorDeckCondition, true, out deckCondition);
            if (IsValidHeadedAnchorDeckCondition == false)
            {

                throw new Exception("Headed anchor position and group factor calculation failed. Invalid string provided for HeadedAnchorDeckCondition.");
            }

            CompositeIBeamWebOpening op = new CompositeIBeamWebOpening(sectionI, b_eff, h_solid, h_rib, F_y, fc_prime, N_anchors, Q_n, N_o, a_o, h_op, e_op, 
                t_r, b_r, deckCondition,w_rMin,s_r, IsSingleSideReinforcement);
            phiV_n = op.GetShearStrength();

            return new Dictionary<string, object>
            {
                { "phiV_n", phiV_n }
 
            };
        }



    }
}


