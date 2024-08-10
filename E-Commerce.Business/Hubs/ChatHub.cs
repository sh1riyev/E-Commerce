using System;
using E_Commerce.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace E_Commerce.Business.Hubs
{
	public class ChatHub : Hub
    {
        private readonly UserManager<AppUser> _userManager;
        public ChatHub(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task SendMessageAsync(string message, string fromId, string toId)
        {
            await Clients.All.SendAsync("resieveMessage", message, fromId, toId);
        }
        public async Task UserTyping(string userId)
        {
            await Clients.All.SendAsync("recieveTyping", userId);
        }
        public override Task OnConnectedAsync()
        {
            string userId = Context.UserIdentifier;
            UpdateUserStatus(userId, true, "OnConnect");
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            string userId = Context.UserIdentifier;
            UpdateUserStatus(userId, false, "DisConnect");
            return base.OnDisconnectedAsync(exception);
        }

        private async Task UpdateUserStatus(string userId, bool isOnline, string message)
        {
            AppUser appUser = _userManager.FindByIdAsync(userId).Result;
            appUser.isOnline = isOnline;
            appUser.ConnectionId = Context.ConnectionId;
            var result = _userManager.UpdateAsync(appUser).Result;
            await Clients.All.SendAsync(message, appUser.Id);
        }
    }
}

