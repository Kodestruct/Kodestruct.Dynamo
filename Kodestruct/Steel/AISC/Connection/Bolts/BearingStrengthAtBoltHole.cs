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
using Kodestruct.Steel.AISC360v10.Connections.AffectedElements;
using System;
using Kodestruct.Steel.AISC.SteelEntities.Bolts;
using Kodestruct.Steel.AISC;
using Dynamo.Graph.Nodes;

#endregion

namespace Steel.AISC.Connection
{

/// <summary>
///     Bearing strength at bolt hole
///     Category:   Steel.AISC.Connection
/// </summary>
/// 


    [IsDesignScriptCompatible]
    public partial class Bolted 
    {
        /// <summary>
        ///    Calculates Bearing strength at bolt hole (kip - in unit system for all inputs and outputs)
        /// </summary>
        /// <param name="BoltHoleType">  Type of bolt hole </param>
        /// <param name="l_c">  Clear distance in the direction of the force between the edge of the hole and the edge of the adjacent hole or edge of the material  </param>
        /// <param name="F_u">  Specified minimum tensile strength   </param>
        /// <param name="F_y">  Specified minimum yield stress </param>
        /// <param name="d_b">  Nominal fastener diameter </param>
        /// <param name="t">  Thickness of element plate or element wall  </param>
        /// <param name="BoltHoleDeformationType">  Identifies of bolt deformation is a design consideration </param>
        /// <param name="IsUnstiffenedHollowSection">  Distinguishes between connections made using bolts that pass completely through an unstiffened box member or HSS and all other cases </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="phiR_nv"> Connection shear strength </returns>
        
        [MultiReturn(new[] { "phiR_nv" })]
        public static Dictionary<string, object> BearingStrengthAtBoltHole(string BoltHoleType,double l_c,double F_u,double F_y,double d_b,double t,
            string BoltHoleDeformationType = "ConsideredUnderServiceLoad", bool IsUnstiffenedHollowSection = false, string Code = "AISC360-10")
        {
            //Default values
            double phiR_nv = 0;
            BoltHoleType holeType;
            bool IsValidString =Enum.TryParse(BoltHoleType, true, out holeType);
            if (IsValidString ==true)
            {
                BoltHoleDeformationType deformationType;
                bool IsValidDeformationType = Enum.TryParse(BoltHoleDeformationType, true, out deformationType);
                if (IsValidDeformationType==true)
	                {
                        AffectedElementWithHoles element = new AffectedElementWithHoles();
                        phiR_nv = element.GetBearingStrengthAtBoltHole(l_c, d_b, t, F_y, F_u, holeType, deformationType, IsUnstiffenedHollowSection);
	                }
                else
                {
                    throw new Exception("Invalid Bolt Hole Deformation Type string");
                }
            }
            else
            {
                throw new Exception("Invalid bolt hole type string");
            }



            return new Dictionary<string, object>
            {
                { "phiR_nv", phiR_nv }
 
            };
        }



    }
}


