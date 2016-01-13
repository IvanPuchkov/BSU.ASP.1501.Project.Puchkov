using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.DTO;
using DAL.Interface.Repository;

namespace DAL.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; private set; }

        public UnitOfWork(DbContext context)
        {
            Context = context;
        }

        public void Commit()
        {
            try
            {
                Context?.SaveChanges();
            }
            catch(DbUpdateException ex)
            {
                SqlException baseException = ex.GetBaseException() as SqlException;
                if (baseException?.Number == 50000)
                {
                    throw new DalBidTooLowException();
                }
                throw;
            }
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
