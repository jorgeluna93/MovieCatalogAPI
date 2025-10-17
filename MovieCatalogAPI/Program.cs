using Microsoft.EntityFrameworkCore;
using MovieCatalogAPI.DAL;
using MovieCatalogAPI.Repositories.Implementations;
using MovieCatalogAPI.Repositories.Interfaces;
using MovieCatalogAPI.Services.Implementations;
using MovieCatalogAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
// Modification: Add services to the container.
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IMovieService, MovieService>();

// Add services to the container.
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"])
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// Modification: Add seed an ensure migration
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate(); // Ensure DB is created and migrations applied
    DbSeeder.Seed(context);     // Seed data
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


