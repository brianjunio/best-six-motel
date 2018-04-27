using System.Windows;
using Database;
using System.Windows.Controls;
using System.Printing;          //Go to Project, Add Reference menu item. In Assemblies Choose System.Printing.



namespace bestsixapp

{

    /// <summary>

    /// Interaction logic for PrintReceipt.xaml

    /// </summary>

    public partial class Print : Page

    {

        public Print()

        {

            InitializeComponent();

        }

        private void PrintButtonClick(object sender, RoutedEventArgs e)

        {

            PrintDialog printDialog = new PrintDialog();
            printDialog.PrintTicket.PageOrientation = PageOrientation.Landscape;

            printDialog.PrintVisual(PrintPage, "Landscape Print");

        }

        private void PopulateTextboxes()
        {
            using(DatabaseContext dbContext = new DatabaseContext())
            {
                //dbContext.Customers.Find
            }
        }
    }

}