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
           return DbOperations.CreatePlayer(txtbox_firstname.Text, txtbox_lastname.Text, txtbox_nickname.Text);
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
            MessageBox.Show(CheckUserName(txtbox_nickname.Text, txtbox_firstname.Text, txtbox_lastname.Text));
        }
        private string CheckUserName(string nickname, string fname, string lname)
        {
            if (DbOperations.IsBlankNickName(nickname))
            {
                return $"{nickname} är inte ett giltigt smeknamn/spelarnamn";
            }
            else if (DbOperations.IsDuplicateNickname(nickname))
            {
                return $"{nickname} är upptaget! Välj ett annat användarnamn.";
            }
            else
            {
                MainWindow mainwindow = new MainWindow();
                mainwindow.Show();
                this.Close();
                return CreateNewUser();
            }
        }

    }
}
