
using Microsoft.EntityFrameworkCore;

namespace EasyInventory.Models
{
    public class Inventorycontext : DbContext
    {
        public Inventorycontext(DbContextOptions<Inventorycontext> options) :base(options)
        {
            
        }
        public DbSet<Inventory> Inventories { get; set; }
    }
}
