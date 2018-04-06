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

        // This will be used for testing purposes.
        private void TRClose_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(this.Parent);
        }

        // Used to load the Transaction table data
        private void TransactionsView_Load(object sender, EventArgs e)
        {

        }


    }
}
