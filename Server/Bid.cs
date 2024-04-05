using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Onlineauction
{
  public class BidData
  {
    public record Bid(string id, int auctionId, decimal bidAmount, string userId);

    public static List<Bid> All(State state)
    {
      List<Bid> bids = new();
      string strInfo = "";
      MySqlCommand command = new("SELECT * FROM bid", state.DB);

      using var reader = command.ExecuteReader();
      while (reader.Read())
      {
        string id = reader.GetString("id"); 
        int auctionId = reader.GetInt32("auctionId");
        decimal bidAmount = reader.GetDecimal("bidAmount");
        string userId = reader.GetString("userId");
        bids.Add(new Bid(id, auctionId, bidAmount, userId));

        strInfo += $"Bid id: {id}, Auction ID: {auctionId}, Bid Amount: {bidAmount}, User ID: {userId}\n";
        Console.WriteLine(strInfo);
      }
      return bids;
    }
  }
}
