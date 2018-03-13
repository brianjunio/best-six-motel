using Database;
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

namespace bestsixapp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Creates a Frame For Pages
        public object Frame { get; private set; }
        public static object NavigationService { get; internal set; }

        public MainWindow()
        {
            InitializeComponent();
            this.Closed += new EventHandler(MainWindow_Closed);
        }


        // Goes to Make Rooms Editor in NEW WINDOW
        private void RoomClick(object sender, RoutedEventArgs e)
        {
            RoomMake roomWindow = new RoomMake();
            roomWindow.ShowDialog();
            roomWindow.Close();
        }
        
        // Goes to Customer Check in window
        private void CustomerClick(object sender, RoutedEventArgs e)
        {
            Check checkWindow = new Check();
            checkWindow.ShowDialog();
        }

        // Goes to Customer Check in Page
        private void CheckinPageClick(object sender, RoutedEventArgs e) {
            //NavigationService.Navigate(new Uri("CheckinPage.xaml", UriKind.RelativeOrAbsolute));
            CheckinPage cp = new CheckinPage();
            Main.NavigationService.Navigate(new Uri("CheckinPage.xaml", UriKind.RelativeOrAbsolute));   
        }


        // Closes the application
        protected void MainWindow_Closed(object sender, EventArgs args)
        {
            App.Current.Shutdown();
        }
        private void CheckoutWindowClick(object sender, RoutedEventArgs e)
        {
            CheckoutWindow cw = new CheckoutWindow();
            cw.ShowDialog();
        }

        private void Main_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }

}

