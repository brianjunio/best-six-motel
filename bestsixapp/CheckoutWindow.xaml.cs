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
using System.Windows.Shapes;

namespace bestsixapp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class CheckoutWindow : Window
    {
        public CheckoutWindow()
        {
            InitializeComponent();
        }

        public void ButtonCheckout_Click(object sender, RoutedEventArgs e)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                dbContext.Rooms.Add(new Room
                {
                    Checkout = DateTime.Parse(CheckoutYear.Text + "-" + CheckoutMonth.Text + "-" + CheckoutDay.Text),
                    Checkin = DateTime.Parse(CheckinYear.Text + "-" + CheckinMonth.Text + "-" + CheckinDay.Text)
                });
            }
        }

        private void ButtonRefund_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PopulateLabels()
        {

        }
    }
}
