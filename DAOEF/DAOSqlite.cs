using Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAOEF
{
    public class DAOSqlite : DbContext, IDAO
    {
        // DbSet pozwala na interakcję z bazą danych
        public DbSet<BO.Producer> Producers { get; set; }
        public DbSet<BO.Game> Games { get; set; }

        // Połączenie z bazą danych
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Filename=D:\Studia\Projekty\Projekt Wizualne\repo\DAOEF\games_producers.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BO.Game>()
                .HasOne(c => c.Producer)
                .WithMany(p => p.Games)
                .HasForeignKey(c => c.ProducerId)
                .IsRequired();
        }


        public IProducer CreateNewProducer()
        {
            return new BO.Producer();
        }

        public void AddProducer(IProducer producer)
        {
            BO.Producer p = producer as BO.Producer;
            Producers.Add(p);
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return Producers;
        }

        public void UpdateProducer(IProducer producer)
        {
            throw new NotImplementedException();
        }

        public void RemoveProducer(IProducer producer)
        {
            Producers.Remove(producer as BO.Producer);
        }
        

        public IGame CreateNewGame()
        {
            return new BO.Game();
        }

        public void AddGame(IGame game)
        {
            BO.Game g = game as BO.Game;
            Games.Add(g);
        }

        public IEnumerable<IGame> GetAllGames()
        {
            var games = Games.Include("Producer").ToList();
            return games;
        }

        public void UpdateGame(IGame game)
        {
            throw new NotImplementedException();
        }

        public void RemoveGame(IGame game)
        {
            Games.Remove(game as BO.Game);
        }

        void IDAO.SaveChanges()
        {
            this.SaveChanges();
        }

        public void UndoChanges()
        {
            foreach (var entry in this.ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Modified)
                    entry.State = EntityState.Unchanged;
                this.SaveChanges();
            }
        }
    }
}
