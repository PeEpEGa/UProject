using Microsoft.EntityFrameworkCore;
using TODOListUniProject.Domain.Database;

namespace TODOListUniProject.UnitTests.Helpers;

internal static class DbContextHelper
{
    public static ListDbContext CreateTestDb()
    {
        var tempFile = Path.GetTempFileName();
        return CreateTestDb($"Data Source={tempFile};");
    }
    public static ListDbContext CreateTestDb(string connectionString)
    {
        var tempFile = Path.GetTempFileName();
        var options = new DbContextOptionsBuilder<ListDbContext>().UseSqlite(connectionString).Options;
        
        var dbContext = new ListDbContext(options);
        dbContext.Database.Migrate();

        return dbContext;
    }
}