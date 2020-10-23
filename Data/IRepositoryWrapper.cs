using Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public interface IRepositoryWrapper
    {
        IOwnerRepository OwnerRepository { get; }
        IAccountRepository AccountRepository { get; }
        void Commit();
    }
}
