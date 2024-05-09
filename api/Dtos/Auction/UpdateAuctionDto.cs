using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Auction
{
    public class UpdateAuctionDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Auction Title must be atleast 5 characters")]
        public string Title { get; set; } = string.Empty;

    }
}