using Data.IRepositories;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private Context _context;
        private IOwnerRepository _owner;
        private IAccountRepository _account;
        public RepositoryWrapper(Context context)
        {
            _context = context;
        }
        public IOwnerRepository OwnerRepository
        {
            get
            {
                if (_owner == null)
                {
                    _owner = new OwnerRepository(_context);
                }
                return _owner;
            }
        }

        public IAccountRepository AccountRepository
        {
            get
            {
                if (_account == null)
                {
                    _account = new AccountRepository(_context);
                }
                return _account;
            }
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }
    }
}
