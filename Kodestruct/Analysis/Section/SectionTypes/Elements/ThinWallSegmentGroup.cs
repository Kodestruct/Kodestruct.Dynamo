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
using Autodesk.DesignScript.Geometry;
using Kodestruct.Common.Section;
using System.Linq;
using Kodestruct.Common.Mathematics;

#endregion

namespace Analysis.Section.SectionTypes.Elenents
{


    public partial class ThinWallSegmentGroup
    {

        [IsVisibleInDynamoLibrary(false)]
        internal ThinWallSegmentGroup(List<Line> Lines, double WallThickness)
        {

            _segments = (List<ThinWallSegment>)Lines.Select(l =>
               {
                   Point2D nodeI = new Point2D(l.StartPoint.X, l.StartPoint.Y);
                   Point2D nodeJ = new Point2D(l.EndPoint.X, l.EndPoint.Y);
                   Line2D line = new Line2D(nodeI, nodeJ);
                   ThinWallSegment s = new ThinWallSegment(line, WallThickness);
                   return s;
               }).ToList();
        }

        public static ThinWallSegmentGroup ByLines(List<Line> Lines, double WallThickness)
        {

            return new ThinWallSegmentGroup(Lines, WallThickness);
        }


        List<ThinWallSegment> _segments;

        [IsVisibleInDynamoLibrary(false)]
        public List<ThinWallSegment> Segments
        {
            get { return _segments; }
            set { _segments = value; }
        }

    }
}
