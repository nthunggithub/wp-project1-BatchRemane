using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _1712482_1712509_1712528
{
    public partial class NewCase : Window
    {
        NewCaseArgs myArgs;

        public NewCase(StringArgs args)
        {
            InitializeComponent();

            myArgs = args as NewCaseArgs;
            //fromTextBox.Text = myArgs.From;

            MainListView.ItemsSource = cases;
        }

        BindingList<MyString> cases = new BindingList<MyString>()
            {
                new MyString(){ Value = NewCaseStringValue.Lowercase },
                new MyString(){ Value = NewCaseStringValue.Uppercase },
                new MyString(){ Value = NewCaseStringValue.Capitalizedcase },
            };

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var selectedItem = MainListView.SelectedItem as MyString;

            myArgs.Value = selectedItem.Value;
            DialogResult = true;
            Close();

        }
    }
}
