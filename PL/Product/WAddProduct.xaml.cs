using BlImplementation;
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
            AttributeSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.ECategory));
        }

        private void Button_Click_Add_update(object sender, RoutedEventArgs e)
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
                this.Close();
            }
            if(addUpdate.Content == "update")
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
                this.Close();
            }
           
        }



    }
}
