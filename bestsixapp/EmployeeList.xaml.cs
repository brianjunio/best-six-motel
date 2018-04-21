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

        private void EmployeeSelected(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if(row_selected != null)
            {
                UsernameTxtbox.Text = row_selected["Username"].ToString();
                PositionTxtbox.Text = row_selected["EmpType"].ToString();
                FirstNameTxtbox.Text = row_selected["FirstName"].ToString();
                LastNameTxtbox.Text = row_selected["LastName"].ToString();
                PasswordTxtbox.Text = row_selected["Password"].ToString();
                
            }
            UsernameTxtbox.Text = "Hi";
        }

        private void ExitButton(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
