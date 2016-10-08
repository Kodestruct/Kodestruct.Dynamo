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
 
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


namespace Kodestruct.Dynamo.Common.Infra
{
    public partial class DataEntryElement : UserControl
    {
        public DataEntryElement()
        {
            InitializeComponent();
          
        }

        #region Description


        public string Description
        {
            get {
                return (string)GetValue(DescriptionProperty);
            }
            set 
            {
                SetValue(DescriptionProperty, value);
            }

        }


        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(DataEntryElement), new PropertyMetadata(""));

        #endregion


        #region DataPath

        private string dataPath;

        public string DataPath
        {
            get { return dataPath; }
            set { dataPath = value; }
        }
        

        public static readonly DependencyProperty DataPathProperty =
            DependencyProperty.Register("DataPath", typeof(string), typeof(DataEntryElement), new PropertyMetadata(dataPathChangedCallback));

        private static void dataPathChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SetNewBinding(e.NewValue, d);
        }

        private static void SetNewBinding(object args, DependencyObject dependencyObject)
        {
            string dp = args as string;
            if (dp != null)
            {

                
                Binding valBinding = new Binding();
                valBinding.Path = new PropertyPath(dp);
                valBinding.Mode = BindingMode.TwoWay;


                DataEntryElement ec = dependencyObject as DataEntryElement;
                if (ec!=null)
                {
                    ec.DataPath = dp;
                    ec.ValueBox.SetBinding(TextBox.TextProperty, valBinding);
                }
                
            }
        }




        #endregion

 

    }
}
