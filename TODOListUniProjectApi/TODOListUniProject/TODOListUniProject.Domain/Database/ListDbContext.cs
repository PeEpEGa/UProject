using Microsoft.EntityFrameworkCore;
using TODOListUniProject.Contracts.Database;

namespace TODOListUniProject.Domain.Database;

public class ListDbContext : DbContext
{
    public DbSet<Objective> Objectives { get; set; }

    public ListDbContext() : base()
    {
    }

    public ListDbContext(DbContextOptions<ListDbContext> options) : base(options)
    {
    }
}