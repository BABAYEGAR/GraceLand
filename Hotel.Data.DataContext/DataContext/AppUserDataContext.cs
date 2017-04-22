using Hotel.Data.Objects.Entities;
using System.Data.Entity;
namespace Hotel.Data.DataContext.DataContext
{
    public class AppUserDataContext : DbContext
    {
        // Your context has been configured to use a 'AppUserDataContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Hotel.Data.DataContext.DataContext.AppUserDataContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'AppUserDataContext' 
        // connection string in the application configuration file.
        public AppUserDataContext()
            : base("name=Hotel")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
    }
}