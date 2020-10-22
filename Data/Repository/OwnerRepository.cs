using Data.IRepositories;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Data.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        public void Create(Owner model)
        {
            throw new NotImplementedException();
        }

        public void Delete(Owner model)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Owner> FindAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Owner> FindByContition(Expression<Func<Owner, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void Update(Owner model)
        {
            throw new NotImplementedException();
        }
    }
}
