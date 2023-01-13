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
    /// Interaction logic for WCart.xaml
    /// </summary>
    public partial class WCart : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public BO.Cart NowCart
        {
            get { return (BO.Cart)GetValue(nowCartProperty); }
            set { SetValue(nowCartProperty, value); }
        }
        public static readonly DependencyProperty nowCartProperty = DependencyProperty.Register(nameof(NowCart),
                                                                                                        typeof(BO.Cart),
                                                                                                       typeof(WCart));
        public WCart(BO.Cart nowCart1)
        {
            NowCart = nowCart1;
            InitializeComponent();
        }

  
    }
}
