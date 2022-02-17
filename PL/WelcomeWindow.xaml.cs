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
using BlApi;

namespace PL
{
    /// <summary>
    /// Interaction logic for WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        internal readonly BLApi.IBL ibl = BlFactory.GetBl();

        public WelcomeWindow()
        {
            InitializeComponent();
        }

        private void Customer_Click(object sender, RoutedEventArgs e)
        {
            new PL.customerMain(ibl).Show(); ;
        }

        private void Manager_Click(object sender, RoutedEventArgs e)
        {
            new PL.MainWindow(ibl).Show(); ;
        }
    }
}
