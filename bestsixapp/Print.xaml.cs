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

    }

}