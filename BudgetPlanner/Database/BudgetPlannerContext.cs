using BudgetPlanner.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetPlanner.Database
{
    public class BudgetPlannerContext : DbContext
    {
        public BudgetPlannerContext(DbContextOptions<BudgetPlannerContext>options) : base(options)
        {

        }
        public DbSet<Ingave> Ingaves { get; set; }
    }
}
