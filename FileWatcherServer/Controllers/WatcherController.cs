﻿using FileWatcherServer.Hubs;
using FileWatcherServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace FileWatcherServer.Controllers
{
    public class WatcherController : ControllerBase
    {
        IHubContext<EventHub> hub;

        [HttpPost]
        public async void Changed([FromBody] string message)
        {
            WatcherModel wm=new WatcherModel();
            wm.UserName= this.User.Identity.Name;
            wm.Message = message;
            await hub.Clients.All.SendAsync("Changed", wm);
        }
    }
}
