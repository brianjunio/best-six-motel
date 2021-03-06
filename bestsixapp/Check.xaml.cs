﻿using Database;
using System;
using System.Linq;
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
    public partial class Check : Page
    {
        string localID;
        DateTime checkin, checkout;
        RoomData room;
        Room roomQuery = new Room();
        Customer customerQuery = new Customer();
        Transactions transactionQuery = new Transactions();

        //int trValue = 1;


        public Check()
        {
            InitializeComponent();
            
        }

        public Check(RoomData room)
        {
            this.room = room;
            InitializeComponent();
            UpdateLabels();
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var customer = dbContext.Customers;
                customerQuery = customer.Find(TBID.Text);
                if(customerQuery == null)
                {
                    customer.Add(new Customer
                    {   
                        FirstName = TBFName.Text,
                        LastName = TBLName.Text,
                        ID = TBID.Text,
                        PhoneNo = TBPhone.Text,
                        Street = TBStreet.Text,
                        City = TBCity.Text,
                        State = CBState.Text,
                        Zip = TBZip.Text,
                        PaymentInfo = CBPayment.Text,
                        RoomNo = room.RoomNo,
                    });
                }

                else
                {
                    customerQuery.FirstName = TBFName.Text;
                    customerQuery.LastName = TBLName.Text;
                    customerQuery.PhoneNo = TBPhone.Text;
                    customerQuery.Street = TBStreet.Text;
                    customerQuery.City = TBCity.Text;
                    customerQuery.State = CBState.Text;
                    customerQuery.Zip = TBZip.Text;
                    customerQuery.PaymentInfo = CBPayment.Text;
                    customerQuery.RoomNo = room.RoomNo;
                }

                roomQuery = dbContext.Rooms.SingleOrDefault(rm => rm.RoomNo == room.RoomNo);
                if(roomQuery != null)
                {
                    roomQuery.Checkin = DateTime.Parse(TBcheckin.Text);
                    roomQuery.Checkout = DateTime.Parse(TBcheckout.Text);
                    roomQuery.Legend = "Occupied";
                    room.Legend = "Occupied";
                    room.Occupied();
                    InvalidateVisual();
                }

               
                dbContext.Transactions.Add(new Transactions
                {
                          
                    DateModified = DateTime.Today,
                    ID = TBID.Text,
                    RoomNo = room.RoomNo
                });
              
                
            
                dbContext.SaveChanges();
                localID = TBID.Text;
                this.NavigationService.Navigate(this.Parent);
                //Close();

            }
        }

        // Checkin and Checkout DatePicker event handlers
        private void CheckinDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // From the datepicker selected date turn into DateTime object
            checkin = (DateTime)checkinValue.SelectedDate;
            TBcheckin.Text = checkin.ToLongDateString().ToString();
            TBcheckin.IsEnabled = false;
        }

        private void CheckoutDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // From the datepicker selected date turn into DateTime object
            checkout = (DateTime)checkoutValue.SelectedDate;
            TBcheckout.Text = checkout.ToLongDateString().ToString();
            TBcheckout.IsEnabled = false;
        }

        /*
         *  Selects entity based on ID, then removes from database.
         *  Must add DataValidation.
         */

        public void ButtonCheckout_Click(object sender, RoutedEventArgs e)
        {
            checkout = DateTime.Now;
            using(DatabaseContext dbContext = new DatabaseContext())
            {
                // Code to Clear out Room Table for the 
                roomQuery = dbContext.Rooms.Find(room.RoomNo);
                // Code to Clear out Room Table for next customer to checkin
                roomQuery.Legend = "Dirty";
                room.Legend = "Dirty";
                room.NeedCleaning();
                roomQuery.Checkin = checkin; // shouldn't this be set back to null/default empty?

                //Customer Query
                customerQuery = dbContext.Customers.Find(TBID.Text);
                customerQuery.LastRoom = customerQuery.RoomNo.Value;
                customerQuery.RoomNo = null;

                transactionQuery = dbContext.Transactions.FirstOrDefault(t => t.RoomNo == room.RoomNo);
                if(transactionQuery != null)
                {
                    //transactionQuery.Checkout = checkout;
                    transactionQuery.DateModified = checkout;
                }

                dbContext.SaveChanges();
                this.NavigationService.Navigate(this.Parent);
                //Close();
            }
        }

        //Button to cancel, abort and go back to home menu
        public void ButtonCancel_Click(object sender, RoutedEventArgs e){ this.NavigationService.Navigate(this.Parent); }// this.Close(); }

        private void UpdateLabels()
        {
            using(DatabaseContext dbContext = new DatabaseContext())
            {

                roomQuery = dbContext.Rooms.Find(room.RoomNo);
                roomValue.Text = roomQuery.RoomNo.ToString();
                bedValue.Text = roomQuery.BedType;
                smokingValue.Text = roomQuery.Smoking;
                priceValue.Text = roomQuery.Price.ToString();

                roomValue.IsEnabled = false;
                bedValue.IsEnabled = false;
                smokingValue.IsEnabled = false;
                priceValue.IsEnabled = false;
                
            }
        }

        public void PopulateTextBoxes()
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                customerQuery = dbContext.Customers.SingleOrDefault(c => c.RoomNo == room.RoomNo);
                TBID.Text = customerQuery.ID.ToString();
                TBFName.Text = customerQuery.FirstName;
                TBLName.Text = customerQuery.LastName;
                TBPhone.Text = customerQuery.PhoneNo;
                TBStreet.Text = customerQuery.Street;
                TBCity.Text = customerQuery.City;
                CBState.Text = customerQuery.State;
                TBZip.Text = customerQuery.Zip;
                CBPayment.Text = customerQuery.PaymentInfo;
                TBcheckin.Text = roomQuery.Checkin.ToLongDateString().ToString(); 
                TBcheckout.Text = roomQuery.Checkout.ToLongDateString().ToString();
            }
        }

        public void DisableTextBoxes()
        {
            TBID.IsEnabled = false;
            TBFName.IsEnabled = false;
            TBLName.IsEnabled = false;
            TBPhone.IsEnabled = false;
            TBStreet.IsEnabled = false;
            TBCity.IsEnabled = false;
            CBState.IsEnabled = false;
            TBZip.IsEnabled = false;
            CBPayment.IsEnabled = false;
            TBcheckin.IsEnabled = false;
            TBcheckout.IsEnabled = false;
        }

      /*  public void CheckInEnable()
        {
            BTRegister.IsEnabled = true;
            BTCheckout.IsEnabled = false;
        }*/

     /*   public void CheckOutEnable()
        {
            BTRegister.IsEnabled = false;
            BTCheckout.IsEnabled = true;
        }*/
    }
}
    

