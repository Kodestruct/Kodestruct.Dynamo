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

#endregion

namespace Concrete.ACI318_14.Section.Flexure
{

/// <summary>
///     Cracking moment
///     Category:   Concrete.ACI318_14.Section.Flexure
/// </summary>
/// 


    [IsDesignScriptCompatible]
    public partial class Flexure 
    {
/// <summary>
///     Cracking moment
/// </summary>
        /// <param name="f_r">   Modulus of rupture of concrete  </param>
/// <param name="S">   Effect of service snow load </param>

        /// <returns name="M_cr">  Cracking moment   </returns>

        [MultiReturn(new[] { "M_cr" })]
        public static Dictionary<string, object> CrackingMoment(double f_r,double S)
        {
            //Default values
            double M_cr = 0;


            //Calculation logic:


            return new Dictionary<string, object>
            {
                { "M_cr", M_cr }
 
            };
        }


        //internal Flexure (double f_r,double S)
        //{

        //}
        //[IsVisibleInDynamoLibrary(false)]
        //public static Flexure  ByInputParameters(double f_r,double S)
        //{
        //    return new Flexure(f_r ,S );
        //}

    }
}


