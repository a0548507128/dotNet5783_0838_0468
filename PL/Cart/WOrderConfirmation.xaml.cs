using BO;
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

        #region cart property
        BlApi.IBl? bl = BlApi.Factory.Get();

        public BO.Cart NowCart
        {
            get { return (BO.Cart)GetValue(nowCartProperty); }
            set { SetValue(nowCartProperty, value); }
        }
        public static readonly DependencyProperty nowCartProperty = DependencyProperty.Register(nameof(NowCart),
                                                                                                        typeof(BO.Cart),
                                                                                                       typeof(WOrderConfirmation));
        public string message
        {
            get { return (string)GetValue(messageProperty); }
            set { SetValue(messageProperty, value); }
        }
        public static readonly DependencyProperty messageProperty = DependencyProperty.Register(nameof(message),
                                                                                                        typeof(string),
                                                                                                       typeof(WOrderConfirmation));
        public string? CustomerName { get; set; } = null;
        public string? CustomerEmail { get; set; } = null;
        public string? CustomerAdress { get; set; } = null;
        #endregion

        public WOrderConfirmation(BO.Cart c)
        {
            NowCart=c;
            InitializeComponent();
        }

        private void confirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl!.Cart.OrderConfirmation(NowCart, CustomerName!, CustomerEmail!, CustomerAdress!);
                MessageBox.Show("The order is complete!");
                Close();
            }
            catch(BO.NameIsNullException ex)
            {
                message=ex.Message;
            }
            catch(BO.AdressIsNullException ex)
            {
                message = ex.Message;
            }
            catch(Exception ex)
            {
                message = ex.Message;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            message = "";
        }
    }
}
