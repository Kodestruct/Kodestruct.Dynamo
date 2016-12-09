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
using ww = Kodestruct.Wood.NDS.Entities;


#endregion

namespace Wood.NDS
{

/// <summary>
///     Incising factor
///     Category:   Wood.NDS
/// </summary>
/// 



    public partial class AdjustmentFactor 
    {
    /// <summary>
        ///     Incising factor (kip - in unit system for all inputs and outputs)
    /// </summary>
        /// <param name="IsIncised">Determines if the member is incised</param>
    /// <param name="WoodMemberType">  Distinguishes between dimensional lumber, timber,glulam etc. </param>
    /// <param name="Code">  Identifies the code or standard used for calculations </param>
    /// <returns name="C_i_Fb"> Incising factor for adjusted bending value </returns>
    /// <returns name="C_i_Ft"> Incising factor for adjusted tension value </returns>
    /// <returns name="C_i_Fv"> Incising factor for adjusted shear value </returns>
    /// <returns name="C_i_Fc"> Incising factor for adjusted compression value </returns>
    /// <returns name="C_i_E">  Incising factor for modulus of elasticity E and minimum modulus of elasticity E_min </returns>



        [MultiReturn(new[] 
            {"C_i_Fb",
             "C_i_Ft",
             "C_i_Fv",
             "C_i_Fc",
             "C_i_E"}
            )]
        public static Dictionary<string, object> IncisingFactor(bool IsIncised =false,
             string WoodMemberType = "SawnDimensionLumber", string Code = "NDS2015")
        {
            //Default values

            double C_i_Fb=1.0;
            double C_i_Ft=1.0;
            double C_i_Fv=1.0;
            double C_i_Fc=1.0;
            double C_i_E =1.0;

            //Calculation logic:


            if (IsIncised == true)
            {
                if (WoodMemberType.Contains("Sawn") && WoodMemberType.Contains("Lumber"))
                {
                    DimensionalLumber m = new DimensionalLumber();
                    C_i_Fb = m.GetInsizingFactor(ww.ReferenceDesignValueType.Bending);
                    C_i_Ft = m.GetInsizingFactor(ww.ReferenceDesignValueType.TensionParallelToGrain);
                    C_i_Fv = m.GetInsizingFactor(ww.ReferenceDesignValueType.ShearParallelToGrain);
                    C_i_Fc = m.GetInsizingFactor(ww.ReferenceDesignValueType.CompresionParallelToGrain);
                    C_i_E = m.GetInsizingFactor(ww.ReferenceDesignValueType.ModulusOfElasticity);
                }
                else if (WoodMemberType.Contains("Sawn") && WoodMemberType.Contains("Timber"))
                {
                    Timber t = new Timber();
                    C_i_Fb = t.GetInsizingFactor(ww.ReferenceDesignValueType.Bending);
                    C_i_Ft = t.GetInsizingFactor(ww.ReferenceDesignValueType.TensionParallelToGrain);
                    C_i_Fv = t.GetInsizingFactor(ww.ReferenceDesignValueType.ShearParallelToGrain);
                    C_i_Fc = t.GetInsizingFactor(ww.ReferenceDesignValueType.CompresionParallelToGrain);
                    C_i_E =  t.GetInsizingFactor(ww.ReferenceDesignValueType.ModulusOfElasticity);
                }
                else
                {
                    throw new Exception("Wood member type not supported.");
                } 
            }

            return new Dictionary<string, object>
            {
                { "C_i_Fb", C_i_Fb }
                ,{ "C_i_Ft", C_i_Ft }
                ,{ "C_i_Fv", C_i_Fv }
                ,{ "C_i_Fc", C_i_Fc }
                ,{ "C_i_E", C_i_E }
 
            };
        }



    }
}


