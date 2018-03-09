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
        int roomNum;
        string localID;
        Room roomQuery = new Room();
        Customer customerQuery = new Customer();

        public Check()
        {
            InitializeComponent();
            
            // RefreshList();  Update Room Page with Customer Information when seleceted
        }

        public Check(int roomNum)
        {
            this.roomNum = roomNum;
            InitializeComponent();
            UpdateLabels();
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                dbContext.Customers.Add(new Customer
                {   // Add Customer info into customer table
                    FirstName = TBFName.Text,
                    LastName = TBLName.Text,
                    ID = TBID.Text,
                    PhoneNo = TBPhone.Text,
                    Street = TBStreet.Text,
                    City = TBCity.Text,
                    State = TBState.Text,
                    Zip = TBZip.Text,
                    PaymentInfo = TBPayment.Text,
                    RoomNo = roomNum
                });

                roomQuery = dbContext.Rooms.SingleOrDefault(rm => rm.RoomNo == roomNum);
                if(roomQuery != null)
                {
                    roomQuery.Checkin = DateTime.Today;
                }
                
                
                dbContext.SaveChanges();
                localID = TBID.Text;
                Close();

            }
        }
        //method for cancel
        public void ButtonCancel_Click(object sender, RoutedEventArgs e) { this.Close(); }

        private void UpdateLabels()
        {
            using(DatabaseContext dbContext = new DatabaseContext())
            {

                roomQuery = dbContext.Rooms.Find(roomNum);
                roomValue.Content = roomQuery.RoomNo.ToString();
                bedValue.Content = roomQuery.BedType;
                smokingValue.Content = roomQuery.Smoking;
                priceValue.Content = roomQuery.Price;
               // System.Diagnostics.Debug.Write(roomEntry.ToString());
               // roomValue.Content = roomEntry.ToString();
            }
        }

        public void populateTextBoxes()
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                customerQuery = dbContext.Customers.SingleOrDefault(c => c.RoomNo == roomNum);
                TBID.Text = customerQuery.ID;
                TBFName.Text = customerQuery.FirstName;
                TBLName.Text = customerQuery.LastName;
                TBPhone.Text = customerQuery.PhoneNo;
                TBStreet.Text = customerQuery.Street;
                TBCity.Text = customerQuery.City;
                TBState.Text = customerQuery.State;
                TBZip.Text = customerQuery.Zip;
                TBPayment.Text = customerQuery.PaymentInfo;


            }
        }

    }



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
    

