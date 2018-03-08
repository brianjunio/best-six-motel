﻿using Database;
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
    /// Interaction logic for RoomMake.xaml
    /// </summary>
    public partial class RoomMake : Window
    {
        List<RoomData> roomList = new List<RoomData>();
        RoomData room;
        RoomInfo newRoomInfo;
        Button rect;
        private bool moveButton = false;
        private bool isEdit = false;
        public RoomMake()
        {
            InitializeComponent();
            FillList();
            DrawAllRoom();
            AddRoomButton.IsEnabled = false;
            MoveRoomButton.IsEnabled = false;
        }

        private void CreateRoomClick(object sender, RoutedEventArgs e)
        {
            //create room object
            if (isEdit)
            {
                room = new RoomData();
                newRoomInfo = new RoomInfo(ref room);
             //   newRoomInfo.SaveButton.Click += SaveClickEventHandler;
                newRoomInfo.SaveRoom += new EventHandler(SaveClickEventHandler);
                newRoomInfo.ShowDialog();

            }
        }
        private void rect_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
  
            if (isEdit)
            {
                rect = (Button)sender; //grabs object selected by mouse and declare it rect
                rect.CaptureMouse();
            } 

        }


        private void rect_MouseLeftButtonUp(object sender, MouseEventArgs e)
        {
          

            if (isEdit)
            {
                rect = (Button)sender;
                rect.ReleaseMouseCapture();
            }
        }
        private void rect_MouseMove(object sender, MouseEventArgs e)
        {

            rect = (Button)sender;
            if (!rect.IsMouseCaptured) return;
            if (moveButton)
            {
                // get the position of the mouse relative to the Canvas
                var mousePos = e.GetPosition(RoomCanvas);

                // center the rect on the mouse
                double left = mousePos.X - (rect.ActualWidth / 2);
                double top = mousePos.Y - (rect.ActualHeight / 2);
                if (isEdit)
                {//boundary for left side
                    if (left < 0) left = 0;
                    //boundary for right side
                    if (left > this.ActualWidth - rect.ActualWidth * 1.16) left = this.ActualWidth - rect.ActualWidth * 1.16;
                    //boundary for top
                    if (top < 0) top = 0;
                    //boundary for bottom
                    if (top > this.ActualHeight - rect.ActualHeight * 2.58) top = this.ActualHeight - rect.ActualHeight * 2.58;
                    Canvas.SetLeft(rect, left);
                    Canvas.SetTop(rect, top);
                    foreach (var e1 in roomList)
                    {
                        //check room list to see if object selected is any of the room
                        if (rect == e1.RoomButton)
                            room = e1; // grabs room object if the rect is selected
                    } //this loop is to find the room object that is attached to rect
                    room.Left = Canvas.GetLeft(rect);
                    room.Top = Canvas.GetTop(rect);

                    //create updated entity
                    var modifiedRoom = new Room()
                    {
                        RoomNo = room.RoomNo,
                        Left = room.Left,
                        Top = room.Top,
                        NoOfBeds = room.NoOfBeds,
                        BedType = room.BedType,
                        Checkin = room.Checkin,
                        Checkout = room.Checkout,
                        Legend = room.Legend,
                        Price = room.Price,
                        Smoking = room.Smoking
                        // create room object and store input to local variables
                    };

                    using (DatabaseContext dbContext = new DatabaseContext())
                    {
                        dbContext.Update<Room>(modifiedRoom); //update database
                        dbContext.SaveChanges();        //save changes
                    }
                }
               
            }
            else
            {   if (isEdit)
                {
                    foreach (var e1 in roomList)
                    {
                        //check room list to see if object selected is any of the room
                        if (rect == e1.RoomButton)
                            room = e1; // grabs room object if the rect is selected
                    } //this loop is to find the room object that is attached to rect

                    RoomInfo editRoom = new RoomInfo(ref room);
                    //fill out textbox with room data
                    editRoom.TextBoxBedType.Text = room.BedType;
                    editRoom.TextBoxRoomNo.Text = room.RoomNo.ToString();
                    editRoom.TextBoxRoomNo.IsReadOnly = true;
                    editRoom.TextBoxRoomNo.Background = new SolidColorBrush(Color.FromArgb(115, 220, 220, 220));
                    editRoom.TextBoxNoOfBeds.Text = room.NoOfBeds.ToString();
                    editRoom.TextBoxPrice.Text = room.Price.ToString();
                    editRoom.ComboBoxSmoking.SelectedItem = room.Smoking;
                    editRoom.ShowDialog();
                }
            }

        }



        private void EditRoomClick(object sender, RoutedEventArgs e)
        {
            if (EditRoomButton.Content.ToString() == "Edit Rooms")
            {
                //Change button to finish and enable edit
                EditRoomButton.Padding = new Thickness(16.7);
                EditRoomButton.Content = "Finish";
                isEdit = true;
                AddRoomButton.IsEnabled = true;
                MoveRoomButton.IsEnabled = true;

            }
            else
            {
                //change button to edit rooms and disable edit
                EditRoomButton.Content = "Edit Rooms";
                EditRoomButton.Padding = new Thickness(2);
                isEdit = false;
                AddRoomButton.IsEnabled = false;
                MoveRoomButton.IsEnabled = false;

            }
        }


        private void SaveClickEventHandler(object sender, EventArgs e) => CreateRoom(); //call cancel method

        private void CreateRoom()
        {
            if (isEdit)
            {
                //cast object
                rect = (Button)room.drawRoom();
                //tract mouse events
                rect.PreviewMouseLeftButtonDown += rect_MouseLeftButtonDown;
                rect.PreviewMouseLeftButtonUp += rect_MouseLeftButtonUp;
  
                rect.PreviewMouseMove += rect_MouseMove;
                rect.Click += CheckButton_Click;
                //add room object to room list
                Canvas.SetLeft(rect, 0);
                Canvas.SetTop(rect, 0);
                room.Left = Canvas.GetLeft(rect);
                room.Top = Canvas.GetTop(rect);
          
                roomList.Add(room);
                //add object to canvas
                RoomCanvas.Children.Add(rect);

            }
        }

        private void FillList()
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var roomAttr = dbContext.Rooms
                                        .Select(rm => new
                                        {
                                            roomNo = rm.RoomNo,
                                            bedType = rm.BedType,
                                            noOfBed = rm.NoOfBeds,
                                            checkIn = rm.Checkin,
                                            checkOut = rm.Checkout,
                                            left = rm.Left,
                                            top = rm.Top,
                                            price = rm.Price,
                                            smoking = rm.Smoking,
                                            legend = rm.Legend

                                        });
                foreach (var obj in roomAttr)
                {
                    roomList.Add(new RoomData(obj.roomNo, obj.bedType, obj.noOfBed, obj.price, obj.smoking, obj.left, obj.top,obj.legend ));
                }           
                
            }
        }
        
        void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            rect = (Button)sender;

            if (!isEdit)
            {

                foreach (var e1 in roomList)
                {
                    //check room list to see if object selected is any of the room
                    if (rect == e1.RoomButton)
                        room = e1; // grabs room object if the rect is selected
                } //this loop is to find the room object that is attached to rect
                Check checkWindow = new Check(room.RoomNo, room.Smoking, room.BedType, room.Price);
              //  checkWindow.TextBofRoom.Text = Convert.ToString(room.RoomNo);
                checkWindow.ShowDialog();
            }
            
        }

        private void DrawAllRoom()
        {
            foreach(var obj in roomList)
            {
                //cast object
                rect = (Button)obj.drawRoom();
                //tract mouse events
                rect.PreviewMouseLeftButtonDown += rect_MouseLeftButtonDown;
                rect.PreviewMouseLeftButtonUp += rect_MouseLeftButtonUp;

                rect.PreviewMouseMove += rect_MouseMove;
                rect.Click += CheckButton_Click;
                //add room object to room list
                Canvas.SetLeft(rect, obj.Left);
                Canvas.SetTop(rect, obj.Top);

                //add object to canvas
                RoomCanvas.Children.Add(rect);
            }
        }

        private void MoveRoomButton_Click(object sender, RoutedEventArgs e)
        {
            if (!moveButton)
            {
                moveButton = true;
                MoveRoomButton.Background = new SolidColorBrush(Color.FromArgb(255, 57, 255, 20));
            }
            else
            {
                moveButton = false;
                MoveRoomButton.ClearValue(Button.BackgroundProperty);
            }

        }
    }
}


