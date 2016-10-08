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
///     Temperature factor
///     Category:   Wood.NDS
/// </summary>
/// 



    public partial class AdjustmentFactor 
    {
        /// <summary>
        ///     Temperature factor
        /// </summary>
        /// <param name="Temperature">  Service temperature </param>
        /// <param name="ServiceMoistureCondition">  Identifies the type of service moisture conditions for wet service factor </param>
        /// <param name="WoodMemberType">  Distinguishes between dimensional lumber, timber,glulam etc. </param>
        /// <param name="Code">  Identifies the code or standard used for calculations </param>
        /// <returns name="C_t_Fb"> Temperature factor for adjusted bending value </returns>
        /// <returns name="C_t_Ft"> Temperature factor for adjusted tension value </returns>
        /// <returns name="C_t_Fv"> Temperature factor for adjusted shear value </returns>
        /// <returns name="C_t_Fc"> Temperature factor for adjusted compression value </returns>
        /// <returns name="C_t_E">  Temperature factor for modulus of elasticity E and minimum modulus of elasticity E_min </returns>



        [MultiReturn(new[] { 
            
            "C_t_Fb",
            "C_t_Ft",
            "C_t_Fv",
            "C_t_Fc",
            "C_t_E"

        })]
        public static Dictionary<string, object> TemperatureFactor(double Temperature, string ServiceMoistureCondition="Dry",
             string WoodMemberType = "SawnDimensionLumber", string Code = "NDS2015")
        {
            //Default values
            double C_t_Fb =1.0;
            double C_t_Ft =1.0;
            double C_t_Fv =1.0;
            double C_t_Fc =1.0;
            double C_t_E = 1.0;

            //Calculation logic:

            
            ww.ServiceMoistureConditions moistureCondition;
            bool IsValidInputString = Enum.TryParse(ServiceMoistureCondition, true, out moistureCondition);
            if (IsValidInputString == false)
            {
                throw new Exception("Failed to convert string. Moisture condition string is invalid. Please check input");
            }


            if (WoodMemberType.Contains("Sawn") && WoodMemberType.Contains("Lumber"))
            {
                DimensionalLumber m = new DimensionalLumber();
                C_t_Fb = m.GetTemperatureFactorCt(ww.ReferenceDesignValueType.Bending, Temperature, moistureCondition);
                C_t_Ft = m.GetTemperatureFactorCt(ww.ReferenceDesignValueType.TensionParallelToGrain, Temperature, moistureCondition);
                C_t_Fv = m.GetTemperatureFactorCt(ww.ReferenceDesignValueType.ShearParallelToGrain, Temperature, moistureCondition);
                C_t_Fc = m.GetTemperatureFactorCt(ww.ReferenceDesignValueType.CompresionParallelToGrain, Temperature, moistureCondition);
                C_t_E = m.GetTemperatureFactorCt(ww.ReferenceDesignValueType.ModulusOfElasticity, Temperature, moistureCondition);
            }
            else if (WoodMemberType.Contains("Sawn") && WoodMemberType.Contains("Timber"))
            {
                Timber t = new Timber();
                C_t_Fb = t.GetTemperatureFactorCt(ww.ReferenceDesignValueType.Bending, Temperature, moistureCondition);
                C_t_Ft = t.GetTemperatureFactorCt(ww.ReferenceDesignValueType.TensionParallelToGrain, Temperature, moistureCondition);
                C_t_Fv = t.GetTemperatureFactorCt(ww.ReferenceDesignValueType.ShearParallelToGrain, Temperature, moistureCondition);
                C_t_Fc = t.GetTemperatureFactorCt(ww.ReferenceDesignValueType.CompresionParallelToGrain, Temperature, moistureCondition);
                C_t_E =  t.GetTemperatureFactorCt(ww.ReferenceDesignValueType.ModulusOfElasticity, Temperature, moistureCondition);
            }
            else
            {
                throw new Exception("Wood member type not supported.");
            }

            return new Dictionary<string, object>
            {
                { "C_t_Fb", C_t_Fb },
                { "C_t_Ft", C_t_Ft },
                { "C_t_Fv", C_t_Fv },
                { "C_t_Fc", C_t_Fc },
                { "C_t_E",  C_t_E  } 
            };
        }


    }
}


