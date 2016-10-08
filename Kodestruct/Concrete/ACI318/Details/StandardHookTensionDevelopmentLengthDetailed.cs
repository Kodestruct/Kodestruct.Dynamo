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
using Concrete.ACI318.General.Reinforcement;
using Kodestruct.Concrete.ACI318_14;
using Kodestruct.Concrete.ACI;
using Kodestruct.Common.CalculationLogger;
using System;

#endregion

namespace Concrete.ACI318.Details
{

/// <summary>
///     Headed bar tension development length (Basic)
///     Category:   Concrete.ACI318.Details
/// </summary>
/// 


    public partial class DevelopmentLength
    {
        /// <summary>
        ///     Standard hook tension development length (Basic)
        /// </summary>
        /// <param name="ConcreteMaterial">  Concrete material object used to extract material properties, create the object using input parameters first </param>
        /// <param name="d_b">   Nominal diameter of bar, wire, or prestressing  strand  </param>
        /// <param name="RebarMaterial">   Reinforcement material </param>
        /// <param name="HookType">  Identifies rebar hook configuration (90-degree versus 180-degree) </param>
        /// <param name="RebarCoatingType">  Type of rebar surface coating (epoxy coated or black) </param>
        /// <param name="ExcessRebarRatio">  Indicates the ration of areas of required reinforcement and provided renforcement. This value must be less than 1 </param>
        /// <param name="c_side">  Reinforcement side clear cover </param>
        /// <param name="c_extension">  Reinforcement standard hook clear cover for bar extension </param>
        /// <param name="EnclosingRebarDirection">  Indicates if enclosing reinforcement is perpendicular or parallel to bar </param>
        /// <param name="s_enclosing">Spacing of enclosing reinforcement</param>
        /// <param name="Code"> Applicable version of code/standard</param>
        /// <returns name="l_dh">  Development length in tension of deformed bar or  deformed wire with a standard hook, measured  from outside end of hook, point of tangency, toward  critical section  </returns>

        [MultiReturn(new[] { "l_dh" })]
        public static Dictionary<string, object> StandardHookTensionDevelopmentLengthDetailed(Concrete.ACI318.General.Concrete.ConcreteMaterial ConcreteMaterial, double d_b,
            RebarMaterial RebarMaterial, string HookType, string RebarCoatingType, double ExcessRebarRatio, double c_side, double c_extension, string EnclosingRebarDirection, 
            double s_enclosing, string Code = "ACI318-14")
        {
            //Default values
            double l_dh = 0;
                                                                                                                                                                

            //Calculation logic:

            IRebarMaterial mat = RebarMaterial.Material;

            bool IsEpoxyCoated = true;

            if (RebarCoatingType.ToLower() == "uncoated")
            {
                IsEpoxyCoated=false;
            }
            else if (RebarCoatingType.ToLower() == "epoxycoated")
            {
                IsEpoxyCoated =true;
            }
            else
            {
                throw new Exception("Unrecognized rebar coating string.");
            }

            Rebar rebar = new Rebar(d_b, IsEpoxyCoated, mat);

            CalcLog log = new CalcLog();

            StandardHookInTension hook = new StandardHookInTension(ConcreteMaterial.Concrete, rebar, log, ExcessRebarRatio);
            
            HookType _HookType;
            bool IsValidHookTypeString = Enum.TryParse(HookType, true, out _HookType);
            if (IsValidHookTypeString == false)
            {
                throw new Exception("Failed to convert string. Check HookType string. Please check input");
            }


            bool enclosingRebarIsPerpendicular = false;
            if (EnclosingRebarDirection.ToLower()=="perpendicular")
            {
                enclosingRebarIsPerpendicular = true;
            }
            else if (EnclosingRebarDirection.ToLower() == "parallel")
            {
                enclosingRebarIsPerpendicular = false;
            }
            else
	        {
                throw new Exception("Failed to convert string. Check EnclosingRebarDirection string. Please check input");
	        }


            l_dh = hook.GetDevelopmentLength(_HookType, c_side, c_extension, enclosingRebarIsPerpendicular,s_enclosing);

            return new Dictionary<string, object>
            {
                { "l_dh", l_dh }
 
            };
        }



    }
}


