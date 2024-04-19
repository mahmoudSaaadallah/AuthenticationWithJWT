using DataAccess.Data;
using Domain.IRepository;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        private readonly ApplicationDbContext _context;

        public TeamRepository(ApplicationDbContext context):base (context)
        {
            this._context = context;
        }
        public void Update(Team entity)
        {
            _context.Teams.Update(entity);   
        }
    }
}
