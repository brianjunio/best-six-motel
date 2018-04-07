using Database;
using System;
using System.Linq;
using System.Windows;

// using System.Windows.Controls;
// using System.Windows.Data;
// using System.Windows.Documents;
// using System.Windows.Input;
// using System.Windows.Media;
// using System.Windows.Media.Imaging;
// using System.Windows.Navigation;
// using System.Windows.Shapes;
using System.Windows.Controls;




namespace bestsixapp
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Check : Window
    {
        int roomNum;
        string localID;
        DateTime checkin, checkout;
        Room roomQuery = new Room();
        Customer customerQuery = new Customer();
        Transactions transactionQuery = new Transactions();
        int trValue = 1;


        public Check()
        {
            InitializeComponent();
            
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
                    RoomNo = roomNum,
                });

                roomQuery = dbContext.Rooms.SingleOrDefault(rm => rm.RoomNo == roomNum);
                if(roomQuery != null)
                {
                    roomQuery.Checkin = DateTime.Now;
                    roomQuery.Legend = "Occupied";
                }

                /* Adds new Transaction to transaction table in sequential order */
                do
                {
                    transactionQuery = dbContext.Transactions.SingleOrDefault(t => t.TrNumber == trValue);
                    if (transactionQuery == null)
                    {
                        dbContext.Transactions.Add(new Transactions
                        {
                            TrNumber = trValue,
                            Checkin = DateTime.Today,
                            DateModified = DateTime.Today,
                            ID = TBID.Text,
                            RoomNo = roomNum
                        });
                    }
                    else
                    {
                        trValue += 1;
                    }
                }
                while (transactionQuery != null);
                
            
                dbContext.SaveChanges();
                localID = TBID.Text;
                Close();

            }
        }

        // Checkin and Checkout DatePicker event handlers
        private void CheckinDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // From the datepicker selected date turn into DateTime object
            checkin = (DateTime)checkinValue.SelectedDate;
        }

        private void CheckoutDateChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // From the datepicker selected date turn into DateTime object
            checkout = (DateTime)checkoutValue.SelectedDate;
        }

        /*
         *  Selects entity based on ID, then removes from database.
         *  Must add DataValidation.
         */

        public void ButtonCheckout_Click(object sender, RoutedEventArgs e)
        {
            var checkOut = DateTime.Now;
            using(DatabaseContext dbContext = new DatabaseContext())
            {               
                roomQuery = dbContext.Rooms.Find(roomNum);
                roomQuery.Legend = "Vacant";
                roomQuery.Checkin = checkOut;

                transactionQuery = dbContext.Transactions.SingleOrDefault(t => t.RoomNo == roomNum);
                if(transactionQuery != null)
                {
                    transactionQuery.Checkout = checkOut;
                    transactionQuery.DateModified = checkOut;
                }

                dbContext.SaveChanges();
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
            TBState.IsEnabled = false;
            TBZip.IsEnabled = false;
            TBPayment.IsEnabled = false;
            TBcheckin.IsEnabled = false;
            TBcheckout.IsEnabled = false;
        }

        public void CheckInEnable()
        {
            BTRegister.IsEnabled = true;
            BTCheckout.IsEnabled = false;
        }

        public void CheckOutEnable()
        {
            BTRegister.IsEnabled = false;
            BTCheckout.IsEnabled = true;
        }
    }
}
    

