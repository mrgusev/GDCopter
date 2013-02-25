using System;
using System.Collections.Generic;
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
using GDCopter.Client.Models;

namespace GDCopter.Client.Views
{
    /// <summary>
    /// Interaction logic for RotorsView.xaml
    /// </summary>
    public partial class RotorsView : UserControl
    {
        public RotorsView()
        {
            InitializeComponent();
        }

        private void StatusButtonClick(object sender, RoutedEventArgs e)
        {
            var model = ((ModelBase)DataContext);
            model.IsRunning = !model.IsRunning;
        }
    }
}
