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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class CheckinPage : Page
    {
        public CheckinPage()
        {
            InitializeComponent();
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            //try{
                // Create Database Connection
                using (DatabaseContext dbContext = new DatabaseContext())
                {
                    // Add Customer info into customer table
                    dbContext.Customers.Add(new Customer
                    {

                        FirstName = TBFName.Text,
                        LastName = TBLName.Text,
                        ID = Int32.Parse(TBID.Text),
                        PhoneNo = TBPhone.Text,
                        Street = TBStreet.Text,
                        City = TBCity.Text,
                        State = TBState.Text,
                        Zip = TBZip.Text,
                        PaymentInfo = TBPayment.Text
                    });
                    dbContext.SaveChanges();

                }
                // Navigate Back to Home Page after Customer is added
                this.NavigationService.Navigate(this.Parent);

                // RefreshList();  Update Room Page with Customer Information when seleceted
            //}catch (Exception) { }
        }

        // Cancel Button navigates back to MainWindow or "Parent Window"
        public void ButtonCancel_Click(object sender, RoutedEventArgs e) { this.NavigationService.Navigate(this.Parent); }
    }
}
