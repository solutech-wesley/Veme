using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Veme.Models.POCO;

namespace Veme.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Merchant Merchant { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<MerchantAddress> MerchantAddresses { get; set; }

        public DbSet<Offer> Offers { get; set; }

        public DbSet<ProductionCode> ProductionCodes { get; set; }

        public DbSet<CouponRepository> CouponRepositories { get; set; }

        public DbSet<CheckPoint> CheckPoints { get; set; }

        public DbSet<StripeCustomer> StripeCustomers { get; set; }

        //This is responsible for fluent API
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Implement many-to-many relationship between merchant and merchant Address
            modelBuilder.Entity<Merchant>()
                        .HasMany<MerchantAddress>(c => c.Addresses)
                        .WithMany(c => c.Merchants)
                        .Map(c =>
                        {
                            c.ToTable("MerchantLocations");
                        });

            //Implement many-to-many relationship between merchant and category
            modelBuilder.Entity<Merchant>()
                        .HasMany<Category>(c => c.Categories)
                        .WithMany(c => c.Merchants)
                        .Map(c => c.ToTable("MerchantCategories"));

            //sets the one-zero/one relationship between user and customer
            modelBuilder.Entity<ApplicationUser>()
                        .HasOptional(c => c.Customer)
                        .WithRequired(c => c.User)
                        .WillCascadeOnDelete();

            //sets the one-zero/one relationship between user and Merchant
            modelBuilder.Entity<ApplicationUser>()
                        .HasOptional(c => c.Merchant)
                        .WithRequired(c => c.User)
                        .WillCascadeOnDelete();



            base.OnModelCreating(modelBuilder);
        }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}