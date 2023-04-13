using Microsoft.EntityFrameworkCore;
using PracticaDB2TCB.Model;

namespace PracticaDB2TCB.DB;

public class MovimientoDBContext : DbContext
{
    private readonly ConnectionModel db;
    
    public DbSet<Movimiento> Movimiento { get; set; }
    
    public MovimientoDBContext(ConnectionModel db)
    {
        this.db = db;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL(db.ConnectionString);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.ToTable("Movimiento");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Value).HasColumnName("valor");
        });
    }
}