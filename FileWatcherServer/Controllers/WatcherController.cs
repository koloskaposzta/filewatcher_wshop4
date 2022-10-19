using Microsoft.AspNetCore.Mvc;

namespace FileWatcherServer.Controllers
{
    public class WatcherController : ControllerBase
    {
        string path;
        FileSystemWatcher watcher = new FileSystemWatcher();

        public async void AddPath([FromBody] string path)
        {
           watcher.Path = path;
        }

        public void FolderWatcherChange()
        {
            //watcher.Created += new FileSystemEventHandler(OnChanged);
            //watcher.Changed += new FileSystemEventHandler(OnChanged);
            //watcher.Deleted += new FileSystemEventHandler(OnChanged);

        }
    }
}
