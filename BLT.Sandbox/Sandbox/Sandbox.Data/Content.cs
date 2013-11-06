using System;

namespace Sandbox.Data
{
    public class Content //: EntityBase
    {
        // primary key
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string ContentUrl { get; set; }
        public string ContentType { get; set; }
        public int ContentLength { get; set; }

        // foreign key + relationships
        public Guid RoundId { get; set; }
        public virtual Round Round { get; set; }
    }
}
