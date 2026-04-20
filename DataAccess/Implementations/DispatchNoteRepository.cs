using DataAccess.Domain;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Implementations
{
    internal class DispatchNoteRepository(ApplicationDbContext _context, IConfiguration _configuration) : IDispatchNoteRepository
    {
        
    }
}
