using MakeMeRich.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace MakeMeRich.Application.UnitTests.Common
{
    public abstract class HandlerTest
    {
        protected HandlerTest()
        {
            DbContextOptions =
                DbContextOptionsFactory<ApplicationDbContext>.Create();
        }

        protected DbContextOptions<ApplicationDbContext> DbContextOptions { get;}
    }
}
