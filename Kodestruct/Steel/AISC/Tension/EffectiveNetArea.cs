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
using Kodestruct.Steel.AISC.AISC360v10.Tension;

#endregion

namespace Steel.AISC
{

/// <summary>
///     Effective net area 
///     Category:   Steel.AISC10
/// </summary>
/// 



    public partial class Tension 
    {
        /// <summary>
        ///     Effective area for tensile strength (kip - in unit system for all inputs and outputs)
        /// </summary>
        /// <param name="U">  Shear lag factor  </param>
        /// <param name="A_n">  Net area of member   </param>
        /// <param name="A_g">  Gross cross-sectional area of member (used is parameter IsBoltedSplice is set to true)  </param>
        /// <param name="A_connected">  Area of directly connected elements (to be used for Case 3 from AISC Table D3.1) when variable IsPartiallyWeldedWithTransverseWelds is set to true </param>
        /// <param name="IsPartiallyWeldedWithTransverseWelds"> Identifies whether this is a tension members where the tension load is transmitted only by transverse welds to some but not all of the cross-sectional elements </param>
        /// <param name="IsBoltedSplice">  Identifies whether member is spliced using bolted plates </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="A_e"> Effective net area </returns>


        
        public static Dictionary<string, object> EffectiveNetArea(double U,double A_n,
            double A_g,
            double A_connected=0,
            bool IsPartiallyWeldedWithTransverseWelds =false,
            bool IsBoltedSplice = false, string Code = "AISC360-10")
        {
            //Default values
            double A_e = 0;


            //Calculation logic:

            TensionMember tm = new TensionMember();
            A_e = tm.GetEffectiveNetArea(A_n, U, A_g, A_connected, IsPartiallyWeldedWithTransverseWelds, IsBoltedSplice);

            return new Dictionary<string, object>
            {
                { "A_e", A_e }
 
            };
        }


        //internal Tension (string ShearLagCaseId,double U,double A_n,double A_g,double A_connected,bool IsPartiallyWeldedWithTransverseWelds,bool IsBoltedSplice)
        //{

        //}
        //[IsVisibleInDynamoLibrary(false)]
        //public static Tension  ByInputParameters(string ShearLagCaseId,double U,double A_n,double A_g,double A_connected,bool IsPartiallyWeldedWithTransverseWelds,bool IsBoltedSplice)
        //{
        //    return new Tension(ShearLagCaseId ,U ,A_n ,A_g ,A_connected ,IsPartiallyWeldedWithTransverseWelds ,IsBoltedSplice );
        //}

    }
}


