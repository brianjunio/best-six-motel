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
    /// Interaction logic for CompanyInfo.xaml
    /// </summary>
    public partial class CompanyInfo : Window
    {
        public CompanyInfo()
        {
            InitializeComponent();
            loadText();
        }

        private void loadText()
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {
                if (dbContext.Companies.Any())
                {
                    var companyInfo = dbContext.Companies.FirstOrDefault();
                    TextBoxName.Text = companyInfo.CompanyName;
                    TextBoxAddress.Text = companyInfo.Address;
                    TextBoxOwner.Text = companyInfo.Owner;
                    TextBoxNumber.Text = companyInfo.PhoneNumber;
                }
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            using (DatabaseContext dbContext = new DatabaseContext())
            {

                if (!dbContext.Companies.Any())
                {
                    var companyInfo = new Company()
                    {
                        CompanyName = TextBoxName.Text,
                        Address = TextBoxAddress.Text,
                        Owner = TextBoxOwner.Text,
                        PhoneNumber = TextBoxNumber.Text

                    };
                    dbContext.Companies.Add(companyInfo);
                }
                else
                {
                    var tempCompany = dbContext.Companies.Where(s => s.CompanyID == 1)
                                             .FirstOrDefault();
                    tempCompany.CompanyName = TextBoxName.Text;
                    tempCompany.Address = TextBoxAddress.Text;
                    tempCompany.Owner = TextBoxOwner.Text;
                    tempCompany.PhoneNumber = TextBoxNumber.Text;
                    dbContext.Update<Company>(tempCompany); //update database

                }
                dbContext.SaveChanges();        //save changes
            }
            
        
    


            //close screen
            Close();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
