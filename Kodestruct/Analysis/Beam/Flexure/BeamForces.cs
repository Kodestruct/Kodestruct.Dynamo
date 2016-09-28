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
using Kodestruct.Analysis;
using wa = Kodestruct.Analysis;

#endregion

namespace Analysis.Beam
{

/// <summary>
///     Beam forces
///     Category:   Analysis.Beam.Flexure
/// </summary>
/// 


    public partial class Flexure 
    {
        /// <summary>
        ///    Calculates Calculation of beam forces
        /// </summary>
        /// <param name="BeamForcesCaseId">  Case ID used in calculation of the beam forces </param>
        /// <param name="L">  member span length </param>
        /// <param name="X">  Distance from left support </param>
        /// <param name="P">  Concentrated load in beam, or axial load in compression member </param>
        /// <param name="M">  Concentrated moment </param>
        /// <param name="w">  Uniformly distributed load </param>
        /// <param name="a_load">  Load offset dimension </param>
        /// <param name="b_load">  Load offset dimension </param>
        /// <param name="c_load">  Load offset dimension </param>
        /// <param name="P1">  Concentrated load 1, when multiple point loads are present </param>
        /// <param name="P2">  Concentrated load 2, when multiple point loads are present </param>
        /// <param name="M1">  Concentrated moment 1, when multiple point moments are applied </param>
        /// <param name="M2">  Concentrated moment 2, when multiple point moments are applied </param>
        /// <returns name="M_max"> Maximum positive moment </returns>
        /// <returns name="M_min"> Maximum negative moment </returns>
        /// <returns name="V_max"> Maximum shear (absolute value) </returns>
        /// <returns name="M_x"> Moment at location X </returns>
        /// <returns name="V_x"> Shear at location X </returns>

        [MultiReturn(new[] { "M_max","M_min","V_max","M_x","V_x" })]
        public static Dictionary<string, object> BeamForces(string BeamForcesCaseId, double L,double X=0,double P=0,double M=0,double w=0,double a_load=0,double b_load=0,double c_load=0,double P1=0,double P2=0,double M1=0,double M2=0)
        {
            //Default values
            double M_max = 0;
            double M_min = 0;
            double V_max = 0;
            double M_x = 0;
            double V_x = 0;


            //Calculation logic:


            BeamFactoryData dat = new BeamFactoryData(L,P,M,w,a_load,b_load,c_load,P1,P2,M1,M2);
            BeamLoadFactoryLocator loc = new BeamLoadFactoryLocator();
            IBeamLoadFactory loadFactory = loc.GetLoadFactory(BeamForcesCaseId, dat);
            LoadBeam load = loadFactory.GetLoad(BeamForcesCaseId);
            BeamInstanceFactory beamFactory = new BeamInstanceFactory(dat);
            IAnalysisBeam bm = beamFactory.CreateBeamInstance(BeamForcesCaseId, load, null);


            M_x = bm.GetMoment(X);
            V_x = bm.GetShear(X);
            M_max = bm.GetMomentMaximum().Value;
            M_min = bm.GetMomentMinimum().Value;
            V_max = bm.GetShearMaximumValue().Value;

            return new Dictionary<string, object>
            {
                { "M_max", M_max }
                ,{ "M_min", M_min }
                ,{ "V_max", V_max }
                ,{ "M_x", M_x }
                ,{ "V_x", V_x }
 
            };
        }



    }
}


