using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Add AppDbContext
builder.Services.AddDbContext<TaskManagerDbContext>((serviceProvider, options) =>
    options.UseSqlServer(serviceProvider.GetRequiredService<IConfiguration>().GetConnectionString("TaskManagerDatabase")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();
