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
using Kodestruct.Concrete.ACI;
using Dynamo.Graph.Nodes;
using KodestructAci =Kodestruct.Concrete.ACI;

#endregion

namespace Concrete.ACI318.General.Reinforcement
{

/// <summary>
///     Rebar point by area
///     Category:   Concrete.ACI318_14.General.Rebar
/// </summary>
/// 


    [IsDesignScriptCompatible]
    public partial class RebarPoint
    {




        [IsVisibleInDynamoLibrary(false)]
         internal RebarPoint(double A_b, double X_shp, double Y_shp, RebarMaterial RebarMaterial)
        {

            KodestructAci.Rebar b = new KodestructAci.Rebar(A_b, RebarMaterial.Material);
            this.RebarPointLData = new KodestructAci.RebarPoint(b, new KodestructAci.RebarCoordinate() { X = X_shp, Y = Y_shp });

        }

         /// <summary>
        ///     Rebar point object by area
        /// </summary>
        /// <param name="A_b">   Area of an individual bar or wire  </param>
        /// <param name="X_shp">  Point coordinate X (in shape coordinate system) </param>
        /// <param name="Y_shp">  Point coordinate Y  (in shape coordinate system) </param>
        /// <param name="RebarMaterial">  Rebar material (object) </param>
        /// <returns name="RebarPoint"> Rebar point object. Create the object using input parameters first </returns>
        public static RebarPoint ByArea(double A_b, double X_shp, double Y_shp, RebarMaterial RebarMaterial)
        {

            return new RebarPoint(A_b,X_shp,Y_shp,RebarMaterial);
        }


        private KodestructAci.RebarPoint rebarPointData;

        [IsVisibleInDynamoLibrary(false)]
        public KodestructAci.RebarPoint RebarPointLData
        {
            get { return rebarPointData; }
            set { rebarPointData = value; }
        }


 
    }
}


