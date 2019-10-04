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

namespace HampesYatzy
{
    /// <summary>
    /// Interaction logic for NewUser.xaml
    /// </summary>
    public partial class NewUser : Window
    {
        public NewUser()
        {
            InitializeComponent();
        }
        private string CreateNewUser()
        {
           return DbOperations.CreatePlayer(txtFirstname.Text, txtLastname.Text, txtNickname.Text);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Rating rating = new Rating();
            rating.Show();
            this.Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
            this.Close();
        }

        private void BtnCreateUser_Click(object sender, RoutedEventArgs e)
        {
            CheckUserName(txtNickname.Text, txtFirstname.Text, txtLastname.Text);
        }
        private void CheckUserName(string nickname, string fname, string lname)
        {
            if (DbOperations.IsBlankName(nickname))
            {
                MessageBox.Show($"{nickname} är inte ett giltigt smeknamn/spelarnamn");
            }
            else if (DbOperations.IsDuplicateNickname(nickname))
            {
                MessageBox.Show($"{nickname} är upptaget! Välj ett annat användarnamn.");
            }
            else
            {
                MessageBox.Show(CreateNewUser());
                MainWindow mainwindow = new MainWindow();
                mainwindow.Show();
                this.Close();
            }
        }

    }
}
