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
using Dynamo.Graph.Nodes;


#endregion

namespace Analysis.Beam
{


    [IsDesignScriptCompatible]
    public partial class Flexure 
    {

         //[IsVisibleInDynamoLibrary(false)]
        internal Flexure(double L, double X, double P, double M, double w, double a_load, double b_load, double c_load, double P1, double P2, double M1, double M2)
        {

        }
        [IsVisibleInDynamoLibrary(false)]
         public static Flexure ByInputParameters(double L, double X, double P, double M, double w, double a_load, double b_load, double c_load, double P1, double P2, double M1, double M2)
        {
            return new Flexure(L, X, P, M, w, a_load, b_load, c_load, P1, P2, M1, M2);
        }

    }
}


