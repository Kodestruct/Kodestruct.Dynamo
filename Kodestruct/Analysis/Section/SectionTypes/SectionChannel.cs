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
using Kodestruct.Common.Section.Interfaces;
using ds = Kodestruct.Common.Section.SectionTypes;

#endregion

namespace Analysis.Section.SectionTypes
{


    public partial class SectionChannel: CustomProfile
    {

        [IsVisibleInDynamoLibrary(false)]
        internal SectionChannel(double d, double b_f, double t_f, double t_w, bool IsWeakAxis, bool AreFlangeTipsDown)
        {
            ISection r = new ds.SectionChannel("", d, b_f, t_f, t_w, IsWeakAxis, AreFlangeTipsDown);
            Section = r;
        }

        /// <summary>
        /// Creates an instance of a channel shape
        /// </summary>
        /// <param name="d">Depth</param>
        /// <param name="b_f">Flange width</param>
        /// <param name="t_f">Flange thickness</param>
        /// <param name="t_w">Web thickness</param>
        /// <param name="IsWeakAxis">Indicates if the section is rotated to have weak axis oriented horizontally</param>
        /// <param name="AreFlangeTipsDown">If in weak axis shape orientation, indicates if the shape is oriended with flanges pointing up or down</param>
        /// <returns></returns>
        public static SectionChannel ByFlangeAndWebDimensions(double d, double b_f, double t_f, double t_w, bool IsWeakAxis=false, bool AreFlangeTipsDown=false)
        {
            return new SectionChannel(d, b_f, t_f, t_w, IsWeakAxis, AreFlangeTipsDown);
        }

    }
}
