using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Drawing;
using System.Windows.Controls;
using System.ComponentModel;

namespace bestsixapp
{
    public class RoomData : IDataErrorInfo
    {
    
        private double width, height;
        Rectangle rect;
        Button myRoom;
        private int roomNo, noOfBeds;
        private string smoking, bedType, legend;
        private double price;
        private double left, top;
        private DateTime _Checkin, _Checkout;
        public Rectangle Rect { get => rect; set => rect = value; }
        public Button RoomButton { get => myRoom; set => myRoom = value; } 
        public int RoomNo { get => roomNo; set => roomNo = value; }
        public string Smoking
        {
            get { return smoking; }
            set
            {
                smoking = value;
                //OnPropertyChanged("Smoking");
            }
        }
        public int NoOfBeds { get => noOfBeds; set => noOfBeds = value; }
        public double Left { get => left; set => left = value; }
        public double Top { get => top; set => top = value; }
        public double Price { get => price; set => price = value; }
        public string BedType { get => bedType; set => bedType = value; }
        public string Legend { get => legend; set => legend = value; }
        private List<string> smokingList = new List<string>();

        public void Enumerate<T>(List<string> smokingList)
        {
            foreach(var item in smokingList)
            {
                //...
            }
        }
        public RoomData()
        {
            width = 115; // width of rect
            height = 65; //height of rect
        }
        public RoomData(int RoomNo, string BedType, int NoOfBeds, double Price, string Smoking, double Left, double Top, string Legend)
        {
            this.RoomNo = RoomNo;
            this.BedType = BedType;
            this.NoOfBeds = NoOfBeds;
            this.Price = Price;
            this.Smoking = Smoking;
            this.Left = Left;
            this.Top = Top;
            this.Legend = Legend;
            width = 115; // width of rect
            height = 65; //height of rect
        }
        //create rectangle to display room
        public object MakeRoom()
        {
            rect = new Rectangle
            {
                Fill = new SolidColorBrush(Color.FromArgb(255,50,245,88)),
                Stroke = new SolidColorBrush(Colors.Black),
                Width = width,
                Height = height,
                StrokeThickness = 2
            };
            
            return Rect;
        }

        public object DrawRoom()
        {
            myRoom = new Button();
            myRoom.Background = new SolidColorBrush(Color.FromArgb(255, 50, 245, 88));
            myRoom.Content = RoomNo;
            myRoom.Width = width;
            myRoom.Height = height;
            return myRoom;
        }
        
        public void Occupied()
        {    
            rect.Fill = new SolidColorBrush(Colors.Red);
        }

        public void Empty()
        {
            rect.Fill = new SolidColorBrush(Colors.LightGreen);
        }
        
        public void NeedCleaning()
        {
            rect.Fill = new SolidColorBrush(Colors.Yellow);
        }

        public string Error
        {
            get { return null; }
        }

        public DateTime Checkin { get => _Checkin; set => _Checkin = value; }
        public DateTime Checkout { get => _Checkout; set => _Checkout = value; }

        public string this[string columnName]
        {
            get
            {
                if(columnName == "RoomNo")
                {
                    if(roomNo < 1)
                    {
                        return "Please input new room number.";
                    }
                }
                
                if (columnName == "BedType")
                {
                    if (string.IsNullOrEmpty(columnName))
                        return "Field Input is required";
                    else
                    {
                        if (this.BedType == "Queen" || this.BedType == "King" || this.BedType == "Single")
                            BedType = columnName;
                        else
                            return "Invalid Option";
                    }
                }

                if(columnName == "Smoking")
                {
                    Console.WriteLine("yolo");
                    if (this.Smoking == null || this.Smoking == "0" )
                        return "Field Input is required";
                   
                }
                
                return null;
            }
        }
    }
}
