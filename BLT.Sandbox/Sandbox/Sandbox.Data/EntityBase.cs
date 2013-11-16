using System;

namespace Sandbox.Data
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }




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