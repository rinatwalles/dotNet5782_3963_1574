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
    /// Interaction logic for StationListWindow.xaml
    /// </summary>
    public partial class StationListWindow : Window
    {

        private BLApi.IBL ibl;
        public StationListWindow(BLApi.IBL newIbl)
        {
            InitializeComponent();
            ibl = newIbl;
            IEnumerable<StationToList> lst = ibl.GetAllStations();
            stationToListDataGrid.ItemsSource = ibl.GetAllDrones();
            stationToListDataGrid.IsReadOnly = true;
        }

        private void closeButtun_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void OptionButtun_Click(object sender, RoutedEventArgs e)
        {
            new PL.StationWindow(ibl).Show();
        }

        private void stationToListDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            StationToList s = stationToListDataGrid.SelectedItem as StationToList;
            new PL.StationWindow(ibl, s).Show();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            stationToListDataGrid.ItemsSource = ibl.GetAllStations();
        }
    }
}
