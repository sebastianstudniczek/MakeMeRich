using System;

using MakeMeRich.Application.Common.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace MakeMeRich.Application.UnitTests.Helper
{
    public static class ApplicationDbContextOptionsFactory<TContext> where TContext : DbContext, IApplicationDbContext
    {
        public static DbContextOptions<TContext> Create()
        {
            var options = new DbContextOptionsBuilder<TContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .Options;

            return options;
        }
    }
}
