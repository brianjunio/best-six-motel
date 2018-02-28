using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
// Tony says we dont need the un-highlighted ones
// using System.Windows.Controls;
// using System.Windows.Data;
// using System.Windows.Documents;
// using System.Windows.Input;
// using System.Windows.Media;
// using System.Windows.Media.Imaging;
// using System.Windows.Navigation;
// using System.Windows.Shapes;

namespace bestsixapp
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Check : Window
    {
        public Check()
        {
            InitializeComponent();

            // RefreshList();  Update Room Page with Customer Information when seleceted
        }
        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                dbContext.Customers.Add(new Customer
                {   // Add Customer info into customer table
                    FirstName = TBFName.Text, LastName = TBLName.Text, ID = Int32.Parse(TBID.Text),
                    PhoneNo = TBPhone.Text,   Street = TBStreet.Text, City = TBCity.Text,
                    State = TBState.Text, Zip = TBZip.Text, PaymentInfo = TBPayment.Text }); 
                dbContext.SaveChanges();

                Close();
                // RefreshList();  Update Room Page with Customer Information when seleceted

            }
        }
        //method for cancel
        public void ButtonCancel_Click(object sender, RoutedEventArgs e) { this.Close(); }
        
        //private void RefreshList()
        //{
        //    using (DatabaseContext dbContext = new DatabaseContext())
        //    {
        //        ListViewNames.ItemsSource = dbContext.Customers
        //            .OrderBy(m => m.FirstName)
        //            .Select(m => m.FirstName)
        //            .ToList();
        //    }
        //}
    }
}
