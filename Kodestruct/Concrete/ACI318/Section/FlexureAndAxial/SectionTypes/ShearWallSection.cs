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
using System.Linq;
using Dynamo.Models;
using System.Collections.Generic;
using Dynamo.Nodes;
using Kodestruct.Concrete.ACI318_14;
using Concrete.ACI318.General;
using Concrete.ACI318.General.Reinforcement;
using Concrete.ACI318.General.Concrete;
using Dynamo.Graph.Nodes;
using KodestructAci = Kodestruct.Concrete.ACI;
using KodestructAci14 = Kodestruct.Concrete.ACI318_14;
using KodestructSection = Kodestruct.Common.Section;
using Kodestruct.Common.CalculationLogger;
using Kodestruct.Concrete.ACI;
using Kodestruct.Common.Section.Interfaces;
using System;
using Kodestruct.Concrete.ACI.Entities;
using Kodestruct.Common.Mathematics;

#endregion

namespace Concrete.ACI318.Section.SectionTypes
{

/// <summary>
///   ShearWallSection
///   Category:   Concrete.ACI318_14.General
/// </summary>
 


    [IsDesignScriptCompatible]
    public partial class ShearWallSection : ConcreteFlexureAndAxiaSection
    {

            //Default values
            double b;
            double h;
            double A_s;
            double c_cntr;
            ConcreteMaterial ConcreteMaterial;
            RebarMaterial LongitudinalRebarMaterial;

         [IsVisibleInDynamoLibrary(false)]
            internal ShearWallSection(double h_total, double t_w,
            string RebarSizeId, double N_curtains, double s, double c_edge,
            BoundaryZone BoundaryZoneTop, BoundaryZone BoundaryZoneBottom,
             ConcreteMaterial ConcreteMaterial, RebarMaterial LongitudinalRebarMaterial)
            {

                ConfinementReinforcementType ConfinementReinforcementType = KodestructAci.ConfinementReinforcementType.Ties;
                base.ConcreteMaterial = ConcreteMaterial; 
                CrossSectionIShape shape = GetIShape(ConcreteMaterial.Concrete, h_total, t_w, BoundaryZoneTop, BoundaryZoneBottom);

                List<KodestructAci.RebarPoint> LongitudinalBars = GetLongitudinalBars(shape.SliceableShape as ISectionI, h_total, t_w, RebarSizeId, N_curtains, s, c_edge,
                BoundaryZoneTop, BoundaryZoneBottom, LongitudinalRebarMaterial);

                KodestructAci.IConcreteFlexuralMember fs = new KodestructAci14.ConcreteSectionFlexure(shape, LongitudinalBars, new CalcLog(), ConfinementReinforcementType);
                this.FlexuralSection = fs;
             }

        [IsVisibleInDynamoLibrary(false)]
         private List<KodestructAci.RebarPoint> GetLongitudinalBars(KodestructSection.Interfaces.ISectionI shape, double h_total, double t_w, 
            string RebarSizeId, double N_curtains, double s, double c_edge, 
             BoundaryZone BoundaryZoneTop, BoundaryZone BoundaryZoneBottom, RebarMaterial LongitudinalRebarMaterial)
         {

             List<KodestructAci.RebarPoint> BzTopBars = GetBoundaryZoneBars(BoundaryZoneTop, LongitudinalRebarMaterial, new Point2D(0.0, shape.d/2.0 - BoundaryZoneTop.h / 2.0),true);
             List<KodestructAci.RebarPoint> BzBottomBars = GetBoundaryZoneBars(BoundaryZoneBottom, LongitudinalRebarMaterial, new Point2D(0.0, shape.d/2.0 + BoundaryZoneTop.h / 2.0), false);
             List<KodestructAci.RebarPoint> WallBars = GetWallBars(h_total - (BoundaryZoneTop.h + BoundaryZoneBottom.h), t_w, RebarSizeId, N_curtains, s, c_edge, LongitudinalRebarMaterial);

             List<KodestructAci.RebarPoint> retBars = BzTopBars.Concat(BzBottomBars).Concat(WallBars).ToList() ;
             return retBars;
        }

