using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using RadencyWebApi.DataAccess;
using RadencyWebApi.DataAccess.Helpers;
using RadencyWebApi.Domain.Configs;
using RadencyWebApi.Domain.Interfaces;
using RadencyWebApi.Domain.Mapping;
using RadencyWebApi.Domain.Services;
using RadencyWebApi.Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        b =>
        {
            b.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});
builder.Services.AddControllers()
                .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Program>());
builder.Logging.ClearProviders();
builder.Host.UseNLog();
builder.Services.AddDbContext<DataContext>(t => t.UseInMemoryDatabase("in-memory-db"));
builder.Services.AddTransient<DataSeeder>();
builder.Services.AddScoped<IBookService, BookService>();
var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
typeAdapterConfig.Scan(typeof(MappingRegistration).Assembly);
var mapperConfig = new Mapper(typeAdapterConfig);
builder.Services.AddSingleton<IMapper>(mapperConfig);
builder.Services.Configure<SecretPhraseConfig>(builder.Configuration.GetSection("SecretPhraseConfig"));
var app = builder.Build();
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.MapControllers();
app.UseMiddleware<ErrorMiddleware>();
var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
using var scope = scopedFactory.CreateScope();
var dataSeeder = scope.ServiceProvider.GetService<DataSeeder>();
await dataSeeder.SeedAsync();

app.Run();