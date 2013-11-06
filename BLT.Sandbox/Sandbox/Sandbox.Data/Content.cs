using System;

namespace Sandbox.Data
{
    public class Content
    {
        // surrogate and primary key
        public int Id { get; set; }
        public Guid ContentId { get; set; }

        public string Name { get; set; }
        public string ContentUrl { get; set; }
        public string ContentType { get; set; }
        public int ContentLength { get; set; }

        // foreign key + relationships
        public Guid RoundId { get; set; }
        public virtual Round Round { get; set; }
    }
}
