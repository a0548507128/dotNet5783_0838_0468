using BO;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace PL;

/// <summary>
/// Interaction logic for WCart.xaml
/// </summary>
public partial class WCart : Window, INotifyPropertyChanged
{
    BlApi.IBl? bl = BlApi.Factory.Get();

    public event PropertyChangedEventHandler? PropertyChanged;

    private BO.Cart? Cart;
    public BO.Cart? NowCart
    {
        get { return Cart; }
        set
        {
            Cart = value;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(NowCart)));
        }
    }

    //public BO.Cart NowCart
    //{
    //    get { return (BO.Cart)GetValue(nowCartProperty); }
    //    set { SetValue(nowCartProperty, value); }
    //}
    //public static readonly DependencyProperty nowCartProperty = DependencyProperty.Register(nameof(NowCart),
    //                                                                                                typeof(BO.Cart),
    //                                                                                               typeof(WCart));
    public BO.ProductItem DetailsOfProductItem
    {
        get { return (BO.ProductItem)GetValue(DetailsOfProductItemProperty); }
        set { SetValue(DetailsOfProductItemProperty, value); }
    }
    public static readonly DependencyProperty DetailsOfProductItemProperty = DependencyProperty.Register(nameof(DetailsOfProductItem),
                                                                                                    typeof(BO.ProductItem),
                                                                                                   typeof(WCart));

    public bool isEnabled
    {
        get { return (bool)GetValue(isEnabledProperty); }
        set { SetValue(isEnabledProperty, value); }
    }
    public static readonly DependencyProperty isEnabledProperty = DependencyProperty.Register(nameof(isEnabled),
                                                                                                    typeof(bool),
                                                                                                   typeof(WCart));
    Action<ProductItem> action;
    public WCart(BO.Cart myCart, Action<ProductItem> a)
    {
        NowCart = myCart;
        DetailsOfProductItem = new();
        if (NowCart?.ItemList?.Count == 0) isEnabled = false;
        else if(NowCart?.ItemList?.Count != 0) isEnabled = true;
        InitializeComponent();
        action = a;
    }

    private void plus_Click(object sender, RoutedEventArgs e)//+
    {
        var element = e.OriginalSource as FrameworkElement;

        if (element != null && element.DataContext is BO.OrderItem)
        {
            if (NowCart!.ItemList != null && bl != null)
            {
                NowCart = bl.Cart.UpdateAmountProduct(NowCart, (element.DataContext as BO.OrderItem)!.ID, (element.DataContext as BO.OrderItem)!.Amount + 1);
                // message = "the amount update succesfully";
            }
        }
        DetailsOfProductItem.ID= (element!.DataContext as BO.OrderItem)!.ID;
        DetailsOfProductItem = bl!.Product.GetProductItemDetails(DetailsOfProductItem.ID, NowCart!);
        action(DetailsOfProductItem);
    }
    private void minus_Click(object sender, RoutedEventArgs e)//-
    {
        var element = e.OriginalSource as FrameworkElement;

        if (element != null && element.DataContext is BO.OrderItem)
        {
            if (NowCart!.ItemList != null && bl != null)
            {
                if ((element.DataContext as BO.OrderItem)!.Amount == 1)
                {
                    NowCart.ItemList.RemoveAll(item => item?.ID == (element.DataContext as BO.OrderItem)!.ID);
                    MessageBox.Show("the order item removed");
                    DetailsOfProductItem.ID = (element!.DataContext as BO.OrderItem)!.ID;
                    DetailsOfProductItem = bl!.Product.GetProductItemDetails(DetailsOfProductItem.ID, NowCart!);
                    action(DetailsOfProductItem);
                    // NavigateToProductCatalog(sender, e);
                    return;
                }

               // MessageBox.Show(NowCart?.ItemList?.First()?.Amount.ToString());
                NowCart = bl.Cart.UpdateAmountProduct(NowCart!, (element.DataContext as BO.OrderItem)!.ID, (element.DataContext as BO.OrderItem)!.Amount - 1);
                // message = "the amount update succesfully";
                //MessageBox.Show(NowCart?.ItemList?.First()?.Amount.ToString());
            }
        }
        DetailsOfProductItem.ID = (element!.DataContext as BO.OrderItem)!.ID;
        DetailsOfProductItem = bl!.Product.GetProductItemDetails(DetailsOfProductItem.ID, NowCart!);
        action(DetailsOfProductItem);
    }
    private void delete_product_Click(object sender, RoutedEventArgs e)//-delete
    {
        var element = e.OriginalSource as FrameworkElement;
        if (element != null && element.DataContext is BO.OrderItem)
        {
            var orderItem = (BO.OrderItem)element.DataContext;

            NowCart = bl!.Cart.UpdateAmountProduct(NowCart!, orderItem.ID, 0);


            DetailsOfProductItem.ID = orderItem.ID;
            DetailsOfProductItem = bl!.Product.GetProductItemDetails(DetailsOfProductItem.ID, NowCart!);
            action(DetailsOfProductItem);
        }
    }
    private void OrderConfirmation(object sender, RoutedEventArgs e)
    {
        
            new WOrderConfirmation(NowCart!).ShowDialog();
        //NowCart= null;
    }
}
