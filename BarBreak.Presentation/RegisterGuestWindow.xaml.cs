using BarBreak.Core.DTOs;
using BarBreak.Core.User;
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

namespace BarBreak.Presentation
{
    /// <summary>
    /// Interaction logic for RegisterGuestWindow.xaml
    /// </summary>
    public partial class RegisterGuestWindow : Window
    {
        private readonly IUserService _userService;
        public RegisterGuestWindow()
        {
            InitializeComponent();
            //_userService = userService;
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string firstName = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;
            bool acceptTerms = AcceptTermsCheckBox.IsChecked ?? false;

            // Валідація введених даних
            if (string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                !acceptTerms)
            {
                MessageBox.Show("Please fill in all fields and accept the terms.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                // Логіка реєстрації користувача
                MessageBox.Show("Registration successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
