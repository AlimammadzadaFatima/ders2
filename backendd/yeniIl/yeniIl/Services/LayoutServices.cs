using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using yeniIl.Models;

namespace yeniIl.Services
{
    public class LayoutServices
    {
        private readonly DataContext _context;

        public LayoutServices(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Setting>> GetSettings()
        {
            return await _context.settings.ToListAsync();
        }
        
    }
}
