﻿
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
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BLApi.IBL ibl;

        public MainWindow(BLApi.IBL newIbl)
        {
            InitializeComponent();
            ibl = newIbl;
        }

        private void DronesWindow_Click(object sender, RoutedEventArgs e)
        {
            new PL.DroneListWindow(ibl).Show();
        }

        private void CustomersWindow_Click(object sender, RoutedEventArgs e)
        {
            new PL.CustomerListWindow(ibl).Show();
        }

        private void StationsWindow_Click(object sender, RoutedEventArgs e)
        {
            new PL.StationListWindow(ibl).Show();
        }

        private void ParcelsWindow_Click(object sender, RoutedEventArgs e)
        {
            new PL.ParcelToListWindow(ibl).Show();
        }


    }
}
