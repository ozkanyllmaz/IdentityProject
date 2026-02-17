# 📧 MendyAdmin - Gelişmiş Email Yönetim Sistemi

ASP.NET Core 8.0 ve Microsoft Identity kütüphanesi kullanılarak geliştirilmiş, gerçek zamanlı mail gönderimi, zengin metin editörü ve çoklu dosya yönetimi özelliklerine sahip kapsamlı bir web uygulaması.

---

## ✨ Özellikler

### 🔐 Kimlik Doğrulama & Güvenlik

- **Microsoft Identity Entegrasyonu** — Kayıt, giriş ve güvenli oturum yönetimi
- **İki Faktörlü Doğrulama (2FA)** — Ekstra güvenlik katmanı
- **Email Doğrulama** — MailKit & MimeKit ile SMTP üzerinden doğrulama kodu gönderimi
- **[Authorize] Koruması** — Yetkisiz erişime karşı sayfa güvenliği

### 📬 Mesaj Yönetimi

- **Gelen Kutusu (Inbox)** — Alınan mesajları görüntüleme ve yönetme
- **Gönderilenler (Sendbox)** — Gönderilen mesajların takibi
- **Yıldızlı Mesajlar** — Önemli mesajları işaretleme
- **Çöp Kutusu** — Silinen mesajların geçici depolanması
- **Gerçek Mail Gönderimi** — SMTP (Gmail vb.) üzerinden gerçek email iletimi

### 📎 Dosya & Ek Yönetimi

- **Çoklu Dosya Yükleme** — Dropzone.js ile sürükle-bırak desteği
- **GUID ile Benzersiz Depolama** — Sunucuda çakışmasız dosya isimlendirme
- **Dosya İndirme** — Ekli dosyaları kolayca indirme

### 🖊️ Zengin İçerik

- **Summernote Editörü** — Mesaj içeriklerini HTML formatında zenginleştirme
- **Kategori Sistemi** — Mesajları kategorilere göre düzenleme

---

## 🛠️ Teknoloji Yığını

### Backend

- **ASP.NET Core 8.0** — Web framework (MVC pattern)
- **Entity Framework Core** — ORM
- **MS SQL Server** — Veritabanı
- **Microsoft Identity** — Kullanıcı yönetimi ve kimlik doğrulama
- **MailKit & MimeKit** — Email gönderimi ve MIME işlemleri

### Frontend

- **Razor Views** — Server-side rendering
- **Bootstrap 4** — Responsive UI (Mendy Admin Template)
- **jQuery & Ajax** — Dinamik işlemler
- **Dropzone.js** — Sürükle-bırak dosya yükleme
- **Summernote** — Zengin metin editörü

---

## 📂 Ekran Görüntüleri

<table>
  <tr>
    <td><img src="https://github.com/user-attachments/assets/0b4464ab-8d52-42a6-b208-80c7455cbd14" width="480"/></td>
    <td><img src="https://github.com/user-attachments/assets/f11776df-a516-41f1-98ab-b9e716e077b7" width="480"/></td>
  </tr>
  <tr>
    <td><img src="https://github.com/user-attachments/assets/5be93d39-b7e4-443e-9e83-5db452e7393f" width="480"/></td>
    <td><img src="https://github.com/user-attachments/assets/d0a9bcc2-d2a2-4b70-824d-8833b07664a9" width="480"/></td>
  </tr>
  <tr>
    <td><img src="https://github.com/user-attachments/assets/d6e64e8d-dd21-4fd3-8b44-d51a125c3b47" width="480"/></td>
    <td><img src="https://github.com/user-attachments/assets/f6beba96-c9da-4485-8af2-c4813ccda6d0" width="480"/></td>
  </tr>
  <tr>
    <td><img src="https://github.com/user-attachments/assets/4a35db51-29af-405b-a8e3-22c6ad3f39ad" width="480"/></td>
    <td><img src="https://github.com/user-attachments/assets/f029155a-c483-4ead-ae0d-cd9f24692a4b" width="480"/></td>
  </tr>
  <tr>
    <td><img src="https://github.com/user-attachments/assets/50dcd71e-9191-43e3-818d-c96dfdbe21ef" width="480"/></td>
    <td><img src="https://github.com/user-attachments/assets/fe59195f-8c42-4f69-8d14-720f473c38d4" width="480"/></td>
  </tr>
  <tr>
    <td colspan="2" align="center">
      <img src="https://github.com/user-attachments/assets/2083455f-7479-4a44-be55-5b5d5f6af3e8" width="700"/>
    </td>
  </tr>
