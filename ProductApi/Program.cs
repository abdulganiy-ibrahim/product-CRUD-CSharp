using ProductApi.Data;

var builder = WebApplication.CreateBuilder(args);

// get database connectionStrings
var connectionString = builder.Configuration.GetConnectionString("ProductDB")
    ?? throw new InvalidOperationException("Connection string 'ProductDB' not found.");

// Register ProductRepository with the DI Container
builder.Services.AddScoped<ProductRepository>(sp => new ProductRepository(connectionString));

// Register Controllers so ASP.NET Core discovers and activates them
builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Map incomming request to the controller
app.MapControllers();

app.Run();
