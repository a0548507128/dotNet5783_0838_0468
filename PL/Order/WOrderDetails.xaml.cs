using BO;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for WOrderDetails.xaml
    /// </summary>
    public partial class WOrderDetails : Window
    {
        #region property
        BlApi.IBl? bl = BlApi.Factory.Get();
        #region DelailsOfOrderProperty
        public BO.Order DelailsOfOrder
        {
            get { return (BO.Order)GetValue(DelailsOfOrderProperty); }
            set { SetValue(DelailsOfOrderProperty, value); }
        }
        public static readonly DependencyProperty DelailsOfOrderProperty = DependencyProperty.Register(nameof(DelailsOfOrder),
                                                                                                        typeof(BO.Order),
                                                                                                         typeof(WOrderDetails));
        #endregion
        #region IsVisibleShipDateProperty
        public Visibility IsVisibleShipDate
        {
            get { return (Visibility)GetValue(IsVisibleShipDateProperty); }
            set { SetValue(IsVisibleShipDateProperty, value); }
        }
        public static readonly DependencyProperty IsVisibleShipDateProperty = DependencyProperty.Register(nameof(IsVisibleShipDate),
                                                                                                        typeof(Visibility),
                                                                                                       typeof(WOrderDetails));                                                                        
        #endregion
        #region IsVisibleDeliveryDateProperty
        public Visibility IsVisibleDeliveryDate
        {
            get { return (Visibility)GetValue(IsVisibleDeliveryDateProperty); }
            set { SetValue(IsVisibleDeliveryDateProperty, value); }
        }
        public static readonly DependencyProperty IsVisibleDeliveryDateProperty = DependencyProperty.Register(nameof(IsVisibleDeliveryDate),
                                                                                                        typeof(Visibility),
                                                                                                       typeof(WOrderDetails));                                                                        
        #endregion
        #endregion
        public WOrderDetails(int id, string win)
        {
            DelailsOfOrder = new();
            if (bl != null)
            {
                DelailsOfOrder = bl.Order.GetOrderDetails(id);
            }
            DelailsOfOrder.ShipDate = null;
            if (win == "maneger")
            {
                if (DelailsOfOrder.ShipDate == null)
                    IsVisibleShipDate = Visibility.Visible;
                else
                    IsVisibleShipDate = Visibility.Collapsed;

                if (DelailsOfOrder.DeliveryDate == null)
                    IsVisibleDeliveryDate = Visibility.Visible;
                else
                    IsVisibleDeliveryDate = Visibility.Collapsed;
            }
            else if(win== "customer")
            {
                IsVisibleShipDate = Visibility.Collapsed;
                IsVisibleDeliveryDate = Visibility.Collapsed;
            }
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)//ShipDate
        {
            //DelailsOfOrder.ShipDate = DateTime.Now;
            if (bl != null) DelailsOfOrder = bl.Order.OrderShippingUpdate(DelailsOfOrder.ID);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//DeliveryDate
        {
            if (bl != null) DelailsOfOrder = bl.Order.OrderDeliveryUpdate(DelailsOfOrder.ID);
        }
    }
}