</table>

---

## 🚀 Başlangıç

### Gereksinimler

- .NET 8.0 SDK veya üzeri
- SQL Server 2019 veya üzeri
- Visual Studio 2022 (önerilir)
- Gmail veya SMTP destekli bir email hesabı (uygulama şifresi gerekli)

### Kurulum Adımları

1. **Projeyi klonla**

```bash
git clone https://github.com/ozkanyllmaz/MendyAdmin.git
cd MendyAdmin
```

2. **Veritabanı bağlantısını yapılandır**

`appsettings.json` dosyasındaki `DefaultConnection` stringini kendi SQL Server adresine göre güncelle:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=MendyAdminDb;Trusted_Connection=True;"
}
```

3. **Gerekli paketleri yükle**

```bash
dotnet restore
```

4. **Migration ile tabloları oluştur**

Package Manager Console üzerinden:

```
update-database
```

veya terminal ile:

```bash
dotnet ef database update
```

5. **SMTP ayarlarını güncelle**

`RegisterController` ve `SendMessage` metodlarındaki Gmail SMTP bilgilerini kendi uygulama şifrenle güncelle.

6. **Dosya izinlerini kontrol et**

`wwwroot/messageFiles` klasörünün yazma iznine sahip olduğundan emin ol.

7. **Uygulamayı başlat**

```bash
dotnet run
```

8. **Tarayıcıda aç**

```
https://localhost:7000
```

---

## ⚙️ Yapılandırma Notları

### SMTP (Gmail) Kurulumu

Gmail hesabında **Uygulama Şifresi** oluşturman gerekiyor:

1. Google Hesabı → Güvenlik → 2 Adımlı Doğrulama'yı etkinleştir
2. Uygulama Şifreleri bölümünden yeni bir şifre oluştur
3. Oluşturulan şifreyi controller'daki SMTP ayarlarına ekle

### Dosya Yükleme

Yüklenen ekler `wwwroot/messageFiles/` klasörüne GUID formatında kaydedilir:

```
wwwroot/
└── messageFiles/
    ├── a3f2c1d4-xxxx-xxxx-xxxx-xxxxxx.pdf
    └── b7e9f2a1-xxxx-xxxx-xxxx-xxxxxx.png
```

---

## 🔒 Güvenlik Özellikleri

- ✅ Microsoft Identity ile şifreli kullanıcı yönetimi
- ✅ İki Faktörlü Doğrulama (2FA) desteği
- ✅ Email doğrulama akışı
- ✅ `[Authorize]` attribute ile sayfa koruması
- ✅ CSRF koruması (ASP.NET Core built-in)
- ✅ SQL Injection önlemi (EF Core parametrize sorgular)
- ✅ GUID ile güvenli dosya depolama

---

## 🗄️ Veritabanı Modelleri

```
AppUser        - Identity kullanıcı modeli
Messages       - Gönderilen ve alınan mesajlar
Attachments    - Mesajlara eklenen dosyalar
Categories     - Mesaj kategorileri
```

## 👨‍💻 Geliştirici

**Özkan Yılmaz**  
Kocaeli Üniversitesi — Bilgisayar Mühendisliği  
Junior Software Developer (.NET & C#)

📎 LinkedIn: [linkedin.com/in/devozkanyilmaz](https://www.linkedin.com/in/devozkanyilmaz/)

---

## 📄 Lisans

Bu proje MIT Lisansı altında sunulmaktadır.

---

**Son Güncellenme:** Ocak 2026  
**Sürüm:** 1.0.0