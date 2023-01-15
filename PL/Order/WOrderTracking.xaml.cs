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

namespace PL;

/// <summary>
/// Interaction logic for WOrderTracking.xaml
/// </summary>
public partial class WOrderTracking : Window
{
    #region property
    BlApi.IBl? bl = BlApi.Factory.Get();
    public string message
    {
        get { return (string)GetValue(messageProperty); }
        set { SetValue(messageProperty, value); }
    }
    public static readonly DependencyProperty messageProperty = DependencyProperty.Register(nameof(message),
                                                                                                    typeof(string),
                                                                                                   typeof(WOrderTracking));
    public Visibility IsVisible1
    {
        get { return (Visibility)GetValue(IsVisible1Property); }
        set { SetValue(IsVisible1Property, value); }
    }
    public static readonly DependencyProperty IsVisible1Property = DependencyProperty.Register(nameof(IsVisible1),
                                                                                                    typeof(Visibility),
                                                                                                   typeof(WOrderTracking));
    public int OrderId { get; set; } = 0;
    #endregion
    public BO.OrderTracking OrderTracking1
    {
        get { return (BO.OrderTracking)GetValue(OrderTracking1Property); }
        set { SetValue(OrderTracking1Property, value); }
    }
    public static readonly DependencyProperty OrderTracking1Property = DependencyProperty.Register(nameof(OrderTracking1),
                                                                                                    typeof(BO.OrderTracking),
                                                                                                   typeof(WOrderTracking));
    public WOrderTracking()
    {
        message = "";
        IsVisible1 = Visibility.Collapsed;
        InitializeComponent();
    }

    private void Show_Status_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (bl != null) OrderTracking1 = bl.Order.OrderTracking(OrderId);
            IsVisible1 = Visibility.Visible;
        }
        catch (OrderNotExistsException ex)
        {
            message = ex.Message;
        }
    }

    private void Order_Details_Click(object sender, RoutedEventArgs e)
    {
        new WOrderDetails(OrderId, "customer").ShowDialog();
    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        message = "";
    }
}