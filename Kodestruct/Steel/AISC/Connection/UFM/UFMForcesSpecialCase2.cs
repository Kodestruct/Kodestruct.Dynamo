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
using Kodestruct.Steel.AISC.UFM;

#endregion

namespace Steel.AISC.Connection
{

/// <summary>
///     UFM forces special case 2
///     Category:   Steel.AISC10.Connection
/// </summary>
/// 


    public partial class UniformForceMethod 
    {
        /// <summary>
        ///     Connection interface forces per Uniform Force Method for brace connection.  Case when beam shear reaction contribution is reduced by a force  DeltaV_b (kip - in unit system for all inputs and outputs)
        /// </summary>
        /// <param name="d_b">  Depth of beam </param>
        /// <param name="d_c">  Depth of column  </param>
        /// <param name="theta">  Angle of brace to column </param>
        /// <param name="alpha">  Distance from the face of the column flange or web to the ideal centroid of the gusset-to-beam connection </param>
        /// <param name="alpha_bar">  Distance from the face of the column flange or web to the actual centroid of the gusset-to-beam connection </param>
        /// <param name="beta">  Distance from the face of the beam flange to the ideal centroid of the gusset-to-column connection </param>
        /// <param name="beta_bar">  Distance from the face of the beam flange to the actual centroid of the gusset-to-column connection </param>
        /// <param name="P_u">  Required axial strength </param>
        /// <param name="DeltaV_b">  Reduction in beam shear force component of brace force (per Uniform Force Method special case 2) </param>
        /// <param name="R_beam">  Beam reaction (from gravity and other loads on beam outside of bracing connection). Positive down. </param>
        /// <param name="IncludeDistortionalMomentForces">  Identifies whether distortional moment is accounted for in calculation of braced connection forces </param>
        /// <param name="M_d">  Distortional moment at gusset connection </param>
        /// <param name="A_ub">  External force at braced connection </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="V_uc"> Required shear force on the gusset-to-column connection </returns>
        /// <returns name="V_ub"> Required axial force on the gusset-to-beam connection </returns>
        /// <returns name="M_ub"> Moment at gusset-to-beam interface </returns>
        /// <returns name="H_ub"> Required shear force on the gusset-to-beam connection </returns>
        /// <returns name="H_uc"> Required axial force on the gusset-to-column connection </returns>
        /// <returns name="V_ubc"> Required shear force on the beam-to-column connection </returns>
        /// <returns name="H_ubc"> Required axial force on the beam-to-column connection </returns>

        [MultiReturn(new[] { "V_uc","V_ub","M_ub","H_ub","H_uc", "V_ubc", "H_ubc" })]
        public static Dictionary<string, object> UFMForcesSpecialCase2(double d_b,double d_c,double theta,double alpha,double alpha_bar,
            double beta, double beta_bar, double P_u, double DeltaV_b, double R_beam, bool IncludeDistortionalMomentForces = false, double M_d = 0, double A_ub = 0, string Code = "AISC360-10")
        {
            //Default values
            double V_uc = 0;
            double V_ub = 0;
            double M_ub = 0;
            double H_ub = 0;
            double H_uc = 0;
            double V_ubc = 0;
            double H_ubc = 0;

            //Calculation logic:

            UFMCase2ReducedVerticalBraceShearForceBCConnection ufmCase = new UFMCase2ReducedVerticalBraceShearForceBCConnection
            (d_b, d_c, theta, alpha, beta, alpha_bar, beta_bar, DeltaV_b, P_u, R_beam, IncludeDistortionalMomentForces, M_d, A_ub);

             V_uc=ufmCase.V_uc;
             V_ub=ufmCase.V_ub;
             M_ub=ufmCase.M_ub;
             H_ub=ufmCase.H_ub;
             H_uc = ufmCase.H_uc;
            V_ubc = ufmCase.V_bc;
            H_ubc = ufmCase.H_bc;

            return new Dictionary<string, object>
            {
            { "V_uc",  V_uc }
            ,{ "V_ub", V_ub }
            ,{ "M_ub", M_ub }
            ,{ "H_ub", H_ub }
            ,{ "H_uc", H_uc }
                   ,{ "V_ubc", V_ubc }
                ,{ "H_ubc", H_ubc }
            };
        }


    }
}


