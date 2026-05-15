# Keylogger Tespit Sistemi - Defensive Malware Analysis Tool

ASP.NET Core MVC, Entity Framework Core ve PostgreSQL ile hazırlanmış savunma amaçlı öğrenci projesidir.

Bu proje keylogger yazmaz, tuş kaydı almaz, canlı process izlemez ve zararlı kod üretmez. Sadece manuel girilen veya seed edilen process/log bilgilerini detection rule kayıtlarına göre analiz eder.

## Çalıştırma

PostgreSQL Docker container'ını başlatın:

```powershell
docker compose up -d
```

Veritabanı host portu:

```text
localhost:55432
```

Migration uygulamak için:

```powershell
dotnet ef database update
```

Uygulamayı çalıştırmak için:

```powershell
dotnet run
```

Varsayılan kullanıcı:

```text
E-posta: admin@local.test
Şifre: Admin123!
```

## Ana Sayfalar

- Login
- Dashboard
- Yeni Analiz
- Analiz Listesi
- Analiz Detay
- Detection Rules Yönetimi
- Kullanıcı Yönetimi
- Raporlar
- Etik Kullanım

## Risk Sınıflandırması

- 0-30: Düşük Risk
- 31-60: Orta Risk
- 61-100: Yüksek Risk

## Mimari

- `Models`: Entity ve enum sınıfları
- `Data`: EF Core `ApplicationDbContext` ve seed data
- `Services`: OOP iş kuralları, analiz, auth, rapor ve dashboard servisleri
- `Controllers`: MVC controller akışları
- `ViewModels`: Form ve ekran modelleri
- `Views`: Razor UI sayfaları
