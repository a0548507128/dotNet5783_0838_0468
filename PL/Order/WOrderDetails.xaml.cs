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
        public BO.Order DelailsOfOrder
        {
            get { return (BO.Order)GetValue(DelailsOfOrderProperty); }
            set { SetValue(DelailsOfOrderProperty, value); }
        }
        public static readonly DependencyProperty DelailsOfOrderProperty = DependencyProperty.Register(nameof(DelailsOfOrder),
                                                                                                        typeof(BO.Order),
                                                                                                       typeof(WOrderDetails));
        public WOrderDetails(int id)
        {
            DelailsOfOrder=new();
            InitializeComponent();
        }
    }
}
