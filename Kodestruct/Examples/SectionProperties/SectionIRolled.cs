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
using Dynamo.Nodes;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Kodestruct.Loads.ASCE.ASCE7_10.DeadLoads;
using Kodestruct.Loads.ASCE.ASCE7_10.LiveLoads;


#endregion



//namespace SectionProperties
//{

//    [IsDesignScriptCompatible]
//    public class SectionIRolled
//    {

//        [MultiReturn(new[] { "Ix", "Iy" })]
//        public static Dictionary<string, object> SectionIRolledMomentsOfInertia(double depth,
//            double flangeWidth, double flangeThickness, double webThickness,
//            double filletDistance)
//        {

//            using (var client = new HttpClient())
//            {
//                // TODO: use piblic url instead
//                client.BaseAddress = new Uri("http://localhost:2129/api/");
//                client.DefaultRequestHeaders.Accept.Clear();
//                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

//                var resource = string.Format("massprop/irolled/depth/{0}/flangeWidth/{1}/flangeThickness/{2}/webThickness/{3}/filletDistance/{4}/full",
//                    depth, flangeWidth, flangeThickness, webThickness, filletDistance);
//                // New code:
//                HttpResponseMessage response = client.GetAsync(resource).Result;
//                if (response.IsSuccessStatusCode)
//                {
//                    var massProp = response.Content.ReadAsAsync<MassPropertiesModel>().Result;

//                    return new Dictionary<string, object>
//                    {
//                        { "Ix", massProp.MomentOfInertiaX },
//                        { "Iy", massProp.MomentOfInertiaY }
//                    };
//                }
//            }

//            return new Dictionary<string, object>
//            {
//                { "Ix", 0.0 },
//                { "Iy", 0.0 }
//            };

//        }

//    }
//}


