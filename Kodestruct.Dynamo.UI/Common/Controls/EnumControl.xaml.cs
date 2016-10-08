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
    public partial class EnumControl : UserControl
    {
        public EnumControl()
        {
            InitializeComponent();
          
        }

        #region SelectedItem



        public EnumDataElement SelectedItem
        {
            get { return (EnumDataElement)GetValue(SelectedItemProperty); }
            set { 
                SetValue(SelectedItemProperty, value); }
        }

      

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(EnumDataElement), typeof(EnumControl), new PropertyMetadata(OnSelectedItemChanged));

        private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
        }

         

        #endregion

        #region Icon width

        public string IconWidth
        {
            get { return (string)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }

        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(string), typeof(EnumControl), new PropertyMetadata(OnIconWidthPropertyChanged));

        private static void OnIconWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            EnumControl ec = d as EnumControl;
            if (ec != null)
            {
                var column1 = ec.LayoutRoot.ColumnDefinitions[0];
                try
                {
                    double colwidth = (double)e.NewValue;
                    column1.Width = new GridLength(colwidth);
                }
                catch (System.Exception)
                {


                }

            }
        } 
        #endregion

        #region Header text


        public string HeaderText
        {
            get { 
                return (string)GetValue(HeaderTextProperty); }
            set 
            { 
                SetValue(HeaderTextProperty, value);
            }

        }


        public static readonly DependencyProperty HeaderTextProperty =
            DependencyProperty.Register("HeaderText", typeof(string), typeof(EnumControl), new PropertyMetadata(""));

        #endregion

        #region Parameter path

        public string ParameterPath
        {
            get { return (string)GetValue(ParameterPathProperty); }
            set { SetValue(ParameterPathProperty, value); }
        }


        public static readonly DependencyProperty ParameterPathProperty =
            DependencyProperty.Register("ParameterPath", typeof(string), typeof(EnumControl), new PropertyMetadata(null));

        
        #endregion

        #region DataPath

        private string dataPath;

        public string DataPath
        {
            get { return dataPath; }
            set { dataPath = value; }
        }
        

        public static readonly DependencyProperty DataPathProperty =
            DependencyProperty.Register("DataPath", typeof(string), typeof(EnumControl), new PropertyMetadata( dataPathChangedCallback));

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
                

                EnumControl ec = dependencyObject as EnumControl;
                if (ec!=null)
                {
                    ec.dataPath = dp;
                    ec.EnumCombobox.SetBinding(ComboBox.SelectedValueProperty, valBinding);
                }
                
            }
        }




        #endregion

        #region ItemsSource



        public EnumDataCollection ItemsSource
        {
            get { return (EnumDataCollection)GetValue(ItemsSourceProperty); }
            set { 
                SetValue(ItemsSourceProperty, value);
            }
        }


        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(EnumDataCollection), typeof(EnumControl),new PropertyMetadata(null));

        
        #endregion

 

    }
}
