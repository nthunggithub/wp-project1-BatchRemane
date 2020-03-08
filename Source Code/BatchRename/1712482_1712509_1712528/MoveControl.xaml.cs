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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _1712482_1712509_1712528
{
    /// <summary>
    /// Interaction logic for MoveControl.xaml
    /// </summary>
    public partial class MoveControl : Window
    {
        MoveArgs MyArgs;

        public MoveControl(StringArgs args)
        {
            InitializeComponent();
            MyArgs = args as MoveArgs;
        }


        public abstract class DesMove
        {
            public abstract string value { get; }
            public abstract string Name { get; }
        }
        public class MoveArgBegin:DesMove
        {
            public override string value => "Begin";
            public override string Name => "Begin";
        }
        public class MoveArgEnd:DesMove
        {
            public override string value => "End";
            public override string Name => "End";
        }
        BindingList<DesMove> _Prop = new BindingList<DesMove>();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var p1 = new MoveArgBegin();
            var p2 = new MoveArgEnd();

            _Prop.Add(p1);
            _Prop.Add(p2);

            DesComboBox.ItemsSource = _Prop;
            
        }

        private void AddMoveAction_click(object sender, RoutedEventArgs e)
        {
            MyArgs.Start = StartIndex.Text;
            MyArgs.Length = Length.Text;
            var Item = DesComboBox.SelectedItem as DesMove;
            MyArgs.Des = Item.value;
            DialogResult = true;
            Close();
                
        }
    }
}
