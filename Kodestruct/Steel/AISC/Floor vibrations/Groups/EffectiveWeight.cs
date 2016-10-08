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

namespace Steel.AISC.FloorVibrations
{




    [IsDesignScriptCompatible]
    public partial class EffectiveProperties 
    {

        internal EffectiveProperties()
        {

        }
        [IsVisibleInDynamoLibrary(false)]
        public static EffectiveProperties ByInputParameters()
        {
            return new EffectiveProperties();
        }

    }
}


