namespace Onlineauction
{
  using MySql.Data.MySqlClient;
  using System;
  using System.Data;
  using System.Threading.Tasks;

  public class Bid
  {
    public int AuctionId { get; set; }
    public decimal BidAmount { get; set; }
    public string UserId { get; set; }

    public Bid(int auctionId, decimal bidAmount, string userId)
    {
      AuctionId = auctionId;
      BidAmount = bidAmount;
      UserId = userId;
    }

    public static async Task<IResult> CreateBidAsync(Bid bid, MySqlConnection connection)
    {
      if (connection.State != ConnectionState.Open)
      {
        connection.Open();
      }

      try
      {
        var query = "INSERT INTO bid (AuctionId, BidAmount, UserId) VALUES (@AuctionId, @BidAmount, @UserId);";
        using var cmd = new MySqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@AuctionId", bid.AuctionId);
        cmd.Parameters.AddWithValue("@BidAmount", bid.BidAmount);
        cmd.Parameters.AddWithValue("@UserId", bid.UserId);

        var result = await cmd.ExecuteNonQueryAsync();
        if (result > 0)
        {
          return Results.Created($"/bid/{cmd.LastInsertedId}", bid);
        }
        else
        {
          return Results.Problem("Failed to create a bid.");
        }
      }
      catch (Exception ex)
      {
        return Results.Problem(ex.Message);
      }
      finally
      {
        connection.Close();
      }
    }
  }
}
