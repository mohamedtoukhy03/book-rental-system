using System;
using System.Collections.Generic;

namespace BookRentalSystem.DataAccess.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string ISBN { get; set; }
        public int AvailableCopies { get; set; }
        public int TotalCopies { get; set; }
        public ICollection<RentalRequest> RentalRequests { get; set; } = new List<RentalRequest>();
    }
}
