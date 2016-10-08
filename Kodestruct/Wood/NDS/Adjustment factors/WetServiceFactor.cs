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
using System;
using Kodestruct.Wood.NDS.NDS2015;
using Kodestruct.Wood.NDS.Entities;

#endregion

namespace Wood.NDS
{

/// <summary>
///     Wet service factor
///     Category:   Wood.NDS
/// </summary>
/// 



    public partial class AdjustmentFactor 
    {
        /// <summary>
        ///     Wet service factor
        /// </summary>
        /// <param name="ServiceMoistureCondition">  Identifies the type of service moisture conditions for wet service factor </param>
        /// <param name="F_ref">Reference value F_b or Fc</param>
        /// <param name="C_F">Size factor</param>
        /// <param name="WoodMemberType">  Distinguishes between dimensional lumber, timber,glulam etc. </param>
        /// <param name="Code">  Identifies the code or standard used for calculations </param>
        /// <returns name="C_M_Fb"> Wet service factor for adjusted bending value </returns>
        /// <returns name="C_M_Ft"> Wet service factor for adjusted tension value </returns>
        /// <returns name="C_M_Fv"> Wet service factor for adjusted shear value </returns>
        /// <returns name="C_M_Fc"> Wet service factor for adjusted compression value </returns>
        /// <returns name="C_M_E"> Wet service factor for modulus of elasticity E and minimum modulus of elasticity E_min </returns>

        [MultiReturn(new[] { "C_M_Fb","C_M_Ft","C_M_Fv","C_M_Fc","C_M_E" })]
        public static Dictionary<string, object> WetServiceFactor(string ServiceMoistureCondition, double F_ref, double C_F,
             string WoodMemberType = "SawnDimensionLumber", string Code = "NDS2015")
        {
            //Default values
            double C_M_Fb = 0;
            double C_M_Ft = 0;
            double C_M_Fv = 0;
            double C_M_Fc = 0;
            double C_M_E = 0;


            //Calculation logic:

            if (WoodMemberType.Contains("Sawn"))
            {
                       SawnLumberType sawnLumberType;

                        string memberType = WoodMemberType.TrimStart("Sawn".ToCharArray());
                        bool IsValidSawnLumberTypeString = Enum.TryParse(memberType, true, out sawnLumberType);
                        if (IsValidSawnLumberTypeString == false)
                        {
                            throw new Exception("Failed to convert string. Check string input for sawn lumber type. Please check input");
                        }


                    if ( WoodMemberType.Contains("Lumber"))
                    {
                        DimensionalLumber m = new DimensionalLumber();
                        C_M_Fb = m.GetWetServiceFactor(ReferenceDesignValueType.Bending,F_ref,C_F,sawnLumberType);
                        C_M_Ft = m.GetWetServiceFactor(ReferenceDesignValueType.TensionParallelToGrain, F_ref, C_F, sawnLumberType);
                        C_M_Fv = m.GetWetServiceFactor(ReferenceDesignValueType.ShearParallelToGrain, F_ref, C_F, sawnLumberType);
                        C_M_Fc = m.GetWetServiceFactor(ReferenceDesignValueType.CompresionParallelToGrain, F_ref, C_F, sawnLumberType);
                        C_M_E =  m.GetWetServiceFactor(ReferenceDesignValueType.ModulusOfElasticity, F_ref, C_F, sawnLumberType);
                

                

                    }
                    else if ( WoodMemberType.Contains("Timber"))
                    {
                        Timber t = new Timber();
                        C_M_Fb = t.GetWetServiceFactor(ReferenceDesignValueType.Bending,F_ref,C_F,sawnLumberType);
                        C_M_Ft = t.GetWetServiceFactor(ReferenceDesignValueType.TensionParallelToGrain, F_ref, C_F, sawnLumberType);
                        C_M_Fv = t.GetWetServiceFactor(ReferenceDesignValueType.ShearParallelToGrain, F_ref, C_F, sawnLumberType);
                        C_M_Fc = t.GetWetServiceFactor(ReferenceDesignValueType.CompresionParallelToGrain, F_ref, C_F, sawnLumberType);
                        C_M_E = t.GetWetServiceFactor(ReferenceDesignValueType.ModulusOfElasticity, F_ref, C_F, sawnLumberType);
                    }
                      else
                    {
                        throw new Exception("Wood member type not supported.");
                    }
            }
            else
            {
                throw new Exception("Wood member type not supported.");
            }

            return new Dictionary<string, object>
            {
                { "C_M_Fb", C_M_Fb }
                ,{ "C_M_Ft", C_M_Ft }
                ,{ "C_M_Fv", C_M_Fv }
                ,{ "C_M_Fc", C_M_Fc }
                ,{ "C_M_E", C_M_E }
 
            };
        }




    }
}


