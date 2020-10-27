using Data.IRepositories;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Data.Repository
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(Context context) : base(context)
        {
        }

        public IEnumerable<Account> AccountsByOwner(Guid ownerId)
        {
            return FindByContition(m => m.OwnerId == ownerId).ToList();
        }

        public void CreateAccount(Account account)
        {
            Create(account);
        }

        public Account GetAccountById(Guid id)
        {
            return FindByContition(m => m.Id == id).FirstOrDefault();
        }

        public IEnumerable<Account> GetAccounts()
        {
            return FindAll().OrderBy(m => m.AccountType).ToList();
        }

        public void UpdateAccount(Account account)
        {
            Update(account);
        }
    }
}
