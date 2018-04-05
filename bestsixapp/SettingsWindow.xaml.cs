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
using System.Windows.Shapes;

namespace bestsixapp
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        RoomMake rm;
        public SettingsWindow(RoomMake rm)
        {
            this.rm = rm;
            InitializeComponent();
        }

        public object Frame { get; private set; }
        public static object NavigationService { get; internal set; }

        private void RoomMakeButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void TransactionButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            TransactionsView tv = new TransactionsView();
            rm.NavigationService.Navigate(new Uri("TransactionView.xaml", UriKind.RelativeOrAbsolute));
        }
        private void OtherButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
