using APIEscola5.Context;
using APIEscola5.Models;
using APIEscola5.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APIEscola5.Repositories;

public class SalaRepository : Repository<Sala>, ISalaRepository
{
    public SalaRepository(AppDbContext context) : base(context) { }
}
