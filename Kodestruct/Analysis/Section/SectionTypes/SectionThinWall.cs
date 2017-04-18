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
using Analysis.Section.SectionTypes.Elements;
using Kodestruct.Common.Section;

#endregion

namespace Analysis.Section.SectionTypes
{


    public partial class SectionThinWall : CustomProfile
    {

        [IsVisibleInDynamoLibrary(false)]
        internal SectionThinWall(List<ThinWallSegmentGroup> ThinWallSegmentGroups)
        {


            List<ThinWallSegment> segments = new List<ThinWallSegment>();

            foreach (var grp in ThinWallSegmentGroups)
            {
                // segments.AddRange(grp.Segments);
                foreach (var seg in grp.Segments)
                {
                    segments.Add(seg);
                }
            }

            ISection r = new Kodestruct.Common.Section.SectionTypes.SectionThinWall(segments);
            Section = r;

        }

        public static SectionThinWall BySegmentGroups(List<ThinWallSegmentGroup> ThinWallSegmentGroups)
        {

            return new SectionThinWall(ThinWallSegmentGroups);
        }

    }
}
