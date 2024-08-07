using food_delivery.Models;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace food_delivery.Repository;



internal class ApplicationDbContext: IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Cart> Carts{ get; set; }
    public DbSet<Category> Categories{ get; set; }
    public DbSet<Coupon> Coupons{ get; set; }
    public DbSet<Item> Items{ get; set; }
    public DbSet<OrderDetails> OrderDetails{ get; set; }
    public DbSet<OrderHeader> OrderHeaders { get; set; }
    public DbSet<Subcategory> Subcategories{ get; set; }
    public DbSet<Review> Reviews { get; set; }
}
