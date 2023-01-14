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
                PropertyChanged(this, new PropertyChangedEventArgs("Cart"));
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
    public WCart(BO.Cart myCart)
    {
        NowCart = myCart;
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)//+
    {
        var element = e.OriginalSource as FrameworkElement;

        if (element != null && element.DataContext is BO.OrderItem)
        {
            if (NowCart!.ItemList != null && bl != null)
            {
                try
                {
                    NowCart = bl.Cart.UpdateAmountProduct(NowCart, (element.DataContext as BO.OrderItem)!.ID, (element.DataContext as BO.OrderItem)!.Amount + 1);
                    // message = "the amount update succesfully";
                }
                catch
                {

                }
            }
        }
    }
    private void Button_Click_1(object sender, RoutedEventArgs e)//-
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
                   // NavigateToProductCatalog(sender, e);
                    return;
                }
                try
                {
                    //MessageBox.Show(NowCart?.ItemList?.First().Amount.ToString());
                    NowCart = bl.Cart.UpdateAmountProduct(NowCart, (element.DataContext as BO.OrderItem)!.ID, (element.DataContext as BO.OrderItem)!.Amount - 1);
                    // message = "the amount update succesfully";
                  //  MessageBox.Show(NowCart.ItemList.First().Amount.ToString());
                }
                catch
                {

                }
            }
        }
    }
    private void Button_Click_2(object sender, RoutedEventArgs e)//-delete
    {

    }
        private void OrderConfirmation(object sender, RoutedEventArgs e)
    {
        new WOrderConfirmation(NowCart).ShowDialog();
    }
}
