using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
    /// Interaction logic for WOrderConfirmation.xaml
    /// </summary>
    public partial class WOrderConfirmation : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        #region cart property
        public BO.Cart NowCart
        {
            get { return (BO.Cart)GetValue(nowCartProperty); }
            set { SetValue(nowCartProperty, value); }
        }
        public static readonly DependencyProperty nowCartProperty = DependencyProperty.Register(nameof(NowCart),
                                                                                                        typeof(BO.Cart),
                                                                                                       typeof(WOrderConfirmation));
        #endregion
        public string CustomerName { get; set; } = "";
        public string CustomerEmail { get; set; } = "";
        public string CustomerAdress { get; set; } = "";
        public WOrderConfirmation(BO.Cart c)
        {
            NowCart=c;
            InitializeComponent();
        }

        private void confirm_Click(object sender, RoutedEventArgs e)
        {
            bl!.Cart.OrderConfirmation(NowCart, CustomerName, CustomerEmail, CustomerAdress);
            MessageBox.Show("The order is complete!");
            this.Close();
        }
    }
}
