# Plan-It

Sade ve hızlı bir plan yönetim uygulaması. Kanban panosu üzerinden görevlerini takip et, öncelik ve son tarih belirle.

ASP.NET Core 8 (MVC) · Entity Framework Core · SQLite · Identity · Bootstrap 5

---

## Özellikler

- **Kanban panosu** — sürükle-bırak ile durum değiştirme (Yapılacak / Devam / Tamamlandı / İptal)
- **Öncelik seviyeleri** — düşük, orta, yüksek
- **Son tarih takibi** — geçmiş tarihli görevler otomatik vurgulanır
- **Kategoriler** — planları gruplayabilmek için (admin yönetir)
- **Admin paneli** — kullanıcı listesi, rol değiştirme, kullanıcı silme + sistem istatistikleri
- **Identity tabanlı yetkilendirme** — kayıt, giriş, hesap kilitleme

## Hızlı başlangıç

```bash
git clone <repo-url>
cd Plan-It

# Veritabanını oluştur (SQLite, ./Plan_It.db)
dotnet ef database update

# Admin şifresini user-secrets ile ayarla (committen güvenli kalır)
dotnet user-secrets init
dotnet user-secrets set "AdminUser:Password" "Senin-Gizli-Sifren!"

# Çalıştır
dotnet run
```

Varsayılan admin: `admin@example.com` (şifre yukarıda set ettiğin).

## Yapılandırma

`appsettings.json` içindeki tüm gizli değerler `dotnet user-secrets` veya environment
variables ile override edilmelidir. Dosyada üretim sırrı bırakma.

## Mimari

```
Controllers/    HTTP endpoint'leri
Services/       İş mantığı (IPlanService, ICategoryService)
Repository/     EF Core veri erişimi
Models/         Domain + ViewModel sınıfları
Areas/Identity/ ASP.NET Identity scaffold (Login/Register/Logout)
wwwroot/css/    Tasarım sistemi (site, landing, kanban, auth)
```

Repository → Service → Controller akışı; her katman kendi sorumluluğunda.
Yetkilendirme `userId` parametresiyle her katmanda zorunlu — bir kullanıcı
başkasının planına asla ulaşamaz.

## Güvenlik

- CSRF koruması (`[ValidateAntiForgeryToken]`) tüm POST endpoint'lerinde
- Content Security Policy + X-Frame-Options + nosniff header'ları
- Brute-force koruması: 5 başarısız denemeden sonra 15 dakika kilit
- Cookie: HttpOnly + SameSite=Lax + sliding 8h expiration
- Şifre politikası: min 8 karakter, büyük/küçük/rakam zorunlu

## Lisans

MIT
