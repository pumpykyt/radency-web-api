using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using RadencyWebApi.DataAccess;
using RadencyWebApi.DataAccess.Helpers;
using RadencyWebApi.Domain.Configs;
using RadencyWebApi.Domain.Interfaces;
using RadencyWebApi.Domain.Mapping;
using RadencyWebApi.Domain.Services;
using RadencyWebApi.Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
                .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Program>());
builder.Services.AddDbContext<DataContext>(t => t.UseInMemoryDatabase("in-memory-db"));
builder.Services.AddTransient<DataSeeder>();
builder.Services.AddScoped<IBookService, BookService>();
var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
typeAdapterConfig.Scan(typeof(MappingRegistration).Assembly);
var mapperConfig = new Mapper(typeAdapterConfig);
builder.Services.AddSingleton<IMapper>(mapperConfig);
builder.Services.Configure<SecretPhraseConfig>(builder.Configuration.GetSection("SecretPhraseConfig"));
var app = builder.Build();
app.UseHttpsRedirection();
app.MapControllers();
app.UseMiddleware<ErrorMiddleware>();
var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
using var scope = scopedFactory.CreateScope();
var dataSeeder = scope.ServiceProvider.GetService<DataSeeder>();
dataSeeder.Seed();

app.Run();