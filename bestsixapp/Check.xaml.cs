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
        string smoking;
        string bedType;
        double price;

        public Check()
        {
            InitializeComponent();
            UpdateLabels();
            // RefreshList();  Update Room Page with Customer Information when seleceted
        }

        public Check(int roomNum, string smoking, string bedType, double price)
        {
            this.roomNum = roomNum;
            this.smoking = smoking;
            this.bedType = bedType;
            this.price = price;
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
                    PaymentInfo = TBPayment.Text
                });
                dbContext.SaveChanges();

                Close();
                // RefreshList();  Update Room Page with Customer Information when seleceted

            }
        }
        //method for cancel
        public void ButtonCancel_Click(object sender, RoutedEventArgs e) { this.Close(); }

        private void UpdateLabels()
        {
            using(DatabaseContext dbContext = new DatabaseContext())
            {
                /* var roomEntry = from r in dbContext.Rooms
                                 where r.RoomNo.Equals(1)
                                 select r;*/
                Room query = new Room();
                query = dbContext.Rooms.Find(roomNum);
                roomValue.Content = query.RoomNo.ToString();
                bedValue.Content = query.BedType;
                smokingValue.Content = query.Smoking;
                priceValue.Content = query.Price;
               // System.Diagnostics.Debug.Write(roomEntry.ToString());
               // roomValue.Content = roomEntry.ToString();
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
    

