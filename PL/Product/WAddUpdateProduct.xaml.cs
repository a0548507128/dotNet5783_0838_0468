using BlImplementation;
using BO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.Pkcs;
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
using System.Text.RegularExpressions;
using static DO.Enums;

namespace PL;

/// <summary>
/// Interaction logic for AddProduct.xaml
/// </summary>
public partial class WAddUpdateProduct : Window
{
    #region property
    BlApi.IBl? bl = BlApi.Factory.Get();
    public System.Array Categories { get; set; } = Enum.GetValues(typeof(BO.Enums.ECategory));
    public bool readOnly { get; set; } = true;
    public static string MyContent { get; set; } = "add";
    public BO.Product UpdateAddProduct
    {
        get { return (BO.Product)GetValue(UpdateAddProductProperty); }
        set { SetValue(UpdateAddProductProperty, value); }
    }
    public static readonly DependencyProperty UpdateAddProductProperty = DependencyProperty.Register(nameof(UpdateAddProduct),
                                                                                                    typeof(BO.Product),
                                                                                                   typeof(WAddUpdateProduct));
    public string message
    {
        get { return (string)GetValue(messageProperty); }
        set { SetValue(messageProperty, value); }
    }
    public static readonly DependencyProperty messageProperty = DependencyProperty.Register(nameof(message),
                                                                                                    typeof(string),
                                                                                                   typeof(WAddUpdateProduct));
    #endregion
    public WAddUpdateProduct()
    {
        UpdateAddProduct = new();
        MyContent = "add";
        readOnly=false;
        InitializeComponent();
    }
    public WAddUpdateProduct( int i)
    {
        UpdateAddProduct = new();
        if (bl != null)
        {
            UpdateAddProduct = bl.Product.GetProductDetailsManager(i);
        }
        MyContent = "update";
        readOnly = true;
        InitializeComponent();
    }

    private void Button_Click_Add_update(object sender, RoutedEventArgs e)
    {
        try
        {
            if (MyContent == "add")
            {
                bl?.Product.AddProductManager(UpdateAddProduct);
            }
            if (MyContent == "update")
            {
                bl?.Product.UpdateProductManager(UpdateAddProduct);
            }
            this.Close();
        }
        catch (ProductInUseException ex)
        {
            message = ex.Message;
        }
        catch (NegativeIdException ex)
        {
            message = ex.Message;
        }
        catch (EmptyNameException ex)
        {
            message = ex.Message;
        }
        catch (NegativePriceException ex)
        {
            message = ex.Message;
        }
        catch (NegativeStockException ex)
        {
            message = ex.Message;
        }
        catch (ItemAlreadyExistsException ex)
        {
            message = ex.Message;
        }
    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        message = "";
    }
}

