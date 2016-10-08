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

#endregion

namespace Analysis.Section.SectionTypes
{


    public partial class SectionTube: CustomProfile
    {

        [IsVisibleInDynamoLibrary(false)]
        internal SectionTube(double H, double B, double t, double t_des)
        {
             if (t_des == 0)
            {
                t_des = t;
            } 

            ISection  r = new ds.SectionTube("", H, B,t,t_des);
            Section = r;
        }

        public static SectionTube ByHeightWidthThickness(double H, double B, double t, double t_des)
        {
            return new SectionTube( H,  B,  t, t_des);
        }

    }
}
