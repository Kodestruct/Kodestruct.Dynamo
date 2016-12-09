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
using Kodestruct.Steel.AISC;

#endregion

namespace Steel.AISC.Connection.SpecialCase
{

/// <summary>
///     Extended single plate flexural buckling strength
///     Category:   Steel.AISC.Connection.SpecialCase
/// </summary>
/// 


    public partial class ExtendedSinglePlate 
    {
        /// <summary>
        ///    Calculates Flexural strength of extended single plate, using plate buckling equation for coped beams (kip - in unit system for all inputs and outputs)
        /// </summary>
        /// <param name="a_bolts">  Distance from support to first line of bolts </param>
        /// <param name="t_p">  Thickness of plate   </param>
        /// <param name="d_pl">  Depth of plate </param>
        /// <param name="F_y">  Specified minimum yield stress of plate </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="phiM_n"> Moment strength </returns>

        [MultiReturn(new[] { "phiM_n" })]
        public static Dictionary<string, object> ExtendedSinglePlateFlexuralBucklingStrength(double a_bolts, double t_p, double d_pl, double F_y, string Code = "AISC360-10")
        {
            //Default values
            double phiM_n = 0;


            //Calculation logic:
            double c = a_bolts;
            double b_f = t_p;
            double t_w = t_p;
            double t_f = 0;
            double d = d_pl;
            double d_cope = 0;

            BeamCopeFactory factory = new BeamCopeFactory();
            IBeamCope copedBeam = factory.GetCope(BeamCopeCase.CopedBothFlanges, d, b_f, t_f, t_w, d_cope, c, F_y, F_y);
            phiM_n = copedBeam.GetFlexuralStrength();

            return new Dictionary<string, object>
            {
                { "phiM_n", phiM_n }
 
            };
        }


    }
}


