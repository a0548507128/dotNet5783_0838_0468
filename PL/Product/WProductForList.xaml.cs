using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
namespace PL
{

    /// <summary>
    /// Interaction logic for exhibition.xaml
    /// </summary>
   
    public partial class WProductForList : Window
    {
        #region proprty
        BlApi.IBl? bl = BlApi.Factory.Get();
        public System.Array Categories { get; set; } = Enum.GetValues(typeof(BO.Enums.ECategory));
        public Enums.ECategory? selectedCategory { get; set; } = null;
        public BO.ProductForList? updateProduct { get; set; } = new();
        #region ListOfProductProperty
        public static readonly DependencyProperty ListOfProductProperty = DependencyProperty.Register(nameof(ListOfProduct),
                                                                                                               typeof(ObservableCollection<ProductForList?>),
                                                                                                       typeof(WProductForList));
        public ObservableCollection<ProductForList?> ListOfProduct
        {
            get { return (ObservableCollection<ProductForList?>)GetValue(ListOfProductProperty); }
            set { SetValue(ListOfProductProperty, value); }
        }
        #endregion
        #endregion


        public WProductForList()
        {
            ListOfProduct =new( bl.Product.GetProductsList());
            InitializeComponent();
            //ListOfProduct1.ItemsSource = bl.Product.GetProductsList();
            //AttributeSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.ECategory));
        }

        private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectedCategory is not null)
            if(bl!=null) ListOfProduct = new(bl.Product.GetProductsList((x) => x?.Category.ToString() == ((Enums.ECategory)selectedCategory)!.ToString()));
            //DO.Enums.Category selector = (DO.Enums.Category)AttributeSelector.SelectedItem;
            //ListOfProduct1.ItemsSource = bl?.Product.GetProductsList((x) => x!.Value.Category == selector);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new WAddUpdateProduct().ShowDialog();
            if (bl != null) ListOfProduct = new(bl.Product.GetProductsList());
          //ListOfProduct1.ItemsSource = bl?.Product.GetProductsList();
        }

        private void ListOfProduct1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (updateProduct is not null)
                new WAddUpdateProduct(updateProduct.ID).ShowDialog();
            //int id = 0;
            //if(ListOfProduct1.SelectedItem is BO.ProductForList list)
            //{
            //    id=list.ID;
            //}
            //new WAddUpdateProduct("update", id).ShowDialog();
            if (bl != null) ListOfProduct = new(bl.Product.GetProductsList());
            // ListOfProduct1.ItemsSource = bl?.Product.GetProductsList();
        }
    }
}
