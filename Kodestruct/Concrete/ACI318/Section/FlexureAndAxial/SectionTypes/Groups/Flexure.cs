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
using Kodestruct.Concrete.ACI318_14;
using Kodestruct.Concrete.ACI;
using Concrete.ACI318.General;
using Kodestruct.Common.Section.Interfaces;
using System;
using KodestructAci = Kodestruct.Concrete.ACI;
using Dynamo.Graph.Nodes;

#endregion

namespace Concrete.ACI318.Section //.FlexureAndAxialForce
{

/// <summary>
///     SectionFlexural strength
///     Category:   Concrete.ACI318_14.Section.Flexure
/// </summary>
/// 




    [IsDesignScriptCompatible]
    public partial class FlexureAndAxialForce 
    {

        internal FlexureAndAxialForce()
        {

        }
        [IsVisibleInDynamoLibrary(false)]
        public static Flexure ByInputParameters()
        {
            return new Flexure();
        }

    }
}


