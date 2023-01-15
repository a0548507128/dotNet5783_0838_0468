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


namespace PL
{
    /// <summary>
    /// Interaction logic for WNewOrder.xaml
    /// </summary>
    
    public partial class WNewOrder : Window
    {
        #region property
        BlApi.IBl? bl = BlApi.Factory.Get();
        public System.Array Categories { get; set; } = Enum.GetValues(typeof(BO.Enums.ECategory));
        public Enums.ECategory? selectedCategory { get; set; } = null;
        public BO.ProductItem? ProductitemDetails { get; set; } = new();
        public static readonly DependencyProperty ProductForItemProperty = DependencyProperty.Register(nameof(ProductForItem),
                                                                                                             typeof(ObservableCollection<ProductItem?>),
                                                                                                     typeof(WNewOrder));
        public ObservableCollection<ProductItem?> ProductForItem
        {
            get { return (ObservableCollection<ProductItem?>)GetValue(ProductForItemProperty); }
            set { SetValue(ProductForItemProperty, value); }
        }

        public BO.Cart newCart
        {
            get { return (BO.Cart)GetValue(nowCartProperty); }
            set { SetValue(nowCartProperty, value); }
        }
        public static readonly DependencyProperty nowCartProperty = DependencyProperty.Register(nameof(newCart),
                                                                                                        typeof(BO.Cart),
                                                                                                       typeof(WCart));

        #endregion
        public WNewOrder()
        {
            newCart = new();
            ProductForItem = new(bl.Product.GetProductsItem());

            InitializeComponent();
        }

        private void updateList(ProductItem p)
        {
           var item= ProductForItem.FirstOrDefault(item => item!.ID == p.ID);
            ProductForItem[ProductForItem.IndexOf(item)] = p;
        }
        private void ProductItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
             if (ProductitemDetails is not null)
                new WProductItemDetails(ProductitemDetails.ID, newCart, updateList).ShowDialog();
        }

        private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectedCategory is not null)
                 ProductForItem = new(bl!.Product.GetProductsItem((x) => x?.Category.ToString() == ((Enums.ECategory)selectedCategory)!.ToString()));
        }

        private void To_Cart_Click(object sender, RoutedEventArgs e)
        {
            new WCart(newCart, updateList).ShowDialog();
            //ProductForItem = new(bl!.Product.GetProductsItem());
        }

        private void Group_Category_Click(object sender, RoutedEventArgs e)
        {
            var GropupingProducts = (from p in ProductForItem
                                     group p by p.Category into catGroup
                                     from pr in catGroup
                                     select pr).ToList();
            ProductForItem = new(GropupingProducts);
        }
    }
}
