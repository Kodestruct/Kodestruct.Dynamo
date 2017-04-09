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
using Kodestruct.Concrete.ACI;
using Dynamo.Graph.Nodes;
using Kodestruct.Aluminum.AA.Entities.Material;
using Kodestruct.Common.Section.Interfaces;

#endregion

namespace Aluminum.AA.Material
{


    [IsDesignScriptCompatible]
    public partial class AluminumMaterial
    {

        [IsVisibleInDynamoLibrary(false)]
        internal AluminumMaterial(string AluminumAlloyId, string AluminumTemperId, string AluminumProductId, string ThicknessRangeId, string WeldCaseId = "NotAffected",
            bool MeetsAdditionalWeldingCriteria=true)
        {
            
            if (WeldCaseId == "NotAffected")
            {
                IsWelded = false;
            }
            else
            {
                IsWelded = true;
            }
            //Material = new Kodestruct.Aluminum.AA.AA2015.AluminumMaterial(Alloy, Temper, ThicknessRange, ProductType, IsWelded,
            //MeetsAdditionalWeldingCriteria);

            this.Alloy = AluminumAlloyId;
            this.Temper = AluminumTemperId;
            this.ThicknessRange = ThicknessRangeId;
            this.ProductType = AluminumProductId;
            this.WeldCaseId                      =WeldCaseId                     ;
            this.MeetsAdditionalWeldingCriteria  =MeetsAdditionalWeldingCriteria ;
        }

        public static AluminumMaterial ByAlloyTemperProduct(string AluminumAlloyId, string AluminumTemperId, string AluminumProductId, string ThicknessRangeId, string WeldCaseId = "NotAffected",
            bool MeetsAdditionalWeldingCriteria = true)
        {
            return new AluminumMaterial(AluminumAlloyId, AluminumTemperId, AluminumProductId, ThicknessRangeId, WeldCaseId,
            MeetsAdditionalWeldingCriteria);
        }


               //[IsVisibleInDynamoLibrary(false)]
        //Kodestruct.Aluminum.AA.AA2015.AluminumMaterial GetMaterial()
        //{
        //    Kodestruct.Aluminum.AA.AA2015.AluminumMaterial m = new Kodestruct.Aluminum.AA.AA2015.AluminumMaterial(Alloy, Temper, ThicknessRange, ProductType, IsWelded,
        //    MeetsAdditionalWeldingCriteria);
        //    return m; }

        [IsVisibleInDynamoLibrary(false)]
            public string Alloy                           ;
        [IsVisibleInDynamoLibrary(false)]
            public string Temper                          ;
        [IsVisibleInDynamoLibrary(false)]
            public string ThicknessRange                  ;
        [IsVisibleInDynamoLibrary(false)]
            public string ProductType                     ;
        [IsVisibleInDynamoLibrary(false)]
            public string WeldCaseId                      ;
        [IsVisibleInDynamoLibrary(false)]
            public bool   MeetsAdditionalWeldingCriteria  ;
        [IsVisibleInDynamoLibrary(false)]
            public bool IsWelded;
    }
}


