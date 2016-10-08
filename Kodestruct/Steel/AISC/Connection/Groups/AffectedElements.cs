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

namespace Steel.AISC.Connection
{


    public partial class AffectedElements
    {
        [IsVisibleInDynamoLibrary(false)]
        internal AffectedElements(double A_gv, double A_nv, double A_nt, double F_y, double F_u, string StressDistibutionType)
        {

        }
        [IsVisibleInDynamoLibrary(false)]
        public static AffectedElements ByInputParameters(double A_gv, double A_nv, double A_nt, double F_y, double F_u, string StressDistibutionType)
        {
            return new AffectedElements(A_gv, A_nv, A_nt, F_y, F_u, StressDistibutionType);
        }
    }
}
