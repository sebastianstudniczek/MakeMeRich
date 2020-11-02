using MakeMeRich.Application.UnitTests.Helper;

using Microsoft.EntityFrameworkCore;

namespace MakeMeRich.Application.UnitTests.Common
{
    public class CommandTestBase
    {
        public CommandTestBase()
        {
            DbContextOptions = ApplicationDbContextOptionsFactory.Create();
        }

        public DbContextOptions DbContextOptions { get;}
    }
}
