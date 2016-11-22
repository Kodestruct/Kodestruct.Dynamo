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
using Kodestruct.Concrete.ACI318_14;
using Concrete.ACI318.General;
using Concrete.ACI318.General.Reinforcement;
using Concrete.ACI318.General.Concrete;
using Dynamo.Graph.Nodes;
using KodestructAci = Kodestruct.Concrete.ACI;
using KodestructAci14 = Kodestruct.Concrete.ACI318_14;
using Kodestruct.Common.CalculationLogger;
using Kodestruct.Concrete.ACI.Entities;
using Kodestruct.Concrete.ACI;
using System;

#endregion

namespace Concrete.ACI318.Section.SectionTypes
{

/// <summary>
///   Rectangular section singly reinforced
///   Category:   Concrete.ACI318_14.General
/// </summary>
 


    [IsDesignScriptCompatible]
    public  class BoundaryZone 
    {

            [IsVisibleInDynamoLibrary(false)]
            public double b                {get; set;}
            [IsVisibleInDynamoLibrary(false)]
            public double h                {get; set;}
            [IsVisibleInDynamoLibrary(false)]
            public double A_s              {get; set;}
            [IsVisibleInDynamoLibrary(false)]
            public double N_curtains { get; set; }
            [IsVisibleInDynamoLibrary(false)]
            public string RebarSizeId      {get; set;}
            [IsVisibleInDynamoLibrary(false)]
            public double s                {get; set;}
            [IsVisibleInDynamoLibrary(false)]
            public double c_cntrEdge       {get; set;}
            [IsVisibleInDynamoLibrary(false)]
            public double N_Bar_Rows { get; set; }
            [IsVisibleInDynamoLibrary(false)]
            public double c_cntrInterior   { get; set; }

         [IsVisibleInDynamoLibrary(false)]
            internal BoundaryZone(double N_curtains, double N_Bar_Rows, string RebarSizeId, double s, double c_cntrEdge,
            double c_cntrInterior, double b=0)
        {
            h = (N_Bar_Rows - 1) * s + c_cntrEdge + c_cntrInterior;
            this.b = b;
            this.N_curtains = N_curtains;
            this.N_Bar_Rows            =  N_Bar_Rows           ;
            this.RebarSizeId      =  RebarSizeId       ;
            this.s                =  s                 ;
            this.c_cntrEdge       =  c_cntrEdge        ;
            this.c_cntrInterior = c_cntrInterior;

            RebarDesignation des;
            bool IsValidString = Enum.TryParse(RebarSizeId, true, out des);
            if (IsValidString == false)
            {
                throw new Exception("Rebar size is not recognized. Check input.");
            }
            RebarSection sec = new RebarSection(des);
            double A_b = sec.Area;

            //int NBarLines = (int)Math.Floor(h / s);
            //A_s = NBarLines * N_curtains * A_b;
            A_s = N_Bar_Rows * N_curtains * A_b;

        }

        /// <summary>
        /// Boundary zone object to be used as a part of shear wall object definition. Vertical orientation of shape is assumed for defining geometry. Width of boundary zone is taken to match web wall thickness.
        /// </summary>
        /// <param name="N_curtains">Number of bar curtains (along the length of the boundary wall, in the plane of the wall)</param>
        /// <param name="N_Bar_Rows">Number of bar rows</param>
        /// <param name="RebarSizeId">Bar designation</param>
        /// <param name="s">Spacing of rebar rows</param>
        /// <param name="c_cntrEdge">Cover to rebar center to boundary zone free edge</param>
         /// <param name="c_cntrInterior">Cover to rebar center to boundary zone at web wall</param>
        /// <returns></returns>
         public static BoundaryZone ByRebarSizeAndSpacing(double N_curtains, double N_Bar_Rows, string RebarSizeId, double s, double c_cntrEdge,
            double c_cntrInterior)
        {
            return new BoundaryZone(N_curtains, N_Bar_Rows, RebarSizeId, s, c_cntrEdge,
            c_cntrInterior);
        }

         /// <summary>
         /// Boundary zone object to be used as a part of shear wall object definition. Vertical orientation of shape is assumed for defining geometry. 
         /// </summary>
         /// <param name="b"> Width of boundary zone, measured normal to plane of wall</param>
         /// <param name="N_curtains">Number of bar curtains (along the length of the boundary wall, in the plane of the wall)</param>
         /// <param name="N_Bar_Rows">Number of bar rows</param>
         /// <param name="RebarSizeId">Bar designation</param>
         /// <param name="s">Spacing of rebar rows</param>
         /// <param name="c_cntrEdge">Cover to rebar center to boundary zone free edge</param>
         /// <param name="c_cntrInterior">Cover to rebar center to boundary zone at web wall</param>
         /// <returns></returns>
         public static BoundaryZone ByWidthAndRebarSizeAndSpacing(double b, double N_curtains, double N_Bar_Rows, string RebarSizeId, double s, double c_cntrEdge,
        double c_cntrInterior)
         {
             return new BoundaryZone(N_curtains, N_Bar_Rows, RebarSizeId, s, c_cntrEdge,
             c_cntrInterior,b);
         }



    }
}


