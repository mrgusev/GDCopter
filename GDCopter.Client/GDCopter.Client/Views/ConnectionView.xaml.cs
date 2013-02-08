using System;
using System.Collections.Generic;
using System.IO.Ports;
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
    /// Interaction logic for ConnectionView.xaml
    /// </summary>
    public partial class ConnectionView : UserControl
    {
        public ConnectionView()
        {
            InitializeComponent();
            foreach (var item in SerialPort.GetPortNames())
            {
                portsComboBox.Items.Add(item);
            }
            baudrateComboBox.Items.Add(4800);
            baudrateComboBox.Items.Add(9600);
            baudrateComboBox.Items.Add(14400);
            baudrateComboBox.Items.Add(19200);
            baudrateComboBox.Items.Add(38400);
            baudrateComboBox.Items.Add(57600);
            baudrateComboBox.Items.Add(115200);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var model = (ConnectionModel) DataContext;
            model.IsOpen = !model.IsOpen;
        }
    }
}
