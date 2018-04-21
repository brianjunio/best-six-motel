using Database;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
//using System.Windows.Navigation.NavigationWindow;
using System.Windows.Input;
using System.Windows.Media;


namespace bestsixapp
{
    /// <summary>
    /// Interaction logic for RoomMake.xaml
    /// </summary>
    public partial class RoomMake : Window
    {
        List<RoomData> roomList = new List<RoomData>();
        Room roomQuery = new Room();
        RoomData room;
        RoomInfo newRoomInfo;
        Button rect;
        DateTime defaultTime = DateTime.Parse("0001-01-01 00:00:00");
        private bool moveButton = false;
        private bool isEdit = false;
        private bool deleteButton = false;

        public object Frame { get; private set; }
        public static RoomMake NavigationService { get; internal set; }

        //NavigationService navService = NavigationService.GetNavigationService(this);

        public RoomMake(string empType)
        {
            

            InitializeComponent();
            FillList();
            DrawAllRoom();
            if (empType == "Admin")
            {
                AddRoomButton.IsEnabled = false;
                MoveRoomButton.IsEnabled = false;
                DeleteRoomButton.IsEnabled = false;
            }
            else if (empType == "Employee")
            {
                SettingsButton.IsEnabled = false;
                RoomEditButton.IsEnabled = false;

            }
            // Changes to Menu Grid Button Panel
            RoomEditor.Visibility = Visibility.Hidden;
            MenuButtons.Visibility = Visibility.Visible;
            TransactionsButton.Visibility = Visibility.Hidden;

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Rect_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            //capture click of mouseleft butotn
            if (isEdit)
            {
                rect = (Button)sender; //grabs object selected by mouse and declare it rect
                rect.CaptureMouse();
            }

        }

        private void Rect_MouseLeftButtonUp(object sender, MouseEventArgs e)
        {
            //capture release of mouseleft button
            if (isEdit)
            {
                rect = (Button)sender;
                rect.ReleaseMouseCapture();
            }
        }
        private void Rect_MouseMove(object sender, MouseEventArgs e)
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
                        //  Checkin = room.Checkin,
                        //  Checkout = room.Checkout,
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
            {
                foreach (var e1 in roomList)
                {
                    //check room list to see if object selected is any of the room
                    if (rect == e1.RoomButton)
                        room = e1; // grabs room object if the rect is selected
                } //this loop is to find the room object that is attached to rect
                if (isEdit && !deleteButton) //edit room info
                {
                    

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
                else if (deleteButton)
                {
                    foreach (var e1 in roomList)
                    {
                        //check room list to see if object selected is any of the room
                        if (rect == e1.RoomButton)
                            room = e1; // grabs room object if the rect is selected
                    } //this loop is to find the room object that is attached to rect
                    //Room delete dailog
                    string sMessageBoxText = "Do you want to delete this room?";
                    string sCaption = "Delete Room";
                    MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
                    MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                    MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

                    switch (rsltMessageBox)
                    {
                        case MessageBoxResult.Yes:                           

                            if (room.Legend.Equals("Vacant") || room.Legend.Equals("Dirty")) {
                                roomList.Remove(room);
                                RoomCanvas.Children.Remove(rect);
                                var deletedRoom = new Room()
                                {
                                    RoomNo = room.RoomNo
                                };
                                using (var context = new DatabaseContext())
                                {
                                    context.Remove<Room>(deletedRoom);
                                    context.SaveChanges();
                                }
                                InvalidateVisual();
                            }
                            else
                            {
                                System.Windows.Forms.MessageBox.Show("There is a customer in the current room.");
                            }
                            break;

                        case MessageBoxResult.No:
                            /* ... */
                            break;
                    }
                }
            }

        }

        private void SaveClickEventHandler(object sender, EventArgs e) => CreateRoom(); //call cancel method

        private void CreateRoom()
        {
            if (isEdit)
            {
                //cast object
                rect = (Button)room.DrawRoom();
                //tract mouse events
                rect.PreviewMouseLeftButtonDown += Rect_MouseLeftButtonDown;
                rect.PreviewMouseLeftButtonUp += Rect_MouseLeftButtonUp;

                rect.PreviewMouseMove += Rect_MouseMove;
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
        //grab data from db and add to local list
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var roomAttr = dbContext.Rooms
                                        .Select(rm => new
                                        {
                                            roomNo = rm.RoomNo,
                                            bedType = rm.BedType,
                                            noOfBed = rm.NoOfBeds,
                                            // checkIn = DateTime.Today,
                                            // checkOut = rm.Checkout,
                                            left = rm.Left,
                                            top = rm.Top,
                                            price = rm.Price,
                                            smoking = rm.Smoking,
                                            legend = rm.Legend

                                        });
                foreach (var obj in roomAttr)
                {
                    roomList.Add(new RoomData(obj.roomNo, obj.bedType, obj.noOfBed, obj.price, obj.smoking, obj.left, obj.top, obj.legend));
                }
            }
        }

