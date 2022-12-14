using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using static DO.Enums;

namespace PL
{
    /// <summary>
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class WAddProduct : Window
    {
        private BlApi.IBl bl = new BlImplementation.Bl();

        public WAddProduct()
        {
            InitializeComponent();
          
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            //BO.Product p;
            //bl.Product.AddProductManager(p);
            
        }

    }
}
