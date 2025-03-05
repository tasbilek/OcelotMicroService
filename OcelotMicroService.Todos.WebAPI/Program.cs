using Microsoft.EntityFrameworkCore;
using OcelotMicroService.Todos.WebAPI.Context;
using OcelotMicroService.Todos.WebAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseInMemoryDatabase("TodoDb");
});
var app = builder.Build();

app.MapGet("/todos/create", (string work, ApplicationDbContext context) =>
{
    Todo todo = new()
    {
        Work = work
    };
    context.Todos.Add(todo);
    context.SaveChanges();

    return new { Message = $"{work} is created" };
});

app.MapGet("/todos/getall", (ApplicationDbContext context) =>
{
    var todos = context.Todos.ToList();
    return todos;
});

app.Run();
