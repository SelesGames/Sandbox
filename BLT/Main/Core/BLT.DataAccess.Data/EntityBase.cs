using System;

namespace BLT.ClientExtranet.Data.Models
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        public virtual byte[] RowVersion { get; set; }

        public EntityBase()
        {
            Id = Guid.NewGuid();
        }




        #region Override object equality (Equals and GetHashCode)

        public override bool Equals(object obj)
        {
            return obj is EntityBase && ((EntityBase)obj).Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion
    }
}