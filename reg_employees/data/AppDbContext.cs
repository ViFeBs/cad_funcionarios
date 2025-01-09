using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Login> Login { get; set; }
    public DbSet<Funcionario> Funcionarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuração para Login
        modelBuilder.Entity<Login>()
            .HasKey(l => l.IdLogin);

        modelBuilder.Entity<Login>()
            .Property(l => l.login)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.Entity<Login>()
            .Property(l => l.Senha)
            .IsRequired();

        modelBuilder.Entity<Login>()
            .HasIndex(l => l.login)
            .IsUnique();

        // Configuração para Funcionarios
        modelBuilder.Entity<Funcionario>()
            .HasKey(f => f.IdFunc);

        modelBuilder.Entity<Funcionario>()
            .Property(f => f.NomeFunc)
            .IsRequired()
            .HasMaxLength(100);

        modelBuilder.Entity<Funcionario>()
            .Property(f => f.Cpf)
            .IsRequired()
            .HasMaxLength(11);

        modelBuilder.Entity<Funcionario>()
            .HasIndex(f => f.Cpf)
            .IsUnique();

        modelBuilder.Entity<Funcionario>()
            .Property(f => f.Cargo)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.Entity<Funcionario>()
            .Property(f => f.Salario)
            .IsRequired();

        modelBuilder.Entity<Funcionario>()
            .HasOne(f => f.Login)
            .WithMany(l => l.Funcionarios)
            .HasForeignKey(f => f.IdLogin)
            .OnDelete(DeleteBehavior.Cascade);
    }
}