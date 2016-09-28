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
using dm = Kodestruct.Common.Mathematics;
using Kodestruct.Common.Section.Predefined;
using Analysis.Section.SectionTypes;
using shapes = Kodestruct.Common.Section.SectionTypes;
using Kodestruct.Common.Entities;
using Kodestruct.Common;
using System;

#endregion

namespace Analysis.Section.AISC
{


    public partial class CatalogSection : CustomProfile
    {


        [IsVisibleInDynamoLibrary(false)]
        internal CatalogSection(string ShapeId, string AngleOrientation="LongLegVertical", string AngleRotation="FlatLegTop")
        {
            AiscShapeFactory factory = new AiscShapeFactory();
            AngleOrientation ori = new AngleOrientation();
            AngleRotation  rot = new Kodestruct.Common.AngleRotation();

              AngleOrientation _AngleOrientation;
              bool IsValidInputString = Enum.TryParse(AngleOrientation, true, out _AngleOrientation);
                if (IsValidInputString == false)
                {
                        throw new Exception("Failed to convert string. Specifuy AngleOrientation. Please check input");
                }

                
                AngleRotation _AngleRotation;
                bool IsValidInputRotation = Enum.TryParse(AngleRotation, true, out _AngleRotation);
                if (IsValidInputRotation == false)
                {
                    throw new Exception("Failed to convert string. Errormessage. Please check input");
                }


             ISection section = factory.GetShape(ShapeId, _AngleOrientation, _AngleRotation);
            //PredefinedSectionI catI = section as PredefinedSectionI;
            //ISliceableSection secI = new shapes.SectionIRolled("", catI.d, catI.b_fTop, catI.t_f, catI.t_w, catI.k);
            //Section = secI;
            Section = section;
        }


        public static CatalogSection FromShapeId(string ShapeId, string AngleOrientation = "LongLegVertical", string AngleRotation ="FlatLegBottom")
        {

            return new CatalogSection(ShapeId, AngleOrientation, AngleRotation);
        }
    }
}
