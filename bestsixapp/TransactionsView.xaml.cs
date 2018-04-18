using Database;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

using System.Collections;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace bestsixapp
{
    /// <summary>
    /// Interaction logic for TransactionsView.xaml
    /// 
    /// </summary>
    public partial class TransactionsView : Page
    {
        DateTime StartDate, EndDate;
        Room roomData = new Room();
        Customer custData = new Customer();
        Transactions tr = new Transactions();

        public TransactionsView()
        {
            InitializeComponent();
            // Default list of all Transactions on the table.
            trDataGrid.ItemsSource = LoadTransactions();
        }

        /*
         *  Default: Transforms transaction table into a list. 
         *  List of Transactions become the data source of the data grid.
         */
        private List<Transactions> LoadTransactions()
        {
            List<Transactions> transactionList = new List<Transactions>();

            using (DatabaseContext dbContext = new DatabaseContext())
            {
                    transactionList = dbContext.Transactions.ToList();
            }
            return transactionList;
        }

        /*
         * Transforms transaction table into a list. 
         * Date Range choices of Transactions becomes the search key for source of the data grid.
         */
        private List<Transactions> LoadDateRangeTransactions()
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var trDateRangeList = dbContext.Transactions.ToList().Where(x => (x.Room.Checkin >= StartDate && x.Room.Checkin <= EndDate) 
                                                                              || (x.Room.Checkout >= StartDate && x.Room.Checkout <= EndDate));
                var result = trDateRangeList.ToList();
                if (!result.Any())
                {
                    System.Windows.Forms.MessageBox.Show("There were no records for the provided date range.");
                    return null;
                } else
                {
                    return result;
                }
            }
        }

        /*
         * Transforms transaction table into a list. 
         * License of customer in Transactions become the search key for source of the data grid. 
         */
        private List<Transactions> LoadLicenseTransactions()
        {
            //List<Transactions> trLicenseList = new List<Transactions>();

            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var trLicenseList = dbContext.Transactions.ToList().Where(x => x.ID == int.Parse(CustomerLicense.Text));
                var result = trLicenseList.ToList();
                if (!result.Any())
                {
                    System.Windows.Forms.MessageBox.Show("There were no records for the provided Driver's License.");
                    return null;
                }
                else
                {
                    return result;
                }
            }
        }

        // Grab datepicker StartDate's value and make it a DateTime Obj.
        private void StartDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            StartDate = (DateTime)Start.SelectedDate;
            startingDate.Text = StartDate.ToLongDateString().ToString();
            startingDate.IsEnabled = false;
        }
        // Grab datepicker EndDate's value and make it a DateTime Obj.
        private void EndDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            EndDate = (DateTime)End.SelectedDate;
            endingDate.Text = EndDate.ToLongDateString().ToString();
            endingDate.IsEnabled = false;
        }

        /*
         * Load Date Range button functionality.
         */

        public void Load_button_click(object sender, RoutedEventArgs e)
        {
            trDataGrid.ItemsSource = LoadDateRangeTransactions();
        }

        /*
         * Search Driver's License button functionality.
         */

        public void Search_button_click(object sender, RoutedEventArgs e)
        {
            trDataGrid.ItemsSource = LoadLicenseTransactions();
        }

        // Closes window back to the 'Parent' page which is home.
        private void TRClose_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(this.Parent);
        }
    }
}
