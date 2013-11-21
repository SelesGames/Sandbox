using System;

namespace Sandbox.Data
{
    public class UserProjectPermission
    {
        // identity primary key
        public int Id { get; protected set; }

        public UserProjectRole Role; // Approver, Normal

        // foreign key + relationships
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
        public virtual User User { get; set; }
        public virtual Project Project { get; set; }
    }

    public enum UserProjectRole
    {
        Normal,
        Approver
    }
}
