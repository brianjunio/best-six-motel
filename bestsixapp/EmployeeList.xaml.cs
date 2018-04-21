using Database;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for EmployeeList.xaml
    /// </summary>
    public partial class EmployeeList : Window
    {
        public EmployeeList()
        {
            InitializeComponent();
            fillEmployeee();
        }

        private void fillEmployeee()
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                EmployeeData.ItemsSource = dbContext.Employees.ToList();
            }
        } 

        private void ExitButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                //create updated entity
                var modifiedEmployee = new Employee()
                {
                    Username = UsernameTxtbox.Text,
                    EmpType = PositionTxtbox.Text,
                    FirstName = FirstNameTxtbox.Text,
                    LastName = LastNameTxtbox.Text,
                    Password = PasswordTxtbox.Text
                };
                
               
                dbContext.Update<Employee>(modifiedEmployee); //update database
                dbContext.SaveChanges();        //save changes
                fillEmployeee();
            }
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var hit = dbContext.Employees.FirstOrDefault(f => f.Username == UsernameTxtbox.Text);
                if (hit != null)
                {
                    e.CanExecute = true;
                    e.Handled = true;
                    UsernameTxtbox.IsReadOnly = true;
                    UsernameTxtbox.IsEnabled = false;
                }
            }
        }
        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                //create updated entity
                var newEmployee = new Employee()
                {
                    Username = UsernameTxtbox.Text,
                    EmpType = PositionTxtbox.Text,
                    FirstName = FirstNameTxtbox.Text,
                    LastName = LastNameTxtbox.Text,
                    Password = PasswordTxtbox.Text
                };


                dbContext.Employees.Add(newEmployee); //update database
                dbContext.SaveChanges();        //save changes
                fillEmployeee();
            }
        }

        private void New_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var hit = dbContext.Employees.FirstOrDefault(f => f.Username == UsernameTxtbox.Text);
                if (hit == null)
                {
                    e.CanExecute = true;
                    e.Handled = true;
                    UsernameTxtbox.IsReadOnly = false;
                    UsernameTxtbox.IsEnabled = true;
                }
            }
        }

        private void Delete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                var hit = dbContext.Employees.FirstOrDefault(f => f.Username == UsernameTxtbox.Text);
                if (hit != null)
                {
                    e.CanExecute = true;
                    e.Handled = true;
                    UsernameTxtbox.IsReadOnly = true;
                    UsernameTxtbox.IsEnabled = false;
                }
            }
        }

        private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                //create updated entity
                var deletedEmployee = new Employee()
                {
                    Username = UsernameTxtbox.Text
                };
                dbContext.Remove<Employee>(deletedEmployee); //update database
                dbContext.SaveChanges();        //save changes
                fillEmployeee();
            }
        }
    }
}
