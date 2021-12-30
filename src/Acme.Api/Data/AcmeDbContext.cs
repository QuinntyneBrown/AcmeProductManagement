using Acme.Api.Models;
using Acme.Api.Core;
using Acme.Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace Acme.Api.Data
{
    public class AcmeDbContext: DbContext, IAcmeDbContext
    {
        public DbSet<Product> Products { get; private set; }
        public AcmeDbContext(DbContextOptions options)
            :base(options) { }

    }
}
