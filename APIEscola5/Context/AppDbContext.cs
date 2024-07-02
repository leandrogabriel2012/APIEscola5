using APIEscola5.Models;
using Microsoft.EntityFrameworkCore;

namespace APIEscola5.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Turma> Turmas { get; set; }
    public DbSet<Sala> Salas { get; set; }
}
