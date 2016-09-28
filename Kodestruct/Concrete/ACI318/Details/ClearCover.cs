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

#endregion

namespace Concrete.ACI318_14.Details
{

/// <summary>
///     Compression lap splice length
///     Category:   Concrete.ACI318_14.Details
/// </summary>
/// 


    [IsDesignScriptCompatible]
    public partial class 0 
    {
/// <summary>
///     Clear cover
/// </summary>
        /// <param name="ConcreteCoverType">  Indicates the type of element and service condition for concrete cover selection </param>

        
        [MultiReturn(new[] {  })]
        public static Dictionary<string, object> ClearCover(string ConcreteCoverType)
        {
            //Default values
            

            //Calculation logic:


            return new Dictionary<string, object>
            {
                 
            };
        }


        //internal 0 (string ConcreteCoverType)
        //{

        //}
        //[IsVisibleInDynamoLibrary(false)]
        //public static 0  ByInputParameters(string ConcreteCoverType)
        //{
        //    return new 0(ConcreteCoverType );
        //}

    }
}


