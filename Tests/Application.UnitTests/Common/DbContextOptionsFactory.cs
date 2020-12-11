using System;
using MakeMeRich.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MakeMeRich.Application.UnitTests.Common
{
    public static class DbContextOptionsFactory<TContext> where TContext : DbContext, IApplicationDbContext
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
