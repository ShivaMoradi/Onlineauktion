using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{
    [Table("Bids")]
    public class Bid
    {
        public int Id { get; set; }
        public int BidAmount { get; set; } 
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int AuctionId { get; set; }
        public int UserId { get; set; }
    }
}