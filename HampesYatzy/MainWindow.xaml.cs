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


namespace HampesYatzy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DiceTest();
        }
        private void DiceTest()
        {
            int nr = 1;
            BitmapImage image = new BitmapImage(new Uri(@"Resources\d" + nr.ToString() + ".png", UriKind.Relative)); //skapar en bitmappsbild av en tärning
            dice1.Source = image; //gör ovanstående bild till source till en tom image i gränssnittet
            dice2.Source = image;
            dice3.Source = image;
            dice4.Source = image;
            dice5.Source = image;
        }
      
    }
}
