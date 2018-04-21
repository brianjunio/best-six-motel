using System.Windows;


namespace bestsixapp
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
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
            //rmw.NavigationService.Navigate(new Uri("TransactionView.xaml", UriKind.RelativeOrAbsolute));
        }
        private void CompanyButton_Click(object sender, RoutedEventArgs e)
        {
            CompanyInfo cm = new CompanyInfo();
            cm.Show();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void EmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            EmployeeList em = new EmployeeList();
            em.Show();
        }
    }

}
