using Microsoft.EntityFrameworkCore;
using Summary.API.Models;

namespace Summary.API.Services
{
    public class SummaryContext : DbContext
    {
        public SummaryContext(DbContextOptions<SummaryContext> options) : base(options)
        {

        }

        public DbSet<Project> Projects { get; set; }
    }
}