using Microsoft.EntityFrameworkCore;
using Net9RestApi.Data;
using Net9RestApi.Repositories;
using Net9RestApi.Services;
using Net9RestApi.Endpoints; // Minimal API için gerekli

var builder = WebApplication.CreateBuilder(args);

// --- 1. Servislerin Eklenmesi (Dependency Injection) ---

// Controller desteğini ekle
builder.Services.AddControllers();

// Swagger / OpenAPI yapılandırması (Test arayüzü için)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Veritabanı Bağlantısı (SQLite)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repository Servisi Kaydı (Generic)
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Business Services (İş Mantığı Katmanı)
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// --- 2. HTTP İstek Hattı (Middleware Pipeline) ---

// Geliştirme ortamındaysak Swagger'ı aktif et
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// --- 3. Endpoint Tanımlamaları ---

// Klasik Controller Endpoint'lerini bağla
app.MapControllers();

// Minimal API Endpoint'lerini bağla (UserEndpoints.cs içindeki metot)
app.MapUserEndpoints();

app.Run();