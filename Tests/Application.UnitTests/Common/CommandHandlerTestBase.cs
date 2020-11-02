using MakeMeRich.Application.UnitTests.Helper;
using MakeMeRich.Infrastructure.Persistance;

using Microsoft.EntityFrameworkCore;

namespace MakeMeRich.Application.UnitTests.Common
{
    public abstract class CommandHandlerTestBase
    {
        protected CommandHandlerTestBase()
        {
            DbContextOptions =
                ApplicationDbContextOptionsFactory<ApplicationDbContext>.Create();
        }

        protected DbContextOptions<ApplicationDbContext> DbContextOptions { get; set; }
    }
}
