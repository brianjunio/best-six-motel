using Database;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace bestsixapp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            RefreshList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                dbContext.Customers.Add(new Customer {ID = Int32.Parse(TextBoxID.Text),Name = TextBoxName.Text});
                dbContext.SaveChanges();

                RefreshList();
            }
        }

        private void RefreshList()
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                ListViewNames.ItemsSource = dbContext.Customers
                    .OrderBy(m => m.Name)
                    .Select(m => m.Name)
                    .ToList();
            }
        }
    }
}
