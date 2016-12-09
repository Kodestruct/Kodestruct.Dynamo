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
///     Repetitive member factor
///     Category:   Wood.NDS
/// </summary>
/// 



    public partial class AdjustmentFactor 
    {
        /// <summary>
        ///     Repetitive member factor (kip - in unit system for all inputs and outputs)
        /// </summary>
        /// <param name="t">Member thickness</param>
        /// <param name="s_member">Center-to-center spacing between adjacent members in a row </param>
        /// <param name="IsRepetitiveMember"> Identifies if member meets repetitve member criteria </param>
        /// <param name="WoodMemberType">  Distinguishes between dimensional lumber, timber,glulam etc. </param>
        /// <param name="Code">  Identifies the code or standard used for calculations </param>
        /// <returns name="C_r"> Repetitive-member factor for bending design values </returns>

        [MultiReturn(new[] { "C_r" })]
        public static Dictionary<string, object> RepetitiveMemberFactor(double t =1.5, double s_member=24, bool IsRepetitiveMember=false,
             string WoodMemberType = "SawnDimensionLumber", string Code = "NDS2015")
        {
            //Default values
            double C_r = 0;


            //Calculation logic:

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
                C_r = m.GetRepetitiveMemberFactor(IsRepetitiveMember, s_member, t, sawnLumberType);
            }
            else if (WoodMemberType.Contains("Sawn") && WoodMemberType.Contains("Timber"))
            {
                C_r = 1.0;
            }
            else
            {
                throw new Exception("Wood member type not supported.");
            }

            return new Dictionary<string, object>
            {
                { "C_r", C_r }
 
            };
        }



    }
}


