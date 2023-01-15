using BO;
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
        public BO.Cart NowCart
        {
            get { return (BO.Cart)GetValue(nowCartProperty); }
            set { SetValue(nowCartProperty, value); }
        }
        public static readonly DependencyProperty nowCartProperty = DependencyProperty.Register(nameof(NowCart),
                                                                                                        typeof(BO.Cart),
                                                                                                       typeof(WProductItemDetails));
        public BO.ProductItem DetailsOfProductItem
        {
            get { return (BO.ProductItem)GetValue(DetailsOfProductItemProperty); }
            set { SetValue(DetailsOfProductItemProperty, value); }
        }
        public static readonly DependencyProperty DetailsOfProductItemProperty = DependencyProperty.Register(nameof(DetailsOfProductItem),
                                                                                                        typeof(BO.ProductItem),
                                                                                                       typeof(WProductItemDetails));

        public bool isEnabled
        {
            get { return (bool)GetValue(isEnabledProperty); }
            set { SetValue(isEnabledProperty, value); }
        }
        public static readonly DependencyProperty isEnabledProperty = DependencyProperty.Register(nameof(isEnabled),
                                                                                                        typeof(bool),
                                                                                                       typeof(WProductItemDetails));

        Action<ProductItem> action;
        public WProductItemDetails(int id, BO.Cart nowCart1, Action<ProductItem> a)
        {
            DetailsOfProductItem = new();
            NowCart = nowCart1;
            DetailsOfProductItem = bl!.Product.GetProductItemDetails(id, NowCart);
            if (DetailsOfProductItem.InStock <= 0)
            {
                isEnabled = false;
            }
            else if(DetailsOfProductItem.InStock > 0) 
            {
                isEnabled = true;
            }
            InitializeComponent();
            action = a;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            NowCart= bl!.Cart.AddProduct(NowCart, DetailsOfProductItem.ID);
            //int newAmount = DetailsOfProductItem.AmoutInYourCart + 1;
            //bl!.Cart.UpdateAmountProduct(NowCart, DetailsOfProductItem.ID, newAmount);
            DetailsOfProductItem = bl!.Product.GetProductItemDetails(DetailsOfProductItem.ID, NowCart);
            action(DetailsOfProductItem);
            Close();
        }
    }
}
