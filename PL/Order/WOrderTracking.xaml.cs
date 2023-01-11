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
    /// Interaction logic for WOrderTracking.xaml
    /// </summary>
    public partial class WOrderTracking : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public int OrderId { get; set; } = 0;
        public BO.OrderTracking OrderTracking1
        {
            get { return (BO.OrderTracking)GetValue(OrderTracking1Property); }
            set { SetValue(OrderTracking1Property, value); }
        }
        public static readonly DependencyProperty OrderTracking1Property = DependencyProperty.Register(nameof(OrderTracking1),
                                                                                                        typeof(BO.OrderTracking),
                                                                                                       typeof(WOrderTracking));
        public WOrderTracking()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (bl != null) OrderTracking1 =  bl.Order.OrderTracking(OrderId);
          
        }

    }
}
