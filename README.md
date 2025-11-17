# ShopApp_Case_2

Bu repo .NET tabanlı bir projedir (backend + frontend). Aşağıda yerel geliştirme, Docker ile çalıştırma, migration için adımlar bulunmaktadır.

---

## Gereksinimler

* Git
* .NET 9.0.
* Visual Studio 2022 - VS Code 
* Next.js
* Docker

---

## 1. Repo'yu klonlama

```bash
git clone https://github.com/MCanGumus/ShopApp_Case_2.git
cd ShopApp_Case_2
```

---

## 2. Tüm projeleri restore ve build

```bash
dotnet restore
dotnet build -c Debug
```

Konsolda çıkan URL'leri takip edin (`http://localhost:5193` veya `https://localhost:7072`).

### Frontend çalıştırma (varsa)

```bash
cd src/ShopApp.UI/shop-app
npm install
npm start # veya npm run dev 
```
Konsolda çıkan URL'leri takip edin (`http://localhost:3000`).

---

## 3. Visual Studio ile (GUI)

1. Visual Studio'yu açın.
2. Repo kökündeki `.sln` dosyasını açın (ör. `Kayra-Export-Case-2.sln`).
3. Çözümü restore edin (Visual Studio otomatik olarak yapar) ve `F5` ile çalıştırın.

---

## 4. Docker ile çalıştırma: proje kökünde docker-compose.yml var. Buradan PostgreSQL, Redis, pgadmin ve redisinsight image'ları bulunmakta.

```bash

cd /Kayra-Export-Case-2
docker-compose up
```

DB ve Redis ayağa kalktıktan sonra db'lere migration yapmanız gerekiyor.

```bash
# 2 adet UI projesi var. Herhangi birini startup project olarak seçip onun klasöründe
dotnet ef migrations add Initial --project ../ShopApp.Infrastructure
dotnet ef database update --project ../ShopApp.Infrastructure
```

---

Bağlantı stringlerini projeler içerisindeki .env dosyasından sistem çekiyor herhangi bir değişiklik yapmanıza gerek yok.

---

Her şey ayağa kalktıktan sonra localhost:3000/user/register adresine gidin. Kayıt olduktan sonra localhost:3000/user/login adresinden giriş yapıp /products sayfasına yönlendirileceksiniz.

