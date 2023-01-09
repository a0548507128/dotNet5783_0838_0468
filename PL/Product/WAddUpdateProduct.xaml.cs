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
    #endregion
    public WAddUpdateProduct()
    {
        UpdateAddProduct = new();
        MyContent = "add";
        readOnly=false;
        InitializeComponent();
        //if (s == "add")
        //{
        //    addUpdate.Content = "add";
        //}
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
        //Category= Enum.GetValues(typeof(BO.Enums.ECategory));
        InitializeComponent();

        //id.Text = Convert.ToString(i);
        //id.IsReadOnly = true;
        //name.Text = ProductToUpOrAdd.Name;
        //price.Text = Convert.ToString(ProductToUpOrAdd.Price);
        //inStock.Text = Convert.ToString(ProductToUpOrAdd.InStock);
        //AttributeSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.ECategory));
        //if (s == "update")
        //{
        //    addUpdate.Content = "update";
        //}
        //BO.Product p = bl.Product.GetProductDetailsManager(i);
    }

    private void Button_Click_Add_update(object sender, RoutedEventArgs e)
    {
        try
        {
            //BO.Product p = new()
            //{
            //    ID = int.Parse(id.Text),
            //    Price = double.Parse(price.Text),
            //    InStock = int.Parse(inStock.Text),
            //    Name = name.Text,
            //    Category = (BO.Enums.ECategory?)AttributeSelector.SelectedValue
            //};
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
            Label myLabel = new Label()
            {
                Content = ex.Message,
                Name = "emptyField"
            };
            Grid.SetRow(myLabel, 3);
            MainGrid.Children.Add(myLabel);
        }
        catch (NegativeIdException ex)
        {
            Label myLabel = new Label()
            {
                Content = ex.Message,
                Name="lblid"
            };
            Grid.SetRow(myLabel, 3); //put the label under the invalid textBox
            MainGrid.Children.Add(myLabel);
        }
        catch (EmptyNameException ex)
        {
            Label myLabel = new Label()
            {
                Content = ex.Message,
                Name = "lblname"
            };
            Grid.SetRow(myLabel, 3); //put the label under the invalid textBox
            MainGrid.Children.Add(myLabel);
        }
        catch (NegativePriceException ex)
        {
            Label myLabel = new Label()
            {
                Content = ex.Message,
                Name = "lblprice"
            };
            Grid.SetRow(myLabel, 3); //put the label under the invalid textBox
            MainGrid.Children.Add(myLabel);
        }
        catch (NegativeStockException ex)
        {
            Label myLabel = new Label()
            {
                Content = ex.Message,
                Name = "lblInStock"
            };
            Grid.SetRow(myLabel, 3); //put the label under the invalid textBox
            MainGrid.Children.Add(myLabel);
        }
        catch (ItemAlreadyExistsException ex)
        {
            Label myLabel = new Label()
            {
                Content = ex.Message,
                Name = "alreadyExist"
            };
            Grid.SetRow(myLabel, 3); //put the label under the invalid textBox
            MainGrid.Children.Add(myLabel);
        }
        

    }
    private void removeLabel(string getterName)
    {
        var child1 = MainGrid.Children.OfType<Control>().Where(x => x.Name == getterName).FirstOrDefault();
        if (child1 != null)
            MainGrid.Children.Remove(child1);
    }
    private void id_TextChanged(object sender, TextChangedEventArgs e)
    {
        removeLabel("lblid");
        removeLabel("alreadyExist");
    }
    private void inStock_TextChanged(object sender, TextChangedEventArgs e)
    {
        removeLabel("lblInStock");
    }
    private void price_TextChanged(object sender, TextChangedEventArgs e)
    {
        removeLabel( "lblprice");
    }
    private void name_TextChanged(object sender, TextChangedEventArgs e)
    {
        removeLabel("lblname");
    }
    private void empty_TextChanged(object sender, TextChangedEventArgs e)
    {
        removeLabel("emptyField");
    }
}

