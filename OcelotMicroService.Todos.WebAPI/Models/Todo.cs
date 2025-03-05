using System;

namespace OcelotMicroService.Todos.WebAPI.Models;

public sealed class Todo
{
    public int Id { get; set;}
    public string Work { get; set;} = default!;
}
