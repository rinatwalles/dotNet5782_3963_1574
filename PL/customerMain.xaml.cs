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

        private void TxtId_TextChanged(object sender, TextChangedEventArgs e)
        {
           // int id = int.Parse(TxtId.Text);
            new ParcelToListWindow(ibl, 4);
        }
    }
}
