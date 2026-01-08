using Microsoft.EntityFrameworkCore;
using Net9RestApi.Data;
using Net9RestApi.Repositories;
using Microsoft.Extensions.Hosting; // IsDevelopment() için gerekli
using Microsoft.AspNetCore.Builder; // WebApplication için gerekli

var builder = WebApplication.CreateBuilder(args);

// --- 1. Servislerin Eklenmesi (Dependency Injection) ---

// Controller desteğini ekle
builder.Services.AddControllers();

// Swagger / OpenAPI yapılandırması (Test arayüzü için)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Veritabanı Bağlantısı (SQLite)
// appsettings.json dosyasındaki "DefaultConnection" ayarını okur.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repository Servisi Kaydı
// Generic olduğu için typeof ile tanımlıyoruz.
// Scoped: Her HTTP isteği için yeni bir nesne üretir (Web API için en uygunu).
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

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

// Controller'ları eşleştir (Endpoint'leri aktif et)
app.MapControllers();

app.Run();