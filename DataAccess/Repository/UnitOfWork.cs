using DataAccess.Data;
using Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ITeamRepository Teams { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            this._context = context;
            Teams = new TeamRepository(context);   
        }


        public void save()
        {
           _context.SaveChanges();
        }
    }
}
