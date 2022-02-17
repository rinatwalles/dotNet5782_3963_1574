﻿using BO;
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
    /// Interaction logic for ParcelToListWindow.xaml
    /// </summary>
    public partial class ParcelToListWindow : Window
    {

        private BLApi.IBL ibl;
        public ParcelToListWindow(BLApi.IBL newIbl)
        {
            InitializeComponent();
            ibl = newIbl;
            IEnumerable<ParcelToList> lst = ibl.GetAllParcels();
            parcelToListDataGrid.ItemsSource = lst;
            parcelToListDataGrid.IsReadOnly = true;
        }
        public ParcelToListWindow(BLApi.IBL newIbl,int id)
        {
            InitializeComponent();
            ibl = newIbl;
            IEnumerable<ParcelToList> lst = from pr in ibl.GetAllParcels()
                                            where pr.Receiver.Id == id || pr.Sender.Id == id
                                            select pr;
            parcelToListDataGrid.ItemsSource = lst;
            parcelToListDataGrid.IsReadOnly = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new PL.ParcelWindow(ibl).Show();
        }

        private void parcelToListDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            ParcelToList p = parcelToListDataGrid.SelectedItem as ParcelToList;
            Parcel pc = ibl.GetParcel(p.Id);  //ghjkl
            new PL.ParcelWindow(ibl, pc).Show();
        }

        private void closeButtun_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void groupButtun_Click(object sender, RoutedEventArgs e)
        {

        }

        private void clearButtun_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Activated(object sender, EventArgs e)
        {
            parcelToListDataGrid.ItemsSource = ibl.GetAllParcels();
        }
    }
}
