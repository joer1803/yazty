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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HampesYatzy.View
{
    /// <summary>
    /// Interaction logic for Lobby.xaml
    /// </summary>
    public partial class Lobby : Page
    {
        public Lobby()
        {
            InitializeComponent();
        }

        private void Controlled_btn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Play());
        }

        private void Classic_btn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Play());
        }
    }
}
