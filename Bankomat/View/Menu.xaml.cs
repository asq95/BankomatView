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

namespace Bankomat.View
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);
            GridCursor.Margin = new Thickness(10 + (200 * index), 0, 0, 0);
            switch (index)
            {
                case 0:
                    Wyplac.Visibility = Visibility.Visible;
                    Saldo.Visibility = Visibility.Collapsed;
                    Pin.Visibility = Visibility.Collapsed;
                    
                    break;
                case 1:
                    Wyplac.Visibility = Visibility.Collapsed;
                    Saldo.Visibility = Visibility.Visible;
                    Pin.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    Wyplac.Visibility = Visibility.Collapsed;
                    Saldo.Visibility = Visibility.Collapsed;
                    Pin.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }
    }
}
