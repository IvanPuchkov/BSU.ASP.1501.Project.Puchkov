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
            bool concurrencySaveFail = false;
            do
            {
                try
                {
                    Context?.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    concurrencySaveFail = true;
                    ex.Entries.Single().Reload();
                }
                catch (DbUpdateException ex)
                {
                    SqlException baseException = ex.GetBaseException() as SqlException;
                    if (baseException?.Number == 50000)
                    {
                        throw new DalBidTooLowException();
                    }
                    throw;
                }
            } while (concurrencySaveFail);
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
