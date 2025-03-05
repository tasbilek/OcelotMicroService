using Microsoft.EntityFrameworkCore;
using OcelotMicroService.Categories.WebAPI.Models;

namespace OcelotMicroService.Categories.WebAPI.Context;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Category> Categories { get; set;}
}
