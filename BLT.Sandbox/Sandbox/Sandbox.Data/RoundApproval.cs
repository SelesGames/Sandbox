using System;

namespace Sandbox.Data
{
    public class RoundApproval
    {
        public int Id { get; set; }
        public bool GaveApproval { get; set; }

        // foreign key + relationships
        public Guid RoundId { get; set; }
        public Guid UserId { get; set; }
        public Round Round { get; set; }
        public User User { get; set; }
    }
}
