using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace FileWatcherDesktop
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public class RegisterViewModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
        private RegisterViewModel model = new RegisterViewModel();
        public Register()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tb_password.Password != tb_password2.Password)
            {
                MessageBox.Show("Passwords not match!", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:5095");
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );

                var response = await client.PutAsJsonAsync<RegisterViewModel>("auth", model);
                model.Email = tb_email.Text;
                model.Password = tb_password.Password;

                if (response.IsSuccessStatusCode)
                {
                    var result = MessageBox.Show("Registration succesful", "Info", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    this.DialogResult = true;
                }
            }
        }
    }
}
