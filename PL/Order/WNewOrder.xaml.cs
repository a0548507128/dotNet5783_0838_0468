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
        // public BO.Cart newCart { get; set; } = new();
        
        //public List<ProductItem?> GropupingProducts = (from p in ProductForItem
        //                                               group p by p.Category into catGroup
        //                                               from pr in catGroup
        //                                               select pr).ToList();
        public WNewOrder()
        {
            newCart = new();
            ProductForItem = new(bl.Product.GetProductsItem());

            InitializeComponent();
        }

        private void ProductItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
             if (ProductitemDetails is not null)
                new WProductItemDetails(ProductitemDetails.ID, newCart).ShowDialog();
        }

        private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selectedCategory is not null)
                if (bl != null) ProductForItem = new(bl.Product.GetProductsItem((x) => x?.Category.ToString() == ((Enums.ECategory)selectedCategory)!.ToString()));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new WCart(newCart).ShowDialog();

        }
    }
}
