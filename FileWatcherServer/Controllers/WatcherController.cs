using FileWatcherServer.Hubs;
using FileWatcherServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.FileSystemGlobbing;

namespace FileWatcherServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WatcherController : ControllerBase
    {
        static List<WatcherModel> watchers = new List<WatcherModel>();
        IHubContext<EventHub> hub;
        UserManager<IdentityUser> _userManager;

        public WatcherController(IHubContext<EventHub> hub, UserManager<IdentityUser> userManager)
        {
            this.hub = hub;
            _userManager = userManager;
        }
        [HttpPost]
        [Route("[action]")]
        public async void Changed([FromBody] string message)
        {
            WatcherModel wm = new WatcherModel();
            wm.UserName= this.User.Identity.Name;
            wm.Message = message;
            watchers.Add(wm);
            await hub.Clients.All.SendAsync("FileChanged", wm);
        }

        [HttpGet]
        public IEnumerable<WatcherModel> Watchers()
        {
            return watchers;
        }
    }

}
