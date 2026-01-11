using Microsoft.EntityFrameworkCore;
using Net9RestApi.Data;
using Net9RestApi.Repositories;
using Net9RestApi.Services;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Servislerin Eklenmesi ---

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// --- Business Logic Servisleri ---
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IArticleService, ArticleService>(); // YENİ EKLENDİ

var app = builder.Build();

// --- 2. Middleware Pipeline ---

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();