
using Microsoft.EntityFrameworkCore;
using PustokProject.CoreModels;

namespace PustokProject.Persistance
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Slider> Sliders {  get; set; }
        public DbSet<Book> Books {  get; set; }
        public DbSet<Brand> Brands{  get; set; }
        public DbSet<Category> Categories{  get; set; }
        public DbSet<BookImage> BookImages{  get; set; }
        public DbSet<BookAuthor> BookAuthors{  get; set; }
        public DbSet<Author> Authors{  get; set; }
        public DbSet<Blog> Blogs{  get; set; }
        public DbSet<Tag> Tags{  get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                //.UseSqlServer(@"Server=DESKTOP-MV8SC5T\SQLEXPRESS;Database=PracticePustok04;TrustServerCertificate=True;Encrypt=False;Trusted_Connection=True");
                .UseSqlServer(@"Server=localhost;Database=Pustok4;User Id=SA;Password=Vugar2003Vs$");
        }
    }
}
