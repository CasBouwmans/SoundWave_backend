using App.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Connection string instellen
string connectionString = "Server=host.docker.internal,3306;Database=soundwave;Uid=root;Pwd=";

// Repositories en services registreren
builder.Services.AddSingleton(new App.Data.ReviewRepository(connectionString));
builder.Services.AddTransient<Services.ReviewService>();

// Voeg de DbContext toe aan de dependency injection container
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"))); // Zorg ervoor dat je een juiste connection string hebt

// Voeg CORS-configuratie toe
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        policy => policy.WithOrigins("http://localhost:3000") // Sta verzoeken van de frontend toe
                        .AllowAnyHeader()    // Sta alle headers toe
                        .AllowAnyMethod());  // Sta alle HTTP-methoden toe
});


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

// Gebruik CORS voor je app
app.UseCors("AllowLocalhost");

app.UseHttpsRedirection();

app.Urls.Add("http://0.0.0.0:7283");  // Luister op poort 7283
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();  // Zet deze bovenaan
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseAuthorization();

app.MapControllers();

app.Run();
