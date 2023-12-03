using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace TP3.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Movie>? Movies { get; set; }
        public DbSet<Genre>? Genres { get; set; }

        public DbSet<Customer>? custumors { get; set; }
        public DbSet<Membershiptype>? Membershiptypes { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {

            base.OnModelCreating(model);
            string GenreJSon = File.ReadAllText("GenreSeedData.json");
            List<Genre>? genres = System.Text.Json.JsonSerializer.Deserialize<List<Genre>>(GenreJSon);
            //Seed to categorie
            foreach (Genre genre in genres)
                model.Entity<Genre>().HasData(genre);
        }

    }
}
