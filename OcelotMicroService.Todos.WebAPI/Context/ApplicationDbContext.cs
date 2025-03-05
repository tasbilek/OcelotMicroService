using System;
using Microsoft.EntityFrameworkCore;
using OcelotMicroService.Todos.WebAPI.Models;

namespace OcelotMicroService.Todos.WebAPI.Context;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Todo> Todos {get; set;}
}
