# Klinik Veri Sınıflandırma Sistemi

ASP.NET Core MVC, Entity Framework Core ve PostgreSQL ile hazırlanmış akademik klinik veri sınıflandırma projesidir. Sistem hasta kayıtlarını, hastalara bağlı klinik verileri ve kural tabanlı otomatik sınıflandırma sonucunu yönetir.

## Teknolojiler

- ASP.NET Core MVC (.NET 8)
- Entity Framework Core
- PostgreSQL
- Bootstrap 5
- Razor View
- Code First Migration

## Veritabanı Tasarımı

Tablolar:

- `Patients`: Hasta bilgileri
- `ClinicalRecords`: Hastalara ait klinik kayıtlar
- `ClinicalClasses`: Sınıflandırma kategorileri

İlişkiler:

- Bir hasta birden fazla klinik kayda sahip olabilir.
- `ClinicalRecords.PatientId`, `Patients.Id` alanına foreign key olarak bağlıdır.
- `ClinicalRecords.ClinicalClassId`, `ClinicalClasses.Id` alanına foreign key olarak bağlıdır.

## Klasör Yapısı

```text
KlinikVeriSiniflandirmaSistemi/
├── Controllers/
│   ├── DashboardController.cs
│   ├── PatientsController.cs
│   ├── ClinicalRecordsController.cs
│   └── ClinicalClassesController.cs
├── Data/
│   ├── ApplicationDbContext.cs
│   └── SeedData.cs
├── Models/
│   ├── Patient.cs
│   ├── ClinicalRecord.cs
│   ├── ClinicalClass.cs
│   └── ViewModels/
│       └── DashboardViewModel.cs
├── Services/
│   ├── IClassificationService.cs
│   └── ClassificationService.cs
├── Views/
│   ├── Dashboard/
│   ├── Patients/
│   ├── ClinicalRecords/
│   ├── ClinicalClasses/
│   └── Shared/
├── wwwroot/
│   ├── css/
│   ├── js/
│   └── lib/
├── Migrations/
├── appsettings.json
├── Program.cs
└── KlinikVeriSiniflandirmaSistemi.csproj
```

## PostgreSQL Bağlantı Ayarı

`appsettings.json` içindeki varsayılan bağlantı:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=klinik_veri_db;Username=postgres;Password=123456"
  }
}
```

## Docker ile PostgreSQL

Projede PostgreSQL için `docker-compose.yml` dosyası bulunur. Container ayarları uygulamanın connection string'i ile uyumludur.

```bash
docker compose up -d postgres
dotnet ef database update
dotnet run
```

Container bilgileri:

- Container adı: `klinik-veri-postgres`
- Database: `klinik_veri_db`
- Username: `postgres`
- Password: `123456`
- Port: `5432`

Container durumunu kontrol etmek için:

```bash
docker compose ps postgres
```

Veritabanı container'ını durdurmak için:

```bash
docker compose stop postgres
```

## Migration Komutları

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Bu projede migration dosyaları oluşturulmuştur. PostgreSQL servisi çalışır durumdayken veritabanını oluşturmak için şu komut yeterlidir:

```bash
dotnet ef database update
```

## Çalıştırma

```bash
dotnet restore
dotnet build
dotnet ef database update
dotnet run
```

Uygulama açıldığında varsayılan rota `Dashboard/Index` sayfasıdır.

## Sınıflandırma Mantığı

`Services/ClassificationService.cs` dosyasında kural tabanlı sınıflandırma vardır.

- Nabız `< 50` veya `> 120` ise `Acil`
- Kan şekeri `> 180` ise anormal değer sayısı artar
- Vücut sıcaklığı `> 38` ise anormal değer sayısı artar
- Büyük tansiyon `>= 140` veya küçük tansiyon `>= 90` ise anormal değer sayısı artar
- Kolesterol `> 240` ise anormal değer sayısı artar
- Anormal değer sayısı `>= 2` ise `Acil`
- Tanı veya notlarda `kronik` ya da `takip` geçiyorsa `Kronik Takip Gerekli`
- Tek anormal değer varsa `Riskli`
- Anormal değer yoksa `Normal`

## Örnek Seed Data

Örnek hasta, klinik kayıt ve klinik sınıf verileri `ApplicationDbContext.OnModelCreating` içinde `HasData` ile migration'a dahil edilmiştir. Ayrıca `Data/SeedData.cs` dosyasında runtime seed örneği de bulunur.

## Modüller

- Dashboard: Özet kartlar, sınıf istatistikleri ve son 5 klinik kayıt
- Hasta Yönetimi: Listeleme, arama, ekleme, güncelleme, detay ve silme
- Klinik Kayıt Yönetimi: CRUD, sınıf/risk/tarih filtreleme ve otomatik sınıflandırma
- Sınıflandırma Kategorileri: CRUD ve kategori bazlı kayıt sayıları

## Not

`dotnet ef database update` komutunun çalışması için PostgreSQL servisinin `localhost:5432` üzerinde çalışıyor olması ve `postgres` kullanıcısının şifresinin connection string ile uyumlu olması gerekir.
