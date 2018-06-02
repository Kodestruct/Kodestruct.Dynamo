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
using Kodestruct.Steel.AISC.AISC360v10;
using System;

#endregion

namespace Steel.AISC
{

/// <summary>
///     Shear lag factor
///     Category:   Steel.AISC10
/// </summary>
/// 



    public partial class Tension 
    {
        /// <summary>
        ///     Shear lag factor (kip - in unit system for all inputs and outputs)
        /// </summary>
        /// <param name="ShearLagCaseId">  Defines the type of tension element for shear lag calculation </param>
        /// <param name="x_bar">Element eccentricity</param>
        /// <param name="t_p"> Plate width,  lapped plate transverse width, flange b_f</param>
        /// <param name="l">  Length of connection or weld   </param>
        /// <param name="B">  Overall width of rectangular steel section along face transferring load or overall width of rectangular HSS member  </param>
        /// <param name="H">  Overall height of rectangular HSS member (for HSS connections H is measured in the plane of the connection) or "d" for Case 7 </param>
        ///  <param name="A_g">  Gross cross-sectional area of member (used is parameter IsBoltedSplice is set to true)  </param>
        /// <param name="A_connected">  Area of directly connected elements (to be used for Case 3 from AISC Table D3.1) when variable IsPartiallyWeldedWithTransverseWelds is set to true </param>
        /// <param name="IsOpenTensionSection">Indicates if a section is open (such as I-shape, Tee etc)</param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="U"> Shear lag factor  </returns>


        [MultiReturn(new[] { "U" })]
        public static Dictionary<string, object> ShearLagFactor(string ShearLagCaseId,double x_bar=0,double t_p=0, double l=0,double B=0,double H=0,
            double A_g=0, double A_connected=0,
            bool IsOpenTensionSection = true, string Code = "AISC360-10")
        {
            //Default values
            double U = 0;


            //Calculation logic:
            ShearLagCase ShearLagCase;
            bool ValidCase = Enum.TryParse(ShearLagCaseId, out ShearLagCase);
            if (ValidCase == false)
            {
                throw new Exception("Shear lag case not recognized. Check input string.");
            }

            ShearLagFactor factor = new ShearLagFactor();
            U = factor.GetShearLagFactor(ShearLagCase, x_bar, t_p, l, B, H,A_g,A_connected,IsOpenTensionSection);


            return new Dictionary<string, object>
            {
                { "U", U }
 
            };
        }


        //internal Tension (string ShearLagCaseId,double l,double B,double H)
        //{

        //}
        //[IsVisibleInDynamoLibrary(false)]
        //public static Tension  ByInputParameters(string ShearLagCaseId,double l,double B,double H)
        //{
        //    return new Tension(ShearLagCaseId ,l ,B ,H );
        //}

    }
}


