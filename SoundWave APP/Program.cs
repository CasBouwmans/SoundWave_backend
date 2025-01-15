using App.Data;
using Microsoft.EntityFrameworkCore;
using Services;


var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseMySql(
//        builder.Configuration.GetConnectionString("DefaultConnection"), // Haal de verbindingstring uit de appsettings.json
//        new MySqlServerVersion(new Version(8, 0, 27)) // Specificeer de versie van MySQL (pas aan afhankelijk van jouw versie)
//    )
//);

// Voeg de DbContext toe aan de dependency injection container
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("SoundwaveDb"));

// Repositories en services registreren
builder.Services.AddScoped<ReviewRepository>();
builder.Services.AddTransient<ReviewService>();

// Voeg CORS-configuratie toe
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", builder => builder
        .WithOrigins("http://localhost:3000")  // Poort van je frontend
        .AllowAnyHeader()
        .AllowAnyMethod());
});


// Add services to the container
builder.Services.AddControllers();

// Swagger voor API-documentatie
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// Gebruik CORS voor je app
app.UseCors("AllowLocalhost");


//app.UseHttpsRedirection();

app.Urls.Add("http://0.0.0.0:7283");  // Luister op poort 8080
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();  // Zet deze bovenaan
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();