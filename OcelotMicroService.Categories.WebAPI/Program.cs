using Microsoft.EntityFrameworkCore;
using OcelotMicroService.Categories.WebAPI.Context;
using OcelotMicroService.Categories.WebAPI.Dtos;
using OcelotMicroService.Categories.WebAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

var app = builder.Build();

app.MapGet("/categories/getall", async (ApplicationDbContext context, CancellationToken cancellationToken) =>
{
    var categories = await context.Categories.ToListAsync(cancellationToken);
    return Results.Ok(categories);
});

app.MapPost("/categories/create", async (ApplicationDbContext context, CreateCategoryDto request, CancellationToken cancellationToken) =>
{

    var isCategoryExists = await context.Categories.AnyAsync(a => a.Name == request.Name, cancellationToken);
    if (isCategoryExists)
    {
        return Results.BadRequest(new { Message = $"Category name {request.Name} is already exists." });
    }
    Category category = new()
    {
        Name = request.Name
    };
    await context.Categories.AddAsync(category, cancellationToken);
    await context.SaveChangesAsync(cancellationToken);

    return Results.Ok(new { Message = $"{category.Name} is created" });
});

using (var scoped = app.Services.CreateScope())
{
    var srv = scoped.ServiceProvider;
    var context = srv.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}

app.Run();
