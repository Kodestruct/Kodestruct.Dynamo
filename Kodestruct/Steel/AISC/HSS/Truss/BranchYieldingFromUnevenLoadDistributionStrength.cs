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
using Analysis.Section;
using Kodestruct.Steel.AISC.AISC360v10.HSS.TrussConnections;
using Kodestruct.Steel.AISC.AISC360v10.K_HSS.TrussConnections;
using Kodestruct.Common.Section.Interfaces;
using System;
using Kodestruct.Steel.AISC.Entities;

#endregion

namespace Steel.AISC.HSS
{

    /// <summary>
    ///     Branch yielding from uneven load distribution strength
    ///     Category:   Steel.AISC10.HSS
    /// </summary>
    /// 


    public partial class Truss
    {
        /// <summary>
        ///     Branch yielding from uneven load distribution strength
        /// </summary>
        /// <param name="HssTrussConnectionMemberType">  Specifies if the connection members are circular HSS (CHS) or rectangular HSS </param>
        /// <param name="HssTrussConnectionClassification">  Distinguishes between T, Y, X, gapped K or overlapped K connection </param>
        /// <param name="MainBranchSection">  Section object (Tube or Pipe) </param>
        /// <param name="theta_main">  Angle between chord and main branch or overlapped branch  </param>
        /// <param name="AxialForceTypeMain">  Distinguishes between tension, compression or reversible force in main branch member </param>
        /// <param name="SecondaryBranchSection">  Section object (Tube or Pipe). Specify same section as main branch for T and Y connections </param>
        /// <param name="theta_sec">  Angle between chord and secondary branch or overlapping branch. Specify same angle as main branch for T and Y connections </param>
        /// <param name="AxialForceTypeSecondary">  Distinguishes between tension, compression or reversible force in main branch member </param>
        /// <param name="F_yb">  Specified minimum yield stress of HSS branch member material  </param>
        /// <param name="ChordSection">  Section object (Tube or Pipe) </param>
        /// <param name="F_yc">  Specified minimum yield stress of HSS chord member material  </param>
        /// <param name="IsTensionChord">  Indicates if chord face is in tension  </param>
        /// <param name="P_uChord">  Required axial strength in chord member </param>
        /// <param name="M_uChord">  Required flexural strength in chord member </param>
        /// <param name="O_v">  Overlap connection coefficient (ranges from 0.25 and 1) </param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="phiP_nMain"> Main branch strength at connection </returns>
        /// <returns name="phiP_nSec"> Secondary branch strength at connection </returns>
        /// <returns name="IsApplicableMain"> Identifies whether the selected limit state is applicable (main branch)</returns>
        /// <returns name="IsApplicableSecn"> Identifies whether the selected limit state is applicable (secondary branch)</returns>

        [MultiReturn(new[] { "phiP_nMain", "phiP_nSec", "IsApplicableMain", "IsApplicableSecn" })]
        public static Dictionary<string, object> BranchYieldingFromUnevenLoadDistributionStrength(string HssTrussConnectionMemberType, string HssTrussConnectionClassification, CustomProfile MainBranchSection, double theta_main,
            string AxialForceTypeMain, CustomProfile SecondaryBranchSection, double theta_sec, string AxialForceTypeSecondary, double F_yb, CustomProfile ChordSection, double F_yc,
            bool IsTensionChord, double P_uChord, double M_uChord, double O_v, string Code = "AISC360-10")
        {
            //Default values
            double phiP_nMain = 0;
            double phiP_nSec = 0;
            bool IsApplicableMain = false;
            bool IsApplicableSecn = false;


            //Calculation logic:

            #region Evaluate and update input

            HssTrussConnectionMemberType _MemberType;
            HssTrussConnectionClassification _Class;
            AxialForceType _MainBranchForceType;
            AxialForceType _SecondaryBranchForceType;


            ISectionHollow _MainBranchSection;
            ISectionHollow _SecondaryBranchSection;
            ISectionHollow _ChordSection;

            bool IsValidMemType = Enum.TryParse(HssTrussConnectionMemberType, true, out _MemberType);
            if (IsValidMemType == false)
            {
                throw new Exception("Failed to convert string. HssTrussConnectionMemberType must be either RHS (rectanguar HSS) or CHS (circular HSS) . Please check input.");
            }


            bool IsValidIClass = Enum.TryParse(HssTrussConnectionClassification, true, out _Class);
            if (IsValidIClass == false)
            {
                throw new Exception("Failed to convert string. HssTrussConnectionClassification needs to specify T,Y,X, GappedK or Overlapped K connection type. Please check input.");
            }


            bool IsValidMainForce = Enum.TryParse(AxialForceTypeMain, true, out _MainBranchForceType);
            if (IsValidMainForce == false)
            {
                throw new Exception("Failed to convert string. Specify force as Tension, Compression or Reversible. Please check input.");
            }


            bool IsValidSecForce = Enum.TryParse(AxialForceTypeSecondary, true, out _SecondaryBranchForceType);
            if (IsValidSecForce == false)
            {
                throw new Exception("Failed to convert string. Specify force as Tension, Compression or Reversible. Please check input.");
            }

            if (!(MainBranchSection.Section is ISectionHollow) || !(SecondaryBranchSection.Section is ISectionHollow) || !(ChordSection.Section is ISectionHollow))
            {
                throw new Exception("Failed to convert section. Section needs to be either a Pipe or a Tube. Please check input.");
            }

            _MainBranchSection = MainBranchSection.Section as ISectionHollow;
            _SecondaryBranchSection = SecondaryBranchSection.Section as ISectionHollow;
            _ChordSection = ChordSection.Section as ISectionHollow;
            #endregion


            HssTrussConnectionFactory factory = new HssTrussConnectionFactory();

            IHssTrussBranchConnection con = factory.GetConnection(_MemberType, _Class, _ChordSection, _MainBranchSection, _SecondaryBranchSection, F_yc,
            F_yb, theta_main, theta_sec, _MainBranchForceType, _SecondaryBranchForceType, IsTensionChord, P_uChord, M_uChord, O_v);


            phiP_nMain = con.GetBranchYieldingFromUnevenLoadDistributionStrength(true).Value;
            phiP_nSec = con.GetBranchYieldingFromUnevenLoadDistributionStrength(false).Value;

            IsApplicableMain = con.GetBranchYieldingFromUnevenLoadDistributionStrength(true).IsApplicable;
            IsApplicableSecn = con.GetBranchYieldingFromUnevenLoadDistributionStrength(false).IsApplicable;

            return new Dictionary<string, object>
            {
                { "phiP_nMain", phiP_nMain }
                ,{ "phiP_nSec", phiP_nSec }
                ,{ "IsApplicableMain", IsApplicableMain }
                ,{ "IsApplicableSecn", IsApplicableSecn }
              };
        }

    }
}


