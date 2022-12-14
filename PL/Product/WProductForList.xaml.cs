using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
    /// Interaction logic for exhibition.xaml
    /// </summary>
    public partial class WProductForList : Window
    {
        private BlApi.IBl bl = new BlImplementation.Bl();
             
        public WProductForList()
        {
            InitializeComponent();
            ListOfProduct1.ItemsSource = bl.Product.GetProductsList();
            AttributeSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.ECategory));
        }

        private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DO.Enums.Category selector = (DO.Enums.Category)AttributeSelector.SelectedItem;
            ListOfProduct1.ItemsSource = bl.Product.GetProductsList((x)=> x!.Value.Category == selector);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new WAddProduct().ShowDialog();
            ListOfProduct1.ItemsSource = bl.Product.GetProductsList();
        }
    }
}
