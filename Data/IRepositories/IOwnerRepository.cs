using Contracts;
using Data.Models;
using System;
using System.Collections.Generic;

namespace Data.IRepositories
{
    public interface IOwnerRepository : IRepositoryBase<Owner>
    {
        IEnumerable<Owner> GetOwnersOrderByName();
        Owner GetOwnerById(Guid Id);
        Owner GetOwnerWithDetails(Guid ownerId);
        void CreateOwner(Owner owner);
        void UpdateOwner(Owner owner);
        void DeleteOwner(Owner owner);
    }
}
