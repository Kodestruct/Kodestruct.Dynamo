//#region Copyright
//   /*Copyright (C) 2015 Konstantin Udilovich

//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at

//   http://www.apache.org/licenses/LICENSE-2.0

//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
//   */
//#endregion
 
//#region

//using Autodesk.DesignScript.Runtime;
//using Dynamo.Models;
//using System.Collections.Generic;
//using Dynamo.Nodes;
//using Kodestruct.Common.Section.Interfaces;
//using ds = Kodestruct.Common.Section.SectionTypes;
//using dm = Kodestruct.Common.Mathematics;
//using Kodestruct.Common.Section.Predefined;
//using Analysis.Section.SectionTypes;
//using shapes = Kodestruct.Common.Section.SectionTypes;
//using Kodestruct.Common.Entities;

//#endregion

//namespace Steel.AISC.Composite.Sections
//{


//    public partial class CompositeSectionI : CompositeSteelShape
//    {

//        [IsVisibleInDynamoLibrary(false)]
//        internal CompositeSectionI(double d, double b_f, double t_f, double t_w, double k)
//        {
//            ISliceableSection secI;

//            if (k<t_f)
//            {
//                secI = new shapes.SectionI("", d, b_f, t_f, t_w);
//            }
//            else
//            {
//                secI = new shapes.SectionIRolled("", d, b_f, t_f, t_w, k);
                
//            }
//            Section = secI;
//        }

//        [IsVisibleInDynamoLibrary(false)]
//        internal CompositeSectionI(string ShapeId)
//        {
//            AiscShapeFactory factory = new AiscShapeFactory();
//            //ISection section = factory.GetShape(ShapeId, ShapeTypeSteel.IShapeRolled);
//            //PredefinedSectionI catI = section as PredefinedSectionI;
//            //ISliceableSection secI = new shapes.SectionIRolled("", catI.d, catI.b_fTop, catI.tf, catI.t_w, catI.k);
//            //Section = secI;

//           //new! 
//            Section = factory.GetSliceableSection(ShapeId, ShapeTypeSteel.IShapeRolled);
//        }

//        public static CompositeSectionI FromGeometry(double d, double b_f, double t_f, double t_w, double k = 0)
//        {

//            return new CompositeSectionI(d, b_f, t_f, t_w, k);
//        }

//        public static CompositeSectionI FromShapeId(string ShapeId)
//        {

//            return new CompositeSectionI(ShapeId);
//        }
//    }
//}
