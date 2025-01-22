using Restaurants.Infrastructure.Extenstions;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Application.Extenstions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Host.UseSerilog((context, configuration) =>
    configuration
    .ReadFrom.Configuration(context.Configuration));
    

var app = builder.Build();

// Add Seeder - some values to work with and test the API
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
await seeder.Seed();


// Configure the HTTP request pipeline.
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
