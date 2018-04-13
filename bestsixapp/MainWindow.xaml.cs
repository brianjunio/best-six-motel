﻿using Database;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        // Creates a Frame For Pages
        //public object Frame { get; private set; }
        //public static object NavigationService { get; internal set; }
        private string _name;
        public MainWindow()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            checkDB();
            InitializeComponent();
            CompanyName = "Room Management System";
            this.Closed += new EventHandler(MainWindow_Closed);           
        }


        // Goes to Make Rooms Editor in NEW WINDOW
       /* private void RoomClick(object sender, RoutedEventArgs e)
        {
            RoomMake roomWindow = new RoomMake();
            roomWindow.ShowDialog();
        }

        
        // Goes to employee Check in window
        private void employeeClick(object sender, RoutedEventArgs e)
        {
            Check checkWindow = new Check();
            checkWindow.ShowDialog();
        }

        // Goes to Tranasactions table view
        private void TransactionViewClick(object sender, RoutedEventArgs e)
        {
            TransactionsView transactionsWindow = new TransactionsView();
            transactionsWindow.ShowDialog();
        }


        // Goes to Tranasactions table view
        private void TransactionViewClick(object sender, RoutedEventArgs e)
        {
            TransactionsView Tr = new TransactionsView();
            Tr.ShowDialog();
        }
        */


        /*private void CheckoutWindowClick(object sender, RoutedEventArgs e)
        {
            Check cw = new Check();
            cw.ShowDialog();
        }

        private void Main_Navigated(object sender, NavigationEventArgs e)
        {

        }*/

        // Closes the application
        protected void MainWindow_Closed(object sender, EventArgs args)
        {
            App.Current.Shutdown();
        }
        public string CompanyName
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("CompanyName");
            }
            
        }
        private void OnPropertyChanged(String property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        private void myLoginButton(object sender, RoutedEventArgs e)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                if (String.IsNullOrEmpty(UserText.Text))
                {
                    MessageBox.Show("Please input field.");
                }
                else
                {
                    var employeeLogin = dbContext.Employees
                                      .SingleOrDefault(s => s.Username == UserText.Text && s.Password == UserPassword.Password.ToString());
                    if (employeeLogin != null)
                    {
                        RoomMake roomWindow = new RoomMake(employeeLogin.EmpType);
                        this.Hide();
                        roomWindow.ShowDialog();
                        //Main.NavigationService.Navigate(roomWindow);
                    }
                    else
                    {
                        MessageBox.Show("Login Invalid.");
                    }    
                }
            }
        }

        private void checkDB()
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                if (!dbContext.Employees.Any())
                {
                    var DefaultRoot = new Employee() { Username = "Root", EmpType = "Admin", Password = "Password" };
                    dbContext.Add<Employee>(DefaultRoot);
                    dbContext.SaveChanges();
                }
            }
        }
    }
}

