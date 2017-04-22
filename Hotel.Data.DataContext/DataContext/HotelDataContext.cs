using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Data.Objects.Entities;

namespace Hotel.Data.DataContext.DataContext
{
    public class HotelDataContext : DbContext
    {
        // Your context has been configured to use a 'HotelDataContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Hotel.Data.DataContext.DataContext.HotelDataContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'HotelDataContext' 
        // connection string in the application configuration file.
        public HotelDataContext()
            : base("name=Hotel")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<ItemCategory> ItemCategories { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemLog> ItemLogs { get; set; }
        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
    }
}
