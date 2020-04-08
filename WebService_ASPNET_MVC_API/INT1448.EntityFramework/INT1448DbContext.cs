using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INT1448.Core;

namespace INT1448.EntityFramework
{
    public class INT1448DbContext : DbContext
    {
        public INT1448DbContext() 
            : base("INT1448ConnectionString")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }

    }
}
