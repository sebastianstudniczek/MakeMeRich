using System;
using System.Threading;
using System.Threading.Tasks;

using MakeMeRich.Application.Common.Interfaces;

namespace MakeMeRich.Application
{
    public class CreateInternalTransactionCommandHandler
    {
        private readonly IApplicationDbContext _context;

        public CreateInternalTransactionCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateInternalTransactionCommand command, CancellationToken none)
        {
            throw new NotImplementedException();
        }
    }
}