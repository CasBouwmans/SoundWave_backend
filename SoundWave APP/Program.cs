using App.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Connection string instellen
string connectionString = "Server=localhost;Database=soundwave;Uid=root;Pwd=";

// Repositories en services registreren
builder.Services.AddSingleton(new App.Data.ReviewRepository(connectionString));
builder.Services.AddTransient<Services.ReviewService>();

// Voeg de DbContext toe aan de dependency injection container
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"))); // Zorg ervoor dat je een juiste connection string hebt

// Add services to the container
builder.Services.AddControllers();

// Swagger voor API-documentatie
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
