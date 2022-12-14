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
    /// Interaction logic for Login.xaml
    /// </summary>
    /// 
    public class TokenModel
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }
    }
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Path { get; set; }
    }
    public partial class Login : Window
    {


        public Login()
        {
            InitializeComponent();
        }

        private async void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7045/");
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
            );

            var model = new LoginViewModel()
            {
                Username = tb_username.Text,
                Password = tb_password.Password,
                //Path = tb_path.Text,
            };
            var response = await client.PostAsJsonAsync<LoginViewModel>("auth", model);

            var token = await response.Content.ReadAsAsync<TokenModel>();
            token.Expiration = token.Expiration.ToLocalTime();
            model.Path = tb_path.Text;
            MainWindow mw = new MainWindow(token, model);
            mw.ShowDialog();
        }

        private void Register_Button_Click(object sender, RoutedEventArgs e)
        {
            Register rw = new Register();
            rw.ShowDialog();
        }
    }
}
