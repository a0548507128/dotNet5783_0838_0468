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
    /// Interaction logic for WOrderForList.xaml
    /// </summary>
    public partial class WOrderForList : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public BO.Order? OrderDetails { get; set; } = new();
        #region ListOfOrderProperty
        public static readonly DependencyProperty ListOfOrder1Property = DependencyProperty.Register(nameof(ListOfOrder1),
                                                                                                              typeof(ObservableCollection<OrderForList?>),
                                                                                                      typeof(WOrderForList));
        public ObservableCollection<OrderForList?> ListOfOrder1
        {
            get { return (ObservableCollection<OrderForList?>)GetValue(ListOfOrder1Property); }
            set { SetValue(ListOfOrder1Property, value); }
        }

        #endregion

        public WOrderForList()
        {
            ListOfOrder1 = new(bl.Order.GetOrderList());
            InitializeComponent();
        }

        private void ListOfOrder_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (OrderDetails is not null)
                new WAddUpdateProduct(OrderDetails.ID).ShowDialog();
        }
    }
}
