using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
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

namespace FileWatcherDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    public class WatcherModel
    {
        public string UserName { get; set; }
        public string Message { get; set; }
    }
    public partial class MainWindow : Window ,INotifyPropertyChanged
    {
        TokenModel token;
         public LoginViewModel user;
        FileSystemWatcher watcher;

        public HttpClient client;
        HubConnection conn;

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindow(TokenModel token, LoginViewModel model)
        {
            this.token = token;
            this.user = model;
            watcher = new FileSystemWatcher(model.Path);
            watcher.Created += OnCreated;
            watcher.Deleted += OnDeleted;
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;

            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7045");

            conn = new HubConnectionBuilder().WithUrl("https://localhost:7045/events").Build();
            conn.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await conn.StartAsync();
            };

            conn.On<WatcherModel>("FileChanged", async t => await Refresh(t));

            InitializeComponent();
        }

        private async void OnCreated(object sender, FileSystemEventArgs e)
        {
            string value = $"Created: {e.FullPath}";
            var resp = await client.PostAsJsonAsync("Watcher/Changed", value);
            ;
        }

        private async  void OnDeleted(object sender, FileSystemEventArgs e)
        {
            string value = ($"Deleted: {e.FullPath} ");
            var resp = await client.PostAsJsonAsync("Watcher/Changed", value);
            ;
            
        }
        async Task Refresh(WatcherModel t)
        {
            MessageBox.Show(t.Message);
            //Cars = new ObservableCollection<Car>(await GetCars());
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Cars"));
        }
    }

}
