using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Models
{   
    [Table("Auctions")]
    public class Auction
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public long HighestBid { get; set; }


        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool Status { get; set; } = false;
        public DateTime CreatedOn { get; set; } = DateTime.Now;


        // navigation properties - will not be generated in the db:
        public Car Car { get; set; } = null!;
        public List<Bid>? Bids { get; set; }
    }
}