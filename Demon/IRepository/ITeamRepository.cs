﻿using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepository
{
    public interface ITeamRepository : IRepository<Team>
    {
        void Update(Team entity);
    }
}
