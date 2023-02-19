using Microsoft.EntityFrameworkCore;
using RadencyWebApi.DataAccess;
using RadencyWebApi.DataAccess.Helpers;
using RadencyWebApi.Domain.Interfaces;
using RadencyWebApi.Domain.Mapping;
using RadencyWebApi.Domain.Services;
using RadencyWebApi.Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(t => t.UseInMemoryDatabase("in-memory-db"));
builder.Services.AddTransient<DataSeeder>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));


var app = builder.Build();
app.UseHttpsRedirection();
app.MapControllers();
app.UseMiddleware<ErrorMiddleware>();
var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
using var scope = scopedFactory.CreateScope();
var dataSeeder = scope.ServiceProvider.GetService<DataSeeder>();
dataSeeder.Seed();

app.Run();