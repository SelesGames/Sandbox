using System;

namespace BLT.ClientExtranet.Data.Models
{
    public class Content : EntityBase
    {
        public int ContentIndex { get; set; }
        public string Description { get; set; }
        public string ContentUrl { get; set; }
        public string ContentType { get; set; }
        public int ContentLength { get; set; }

        // foreign key + relationships
        public Guid RoundId { get; set; }
        public virtual Round Round { get; set; }
    }
}
