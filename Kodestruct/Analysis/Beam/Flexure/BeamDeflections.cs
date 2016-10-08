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
using Kodestruct.Analysis;
using wa = Kodestruct.Analysis;

#endregion

namespace Analysis.Beam
{

/// <summary>
///     Beam deflections
///     Category:   Analysis.Beam.Flexure
/// </summary>
/// 


    public partial class Flexure 
    {
        /// <summary>
        ///    Calculates Calculation of beam deflections
        /// </summary>
        ///  <param name="BeamDeflectionCaseId"> Case ID used in calculation of the beam deflection</param>
        /// <param name="L">  member span length </param>
        /// <param name="P">  Concentrated load in beam, or axial load in compression member </param>
        /// <param name="M">  Concentrated moment </param>
        /// <param name="w">  Uniformly distributed load </param>
        /// <param name="E">  Modulus of elasticity of steel </param>
        /// <param name="I">  Moment of inertia (I_x or I_y where applicable) </param>
        /// <param name="a_load">  Load offset dimension </param>
        /// <param name="b_load">  Load offset dimension </param>
        /// <param name="c_load">  Load offset dimension </param>
        /// <param name="P1">  Concentrated load 1, when multiple point loads are present </param>
        /// <param name="P2">  Concentrated load 2, when multiple point loads are present </param>
        /// <param name="M1">  Concentrated moment 1, when multiple point moments are applied </param>
        /// <param name="M2">  Concentrated moment 2, when multiple point moments are applied </param>
        /// <returns name="Delta_max"> Maximum deflection </returns>

        [MultiReturn(new[] { "Delta_max" })]
        public static Dictionary<string, object> BeamDeflections(string BeamDeflectionCaseId,double L,  double P, double M, double w, double E, double I, double a_load = 0, double b_load = 0, double c_load = 0, double P1 = 0, double P2 = 0, double M1 = 0, double M2 = 0)
        {
            //Default values
            double Delta_max = 0;


            //Calculation logic:
            BeamFactoryData dat = new BeamFactoryData(L, P, M, w, a_load, b_load, c_load, P1, P2, M1, M2,E,I);
            BeamLoadFactoryLocator loc = new BeamLoadFactoryLocator();
            IBeamLoadFactory loadFactory = loc.GetLoadFactory(BeamDeflectionCaseId, dat);
            LoadBeam load = loadFactory.GetLoad(BeamDeflectionCaseId);
            BeamInstanceFactory beamFactory = new BeamInstanceFactory(dat);
            IAnalysisBeam bm = beamFactory.CreateBeamInstance(BeamDeflectionCaseId, load, null);

            Delta_max = bm.GetMaximumDeflection();

            return new Dictionary<string, object>
            {
                { "Delta_max", Delta_max }
 
            };
        }




    }
}


