using BlImplementation;
using BO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using static DO.Enums;

namespace PL
{
    /// <summary>
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class WAddProduct : Window
    {
        private BlApi.IBl bl = new BlImplementation.Bl();

        public WAddProduct(string s)
        {
            InitializeComponent();
            if (s == "add")
            {
                addUpdate.Content = "add";
            }
            AttributeSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.ECategory));
        }
        public WAddProduct(string s, int i)
        {
            InitializeComponent();
            if (s == "update")
            {
                addUpdate.Content = "update";
            }
            id.Text = Convert.ToString(i);
            id.IsReadOnly=true; 
            AttributeSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.ECategory));
       
        }

        private void Button_Click_Add_update(object sender, RoutedEventArgs e)
        {
            try
            {
                if (addUpdate.Content == "add")
                {
                    BO.Product p = new()
                    {
                        ID = int.Parse(id.Text),
                        Price = double.Parse(price.Text),
                        InStock = int.Parse(inStock.Text),
                        Name = name.Text,
                        Category = (BO.Enums.ECategory?)AttributeSelector.SelectedValue
                    };
                    bl.Product.AddProductManager(p);

                }
                if (addUpdate.Content == "update")
                {

                    BO.Product p = new()
                    {
                        ID = int.Parse(id.Text),
                        Price = double.Parse(price.Text),
                        InStock = int.Parse(inStock.Text),
                        Name = name.Text,
                        Category = (BO.Enums.ECategory?)AttributeSelector.SelectedValue
                    };
                    bl.Product.UpdateProductManager(p);

                }
                this.Close();
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

        private void id_TextChanged(object sender, TextChangedEventArgs e)
        {
            removeLabel("lblid");
            removeLabel("alreadyExist");
            

        }
        private void removeLabel(string getterName)
        {
            var child1 = MainGrid.Children.OfType<Control>().Where(x => x.Name == getterName).FirstOrDefault();
            if (child1 != null)
                MainGrid.Children.Remove(child1);
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
    }



}

