using Restaurants.Infrastructure.Extenstions;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Application.Extenstions;
using Serilog;
using Restaurants.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<RequestTimeLoggingMiddleware>();

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
app.UseMiddleware<ErrorHandlingMiddleware>(); //the error handling middleware
app.UseMiddleware<RequestTimeLoggingMiddleware>(); //Timer Middleware

// Configure the HTTP request pipeline.
app.UseSerilogRequestLogging();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
