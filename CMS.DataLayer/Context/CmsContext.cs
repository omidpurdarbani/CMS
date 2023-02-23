using System.Data.Entity;

namespace CMS.DataLayer
{
    public class CmsContext : DbContext
    {

        public DbSet<Page> Pages { get; set; }

        public DbSet<PageGroup> PageGroups { get; set; }

        public DbSet<PageComment> PageComments { get; set; }

    }
}
