using System;

using MakeMeRich.Infrastructure.Persistance;

using Microsoft.EntityFrameworkCore;

namespace MakeMeRich.Application.UnitTests.Helper
{
    public static class ApplicationDbContextOptionsFactory
    {
        public static DbContextOptions Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return options;
        }
    }
}
