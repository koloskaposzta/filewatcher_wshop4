using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class MainWindow : Window    
    {
        TokenModel token;
         public LoginViewModel user;
        FileSystemWatcher watcher;
        public MainWindow(TokenModel token, LoginViewModel model)
        {
            this.token = token;
            this.user = model;
            watcher = new FileSystemWatcher(model.Path);
            watcher.Created += OnCreated;
            watcher.Deleted += OnDeleted;
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
            InitializeComponent();
        }

        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            string value = $"Created: {e.FullPath}";
            MessageBox.Show(value);
        }

        private static void OnDeleted(object sender, FileSystemEventArgs e)
        {
            string value = ($"Deleted: {e.FullPath} ");
            MessageBox.Show(value);
        }
    }
}
