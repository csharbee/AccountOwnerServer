using Data.IRepositories;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Data.Repository
{
    public class OwnerRepository : RepositoryBase<Owner>, IOwnerRepository
    {
        public OwnerRepository(Context context) : base(context)
        {
        }

        public IEnumerable<Owner> GetOwnersOrderByName()
        {
            return FindAll().OrderBy(m => m.Name).ToList();
        }
    }
}
