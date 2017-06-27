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
///     UFM forces moment at column gusset interface
///     Category:   Steel.AISC10.Connection
/// </summary>
/// 


    public partial class UniformForceMethod 
    {
        /// <summary>
        ///     Connection interface forces per Uniform Force Method for brace connection.  Case when moment is present at COLUMN-to-gusset interface (kip - in unit system for all inputs and outputs)
        /// </summary>
        /// <param name="d_b">  Depth of beam </param>
        /// <param name="d_c">  Depth of column  </param>
        /// <param name="theta">  Angle of brace to column </param>
        /// <param name="alpha">  Distance from the face of the column flange or web to the ideal centroid of the gusset-to-beam connection </param>
        /// <param name="beta">  Distance from the face of the beam flange to the ideal centroid of the gusset-to-column connection </param>
        /// <param name="beta_bar">  Distance from the face of the beam flange to the actual centroid of the gusset-to-column connection </param>
        /// <param name="P_u">  Required axial strength </param>
        /// <param name="R_beam">  Beam reaction (from gravity and other loads on beam outside of bracing connection). Positive down. </param>
        /// <param name="IncludeDistortionalMomentForces">  Identifies whether distortional moment is accounted for in calculation of braced connection forces </param>
        /// <param name="M_d">  Distortional moment at gusset connection </param>
        /// <param name="A_ub">  External force at braced connection </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="V_uc"> Required shear force on the gusset-to-column connection </returns>
        /// <returns name="H_uc"> Required axial force on the gusset-to-column connection </returns>
        /// <returns name="M_uc"> Moment at gusset-to-column interface </returns>
        /// <returns name="V_ub"> Required axial force on the gusset-to-beam connection </returns>
        /// <returns name="H_ub"> Required shear force on the gusset-to-beam connection </returns>

        [MultiReturn(new[] { "V_uc","H_uc","M_uc","V_ub","H_ub" })]
        public static Dictionary<string, object> UFMForcesMomentAtColumnGussetInterface(double d_b,double d_c,double theta,double alpha,double beta,
            double beta_bar, double P_u, double R_beam, bool IncludeDistortionalMomentForces = false, double M_d = 0, double A_ub = 0, string Code = "AISC360-10")
        {
            //Default values
            double V_uc = 0;
            double H_uc = 0;
            double M_uc = 0;
            double V_ub = 0;
            double H_ub = 0;


            //Calculation logic:
            UFMGeneralMomentAtColumnGussetInterface ufmCase = new UFMGeneralMomentAtColumnGussetInterface(d_b,d_c,theta,alpha,beta,beta_bar, P_u, R_beam, 
                IncludeDistortionalMomentForces, M_d, A_ub);

             V_uc = ufmCase.V_uc;
             H_uc = ufmCase.H_uc;
             M_uc = ufmCase.M_uc;
             V_ub = ufmCase.V_ub;
             H_ub = ufmCase.H_ub;

            return new Dictionary<string, object>
            {
                { "V_uc", V_uc }
                ,{ "H_uc", H_uc }
                ,{ "M_uc", M_uc }
                ,{ "V_ub", V_ub }
                ,{ "H_ub", H_ub }
            };
        }


    }
}


