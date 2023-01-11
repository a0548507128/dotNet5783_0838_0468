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
    /// Interaction logic for WProductItemDetails.xaml
    /// </summary>
    public partial class WProductItemDetails : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public BO.ProductItem DetailsOfProductItem
        {
            get { return (BO.ProductItem)GetValue(DetailsOfProductItemProperty); }
            set { SetValue(DetailsOfProductItemProperty, value); }
        }
        public static readonly DependencyProperty DetailsOfProductItemProperty = DependencyProperty.Register(nameof(DetailsOfProductItem),
                                                                                                        typeof(BO.ProductItem),
                                                                                                       typeof(WProductItemDetails));


        public WProductItemDetails(int id)
        {
            DetailsOfProductItem = new();
            if (bl != null)
            {
                DetailsOfProductItem = bl.Product.GetProductItemDetails(id);
            }
            InitializeComponent();
        }

        private void price_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
