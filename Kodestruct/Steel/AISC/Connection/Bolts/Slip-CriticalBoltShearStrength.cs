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
using Kodestruct.Steel.AISC.AISC360v10.Connections.Bolted;
using Kodestruct.Steel.AISC;
using Kodestruct.Steel.AISC.Interfaces;
using b = Kodestruct.Steel.AISC.SteelEntities.Bolts;
using System;

#endregion

namespace Steel.AISC.Connection
{

/// <summary>
///     Slip-critical bolt shear strength
///     Category:   Steel.AISC.Connection
/// </summary>
/// 


    public partial class Bolted 
    {
        /// <summary>
        ///    Calculates Slip-critical bolt shear strength
        /// </summary>
        /// <param name="d_b">  Nominal fastener diameter </param>
        /// <param name="BoltMaterialId">  Bolt material specification </param>
        /// <param name="BoltHoleType">  Type of bolt hole </param>
        /// <param name="BoltFillerCase">  Distinguishes between filler cases for slip-critical bolt capacity calculations </param>
        /// <param name="BoltFayingSurfaceClass">  Identifies the type of faying surface for a slip critical bolt </param>
        /// <param name="NumberShearPlanes">  Number of shear planes </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="phiR_n"> Strength of member or connection </returns>

        [MultiReturn(new[] { "phiR_n" })]
        public static Dictionary<string, object> SlipCriticalBoltShearStrength(double d_b,string BoltMaterialId, string BoltHoleType, string BoltFillerCase="One", 
            string BoltFayingSurfaceClass="ClassA", double NumberShearPlanes=1, string Code = "AISC360-10")
        {
            //Default values
            double phiR_n = 0;

            BoltFayingSurfaceClass SurfaceClass = ParseSurfaceClass(BoltFayingSurfaceClass);
            BoltFillerCase FillerCase = ParseFillerCase(BoltFillerCase);
            b.BoltHoleType HoleType = ParseBoltHoleType(BoltHoleType);



            //Calculation logic:
            BoltFactory bf = new BoltFactory(BoltMaterialId);
            IBoltSlipCritical bolt = bf.GetSlipCriticalBolt(d_b, BoltThreadCase.Included, SurfaceClass, HoleType, FillerCase, NumberShearPlanes);
            phiR_n = bolt.GetSlipResistance();


            return new Dictionary<string, object>
            {
                { "phiR_n", phiR_n }
 
            };
        }

        private static b.BoltHoleType ParseBoltHoleType(string BoltHoleType)
        {
            b.BoltHoleType holeType;
            bool IsValidString = Enum.TryParse(BoltHoleType, true, out holeType);
            if (IsValidString == true)
            {
                return holeType;
            }
            else
            {
                throw new Exception("Bolt strength calculation failed. Invalid hole type designation.");
            }
        }

        private static BoltFillerCase ParseFillerCase(string BoltFillerCase)
        {
            BoltFillerCase fillerCase;
            bool IsValidString = Enum.TryParse(BoltFillerCase, true, out fillerCase);
            if (IsValidString == true)
            {
                return fillerCase;
            }
            else
            {
                throw new Exception("Bolt strength calculation failed. Invalid filler case specification.");
            }
        }

        private static BoltFayingSurfaceClass ParseSurfaceClass(string BoltFayingSurfaceClass)
        {
            BoltFayingSurfaceClass surfaceClass;

            if (BoltFayingSurfaceClass =="A")
            {
                surfaceClass = Kodestruct.Steel.AISC.BoltFayingSurfaceClass.ClassA;
            }
            else if (BoltFayingSurfaceClass == "B")
            {
                surfaceClass = Kodestruct.Steel.AISC.BoltFayingSurfaceClass.ClassB;
            }
            else
            {

                bool IsValidString = Enum.TryParse(BoltFayingSurfaceClass, true, out surfaceClass);
                if (IsValidString == true)
                {
                    return surfaceClass;
                }
                else
                {
                    throw new Exception("Bolt strength calculation failed. Invalid faying surface class.");
                }
            }

            return surfaceClass;
        }



    }
}


