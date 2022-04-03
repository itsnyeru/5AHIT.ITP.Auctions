
using Microsoft.AspNetCore.SignalR;

namespace SignalR;

public class AuctionHub : Hub {
    public async Task SendBet(int auctionId, string name, decimal price) {
        await Clients.All.SendAsync($"ReceiveBet-{auctionId}", auctionId, name, price);
    }
}
