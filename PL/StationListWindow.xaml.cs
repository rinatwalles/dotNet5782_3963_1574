using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            stationToListDataGrid.ItemsSource = lst;
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
            Station st = ibl.GetStation(s.Id);

            new PL.StationWindow(ibl, st).Show();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            stationToListDataGrid.ItemsSource = ibl.GetAllStations();
        }

        private void groupButtun_Click(object sender, RoutedEventArgs e)
        {
            stationToListDataGrid.ItemsSource = (from station in ibl.GetAllStations()
                                                 orderby station.AvailableChargeSlots
                                                 select station);
            //group station by station.AvailableCharginggSlotsNumber into gs


            //IEnumerable<IGrouping<int,StationToList>> result = (from station in ibl.GetAllStations()
            //                                     group station by station.AvailableCharginggSlotsNumber into gs
            //                                     select gs);

            //List<StationToList> lst1 = new List<StationToList>();
            //foreach (IGrouping<int, StationToList> g in result)
            //{


            //            foreach (StationToList n in g)
            //                lst1.Add(n);                       


            //}
            //stationToListDataGrid.ItemsSource = lst1;

        }

        private void clearButtun_Click(object sender, RoutedEventArgs e)
        {
            stationToListDataGrid.ItemsSource = (from station in ibl.GetAllStations()
                                                 orderby station.Id
                                                 select station);

        }
    }
}