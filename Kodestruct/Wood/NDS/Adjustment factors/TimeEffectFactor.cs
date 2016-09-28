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

#endregion

namespace Wood.NDS
{

/// <summary>
///     Time effect factor
///     Category:   Wood.NDS
/// </summary>
/// 



    public partial class AdjustmentFactor 
    {
    /// <summary>
    ///     Time effect factor
    /// </summary>
    /// <param name="LoadCombinationType">  Identifies the type of load combination as required to calculate time-effect factor </param>
    /// <param name="IsConnection">  Identifies if the calculated value is applied in connection designed </param>
    /// <param name="IsTreated">  Identifies if the member is treated </param>
    /// <param name="WoodMemberType">  Distinguishes between dimensional lumber, timber,glulam etc. </param>
    /// <param name="Code">  Identifies the code or standard used for calculations </param>
    /// <returns name="lambda"> Time effect factor  </returns>

        [MultiReturn(new[] { "lambda" })]
        public static Dictionary<string, object> TimeEffectFactor(string LoadCombinationType, bool IsConnection=false, bool IsTreated=false, 
            string WoodMemberType = "SawnDimensionLumber", string Code = "NDS2015")
        {
            //Default values
            double lambda = 0;


            //Calculation logic:


            Kodestruct.Wood.NDS.LoadCombinationType _loadCombinationType;
            bool IsValidLoadComboString = Enum.TryParse(LoadCombinationType, true, out _loadCombinationType);
            if (IsValidLoadComboString == false)
            {
                throw new Exception("Failed to convert string. Errormessage. Please check input");
            }


            if (WoodMemberType.Contains("Sawn") && WoodMemberType.Contains("Lumber"))
            {
                //GetTimeEffectFactor
                DimensionalLumber m = new DimensionalLumber();
                lambda = m.GetTimeEffectFactor(_loadCombinationType, IsConnection, IsTreated);
            }
            else if (WoodMemberType.Contains("Sawn") && WoodMemberType.Contains("Timber"))
            {
                Timber t = new Timber();
                lambda = t.GetTimeEffectFactor(_loadCombinationType, IsConnection, IsTreated);
            }
            else
            {
                throw new Exception("Wood member type not supported.");
            }

            return new Dictionary<string, object>
            {
                { "lambda", lambda }
 
            };
        }



    }
}


