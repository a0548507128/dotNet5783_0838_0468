using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for WNewOrder.xaml
    /// </summary>
    
    public partial class WNewOrder : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public static readonly DependencyProperty ProductForItemProperty = DependencyProperty.Register(nameof(ProductForItem),
                                                                                                             typeof(ObservableCollection<ProductItem?>),
                                                                                                     typeof(WNewOrder));
        public ObservableCollection<ProductItem?> ProductForItem
        {
            get { return (ObservableCollection<ProductItem?>)GetValue(ProductForItemProperty); }
            set { SetValue(ProductForItemProperty, value); }
        }
        public WNewOrder()
        {

         //   ProductForItem = new(bl.Product.GetProductDetailsManager());
            InitializeComponent();

        }
    }
}
