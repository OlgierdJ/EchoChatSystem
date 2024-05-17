using Microsoft.EntityFrameworkCore;

namespace OAuthServer.Context
{
    public class OAuthDbContext : DbContext
    {
        public OAuthDbContext(DbContextOptions<OAuthDbContext> options) : base(options)
        {

        }
    }
}
