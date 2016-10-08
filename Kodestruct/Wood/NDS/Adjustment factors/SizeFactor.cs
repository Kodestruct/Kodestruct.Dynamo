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
using ww=Kodestruct.Wood.NDS.Entities;

#endregion

namespace Wood.NDS
{

/// <summary>
///     Size factor
///     Category:   Wood.NDS
/// </summary>
/// 



    public partial class AdjustmentFactor 
    {
        /// <summary>
        ///     Size factor
        /// </summary>
        /// <param name="d">  Depth of rectangular beam cross section  </param>
        /// <param name="t">  Thickness  </param>
        /// <param name="WoodCommercialGrade">Identifies commercial grade of wood being considered</param>
        /// <param name="LoadAppliedToNarrowFace">  Identifies if load is applied to narrow face </param>
        /// <param name="WoodMemberType">  Distinguishes between dimensional lumber, timber,glulam etc. </param>
        /// <param name="Code">  Identifies the code or standard used for calculations </param>
        /// <returns name="C_F_Fb"> Size factor for adjusted bending value </returns>
        /// <returns name="C_F_Fc"> Size factor for adjusted compression value </returns>
        /// <returns name="C_F_Ft"> Size factor for adjusted tension value </returns>

        [MultiReturn(new[] { "C_F_Fb", "C_F_Fc", "C_F_Ft" })]
        public static Dictionary<string, object> SizeFactor(double d,double t, string WoodCommercialGrade, bool LoadAppliedToNarrowFace=false,
           string WoodMemberType = "SawnDimensionLumber", string Code = "NDS2015")
        {
            //Default values

            double C_F_Fb = 1.0;
            double C_F_Fc = 1.0;
            double C_F_Ft = 1.0;

            //Calculation logic:



            if (WoodMemberType.Contains("Sawn") && WoodMemberType.Contains("Lumber"))
            {
                DimensionalLumber m = new DimensionalLumber();

                string memberType = WoodMemberType.TrimStart("Sawn".ToCharArray());
                
                SawnLumberType sawnLumberType;
                bool IsValidSawnLumberTypeString = Enum.TryParse(memberType, true, out sawnLumberType);
                if (IsValidSawnLumberTypeString == false)
                {
                    throw new Exception("Failed to convert string. Sawn lumber type is incorrectly specified. Please check input");
                }

                
                ww.CommercialGrade _commercialGrade;
                bool IsValidCommercialGradeString = Enum.TryParse(WoodCommercialGrade, true, out _commercialGrade);
                if (IsValidCommercialGradeString == false)
                {
                    throw new Exception("Failed to convert string. Wood commercial grade is incorrectly specified. Please check input");
                }


                C_F_Fb = m.GetSizeFactor(d, t, sawnLumberType, _commercialGrade, ww.ReferenceDesignValueType.Bending, LoadAppliedToNarrowFace);
                C_F_Ft = m.GetSizeFactor(d, t, sawnLumberType, _commercialGrade, ww.ReferenceDesignValueType.TensionParallelToGrain, LoadAppliedToNarrowFace);
                C_F_Fc = m.GetSizeFactor(d, t, sawnLumberType, _commercialGrade, ww.ReferenceDesignValueType.CompresionParallelToGrain, LoadAppliedToNarrowFace);

            }
            else if (WoodMemberType.Contains("Sawn") && WoodMemberType.Contains("Timber"))
            {
                Timber timber = new Timber();
            }
            else
            {
                throw new Exception("Wood member type not supported.");
            }

            return new Dictionary<string, object>
            {
                { "C_F_Fb", C_F_Fb },
                { "C_F_Fc", C_F_Fc },
                { "C_F_Ft", C_F_Ft },
            };
        }




    }
}


