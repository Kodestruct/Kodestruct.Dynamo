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


    public partial class SectionIWithReinfWebOpening: CustomProfile
    {

        [IsVisibleInDynamoLibrary(false)]
        internal SectionIWithReinfWebOpening(double d, double b_f, double t_f, double t_w, double h_o,
            double e, double b_r, double t_r, bool IsOneSidedReinforcment= true)
        {
            ds.SectionIWithReinfWebOpening r = new ds.SectionIWithReinfWebOpening("", d, b_f, t_f, t_w,h_o,e,b_r,t_r,IsOneSidedReinforcment);
            Section = r;
        }

        public static SectionIWithReinfWebOpening ByFlangeWebAndPlateDimensions(double d, double b_f, double t_f, double t_w, double h_o,
            double e,double b_r,double t_r,bool IsOneSidedReinforcment)
        {
            return new SectionIWithReinfWebOpening(d, b_f, t_f, t_w, h_o, e, b_r, t_r, IsOneSidedReinforcment);
        }

    }
}
