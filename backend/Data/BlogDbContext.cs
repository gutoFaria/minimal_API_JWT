using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {}

        public DbSet<User> Users => Set<User>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Post> Posts => Set<Post>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new 
                {
                    CategoryId = 1,
                    CategoryName = "Tecnologia"
                },
                new 
                {
                    Id = 2,
                    CategoryName = "Inovação"
                },
                new 
                {
                    Id = 3,
                    CategoryName = "Curiosidades"
                }
            );

            modelBuilder.Entity<User>().HasData(
                new 
                {
                    UserName = "guto_admin",
                    EmailAddress = "guto.admin@email.com",
                    Password = "guto_12345",
                    GiceName="Guto",
                    SurName ="Faria",
                    Role ="Administrator"
                },

                new 
                {
                    UserName = "tah_standard",
                    EmailAddress = "tah@email.com",
                    Password = "tah_12345",
                    GiceName="tah",
                    SurName ="sw",
                    Role ="Standard"
                }
            );

            modelBuilder.Entity<Post>().HasData(
                new
                {
                    PostId = 1,
                    Title = "Nova era dos computadores",
                    ImageUrl = "https://th.bing.com/th/id/R.5f4fc95b6b0ff166056090c17c4f1d0d?rik=PTrLf29qr%2bho7A&riu=http%3a%2f%2f3.bp.blogspot.com%2f-WLfPX7AkodU%2fTV50hKfU32I%2fAAAAAAAAANk%2f-ouLn4B4kHk%2fs1600%2fgeracao.JPG&ehk=5E91Tm%2br8pj2bGjK2%2fBZwLRghAhFJwu1KBLNx%2f5VaF4%3d&risl=&pid=ImgRaw&r=0&sres=1&sresct=1",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    PublishDate = new DateTime(),
                    CategoryId = 1
                },

                new
                {
                    PostId = 2,
                    Title = "Internet",
                    ImageUrl = "https://th.bing.com/th/id/OIP.j1CPjOH7NZPpnzUL8jXIrgHaFk?w=253&h=190&c=7&r=0&o=5&pid=1.7",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    PublishDate = new DateTime(),
                    CategoryId = 1
                },

                new
                {
                    PostId = 3,
                    Title = "Energia Solar",
                    ImageUrl = "https://th.bing.com/th/id/OIP.SP_GdnGhzXztUQ1y_L3m1QHaE8?w=228&h=180&c=7&r=0&o=5&pid=1.7",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    PublishDate = new DateTime(),
                    CategoryId = 2
                },

                new
                {
                    PostId = 4,
                    Title = "Energia Eólica",
                    ImageUrl = "https://th.bing.com/th/id/OIP.A3yCPtQmWObNBs6fMUGv2wHaE6?w=247&h=180&c=7&r=0&o=5&pid=1.7",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    PublishDate = new DateTime(),
                    CategoryId = 2
                }

            );
        }
    }
}