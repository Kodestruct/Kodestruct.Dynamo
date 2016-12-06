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
using System.Linq;
using Kodestruct.Common.Section.Interfaces;
using ds = Kodestruct.Common.Section.SectionTypes;
using dm = Kodestruct.Common.Mathematics;
using Autodesk.DesignScript.Geometry;
using Kodestruct.Common.Section.General;
using Kodestruct.Common.Mathematics;

#endregion

namespace Analysis.Section.SectionTypes
{

    public partial class SectionPolygon: CustomProfile
    {

        [IsVisibleInDynamoLibrary(false)]
        internal SectionPolygon(List<Point> Points)
        {
            List<Point2D> points = Points.Select(p =>
                {
                    return new Point2D(p.X, p.Y);
                }).ToList();
            ISection poly = new PolygonShape(points);
            Section = poly;
           
        }

        public static SectionPolygon FromPoints(List<Point> Points)
        {
            return new SectionPolygon(Points);
        }

    }
}
