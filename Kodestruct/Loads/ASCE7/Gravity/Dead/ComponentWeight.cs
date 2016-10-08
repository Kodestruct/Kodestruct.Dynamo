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
using Dynamo.Nodes;
using System.Collections.Generic;
using Kodestruct.Loads.ASCE.ASCE7_10.DeadLoads;
using Kodestruct.Loads.ASCE.ASCE7_10.LiveLoads;

#endregion

namespace Loads.ASCE7.Gravity
{
    /// <summary>
    ///     Component weight
    ///     Category:   Kodestruct.Loads.ASCE7-10.Gravity.Dead
    /// </summary>
    /// 


    public partial class Dead
    {
        /// <summary>
        ///    Calculates Building component weight per surface area of the component (PSF UNITS)
        /// </summary>
        /// <param name="ComponentId">  building component id (name) </param>
        /// <param name="ComponentOption1">  building component subtype (option1) </param>
        /// <param name="ComponentOption2">  building component subtype (option2) </param>
        /// <param name="ComponentValue">  building component numerical value</param>
        /// <returns> "Parameter name: q_D", Uniformly distributed component dead load </returns>
        
        [MultiReturn(new[] { "q_D" })]
        public static Dictionary<string, object> ComponentWeight(string ComponentId,
            double ComponentOption1, double ComponentOption2, double ComponentValue)
        {
            //Default values
            double q_D = 0;
            int Option1 = (int)ComponentOption1;
            int Option2 = (int)ComponentOption2;

            BuildingElementComponent bec = new BuildingElementComponent(ComponentId, Option1, Option2, ComponentValue, "");
            try
            {
                q_D = bec.GetComponentWeight().LoadValue;
            }
            catch (System.Exception)
            {


            }


            return new Dictionary<string, object>
            {
                { "q_D", q_D }
 
            };
        }



    }
}


