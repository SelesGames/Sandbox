using System;
using System.Collections.Generic;

namespace Sandbox.Data
{
    public class Round : EntityBase
    {
        public int FileCount { get; set; }
        public DateTime CreatedOn { get; set; }

        // foreign key + relationships
        public Guid ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public virtual ICollection<Content> Contents { get; set; }

        public Round()
        {
            Contents = new List<Content>();
        }




        #region Add/Remove methods

        public void Add(Content content)
        {
            content.Round = this;
            Contents.Add(content);
        }

        public void Remove(Content content)
        {
            Contents.Remove(content);
        }

        #endregion
    }
}
