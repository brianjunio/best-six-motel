﻿using Database;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

using System.Collections;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Windows.Input;

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
        Print p = new Print();
        public TransactionsView()
        {
            InitializeComponent();
            trDataGrid.ItemsSource = LoadTransactions();
        }

        /*
         *  Transforms transaction table into a list. 
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
                // Look for values that match the searched date range.
                var trDateRangeList = dbContext.Transactions.Where(x => (x.Room.Checkin >= StartDate && x.Room.Checkin <= EndDate)
                                                                              || (x.Room.Checkout >= StartDate && x.Room.Checkout <= EndDate));
                var result = trDateRangeList.ToList();
                if (!result.Any())
                {
                    System.Windows.Forms.MessageBox.Show("There are no records for that date range.");
                    return null;
                }
                else
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
                // Look for values that match the searched driver's license ID.
                var trLicenseList = dbContext.Transactions.Where(x => x.ID == CustomerLicense.Text);
                var result = trLicenseList.ToList();
                if (!result.Any())
                {
                    System.Windows.Forms.MessageBox.Show("There are no records for that License.");
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

        private void Print_Customer_click(object sender, RoutedEventArgs e)
        {
            // Error handling for not selecting a row and clicking the print button.
            if (trDataGrid.SelectedIndex != -1)
            {
                var cells = trDataGrid.SelectedItems;
                p.PopulateTextboxes(cells);



                // var item1 = cells[0].GetType().GetProperty().GetValue();
                // p.FnameBox.Text = item1;
                this.NavigationService.Navigate(p);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Please select a record (row) to be printed before proceeding.");
            }
        }
    }
}