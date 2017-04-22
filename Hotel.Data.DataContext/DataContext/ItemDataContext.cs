using Hotel.Data.Objects.Entities;
using System.Data.Entity;

namespace Hotel.Data.DataContext.DataContext
{
    public class ItemDataContext : DbContext
    {
        // Your context has been configured to use a 'ItemDataContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Hotel.Data.DataContext.DataContext.ItemDataContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ItemDataContext' 
        // connection string in the application configuration file.
        public ItemDataContext()
            : base("name=Hotel")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemCategory> ItemCategories { get; set; }
        public virtual DbSet<ItemLog> ItemLogs { get; set; }
    }
}