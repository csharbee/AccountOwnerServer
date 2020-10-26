using Data.IRepositories;
using Data.Repository;

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
        public IOwnerRepository Owner
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

        public IAccountRepository Account
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
            _context.SaveChanges();
        }
    }
}
