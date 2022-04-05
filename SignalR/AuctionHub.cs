
using Microsoft.AspNetCore.SignalR;
using Model.Entity;

namespace SignalR;

public class AuctionHub : Hub {
    public async Task SendBet(BiddingAuctionBid bid) {
        await Clients.All.SendAsync($"ReceiveBet-{bid.Auction.Id}", bid);
    }
}
