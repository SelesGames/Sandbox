using System;

namespace BLT.ClientExtranet.Data.Models
{
    public class RoundApproval
    {
        // identity primary key
        public int Id { get; protected set; }

        public bool GaveApproval { get; set; }

        // foreign key + relationships
        public Guid RoundId { get; set; }
        public Guid UserId { get; set; }
        public Round Round { get; set; }
        public User User { get; set; }
    }
}
