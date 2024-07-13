using Microsoft.EntityFrameworkCore;

namespace Echo.Chat.Web.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }
}