        private List<KodestructAci.RebarPoint> GetWallBars(double h, double t_w, string RebarSizeId, double N_curtains, double s, double c_edge, RebarMaterial LongitudinalRebarMaterial)
        {

            RebarDesignation des;
            bool IsValidString = Enum.TryParse(RebarSizeId, true, out des);
            if (IsValidString == false)
            {
                throw new Exception("Rebar size is not recognized. Check input.");
            }
            RebarSection sec = new RebarSection(des);
            double A_b = sec.Area;

            int NBarLines = (int)Math.Floor(h / s);
            double A_s = NBarLines * N_curtains*A_b;
            RebarLine Line = new RebarLine(A_s,
            new Point2D(0.0, -h / 2.0 + c_edge),
            new Point2D(0.0, h / 2.0 - c_edge),
            LongitudinalRebarMaterial.Material, false, false, NBarLines);

            return Line.RebarPoints;
        }

        private List<KodestructAci.RebarPoint> GetBoundaryZoneBars(BoundaryZone BoundaryZone, RebarMaterial LongitudinalRebarMaterial, Point2D BzCentroid, bool IsTop)
        {

            Point2D topPoint;
            Point2D botPoint;

            if (IsTop == true)
	            {
                    topPoint = new Point2D(0, BzCentroid.Y + (BoundaryZone.h / 2.0 - BoundaryZone.c_cntrEdge));
                    botPoint = new Point2D(0, BzCentroid.Y - (BoundaryZone.h / 2.0 - BoundaryZone.c_cntrInterior));
	            }
            else
	        {
                topPoint = new Point2D(0, BzCentroid.Y + (BoundaryZone.h / 2.0 - BoundaryZone.c_cntrInterior));
                botPoint = new Point2D(0, BzCentroid.Y - (BoundaryZone.h / 2.0 - BoundaryZone.c_cntrEdge));
	        }



            RebarLine Line = new RebarLine(BoundaryZone.A_s,
            botPoint, topPoint, LongitudinalRebarMaterial.Material, false, false, (int)BoundaryZone.N_Bar_Rows-1);

            return Line.RebarPoints;
        }

        [IsVisibleInDynamoLibrary(false)]
         private CrossSectionIShape GetIShape(IConcreteMaterial Material,double h_total, double t_w, BoundaryZone BoundaryZoneTop, BoundaryZone BoundaryZoneBottom)
         {

            double d = h_total;
            double b_fTop = BoundaryZoneTop.b == 0 ? t_w : BoundaryZoneTop.b;
            double b_fBot = BoundaryZoneBottom.b == 0 ? t_w : BoundaryZoneBottom.b;
            double t_fTop =  BoundaryZoneTop.h ;
            double t_fBot = BoundaryZoneBottom.h;

            return new CrossSectionIShape( Material,null,  d,  b_fTop,  b_fBot,  t_fTop,  t_fBot,  t_w);
        
         }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="h_total">Total height (including boundary zones)</param>
        /// <param name="t_w">Wall web thickness</param>
        /// <param name="RebarSizeId">Rebar Id</param>
        /// <param name="N_curtains">Number of rebar curtains</param>
        /// <param name="s">Bar spacing</param>
        /// <param name="c_edge">Concrete cover to center of rebar at wall ege</param>
        /// <param name="BoundaryZoneTop">Boundary zone object</param>
        /// <param name="BoundaryZoneBottom">Boundary zone object</param>
        /// <param name="ConcreteMaterial">Concrete material</param>
        /// <param name="LongitudinalRebarMaterial">Rebar material</param>
        /// <returns></returns>
         public static ShearWallSection ByWallGeometryAndBoundaryZones(double h_total, double t_w,
            string RebarSizeId, double N_curtains, double s, double c_edge,           
            BoundaryZone BoundaryZoneTop, BoundaryZone BoundaryZoneBottom,
             ConcreteMaterial ConcreteMaterial, RebarMaterial LongitudinalRebarMaterial)
        {
            return new ShearWallSection(h_total, t_w,
             RebarSizeId,  N_curtains, s, c_edge,           
             BoundaryZoneTop,  BoundaryZoneBottom,
              ConcreteMaterial,  LongitudinalRebarMaterial);
        }


    }
}


