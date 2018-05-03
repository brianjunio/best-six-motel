using System.Windows;
using Database;
using System.Windows.Controls;
using System.Printing;          //Go to Project, Add Reference menu item. In Assemblies Choose System.Printing.
using System.Collections;
using System;

namespace bestsixapp

{

    /// <summary>

    /// Interaction logic for PrintReceipt.xaml

    /// </summary>

    public partial class Print : Page

    {

        public Print()

        {

            InitializeComponent();

        }

        private void PrintButtonClick(object sender, RoutedEventArgs e)

        {

            PrintDialog printDialog = new PrintDialog();
            printDialog.PrintTicket.PageOrientation = PageOrientation.Landscape;

            printDialog.PrintVisual(PrintPage, "Landscape Print");

        }

        public void PopulateTextboxes(IList selectedRow)
        {
            var transaction = (Transactions)selectedRow[0];
            string custID = transaction.ID.ToString();
            Customer c;

            using(DatabaseContext dbContext = new DatabaseContext())
            {
                c = dbContext.Customers.Find(custID);
                FnameBox.Text = c.FirstName;
                LnameBox.Text = c.LastName;
                PaymentTypeBox.Text = c.PaymentInfo;
                
            }

            using (DatabaseContext dbContext = new DatabaseContext())
            {
               var r = dbContext.Rooms.Find(transaction.RoomNo);
               PriceBox.Text = r.Price.ToString();
               CheckInBox.Text = r.Checkin.ToString();
               CheckOutBox.Text = r.Checkout.ToString();

            }
            RoomNumberBox.Text = transaction.RoomNo.ToString();
            
        }

    }

}