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
using Kodestruct.Analysis.Torsion;
using System;

#endregion

namespace Analysis.Beam
{

/// <summary>
///     Torsional function values
///     Category:   Analysis.Beam.Torsion
/// </summary>
/// 


    public partial class Torsion 
    {
            /// <summary>
            ///    Calculates Evaluation of torsional functions 
            /// </summary>
            /// <param name="TorsionalFunctionCaseId">  Case ID used in calculation of the values of torsional functions (per AISC design guide 9) </param>
            /// <param name="E">  Modulus of elasticity </param>
            /// <param name="G">  Shear modulus of elasticity </param>
            /// <param name="J">  Torsional constant for the cross-section </param>
            /// <param name="L">  member span length </param>
            /// <param name="z">  Distance from left support </param>
            /// <param name="T">  Concentrated torque </param>
            /// <param name="C_w">  Warping constant for the cross-section </param>
            /// <param name="t">  Uniformly distributed torque </param>
            /// <param name="alpha">  Fraction of total span at the point of concentrated torque </param>
            /// <returns name="theta"> Angle of rotation </returns>
            /// <returns name="theta_1der"> First derivative of angle of rotation with respect to z </returns>
            /// <returns name="theta_2der"> Second derivative of angle of rotation with respect to z </returns>
            /// <returns name="theta_3der"> Third derivative of angle of rotation with respect to z </returns>

        [MultiReturn(new[] { "theta","theta_1der","theta_2der","theta_3der" })]
        public static Dictionary<string, object> TorsionalFunctionValues(string TorsionalFunctionCaseId,double E, double G,double J,double L,double z,double T,double C_w,double t,double alpha)
        {
            //Default values
            double theta = 0;
            double theta_1der = 0;
            double theta_2der = 0;
            double theta_3der = 0;


            //Calculation logic:

            TorsionalFunctionCase TCase;
            bool IsValidStringTorsionCase = Enum.TryParse(TorsionalFunctionCaseId, true, out TCase);
            if (IsValidStringTorsionCase == false)
            {
                throw new Exception("Torsional case is not recognized. Check input string.");
            }

            TorsionalFunctionFactory tf = new TorsionalFunctionFactory();

            ITorsionalFunction function = tf.GetTorsionalFunction(TCase, E, G, J, L, z, T, C_w, t, alpha);
            theta = function.Get_theta();
            theta_1der = function.Get_theta_1();
            theta_2der = function.Get_theta_2();
            theta_3der = function.Get_theta_3();

            #region For debugging calculate the normalized values as in AISC design Guide

             double thetaNorm      ;
             double theta_1derNorm ;
             double theta_2derNorm ;
             double theta_3derNorm ;
                //FOR TESTING CALCULATE THE SECTION PROPS
            double a=Math.Sqrt((E*C_w)/(G*J));
            double la = L / a;
            
            #region Case 12
             thetaNorm     =  theta*(G*J/t)*1/(Math.Pow(a,2));
             theta_1derNorm=  theta_1der *(G*J/t)*2.0/(a);
             theta_2derNorm=  theta_2der*G*J/t;
             theta_3derNorm = theta_3der * G * J / t * a;


            #endregion
            #endregion

            return new Dictionary<string, object>
            {
                { "theta", theta }
                ,{ "theta_1der", theta_1der }
                ,{ "theta_2der", theta_2der }
                ,{ "theta_3der", theta_3der }
 
            };
        }


    }
}


