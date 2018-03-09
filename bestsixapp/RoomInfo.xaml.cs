
using Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace bestsixapp
{
    /// <summary>
    /// Interaction logic for RoomInfo.xaml
    /// </summary>
    public partial class RoomInfo : Window
    {
        RoomData room;
        private int _roomNo, _price, _noOfBeds;
        private string _bedType, _smoking;
        private int _noOfErrorsOnScreen;
        public int RoomNo { get => _roomNo; set => _roomNo = value; }
        public int Price { get => _price; set => _price = value; }
        public int NoOfBeds { get => _noOfBeds; set => _noOfBeds = value; }
        public string BedType { get => _bedType; set => _bedType = value; }
        public string Smoking { get => _smoking; set => _smoking = value; }
        

        public event EventHandler SaveRoom = delegate { };
        public RoomInfo(ref RoomData room)
        {
            InitializeComponent();
            FillCombo();
            this.room = room;
        }

      
        //method for cancel
        public void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Validation_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                _noOfErrorsOnScreen++;
            else
                _noOfErrorsOnScreen--;
        }

        private void AddRoom_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _noOfErrorsOnScreen == 0;
            e.Handled = true;
        }

        public void AddRoom_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            
                //add data to database
                room.RoomNo = Int32.Parse(TextBoxRoomNo.Text);
                room.BedType = TextBoxBedType.Text;
                room.NoOfBeds = Int32.Parse(TextBoxNoOfBeds.Text);
                room.Price = Double.Parse(TextBoxPrice.Text);
                room.Smoking = ComboBoxSmoking.Text;
                
               
                //create updated entity
                var modifiedRoom = new Room()
                {
                    RoomNo = room.RoomNo,
                    Left = room.Left,
                    Top = room.Top,
                    NoOfBeds = room.NoOfBeds,
                    BedType = room.BedType,
                  //  Checkin = room.Checkin,
                  //  Checkout = room.Checkout,
                    Legend = room.Legend,
                    Price = room.Price,
                    Smoking = room.Smoking

                };

                using (DatabaseContext dbContext = new DatabaseContext())
                {
                
                    var tempRoom = dbContext.Rooms.Where(s => s.RoomNo == modifiedRoom.RoomNo)
                                              .FirstOrDefault();
                    if(tempRoom == null)
                    {
                        dbContext.Rooms.Add(new Room { RoomNo = Int32.Parse(TextBoxRoomNo.Text), BedType = TextBoxBedType.Text,
                        NoOfBeds = Int32.Parse(TextBoxNoOfBeds.Text), Price = Double.Parse(TextBoxPrice.Text), Smoking = ComboBoxSmoking.Text, Left = room.Left, Top = room.Top  });
                    }else
                    {
                        dbContext.Update<Room>(tempRoom); //update database
                    }
                    dbContext.SaveChanges();        //save changes
                }


            //close screen
                Close();
           // }
            this.SaveRoom(this, new EventArgs()); //Raise Event, which trigger all events subscribed to it.
        }
        private void FillCombo()
        {
            ComboBoxSmoking.Items.Add("Yes");
            ComboBoxSmoking.Items.Add("No");   
        }
    }
}
