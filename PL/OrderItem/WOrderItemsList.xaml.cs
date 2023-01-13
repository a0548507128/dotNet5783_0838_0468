using BO;
using DO;
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
    /// Interaction logic for WOrderItemsList.xaml
    /// </summary>
    public partial class WOrderItemsList : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public static readonly DependencyProperty OrderForItemProperty = DependencyProperty.Register(nameof(OrderForItem),
                                                                                                           typeof(ObservableCollection<BO.OrderItem?>),
                                                                                                             typeof(WOrderItemsList));
        public ObservableCollection<BO.OrderItem?> OrderForItem
        {
            get { return (ObservableCollection<BO.OrderItem?>)GetValue(OrderForItemProperty); }
            set { SetValue(OrderForItemProperty, value); }
        }

        public WOrderItemsList(List<BO.OrderItem?>? oi)
        {
           // OrderForItem = new ObservableCollection<BO.OrderItem?>((bl.Order.GetOrderById(orderId).OrderItemList!).Cast<BO.OrderItem?>());
            InitializeComponent();
        }
    }
}
