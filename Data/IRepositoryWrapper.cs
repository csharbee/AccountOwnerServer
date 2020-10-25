using Data.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public interface IRepositoryWrapper
    {
        IOwnerRepository Owner { get; }
        IAccountRepository Account { get; }
        void Commit();
    }
}
