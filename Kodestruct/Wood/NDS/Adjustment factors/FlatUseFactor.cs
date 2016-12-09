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
using System;
using Kodestruct.Wood.NDS.NDS2015;
using Kodestruct.Wood.NDS.Entities;

#endregion

namespace Wood.NDS
{

/// <summary>
///     Flat use factor
///     Category:   Wood.NDS
/// </summary>
/// 



    public partial class AdjustmentFactor 
    {
        /// <summary>
        ///     Flat use factor (kip - in unit system for all inputs and outputs)
        /// </summary>
        /// <param name="b"> Width (long dimension)</param>
        /// <param name="t"> Thickness </param>
        /// <param name="WoodCommercialGrade">  Identifies commercial grade of wood being considered </param>
        /// <param name="WoodMemberType">  Distinguishes between dimensional lumber, timber,glulam etc. </param>
        /// <param name="Code">  Identifies the code or standard used for calculations </param>
        /// <returns name="C_fu_Fb"> Flat use  factor for adjusted bending value </returns>
        /// <returns name="C_fu_Ft"> Flat use  factor for adjusted tension value </returns>
        /// <returns name="C_fu_Fv"> Flat use  factor for adjusted shear value </returns>
        /// <returns name="C_fu_Fc"> Flat use  factor for adjusted compression value </returns>
        /// <returns name="C_fu_E">  Flat use  factor for modulus of elasticity E and minimum modulus of elasticity E_min </returns>


        [MultiReturn(new[] 
        {
        "C_fu_Fb",
        "C_fu_Ft",
        "C_fu_Fv",
        "C_fu_Fc",
        "C_fu_E" 
        }
        
        
        )]
        public static Dictionary<string, object>
            
            
            FlatUseFactor(double b, double t, string WoodCommercialGrade,
             string WoodMemberType = "SawnDimensionLumber", string Code = "NDS2015")
        {
            //Default values
 
            double C_fu_Fb  =0.0;
            double C_fu_Ft  =0.0;
            double C_fu_Fv  =0.0;
            double C_fu_Fc  =0.0;
            double C_fu_E = 0.0;

            //Calculation logic:

            
            CommercialGrade comGr;
            bool IsValidComGrString = Enum.TryParse(WoodCommercialGrade, true, out comGr);
            if (IsValidComGrString == false)
            {
                throw new Exception("Failed to convert string. Invalid Id for wood commercial grade. Please check input");
            }




           if (WoodMemberType.Contains("Sawn") && WoodMemberType.Contains("Lumber"))
            {
                DimensionalLumber m = new DimensionalLumber();
                string memberType = WoodMemberType.TrimStart("Sawn".ToCharArray());

                SawnLumberType sawnLumberType;
                bool IsValidSawnLumberTypeString = Enum.TryParse(memberType, true, out sawnLumberType);
                if (IsValidSawnLumberTypeString == false)
                {
                    throw new Exception("Failed to convert string. Check string input for sawn lumber type. Please check input");
                }

                C_fu_Fb = m.GetFlatUseFactor(b, t, sawnLumberType, comGr, Kodestruct.Wood.NDS.Entities.ReferenceDesignValueType.Bending);
                C_fu_Ft = m.GetFlatUseFactor(b, t, sawnLumberType, comGr, Kodestruct.Wood.NDS.Entities.ReferenceDesignValueType.TensionParallelToGrain);
                C_fu_Fv = m.GetFlatUseFactor(b, t, sawnLumberType, comGr, Kodestruct.Wood.NDS.Entities.ReferenceDesignValueType.ShearParallelToGrain);
                C_fu_Fc = m.GetFlatUseFactor(b, t, sawnLumberType, comGr, Kodestruct.Wood.NDS.Entities.ReferenceDesignValueType.CompresionParallelToGrain);
                C_fu_E = m.GetFlatUseFactor(b, t, sawnLumberType, comGr, Kodestruct.Wood.NDS.Entities.ReferenceDesignValueType.ModulusOfElasticity);


            }
           else if (WoodMemberType.Contains("Sawn") && WoodMemberType.Contains("Timber"))
           {

               C_fu_Fb =1.0;
               C_fu_Ft =1.0;
               C_fu_Fv =1.0;
               C_fu_Fc =1.0;
               C_fu_E = 1.0;
           }
           else if (WoodMemberType.Contains("Sawn") && WoodMemberType.Contains("BeamOrStringer"))
           {
               throw new Exception("Wood member type not supported.");
           }
           else
           {
               throw new Exception("Wood member type not supported.");
           }

            return new Dictionary<string, object>
            {
                { "C_fu_Fb", C_fu_Fb },
                { "C_fu_Ft", C_fu_Ft },
                { "C_fu_Fv", C_fu_Fv },
                { "C_fu_Fc", C_fu_Fc },
                { "C_fu_E", C_fu_E	 },
            };
        }



    }
}


