using System;

namespace EcommerceAPI.Entidades
{
    public abstract class EntidadeBase
    {
        protected EntidadeBase(Guid id)
        {
            Id = id;
        }

        public Guid Id { get;private set; }
    }
}
