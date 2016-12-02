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
using Kodestruct.Common.CalculationLogger;
using Kodestruct.Concrete.ACI.Entities;
using Kodestruct.Concrete.ACI;
using Kodestruct.Concrete.ACI.ACI318_14.C22_SectionalStrength.Shear.TwoWay;
using System;
using Autodesk.DesignScript.Geometry;
using Kodestruct.Common.Mathematics;

#endregion

namespace Concrete.ACI318.Section.ShearAndTorsion.TwoWayShear.Perimeter
{




    [IsDesignScriptCompatible]
    public  class PunchingShearPerimeter 
    {



         [IsVisibleInDynamoLibrary(false)]
        internal PunchingShearPerimeter (string PunchingPerimeterConfiguration, List<Line> Lines, Point ColumnCenter)
        {
             List<PerimeterLineSegment> segments = Lines.Select( l => 
                 {
                     return new PerimeterLineSegment(new Point2D(l.StartPoint.X, l.StartPoint.Y), new Point2D(l.EndPoint.X, l.EndPoint.Y));
                 }).ToList();
                this.PerimeterData =  new PunchingPerimeterData(
                segments, new Point2D(ColumnCenter.X,ColumnCenter.Y));
        }

         [IsVisibleInDynamoLibrary(false)]
            internal PunchingShearPerimeter(string PunchingPerimeterConfiguration, double c_x, double c_y, double d,
            double b_xCant, double b_yCant, double X_ColumnCenter , double Y_ColumnCenter)
        {

            
            PunchingPerimeterConfiguration Configuration;
            bool IsValidInputString = Enum.TryParse(PunchingPerimeterConfiguration, true, out Configuration);
            if (IsValidInputString == false)
            {
                throw new Exception("Failed to convert string. Examples of acceptable values are Interior, EdgeLeft, CornerLeftTop. Please check input");
            }

            this.Configuration = PunchingPerimeterConfiguration;
             this.c_x           = c_x           ;
             this.c_y           = c_y           ;
             this.d             = d             ;
             this.b_xCant       = b_xCant       ;
             this.b_yCant = b_yCant             ;
             this.X_ColumnCenter = X_ColumnCenter;
             this.Y_ColumnCenter = Y_ColumnCenter;

            PerimeterFactory factory = new PerimeterFactory();
            this.PerimeterData = factory.GetPerimeterData(Configuration, c_x, c_y, d,
                b_xCant, b_yCant, new Point2D(X_ColumnCenter,Y_ColumnCenter));
        }


         public static PunchingShearPerimeter ByColumnType(string PunchingPerimeterConfiguration, double c_x, double c_y, double d,
        double b_xCant=0, double b_yCant=0, double X_ColumnCenter = 0, double Y_ColumnCenter=0)
        {
            return new PunchingShearPerimeter( PunchingPerimeterConfiguration, c_x, c_y,  d, b_xCant,  b_yCant,  X_ColumnCenter,  Y_ColumnCenter);
        }

         public static PunchingShearPerimeter ByLineGeometry(string PunchingPerimeterConfiguration, List<Line> Lines, Point ColumnCenter)
         {
             return new PunchingShearPerimeter(PunchingPerimeterConfiguration, Lines,ColumnCenter);
         }



        [IsVisibleInDynamoLibrary(false)]
            List<Line> Lines    {get; set;}
        [IsVisibleInDynamoLibrary(false)]
            Point ColumnCenter { get; set; }

                [IsVisibleInDynamoLibrary(false)]
                public PunchingPerimeterData PerimeterData { get; set; }
                [IsVisibleInDynamoLibrary(false)]
                public double c_x    {get; set;}
                [IsVisibleInDynamoLibrary(false)]
                public double c_y { get; set; }
                [IsVisibleInDynamoLibrary(false)]
                public double d { get; set; }
                [IsVisibleInDynamoLibrary(false)]
                public double b_xCant { get; set; }
                [IsVisibleInDynamoLibrary(false)]
                public double b_yCant { get; set; }
                [IsVisibleInDynamoLibrary(false)]
                public string Configuration { get; set; }
                
                [IsVisibleInDynamoLibrary(false)]
                public double X_ColumnCenter { get; set; }

                [IsVisibleInDynamoLibrary(false)]
                public double Y_ColumnCenter { get; set; }

    }
}


