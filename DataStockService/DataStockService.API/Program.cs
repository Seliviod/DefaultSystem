using DataStockService.API.ServiceCollection;
using DataStockService.App;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(
        Assembly.GetExecutingAssembly(),
        typeof(AppAsemblyReference).Assembly
    );
});
builder.Services
    .RegisterDatabase(builder.Configuration)
    .AddKafkaProducers(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
