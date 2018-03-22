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
using System.Windows.Shapes;

namespace bestsixapp
{
    /// <summary>
    /// Interaction logic for TransactionsView.xaml
    /// 
    /// </summary>
    public partial class TransactionsView : Window
    {
        public TransactionsView()
        {
            InitializeComponent();
        }

        // This will be used for testing purposes.
        private void InsertTR_Click(object sender, RoutedEventArgs e)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                dbContext.Transactions.Add(new Transactions
                {   // Add Transaction info into customer table
                    ID = "343321",
                    Customer = null,
                    RoomNo = '5',
                    Checkin = new DateTime(),
                    Checkout = new DateTime(),
                    DateModified = new DateTime(),
                });

                dbContext.SaveChanges();
                Close();
            }
        }

        private void TransactionsView_Load(object sender, EventArgs e)
        {

        }


    }
}
