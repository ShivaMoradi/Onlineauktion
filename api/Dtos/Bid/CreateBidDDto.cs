using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Bid
{
    public class CreateBidDDto
    {
        [Required]
        [Range(1, 1000000000)]
        public int BidAmount { get; set; } 

    }
}