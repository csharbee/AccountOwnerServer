using Data.IRepositories;
using Data.Models;
using Microsoft.EntityFrameworkCore;
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

        public Owner GetOwnerById(Guid Id)
        {
            return FindByContition(m => m.Id == Id).FirstOrDefault();
        }

        public IEnumerable<Owner> GetOwnersOrderByName()
        {
            return FindAll().OrderBy(m => m.Name).ToList();
        }

        public Owner GetOwnerWithDetails(Guid ownerId)
        {
            return FindByContition(m => m.Id == ownerId)
                    .Include(m => m.Accounts)
                    .FirstOrDefault();
        }
    }
}
