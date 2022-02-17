using BO;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for customerMain.xaml
    /// </summary>
    public partial class customerMain : Window
    {
        private BLApi.IBL ibl;
        public customerMain(BLApi.IBL newIbl)
        {
            InitializeComponent();
            ibl = newIbl;
        }



        private void ContinueButtun_Click(object sender, RoutedEventArgs e)
        {
            bool check=false;
            int id = int.Parse(TxtId.Text);
            try
            {
                Customer cs = ibl.GetCustomer(id);
            }
            catch (Exception ex)        //if weve got here he doesnt have an acount
            {
                check = true;
                if (isCustomerCheckBox.IsChecked == true)
                    MessageBox.Show("One of the inputs are wrong. try again");
                else
                {
                    new PL.CustomerWindow(ibl).Show();
                    this.Close();
                }
            }   
            if (check == false)
            {
                if (isCustomerCheckBox.IsChecked != true)
                    MessageBox.Show("One of the inputs are wrong. try again");
                else
                {
                    Customer cs = ibl.GetCustomer(id);
                    new PL.CustomerWindow(ibl, cs, false).Show();
                    this.Close();

                }
            }
        }
    }
}
