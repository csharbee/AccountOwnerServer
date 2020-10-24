using Contracts;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.IRepositories
{
    public interface IOwnerRepository: IRepositoryBase<Owner>
    {
        IEnumerable<Owner> GetOwnersOrderByName();
    }
}
