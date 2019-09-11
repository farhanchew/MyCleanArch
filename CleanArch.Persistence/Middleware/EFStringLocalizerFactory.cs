using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Persistence.Middleware
{

    public class EFStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly ApplicationDbContext _context;

        public EFStringLocalizerFactory(ApplicationDbContext context)
        {
            _context = context;
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            return new EFStringLocalizer(_context);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new EFStringLocalizer(_context);
        }
    }
}
