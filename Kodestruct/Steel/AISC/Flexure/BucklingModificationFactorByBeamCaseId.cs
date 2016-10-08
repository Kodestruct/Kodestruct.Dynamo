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
//using Kodestruct.Analysis;
//using System;
//using System.Linq;
//using Kodestruct.Steel.AISC.AISC360v10.Flexure;
//using Kodestruct.Common.CalculationLogger;

//#endregion

//namespace Steel.AISC10
//{

///// <summary>
/////     Buckling modification factor by Beam Case Id
/////     Category:   Steel.AISC10.Flexure
///// </summary>
///// 



//    public partial class Flexure 
//    {
//        /// <summary>
//        ///     Buckling modification factor. 
//        /// </summary>
//        /// <param name="BeamForcesCaseId">  Case ID used in calculation of the beam forces </param>
//        /// <param name="L">  member span length </param>
//        /// <param name="X_unbracedStart">  Distance from left support, identifying the beginning of unbraced length segment </param>
//        /// <param name="X_unbracedEnd">  Distance from left support, identifying the end of unbraced length segment </param>
//        /// <param name="P">  Concentrated load in beam, or axial load in compression member </param>
//        /// <param name="M">  Concentrated moment </param>
//        /// <param name="w">  Uniformly distributed load </param>
//        /// <param name="a_load">  Load offset dimension </param>
//        /// <param name="b_load">  Load offset dimension </param>
//        /// <param name="c_load">  Load offset dimension </param>
//        /// <param name="P1">  Concentrated load 1, when multiple point loads are present </param>
//        /// <param name="P2">  Concentrated load 2, when multiple point loads are present </param>
//        /// <param name="M1">  Concentrated moment 1, when multiple point moments are applied </param>
//        /// <param name="M2">  Concentrated moment 2, when multiple point moments are applied </param>
//        /// <returns name="C_b"> Lateral-torsional buckling modification factor for nonuniform moment diagrams  </returns>

//        [MultiReturn(new[] { "C_b" })]
//        public static Dictionary<string, object> BucklingModificationFactorByBeamCaseId(string BeamForcesCaseId, double L, double X_unbracedStart, double X_unbracedEnd, double P=0,double M=0,double w=0,double a_load=0,double b_load=0,double c_load=0,double P1=0,double P2=0,double M1=0,double M2=0)
//        {
//            //Default values
//            double C_b = 0;


//            ////Calculation logic:
//            //BeamFactoryData dat = new BeamFactoryData(L, P, M, w, a_load, b_load, c_load, P1, P2, M1, M2);
//            //BeamLoadFactoryLocator loc = new BeamLoadFactoryLocator();
//            //IBeamLoadFactory loadFactory = loc.GetLoadFactory(BeamForcesCaseId, dat);
//            //LoadBeam load = loadFactory.GetLoad(BeamForcesCaseId);
//            //BeamInstanceFactory beamFactory = new BeamInstanceFactory(dat);
//            //IAnalysisBeam beam = beamFactory.CreateBeamInstance(BeamForcesCaseId, load, null);

//            //double M_max = GetMomentMaximumInSegment(beam, X_unbracedStart, X_unbracedEnd);
//            //double M_min = GetMomentMinimumInSegment(beam, X_unbracedStart, X_unbracedEnd);

//            ////Mmax = absolute value of maximum moment in the unbraced segment
//            ////MA = absolute value of moment at quarter point of the unbraced segment,
//            ////MB = absolute value of moment at centerline of the unbraced segment
//            ////MC = absolute value of moment at three-quarter point of the unbraced segment

//            //double M_maxAbs= Math.Max(Math.Abs(M_max), Math.Abs(M_max));
//            //double M_A = Math.Abs(beam.GetMoment((X_unbracedEnd - X_unbracedStart) * 0.25 + X_unbracedStart));
//            //double M_B = Math.Abs(beam.GetMoment((X_unbracedEnd - X_unbracedStart) * 0.5 + X_unbracedStart));
//            //double M_C = Math.Abs(beam.GetMoment((X_unbracedEnd - X_unbracedStart) * 0.75 + X_unbracedStart));

            
//            //GeneralFlexuralMember fm = new GeneralFlexuralMember(new CalcLog());
//            //C_b = fm.GetCb(M_maxAbs, M_A, M_B, M_C);

//            BeamFactoryData dat = new BeamFactoryData(L, P, M, w, a_load, b_load, c_load, P1, P2, M1, M2);
//            BeamLoadFactoryLocator loc = new BeamLoadFactoryLocator();
//            IBeamLoadFactory loadFactory = loc.GetLoadFactory(BeamForcesCaseId, dat);
//            LoadBeam load = loadFactory.GetLoad(BeamForcesCaseId);
//            BeamInstanceFactory beamFactory = new BeamInstanceFactory(dat);
//            IAnalysisBeam beam = beamFactory.CreateBeamInstance(BeamForcesCaseId, load, null);

//            double M_maxSeg = beam.GetMaxMomentBetweenPoints(X_unbracedStart,X_unbracedEnd,20);
//            double M_minSeg = beam.GetMinMomentBetweenPoints(X_unbracedStart, X_unbracedEnd,20);
//            ////Mmax = absolute value of maximum moment in the unbraced segment
//            ////MA = absolute value of moment at quarter point of the unbraced segment,
//            ////MB = absolute value of moment at centerline of the unbraced segment
//            ////MC = absolute value of moment at three-quarter point of the unbraced segment

//            double M_maxAbs = Math.Max(Math.Abs(M_maxSeg), Math.Abs(M_minSeg));
//            double M_A = Math.Abs(beam.GetMoment((X_unbracedEnd - X_unbracedStart) * 0.25 + X_unbracedStart));
//            double M_B = Math.Abs(beam.GetMoment((X_unbracedEnd - X_unbracedStart) * 0.5 + X_unbracedStart));
//            double M_C = Math.Abs(beam.GetMoment((X_unbracedEnd - X_unbracedStart) * 0.75 + X_unbracedStart));

//            GeneralFlexuralMember fm = new GeneralFlexuralMember(new CalcLog());
//            C_b = fm.GetCb(M_maxAbs, M_A, M_B, M_C);

//            //double segLen = X_unbracedEnd - X_unbracedStart;
//            //double segStep = segLen / 20;
//            //List<double> Ms = new List<double>();

//            //for (int i = 0; i <= 20; i++)
//            //{
//            //    double X_pt = segStep * i + X_unbracedStart;
//            //    double M_x = bm.GetMoment(X_pt);
//            //    Ms.Add(M_x);
//            //}
//            //var M_max = Ms.Max();
//            //var M_min = Ms.Min();


//            //double M_x = bm.GetMoment(X_unbracedEnd);
           
//            C_b = 0;

//            return new Dictionary<string, object>
//            {
//                { "C_b", C_b }
 
//            };
//        }



//    }
//}


