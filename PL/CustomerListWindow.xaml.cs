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
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerListWindow.xaml
    /// </summary>
    public partial class CustomerListWindow : Window
    {
        private BLApi.IBL ibl;
        public CustomerListWindow(BLApi.IBL newIbl)
        {
            InitializeComponent();
            ibl = newIbl;
            IEnumerable<CustomerToList> lst = ibl.GetAllCustomers();
            customerToListDataGrid.ItemsSource = ibl.GetAllCustomers();
            customerToListDataGrid.IsReadOnly = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new PL.CustomerWindow(ibl).Show();
        }

        private void closeButtun_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void customerToListDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CustomerToList c = customerToListDataGrid.SelectedItem as CustomerToList;
            Customer cs = ibl.GetCustomer(c.Id);
            new PL.CustomerWindow(ibl, cs).Show();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            customerToListDataGrid.ItemsSource = ibl.GetAllCustomers();
        }
    }
}
