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
using Kodestruct.Common.Section.Interfaces;
using ds = Kodestruct.Common.Section.SectionTypes;
using dm = Kodestruct.Common.Mathematics;
using System;
using Kodestruct.Common.Section.SectionTypes;

#endregion

namespace Analysis.Section.SectionTypes
{


    public partial class SectionMultipleStacked : CustomProfile
    {

        [IsVisibleInDynamoLibrary(false)]
        internal SectionMultipleStacked(List<SectionRectangular> Rectangles)
        {

            List<ds.SectionRectangular> rectangs = new List<ds.SectionRectangular>();
            foreach (var r in Rectangles)
            {
                ds.SectionRectangular sr = r.Section as ds.SectionRectangular;
                rectangs.Add(sr);
            }
            this.Section = new SectionCompoundStacked(rectangs);
        }

        /// <summary>
        /// Creates a custom profile object consisting of multiple vertically stacked rectangles.List rectangles in the ascending Y-coordinate order (lower rectangles first). Ensure there are no vertical overlaps or gaps between rectnagles.
        /// </summary>
        /// <param name="Rectangles">List of rectangle profile objects</param>
        /// <returns></returns>
        public static SectionMultipleStacked ByStackedRectangles(List<SectionRectangular> Rectangles)
        {
            return new SectionMultipleStacked(Rectangles);
        }

    }
}
