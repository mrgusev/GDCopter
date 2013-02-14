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
    /// Interaction logic for TerminalView.xaml
    /// </summary>
    public partial class TerminalView : UserControl
    {
        public TerminalView()
        {
            InitializeComponent();
            DataContextChanged += TerminalViewDataContextChanged;
            recieveTextBox.TextChanged += RecieveTextBoxTextChanged;
        }

        private void RecieveTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            recieveTextBox.ScrollToEnd();
        }

        private void TerminalViewDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var terminalModel = (TerminalModel)DataContext;
            terminalModel.PropertyChanged += TerminalModelPropertyChanged;
        }

        private void TerminalModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "IsRunning")
            {
                stateButton.Content = ((TerminalModel)DataContext).IsRunning ? "Stop" : "Run";
            }
            //recieveTextBox.ScrollToEnd();
        }

        private void SendButtonClick(object sender, RoutedEventArgs e)
        {
            var terminalModel = (TerminalModel)DataContext;
            terminalModel.Message = inputTextBox.Text;
            inputTextBox.Text = "";
            sendTextBox.ScrollToEnd();
        }

        private void RunButtonClick(object sender, RoutedEventArgs e)
        {
            var terminalModel = (TerminalModel)DataContext;
            terminalModel.IsRunning = !terminalModel.IsRunning;
        }

        private void InputTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                SendButtonClick(null,null);
            }
        }
    }
}