        private void DrawAllRoom()
        {
            //draw all rooms
            foreach (var obj in roomList)
            {
                //cast object
                rect = (Button)obj.DrawRoom();
                //tract mouse events
                rect.PreviewMouseLeftButtonDown += Rect_MouseLeftButtonDown;
                rect.PreviewMouseLeftButtonUp += Rect_MouseLeftButtonUp;

                rect.PreviewMouseMove += Rect_MouseMove;
                rect.Click += CheckButton_Click;
                //add room object to room list
                Canvas.SetLeft(rect, obj.Left);
                Canvas.SetTop(rect, obj.Top);
                //add object to canvas
                RoomCanvas.Children.Add(rect);
            }
        }

        // R O O M   E D I T O R   B U T T O N S 
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

        void CheckButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var e1 in roomList)
            {
                //check room list to see if object selected is any of the room
                if (rect == e1.RoomButton)
                    room = e1; // grabs room object if the rect is selected
            } //this loop is to find the room object that is attached to rect
           
            //checking in customer window
            rect = (Button)sender;
            if (room.Legend.Equals("Dirty"))
            {
                //Room delete dailog
                string sMessageBoxText = "Has this room been cleaned?";
                string sCaption = "Room Clean and Vacant";
                MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

                switch (rsltMessageBox)
                {
                    case MessageBoxResult.Yes:
                        room.Empty();
                        room.Legend = "Vacant";

                        using (DatabaseContext dbContext = new DatabaseContext())
                        {
                            roomQuery = dbContext.Rooms.Find(room.RoomNo);
                            roomQuery.Legend = "Vacant";
                        }
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
            else if (!isEdit)
            {

                
                Check checkWindow = new Check(room);
                //  checkWindow.TextBofRoom.Text = Convert.ToString(room.RoomNo);
                using (DatabaseContext dbContext = new DatabaseContext())
                {
                    roomQuery = dbContext.Rooms.Find(room.RoomNo);
                    if (roomQuery.Legend == "Occupied")
                    {
                        checkWindow.PopulateTextBoxes();
                        checkWindow.DisableTextBoxes();
                        checkWindow.CheckOutEnable();
                        //checkWindow.ShowDialog();
                        Main.NavigationService.Navigate(checkWindow);
                    }
                    else
                    {   // N a v i g a t e   b  a c k  h o m e 
                        checkWindow.CheckInEnable();
                        //checkWindow.ShowDialog();
                        Main.NavigationService.Navigate(checkWindow);
                    }
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
                DeleteRoomButton.IsEnabled = true;
            }
            else
            {
                //change button to edit rooms and disable edit
                EditRoomButton.Content = "Edit Rooms";
                EditRoomButton.Padding = new Thickness(2);
                isEdit = false;
                AddRoomButton.IsEnabled = false;
                MoveRoomButton.IsEnabled = false;
                DeleteRoomButton.IsEnabled = false;
            }
        }

        private void MoveRoomButton_Click(object sender, RoutedEventArgs e)
        {
            //moving rooms
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

        private void DeleteRoomButton_Click(object sender, RoutedEventArgs e)
        {
            if (!deleteButton)
            {
                deleteButton = true;
                DeleteRoomButton.Background = new SolidColorBrush(Color.FromArgb(255, 57, 255, 20));
            }

            else
            {
                deleteButton = false;
                DeleteRoomButton.ClearValue(Button.BackgroundProperty);
            }
        }
        private void BackMenuButton_Click(object sender, RoutedEventArgs e)
        {
            // Changes to Menu Grid Button Panel
            this.Main.NavigationService.Navigate(this.Parent);
            RoomEditor.Visibility = Visibility.Hidden;
            TransactionsButton.Visibility = Visibility.Hidden;
            MenuButtons.Visibility = Visibility.Visible;

            EditRoomButton.Content = "Edit Rooms";
            EditRoomButton.Padding = new Thickness(2);
            isEdit = false;
            AddRoomButton.IsEnabled = false;
            MoveRoomButton.IsEnabled = false;
            DeleteRoomButton.IsEnabled = false;

            moveButton = false;
            MoveRoomButton.ClearValue(Button.BackgroundProperty);
            deleteButton = false;
            DeleteRoomButton.ClearValue(Button.BackgroundProperty);
        }


        // M E N U   B U T T O N S 
        private void TrasactionButton_Click(object sender, RoutedEventArgs e)
        {
            TransactionsView transactionWindow = new TransactionsView();
            this.Main.NavigationService.Navigate(transactionWindow);
            //tv.InitializeComponent();
            //tv.ShowDialog();
            TransactionsButton.Visibility = Visibility.Visible;
            RoomEditor.Visibility = Visibility.Hidden;
            MenuButtons.Visibility = Visibility.Hidden;
        }
        private void RoomEditButton_Click(object sender, RoutedEventArgs e)
        {
            // Changes to Menu Grid Button Panel
            RoomEditor.Visibility = Visibility.Visible;
            MenuButtons.Visibility = Visibility.Hidden;
            TransactionsButton.Visibility = Visibility.Hidden;

        }
        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow sw = new SettingsWindow();
            sw.ShowDialog();
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            this.Close();
            mw.ShowDialog();
            //this.Hide();
        }
        protected void RoomMakeWindow_Closed(object sender, EventArgs args)
        {
            MainWindow mw = new MainWindow();
            this.Close();
            mw.ShowDialog();
        }
    }
}


