
using Microsoft.AspNetCore.SignalR;
using Model.Entity;

namespace SignalR;

public class AuctionHub : Hub {
    public async Task SendBet(int auctionId) {
        await Clients.All.SendAsync($"ReceiveBet-{auctionId}");
    }
}
