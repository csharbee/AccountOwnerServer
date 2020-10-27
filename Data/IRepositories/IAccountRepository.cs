using Contracts;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.IRepositories
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        IEnumerable<Account> GetAccounts();
        IEnumerable<Account> AccountsByOwner(Guid ownerId);
        void CreateAccount(Account account);
        void UpdateAccount(Account account);

        Account GetAccountById(Guid id);
    }
}
