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
using Kodestruct.Concrete.ACI318_14;
using Kodestruct.Common.CalculationLogger;
using Kodestruct.Common.Section.Interfaces;
using System;
using Dynamo.Graph.Nodes;

#endregion

namespace Concrete.ACI318.General.Reinforcement
{

/// <summary>
///     Effective depth to longitudinal rebar
///     Category:   Concrete.ACI318_14.General.Rebar
/// </summary>
/// 


    //[IsDesignScriptCompatible]
    //public partial class Rebar 
    //{
    //    /// <summary>
    //    ///     Effective depth to longitudinal rebar
    //    /// </summary>
    //    /// <param name="ConcreteSection">  Reinforced concrete section </param>
    //    /// <param name="FlexuralCompressionFiberLocation">  Indicates whether the section in flexure has top or bottom in compression due to stresses from bending moment </param>
    //    /// <param name="c">   Distance from extreme compression fiber to neutral  axis  </param>
    //    /// <returns name="d">  Distance from extreme compression fiber to centroid  of longitudinal tension reinforcement  </returns>

    //    [MultiReturn(new[] { "d" })]
    //    public static Dictionary<string, object> EffectiveDepthToLongitudinalRebar(ConcreteSection ConcreteSection,
    //        string FlexuralCompressionFiberLocation,double c=0)
    //    {
    //        //Default values
    //        double d = 0;


    //        //Calculation logic:

    //        FlexuralCompressionFiberPosition p;
    //        bool IsValidStringFiber = Enum.TryParse(FlexuralCompressionFiberLocation, true, out p);
    //        if (IsValidStringFiber == false)
    //        {
    //        throw new Exception("Flexural compression fiber location is not recognized. Check input.");
    //        }
            
    //        CalcLog log = new CalcLog ();
    //        ConcreteSectionFlexure longitudinallyReinforcedSection = new ConcreteSectionFlexure(ConcreteSection.Section,
    //            ConcreteSection.LongitudinalBars, log);
    //        double h = ConcreteSection.Section.SliceableShape.YMax - ConcreteSection.Section.SliceableShape.YMin;
    //        d = longitudinallyReinforcedSection.Get_d(c, h, p);

    //        return new Dictionary<string, object>
    //        {
    //            { "d", d }
 
    //        };
    //    }


    //    //internal Rebar (ConcreteSection ConcreteSection,string FlexuralCompressionFiberLocation,double c)
    //    //{

    //    //}
    //    //[IsVisibleInDynamoLibrary(false)]
    //    //public static Rebar  ByInputParameters(ConcreteSection ConcreteSection,string FlexuralCompressionFiberLocation,double c)
    //    //{
    //    //    return new Rebar(ConcreteSection ,FlexuralCompressionFiberLocation ,c );
    //    //}

    //}
}


